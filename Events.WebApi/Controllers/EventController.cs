using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Events.Application.Events.Queries.GetEventList;
using Events.Application.Events.Queries.GetEventDetails;
using Events.Application.Events.Commands.CreateEvent;
using Events.Application.Events.Commands.UpdateEvent;
using Events.Application.Events.Commands.DeleteEvent;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Hosting;
using Events.WebApi.Models.Event;

namespace Events.WebApi.Controllers
{
    /// <summary>
    /// Manages event-related operations such as registration, retrieval, update, and deletion.
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    public class EventController : BaseController
    {

        private readonly IMapper _mapper;
        public EventController(IMapper mapper) => _mapper = mapper;

        /// <summary>
        /// Retrieves a list of events based on search criteria.
        /// </summary>
        /// <param name="searchTerm">Optional search term to filter events by name.</param>
        /// <param name="category">Optional category to filter events by category.</param>
        /// <param name="location">Optional location to filter events by location.</param>
        /// <param name="eventDateTime">Optional date and time to filter events by date.</param>
        /// <returns>A list of events that match the specified criteria.</returns>
        [HttpGet("GetAll")]
        public async Task<ActionResult<EventListVm>> GetAll(
            [FromQuery] string? searchTerm,
            [FromQuery] string? category,
            [FromQuery] string? location,
            [FromQuery] DateTime? eventDateTime)
        {
            var query = new GetEventListQuery
            {
                SearchTerm = searchTerm,
                Category = category,
                Location = location,
                EventDateTime = eventDateTime,
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Retrieves details of a specific event by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the event.</param>
        /// <returns>The details of the event with the specified ID.</returns>
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<EventDetailsVm>> Get(Guid id)
        {
            var query = new GetEventDetailsQuery
            {
                Id = id
            };

            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Creates a new event.
        /// </summary>
        /// <param name="createEventDto">The data transfer object containing event details.</param>
        /// <returns>The unique identifier of the newly created event.</returns>
        /// <remarks>
        /// This action requires the user to have the "AdminPolicy" authorization.
        /// </remarks>
        [Authorize("AdminPolicy")]
        [HttpPost("Create")]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateEventDto createEventDto)
        {
            var command = _mapper.Map<CreateEventCommand>(createEventDto);
            var eventId = await Mediator.Send(command);
            return Ok(eventId);
        }

        /// <summary>
        /// Updates an existing event.
        /// </summary>
        /// <param name="updateEventDto">The data transfer object containing updated event details.</param>
        /// <returns>No content.</returns>
        /// <remarks>
        /// This action requires the user to have the "AdminPolicy" authorization.
        /// </remarks>
        [Authorize("AdminPolicy")]
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateEventDto updateEventDto)
        {
            var command = _mapper.Map<UpdateEventCommand>(updateEventDto);
            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Deletes an event by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the event to be deleted.</param>
        /// <returns>No content.</returns>
        /// <remarks>
        /// This action requires the user to have the "AdminPolicy" authorization.
        /// </remarks>
        [Authorize("AdminPolicy")]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteEventCommand
            {
                Id = id
            };

            await Mediator.Send(command);
            return NoContent();
        }


    }
}
