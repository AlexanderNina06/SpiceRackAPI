using System.Net;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SpiceRack.Core.Application.Exceptions;
using SpiceRack.Core.Application.Features.Ingredients.Commands;
using SpiceRack.Core.Application.Interfaces.Repositories;
using SpiceRack.Core.Application.Wrappers;

namespace SpiceRack.Core.Application.Features.Ingredients.Handlers;

public class DeleteIngredientHandler : IRequestHandler<DeleteIngredientByIdCommand, Response<int>>
{
    private readonly IIngredientRepository _ingredientRepository;
    private readonly IMapper _mapper;

    public DeleteIngredientHandler(IIngredientRepository ingredientRepository, IMapper mapper)
    {
      _ingredientRepository = ingredientRepository;
      _mapper = mapper;
    }
    public async Task<Response<int>> Handle(DeleteIngredientByIdCommand request, CancellationToken cancellationToken)
    {
        var ingredient = await _ingredientRepository.GetByIdAsync(request.Id);
        if (ingredient == null) throw new  ApiException("Ingredient not found",(int)HttpStatusCode.NotFound);

        await _ingredientRepository.DeleteAsync(ingredient);

        return new Response<int>(ingredient.Id);

    }
}
