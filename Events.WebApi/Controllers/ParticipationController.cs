using AutoMapper;
using Events.Application.Events.Commands.CreateEvent;
using Events.Application.Events.Commands.DeleteEvent;
using Events.Application.Events.Commands.UpdateEvent;
using Events.Application.Events.Queries.GetEventList;
using Events.Application.Participations.Commands.CreateParticipation;
using Events.Application.Participations.Commands.DeleteParticipation;
using Events.Domain;
using Events.WebApi.Models.Participation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Events.WebApi.Controllers
{
    /// <summary>
    /// Controller for managing user participation in events.
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    public class ParticipationController : BaseController
    {
        private readonly IMapper _mapper;
        public ParticipationController(IMapper mapper) => _mapper = mapper;

        /// <summary>
        /// Creates a new participation record for a user in an event.
        /// </summary>
        /// <param name="createParticipationDto">The data transfer object containing details for the participation.</param>
        /// <returns>
        /// The unique identifier of the newly created participation record.
        /// </returns>
        /// <remarks>
        /// This method requires authentication and will automatically associate the participation with the authenticated user.
        /// </remarks>
        [HttpPost("Create")]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateParticipationDto createParticipationDto)
        {
            var command = _mapper.Map<CreateParticipationCommand>(createParticipationDto);
            command.UserId = UserId;
            var participationId = await Mediator.Send(command);
            return Ok(participationId);
        }

        /// <summary>
        /// Deletes a participation record.
        /// </summary>
        /// <returns>No content if the deletion is successful.</returns>
        /// <remarks>
        /// This method requires authentication and will ensure that the participation is associated with the authenticated user.
        /// </remarks>
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id, Guid eventId)
        {
            var command = new DeleteParticipationCommand
            {
                Id = id,
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }

}
