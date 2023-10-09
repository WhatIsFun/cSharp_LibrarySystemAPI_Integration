using cSharp_LibrarySystemAPI_Integration.Controllers;
//using cSharp_LibrarySystemAPI_Integration.Controllers.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace cSharp_LibrarySystemAPI_Integration.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class APIController : ControllerBase
    {
        HttpClient Client;
        public APIController()
        {
            Client = new HttpClient();
            Client.BaseAddress = new Uri("https://localhost:7062/");
        }






        [HttpGet]
        public async Task<ActionResult> GetBook()
        {
            HttpResponseMessage resp = Client.GetAsync("api/Book/getAllBooks").Result;

            if (resp.IsSuccessStatusCode)
            {
                string responseContent = await resp.Content.ReadAsStringAsync();

                List<Book> Book = JsonConvert.DeserializeObject<List<Book>>(responseContent);
                Console.WriteLine(Book);
                return Ok(Book);
            }
            else
            {

                return BadRequest();
            }

        }


        //public async Task<string> AuthenticateUser(string email, string password)
        //{

        //        var content = new FormUrlEncodedContent(new Dictionary<string, string>
        //{
        //    { "email", email },
        //    { "password", password }
        //});

        //        // Send a POST request to the login endpoint
        //        var response = await client.PostAsync("api/Login/EmployeeLogin", content);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            // Read the token from the response
        //            var token = await response.Content.ReadAsStringAsync();
        //            return token;
        //        }
        //        else
        //        {
        //            return null;
        //        }
            
        //}


        

    }
}
