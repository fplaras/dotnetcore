using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using viewcomponents_razor.Models;
using System.Collections.Generic;

namespace viewcomponents_razor.Views.MembersWebsite.Components.Announcements
{
    [ViewComponent]
    public class Announcements : ViewComponent
    {

      private AnnouncementService _announcementService;
        public Announcements(AnnouncementService announcementService)
        {
            _announcementService = announcementService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string viewName)
        {
            IEnumerable<AnnouncementModel> _announcementList = await _announcementService.GetAllAnnouncements();
            return View(viewName,_announcementList);
        }
    }
}