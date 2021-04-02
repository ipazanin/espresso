namespace Espresso.Dashboard.Shared.Pagination
{
    public class PaginationLink
    {
        #region Properties
        public string Text { get; }
        public int Page { get; }
        public bool Enabled { get; }
        public bool Active { get; set; }

        #endregion Properties

        #region Constructors

        public PaginationLink(int page, bool enabled, string text, bool active)
        {
            Page = page;
            Enabled = enabled;
            Text = text;
            Active = active;
        }

        #endregion Constructors
    }
}