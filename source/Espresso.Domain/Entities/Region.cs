using System.Collections.Generic;
using Espresso.Domain.Infrastructure;

namespace Espresso.Domain.Entities
{
    public class Region : IEntity<int, Region>
    {
        #region Properties
        public int Id { get; private set; }

        public string Name { get; private set; }

        public IEnumerable<NewsPortal> NewsPortals { get; private set; } = new List<NewsPortal>();
        #endregion

        #region Constructors
        public Region(int id, string name)
        {
            Id = id;
            Name = name;
        }
        #endregion
    }
}

