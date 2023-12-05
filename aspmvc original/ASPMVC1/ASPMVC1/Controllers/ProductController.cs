using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ASPMVC1.Models;
using Newtonsoft.Json;

namespace ASPMVC1.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult ProductList()
        {
            var products = GetProducts();
            return View(products);
        }

        private List<ProductModel> GetProducts()
        {
            string productsDataPath = @"C:\Users\elont\OneDrive\Рабочий стол\LB-main\aspmvc original\ASPMVC1\ASPMVC1\bin\ProductList\products.json";

            if (System.IO.File.Exists(productsDataPath))
            {
                string json = System.IO.File.ReadAllText(productsDataPath);

                List<ProductModel> products = JsonConvert.DeserializeObject<List<ProductModel>>(json);

                return products;
            }

            return new List<ProductModel>();
        }
        
        public ActionResult DeleteProduct(int id)
        {
            var products = GetProducts();

            var productToDelete = products.FirstOrDefault(p => p.Id == id);

            if (productToDelete != null)
            {
                products.Remove(productToDelete);

                SaveProducts(products);

                return RedirectToAction("ProductList");
            }
            else
            {
                return HttpNotFound();
            }
        }
        private void SaveProducts(List<ProductModel> products)
        {
            string productsDataPath = @"C:\Users\elont\OneDrive\Рабочий стол\LB-main\aspmvc original\ASPMVC1\ASPMVC1\bin\ProductList\products.json";
            
            string json = JsonConvert.SerializeObject(products, Formatting.Indented);
            System.IO.File.WriteAllText(productsDataPath, json);
        }
    }
}