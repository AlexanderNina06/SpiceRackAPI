using System;
using System.Net.Security;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Design;
using SpiceRack.Core.Application.DTOs.Responses;
using SpiceRack.Core.Application.DTOs.Responses.DishResponses;
using SpiceRack.Core.Application.DTOs.Responses.IngredientResponses;
using SpiceRack.Core.Application.DTOs.Responses.OrderResponses;
using SpiceRack.Core.Application.DTOs.Responses.TableResponses;
using SpiceRack.Core.Domain.Entities;

namespace SpiceRack.Core.Application.Mappings;

public class DomainToResponse : Profile
{
    public DomainToResponse()
    {
      
    CreateMap<Ingredient, GetIngredientResponse>()
    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
    .ReverseMap()
    .ForMember(dest => dest.Created, opt => opt.Ignore())
    .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
    .ForMember(dest => dest.LastModified, opt => opt.Ignore())
    .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore());

    CreateMap<Table, GetTableResponse>()
    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
    .ReverseMap()
    .ForMember(dest => dest.Created, opt => opt.Ignore())
    .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
    .ForMember(dest => dest.LastModified, opt => opt.Ignore())
    .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore());

    CreateMap<Dish, GetDishResponse>()
    .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.ToString()))
    .ReverseMap()
    .ForMember(dest => dest.Created, opt => opt.Ignore())
    .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
    .ForMember(dest => dest.LastModified, opt => opt.Ignore())
    .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore());

    CreateMap<Order, GetOrderResponse>()
    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
    .ForMember(dest => dest.TableNumber, opt => opt.MapFrom(src => src.TableId))
    .ReverseMap()
    .ForMember(dest => dest.Created, opt => opt.Ignore())
    .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
    .ForMember(dest => dest.LastModified, opt => opt.Ignore())
    .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore());

    }

}
