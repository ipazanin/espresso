namespace Espresso.Common.Tests.TestUtilities.Models
{
    /// <summary>
    /// ExampleClass Model
    /// </summary>
    public record ExampleClassWithPublicSetters
    {
        #region Properties
        public string? FirstProperty { get; set; }
        public int SecondProperty { get; set; }
        public bool ThirdProperty { get; set; }
        public ExampleSubClassWithPublicSetters? FourthProperty { get; set; }
        #endregion Properties

        #region Constructors
        /// <summary>
        /// ExampleClass Constructor
        /// </summary>
        public ExampleClassWithPublicSetters()
        {
        }
        #endregion Constructors
    }
}