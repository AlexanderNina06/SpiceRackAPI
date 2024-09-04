using System;
using MediatR;
using SpiceRack.Core.Application.DTOs.Responses.DishResponses;
using SpiceRack.Core.Application.Wrappers;

namespace SpiceRack.Core.Application.Features.Dishes.Queries;

public class GetDishByIdQuery : IRequest<Response<GetDishResponse>>
{
   public int Id { get;}
    public GetDishByIdQuery(int id)
    {
        Id = id;
    }

}
