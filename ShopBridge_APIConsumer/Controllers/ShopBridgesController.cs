using ShopBridge_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;

namespace ShopBridge_APIConsumer.Controllers
{
    public class ShopBridgesController : Controller
    {
        // GET: ShopBridges
        // GET: ShopBridges
        
        public ActionResult Index(string option, string search)
        {

            //if a user choose the radio button option as Subject  
            if (option == "asc")
            {
                //Index action method will return a view with a student records based on what a user specify the value in textbox  
                var products = GetProductsFromAPI(option);
                return View(products);
            }
            else if (option == "desc")
            {
                var products = GetProductsFromAPI(option);
                return View(products);
            }
            else
            {
                if (String.IsNullOrEmpty(search))
                {
                    var products = GetAllProductsFromAPI();
                    return View(products);
                }
                else
                {
                    var products = GetSearchProductsFromAPI(search);
                    return View(products);
                }
            }
        }
        private object GetProductsFromAPI(string sort)
        {
            try
            {
                var resultList = new List<ShopBridge>();
                var client = new HttpClient();
                var getData = client.GetAsync("http://localhost:56733/api/ShopBridge/LoadSorted?sort="+sort).ContinueWith(response =>
                {
                    var result = response.Result;
                    if (result.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var readResult = result.Content.ReadAsAsync<List<ShopBridge>>();
                        readResult.Wait();
                        resultList = readResult.Result;
                    }
                });
                getData.Wait();
                return resultList;

            }
            catch (Exception e) { return e; }
        }

        private object GetSearchProductsFromAPI(string search)
        {
            try
            {
                var resultList = new List<ShopBridge>();
                var client = new HttpClient();
                var getData = client.GetAsync("http://localhost:56733/api/ShopBridges/Search?name=" + search).ContinueWith(response =>
                {
                    var result = response.Result;
                    if (result.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var readResult = result.Content.ReadAsAsync<List<ShopBridge>>();
                        readResult.Wait();
                        resultList = readResult.Result;
                    }
                });
                getData.Wait();
                return resultList;

            }
            catch (Exception e) { return e; }
        }
        private object GetAllProductsFromAPI()
        {
            try
            {
                var resultList = new List<ShopBridge>();
                var client = new HttpClient();
                var getData = client.GetAsync("http://localhost:56733/api/ShopBridges").ContinueWith(response =>
                {
                    var result = response.Result;
                    if (result.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var readResult = result.Content.ReadAsAsync<List<ShopBridge>>();
                        readResult.Wait();
                        resultList = readResult.Result;
                    }
                });
                getData.Wait();
                return resultList;

            }
            catch (Exception e) { return e; }
        }

        public async Task<JsonResult> GetProductsFromEdit(int id)
        {
            try
            {
                var resultList = new ShopBridge();
                var client = new HttpClient();
                string APIUri = "http://localhost:56733/api/";
                ShopBridge InventoryInfo = null;

                HttpResponseMessage Res = await client.GetAsync(APIUri + "ShopBridges/" + id.ToString());
                if (Res.IsSuccessStatusCode)
                {
                    var InventoryResponse = Res.Content.ReadAsStringAsync().Result;
                    resultList = JsonConvert.DeserializeObject<ShopBridge>(InventoryResponse);
                }
                var json = JsonConvert.SerializeObject(resultList);
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e) { throw e; }
        }
        
        [HttpPost]
        public async Task<ActionResult> AddEditInventory(ShopBridge inventory)
        {
            try
            {
                int? id = inventory.Id;
                var json = JsonConvert.SerializeObject(inventory);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var resultList = new ShopBridge();
                var client = new HttpClient();
                string APIUri = "http://localhost:56733/api/";
                ShopBridge InventoryInfo = null;
                HttpResponseMessage Res;



                if (id==0)
                {

                    Res = await client.PostAsync(APIUri + "ShopBridges",content );
                    if (Res.IsSuccessStatusCode)
                    {
                        var InventoryResponse = Res.Content.ReadAsStringAsync().Result;
                        List<ShopBridge> InventoryData = JsonConvert.DeserializeObject<List<ShopBridge>>(InventoryResponse);
                    }
                }
                else
                    Res = await client.PutAsync(APIUri + "ShopBridges/" + id.ToString(), content);
                
                return Json(0);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public async Task<ActionResult> DeleteInventory(int id)
        {
            try
            {
                var client = new HttpClient();
                string APIUri = "http://localhost:56733/api/";
                HttpResponseMessage Res = await client.DeleteAsync(APIUri+"ShopBridges/" + id.ToString());
               
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
               
                throw ex;
            }
        }
    }
}
