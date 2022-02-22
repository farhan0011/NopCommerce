using NopStation.Plugin.Widgets.MegaMenu.Models;
using System.Threading.Tasks;

namespace NopStation.Plugin.Widgets.MegaMenu.Factories
{
    public interface IMegaMenuModelFactory
    {
        Task<MegaMenuModel> PrepareMegaMenuModelAsync();
    }
}