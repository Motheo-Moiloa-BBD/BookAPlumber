using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookAPlumber.Core.Exceptions
{
    public class ExceptionResponse
    {
        public Guid Id { get; set; }
        public int StatusCode { get; set; }
        public string? StatusMessage { get; set; }
    }
}
