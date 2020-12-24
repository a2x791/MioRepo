using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mio.API.Repositories;
using Mio.Models.Packets;
using Mio.Models.Time;
using Mio.Models.Users;

namespace Mio.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }


        // GET: api/User
        [HttpGet]
        public async Task<ActionResult> GetUsers()
        {
            try
            {
                return Ok(await userRepository.GetUsers());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving users");
            }
        }

        // GET: api/User/name/{name}
        [HttpGet("name/{name}")]
        public async Task<ActionResult> GetUsersByName(string name)
        {
            try
            {
                return Ok(await userRepository.SearchByName(name));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving users");
            }
        }


        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(string id)
        {
            try
            {
                return Ok(await userRepository.GetUser(id));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error retrieving user with id {id}");
            }
        }

        // GET: api/User/username/{username}
        [HttpGet("username/{username}")]
        public async Task<ActionResult<bool>> GetIfUsernameExists(string username)
        {
            try
            {
                return Ok(await userRepository.UsernameExists(username));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error checking if username {username} exists");
            }
        }


        // GET: api/User/timeslot/{id}
        [HttpGet("timeslot/{id}")]
        public async Task<ActionResult<IEnumerable<Timeslot>>> GetTimeslots(string id)
        {
            try
            {
                return Ok(await userRepository.GetTimeslots(id));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error retrieving timeslots for user with id {id}");
            }
        }

        // GET: api/User/timeslot/{userpacket}
        [HttpGet("timeslot/week/{userpacker}")]
        public async Task<ActionResult<IEnumerable<Timeslot>>> GetTimeslotsForWeek(UserPacket userPacket)
        {
            try
            {
                return Ok(await userRepository.GetTimeslotsForWeek(userPacket));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error retrieving timeslots for week {userPacket.DateTime} for user with id {userPacket.ID}");
            }
        }

        // POST: api/User/{User}
        [HttpPost]
        public async Task<ActionResult<User>> AddUser(User user) 
        {
            try
            {
                if (user == null)
                {
                    return BadRequest();
                }
                User createdUser = await userRepository.AddUser(user);
                if (createdUser == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest,
                 "User already created");
                }

                return createdUser;
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                 "Error adding user");
            }
        }

        // POST: api/User/{userPacket}
        [HttpPost]
        public async Task<ActionResult<Timeslot>> AddUserTimeslot(UserPacket userPacket)
        {
            try
            {
                if (userPacket == null || userPacket.Timeslot == null)
                {
                    return BadRequest();
                }
                Timeslot createdTimeslot = await userRepository.AddTimeslot(userPacket);
                if (createdTimeslot == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest,
                 "User Timeslot already created");
                }

                return createdTimeslot;
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                 $"Error adding timeslot for user with id {userPacket.ID}");
            }
        }


        // PUT: api/User
        [HttpPut]
        public async Task<ActionResult<User>> UpdateUser(User user)
        {
            try
            {
                if(user == null) return BadRequest();
                return Ok(await userRepository.UpdateUser(user));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error updating user with id {user.ID}");
            }
        }

        // PUT: api/User/timeslot
        [HttpPut("timeslot")]
        public async Task<ActionResult<Timeslot>> UpdateTimeslot(UserPacket userPacket)
        {
            try
            {
                if (userPacket == null) return BadRequest();
                return Ok(await userRepository.UpdateTimeslot(userPacket));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error updating timeslot {userPacket.Timeslot.ID} of user with id {userPacket.ID}");
            }
        }

        // PUT: api/User/username
        [HttpPut("username")]
        public async Task<ActionResult<User>> UpdateUser(UserPacket userPacket)
        {
            try
            {
                if (userPacket == null) return BadRequest();
                return Ok(await userRepository.SetUsername(userPacket));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error updating username of user with id {userPacket.ID}");
            }
        }


        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>>  Delete(string id)
        {
            try
            {
                return Ok(await userRepository.DeleteUser(id));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error deleting user with id {id}");
            }
        }

        // DELETE: api/User/timeslot/5
        [HttpDelete("timeslot")]
        public async Task<ActionResult<User>> DeleteTimeslot(UserPacket userPacket)
        {
            try
            {
                if(userPacket == null || userPacket.Timeslot == null) { return BadRequest(); }
                return Ok(await userRepository.DeleteTimeslot(userPacket));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error deleting timeslot {userPacket.Timeslot.ID} of user with id {userPacket.ID}");
            }
        }
    }
}
