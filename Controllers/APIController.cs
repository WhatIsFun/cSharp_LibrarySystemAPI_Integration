using cSharp_LibrarySystemAPI_Integration.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.OAuth;
using cSharp_LibrarySystemAPI_Integration.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using Serilog;

namespace cSharp_LibrarySystemAPI_Integration.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class APIController : ControllerBase
    {
        HttpClient Client;
        public static string Token;
        public APIController()
        {
            Client = new HttpClient();
            Client.BaseAddress = new Uri("https://localhost:7062/");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            Log.Information($"AuthToken value: {Token}");
            // Check if the token is available
            if (string.IsNullOrEmpty(Token))
            {
                return Unauthorized("Token is missing or invalid.");
            }

            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Token);

            try
            {
                HttpResponseMessage resp = await Client.GetAsync("api/BookOperation/ViewAllBooks");

                if (resp.IsSuccessStatusCode)
                {
                    string responseContent = await resp.Content.ReadAsStringAsync();
                    List<Book> books = JsonConvert.DeserializeObject<List<Book>>(responseContent);
                    return Ok(books);
                }
                else
                {
                    return BadRequest($"Failed to retrieve books. Status code: {resp.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            var loginRequest = new { Email = email, Password = password };

            try
            {
                // Log received credentials
                Log.Information($"Received login request - Email: {email}, Password: {password}");

                HttpResponseMessage response = await Client.PostAsJsonAsync("api/UserLogin/user-login", loginRequest);

                if (response.IsSuccessStatusCode)
                {
                    var tokenResponse = await response.Content.ReadAsStringAsync();
                    Token = tokenResponse; // Store the token as JSON string

                    return Ok(new { Token = tokenResponse }); // Return JSON response
                }

                else
                {
                    Log.Warning($"Login failed - Status code: {response.StatusCode}");
                    return Unauthorized("Invalid credentials");
                }
            }
            catch (Exception ex)
            {
                Log.Error($"An error occurred during login: {ex.Message}");
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

    }
}
