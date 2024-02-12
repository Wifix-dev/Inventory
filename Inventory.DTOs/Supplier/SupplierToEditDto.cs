using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.DTOs.Supplier
{
    public class SupplierToEditDto
    {
        public int Id { get; set; }
        public string Name { get; set; }= string.Empty;
        public string ContactPerson { get; set; }= string.Empty;
        public string Phone { get; set; }= string.Empty;
        public string Email { get; set; }= string.Empty;
        public string Address { get; set; }= string.Empty;
        
    }
}