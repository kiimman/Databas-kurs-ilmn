using KursDbInlm.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace KursDbInlm.Contexts;

internal class DataContext : DbContext
{

    public DataContext()
    {
    }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Kimman\source\repos\KursDbInlm\KursDbInlm\Contexts\localdb.mdf;Integrated Security=True;Connect Timeout=30");
    }

    public DbSet<StatusEntity> Statuses { get; set; }

    public DbSet<CaseEntity> Cases { get; set; }

    public DbSet<CommentEntity> Comments { get; set; }
}
