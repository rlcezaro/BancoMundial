using Microsoft.EntityFrameworkCore;
using BancoMundial.Models;

namespace BancoMundial.Data
{
    public class BancoMundialContext : DbContext
    {
        public BancoMundialContext(DbContextOptions<BancoMundialContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<PessoaFisica> PessoasFisicas { get; set; }
        public DbSet<PessoaJuridica> PessoasJuridicas { get; set; }
        public DbSet<ContaBancaria> Contas { get; set; }
        public DbSet<ContaSalario> ContasSalario { get; set; }
        public DbSet<ContaPoupanca> ContasPoupanca { get; set; }
        public DbSet<ContaCorrente> ContasCorrente { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>()
                .HasDiscriminator<string>("ClienteType")
                .HasValue<PessoaFisica>("PessoaFisica")
                .HasValue<PessoaJuridica>("PessoaJuridica");

            modelBuilder.Entity<ContaBancaria>()
                .HasDiscriminator<string>("ContaType")
                .HasValue<ContaSalario>("ContaSalario")
                .HasValue<ContaPoupanca>("ContaPoupanca")
                .HasValue<ContaCorrente>("ContaCorrente");

            modelBuilder.Entity<ContaBancaria>()
                .Property(c => c.Saldo)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<ContaBancaria>()
                .Property(c => c.TaxaSaque)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<ContaCorrente>()
                .Property(c => c.Limite)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<ContaCorrente>()
                .Property(c => c.TaxaJuros)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<PessoaFisica>()
                .Property(p => p.Renda)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<PessoaJuridica>()
                .Property(p => p.Faturamento)
                .HasColumnType("decimal(18,2)");

            base.OnModelCreating(modelBuilder);
        }
    }
}
