using System;
using System.Net;
using AutoMapper;
using MediatR;
using SpiceRack.Core.Application.DTOs.Responses.DishResponses;
using SpiceRack.Core.Application.Exceptions;
using SpiceRack.Core.Application.Features.Dishes.Commands;
using SpiceRack.Core.Application.Interfaces.Repositories;
using SpiceRack.Core.Application.Wrappers;
using SpiceRack.Core.Domain.Entities;
using SpiceRack.Core.Domain.Enums;

namespace SpiceRack.Core.Application.Features.Dishes.Handlers;

public class UpdateDishHandler : IRequestHandler<UpdateDishCommand, Response<GetDishResponse>>
{
  private readonly IDishRepository _dishRepository;
  private readonly IDishIngredientRepository _dishIngredientRepository;
  private readonly IIngredientRepository _ingredientRepository;
  private readonly IMapper _mapper;

    public UpdateDishHandler(IDishRepository dishRepository,
                             IDishIngredientRepository dishIngredientRepository, 
                             IMapper mapper,
                             IIngredientRepository ingredientRepository)
    {
        _dishRepository = dishRepository;
        _dishIngredientRepository = dishIngredientRepository;
        _ingredientRepository = ingredientRepository;
        _mapper = mapper;
    }

    public async Task<Response<GetDishResponse>> Handle(UpdateDishCommand request, CancellationToken cancellationToken)
    {
      var dish = await _dishRepository.GetByIdAsync(request.updateDishRequest.Id);
      
      if (dish == null) throw new ApiException("Dish not found",(int)HttpStatusCode.NotFound);

      var result = _mapper.Map<Dish>(request.updateDishRequest);

      if(request.updateDishRequest.Category.ToLower() == "Appetizer") result.Category = DishTypes.Appetizer;
      if(request.updateDishRequest.Category.ToLower() == "MainCourse") result.Category = DishTypes.MainCourse;
      if(request.updateDishRequest.Category.ToLower() == "Dessert") result.Category = DishTypes.Dessert;
      if(request.updateDishRequest.Category.ToLower() == "Drink") result.Category = DishTypes.Drink;

      //Retrieves the existing ingredients associated with the dish.
      var existingIngredients = await _dishIngredientRepository.GetByDishIdAsync(dish.Id);

      //Finds ingredients present in the update request but not in the existing list using 
      var ingredientIdsToAdd = request.updateDishRequest.DishIngredients
      .Except(existingIngredients.Select(di => di.IngredientId)).ToList();
      
      //Filters existing ingredients that are not present in the update request
      var ingredientIdsToRemove = existingIngredients
      .Where(di => !request.updateDishRequest.DishIngredients.Contains(di.IngredientId))
      .Select(di => di.IngredientId)
      .ToList();
    
    //Iterates through ingredientIdsToAdd to create and add new DishIngredient objects.
    foreach (var ingredientId in ingredientIdsToAdd)
    {
      var ingridientToAdd = new  DishIngredient
       {
        DishId = dish.Id,
        IngredientId = ingredientId
      };
        await _dishIngredientRepository.AddAsync(ingridientToAdd);
    }
    
    //Iterates through ingredientIdsToRemove to fetch the specific DishIngredient objects and delete them if they exist.
    foreach (var ingredientId in ingredientIdsToRemove)
    {
        var dishIngredientToRemove = await _dishIngredientRepository.GetDishIngredientAsync(dish.Id, ingredientId);
        if (dishIngredientToRemove != null)
        {
            await _dishIngredientRepository.DeleteAsync(dishIngredientToRemove);
        }
    }

      await _dishRepository.UpdateAsync(result, dish.Id);

      var response = _mapper.Map<GetDishResponse>(dish);

      var listOfIngredients = await _ingredientRepository.GetAllAsync();
      var ingredientsNames = listOfIngredients
      .Where(i => request.updateDishRequest.DishIngredients.Contains(i.Id))
      .Select(i => i.Name);

      var ingredientResponse = new List<string>();

      foreach(var ing in ingredientsNames)
      {
        ingredientResponse.Add(ing);
      }
      response.Id = dish.Id;
      response.DishIngredients = ingredientResponse;
  
      return new Response<GetDishResponse>(response);
    }
}
