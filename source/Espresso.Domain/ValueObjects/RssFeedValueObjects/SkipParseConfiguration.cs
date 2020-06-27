using System.Collections.Generic;

using Espresso.Domain.Infrastructure;

namespace Espresso.Domain.ValueObjects.RssFeedValueObjects
{
    public class SkipParseConfiguration : ValueObject
    {
        #region Properties
        public int? NumberOfSkips { get; private set; }

        public int? CurrentSkip { get; private set; }
        #endregion

        #region Constructors
        /// <summary>
        /// ORM COnstructor
        /// </summary>
        private SkipParseConfiguration() { }
        public SkipParseConfiguration(int? numberOfSkips, int? currentSkip)
        {
            NumberOfSkips = numberOfSkips;
            CurrentSkip = currentSkip;
        }
        #endregion

        #region Methods
        public bool ShouldParse()
        {
            if (NumberOfSkips is null && CurrentSkip is null)
            {
                return true;
            }

            NumberOfSkips = NumberOfSkips;
            CurrentSkip = CurrentSkip == NumberOfSkips ? 0 : CurrentSkip + 1;

            return CurrentSkip == 0;
        }

        protected override IEnumerable<object?> GetAtomicValues()
        {
            yield return NumberOfSkips;
            yield return CurrentSkip;
        }
        #endregion
    }
}
