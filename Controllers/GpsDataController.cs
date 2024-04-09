using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using SpeedSight.Dto;
using SpeedSight.Interfaces;
using SpeedSight.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace SpeedSight.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GpsDataController : Controller
    {
        private readonly IGpsDataRepository _gpsDataRepository;
        private readonly IMapper _mapper;

        public GpsDataController(IGpsDataRepository gpsDataRepository, IMapper mapper)
        {
            _gpsDataRepository = gpsDataRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<GpsData>))]
        public IActionResult GetGpsDatas()
        {
            var data = _gpsDataRepository.GetGpsDatas();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(data);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(GpsData))]
        [ProducesResponseType(400)]
        public IActionResult GetGpsData(int id)
        {
            if(!_gpsDataRepository.GpsDataExists(id))
                return NotFound();

            var data = _gpsDataRepository.GetGpsData(id);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(data);
        }

        [HttpGet("{id}/avgSpeedByID")]
        [ProducesResponseType(200, Type = typeof(double))]
        [ProducesResponseType(400)]
        public IActionResult GetAvgSpeed(int id)
        {
            if (!_gpsDataRepository.GpsDataExists(id))
                return NotFound();

            var avgSpeed = _gpsDataRepository.GetAvgSpeedForId(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(avgSpeed);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateGpsData([FromBody] GpsDataDto gpsDataCreate)
        {
            if (gpsDataCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var gpsDataMap = _mapper.Map<GpsData>(gpsDataCreate);

            if (!_gpsDataRepository.CreateGpsData(gpsDataMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");

        }

        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult UpdateGpsData(int id, [FromBody] GpsDataDto updatedGpsData)
        {
            if (updatedGpsData == null)
                return BadRequest(ModelState);
            
            if (id != updatedGpsData.Id)
                return BadRequest(ModelState);

            if(!_gpsDataRepository.GpsDataExists(id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var gpsDataMap = _mapper.Map<GpsData>(updatedGpsData);

            if (!_gpsDataRepository.UpdateGpsData(gpsDataMap))
            {
                ModelState.AddModelError("", "Somethong went wrong updating GpsData");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult DeleteGpsData(int id) 
        {
            if (!_gpsDataRepository.GpsDataExists(id))
                return NotFound();

            var GpsDataToDelete = _gpsDataRepository.GetGpsData(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_gpsDataRepository.DeleteGpsData(GpsDataToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting gps data");
            }

            return NoContent();
        }

        [HttpGet("allAvgSpeed")]
        [ProducesResponseType(200, Type = typeof(Dictionary<int,double>))]
        [ProducesResponseType(400)]
        public IActionResult GetAvgSpeedAll()
        {

            var allAvgSpeed = _gpsDataRepository.GetAvgSpeedForAll();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(allAvgSpeed);
        }
    }
}
