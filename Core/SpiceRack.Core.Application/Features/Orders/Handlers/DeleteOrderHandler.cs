using System;
using System.Net;
using AutoMapper;
using MediatR;
using SpiceRack.Core.Application.Exceptions;
using SpiceRack.Core.Application.Features.Orders.Commands;
using SpiceRack.Core.Application.Interfaces.Repositories;
using SpiceRack.Core.Application.Wrappers;

namespace SpiceRack.Core.Application.Features.Orders.Handlers;

public class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand, Response<int>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderDishRepositoty _orderDishRepository;
    private readonly IMapper _mapper;

    public DeleteOrderHandler(IOrderRepository orderRepository, 
                              IOrderDishRepositoty orderDishRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _orderDishRepository = orderDishRepository;
        _mapper = mapper;
    }
    public async Task<Response<int>> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.Id);
        if (order == null) throw new ApiException("Order not found", (int)HttpStatusCode.NotFound);

        await _orderRepository.DeleteAsync(order);

        return new Response<int>(order.Id);
    }
}
