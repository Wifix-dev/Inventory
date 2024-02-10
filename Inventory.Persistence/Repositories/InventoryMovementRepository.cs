using  Inventory.Entities;
using Inventory.Persistence.Interfaces; 

namespace Inventory.Persistence.Repositories
{
    public class InventoryMovementRepository: BaseRepository<InventoryMovement> , IInventoryMovementRepository
    {
        public InventoryMovementRepository(DataContext context)
        :base(context)
        {
            
        }
    }
}