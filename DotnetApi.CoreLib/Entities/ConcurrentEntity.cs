using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetApi.CoreLib.Entities
{
    public class ConcurrentEntity
    {
        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}