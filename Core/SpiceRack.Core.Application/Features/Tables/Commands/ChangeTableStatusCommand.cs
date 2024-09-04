using System;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using SpiceRack.Core.Application.Wrappers;
using SpiceRack.Core.Domain.Entities;


namespace SpiceRack.Core.Application.Features.Tables.Commands;

public class ChangeTableStatusCommand : IRequest<Response<Unit>>
{
public JsonPatchDocument<Table> StatusRequest { get; set; }
public int Id { get; set; }

    public ChangeTableStatusCommand(JsonPatchDocument<Table> statusRequest, int id)
    {
        StatusRequest = statusRequest;
        Id = id;
    }
}
