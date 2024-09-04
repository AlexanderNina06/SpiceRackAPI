using AutoMapper;
using MediatR;
using SpiceRack.Core.Application.DTOs.Responses.IngredientResponses;
using SpiceRack.Core.Application.Features.Ingredients.Commands;
using SpiceRack.Core.Application.Interfaces.Repositories;
using SpiceRack.Core.Application.Wrappers;
using SpiceRack.Core.Domain.Entities;

namespace SpiceRack.Core.Application.Features.Ingredients.Handlers;

public class CreateIngredientHandler : IRequestHandler<CreateIngredientCommand, Response<GetIngredientResponse>>
{
    private readonly IIngredientRepository _ingredientRepository;
    private readonly IMapper _mapper;

    public CreateIngredientHandler(IIngredientRepository ingredientRepository, IMapper mapper)
    {
      _ingredientRepository = ingredientRepository;
      _mapper = mapper;
    }
    public async Task<Response<GetIngredientResponse>> Handle(CreateIngredientCommand request, CancellationToken cancellationToken)
    {
        var ingredient = _mapper.Map<Ingredient>(request.IngredientRequest);
        await _ingredientRepository.AddAsync(ingredient); 

        var response = _mapper.Map<GetIngredientResponse>(ingredient);

        return new Response<GetIngredientResponse>(response);
        
    }
}
