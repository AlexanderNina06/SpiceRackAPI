using System;
using System.Net;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.VisualBasic;
using SpiceRack.Core.Application.DTOs.Responses.DishResponses;
using SpiceRack.Core.Application.Exceptions;
using SpiceRack.Core.Application.Features.Dishes.Commands;
using SpiceRack.Core.Application.Interfaces.Repositories;
using SpiceRack.Core.Application.Wrappers;
using SpiceRack.Core.Domain.Entities;
using SpiceRack.Core.Domain.Enums;

namespace SpiceRack.Core.Application.Features.Dishes.Handlers;

public class CreateDishHandler : IRequestHandler<CreateDishCommand, Response<GetDishResponse>>
{
  private readonly IDishRepository _dishRepository;
  private readonly IIngredientRepository _ingredientRepository;
  private readonly IDishIngredientRepository _dishIngredientRepository;

  private readonly IMapper _mapper;

    public CreateDishHandler(IDishRepository dishRepository, IMapper mapper,
                            IDishIngredientRepository dishIngredientRepository, 
                            IIngredientRepository ingredientRepository)
    {
        _dishRepository = dishRepository;
        _mapper = mapper;
        _dishIngredientRepository = dishIngredientRepository;
        _ingredientRepository = ingredientRepository;
    }

    public async Task<Response<GetDishResponse>> Handle(CreateDishCommand request, CancellationToken cancellationToken)
    {
      var dish = _mapper.Map<Dish>(request.DishRequest);

      if(request.DishRequest.Category.ToLower() == "Appetizer") dish.Category = DishTypes.Appetizer;
      if(request.DishRequest.Category.ToLower() == "MainCourse") dish.Category = DishTypes.MainCourse;
      if(request.DishRequest.Category.ToLower() == "Dessert") dish.Category = DishTypes.Dessert;
      if(request.DishRequest.Category.ToLower() == "Drink") dish.Category = DishTypes.Drink;
      
      await _dishRepository.AddAsync(dish);

      foreach(var IngredientId in request.DishRequest.DishIngredients)
      {
        var dishIngredient = new DishIngredient
        {
          DishId = dish.Id,
          IngredientId = IngredientId
        };

        await _dishIngredientRepository.AddAsync(dishIngredient);
      }
      var response = _mapper.Map<GetDishResponse>(dish);

      var listOfIngredients = await _ingredientRepository.GetAllAsync();
      var ingredientsNames = listOfIngredients
      .Where(i => request.DishRequest.DishIngredients.Contains(i.Id))
      .Select(i => i.Name);

      var ingredientResponse = new List<string>();

      foreach(var ing in ingredientsNames)
      {
        ingredientResponse.Add(ing);
      }

      response.DishIngredients = ingredientResponse;
  
      return new Response<GetDishResponse>(response);
      
    }
}
