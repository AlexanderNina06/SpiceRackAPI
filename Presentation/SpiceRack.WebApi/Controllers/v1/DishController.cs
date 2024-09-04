using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpiceRack.Core.Application.DTOs.Requests.DishRequests;
using SpiceRack.Core.Application.Features.Dishes.Commands;
using SpiceRack.Core.Application.Features.Dishes.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace SpiceRack.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize(Roles = "Admin")]
    [SwaggerTag("Dish Management")]
    public class DishController : BaseApiController
    {
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(
            Summary = "Create Dish",
            Description = "Creates a new dish in the system."
        )]
        public async Task<IActionResult> CreateDish([FromBody] CreateDishRequest request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
                var command = new CreateDishCommand(request);
                var createdDish = await mediator.Send(command);

                return Ok();
        }

        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(
            Summary = "Update Dish",
            Description = "Updates an existing dish in the system."
        )]
        public async Task<IActionResult> UpdateDish(int id, [FromBody] UpdateDishRequest request)
        {
            
                if(!ModelState.IsValid)
                {
                    return BadRequest();
                }
                if(id != request.Id)
                {
                    return BadRequest();
                }
                var command = new UpdateDishCommand(request);
                return Ok(await mediator.Send(command));
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Get All Dishes",
            Description = "Retrieves a list of all dishes in the system."
        )]
        public async Task<IActionResult> GetDishes()
        {
            var query = new GetAllDishesQuery();
            var result = await mediator.Send(query);
            if (result == null)
            {
                return NoContent();
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Get Dish by ID",
            Description = "Retrieves a specific dish by its ID."
        )]
        public async Task<IActionResult> GetDishById(int id)
        {
            var query = new GetDishByIdQuery(id);
            var result = await mediator.Send(query);

              if(result == null)
            {
                return NotFound();
            }
            
            return Ok(result);
        }
    }
}
