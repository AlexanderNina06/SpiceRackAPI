using System;
using MediatR;
using SpiceRack.Core.Application.DTOs.Requests.DishRequests;
using SpiceRack.Core.Application.DTOs.Responses.DishResponses;
using SpiceRack.Core.Application.Wrappers;

namespace SpiceRack.Core.Application.Features.Dishes.Commands;

public class CreateDishCommand : IRequest<Response<GetDishResponse>>
{
  public CreateDishRequest DishRequest{ get; set; }
    public CreateDishCommand(CreateDishRequest dishRequest)
    {
        DishRequest = dishRequest;
    }

}
