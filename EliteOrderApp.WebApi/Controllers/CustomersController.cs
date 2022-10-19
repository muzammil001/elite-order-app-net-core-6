using AutoMapper;
using EliteOrderApp.Domain.Entities;
using EliteOrderApp.Service;
using EliteOrderApp.WebApi.Dtos;
using EliteOrderApp.WebApi.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace EliteOrderApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerService _customerService;
        private readonly IMapper _mapper;

        public CustomersController(CustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get All Customers")]
        [Route("GetCusotmers")]
        public async Task<IActionResult> GetCustomers()
        {
            var list = await _customerService.GetAll();
            return Ok(list);
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get Customer")]
        [Route("GetCusotmer/{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var customer = await _customerService.GetCustomer(id);
            return Ok(customer);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create Customer")]
        [Route("CreateCusotmer")]
        public async Task<IActionResult> CreateCustomer(CustomerDto customerDto)
        {

            if (!TryValidateModel(customerDto))
                return BadRequest(ModelState.GetFullErrorMessage());

            if (await _customerService.CheckCustomer(customerDto.Contact))
            {
                return BadRequest("Customer is already exists with same number.");
            }

            var customer = _mapper.Map<Customer>(customerDto);
            _customerService.NewCustomer(customer);
            return Ok(customer);
        }

        [HttpPut]
        [SwaggerOperation(Summary = "Update Customer")]
        [Route("UpdateCusotmer")]
        public async Task<IActionResult> UpdateCustomer(CustomerDto customerDto)
        {
            var customerInDb = await _customerService.GetCustomer(customerDto.Id);
            if (customerInDb == null)
            {
                return NotFound("Customer not found.");
            }
            _mapper.Map(customerDto, customerInDb);
            _customerService.UpdateCustomer();
            return NoContent();
        }

        [HttpDelete]
        [SwaggerOperation(Summary = "Delete Customer")]
        [Route("DeleteCusotmer/{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            await _customerService.DeleteCustomer(id);
            return NoContent();
        }
    }
}
