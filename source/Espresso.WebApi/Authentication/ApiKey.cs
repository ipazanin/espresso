using System;

namespace Espresso.WebApi.Authentication
{
    /// <summary>
    /// 
    /// </summary>
    public class ApiKey
    {
        #region Constants
        /// <summary>
        /// 
        /// </summary>
        public const string MobileAppRole = "Mobile";

        /// <summary>
        /// 
        /// </summary>
        public const string WebAppRole = "Web";

        /// <summary>
        /// 
        /// </summary>
        public const string ParserRole = "Parser";
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// 
        /// </summary>
        public string Role { get; }

        /// <summary>
        /// 
        /// </summary>
        public string Key { get; }
        #endregion

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="role"></param>
        /// <param name="key"></param>
        public ApiKey(
            int id,
            string role,
            string key
        )
        {
            Id = id;
            Role = role ?? throw new ArgumentNullException(nameof(role));
            Key = key ?? throw new ArgumentNullException(nameof(key));
        }
        #endregion
    }
}
