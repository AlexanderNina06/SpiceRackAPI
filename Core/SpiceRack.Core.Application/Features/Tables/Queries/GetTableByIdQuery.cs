using System;
using MediatR;
using SpiceRack.Core.Application.DTOs.Responses.TableResponses;
using SpiceRack.Core.Application.Wrappers;

namespace SpiceRack.Core.Application.Features.Tables.Queries;

public class GetTableByIdQuery : IRequest<Response<GetTableResponse>>
{
public int Id { get; set; }

    public GetTableByIdQuery(int id)
    {
        Id = id;
    }
}
