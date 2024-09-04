using System;
using MediatR;
using SpiceRack.Core.Application.DTOs.Requests;
using SpiceRack.Core.Application.DTOs.Requests.IngredientRequests;
using SpiceRack.Core.Application.DTOs.Responses;
using SpiceRack.Core.Application.DTOs.Responses.IngredientResponses;
using SpiceRack.Core.Application.Wrappers;

namespace SpiceRack.Core.Application.Features.Ingredients.Commands;

public class CreateIngredientCommand : IRequest<Response<GetIngredientResponse>>
{
 public CreateIngredientRequest IngredientRequest { get; }

    public CreateIngredientCommand(CreateIngredientRequest ingredientRequest)
    {
        IngredientRequest = ingredientRequest;
    }
}