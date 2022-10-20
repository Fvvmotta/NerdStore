using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalog.Domain
{
    public class Product : Entity, IAggregateRoot
    {
        public Guid CategoryId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool Active { get; private set; }
        public decimal Value { get; private set; }
        public DateTime RegisterDate { get; private set; }
        public string Image { get; private set; }
        public int StockQuantity { get; private set; }
        public Dimensions Dimensions { get; private set; }
        public Category Category { get; private set; }

        public Product(string name, string description, bool active, decimal value, Guid categoryId, DateTime registerDate, string image, Dimensions dimensions)
        {
            CategoryId = categoryId;
            Name = name;
            Description = description;
            Active = active;
            Value = value;
            RegisterDate = registerDate;
            Image = image;
            Dimensions = dimensions;

            Validate();
            
        }

        public void Activate() => Active = true;
        public void Deactivate() => Active = false;

        public void ChangeCategory(Category category)
        {
            Category = category;
            CategoryId = category.Id;
        }

        public void ChangeDescription(string description)
        {
            AssertionConcern.AssertArgumentNotEmpty(description, "The Product Description field can not be empty.");
            Description = description;
        }

        public void DebitInventory(int quantity)
        {
            if (quantity < 0) quantity *= -1;
            if (!HasStock(quantity)) throw new DomainException("Insuficient stock");
            StockQuantity -= quantity;
        }

        public void ReplenishStock(int quantity)
        {
            StockQuantity += quantity;
        }
        public bool HasStock(int quantity)
        {
            return StockQuantity >= quantity;
        }
        public void Validate()
        {
            AssertionConcern.AssertArgumentNotEmpty(Name, "The Product Name field can not be empty");
            AssertionConcern.AssertArgumentNotEmpty(Description, "The Product Description field can not be empty");
            AssertionConcern.AssertArgumentEquals(CategoryId, Guid.Empty, "The Product Category Id field can not be empty");
            AssertionConcern.AssertArgumentLessOrEqualsMinimum(Value, 0, "The Product Value field can not be empty");
            AssertionConcern.AssertArgumentNotEmpty(Image, "The Product Image field can not be empty");
        }
    }
}
