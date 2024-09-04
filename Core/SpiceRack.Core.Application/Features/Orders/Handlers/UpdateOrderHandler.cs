using System;
using System.Net;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SpiceRack.Core.Application.Exceptions;
using SpiceRack.Core.Application.Features.Orders.Commands;
using SpiceRack.Core.Application.Interfaces.Repositories;
using SpiceRack.Core.Application.Wrappers;
using SpiceRack.Core.Domain.Entities;

namespace SpiceRack.Core.Application.Features.Orders.Handlers;

public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, Response<bool>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderDishRepositoty _orderDishRepository;
    private readonly IMapper _mapper;

    public UpdateOrderHandler(IOrderRepository orderRepository, 
                              IOrderDishRepositoty orderDishRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _orderDishRepository = orderDishRepository;
        _mapper = mapper;
    }
    public async Task<Response<bool>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.updateOrderRequest.Id);

        if (order == null) throw new ApiException("Order not found",(int)HttpStatusCode.NotFound);

        var existingDishes = await _orderDishRepository.GetByOrderIdAsync(order.Id);
        var dishesIdsToAdd = request.updateOrderRequest.Dishes
        .Except(existingDishes.Select(order => order.DishId)).ToList();

        var dishesIdsToRemove = existingDishes
        .Where(order => !request.updateOrderRequest.Dishes.Contains(order.DishId))
        .Select(order => order.DishId);

        foreach(var dishesIds in dishesIdsToAdd)
        {
          var dishesToAdd = new OrderDish
          {
            DishId = dishesIds,
            OrderId = order.Id
          };
          await _orderDishRepository.AddAsync(dishesToAdd);
        }

        foreach(var dishesIds in dishesIdsToRemove)
        {
          var dishesToRemove = await _orderDishRepository.GetOrderDishesAsync(order.Id, dishesIds);
          if(dishesToRemove != null)
          {
            await _orderDishRepository.DeleteAsync(dishesToRemove);
          } 
        }

        return new Response<bool>(true);
    }
}
