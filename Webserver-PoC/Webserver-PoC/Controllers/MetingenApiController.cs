using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Webserver_PoC.Models;

namespace Webserver_PoC.Controllers
{
    public class MetingenApiController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/MetingenApi
        [Route("API/Metingen")]
        public ApiMeting[] GetMetings()
        {
            return db.Metings.ToArray().Select(meting => ConvertToApiModel(meting)).ToArray(); ;
        }

        // GET: api/MetingenApi/5
        [Route("API/Metingen/{id}")]
        [ResponseType(typeof(Meting))]
        public IHttpActionResult GetMeting(int id)
        {
            Meting meting = db.Metings.Find(id);
            if (meting == null)
            {
                return NotFound();
            }

            return Ok(ConvertToApiModel(meting));
        }

        private ApiMeting ConvertToApiModel(Meting meting)
        {
            return (new ApiMeting()
            {
                meting_id = meting.meting_id,
                meting_count = meting.meting_count,
                received_timestamp = meting.received_timestamp,
                sensor_id = meting.sensor_id
            });
        }
    }
}