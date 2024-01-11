using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Models;
using Models.Entidades;

namespace AcessoDados.Data
{
    public class SystemAppContext : DbContext
    {
        public SystemAppContext(DbContextOptions<SystemAppContext> options)
           : base(options)
        {
          // Database.EnsureCreated();
        }
        //as Tabelas a Ser passada para o banco
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Funcao> Funcoes { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Venda> Vendas { get; set; }
        public DbSet<ItemVenda> ItemsVendas { get; set; }

        public DbSet<EStudenteModel> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //criandp chave composta
            modelBuilder.Entity<Cliente>().OwnsMany(c => c.Enderecos, e =>
            {
                e.WithOwner().HasForeignKey("Id");
                e.HasKey("Id", ("IdEndereco"));
            });

            //Valores Default nas Tabelas
            modelBuilder.Entity<Categoria>().Property(c => c.DataRegisto).HasDefaultValueSql("getdate()")
            .Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);
            modelBuilder.Entity<Categoria>().Property(c => c.DataUltimaAlteracao).HasDefaultValueSql("getdate()")
            .Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);
            modelBuilder.Entity<Producto>().Property(c => c.DataRegisto).HasDefaultValueSql("getdate()")
            .Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);           
            modelBuilder.Entity<Funcionario>().Property(u => u.DataRegisto).HasDefaultValueSql("getdate()")
            .Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);
            modelBuilder.Entity<Funcionario>().Property(u => u.Senha).HasDefaultValue("1234")
            .Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);
            modelBuilder.Entity<Cliente>().Property(u => u.DataRegisto).HasDefaultValueSql("getdate()")
            .Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);
        

            modelBuilder.Entity<Categoria>().Property(u => u.Estado).HasDefaultValue("1");
            modelBuilder.Entity<Producto>().Property(u => u.Estado).HasDefaultValue("1");
            modelBuilder.Entity<Producto>().Property(dt => dt.Preco).HasPrecision(18, 2);
            modelBuilder.Entity<Producto>().Property(u => u.Preco).HasDefaultValue(0);
            modelBuilder.Entity<Producto>().Property(u => u.Estoque).HasDefaultValue(0);
            modelBuilder.Entity<Producto>().Property(u => u.Obs).HasDefaultValue("Sem observações");

            modelBuilder.Entity<EStudenteModel>().Property(u => u.Ativo).HasDefaultValue(true);

            modelBuilder.Entity<Cliente>().Property(u => u.Estado).HasDefaultValue("1");
            modelBuilder.Entity<Cliente>().Property(u => u.Email).HasDefaultValue("Sem E-Mail");
            modelBuilder.Entity<Cliente>().Property(u => u.TelefoneNº).HasDefaultValue("Sem Telefone");


            modelBuilder.Entity<Funcionario>().Property(u => u.Estado).HasDefaultValue("1");

            modelBuilder.Entity<Funcao>().Property(u => u.Estado).HasDefaultValue("1");

            //criandp chave composta de ItemVendas -- relacionamento N por N
            modelBuilder.Entity<ItemVenda>().HasKey(ip => new { ip.IdVenda, ip.IdProducto });
            modelBuilder.Entity<ItemVenda>().Property(dt => dt.ValorUnitario).HasPrecision(18, 2);
           

            modelBuilder.Entity<Venda>().Property(dt => dt.ValorTotal).HasPrecision(18, 2);
            modelBuilder.Entity<Venda>().Property(u => u.Estado).HasDefaultValue("1");
            modelBuilder.Entity<Venda>().OwnsOne(v => v.EnderecoEntrega, e =>
               {
                   e.Ignore(e => e.IdEndereco);
                   e.Ignore(e => e.Selecionado);
                   e.ToTable("Venda");
               });          
        }
    }
}
