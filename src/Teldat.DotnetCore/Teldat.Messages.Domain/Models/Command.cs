using System;
using System.Collections.Generic;
using System.Text;

namespace Teldat.Messages.Domain.Models
{
    public class Command
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string ToUnit { get; set; }
    }
}
