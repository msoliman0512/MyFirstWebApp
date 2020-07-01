﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using MyFirstWebApp.WebSite.Models;

namespace MyFirstWebApp.WebSite.Services
{
    public class JsonFileProductService
    {
        public JsonFileProductService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "products.json"); }
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                IEnumerable<Product> products = null;
                await Task.Run(() =>
                    {
                         products = JsonSerializer.Deserialize<Product[]>(jsonFileReader.ReadToEnd(),
                            new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true
                            });
                    });
                return products;
            }

        }

        public async void AddRating(string productId, int rating)
        {
            Task<IEnumerable<Product>> aysyncProducts = GetProductsAsync();
            var products = await aysyncProducts;
            if (Utills.Utility.IsAnyProduct(products))
            {



                if (products.First(x => x.Id == productId).Ratings == null)
                {
                    products.First(x => x.Id == productId).Ratings = new int[] { rating };
                }
                else
                {
                    var ratings = products.First(x => x.Id == productId).Ratings.ToList();
                    ratings.Add(rating);
                    products.First(x => x.Id == productId).Ratings = ratings.ToArray();
                }

                using (var outputStream = File.OpenWrite(JsonFileName))
                {
                    JsonSerializer.Serialize<IEnumerable<Product>>(
                        new Utf8JsonWriter(outputStream, new JsonWriterOptions
                        {
                            SkipValidation = true,
                            Indented = true
                        }),
                        products
                    );
                }
            }

        }
    }

}
