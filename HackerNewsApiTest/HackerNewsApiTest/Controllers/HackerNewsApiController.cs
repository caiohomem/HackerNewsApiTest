using System.Linq;
using System.Threading.Tasks;
using HackerNewsApiTest.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace HackerNewsApiTest.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HackerNewsApiController : ControllerBase
    {

        private readonly HackerNewsApiService _service;

        public HackerNewsApiController(HackerNewsApiService service)
        {
            _service = service;
        }

        // GET api/values
        [HttpGet]
        [Route("beststories")]
        public async Task<ActionResult<JArray>> GetBestStories()
        {
            var jsonArray = await _service.GetIdsBestStories();
            var idFirst20Stories = jsonArray.Take(20);

            var stories = new JArray();
            foreach (var id in idFirst20Stories)
            {
                var jsonObject = await _service.GetStory(id.Value<string>());
                stories.Add(jsonObject);
            };

            return new JArray(stories.OrderByDescending(obj => (int)obj["score"]));
        }
    }
}
