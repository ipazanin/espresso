using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Espresso.Application.ViewModels.NewsPortalViewModels;
using Espresso.Domain.Entities;

namespace Espresso.Application.ViewModels.RegionViewModels
{
    public class RegionViewModel
    {
        #region Properties
        public int Id { get; private set; }

        public string Name { get; private set; }

        public IEnumerable<NewsPortalViewModel> NewsPortals { get; private set; } = new List<NewsPortalViewModel>();
        #endregion

        #region Constructors
        private RegionViewModel()
        {
            Name = null!;
        }
        #endregion

        #region Methods
        public static Expression<Func<Region, RegionViewModel>> GetProjection()
        {
            return region => new RegionViewModel
            {
                Id = region.Id,
                Name = region.Name,
                NewsPortals = region.NewsPortals.Select(NewsPortalViewModel.GetProjection().Compile())
            };
        }
        #endregion
    }
}
