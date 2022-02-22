using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Web.Areas.Admin.Controllers;
using Nop.Web.Areas.Admin.Factories;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using NopStation.Plugin.Widgets.MegaMenu.Areas.Admin.Models;
using System.Linq;
using System.Threading.Tasks;

namespace NopStation.Plugin.Widgets.MegaMenu.Areas.Admin.Controllers
{
    public class MegaMenuController : BaseAdminController
    {
        #region Fields

        private readonly IBaseAdminModelFactory _baseAdminModelFactory;
        private readonly ILocalizationService _localizationService;
        private readonly INotificationService _notificationService;
        private readonly IPermissionService _permissionService;
        private readonly ISettingService _settingService;
        private readonly IStoreContext _storeContext;
        private readonly MegaMenuSettings _megaMenuSettings;

        #endregion

        #region Ctor

        public MegaMenuController(
            IBaseAdminModelFactory baseAdminModelFactory,
            ILocalizationService localizationService,
            INotificationService notificationService,
            IPermissionService permissionService,
            ISettingService settingService,
            IStoreContext storeContext,
            MegaMenuSettings megaMenuSettings)
        {
            _baseAdminModelFactory = baseAdminModelFactory;
            _localizationService = localizationService;
            _notificationService = notificationService;
            _permissionService = permissionService;
            _settingService = settingService;
            _storeContext = storeContext;
            _megaMenuSettings = megaMenuSettings;
        }

        #endregion

        #region Methods

        public async Task<IActionResult> Configure()
        {
            if (!await _permissionService.AuthorizeAsync(MegaMenuPermissionProvider.ManageMegaMenu))
                return AccessDeniedView();

            var storeId = await _storeContext.GetActiveStoreScopeConfigurationAsync();
            var megaMenuSettings = await _settingService.LoadSettingAsync<MegaMenuSettings>(storeId);
            var model = megaMenuSettings.ToSettingsModel<ConfigurationModel>();

            await _baseAdminModelFactory.PrepareCategoriesAsync(model.AvailableCategories, false);
            await _baseAdminModelFactory.PrepareManufacturersAsync(model.AvailableManufacturers, false);

            if (!string.IsNullOrWhiteSpace(_megaMenuSettings.SelectedManufacturerIds))
                model.SelectedManufacturerIds = _megaMenuSettings.SelectedManufacturerIds.Split(',').Select(int.Parse).ToList();

            if (!string.IsNullOrWhiteSpace(_megaMenuSettings.SelectedCategoryIds))
                model.SelectedCategoryIds = _megaMenuSettings.SelectedCategoryIds.Split(',').Select(int.Parse).ToList();

            model.ActiveStoreScopeConfiguration = storeId;

            if (storeId <= 0)
                return View(model);

            model.EnableMegaMenu_OverrideForStore = await _settingService.SettingExistsAsync(megaMenuSettings, x => x.EnableMegaMenu, storeId);
            model.HideManufacturers_OverrideForStore = await _settingService.SettingExistsAsync(megaMenuSettings, x => x.HideManufacturers, storeId);
            //model.MaxCategoryLevelsToShow_OverrideForStore = await _settingService.SettingExistsAsync(megaMenuSettings, x => x.MaxCategoryLevelsToShow, storeId);
            model.SelectedCategoryIds_OverrideForStore = await _settingService.SettingExistsAsync(megaMenuSettings, x => x.SelectedCategoryIds, storeId);
            model.SelectedManufacturerIds_OverrideForStore = await _settingService.SettingExistsAsync(megaMenuSettings, x => x.SelectedManufacturerIds, storeId);          
            //model.ShowMainCategoryPictureRight_OverrideForStore = await _settingService.SettingExistsAsync(megaMenuSettings, x => x.ShowMainCategoryPictureRight, storeId);
            //model.ShowNumberOfCategoryProductsIncludeSubcategories_OverrideForStore = await _settingService.SettingExistsAsync(megaMenuSettings, x => x.ShowNumberOfCategoryProductsIncludeSubcategories, storeId);
            //model.ShowNumberofCategoryProducts_OverrideForStore = await _settingService.SettingExistsAsync(megaMenuSettings, x => x.ShowNumberOfCategoryProducts, storeId);
            //model.ShowManufacturerPicture_OverrideForStore = await _settingService.SettingExistsAsync(megaMenuSettings, x => x.ShowManufacturerPicture, storeId);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Configure(ConfigurationModel model)
        {
            if (!await _permissionService.AuthorizeAsync(MegaMenuPermissionProvider.ManageMegaMenu))
                return AccessDeniedView();

            var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();
            var megaMenuSettings = await _settingService.LoadSettingAsync<MegaMenuSettings>(storeScope);

            megaMenuSettings = model.ToSettings(megaMenuSettings);
            megaMenuSettings.SelectedCategoryIds = string.Join(",", model.SelectedCategoryIds);
            megaMenuSettings.SelectedManufacturerIds = string.Join(",", model.SelectedManufacturerIds);

            await _settingService.SaveSettingOverridablePerStoreAsync(megaMenuSettings, x => x.EnableMegaMenu, model.EnableMegaMenu_OverrideForStore, storeScope, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(megaMenuSettings, x => x.HideManufacturers, model.HideManufacturers_OverrideForStore, storeScope, false);
            //await _settingService.SaveSettingOverridablePerStoreAsync(megaMenuSettings, x => x.MaxCategoryLevelsToShow, model.MaxCategoryLevelsToShow_OverrideForStore, storeScope, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(megaMenuSettings, x => x.SelectedCategoryIds, model.SelectedCategoryIds_OverrideForStore, storeScope, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(megaMenuSettings, x => x.SelectedManufacturerIds, model.SelectedManufacturerIds_OverrideForStore, storeScope, false);          
            //await _settingService.SaveSettingOverridablePerStoreAsync(megaMenuSettings, x => x.ShowNumberOfCategoryProductsIncludeSubcategories, model.ShowNumberOfCategoryProductsIncludeSubcategories_OverrideForStore, storeScope, false);
            //await _settingService.SaveSettingOverridablePerStoreAsync(megaMenuSettings, x => x.ShowNumberOfCategoryProducts, model.ShowNumberofCategoryProducts_OverrideForStore, storeScope, false);
            //await _settingService.SaveSettingOverridablePerStoreAsync(megaMenuSettings, x => x.ShowManufacturerPicture, model.ShowManufacturerPicture_OverrideForStore, storeScope, false);         
            //await _settingService.SaveSettingOverridablePerStoreAsync(megaMenuSettings, x => x.ShowMainCategoryPictureRight, model.ShowMainCategoryPictureRight_OverrideForStore, storeScope, false);          
            await _settingService.ClearCacheAsync();

            _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.NopStation.MegaMenu.Configuration.Updated"));

            return RedirectToAction("Configure");
        }

        #endregion
    }
}
