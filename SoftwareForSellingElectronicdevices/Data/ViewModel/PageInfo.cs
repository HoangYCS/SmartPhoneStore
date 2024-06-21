using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Data.ViewModel
{
    public class PageInfo
    {
        public int TotalNumber { get; set; }
        public int NumberItemInThePage { get; set; } = 8;
        public int CurrentPage { get; set; } = 1;
        public int NumberOfPages => (int)Math.Ceiling((double)TotalNumber / NumberItemInThePage);
    }
}
