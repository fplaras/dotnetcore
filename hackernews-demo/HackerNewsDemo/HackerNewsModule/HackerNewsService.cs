using HackerNewsModule.Enums;
using HackerNewsModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerNewsModule
{
    public class HackerNewsService : IHackerNewsService
    {
        private readonly HackerNewsClient _client;

        public HackerNewsService(HackerNewsClient client)
        {
            _client = client;
        }

        public async Task<HackerNewsItem> GetHackerNewsItemById(string Id)
        {
            HackerNewsItem itemList = new HackerNewsItem();

            itemList = await _client.GetItem(Id);

            return itemList;
        }

        public async Task<int> GetHackerNewsMaxItem()
        {
            int maxItem = 0;
            maxItem = await _client.GetMaxItem();
            return maxItem;
        }

        public async Task<List<int>> GetHackerNewsStories(HackerNewsEnums.StoryType type) => 
            await _client.GetStoriesByType(type);
        

        #region Business Logic
        public async Task<HackerNewsStories> GetStoryListByType(HackerNewsSearchOptions opts)
        {
            HackerNewsStories stories = new HackerNewsStories();
            if (opts.IncludeNewStories)
            {
                stories.NewStories = await GetHackerNewsStories(HackerNewsEnums.StoryType.newstories);
            }

            return stories;
        }
        #endregion Business Logic
    }
}
