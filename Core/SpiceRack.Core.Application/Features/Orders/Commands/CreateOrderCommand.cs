using System;
using MediatR;
using SpiceRack.Core.Application.DTOs.Requests.OrderRequests;
using SpiceRack.Core.Application.DTOs.Requests.TableRequests;
using SpiceRack.Core.Application.DTOs.Responses.OrderResponses;
using SpiceRack.Core.Application.Wrappers;

namespace SpiceRack.Core.Application.Features.Orders.Commands;

public class CreateOrderCommand : IRequest<Response<GetOrderResponse>>
{
  public CreateOrderRequest CreateOrderRequest{ get; set; }

    public CreateOrderCommand(CreateOrderRequest createOrderRequest)
    {
        CreateOrderRequest = createOrderRequest;
    }
}
