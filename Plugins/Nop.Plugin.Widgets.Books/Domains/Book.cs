using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core;

namespace Nop.Plugin.Widgets.Books.Domains
{
    public class Book : BaseEntity
    {
        
        public string Name { get; set; }
        public string Description { get; set; }
       
        public DateTime CreatedDate { get; set; }
        
        public DateTime UpdatedDate { get; set; }

        public bool IsNew { get; set; }

        internal object mapperconfiguration<T>()
        {
            throw new NotImplementedException();
        }
    }
}
