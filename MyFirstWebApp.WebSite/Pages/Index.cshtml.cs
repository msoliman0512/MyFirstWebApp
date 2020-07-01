using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MyFirstWebApp.WebSite.Models;
using MyFirstWebApp.WebSite.Services;

namespace MyFirstWebApp.WebSite.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public JsonFileProductService ProductService;
        private Task<IEnumerable<Product>> aysyncProducts;
        public IEnumerable<Product> Products { get; private set; }

        public IndexModel(ILogger<IndexModel> logger,
            JsonFileProductService productService)
        {
            _logger = logger;
            ProductService = productService;
        }

        public async Task OnGetAsync()
        {
            await InitializeProducts();
        }
        private async Task InitializeProducts()
        {
            aysyncProducts = ProductService.GetProductsAsync();
            Products = await aysyncProducts;
            if (Utills.Utility.IsAnyProduct(Products))
            {

            }
            else
            {

            }
        }
    }
}
