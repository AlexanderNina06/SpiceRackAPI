using System;
using System.Net;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SpiceRack.Core.Application.DTOs.Responses.IngredientResponses;
using SpiceRack.Core.Application.Exceptions;
using SpiceRack.Core.Application.Features.Ingredients.Queries;
using SpiceRack.Core.Application.Interfaces.Repositories;
using SpiceRack.Core.Application.Wrappers;

namespace SpiceRack.Core.Application.Features.Ingredients.Handlers;

public class GetIngredientByIdHandler : IRequestHandler<GetIngredientByIdQuery, Response<GetIngredientResponse>>
{
    private readonly IIngredientRepository _ingredientRepository;
    private readonly IMapper _mapper;

    public GetIngredientByIdHandler(IIngredientRepository ingredientRepository, IMapper mapper)
    {
      _ingredientRepository = ingredientRepository;
      _mapper = mapper;
    }
    public async Task<Response<GetIngredientResponse>> Handle(GetIngredientByIdQuery request, CancellationToken cancellationToken)
    {
        var ingredient = await _ingredientRepository.GetByIdAsync(request.Id);
        if(ingredient == null)
        {
          throw new ApiException("ingredient not found", (int)HttpStatusCode.NotFound);
        }
        
        var response = _mapper.Map<GetIngredientResponse>(ingredient);
        return new Response<GetIngredientResponse>(response);
    }
}
