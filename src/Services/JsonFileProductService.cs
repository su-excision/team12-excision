using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

using ContosoCrafts.WebSite.Models;

using Microsoft.AspNetCore.Hosting;

namespace ContosoCrafts.WebSite.Services
{
    /// <summary>
    /// JsonFileProductService provides access to a product database stored
    /// in a JSON file stored in the Azure site with the name <c>products.json</c>.
    /// </summary>
    public class JsonFileProductService
    {
        /// <summary>
        /// Constructor for the JsonFileProductService.
        /// </summary>
        /// <param name="webHostEnvironment">the web host environment that the application is running in</param>
        public JsonFileProductService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        /// The Web Host Environment in which the database is stored.
        /// </summary>
        public IWebHostEnvironment WebHostEnvironment { get; }

        /// <summary>
        /// Filename and path for the JSON file containing the database data.
        /// </summary>
        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "products.json"); }
        }

        /// <summary>
        /// GetProducts retrieves a collection of Products as retrieved from the JSON
        /// database.
        /// </summary>
        /// <returns>IEnumerable of products read from the database.</returns>
        public IEnumerable<ProductModel> GetProducts()
        {
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                return JsonSerializer.Deserialize<ProductModel[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
        }

        /// <summary>
        /// GetProduct returns a single product, reference by Product ID. If
        /// the Product ID does not exist, it returns null.
        /// </summary>
        /// <param name="productId">the Product ID to search for</param>
        /// <returns>the Product matching the Product ID or null </returns>
        public ProductModel GetProduct(string productId)
        {
            // get products
            var products = GetProducts();

            // return the desired product or sadness
            return products.FirstOrDefault(x => x.Id.Equals(productId));
        }

        public ProductModel GetSearchedProduct(string productName)
        {
            // get products
            var products = GetProducts();

            // return the desired product or sadness
            return products.FirstOrDefault(x => x.Name.Contains(productName));
        }



        /// <summary>
        /// GetFirstProduct returns the first product in the datastore.
        /// </summary>
        /// <returns>the first Product in the datastore</returns>
        public ProductModel GetFirstProduct()
        {
            // get products
            var products = GetProducts();

            // if empty list
            if (products.Any() == false)
            {
                return null;
            }

            // return the first product
            return products.First<ProductModel>();
        }

        /// <summary>
        /// GetLastProduct returns the last product in the datastore.
        /// </summary>
        /// <returns>the last Product in the datastore</returns>
        public ProductModel GetLastProduct()
        {
            // get products
            var products = GetProducts();

            // if empty list
            if (products.Any() == false)
            {
                return null;
            }

            // return the first product
            return products.Last<ProductModel>();
        }

        /// <summary>
        /// AddRating adds a single rating (numbered 1 to 5) to an entry in the database.
        /// </summary>
        /// <param name="productId">Card ID for the Pokémon card in the database</param>
        /// <param name="rating">the rating (1 to 5) to add to the specific card entry</param>
        /// <returns>true if the rating was successfully added to the Product, false otherwise</returns>
        public bool AddRating(string productId, int rating)
        {

            // if invalid Product
            if (string.IsNullOrEmpty(productId))
            {
                return false;
            }

            // rating out of range low
            if (rating < 1)
            {
                return false;
            }

            // rating out of range high
            if (rating > 5)
            {
                return false;
            }

            // list of products from the JSON database
            var products = GetProducts();
            // get specific product
            var product = products.FirstOrDefault(x => x.Id.Equals(productId));

            // if product does not exist
            if (product == null)
            {
                return false;
            }



            // if the product has no ratings, create the new array
            if (product.Ratings == null)
            {
                products.First(x => x.Id == productId).Ratings = Array.Empty<int>();
            }


            // list of ratings for the product
            var ratings = product.Ratings.ToList();
            ratings.Add(rating);
            product.Ratings = ratings.ToArray();

            // write the JSON file
            WriteJsonFile(products);

            return true;
        }


        /// <summary>
        /// WriteJsonFile writes the contents of list of products to the JSON
        /// file for storage.
        /// This procedure overwrites any existing file.
        /// </summary>
        /// <param name="products">the list of products to be written</param>
        public void WriteJsonFile(IEnumerable<ProductModel> products)
        {
            using (var outputStream = File.Create(JsonFileName))
            {
                JsonSerializer.Serialize<IEnumerable<ProductModel>>(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions
                    {
                        SkipValidation = true,
                        Indented = true
                    }),
                    products
                );
            }
        }

        /// <summary>
        /// UpdateData takes updated prodcut data and updates the list of products
        /// in the JSON data file wiht the new data.
        /// </summary>
        /// <param name="updatedProduct">new product data to update</param>
        /// <returns>true if the update was successful, false otherwise</returns>
        public bool UpdateData(ProductModel updatedProduct)
        {

            // product list
            var products = GetProducts();
            var existingProduct = products.FirstOrDefault(p => p.Id == updatedProduct.Id);

            // if product not in list
            if (existingProduct == null)
            {
                return false;
            }

            // update the properties affected by the Update page
            // existingProduct.Name = updatedProduct.Name;
            // existingProduct.Description = updatedProduct.Description;
            // existingProduct.Rarity = updatedProduct.Rarity;
            // existingProduct.Availability = updatedProduct.Availability;
            // existingProduct.Type = updatedProduct.Type;
            // existingProduct.Value = updatedProduct.Value;

            updatedProduct.CopyTo(existingProduct);

            // write the product list to the file
            WriteJsonFile(products);
            return true;

        }

        /// <summary>
        /// AddProduct adds a new product to the product database. If the product
        /// database already contains a prdocut with the same unique ID, the
        /// opreation does not update the database.
        /// </summary>
        /// <param name="newProduct">new product data to be inserted</param>
        /// <returns>true if the add was successful, false otherwise</returns>
        public bool AddProduct(ProductModel newProduct)
        {

            // load existing product list
            var products = GetProducts();

            // if product ID already exists
            if (products.FirstOrDefault(p => p.Id == newProduct.Id) != null)
            {
                // operation fails
                return false;
            }

            // append the new product to the end of the product list
            products = products.Append(newProduct);


            // write the product list to the file
            WriteJsonFile(products);

            // operation was successful
            return true;

        }

        /// <summary>
        /// DeleteProduct deletes a product with the specified product ID. If the
        /// ID does not exist in the database, it does not delete anything.
        /// </summary>
        /// <param name="productId">the ID corresponding to the product to delete.</param>
        /// <returns>true if the product was deleted, false otherwise</returns>
        public bool DeleteProduct(string productId)
        {
            // product list
            var products = GetProducts();

            // if product ID does not exist in the list
            if (products.FirstOrDefault(p => p.Id == productId) == null)
            {
                // operation fails
                return false;
            }

            // make a new product list without the deleted product
            var newProducts = products.Where(x => x.Id != productId);

            // write the new product list
            WriteJsonFile(newProducts);

            // let it know it worked
            return true;

        }

        /// <summary>
        /// Find the data record
        /// Update the fields
        /// Save to the data store
        /// </summary>
        /// <param name="data"></param>
        public bool SaveComment(ProductModel data)
        {
            // product list
            var products = GetProducts();
            var existingProduct = products.FirstOrDefault(p => p.Id == data.Id);

            // if product not in list
            if (existingProduct == null)
            {
                return false;
            }

            existingProduct.CommentList = data.CommentList;

            // write the product list to the file
            WriteJsonFile(products);
            return true;
        }
    }
}