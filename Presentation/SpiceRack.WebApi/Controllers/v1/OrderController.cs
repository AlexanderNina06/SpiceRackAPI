using System;
using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpiceRack.Core.Application.DTOs.Requests.OrderRequests;
using SpiceRack.Core.Application.Features.Orders.Commands;
using SpiceRack.Core.Application.Features.Orders.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace SpiceRack.WebApi.Controllers.v1;

[ApiVersion("1.0")]
[Authorize(Roles = "Waiters")]
[SwaggerTag("Order Management")]
public class OrderController : BaseApiController
{
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(
            Summary = "Create Order",
            Description = "Creates a new order associated with a table. Sets the state to 'InProgress' by default."
        )]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
                var command = new CreateOrderCommand(request);
                var createdOrder = await mediator.Send(command);

                return Ok();
        }

        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(
            Summary = "Update Order",
            Description = "Updates an existing order. (Allows modifying selected dishes)"
        )]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] UpdateOrderRequest request)
        {
                if(!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var command = new UpdateOrderCommand(request);
                return Ok(await mediator.Send(command));
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Get All Orders",
            Description = "Retrieves a list of all orders in the system."
        )]
        public async Task<IActionResult> GetOrders()
        {
            var query = new GetAllOrdersQuery();
            var result = await mediator.Send(query);
            if (result == null)
            {
                return NoContent();
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Get Order by ID",
            Description = "Retrieves a specific order by its ID, including its associated dishes."
        )]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var query = new GetOrderByIdQuery(id);
            var result = await mediator.Send(query);
            if (result == null)
            {
                return NoContent();
            }
            
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Delete Order",
            Description = "Deletes an order from the system, including its associated dishes."
        )]
        public async Task<IActionResult> Delete(int id)
        {
            await mediator.Send(new DeleteOrderCommand(id));
            return NoContent();            
        }

}
