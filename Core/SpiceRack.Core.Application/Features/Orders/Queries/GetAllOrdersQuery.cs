using System;
using MediatR;
using SpiceRack.Core.Application.DTOs.Responses.OrderResponses;
using SpiceRack.Core.Application.Wrappers;

namespace SpiceRack.Core.Application.Features.Orders.Queries;

public class GetAllOrdersQuery : IRequest<Response<IList<GetOrderResponse>>>
{
  
}
