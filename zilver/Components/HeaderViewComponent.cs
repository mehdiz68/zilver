using Microsoft.AspNetCore.Mvc;
using zilver.ViewModels;

namespace zilver.Components
{
    public class HeaderViewComponent : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            HeaderViewModel header = new HeaderViewModel
            {
                logo = "/images/logo.png"
            };

            return View(header);
        }
    }
}
