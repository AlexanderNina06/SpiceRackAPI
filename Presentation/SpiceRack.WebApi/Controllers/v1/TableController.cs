using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SpiceRack.Core.Application.DTOs.Requests.TableRequests;
using SpiceRack.Core.Application.Features.Tables.Commands;
using SpiceRack.Core.Application.Features.Tables.Queries;
using SpiceRack.Core.Domain.Entities;
using SpiceRack.Core.Domain.Enums;
using Swashbuckle.AspNetCore.Annotations;

namespace SpiceRack.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [SwaggerTag("Table Management")]
    public class TableController : BaseApiController
    {
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(
            Summary = "Create Table",
            Description = "Creates a new table and sets its state to 'Available' by default."
        )]
        public async Task<IActionResult> CreateTable([FromBody] CreateTableRequest request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

                var command = new CreateTableCommand(request);
                var createdTable = await mediator.Send(command);

                return Ok();
        }

        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(
            Summary = "Update Table",
            Description = "Updates an existing table. (Only description and capacity can be changed)"
        )]
        public async Task<IActionResult> UpdateTable(int id, [FromBody] UpdateTableRequest request)
        {
                if(!ModelState.IsValid)
                {
                    return BadRequest();
                }
                if(id != request.Id)
                {
                    return BadRequest();
                }

                var command = new UpdateTableCommand(request);
                return Ok(await mediator.Send(command));
        }

        [HttpPatch("{id}/status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Waiters")]
        [SwaggerOperation(
            Summary = "Change Table Status",
            Description = "Updates the state of a table (One of these: Available, InProgress or Attended)"
        )]
        public async Task<IActionResult> ChangeStatus(int id, [FromBody] JsonPatchDocument<Table> request)
        {
            var command = new ChangeTableStatusCommand(request, id);
            await mediator.Send(command);

            return NoContent();
        }

        [HttpGet]
        [Authorize(Roles = nameof(Roles.Admin) + "," + nameof(Roles.Waiters))]
        [SwaggerOperation(
            Summary = "Get All Tables",
            Description = "Retrieves a list of all tables in the system."
        )]
        public async Task<IActionResult> GetTables()
        {
            var query = new GetAllTablesQuery();
            var result = await mediator.Send(query);
            if (result == null)
            {
                return NoContent();
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = nameof(Roles.Admin) + "," + nameof(Roles.Waiters))]
        [SwaggerOperation(
            Summary = "Get Table by ID",
            Description = "Retrieves a specific table by its ID."
        )]
        public async Task<IActionResult> GetTableById(int id)
        {
            var query = new GetTableByIdQuery(id);
            var result = await mediator.Send(query);

            if(result == null)
            {
                return NotFound();
            }
            
            return Ok(result);
        }

        [HttpGet("{id}/orders")]
        [Authorize(Roles = nameof(Roles.Waiters))]
        [SwaggerOperation(
            Summary = "Get Orders for a Table",
            Description = "Retrieves a list of orders associated with a specific table."
        )]
        public async Task<IActionResult> GetTableOrdersById(int id)
        {
            var query = new GetTableOrderQuery(id);
            var result = await mediator.Send(query);

            if(result == null)
            {
                return NotFound();
            }
            
            return Ok(result);
        }

    }
}
