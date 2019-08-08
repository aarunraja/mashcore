namespace Masha.General.WebApi
{
    using Masha.Foundation;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System.Net;
    using System.Security.Claims;
    using System.Linq;

    /// <summary>
    /// Masha Controller
    /// </summary>
    //[Authorize]
    public abstract class BaseController : ControllerBase
    {
        /// <summary>
        /// _logger
        /// </summary>
        protected readonly ILogger _logger;

        /// <summary>
        /// Masha Controller
        /// </summary>
        /// <param name="logger"></param>
        protected BaseController(ILogger<BaseController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        protected ObjectResult ErrorResponse(Error e)
        {
            var statusCode = ErrorManager.FormatStatusCode(e.Code);
            switch (statusCode)
            {
                case HttpStatusCode.InternalServerError:
                    _logger.LogError(e.Exception, e.Exception.Message);
                    return StatusCode((int)statusCode, "An unexpected error occured, Please try again later.");
                default:
                    return StatusCode((int)statusCode, new ErrorResult(e.Code, e.Message));
            }

        }

    }
}