using System;
using System.Net;
using AutoMapper;
using MediatR;
using SpiceRack.Core.Application.DTOs.Responses.OrderResponses;
using SpiceRack.Core.Application.Exceptions;
using SpiceRack.Core.Application.Features.Orders.Queries;
using SpiceRack.Core.Application.Interfaces.Repositories;
using SpiceRack.Core.Application.Wrappers;

namespace SpiceRack.Core.Application.Features.Orders.Handlers;

public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, Response<GetOrderResponse>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public GetOrderByIdHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async  Task<Response<GetOrderResponse>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetAllOrdersWhithDishesByIdAsync(request.Id);
        if (order == null) throw new ApiException("Order not found",(int)HttpStatusCode.NotFound);

        var response = new GetOrderResponse
        {
          Id = order.Id,
          TableNumber = order.Id,
          Subtotal = order.Subtotal,
          Status = order.Status.ToString(),
          Dishes = order.OrderDishes.Select(di => di.Dish.Name).ToList() 
        };

        return new Response<GetOrderResponse>(response);
    }
}
