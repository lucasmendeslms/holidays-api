using System.Globalization;
using HolidayApi.Data.DTOs;
using HolidayApi.Services.Interfaces;
using HolidayApi.Strategies;
using HolidayApi.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace HolidayApi.Controllers
{
    [Route("/feriados/{ibgeCode:int}")]
    [ApiController]
    public class HolidayController : ControllerBase
    {
        private readonly HolidayStrategyContext _holidayStrategyContext;

        public HolidayController(HolidayStrategyContext holidayStrategyContext)
        {
            _holidayStrategyContext = holidayStrategyContext;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<HolidayDto>>> FindAllHolidays(int ibgeCode)
        {
            IbgeCode validIbgeCode = new IbgeCode(ibgeCode);

            var context = _holidayStrategyContext.SetStrategy(validIbgeCode);

            if (context is null)
            {
                throw new Exception();
            }

            var hollidaysList = await context.FindAllHolidays(validIbgeCode.Id);

            return Ok(hollidaysList);
        }


        [HttpPut("{date}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<int>> RegisterHoliday(
            [FromQuery] int ibgeCode, [FromQuery] string date, [FromBody] string name)
        {
            IbgeCode validIbgeCode = new IbgeCode(ibgeCode);
            HolidayDate validDate = new HolidayDate(date);

            var context = _holidayStrategyContext.SetStrategy(validIbgeCode);

            if (context is null)
            {
                throw new Exception();
            }

            return await context.RegisterHoliday(validIbgeCode.Id, validDate, name);

            // https://www.linkedin.com/pulse/strategy-pattern-dependency-injection-clean-code-alkiviadis-skoutaris-s0oyf/

            // https://dev.to/davidkroell/strategy-design-pattern-with-dependency-injection-7ba
        }
    }

}