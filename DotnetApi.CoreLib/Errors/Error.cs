using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetApi.CoreLib.Errors
{
    public class Error : Exception
    {
        public Error(string message) : base(message) { }

    }
}