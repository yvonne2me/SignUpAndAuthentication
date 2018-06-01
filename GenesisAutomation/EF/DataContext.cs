using GenesisAutomation.Models;
using GenesisAutomation.Types;
using Microsoft.EntityFrameworkCore;

namespace GenesisAutomation.EF
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) 
            : base(options){}  

        public DbSet<User> Users { get; set; }
        public DbSet<AuthorizationResponse> SignUpResponses { get; set; }
            
    }
}
