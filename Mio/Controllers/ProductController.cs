using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mio.API.Repositories;
using Mio.Models.Packets;
using Mio.Models.Relations;
using Mio.Models.Sale;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mio.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;
        private readonly ITextContentRepository textContentRepository;
        public ProductController(IProductRepository productRepository, ITextContentRepository textContentRepository) 
        {
            this.productRepository = productRepository;
            this.textContentRepository = textContentRepository;
        }


        // GET: api/product/commodity
        [HttpGet("commodity")]
        public async Task<ActionResult<IEnumerable<Commodity>>> GetCommodities()
        {
            try
            {
                return Ok(await productRepository.GetCommodities());
    }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving commodities");
}
        }

        // GET: api/product/commodity/search/search_string
        [HttpGet("commodity/search/{search_string}")]
        public async Task<ActionResult<IEnumerable<Commodity>>> GetCommodities(string search_string)
        {
            try
            {
                return Ok(await productRepository.GetCommodities(search_string));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error retrieving commodities for {search_string}");
            }
        }

        
        // GET: api/product/service/
        [HttpGet("service")]
        public async Task<ActionResult<IEnumerable<Commodity>>> GetServices()
        {
            try
            {
                return Ok(await productRepository.GetServices());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error retrieving services");
            }
        }

        
        // GET: api/product/service/search/search_string
        [HttpGet("service/search/{search_string}")]
        public async Task<ActionResult<IEnumerable<Commodity>>> GetServices(string search_string)
        {
            try
            {
                return Ok(await productRepository.GetServices(search_string));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error retrieving services for {search_string}");
            }
        }

        
        // GET: api/product/service/timeslots/
        [HttpGet("service/timeslots")]
        public async Task<ActionResult<IEnumerable<Service>>> GetServices([FromQuery] int[] ids)
        {
            try
            {
                return Ok(await productRepository.GetFreeServices(ids));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error retrieving services for timeslots");
            }
        }

        // GET: api/product/userid
        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(string userId)
        {
            try
            {
                return Ok(await productRepository.GetProducts(userId));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error retrieving products for user {userId}");
            }
        }

        // GET: api/product/rentals/search/search_string
        [HttpGet("rentals/search/{search_string}")]
        public async Task<ActionResult<IEnumerable<Commodity>>> GetRentals(string search_string)
        {
            try
            {
                return Ok(await productRepository.GetRentals(search_string));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error retrieving rentals for {search_string}");
            }
        }


        // GET: api/product/rentals/customer/{userId}
        [HttpGet("rentals/customer/{userId}")]
        public async Task<ActionResult<IEnumerable<ProductRentals>>> GetRentalsCustomer(string userId)
        {
            try
            {
                return Ok(await productRepository.GetRentalsForUser(userId));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error retrieving rentals for user {userId}");
            }
        }

        // GET: api/product/rentals/owner/{userId}
        [HttpGet("rentals/owner/{userId}")]
        public async Task<ActionResult<IEnumerable<ProductRentals>>> GetRentalsOwner(string userId)
        {
            try
            {
                return Ok(await productRepository.GetRentalsOwnedByUser(userId));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error retrieving rentals owned by {userId}");
            }
        }

        // GET: api/product/rentals/expiring/{userId}
        [HttpGet("rentals/expiring/{userId}")]
        public async Task<ActionResult<IEnumerable<ProductRentals>>> GetRentalsExpiringSoon(string userId)
        {
            try
            {
                return Ok(await productRepository.GetRentalsExpiringSoon(userId));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error retrieving rentals expiring soon for user {userId}");
            }
        }

        // GET: api/product/service/appointments/customer/{userId}
        [HttpGet("service/appointments/customer/{userId}")]
        public async Task<ActionResult<IEnumerable<ServiceTimeslot>>> GetAppointments(string userId)
        {
            try
            {
                return Ok(await productRepository.GetUpcomingAppointmentsUser(userId));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error retrieving appointments for user {userId}");
            }
        }

        // GET: api/product/service/appointments/server/{userId}
        [HttpGet("service/appointments/server/{userId}")]
        public async Task<ActionResult<IEnumerable<ServiceTimeslot>>> GetAppointmentsServer(string userId)
        {
            try
            {
                return Ok(await productRepository.GetUpcomingAppointmentsServer(userId));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error retrieving appointments for server {userId}");
            }
        }

        // GET: api/product/history/{userId}
        [HttpGet("history/{userId}")]
        public async Task<ActionResult<IEnumerable<ProductHistory>>> GetHistory(string userId)
        {
            try
            {
                return Ok(await productRepository.GetProductHistory(userId));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error retrieving appointments for server {userId}");
            }
        }


        // GET: api/product/commodity/id
        [HttpGet("commodity/{id:int}")]
        public async Task<ActionResult<Commodity>> GetCommodity(int id)
        {
            try
            {
                return Ok(await productRepository.GetCommodity(id));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error retrieving commodity {id}");
            }
        }

        //Task<Service> GetService(int id);
        // GET: api/product/service/id
        [HttpGet("service/{id:int}")]
        public async Task<ActionResult<Service>> GetService(int id)
        {
            try
            {
                return Ok(await productRepository.GetService(id));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error retrieving service {id}");
            }
        }

        // GET: api/product/service/appointmets/id
        [HttpGet("service/appointments/{id:int}")]
        public async Task<ActionResult<ServiceTimeslot>> GetAppointment(int id)
        {
            try
            {
                return Ok(await productRepository.GetAppointment(id));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error retrieving appointment {id}");
            }
        }

        // GET: api/product/rentals/id
        [HttpGet("rentals/{id:int}")]
        public async Task<ActionResult<ProductRentals>> GetRentals(int id)
        {
            try
            {
                return Ok(await productRepository.GetRental(id));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error retrieving rental {id}");
            }
        }


        // GET: api/product/service/appointments/timeslots
        [HttpGet("service/appointments/timeslots")]
        public async Task<ActionResult<Service>> GetAvailableServices(ServicePacket servicePacket)
        {
            try
            {
                return Ok(await productRepository.GetFreeServices(servicePacket));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error retrieving appointments for packet");
            }
        }

        // POST: api/product/commodity
        [HttpPost("commodity")]
        public async Task<ActionResult> AddCommodity(Commodity commodity)
        {
            try
            {
                if (commodity == null)
                {
                    return BadRequest();
                }

                return Ok(await productRepository.AddCommodity(commodity));
            }
            catch
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    "Commodity already exists");
            }
        }

        // POST: api/product/service
        [HttpPost("service")]
        public async Task<ActionResult> AddService(Service service)
        {
            try
            {
                if (service == null)
                {
                    return BadRequest();
                }

                return Ok(await productRepository.AddService(service));
            }
            catch
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    "Service already exists");
            }
        }

        // POST: api/product/history
        [HttpPost("history")]
        public async Task<ActionResult> AddHistory(ProductHistory productHistory)
        {
            try
            {
                if (productHistory == null)
                {
                    return BadRequest();
                }

                return Ok(await productRepository.AddProductHistory(productHistory));
            }
            catch
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    "Product History already exists");
            }
        }


        // POST: api/product/rentals
        [HttpPost("rentals")]
        public async Task<ActionResult> AddRental(ProductRentals productRentals)
        {
            try
            {
                if (productRentals == null)
                {
                    return BadRequest();
                }

                return Ok(await productRepository.AddProductRentals(productRentals));
            }
            catch
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    "Product Rental already exists");
            }
        }

        // POST: api/service/appointment
        [HttpPost("service/appointmet")]
        public async Task<ActionResult> AddAppointment(ServiceTimeslot serviceTimeslot)
        {
            try
            {
                if (serviceTimeslot == null)
                {
                    return BadRequest();
                }

                return Ok(await productRepository.AddServiceTimeslot(serviceTimeslot));
            }
            catch
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    "Appointment already exists");
            }
        }

        // DELETE: api/product/commodity/id
        [HttpDelete("commodity/{id:int}")]
        public async Task<ActionResult> DeleteCommodity(int id)
        {
            try
            {
                await textContentRepository.DeleteReviews(id);
                return Ok(await productRepository.DeleteCommodity(id));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error deleting commmodity with id {id}");
            }
        }

        // DELETE: api/product/service/id
        [HttpDelete("service/{id:int}")]
        public async Task<ActionResult> DeleteService(int id)
        {
            try
            {
                await textContentRepository.DeleteReviews(id);
                return Ok(await productRepository.DeleteService(id));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error deleting service with id {id}");
            }
        }


        // DELETE: api/product/history/id
        [HttpDelete("history/{id:int}")]
        public async Task<ActionResult> DeleteProductHistory(int id)
        {
            try
            {
                return Ok(await productRepository.DeleteProductHistory(id));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error deleting product history with id {id}");
            }
        }

        
        // DELETE: api/product/rentals/id
        [HttpDelete("rentals/{id:int}")]
        public async Task<ActionResult> DeleteRental(int id)
        {
            try
            {
                return Ok(await productRepository.DeleteProductRentals(id));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error deleting rental with id {id}");
            }
        }

        // DELETE: api/product/service/appointments/id
        [HttpDelete("service/appointments/{id:int}")]
        public async Task<ActionResult> DeleteAppointment(int id)
        {
            try
            {
                return Ok(await productRepository.DeleteServiceTimeslot(id));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error deleting appontment with id {id}");
            }
        }

        // PUT: api/product/commodity/
        [HttpPut("commodity")]
        public async Task<ActionResult<Commodity>> UpdateCommodity(Commodity commodity)
        {
            try
            {
                if (commodity == null) return BadRequest();
                return Ok(await productRepository.UpdateCommodity(commodity));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error updating commodity with id {commodity.ID}");
            }
        }

        // PUT: api/product/service/
        [HttpPut("service")]
        public async Task<ActionResult<Service>> UpdateService(Service service)
        {
            try
            {
                if (service == null) return BadRequest();
                return Ok(await productRepository.UpdateService(service));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error updating service with id {service.ID}");
            }
        }

        // PUT: api/product/rental/
        [HttpPut("rental")]
        public async Task<ActionResult<ProductRentals>> UpdateRental(ProductRentals productRental)
        {
            try
            {
                if (productRental == null) return BadRequest();
                return Ok(await productRepository.UpdateProductRentals(productRental));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error updating rental with id {productRental.ID}");
            }
        }

        //Task<ServiceTimeslot> UpdateServiceTimeslot(ServiceTimeslot serviceTimeslot);

        // PUT: api/product/service/appointments
        [HttpPut("service/appointments")]
        public async Task<ActionResult<ServiceTimeslot>> UpdateAppointment(ServiceTimeslot serviceTimeslot)
        {
            try
            {
                if (serviceTimeslot == null) return BadRequest();
                return Ok(await productRepository.UpdateServiceTimeslot(serviceTimeslot));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error updating appointment with id {serviceTimeslot.ID}");
            }
        }




    }
}
