using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Widgets.Books.Factories;
using Nop.Plugin.Widgets.Books.Model;
using Nop.Plugin.Widgets.Books.Services;
using Nop.Web.Areas.Admin.Controllers;

namespace Nop.Plugin.Widgets.Books.Controllers
{
    public class WidgetsBooksController : BaseAdminController
    {
        private readonly IBookService _service;
        private readonly IBookModelFactory _bookModelFactory;

        public WidgetsBooksController(IBookService service, IBookModelFactory bookModelFactory)
        {
            _service = service;
            _bookModelFactory = bookModelFactory;
        }

        public virtual async Task<IActionResult> Configure()
        {

            //prepare model
            var model = await _bookModelFactory.PrepareBookSearchModelAsync(new BookSearchModel());

            return View("~/Plugins/Widgets.Books/Views/Configure.cshtml", model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> BookList(BookSearchModel searchModel)
        {

            //prepare model
            var model = await _bookModelFactory.PrepareBookListModelAsync(searchModel);

            return Json(model);
        }
        

        public  async Task<IActionResult> Create()
        {
            var model = new BookModel();

            return View("~/Plugins/Widgets.Books/Views/Books/Create.cshtml", model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(BookModel model)
        {
            await _service.AddBookAsync(model);
            return RedirectToAction();
        }
    }
}
