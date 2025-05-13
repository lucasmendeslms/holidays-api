using System.Globalization;
using HolidayApi.Data.DTOs;
using HolidayApi.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace HolidayApi.Controllers
{
    [Route("/feriados/{ibgeCode:int}")]
    [ApiController]
    public class HolidayController : ControllerBase
    {
        private readonly IHolidayService _service;

        public HolidayController(IHolidayService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<HolidayDto>>> FindAllHolidays(int ibgeCode)
        {
            Location location = new Location(ibgeCode);

            var hollidaysList = await _service.FindAllHolidays(location);

            return Ok(hollidaysList);
        }


        [HttpPut("{date}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> RegisterHoliday(
            [FromQuery] int ibgeCode, [FromQuery] string date, [FromBody] string name)
        {
            HolidayDate valueDate = new HolidayDate(date);

            return Created();
        }
    }

}