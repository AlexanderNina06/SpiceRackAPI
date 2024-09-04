using MediatR;
using SpiceRack.Core.Application.DTOs.Responses;
using SpiceRack.Core.Application.DTOs.Responses.IngredientResponses;
using SpiceRack.Core.Application.Wrappers;

namespace SpiceRack.Core.Application.Features.Ingredients.Queries;

public class GetAllIngredientsQuery : IRequest<Response<IList<GetIngredientResponse>>>
{

}
