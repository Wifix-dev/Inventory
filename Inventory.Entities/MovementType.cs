namespace Inventory.Entities
{
    public class MovementType
    {
        public int Id { get; set; }
        public string Name { get; set; }  = string.Empty;
        public string Description { get; set; }  = string.Empty;
        public bool IsComing { get; set; }
        public bool IsOutgoing { get; set; }
        public bool IsInternalTransfer { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public IEnumerable<InventoryMovement>? InventoryMovements{ get; set; }       
    }
}