using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Web.Framework.Models;

namespace Nop.Plugin.Widgets.Books.Model
{
    public record BookModel : BaseNopEntityModel
    {
        

        [Display(Name ="Enter a Name")]
        public string Name { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [DataType(DataType.DateTime)]
        [Display(Name ="Created Date")]
        public DateTime CreatedDate { get; set; }
        [DataType(DataType.DateTime)]
        [Display(Name="Updated Date")]
        public DateTime UpdatedDate { get; set; }
        [Display(Name ="It is New")]
        public bool IsNew { get; set; }
    }
}
