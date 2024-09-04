using System;
using MediatR;
using SpiceRack.Core.Application.DTOs.Requests.IngredientRequests;
using SpiceRack.Core.Application.Wrappers;

namespace SpiceRack.Core.Application.Features.Ingredients.Commands;

public class UpdateIngredientCommand : IRequest<Response<bool>>
{
  public UpdateIngredientRequest Ingredient {get; }

    public UpdateIngredientCommand(UpdateIngredientRequest ingredient)
    {
        Ingredient = ingredient;
    }
}
