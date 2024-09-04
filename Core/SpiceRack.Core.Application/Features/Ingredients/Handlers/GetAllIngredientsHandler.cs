using AutoMapper;
using MediatR;
using SpiceRack.Core.Application.DTOs.Responses;
using SpiceRack.Core.Application.DTOs.Responses.IngredientResponses;
using SpiceRack.Core.Application.Features.Ingredients.Queries;
using SpiceRack.Core.Application.Interfaces.Repositories;
using SpiceRack.Core.Application.Wrappers;

namespace SpiceRack.Core.Application.Features.Ingredients.Handlers;

public class GetAllIngredientsHandler : IRequestHandler<GetAllIngredientsQuery, Response<IList<GetIngredientResponse>>>
{
    private readonly IIngredientRepository _ingredientRepository;
    private readonly IMapper _mapper;

    public GetAllIngredientsHandler(IIngredientRepository ingredientRepository, IMapper mapper)
    {
      _ingredientRepository = ingredientRepository;
      _mapper = mapper;
    }

    public async Task<Response<IList<GetIngredientResponse>>> Handle(GetAllIngredientsQuery request, CancellationToken cancellationToken)
    {
        var IngredientList = await _ingredientRepository.GetAllAsync();

        var response = _mapper.Map<List<GetIngredientResponse>>(IngredientList);
        return new Response<IList<GetIngredientResponse>>(response);
    }
    
}
