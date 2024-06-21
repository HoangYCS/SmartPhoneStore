using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Responses
{
    public record PopularResponse<T>(bool success, string Notification,T? ObjectReturn = default(T), int? key = null);

}
