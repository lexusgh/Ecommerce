using System.Linq;
using Infrastructure.Data;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly StoreContext _context;
        public ProductsController(StoreContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<Product> GetProducts()
        {
            var prod = _context.Products.ToList();
            return Ok(prod);
        }

        [HttpGet("{{id}}")]
        public ActionResult GetProduct(int id)
        {
            var prod = _context.Products.FirstOrDefault(x=>x.Id == id);
            return Ok(prod);
        }
    }
}