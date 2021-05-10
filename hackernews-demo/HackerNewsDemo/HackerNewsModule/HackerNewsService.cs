using AutoMapper;
using HackerNewsModule.Domain;
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
        private readonly HackerNewsDataContext _context;
        private readonly IMapper _mapper;


        public HackerNewsService(HackerNewsClient client, HackerNewsDataContext context, IMapper mapper)
        {
            _client = client;
            _context = context;
            _mapper = mapper;
        }

        public async Task<Models.HackerNewsItem> GetHackerNewsItemDetails(int Id)
        {
            HackerNewsItem item = await _client.GetItem(Id);
            return item;
        }

        public async Task<List<Domain.HackerNewsItemDomain>> GetHackerNewsItemDetailsV2(List<int> Ids)
        {
            List<Domain.HackerNewsItemDomain> itemList = new List<Domain.HackerNewsItemDomain>();

            foreach (var x in Ids)
            {
                var dItem = await _context.HackerNewsItem.FindAsync(x);

                if (dItem == null)
                {
                    HackerNewsItem item = await GetHackerNewsItemDetails(x);

                    var newItem = _mapper.Map<HackerNewsItemDomain>(item);

                    await _context.HackerNewsItem.AddAsync(newItem);

                    itemList.Add(newItem);
                }
                else
                {
                    itemList.Add(dItem);
                }
            }

            await _context.SaveChangesAsync();

            return itemList;
        }

        public async Task<List<Models.HackerNewsItem>> GetHackerNewsItemDetails(List<int> Ids)
        {
            List<Models.HackerNewsItem> itemList = new List<Models.HackerNewsItem>();

            foreach(var x in Ids)
            {
                Models.HackerNewsItem item = new Models.HackerNewsItem();

                item = await GetHackerNewsItemDetails(x);

                if (item != null && !item.Deleted)
                    itemList.Add(item);
            }

            return itemList;
        }

        public async Task<int> GetHackerNewsMaxItem()
        {
            int maxItem = await _client.GetMaxItem();
            return maxItem;
        }

        public async Task<List<int>> GetHackerNewsStories(HackerNewsEnums.StoryType type) => 
            await _client.GetStoriesByType(type);

        public async Task<HackerNewsUpdates> GetHackerNewsItemUpdates()
        {
            return await _client.GetHackerNewsUpdates();
        }

        #region Business Logic
        public async Task<HackerNewsStories> GetStoryListByType(HackerNewsSearchOptions opts)
        {
            HackerNewsStories stories = new HackerNewsStories();
            if (opts.IncludeNewStories)
            {
                stories.NewStories = await GetHackerNewsStories(HackerNewsEnums.StoryType.newstories);
            }

            if (opts.IncludeAskStories)
            {
                stories.AskStories = await GetHackerNewsStories(HackerNewsEnums.StoryType.askstories);
            }

            if (opts.IncludeBestStories)
            {
                stories.BestStories = await GetHackerNewsStories(HackerNewsEnums.StoryType.beststories);
            }

            if (opts.IncludeJobStories)
            {
                stories.JobStories = await GetHackerNewsStories(HackerNewsEnums.StoryType.jobstories);
            }

            if (opts.IncludeShowStories)
            {
                stories.ShowStories = await GetHackerNewsStories(HackerNewsEnums.StoryType.showstories);
            }

            if (opts.IncludeTopStories)
            {
                stories.TopStories = await GetHackerNewsStories(HackerNewsEnums.StoryType.topstories);
            }

            return stories;
        }

        public void DoWork()
        {
            // var itemList =  GetHackerNewsItemUpdates().Result;
            var itemList = GetHackerNewsStories(HackerNewsEnums.StoryType.newstories).Result;

            foreach (var itemId in itemList)
            {
                //get item
                var item = GetHackerNewsItemDetails(itemId).Result;

                var domainItem =  _context.HackerNewsItem.FindAsync(itemId).Result;

                if (domainItem != null)
                {
                    domainItem.Title = item.Title;
                    domainItem.Text = item.Text;
                    domainItem.Score = item.Score;
                    domainItem.Descendants = item.Descendants;

                    _context.HackerNewsItem.Update(domainItem);
                    _context.SaveChanges();
                }

            }
        }
        #endregion Business Logic
    }
}
