using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Plugin.Widgets.Books.Model;
using Nop.Plugin.Widgets.Books.Services;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Nop.Web.Framework.Models.Extensions;

namespace Nop.Plugin.Widgets.Books.Factories
{
    public partial class BookModelFactory : IBookModelFactory
    {
        private readonly IBookService _bookService;

        public BookModelFactory(IBookService bookService)
        {
            _bookService = bookService;
        }
        public virtual async Task<BookSearchModel> PrepareBookSearchModelAsync(BookSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //prepare page parameters
            searchModel.SetGridPageSize();

            return searchModel;
        }

        public virtual async Task<BookListModel> PrepareBookListModelAsync(BookSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));



            //get customers
            var books = await _bookService.GetAllBooksAsync();

            //prepare list model
            var model = await new BookListModel().PrepareToGridAsync(searchModel, books, () =>
            {
                return books.SelectAwait(async book =>
                {

                    //fill in model values from the entity
                    var bookModel = book.ToModel<BookModel>();

                    return bookModel;
                });
            });

            return model;
        }
    }
}
