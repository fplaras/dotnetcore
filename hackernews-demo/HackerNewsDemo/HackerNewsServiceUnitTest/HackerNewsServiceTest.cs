using HackerNewsModule;
using HackerNewsModule.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace HackerNewsServiceUnitTest
{
    [TestClass]
    public class HackerNewsServiceTest
    {
        private readonly IHackerNewsService _service;
        private HttpClient client = new HttpClient();
        public HackerNewsServiceTest()
        {
            _service = new HackerNewsService(new HackerNewsClient(client));
        }

        [TestMethod]
        public async Task GetHackerNewsStories()
        {
            try
            {
                List<int> stories = await _service.GetHackerNewsStories(HackerNewsEnums.StoryType.newstories);
                Assert.IsTrue(stories.Count > 0, "Retrieved New Stories");

                stories = await _service.GetHackerNewsStories(HackerNewsEnums.StoryType.askstories);
                Assert.IsTrue(stories.Count > 0, "Retrieved Ask Stories");

                stories = await _service.GetHackerNewsStories(HackerNewsEnums.StoryType.beststories);
                Assert.IsTrue(stories.Count > 0, "Retrieved Best Stories");

                stories = await _service.GetHackerNewsStories(HackerNewsEnums.StoryType.topstories);
                Assert.IsTrue(stories.Count > 0, "Retrieved Top Stories");
            }
            catch (AssertFailedException ex)
            {
                Assert.Fail(ex.Message);
            }
            
        }

        [TestMethod]
        public async Task GetHackerNewsMaxItem()
        {
            try
            {
                var maxItem = await _service.GetHackerNewsMaxItem();
                Assert.IsTrue(maxItem > 0, "Max Item ID Retrieved");
            }
            catch (AssertFailedException ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public async Task GetHackerNewsItem()
        {
            try
            {
                var maxItem = await _service.GetHackerNewsMaxItem();
                if(maxItem > 0)
                {
                    var item = await _service.GetHackerNewsItemById(maxItem.ToString());

                    Assert.IsTrue(item.Id > 0, "Max Item Retrievable");
                }
                else
                {
                    Assert.Fail("Invalid Max Item");
                }
            }
            catch (AssertFailedException ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
