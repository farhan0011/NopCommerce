using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Plugin.Widgets.Books.Domains;
using Nop.Plugin.Widgets.Books.Model;

namespace Nop.Plugin.Widgets.Books.Services
{
    public interface IBookService
    {
        Task AddBookAsync(BookModel model);
        Task<IPagedList<Book>> GetAllBooksAsync();
    }
}