using System;
using System.Net;
using AutoMapper;
using MediatR;
using SpiceRack.Core.Application.DTOs.Responses.TableResponses;
using SpiceRack.Core.Application.Exceptions;
using SpiceRack.Core.Application.Features.Tables.Queries;
using SpiceRack.Core.Application.Interfaces.Repositories;
using SpiceRack.Core.Application.Wrappers;

namespace SpiceRack.Core.Application.Features.Tables.Handlers;

public class GetTableOrderHandler : IRequestHandler<GetTableOrderQuery, Response<IList<GetTableOrderResponse>>>
{
    private readonly ITableRepository _tableRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public GetTableOrderHandler(ITableRepository tableRepository, IOrderRepository orderRepository, IMapper mapper)
    {
        _tableRepository = tableRepository;
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<Response<IList<GetTableOrderResponse>>> Handle(GetTableOrderQuery request, CancellationToken cancellationToken)
    {
      var table = await _tableRepository.GetByIdAsync(request.Id);
      if (table == null)
         throw new ApiException($"No table found with the Id: {request.Id}",(int)HttpStatusCode.NotFound);

      var ordersByTableId = await _orderRepository.GetOrdersByTableId(request.Id);
      if (ordersByTableId == null) 
        throw new ApiException("No orders found for this table",(int)HttpStatusCode.NotFound);

      var response = ordersByTableId.Select(order => new GetTableOrderResponse
      {
        TableNumber = table.Id,
        OrderNumber = order.Id,
        Subtotal = order.Subtotal,
        Status = order.Status.ToString(),
        Dishes = order.OrderDishes.Select(di => di.Dish.Name).ToList()

      }).ToList();

      return new Response<IList<GetTableOrderResponse>>(response);
      
    }
}
