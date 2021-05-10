using AutoMapper;
using HackerNewsModule;
using HackerNewsModule.Enums;
using HackerNewsModule.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNewsDemo.Controllers
{
    [Route("api/HackerNews/v2")]
    [ApiController]
    public class HackerNewsV2 : ControllerBase
    {
        private readonly IHackerNewsService _service;
        private readonly IMapper _mapper;

        public HackerNewsV2(IHackerNewsService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;

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
            var item = await _service.GetHackerNewsItemDetails(id);
            var hackerNewsDTO = _mapper.Map<HackerNewsDemoDTO>(item);

            if (item.Kids != null)
            {
                var itemChildren = await _service.GetHackerNewsItemDetails(item.Kids);
                hackerNewsDTO.Kids = _mapper.Map<List<HackerNewsDemoDTO>>(itemChildren); ;
            }

            if (item.Parts != null)
            {
                var itemParts = await _service.GetHackerNewsItemDetailsV2(item.Parts);
                hackerNewsDTO.Kids = _mapper.Map<List<HackerNewsDemoDTO>>(itemParts); ;
            }

            return Ok(hackerNewsDTO);
        }

        /// <summary>
        /// Return Stories by Story TYpe
        /// </summary>
        /// <param name="storyType"></param>
        /// <returns>List of Stories</returns>
        [HttpGet("all/{storyType}/details")]
        public async Task<IActionResult> GetStoriesAndDetails([FromRoute] string storyType, 
            [FromQuery] int iteration = 1, [FromQuery] int requestSize = 30)
        {
            if (Enum.IsDefined(typeof(HackerNewsEnums.StoryType), storyType))
            {
                var itemList = await _service.GetHackerNewsStories((HackerNewsEnums.StoryType)Enum.Parse(typeof(HackerNewsEnums.StoryType), storyType));
                itemList = itemList.Skip((iteration - 1) * requestSize).Take(requestSize).ToList();
                var stories = await _service.GetHackerNewsItemDetailsV2(itemList);
                return Ok(stories);
            }
            else
            {
                return BadRequest("Story Typoe does not exist.");
            }
        }
    }
}
