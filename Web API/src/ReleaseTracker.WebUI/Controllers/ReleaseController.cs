using System.ComponentModel;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using Newtonsoft.Json;
using ReleaseTracker.Core.Interfaces;
using ReleaseTracker.DataAccess;

namespace ReleaseTrackers.Controllers
{
    public class ReleaseController : ApiController
    {
        private IReleaseService _Service;

        public ReleaseController(IReleaseService ServiceIn)
        {
            _Service = ServiceIn;
        }

        [System.Web.Http.HttpGet]
        public IHttpActionResult GetAssociatedReleases(int WorkItemId)
        {
            var Releases = _Service.GetReleases(WorkItemId);


            return Ok(JsonConvert.SerializeObject(Releases));
        }
    }
}