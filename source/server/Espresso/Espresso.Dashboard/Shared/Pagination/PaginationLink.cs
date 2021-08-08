// PaginationLink.cs
//
// © 2021 Espresso News. All rights reserved.

namespace Espresso.Dashboard.Shared.Pagination
{
    public class PaginationLink
    {
        public string Text { get; }

        public int Page { get; }

        public bool Enabled { get; }

        public bool Active { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaginationLink"/> class.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="enabled"></param>
        /// <param name="text"></param>
        /// <param name="active"></param>
        public PaginationLink(int page, bool enabled, string text, bool active)
        {
            Page = page;
            Enabled = enabled;
            Text = text;
            Active = active;
        }
    }
}
