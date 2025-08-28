using HolidayApi.Data.DTOs;
using HolidayApi.ResponseHandler;
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
        public async Task<IActionResult> FindHolidayByIbgeCodeAndDate(int ibgeCode, string date)
        {
            IbgeCode validIbgeCode = new IbgeCode(ibgeCode);
            HolidayDate validDate = new HolidayDate(date);

            var context = _holidayStrategyContext.SetStrategy(validIbgeCode);

            if (context is null)
            {
                throw new Exception("Failed to recover the strategy context | Holiday Controller");
            }

            var result = await context.FindHolidayByIbgeCodeAndDate(validIbgeCode.Id, validDate);

            return result.IsSuccess ?
                Ok(result.Value)
                : StatusCode(result.Error!.Code, new { Message = result.Error.Description });
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

            var result = await context.FindAllHolidaysByIbgeCode(validIbgeCode.Id);

            return result.IsSuccess ? Ok(result.Value) : StatusCode(result.Error!.Code, new { Message = result.Error.Description });
        }


        [HttpPut("{date}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> RegisterHolidayByIbgeCode(int ibgeCode, string date, [FromBody] string name)
        {
            IbgeCode validIbgeCode = new IbgeCode(ibgeCode);
            HolidayDate validDate = new HolidayDate(date);

            var context = _holidayStrategyContext.SetStrategy(validIbgeCode);

            if (context is null)
            {
                throw new Exception("Failed to recover the strategy context | Holiday Controller");
            }

            var result = await context.RegisterHolidayByIbgeCode(validIbgeCode.Id, validDate, name);

            if (result.IsFailure)
            {
                return StatusCode(result.Error!.Code, new { Message = result.Error.Description });
            }

            return result.Value == (int)OperationTypeCode.Create ? Created() : Ok();
        }

        [HttpDelete("{date}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteHolidayAsync(int ibgeCode, string date)
        {
            IbgeCode validIbgeCode = new IbgeCode(ibgeCode);
            HolidayDate validDate = new HolidayDate(date);

            var context = _holidayStrategyContext.SetStrategy(validIbgeCode);

            if (context is null)
            {
                throw new Exception("Failed to recover the strategy context | Holiday Controller");
            }

            var result = await context.DeleteHolidayAsync(validIbgeCode.Id, validDate);

            return result.IsSuccess ? NoContent() : StatusCode(result.Error!.Code, new { Message = result.Error.Description });
        }
    }
}