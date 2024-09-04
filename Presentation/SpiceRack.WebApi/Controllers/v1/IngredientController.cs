using System.Net.Mime;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpiceRack.Core.Application.DTOs.Requests.IngredientRequests;
using SpiceRack.Core.Application.Features.Ingredients.Commands;
using SpiceRack.Core.Application.Features.Ingredients.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace SpiceRack.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize(Roles = "Admin")]
    [SwaggerTag("Ingredient Management")]
    public class IngredientController : BaseApiController
    {
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(
            Summary = "Create Ingredient",
            Description = "Creates a new Ingredient in the system."
        )]
        public async Task<IActionResult> CreateIngredient([FromBody] CreateIngredientRequest request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

           
                var command = new CreateIngredientCommand(request);
                var createdIngredient = await mediator.Send(command);

                return Ok();
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Get All Ingredients",
            Description = "Retrieves a list of all dishes in the system."
        )]
        public async Task<IActionResult> GetIngredients()
        {
            var query = new GetAllIngredientsQuery();
            var result = await mediator.Send(query);
            if (result == null)
            {
                return NoContent();
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Get Ingredient by ID",
            Description = "Retrieves a specific ingredient by its ID."
        )]
        public async Task<IActionResult> GetIngredientById(int id)
        {
            var query = new GetIngredientByIdQuery(id);
            var result = await mediator.Send(query);

            if(result == null)
            {
                return NotFound();
            }
            
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(
        Summary = "Update an existing ingredient",
        Description = "Updates an existing ingredient in the system."
        )]
        public async Task<IActionResult> UpdateIngredient(int id, [FromBody] UpdateIngredientRequest request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            if(id != request.Id)
            {
                return BadRequest();
            }
            var command = new UpdateIngredientCommand(request);
            var result = await mediator.Send(command);
            
            if(result.Succeded)
            {
                return NoContent();
            }
            
            return NotFound(result.Message);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
        Summary = "Delete an ingredient",
        Description = "Deletes an ingredient from the system."
        )]
        public async Task<IActionResult> Delete(int id)
        {   
            await mediator.Send(new DeleteIngredientByIdCommand(id));
            return NoContent();   
        }
    }
}
