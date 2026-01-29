using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace MeetingSystem.API.Extensions
{
    public static class ResultExtension
    {
        public static IActionResult ToActionResult(this Result result)
        {
            if (result.IsSuccess)
            {
                return new OkResult();
            }

            return new BadRequestObjectResult(result.Error);
        }

        public static IActionResult ToActionResult<T>(this Result<T> result)
        {
            if (result.IsSuccess)
            {
                var okResult = new OkObjectResult(result.Data);
                okResult.DeclaredType = typeof(T);
                return okResult;
            }

            return new BadRequestObjectResult(result.Error);
        }
    }
}
