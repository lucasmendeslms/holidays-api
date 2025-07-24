using HolidayApi.Data.DTOs;
using HolidayApi.Data.Entities;
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

        [HttpGet("{date}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<HolidayDto>> FindHolidayByIbgeCodeAndDate(int ibgeCode, string date)
        {
            IbgeCode validIbgeCode = new IbgeCode(ibgeCode);
            HolidayDate validDate = new HolidayDate(date);

            var context = _holidayStrategyContext.SetStrategy(validIbgeCode);

            if (context is null)
            {
                throw new Exception("Failed to recover the strategy context | Holiday Controller");
            }

            var holiday = await context.FindHolidayByIbgeCodeAndDate(validIbgeCode.Id, validDate);

            return holiday is not null ? Ok(holiday) : NotFound();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<HolidayDetailDto>>> FindAllHolidaysByIbgeCode(int ibgeCode)
        {
            IbgeCode validIbgeCode = new IbgeCode(ibgeCode);

            var context = _holidayStrategyContext.SetStrategy(validIbgeCode);

            if (context is null)
            {
                throw new Exception("Failed to recover the strategy context | Holiday Controller");
            }

            var holidaysList = await context.FindAllHolidaysByIbgeCode(validIbgeCode.Id);

            return Ok(holidaysList);
        }


        [HttpPut("{date}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<int>> RegisterHolidayByIbgeCode(int ibgeCode, string date, [FromBody] string name)
        {
            IbgeCode validIbgeCode = new IbgeCode(ibgeCode);
            HolidayDate validDate = new HolidayDate(date);

            var context = _holidayStrategyContext.SetStrategy(validIbgeCode);

            if (context is null)
            {
                throw new Exception("Failed to recover the strategy context | Holiday Controller");
            }

            return await context.RegisterHolidayByIbgeCode(validIbgeCode.Id, validDate, name);

            // https://www.linkedin.com/pulse/strategy-pattern-dependency-injection-clean-code-alkiviadis-skoutaris-s0oyf/

            // https://dev.to/davidkroell/strategy-design-pattern-with-dependency-injection-7ba
        }
    }

}