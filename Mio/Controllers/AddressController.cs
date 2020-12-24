using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Mio.API.Repositories;
using Mio.Models.Users;

namespace Mio.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressRepository addressRepository;

        public AddressController(IAddressRepository addressRepository)
        {
            this.addressRepository = addressRepository;
        }


        [HttpGet("primary/{id}")]
        public async Task<ActionResult<Address>> GetAddress(string id)
        {
            try
            {
                var result = await addressRepository.GetDefaultAddress(id);
                if (result == null)
                {
                    return NotFound();
                }
                return result;
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                $"Error retrieving address of user with id = {id}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetAddresses(string id)
        {
            try
            {
                return Ok(await addressRepository.GetAddresses(id));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving address");
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddAddress(Address address)
        {
            try
            {
                if (address == null)
                {
                    return BadRequest();
                }

                await addressRepository.AddAddress(address);
                return Ok(1);
            }
            catch
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    "Address already exists");
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAddress(Address address)
        {
            try
            {
                if (address == null)
                {
                    return BadRequest();
                }

                await addressRepository.UpdateAddress(address);

                return Ok(1);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                 $"Error updating address with id = {address.ID}");
            }
        }


        [HttpDelete("{id:int}")]
        public async Task Delete(int id)
        {
            try
            {
                await addressRepository.DeleteAddress(id);
            }
            catch
            {
            }
        }
    }
}
