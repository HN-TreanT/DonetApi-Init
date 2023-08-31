using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetApi.CoreLib.Common
{
    public class Paging
    {
        public int PageSize { get; set; } = 100;
        public int PageNumber { get; set; } = 1;
    }
}