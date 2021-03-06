﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using CashflowCalculator.Models;
using System.Web.Http.Cors;
using CashflowCalculator.DAL;

namespace CashflowCalculator.API.Controllers
{
    [EnableCors("*", "*", "*")]


    public class CashflowCalculatorController : ApiController
    {
        Loan[] loans = new Loan[]
        {
            new Loan { Amount = 4511, Duration = 12, Rate = 5/100 },
            new Loan { Amount = 1244, Duration = 6, Rate = 1/100 },
            new Loan { Amount = 45641, Duration = 12, Rate = 2/100 }
        };

        public IEnumerable<Loan> GetAllProducts()
        {
            return loans;
        }

        [HttpPost]
        public IHttpActionResult AddLoan([FromBody] Loan loanInput)
        {
            var id = CashflowCalculator.DAL.Service.LoanService.AddLoan(loanInput);
            if (0 < id)
                return Ok(id);
            else
                return BadRequest("Unable to save loan");
        }

        [HttpGet]
        public IHttpActionResult GetLoans()
        {
            var loans = DAL.Service.LoanService.GetLoans();
            if (loans == null)
            {
                return BadRequest("Unable to retrieve loans");
            }

            return Ok(loans);
        }

        [HttpGet]
        public IHttpActionResult GetCashFlows()
        {
            var cashflows = DAL.Service.CashflowService.GetCashFlows();  
            if (cashflows == null)
            {
                return BadRequest("Unable to retrieve cashflows");
            }

            return Ok(cashflows);
        }

        [HttpPost]
        public IHttpActionResult deleteLoans([FromBody] List<int> loans)
        {
            var id = DAL.Service.LoanService.deleteLoans(loans);
            if (0 < id)
                return Ok(id);
            else
                return BadRequest("Unable delete loans");
        }

    }
}
