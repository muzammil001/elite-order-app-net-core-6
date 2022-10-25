using AutoMapper;
using EliteOrderApp.Domain.Entities;
using EliteOrderApp.Service;
using EliteOrderApp.Web.Dtos;
using EliteOrderApp.Web.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using static EliteOrderApp.Web.Extensions.Helper;

namespace EliteOrderApp.Web.Controllers
{
    public class CustomerController : Controller
    {
        private readonly CustomerService _customerService;
        private readonly IMapper _mapper;

        public CustomerController(CustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [NoDirectAccess]
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            //Create form 
            if (id == 0)
            {
                return View(new CustomerDto());

            }
            //edit form
            var customer = await _customerService.GetCustomer(id);
            if (customer == null)
            {
                return NotFound();
            }

            var customerDto = _mapper.Map<CustomerDto>(customer);

            return View(customerDto);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, [Bind("Id,Name,Contact")] CustomerDto customerDto)
        {
            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    if (await _customerService.CheckCustomer(customerDto.Contact))
                    {
                        return BadRequest("customer is already exists with same mobile number");
                    }
                    var customer = _mapper.Map<Customer>(customerDto);
                    await _customerService.NewCustomer(customer);
                }
                else
                {
                    try
                    {
                        var customer = _mapper.Map<Customer>(customerDto);
                        _customerService.UpdateCustomer(customer);
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        return BadRequest();
                    }
                }
                return Json(new { isValid = true, html = "" });
            }

            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", customerDto) });
        }

    }
}
