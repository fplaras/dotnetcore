using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using viewcomponents_razor.Models;

namespace viewcomponents_razor.Views.MembersWebsite.Components.Announcements
{
    public class AnnouncementService
    {
        private IEnumerable<AnnouncementModel> _announcementList;
        private AnnouncementModel _announcement;

        public AnnouncementService(IEnumerable<AnnouncementModel> announcementList)
        {
            _announcementList = new List<AnnouncementModel>() { new AnnouncementModel() { ID = 1, Author = "Frank", Text = "Test 1" } };
        }

        public async Task<IEnumerable<AnnouncementModel>> GetAllAnnouncements()
        {
            return _announcementList;
        }
    }
}
