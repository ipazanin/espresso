using Espresso.WebApi.Configuration;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Espresso.WebApi.Infrastructure
{
    /// <summary>
    /// Api controlles base
    /// </summary>
    [Authorize]
    [ApiController]
    public abstract class ApiController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        protected readonly IMediator Mediator;

        /// <summary>
        /// 
        /// </summary>
        protected readonly IWebApiConfiguration WebApiConfiguration;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="webApiConfiguration"></param>
        public ApiController(IMediator mediator, IWebApiConfiguration webApiConfiguration)
        {
            Mediator = mediator;
            WebApiConfiguration = webApiConfiguration;
        }
    }
}
