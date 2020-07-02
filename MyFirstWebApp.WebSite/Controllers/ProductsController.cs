 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFirstWebApp.WebSite.Models;
using MyFirstWebApp.WebSite.Services;

namespace MyFirstWebApp.WebSite.Controllers
{
    [Route("[controller]")] // it would be /Products cause we routed to the nme of our controller and btw we cn change that any time
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public ProductsController(JsonFileProductService productService)
        {
            this.ProductService = productService;
        }

        public JsonFileProductService ProductService { get; }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return ProductService.GetProducts();
        }

        //[HttpPatch]  "[FromBody]"
        [Route("Rate")]
        [HttpGet]  // I'm only just expermenting editing my database through my browser adrress as if I was getting them 
        public ActionResult Get([FromQuery] string ProductId, [FromQuery] int Rating)
        {
            ProductService.AddRating(ProductId, Rating);
            return Ok();
        }
    }
}
