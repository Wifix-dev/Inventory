using Inventory.Entities;
using Inventory.Persistence.Interfaces;

namespace Inventory.Persistence.Repositories
{
    public class InventoryStockRepository : BaseRepository<InventoryStock>, IInventoryStockRepository
    {
        public InventoryStockRepository(DataContext context) 
        : base(context)
        {
            
        }
    }
}