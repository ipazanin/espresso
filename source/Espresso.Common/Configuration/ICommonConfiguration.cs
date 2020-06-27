using System.Collections.Generic;
using Espresso.Common.Enums;

namespace Espresso.Common.Configuration
{
    public interface ICommonConfiguration
    {
        public AppEnvironment AppEnvironment { get; }

        public string Version { get; }

        public IEnumerable<string> ApiKeys { get; }
    }
}
