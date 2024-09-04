using System;
using MediatR;
using SpiceRack.Core.Application.DTOs.Requests.OrderRequests;
using SpiceRack.Core.Application.Wrappers;

namespace SpiceRack.Core.Application.Features.Orders.Commands;

public class UpdateOrderCommand : IRequest<Response<bool>>
{
public UpdateOrderRequest updateOrderRequest;

    public UpdateOrderCommand(UpdateOrderRequest updateOrderRequest)
    {
        this.updateOrderRequest = updateOrderRequest;
    }
}
