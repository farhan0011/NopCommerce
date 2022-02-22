using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Data;
using Nop.Plugin.Widgets.Books.Domains;
using Nop.Plugin.Widgets.Books.Model;

namespace Nop.Plugin.Widgets.Books.Services
{
    public class BookService : IBookService
    {
        private readonly IRepository<Book> _repository;

        public BookService(IRepository<Book> repository)
        {
            _repository = repository;
        }
        public async Task AddBookAsync(BookModel model)
        {
            var domain = new Book()
            {
                Name = model.Name,
                Description = model.Description,
                UpdatedDate = DateTime.UtcNow,
                CreatedDate = DateTime.UtcNow,
                IsNew = model.IsNew,
            };
            await _repository.InsertAsync(domain);
        }

        public virtual async Task<IPagedList<Book>> GetAllBooksAsync()
        {
            var customers = await _repository.Table.ToPagedListAsync(pageIndex: 0, pageSize: int.MaxValue);

            return customers;
        }
    }
}
