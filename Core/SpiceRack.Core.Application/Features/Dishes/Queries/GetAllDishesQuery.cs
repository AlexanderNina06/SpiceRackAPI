using System;
using MediatR;
using SpiceRack.Core.Application.DTOs.Responses.DishResponses;
using SpiceRack.Core.Application.Wrappers;

namespace SpiceRack.Core.Application.Features.Dishes.Queries;

public class GetAllDishesQuery : IRequest<Response<IList<GetDishResponse>>>
{

}
