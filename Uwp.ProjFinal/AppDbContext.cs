using Microsoft.EntityFrameworkCore;
using Uwp.ProjFinal.Models;

namespace Uwp.ProjFinal
{
    public class AppDbContext : DbContext
    {
        public DbSet<AgendaItem> AgendaItens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Agenda.db");
        }
    }
}
