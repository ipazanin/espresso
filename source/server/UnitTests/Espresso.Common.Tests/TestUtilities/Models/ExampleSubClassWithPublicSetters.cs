namespace Espresso.Common.Tests.TestUtilities.Models
{
    /// <summary>
    /// ExampleClass Model
    /// </summary>
    public record ExampleSubClassWithPublicSetters
    {
        #region Properties
        public int FourthPropertyFirstProperty { get; set; }
        #endregion Properties

        #region Constructors
        /// <summary>
        /// ExampleClass Constructor
        /// </summary>
        public ExampleSubClassWithPublicSetters()
        {
        }
        #endregion Constructors
    }
}