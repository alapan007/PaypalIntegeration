using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaypalIntegeration.Models
{
    public class TransactionResult
    {
        public string TransactionID { get; set; }
        public string Message { get; set; }
        public string PaymentID { get; set; }
    }
}