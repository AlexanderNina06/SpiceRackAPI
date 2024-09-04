using System;
using MediatR;
using SpiceRack.Core.Application.DTOs.Requests.TableRequests;
using SpiceRack.Core.Application.DTOs.Responses.TableResponses;
using SpiceRack.Core.Application.Wrappers;

namespace SpiceRack.Core.Application.Features.Tables.Commands;

public class CreateTableCommand : IRequest<Response<GetTableResponse>>
{
    public CreateTableRequest TableRequest {get; }

    public CreateTableCommand(CreateTableRequest tableRequest)
    {
      TableRequest = tableRequest;
    }
}
