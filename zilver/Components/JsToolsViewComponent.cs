using Microsoft.AspNetCore.Mvc;
using zilver.ViewModels;

namespace zilver.Components
{
    public class JsToolsViewComponent : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            JsToolsViewModel model = new JsToolsViewModel();

            return View(model);
        }
    }
}
