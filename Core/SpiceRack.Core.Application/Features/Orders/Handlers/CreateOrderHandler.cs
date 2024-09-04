using System;
using System.Runtime.Intrinsics.Arm;
using AutoMapper;
using MediatR;
using SpiceRack.Core.Application.DTOs.Responses.OrderResponses;
using SpiceRack.Core.Application.Features.Orders.Commands;
using SpiceRack.Core.Application.Interfaces.Repositories;
using SpiceRack.Core.Application.Wrappers;
using SpiceRack.Core.Domain.Entities;
using SpiceRack.Core.Domain.Enums;

namespace SpiceRack.Core.Application.Features.Orders.Handlers;

public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, Response<GetOrderResponse>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IDishRepository _dishRepository;
    private readonly IOrderDishRepositoty _orderDishRepository;
    private readonly IMapper _mapper;

    public CreateOrderHandler(IOrderRepository orderRepository, IDishRepository dishRepository, 
                              IOrderDishRepositoty orderDishRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _dishRepository = dishRepository;
        _orderDishRepository = orderDishRepository;
        _mapper = mapper;
    }

    public async Task<Response<GetOrderResponse>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
      var order = _mapper.Map<Order>(request.CreateOrderRequest);
      order.Status = OrderStatus.InProgress;

      var requestedDishIds  = request.CreateOrderRequest.Dishes;

      var dishes = await  _dishRepository.GetAllAsync();
      var selectedDishes = dishes.Where(dish => requestedDishIds.Contains(dish.Id)).ToList();

      decimal subtotal = 0;
      foreach (var dish in selectedDishes)
      {
          subtotal += (decimal)dish.Price;
      }
      order.Subtotal = subtotal;
      await _orderRepository.AddAsync(order);


      foreach(var dish in selectedDishes)
      {
        var orderDish = new OrderDish
        {
          OrderId = order.Id,
          DishId = dish.Id
        };

        await _orderDishRepository.AddAsync(orderDish);
      }

      var response = _mapper.Map<GetOrderResponse>(order);
      response.Dishes = order.OrderDishes.Select(di => di.Dish.Name).ToList();
      
      return new Response<GetOrderResponse>(response);

    }
}
