using InsuranceSolution.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceSolution.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        //data access layer/point
        private readonly ApplicationDbContext _db;

        public CustomersController(ApplicationDbContext db)
        {
            _db = db;
        }

        // Retrieve all 
        [HttpGet]
        public IActionResult Get()
        {
            var customers = _db.Customers.ToArray();

            return Ok(customers);
        }

        // Retrieve one with all the details 
        // GET: /api/customers/345435
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var customer = _db.Customers.Find(id);

            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        // Retrieve customers by country // seach criteria 
        // GET: /api/customers/country?code=UK
        // GET: /api/customers/country/UK
        [HttpGet("country/{country}")]
        public IActionResult Get(string country)
        {
            // Find retrieve one customer by it's primary 
            var customers = _db.Customers.Where(c => c.Country == country).ToArray();

            return Ok(customers);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Customer model)
        {
            _db.Customers.Add(model);

            _db.SaveChanges();

            return Ok(model.Id);
        }

        [HttpPut]
        public IActionResult Put([FromBody] Customer model)
        {
            // Fetch teh customer first 
            var customer = _db.Customers.Find(model.Id);
            if (customer == null)
                return NotFound();

            customer.FirstName = model.FirstName;
            customer.LastName = model.LastName; 
            customer.Email = model.Email;   
            customer.Phone = model.Phone;   
            customer.Country = model.Country;
            customer.Birthday = model.Birthday;

            _db.SaveChanges();

            return Ok(); 
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            // Find compares an object with primary value of the object 
            var customer = _db.Customers.Find(id);
            if (customer == null)
                return NotFound(); 

            _db.Customers.Remove(customer);
            _db.SaveChanges(); 
            return Ok();
        }

    }
}
