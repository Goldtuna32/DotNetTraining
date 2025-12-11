using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Refit;
using RestSharp;

namespace DotNetTraining.RestApi2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BirdsController : ControllerBase
    {
        //[HttpGet("/birds")]
        //public async Task<IActionResult> GetBirdsAsync([FromServices] HttpClient httpClient)
        //{
        //    var birds = await httpClient.GetAsync("birds");
        //    return birds.IsSuccessStatusCode
        //        ? Ok(await birds.Content.ReadAsStringAsync())
        //        : StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving birds");
        //}

        [HttpGet("/pick")]
        public async Task<IActionResult> GetPickAsync([FromServices] RestClient restClient)
        {
            RestRequest request = new RestRequest("pick-a-pile", Method.GET);
            var birds = await restClient.ExecuteGetAsync(request);
            return birds.IsSuccessful
                ? Ok(birds.Content)
                : StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving birds");
        }

        [HttpGet("/snakes")]
        public async Task<IActionResult> GetSnakeAsync([FromServices] ISnakeApi snakeApi)
        {
            var birds = await snakeApi.GetSnakesAsync();
            return Ok(birds);

        }

        public interface ISnakeApi
        {
            [Get("/snakes")]
            Task<List<SnakeModel>> GetSnakesAsync();
        }


        public class SnakeModel
        {
            public int Id { get; set; }
            public string ImageUrl { get; set; }
            public string MMName { get; set; }
            public string EngName { get; set; }
            public string Detail { get; set; }
            public string IsPoison { get; set; }
            public string IsDanger { get; set; }
        }

    }
}
