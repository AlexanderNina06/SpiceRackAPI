using System;
using MediatR;
using SpiceRack.Core.Application.DTOs.Requests.DishRequests;
using SpiceRack.Core.Application.DTOs.Responses.DishResponses;
using SpiceRack.Core.Application.Wrappers;

namespace SpiceRack.Core.Application.Features.Dishes.Commands;

public class UpdateDishCommand : IRequest<Response<GetDishResponse>>
{
  public UpdateDishRequest updateDishRequest{ get; set; }
    public UpdateDishCommand(UpdateDishRequest updateDishRequest)
    {
        this.updateDishRequest = updateDishRequest;
    }

}
