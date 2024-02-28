using System.Reflection.Metadata;
using Inventory.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
namespace Inventory.Persistence
{
    public class AuthContext:DbContext
    {
        public AuthContext(DbContextOptions<AuthContext> options)
        :base(options)
        {
            
        }        
        public DbSet<User> Users{get; set;}
    }
}