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
        Task<HackerNewsItem> GetHackerNewsItemDetails(int Id);
        Task<List<HackerNewsItem>> GetHackerNewsItemDetails(List<int> Ids);
        Task<int> GetHackerNewsMaxItem();
        Task<List<int>> GetHackerNewsStories(HackerNewsEnums.StoryType type);
        Task<HackerNewsStories> GetStoryListByType(HackerNewsSearchOptions opts);

        Task<List<Domain.HackerNewsItemDomain>> GetHackerNewsItemDetailsV2(List<int> Ids);

        Task<HackerNewsUpdates> GetHackerNewsItemUpdates();
        void DoWork();
    }
}
