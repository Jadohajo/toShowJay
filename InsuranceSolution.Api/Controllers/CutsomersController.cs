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
    public class CutsomersController : ControllerBase
    {
        //data access layer/point
        private readonly ApplicationDbContext _db;

        public CutsomersController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Customer model)
        {
            _db.Customers.Add(model);

            _db.SaveChanges();

            return Ok(model.Id);
        }

    }
}
