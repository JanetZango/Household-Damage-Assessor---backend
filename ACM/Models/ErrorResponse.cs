using System;
using System.Collections.Generic;
using System.Text;
namespace ACM.Models
{
    public class ErrorResponse
    {
        public bool IsError { get; set; }
        public string ErrorMessage { get; set; }
    }
}
