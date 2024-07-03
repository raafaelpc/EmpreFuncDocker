﻿using EmpresaFuncionario.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmpresaFuncionario.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Empresa>()
                .HasMany(e => e.Funcionarios)
                .WithOne(f => f.Empresa)
                .HasForeignKey(f => f.EmpresaId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
