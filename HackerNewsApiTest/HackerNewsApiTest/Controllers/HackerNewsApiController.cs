using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HackerNewsApiTest.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HackerNewsApiController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        [Route("beststories")]
        public ActionResult<IEnumerable<string>> GetBestStories()
        {
            return new string[] { "value1", "value2" };
        }
    }
}
