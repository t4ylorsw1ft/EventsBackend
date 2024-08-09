using AutoMapper;
using Events.Application.Users.Commands.DeleteUser;
using Events.Application.Users.Commands.Register;
using Events.Application.Users.Commands.UpdateUser;
using Events.Application.Users.Queries.GetUserDetails;
using Events.Application.Users.Queries.GetUserList;
using Events.WebApi.Models.Auth;
using Events.WebApi.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Events.WebApi.Controllers
{
    /// <summary>
    /// Manages user-related operations such as registration, retrieval, update, and deletion.
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        private readonly IMapper _mapper;
        public UserController(IMapper mapper) => _mapper = mapper;

        /// <summary>
        /// Retrieves a list of all users.
        /// </summary>
        /// <returns>A list of users.</returns>
        [HttpGet("GetAll")]
        public async Task<ActionResult<UserListVm>> GetAll()
        {
            var query = new GetUserListQuery();
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Retrieves user details by user ID.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <returns>The details of the user with the specified ID.</returns>
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<UserDetailsVm>> Get(Guid id)
        {
            var query = new GetUserDetailsQuery
            {
                Id = id
            };

            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="registerDto">The data transfer object containing user registration details.</param>
        /// <returns>The unique identifier of the newly registered user.</returns>
        [HttpPost("Register")]
        public async Task<ActionResult<Guid>> Register([FromBody] RegisterDto registerDto)
        {
            var command = _mapper.Map<RegisterCommand>(registerDto);
            var UserId = await Mediator.Send(command);
            return Ok(UserId);
        }

        /// <summary>
        /// Updates an existing user's details.
        /// </summary>
        /// <param name="updateUserDto">The data transfer object containing updated user details.</param>
        /// <returns>No content.</returns>
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateUserDto updateUserDto)
        {
            var command = _mapper.Map<UpdateUserCommand>(updateUserDto);
            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Deletes a user by their ID and verifies with a password.
        /// </summary>
        /// <param name="deleteUserDto">The data transfer object containing user ID and password for verification.</param>
        /// <returns>No content.</returns>
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteUserDto deleteUserDto)
        {
            var command = new DeleteUserCommand
            {
                Id = deleteUserDto.Id,
                Password = deleteUserDto.Password
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}