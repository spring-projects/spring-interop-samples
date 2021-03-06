﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Spring.Interop.StockTraderSample.Common.Data;

namespace Spring.Interop.StockTraderSample.CreditCheckService.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEnumerable<string> _failureReasons = new List<string>()
                                                          {
                                                              "Insufficient Funds.",
                                                              "I don't like you very much.",
                                                              "We don't serve your kind here.",
                                                              "Get out of my sight and don't come back!"
                                                          };

        public string Message { get; set; }

        public ActionResult Index()
        {
            ViewBag.Message = Message;

            return View();
        }
       
        public ActionResult CreditCheck(string accountName, decimal purchaseValue)
        {
            var response = AssembleRandomCreditCheckResponse(accountName, purchaseValue);
            var json = JsonConvert.SerializeObject(response, Formatting.None, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

            return Content(json, "application/json");

        }

        private CreditCheckResponse AssembleRandomCreditCheckResponse(string account, decimal purchaseValue)
        {
            /*
            var rnd = new Random();
            var reasonIndex = rnd.Next(0, _failureReasons.Count() - 1);
            var passFail = rnd.Next(1, 100) % 5.0 == 0 ? false : true;
            */
            if (purchaseValue > 10000)
            {
                return new CreditCheckResponse(account, purchaseValue, false, "Insufficient Funds.");
            }
            return new CreditCheckResponse(account, purchaseValue, true, "");
        }
    }
}
