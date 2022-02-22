using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework.Components;
using NopStation.Plugin.Widgets.MegaMenu.Factories;
using System.Threading.Tasks;

namespace NopStation.Plugin.Widgets.MegaMenu.Components
{
    public class MegaMenuViewComponent : NopViewComponent
    {
        private readonly IMegaMenuModelFactory _megaMenuModelFactory;

        public MegaMenuViewComponent(IMegaMenuModelFactory megaMenuModelFactory)
        {
            _megaMenuModelFactory = megaMenuModelFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _megaMenuModelFactory.PrepareMegaMenuModelAsync();
            return View(model);
        }
    }
}
