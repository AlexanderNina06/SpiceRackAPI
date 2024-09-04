using System;
using MediatR;
using SpiceRack.Core.Application.DTOs.Responses.TableResponses;
using SpiceRack.Core.Application.Wrappers;

namespace SpiceRack.Core.Application.Features.Tables.Queries;

public class GetTableOrderQuery : IRequest<Response<IList<GetTableOrderResponse>>>
{
public int Id { get; set; }
public GetTableOrderQuery(int id)
{
  Id = id;
}
}
