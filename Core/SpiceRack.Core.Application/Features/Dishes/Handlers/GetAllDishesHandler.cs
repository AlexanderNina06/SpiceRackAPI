using System;
using AutoMapper;
using MediatR;
using SpiceRack.Core.Application.DTOs.Responses.DishResponses;
using SpiceRack.Core.Application.Features.Dishes.Queries;
using SpiceRack.Core.Application.Interfaces.Repositories;
using SpiceRack.Core.Application.Wrappers;

namespace SpiceRack.Core.Application.Features.Dishes.Handlers;

public class GetAllDishesHandler : IRequestHandler<GetAllDishesQuery, Response<IList<GetDishResponse>>>
{
  private readonly IDishRepository _dishRepository;
  private readonly IMapper _mapper;

    public GetAllDishesHandler(IDishRepository dishRepository, IMapper mapper)
    {
        _dishRepository = dishRepository;
        _mapper = mapper;
    }

    public async Task<Response<IList<GetDishResponse>>> Handle(GetAllDishesQuery request, CancellationToken cancellationToken)
    {
      var dishesList = await _dishRepository.GetAllDishesWhithIngredientsAsync();
      
      var response = dishesList.Select(dish => new GetDishResponse
      {
        Id = dish.Id,
        Name = dish.Name,
        Price = dish.Price,
        Servings = dish.Servings,
        Category = dish.Category.ToString(),
        DishIngredients = dish.DishIngredients.Select(di => di.Ingredient.Name).ToList()

      }).ToList();

      return new Response<IList<GetDishResponse>>(response);

    }
}
