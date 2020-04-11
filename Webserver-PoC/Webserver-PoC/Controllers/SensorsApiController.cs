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
    public class SensorsApiController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/SensorsApi
        [Route("API/Sensors")]
        public ApiSensor[] GetSensors()
        {
            return db.Sensors.ToArray().Select(sensor => ConvertToApiModel(sensor)).ToArray();
        }

        // GET: api/SensorsApi/5
        [Route("API/Sensors/{id}")]
        [ResponseType(typeof(ApiSensor))]
        public IHttpActionResult GetSensor(int id)
        {
            Sensor sensor = db.Sensors.Find(id);
            if (sensor == null)
            {
                return NotFound();
            }

            return Ok(ConvertToApiModel(sensor));
        }

        private ApiSensor ConvertToApiModel(Sensor sensor)
        {
            return (new ApiSensor() 
            { 
                sensor_id = sensor.sensor_id,
                name = sensor.name,
                location_description = sensor.location_description,
                longitude = sensor.longitude,
                latitude = sensor.latitude
            });
        }
    }
}