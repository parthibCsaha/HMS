using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Application.Common.Exceptions
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException()
            : base("Access to this resource is forbidden.") { }

        public ForbiddenException(string message)
            : base(message) { }
    }
}
