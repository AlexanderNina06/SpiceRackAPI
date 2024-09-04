using System;
using AutoMapper;
using MediatR;
using SpiceRack.Core.Application.DTOs.Responses.OrderResponses;
using SpiceRack.Core.Application.Features.Orders.Queries;
using SpiceRack.Core.Application.Interfaces.Repositories;
using SpiceRack.Core.Application.Wrappers;

namespace SpiceRack.Core.Application.Features.Orders.Handlers;

public class GetAllOrdersHandler : IRequestHandler<GetAllOrdersQuery, Response<IList<GetOrderResponse>>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public GetAllOrdersHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }
    public async Task<Response<IList<GetOrderResponse>>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
      var orderList = await _orderRepository.GetAllOrdersWhithDishesAsync();
       
       var response = orderList.Select(order => new GetOrderResponse
      {
        Id = order.Id,
        TableNumber = order.TableId,
        Subtotal = order.Subtotal,
        Status = order.Status.ToString(),
        Dishes = order.OrderDishes.Select(di => di.Dish.Name).ToList()

      }).ToList();

      return new Response<IList<GetOrderResponse>>(response);
    }
}
