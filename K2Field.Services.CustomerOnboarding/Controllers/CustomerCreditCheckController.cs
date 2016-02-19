using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace K2Field.Services.CustomerOnboarding.Controllers
{
    public class CustomerCreditCheckController : ApiController
    {
        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = true)]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(CreditCheckResult))]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        public IHttpActionResult AutoApprovalCheck([FromUri]int Age, [FromUri]int Rating, [FromUri]decimal CreditRating)
        {
            CreditCheckResult result = new CreditCheckResult();

            try
            {
                if (Rating < 1)
                {
                    Rating = 5;
                }

                if (CreditRating < 1)
                {
                    CreditRating = 650;
                }

                if(Age < 1)
                {
                    Age = 34;
                }

                result.Age = Age;
                result.Rating = Rating;
                result.CreditRating = CreditRating;
                result.CalculatedRating = (Rating * Decimal.Floor(CreditRating)) / Age;

                if (CreditRating < 701)
                {
                    result.AutoApproved = false;                    
                }
                else
                {
                    result.AutoApproved = true;
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                
            }
            base.Dispose(disposing);
        }

    }

    public class CreditCheckResult
    {
        public bool AutoApproved { get; set; }
        public decimal CalculatedRating { get; set; }
        public decimal CreditRating { get; set; }
        public int Rating { get; set; }
        public int Age { get; set; }

    }
}
