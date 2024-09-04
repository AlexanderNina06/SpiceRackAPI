using System;
using System.Net;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore.Storage;
using SpiceRack.Core.Application.DTOs.Responses.DishResponses;
using SpiceRack.Core.Application.Exceptions;
using SpiceRack.Core.Application.Features.Dishes.Queries;
using SpiceRack.Core.Application.Interfaces.Repositories;
using SpiceRack.Core.Application.Wrappers;

namespace SpiceRack.Core.Application.Features.Dishes.Handlers;

public class GetDishByIdHandler : IRequestHandler<GetDishByIdQuery, Response<GetDishResponse>>
{
  private readonly IDishRepository _dishRepository;
  private readonly IDishIngredientRepository _dishIngredientRepository;
  private readonly IMapper _mapper;

    public GetDishByIdHandler(IDishRepository dishRepository, IDishIngredientRepository dishIngredientRepository, IMapper mapper)
    {
        _dishRepository = dishRepository;
        _dishIngredientRepository = dishIngredientRepository;
        _mapper = mapper;
    }

    public async Task<Response<GetDishResponse>> Handle(GetDishByIdQuery request, CancellationToken cancellationToken)
    {
        var dish = await _dishRepository.GetAllDishesWhithIngredientsByIdAsync(request.Id);
        if (dish == null) throw new ApiException("dish not found", (int)HttpStatusCode.NotFound);
       
      var response = new GetDishResponse
      {
        Id = dish.Id,
        Name = dish.Name,
        Price = dish.Price,
        Servings = dish.Servings,
        Category = dish.Category.ToString(),
        DishIngredients = dish.DishIngredients.Select(di => di.Ingredient.Name).ToList()
      };

      return new Response<GetDishResponse>(response);
    }
}
