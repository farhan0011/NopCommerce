using System.Threading.Tasks;
using Nop.Plugin.Widgets.Books.Model;

namespace Nop.Plugin.Widgets.Books.Factories
{
    public interface IBookModelFactory
    {
        Task<BookListModel> PrepareBookListModelAsync(BookSearchModel searchModel);
        Task<BookSearchModel> PrepareBookSearchModelAsync(BookSearchModel searchModel);
    }
}