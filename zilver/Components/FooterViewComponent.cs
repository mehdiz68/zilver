using Microsoft.AspNetCore.Mvc;
using zilver.ViewModels;

namespace zilver.Components
{
    public class FooterViewComponent: ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            FooterViewModel footer = new FooterViewModel
            {
                logo = "/images/logo.png"
            };

            return View(footer);
        }
    }
}
