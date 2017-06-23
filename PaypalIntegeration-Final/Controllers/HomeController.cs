using Braintree;
using PaypalIntegeration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PaypalIntegeration.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string amount,string currency)
        {
            ViewBag.Amount = string.IsNullOrEmpty(amount) ? "300" : amount;
            ViewBag.Currency = string.IsNullOrEmpty(currency) ? "AUD" : currency;
            BraintreeGateway gateway = new BraintreeGateway("access_token$sandbox$yd97mgp557fjzzrh$f33ed4c7d73fc5111311585183e33b94");
            var clientToken = gateway.ClientToken.generate();
            Paypal payModel = new Paypal() { ClientToken = clientToken };
            return View(payModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public JsonResult PaymentNonce(string paymentNonce, string amount) {
            BraintreeGateway gateway = new BraintreeGateway("access_token$sandbox$yd97mgp557fjzzrh$f33ed4c7d73fc5111311585183e33b94");

            decimal netAmount = 300.00M;
            Decimal.TryParse(amount,  out netAmount);
            TransactionRequest request = new TransactionRequest
            {
                Amount = netAmount,
                PaymentMethodNonce = paymentNonce,
                Options = new TransactionOptionsRequest
                {
                    SubmitForSettlement = true,
                }
            };

            string exceptionMessage = string.Empty;
            string transactionID =string.Empty ;
            string paymentID = string.Empty;
            try
            {
                Result<Transaction> result = gateway.Transaction.Sale(request);
                exceptionMessage = result.Message;

                if (result.IsSuccess())
                {
                    Transaction transaction = result.Target;
                    transactionID = transaction.Id;
                    paymentID = result.Target.PayPalDetails.PaymentId;
                }
                else
                {
                    exceptionMessage = "No transaction record has been recieved.";
                }
            }
            catch (Exception e)
            {
                exceptionMessage = e.Message;
                
            }


                         
            return Json( new TransactionResult() { TransactionID= transactionID, PaymentID=paymentID, Message=exceptionMessage});
        }
    }
}