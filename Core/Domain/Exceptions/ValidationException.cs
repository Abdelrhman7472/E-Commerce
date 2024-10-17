using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class ValidationException : Exception

    {
        public IEnumerable<string> Errors { get; set; } // 3shan lw 3andy magmo3et errors fe ay goz2 fel register 
        public ValidationException(IEnumerable<string> errors):base("Validation Failed")
        {
            Errors = errors;
        }
    }
}
