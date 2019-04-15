using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Assessment.Controllers
{
    [RoutePrefix("api/arraycal")]
    public class ArrayCalController : ApiController
    {
        [HttpGet]
        public IEnumerable<string> Get() => new string[] { "value1", "value2" };

        // GET api/values/5
        [Route("reverse")]
        [HttpGet]
        public IHttpActionResult GetByName()
        {
            int[] ProdIDs = null;
            if (!String.IsNullOrWhiteSpace(Request.RequestUri.Query))
            {
                ProdIDs = Array.ConvertAll(HttpUtility.ParseQueryString(Request.RequestUri.Query.Substring(1))["productIds"].Split(','), int.Parse);
                for (int i = 0, j = ProdIDs.Length - 1; i < j; i++, j--)
                {
                    int c = ProdIDs[i];
                    ProdIDs[i] = ProdIDs[j];
                    ProdIDs[j] = c;
                }
            }
            return Ok(ProdIDs);
        }

        // GET api/values/5
        [Route("deletepart")]
        [HttpGet]
        public IHttpActionResult GetBydelete()
        {
            int[] ProdIDs = null;
            int pos;
            int i = 0;
            string AfterPod = "";
            if (!String.IsNullOrWhiteSpace(Request.RequestUri.Query))
            {
                ProdIDs = Array.ConvertAll(HttpUtility.ParseQueryString(Request.RequestUri.Query.Substring(1))["productIds"].Split(','), int.Parse);
                pos = Convert.ToInt32(HttpUtility.ParseQueryString(Request.RequestUri.Query.Substring(0))["position"]);

                for (i = pos - 1; i < ProdIDs.Length - 1; i++)
                {
                    ProdIDs[i] = ProdIDs[i + 1];
                }
                for (i = 0; i < ProdIDs.Length - 1; i++)
                {
                    AfterPod += ProdIDs[i];
                }
            }
            return Ok(String.Join(",", AfterPod.ToCharArray(0, AfterPod.Length)));
        }
    }
}
