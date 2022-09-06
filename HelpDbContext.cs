using HelpDesk.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HelpDesk
{
    public partial class HelpDbContext : DbContext
    {
        public HelpDbContext()
        {
        }

        public HelpDbContext(DbContextOptions<HelpDbContext> options)
            : base(options)
        {
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<BranchTicket> BranchTickets { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<PermissionUser> PermissionUsers { get; set; }
        public DbSet<FeedbackTicket> FeedbackTickets { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<DepartmentTicket> DepartmentTickets { get; set; }
        public DbSet<StatusType> StatusTypes { get; set; }
        public DbSet<PermissionType> PermissionTypes { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketType> TicketTypes { get; set; }
        public DbSet<UserTicket> UserTickets { get; set; }
        public DbSet<UsersBranch> UsersBranch { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string ConnectionString = @"Data Source=ALI-IT\SQLEXPRESS;Initial Catalog=HelpDesk1;Integrated Security=True";

            optionsBuilder.UseSqlServer(ConnectionString);
        }
    }
}

