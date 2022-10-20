
using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalog.Domain
{
    public class Dimensions
    {
        public decimal Height { get; private set; }
        public decimal Width { get; private set; }
        public decimal Depth { get; private set; }

        public Dimensions(decimal height, decimal width, decimal depth)
        {
            AssertionConcern.AssertArgumentLessOrEqualsMinimum(height, 1, "The Height field should can't be less than or equal to 0");
            AssertionConcern.AssertArgumentLessOrEqualsMinimum(width, 1, "The Width field should can't be less than or equal to 0");
            AssertionConcern.AssertArgumentLessOrEqualsMinimum(depth, 1, "The Depth field should can't be less than or equal to 0");

            Height = height;
            Width = width;
            Depth = depth;
        }

        public string FormatedDescription()
        {
            return $"WxHxD: {Width} x {Height} x {Depth}";
        }

        public override string ToString()
        {
            return FormatedDescription();
        }
    }
}
