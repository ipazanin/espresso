using Microsoft.AspNetCore.Components;

namespace Espresso.Dashboard.Shared.NavigationMenu
{
    /// <summary>
    /// NavigationMenuBase
    /// </summary>
    public class NavigationMenuBase : ComponentBase
    {
        #region Fields
        private bool _collapseNavMenu = true;
        #endregion Fields

        #region Properties
        protected string? NavMenuCssClass => _collapseNavMenu ? "collapse" : null;
        #endregion Properties

        #region Methods
        protected void ToggleNavMenu()
        {
            _collapseNavMenu = !_collapseNavMenu;
        }
        #endregion Methods
    }
}
