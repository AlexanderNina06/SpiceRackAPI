using System;
using MediatR;
using SpiceRack.Core.Application.Wrappers;

namespace SpiceRack.Core.Application.Features.Ingredients.Commands;

public class DeleteIngredientByIdCommand : IRequest<Response<int>>
{
    public int Id { get; set; }

    public DeleteIngredientByIdCommand(int id)
    {
        Id = id;
    }
}
