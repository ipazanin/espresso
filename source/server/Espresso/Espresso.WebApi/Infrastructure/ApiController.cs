using Espresso.WebApi.Configuration;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Espresso.WebApi.Infrastructure
{
    /// <summary>
    /// Api controlles base
    /// </summary>
    [ApiController]
    public abstract class ApiController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        protected readonly ISender Sender;

        /// <summary>
        /// 
        /// </summary>
        protected readonly IWebApiConfiguration WebApiConfiguration;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="webApiConfiguration"></param>
        public ApiController(ISender sender, IWebApiConfiguration webApiConfiguration)
        {
            Sender = sender;
            WebApiConfiguration = webApiConfiguration;
        }
    }
}
