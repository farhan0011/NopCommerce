using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Widgets.Books.Model
{
    public partial record class BookSearchModel : BaseSearchModel
    {

        
        public string Name { get; set; }


        public string Description { get; set; }


        [DataType(DataType.DateTime)]
        

        public string SearchDay { get; set; }

        [NopResourceDisplayName("Admin.Customers.Customers.List.SearchDateOfBirth")]
        public string SearchMonth { get; set; }

        public bool DateEnabled { get; set; }

        public bool IsNew { get; set; }
    }
}
