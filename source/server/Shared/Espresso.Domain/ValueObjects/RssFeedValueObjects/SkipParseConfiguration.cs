// SkipParseConfiguration.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Domain.Infrastructure;
using System.Collections.Generic;

namespace Espresso.Domain.ValueObjects.RssFeedValueObjects
{
    public class SkipParseConfiguration : ValueObject
    {
        public int? NumberOfSkips { get; private set; }

        public int? CurrentSkip { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SkipParseConfiguration"/> class.
        /// ORM COnstructor.
        /// </summary>
#pragma warning disable SA1502 // Element should not be on a single line
#pragma warning disable SA1201 // Elements should appear in the correct order
        private SkipParseConfiguration() { }
#pragma warning restore SA1201 // Elements should appear in the correct order
#pragma warning restore SA1502 // Element should not be on a single line

        /// <summary>
        /// Initializes a new instance of the <see cref="SkipParseConfiguration"/> class.
        /// </summary>
        /// <param name="numberOfSkips"></param>
        /// <param name="currentSkip"></param>
        public SkipParseConfiguration(int? numberOfSkips, int? currentSkip)
        {
            NumberOfSkips = numberOfSkips;
            CurrentSkip = currentSkip;
        }

        public bool ShouldParse()
        {
            if (NumberOfSkips is null && CurrentSkip is null)
            {
                return true;
            }

            CurrentSkip = CurrentSkip == NumberOfSkips ? 0 : CurrentSkip + 1;

            return CurrentSkip == 1;
        }

        protected override IEnumerable<object?> GetAtomicValues()
        {
            yield return NumberOfSkips;
            yield return CurrentSkip;
        }
    }
}
