using HackerNewsModule.Enums;
using HackerNewsModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerNewsModule
{
    public interface IHackerNewsService
    {
        Task<HackerNewsItem> GetHackerNewsItemById(string Id);
        Task<int> GetHackerNewsMaxItem();
        Task<List<int>> GetHackerNewsStories(HackerNewsEnums.StoryType type);
        Task<HackerNewsStories> GetStoryListByType(HackerNewsSearchOptions opts);
    }
}
