using HackerNewsModule;
using HackerNewsModule.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HackerNewsDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class HackerNews : ControllerBase
    {
        private readonly IHackerNewsService _service;
        public HackerNews(IHackerNewsService service)
        {
            _service = service;
        }
        // GET: api/<HackerNews>
        /// <summary>
        /// Get Hacker News Item by ID
        /// </summary>
        /// <param name="id">Item ID</param>
        /// <param name="_service">Hacker News Service</param>
        /// <returns>List of Hacker News Items</returns>
        [HttpGet("item/{id}")]
        public async Task<IActionResult> Item([FromRoute] string id)
        {
            var response = await _service.GetHackerNewsItemById(id);
            return Ok(response);
        }

        // GET: api/<HackerNews>
        /// <summary>
        /// Get Hacker News Max Item ID
        /// </summary>
        /// <param name="_service">Hacker News Service</param>
        /// <returns>Max Item ID</returns>
        [HttpGet("maxitem")]
        public async Task<IActionResult> MaxItem([FromQuery] bool includeDetails)
        {
            if (includeDetails)
            {
                int maxItem = await _service.GetHackerNewsMaxItem();
                var item = await _service.GetHackerNewsItemById(maxItem.ToString());
                return Ok(item);
            }
            else
            {
                int maxItem = await _service.GetHackerNewsMaxItem();
                return Ok(maxItem);
            }
        }


        /// <summary>
        /// Get object of requested stories
        /// </summary>
        /// <param name="includeTopStories"></param>
        /// <param name="includeNewStories"></param>
        /// <param name="includeBestStories"></param>
        /// <param name="includeAskStories"></param>
        /// <param name="includeJobStories"></param>
        /// <param name="includeShowStories"></param>
        /// <returns>Object with list of request stories</returns>
        [HttpGet("all")]
        public async Task<IActionResult> GetStories([FromQuery] bool includeTopStories, [FromQuery] bool includeNewStories, 
            [FromQuery] bool includeBestStories, [FromQuery] bool includeAskStories, [FromQuery] bool includeJobStories,
            [FromQuery] bool includeShowStories)
        {
            var stories = await _service.GetStoryListByType(new HackerNewsSearchOptions 
            { 
                IncludeNewStories = includeNewStories,
                IncludeAskStories = includeAskStories,
                IncludeBestStories = includeBestStories,
                IncludeJobStories = includeJobStories,
                IncludeShowStories = includeShowStories,
                IncludeTopStories = includeTopStories
            });
            return Ok(stories);
        }

        /// <summary>
        /// Get list of new stories
        /// Cached for 30seconds
        /// </summary>
        /// <param name="includeNewStories"></param>
        /// <returns>List of new stories</returns>
        [HttpGet("newstories")]
        [ResponseCache(Duration = 30, Location = ResponseCacheLocation.Any)]
        public async Task<IActionResult> GetNewStories([FromQuery] bool includeNewStories) => 
            Ok(await _service.GetStoryListByType(new HackerNewsSearchOptions { IncludeNewStories = includeNewStories}));
    }
}
