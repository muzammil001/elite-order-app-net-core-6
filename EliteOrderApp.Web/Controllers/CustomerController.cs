using AutoMapper;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
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

        public async Task<IActionResult> Index()
        {
            var customers = await _customerService.GetAll();
            var customersModel = _mapper.Map<List<CustomerDto>>(customers);
            return View(customersModel);
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
                        var customer = _customerService.GetCustomer(customerDto.Id);
                        if (customer == null)
                        {
                            return NotFound();
                        }

                        throw;
                    }
                }
                var customers = await _customerService.GetAll();
                var view = Helper.RenderRazorViewToString(this, "AddOrEdit", customers);
                return Json(new { isValid = true, html = view });
            }

            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", customerDto) });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _customerService.DeleteCustomer(id);
            var customers = await _customerService.GetAll();
            return Json(new
            {
                isValid = true,
                html = Helper.RenderRazorViewToString(this, "_ViewAll", customers)
            });

        }

    }
}
