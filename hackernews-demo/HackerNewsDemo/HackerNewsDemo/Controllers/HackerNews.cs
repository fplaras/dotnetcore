using HackerNewsModule;
using HackerNewsModule.Enums;
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
    [ResponseCache(Duration = 30, Location = ResponseCacheLocation.Any)]
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
        public async Task<IActionResult> Item([FromRoute] int id)
        {
            var response = await _service.GetHackerNewsItemDetails(id);
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
                var item = await _service.GetHackerNewsItemDetails(maxItem);
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
        /// 
        /// </summary>
        /// <param name="storyType"></param>
        /// <returns></returns>
        [HttpGet("all/{storyType}/details")]
        public async Task<IActionResult> GetStories([FromRoute] string storyType)
        {
            if (Enum.IsDefined(typeof(HackerNewsEnums.StoryType), storyType))
            {
                var itemList = await _service.GetHackerNewsStories((HackerNewsEnums.StoryType)Enum.Parse(typeof(HackerNewsEnums.StoryType), storyType));
                var stories = await _service.GetHackerNewsItemDetails(itemList);
                return Ok(stories);
            }
            else
            {
                return BadRequest("Story Typoe does not exist.");
            }
        }
        
        /// <summary>
        /// Get details of a list of items
        /// </summary>
        /// <param name="itemList"></param>
        /// <returns>List of items with details</returns>
        [HttpGet("item/list/details")]
        public async Task<IActionResult> GetManyItemDetails([FromQuery] string itemList)
        {
            if (string.IsNullOrEmpty(itemList))
                return BadRequest("No items listed in request");

            //try to parse all elements of the string to int
            (List<string> InvalidItems, List<int> ValidItems) processedItems = await ProcessItemListRequest(itemList);

            if(processedItems.InvalidItems.Count > 0)
            {
                return BadRequest("Invalid Item IDs: " + string.Join(",",processedItems.InvalidItems));
            }

            if (processedItems.ValidItems.Count == 0)
            {
                return BadRequest("No items listed in request");
            }

            var itemListDetails = await _service.GetHackerNewsItemDetails(processedItems.ValidItems);

            return Ok(itemListDetails);
        }




        #region Helper Methods
        private async Task<(List<string> InvalidItems, List<int> ValidItems)> ProcessItemListRequest(string itemList)
        {
            List<int> validItems = new List<int>();
            List<string> invalidItems = new List<string>();
            foreach (var item in itemList.Split(","))
            {
                int itemId = 0;
                if (int.TryParse(item, out itemId))
                {
                    validItems.Add(itemId);
                }
                else
                {
                    invalidItems.Add(item);
                }
            }

            (List<string>, List<int>) processedItems = new (invalidItems, validItems);

            return processedItems;
        }
        #endregion
    }
}
