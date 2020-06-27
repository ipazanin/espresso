using System;

namespace Espresso.WebApi.Authentication
{
    /// <summary>
    /// 
    /// </summary>
    public class ApiKey
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="owner"></param>
        /// <param name="key"></param>
        /// <param name="created"></param>
        public ApiKey(int id, string owner, string key, DateTime created)
        {
            Id = id;
            Owner = owner ?? throw new ArgumentNullException(nameof(owner));
            Key = key ?? throw new ArgumentNullException(nameof(key));
            Created = created;
        }

        /// <summary>
        /// 
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// 
        /// </summary>
        public string Owner { get; }

        /// <summary>
        /// 
        /// </summary>
        public string Key { get; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime Created { get; }
    }
}
