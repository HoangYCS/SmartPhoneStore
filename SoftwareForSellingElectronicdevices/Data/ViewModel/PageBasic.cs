using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ViewModel
{
    public class PageBasic<T>
    {
        public List<T> ListItem { get; set; }
        public PageInfo Info { get; set; } = new PageInfo();
    }
}
