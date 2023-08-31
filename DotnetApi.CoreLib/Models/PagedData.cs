using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetApi.CoreLib.Models
{
    public class PagedData<T>

    {
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
        public bool CanNext { get; set; }
        public bool CanBack { get; set; }
        public List<T> Items { get; set; }
        public int TotalItem { get; set; }

    }
}