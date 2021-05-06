using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using viewcomponent_samples.Services.Interfaces;

namespace viewcomponent_samples.Services
{
    public class AnnouncementService : IAnnouncementService
    {
        protected IApiRequestClient _apiClient;

        public AnnouncementService(IApiRequestClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task GetAnnouncements()
        {
            
        }
    }
}
