using System;
using MediatR;
using SpiceRack.Core.Application.DTOs.Responses.OrderResponses;
using SpiceRack.Core.Application.Wrappers;

namespace SpiceRack.Core.Application.Features.Orders.Queries;

public class GetOrderByIdQuery : IRequest<Response<GetOrderResponse>>
{
    public int Id { get;}
    public GetOrderByIdQuery(int id)
    {
        Id = id;
    }
}
