using AutoMapper;
using EliteOrderApp.Domain.Entities;
using EliteOrderApp.Service;
using EliteOrderApp.Web.Dtos;
using EliteOrderApp.Web.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EliteOrderApp.Web.Controllers.api
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
        [Route("get-customers")]
        public async Task<IActionResult> GetCustomers()
        {
            var list = await _customerService.GetAll();
            return Ok(list);
        }

        [HttpGet]
        [Route("get-customer/{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var customer = await _customerService.GetCustomer(id);
            return Ok(customer);
        }

        [HttpPost]
        [Route("create-customer")]
        public async Task<IActionResult> CreateCustomer(CustomerDto customerDto)
        {

            if (!TryValidateModel(customerDto))
                return BadRequest(ModelState.GetFullErrorMessage());

            if (await _customerService.CheckCustomer(customerDto.Contact))
            {
                return BadRequest("Customer is already exists with same number.");
            }

            var customer = _mapper.Map<Customer>(customerDto);
           await _customerService.NewCustomer(customer);
            return Ok(customer);
        }

        [HttpPut]
        [Route("update-customer")]
        public async Task<IActionResult> UpdateCustomer(CustomerDto customerDto)
        {
            var customerInDb = await _customerService.GetCustomer(customerDto.Id);
            if (customerInDb == null)
            {
                return NotFound("Customer not found.");
            }
            var customer = _mapper.Map(customerDto, customerInDb);
            _customerService.UpdateCustomer(customer);
            return NoContent();
        }

        [HttpDelete]
        [Route("delete-customer/{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            await _customerService.DeleteCustomer(id);
            return NoContent();
        }
    }
}
