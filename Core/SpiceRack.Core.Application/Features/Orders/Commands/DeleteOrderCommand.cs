using System;
using MediatR;
using SpiceRack.Core.Application.Wrappers;

namespace SpiceRack.Core.Application.Features.Orders.Commands;

public class DeleteOrderCommand : IRequest<Response<int>>
{
    public int Id { get; set; }
    public DeleteOrderCommand(int id)
    {
        Id = id;
    }
}
