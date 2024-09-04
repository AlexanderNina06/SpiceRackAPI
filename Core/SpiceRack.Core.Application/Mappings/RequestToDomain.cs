using System;
using AutoMapper;
using SpiceRack.Core.Application.DTOs.Requests;
using SpiceRack.Core.Application.DTOs.Requests.DishRequests;
using SpiceRack.Core.Application.DTOs.Requests.IngredientRequests;
using SpiceRack.Core.Application.DTOs.Requests.OrderRequests;
using SpiceRack.Core.Application.DTOs.Requests.TableRequests;
using SpiceRack.Core.Application.Features.Tables.Commands;
using SpiceRack.Core.Domain.Entities;

namespace SpiceRack.Core.Application.Mappings;

public class RequestToDomain : Profile
{
    public RequestToDomain()
    {
      CreateMap<CreateIngredientRequest, Ingredient>()
      .ForMember(dest => dest.Created, opt => opt.Ignore())
      .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
      .ForMember(dest => dest.LastModified, opt => opt.Ignore())
      .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore());

      CreateMap<UpdateIngredientRequest, Ingredient>()
      .ForMember(dest => dest.Created, opt => opt.Ignore())
      .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
      .ForMember(dest => dest.LastModified, opt => opt.Ignore())
      .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore());

      CreateMap<CreateTableRequest, Table>()
      .ForMember(dest => dest.Created, opt => opt.Ignore())
      .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
      .ForMember(dest => dest.LastModified, opt => opt.Ignore())
      .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore());

      CreateMap<UpdateTableRequest, Table>()
      .ForMember(dest => dest.Created, opt => opt.Ignore())
      .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
      .ForMember(dest => dest.LastModified, opt => opt.Ignore())
      .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore());

      CreateMap<CreateDishRequest, Dish>()
      .ForMember(dest => dest.Created, opt => opt.Ignore())
      .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
      .ForMember(dest => dest.LastModified, opt => opt.Ignore())
      .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore())
      .ForMember(dest => dest.DishIngredients, opt => opt.Ignore())
      .ForMember(dest => dest.OrderDishes, opt => opt.Ignore());

      CreateMap<UpdateDishRequest, Dish>()
      .ForMember(dest => dest.Created, opt => opt.Ignore())
      .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
      .ForMember(dest => dest.LastModified, opt => opt.Ignore())
      .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore())
      .ForMember(dest => dest.DishIngredients, opt => opt.Ignore())
      .ForMember(dest => dest.OrderDishes, opt => opt.Ignore());

      CreateMap<CreateOrderRequest, Order>()
      .ForMember(dest => dest.Created, opt => opt.Ignore())
      .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
      .ForMember(dest => dest.LastModified, opt => opt.Ignore())
      .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore())
      .ForMember(dest => dest.OrderDishes, opt => opt.Ignore())
      .ForMember(dest => dest.TableId, opt => opt.MapFrom(src => src.TableNumber));

      CreateMap<UpdateOrderRequest, Order>()
      .ForMember(dest => dest.Created, opt => opt.Ignore())
      .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
      .ForMember(dest => dest.LastModified, opt => opt.Ignore())
      .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore())
      .ForMember(dest => dest.OrderDishes, opt => opt.Ignore());

    }
}
