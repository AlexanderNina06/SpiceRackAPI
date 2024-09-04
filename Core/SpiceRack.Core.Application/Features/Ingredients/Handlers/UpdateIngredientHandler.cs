using System;
using System.Net;
using AutoMapper;
using MediatR;
using SpiceRack.Core.Application.Exceptions;
using SpiceRack.Core.Application.Features.Ingredients.Commands;
using SpiceRack.Core.Application.Interfaces.Repositories;
using SpiceRack.Core.Application.Wrappers;
using SpiceRack.Core.Domain.Entities;

namespace SpiceRack.Core.Application.Features.Ingredients.Handlers;

public class UpdateIngredientHandler : IRequestHandler<UpdateIngredientCommand, Response<bool>>
{
    private readonly IIngredientRepository _ingredientRepository;
    private readonly IMapper _mapper;

    public UpdateIngredientHandler(IIngredientRepository ingredientRepository, IMapper mapper)
    {
      _ingredientRepository = ingredientRepository;
      _mapper = mapper;
    }
    public async Task<Response<bool>> Handle(UpdateIngredientCommand command, CancellationToken cancellationToken)
    {
        var ingredient = await _ingredientRepository.GetByIdAsync(command.Ingredient.Id);

        if (ingredient == null) throw new ApiException("Ingredient not found", (int)HttpStatusCode.NotFound);

        var result = _mapper.Map<Ingredient>(command.Ingredient);
        await _ingredientRepository.UpdateAsync(result, ingredient.Id);
        
        return new Response<bool>(true); 
        
    }
}
