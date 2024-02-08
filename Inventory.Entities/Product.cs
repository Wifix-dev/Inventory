namespace Inventory.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string BarCode { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        //Relacion de uno a uno
        public int CategoryId {get; set; }
        public Category? Category { get; set; }
        //Relacion de uno a uno
        public int SupplierId { get; set; }
        public Supplier? Supplier { get; set;}
        public IEnumerable<InventoryMovement>? InventoryMovements {get; set;}
    }
}