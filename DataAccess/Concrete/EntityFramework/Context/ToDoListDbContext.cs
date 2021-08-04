using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DataAccess.Concrete.EntityFramework.Context
{
    public class ToDoListDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=EM;DataBase=ToDoListDb;Trusted_Connection=true;");
        }
        public DbSet<User> Users { get; set; }
        public DbSet<ToDoList> ToDoLists { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }

    }
}