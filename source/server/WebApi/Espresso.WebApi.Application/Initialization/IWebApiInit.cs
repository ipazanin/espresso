// IWebApiInit.cs
//
// © 2021 Espresso News. All rights reserved.

using System.Threading.Tasks;

namespace Espresso.WebApi.Application.Initialization
{
    public interface IWebApiInit
    {
        /// <summary>
        ///
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        public Task InitWebApi();
    }
}
