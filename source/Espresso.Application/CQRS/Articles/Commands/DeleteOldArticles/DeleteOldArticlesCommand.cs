using System;
using Espresso.Application.Infrastructure;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using Espresso.Common.Enums;

namespace Espresso.Application.CQRS.Articles.Commands.DeleteOldArticles
{
    public class DeleteOldArticlesCommand : Request<DeleteOldArticlesCommandResponse>
    {
        #region Properties
        public TimeSpan MaxAgeOfOldArticles { get; }
        #endregion

        #region Constructors
        public DeleteOldArticlesCommand(
            TimeSpan maxAgeOfOldArticles,
            string currentEspressoWebApiVersion,
            string espressoWebApiVersion,
            string version,
            DeviceType deviceType
        ) : base(
            currentEspressoWebApiVersion,
            espressoWebApiVersion,
            version,
            deviceType,
            Event.CalculateTrendingScoreCommand
        )
        {
            MaxAgeOfOldArticles = maxAgeOfOldArticles;
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"{nameof(MaxAgeOfOldArticles)}:{MaxAgeOfOldArticles}";
        }
        #endregion
    }
}
