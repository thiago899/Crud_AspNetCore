using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ProjetoWEBCore.Model;

namespace ProjetoWEBCore.Model
{
    public partial class dbCoreContext : DbContext
    {
        public virtual DbSet<Cliente> clientes { get; set; }
        public virtual DbSet<Profissao> profissao { get; set; }
        public dbCoreContext(DbContextOptions<dbCoreContext> options) :
             base(options)
        {
        }

        public dbCoreContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"server=DESKTOP-EOAELKP; Database=dbcliente;Integrated Security=SSPI;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.id).HasName("PK_Cliente");
                entity.Property(e => e.nome).HasColumnType("varchar(30)");
                entity.Property(e => e.sobrenome).HasColumnType("varchar(100)");
                entity.Property(e => e.cpf).HasColumnType("varchar(11)");
                entity.Property(e => e.data_nascimento).HasColumnType("date");
                entity.Property(e => e.idade).HasColumnType("int");
                entity.Property(e => e.profissao_id).HasColumnType("int");
            });

            modelBuilder.Entity<Profissao>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_CARGO");
                entity.Property(e => e.cargo).HasColumnType("varchar(30)");
            });
        }
        public DbSet<ModelCliente> modelClienteCargo { get; set; }
    }
}
