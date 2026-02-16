using HolidayApi.Data.DTOs;
using HolidayApi.ResponseHandler;
using HolidayApi.Strategies;
using HolidayApi.Data.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using HolidayApi.Data.Models;

namespace HolidayApi.Controllers
{
    [Route("/feriados/{ibgeCode:int}")]
    [ApiController]
    public class HolidayController : ControllerBase
    {
        [HttpGet("{date}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> FindHolidayByIbgeCodeAndDate()
        {
            var requestContext = HttpContext.Items[typeof(RequestContext)] as RequestContext;

            var result = await requestContext!.Strategy.FindHolidayByIbgeCodeAndDate(requestContext.IbgeCode.Id, requestContext.Date);

            return result.IsSuccess ?
                Ok(result.Value)
                : StatusCode(result.Error!.Code, new { Message = result.Error.Description });
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<HolidayDetailDto>>> FindAllHolidaysByIbgeCode()
        {
            var requestContext = HttpContext.Items[typeof(RequestContext)] as RequestContext;

            var result = await requestContext!.Strategy.FindAllHolidaysByIbgeCode(requestContext.IbgeCode.Id);

            return result.IsSuccess ? Ok(result.Value) : StatusCode(result.Error!.Code, new { Message = result.Error.Description });
        }


        [HttpPut("{date}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> RegisterHolidayByIbgeCode([FromBody] string name)
        {
            var requestContext = HttpContext.Items[typeof(RequestContext)] as RequestContext;

            var result = await requestContext!.Strategy.RegisterHolidayByIbgeCode(requestContext.IbgeCode.Id, requestContext.Date, name);

            if (result.IsFailure)
            {
                return StatusCode(result.Error!.Code, new { Message = result.Error.Description });
            }

            return result.Value == (int)OperationTypeCode.Create ? Created() : Ok();
        }

        [HttpDelete("{date}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteHolidayAsync()
        {
            var requestContext = HttpContext.Items[typeof(RequestContext)] as RequestContext;

            var result = await requestContext!.Strategy.DeleteHolidayAsync(requestContext.IbgeCode.Id, requestContext.Date);

            return result.IsSuccess ? NoContent() : StatusCode(result.Error!.Code, new { Message = result.Error.Description });
        }
    }
}