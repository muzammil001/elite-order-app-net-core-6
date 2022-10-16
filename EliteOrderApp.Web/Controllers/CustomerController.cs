using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using EliteOrderApp.Domain.Entities;
using EliteOrderApp.Service;
using EliteOrderApp.Web.Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EliteOrderApp.Web.Controllers
{
	public class CustomerController : Controller
	{
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        public IActionResult Index()
		{
			return View();
		}

        [HttpGet]
        public object GetCustomers(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_customerService.GetAll(), loadOptions);
        }

        [HttpPost]
        public IActionResult InsertCustomer(string values)
        {
            var customer = new Customer();
            JsonConvert.PopulateObject(values, customer);

            if (!TryValidateModel(customer))
                return BadRequest(ModelState.GetFullErrorMessage());
            _customerService.NewCustomer(customer);
            return Ok(customer);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCustomer(int key, string values)
        {
            var customer = await _customerService.GetCustomer(key);
            JsonConvert.PopulateObject(values, customer);

            if (!TryValidateModel(customer))
                return BadRequest(ModelState.GetFullErrorMessage());

            _customerService.UpdateCustomer();
            return Ok(customer);
        }

        [HttpDelete]
        public void DeleteCustomer(int key)
        {
            _customerService.DeleteCustomer(key);
        }
    }
}
