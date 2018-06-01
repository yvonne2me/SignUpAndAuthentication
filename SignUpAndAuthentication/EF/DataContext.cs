using SignUpAndAuthentication.Models;
using SignUpAndAuthentication.Types;
using Microsoft.EntityFrameworkCore;

namespace SignUpAndAuthentication.EF
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) 
            : base(options){}  

        public DbSet<User> Users { get; set; }
        public DbSet<AuthorizationResponse> SignUpResponses { get; set; }
            
    }
}
