using System;
using MediatR;
using SpiceRack.Core.Application.DTOs.Requests.TableRequests;
using SpiceRack.Core.Application.DTOs.Responses.TableResponses;
using SpiceRack.Core.Application.Wrappers;

namespace SpiceRack.Core.Application.Features.Tables.Commands;

public class UpdateTableCommand : IRequest<Response<GetTableResponse>>
{
public UpdateTableRequest TableRequest { get; set; }

    public UpdateTableCommand(UpdateTableRequest tableRequest)
    {
        TableRequest = tableRequest;
    }
}
