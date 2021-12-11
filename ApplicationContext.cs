using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testWpf
{
    public class ApplicationContext : DbContext
    {
        public DbSet<InformationNode> Nodes { get; set; }
        //public DbSet<NodeLink> Links { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=IntiligentSystemWPF;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InformationNode>(element =>
            {
                element.HasKey(x => x.Id);
                element.Property(x => x.Text);
                element.HasOne(x => x.UpperInforamtionNodes)
                .WithMany(x => x.LowerInformationNodes)
                .HasForeignKey(x => x.UpperInforamtionNodesId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.ClientCascade);

            });
        }
    }
}
