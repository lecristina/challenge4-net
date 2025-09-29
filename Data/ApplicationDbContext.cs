using Microsoft.EntityFrameworkCore;
using challenge_3_net.Models;
using Oracle.EntityFrameworkCore;

namespace challenge_3_net.Data
{
    /// <summary>
    /// Contexto do Entity Framework para a aplicação
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Tabela de usuários
        /// </summary>
        public DbSet<Usuario> Usuarios { get; set; } = null!;

        /// <summary>
        /// Tabela de motos
        /// </summary>
        public DbSet<Moto> Motos { get; set; } = null!;

        /// <summary>
        /// Tabela de operações
        /// </summary>
        public DbSet<Operacao> Operacoes { get; set; } = null!;

        /// <summary>
        /// Tabela de status das motos
        /// </summary>
        public DbSet<StatusMoto> StatusMotos { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar Oracle para usar aspas duplas (case-sensitive)
            modelBuilder.HasDefaultSchema(null);

            // Configuração da entidade Usuario para Oracle (conforme script)
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("usuarios");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.NomeFilial).IsRequired().HasMaxLength(100).HasColumnName("nome_filial");
                entity.Property(e => e.Email).IsRequired().HasMaxLength(255).HasColumnName("email");
                entity.Property(e => e.SenhaHash).IsRequired().HasMaxLength(255).HasColumnName("senha_hash");
                entity.Property(e => e.Cnpj).IsRequired().HasMaxLength(18).HasColumnName("cnpj");
                entity.Property(e => e.Endereco).HasMaxLength(255).HasColumnName("endereco");
                entity.Property(e => e.Telefone).HasMaxLength(20).HasColumnName("telefone");
                entity.Property(e => e.Perfil).IsRequired().HasConversion<string>().HasColumnName("perfil");
                entity.Property(e => e.DataCriacao).IsRequired().HasColumnName("data_criacao");
                entity.Ignore(e => e.DataAtualizacao); // Oracle só tem data_criacao

                // Índices únicos
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.Cnpj).IsUnique();
            });

            // Configuração da entidade Moto para Oracle (conforme script)
            modelBuilder.Entity<Moto>(entity =>
            {
                entity.ToTable("motos");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Placa).IsRequired().HasMaxLength(10).HasColumnName("placa");
                entity.Property(e => e.Chassi).IsRequired().HasMaxLength(50).HasColumnName("chassi");
                entity.Property(e => e.Motor).HasMaxLength(50).HasColumnName("motor");
                entity.Property(e => e.UsuarioId).IsRequired().HasColumnName("usuario_id");
                entity.Property(e => e.DataCriacao).IsRequired().HasColumnName("data_criacao");
                entity.Ignore(e => e.DataAtualizacao); // Oracle só tem data_criacao

                // Índices únicos
                entity.HasIndex(e => e.Placa).IsUnique();
                entity.HasIndex(e => e.Chassi).IsUnique();

                // Relacionamento com Usuario
                entity.HasOne(e => e.Usuario)
                      .WithMany(u => u.Motos)
                      .HasForeignKey(e => e.UsuarioId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuração da entidade Operacao para Oracle (conforme script)
            modelBuilder.Entity<Operacao>(entity =>
            {
                entity.ToTable("operacoes");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.MotoId).IsRequired().HasColumnName("moto_id");
                entity.Property(e => e.UsuarioId).IsRequired().HasColumnName("usuario_id");
                entity.Property(e => e.TipoOperacao).IsRequired().HasConversion<string>().HasColumnName("tipo_operacao");
                entity.Property(e => e.Descricao).HasColumnName("observacoes"); // Oracle usa observacoes
                entity.Property(e => e.DataOperacao).IsRequired().HasColumnName("data_criacao"); // Oracle usa data_criacao

                // Relacionamentos
                entity.HasOne(e => e.Moto)
                      .WithMany(m => m.Operacoes)
                      .HasForeignKey(e => e.MotoId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Usuario)
                      .WithMany(u => u.Operacoes)
                      .HasForeignKey(e => e.UsuarioId)
                      .OnDelete(DeleteBehavior.NoAction);
            });

            // Configuração da entidade StatusMoto para Oracle (conforme script)
            modelBuilder.Entity<StatusMoto>(entity =>
            {
                entity.ToTable("status_motos");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.MotoId).IsRequired().HasColumnName("moto_id");
                entity.Property(e => e.UsuarioId).IsRequired().HasColumnName("usuario_id");
                entity.Property(e => e.Status).IsRequired().HasConversion<string>().HasColumnName("status");
                entity.Property(e => e.Descricao).HasMaxLength(500).HasColumnName("descricao");
                entity.Property(e => e.Area).IsRequired().HasMaxLength(50).HasColumnName("area");
                entity.Property(e => e.DataStatus).IsRequired().HasColumnName("data_criacao"); // Oracle usa data_criacao

                // Relacionamentos
                entity.HasOne(e => e.Moto)
                      .WithMany(m => m.StatusMotos)
                      .HasForeignKey(e => e.MotoId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Usuario)
                      .WithMany()
                      .HasForeignKey(e => e.UsuarioId)
                      .OnDelete(DeleteBehavior.NoAction);
            });

            // Seed data - desabilitado temporariamente
            // SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            var dataCriacao = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            
            // Usuários iniciais
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    Id = 1,
                    NomeFilial = "Admin FIAP",
                    Email = "admin@fiap.com",
                    SenhaHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                    Cnpj = "12.345.678/0001-90",
                    Endereco = "Rua Admin, 123",
                    Telefone = "(11) 99999-9999",
                    Perfil = PerfilUsuario.ADMIN,
                    DataCriacao = dataCriacao,
                    DataAtualizacao = dataCriacao
                },
                new Usuario
                {
                    Id = 2,
                    NomeFilial = "Gerente FIAP",
                    Email = "gerente@fiap.com",
                    SenhaHash = BCrypt.Net.BCrypt.HashPassword("gerente123"),
                    Cnpj = "12.345.678/0002-90",
                    Endereco = "Rua Gerente, 456",
                    Telefone = "(11) 88888-8888",
                    Perfil = PerfilUsuario.GERENTE,
                    DataCriacao = dataCriacao,
                    DataAtualizacao = dataCriacao
                },
                new Usuario
                {
                    Id = 3,
                    NomeFilial = "Operador FIAP",
                    Email = "operador@fiap.com",
                    SenhaHash = BCrypt.Net.BCrypt.HashPassword("operador123"),
                    Cnpj = "12.345.678/0003-90",
                    Endereco = "Rua Operador, 789",
                    Telefone = "(11) 77777-7777",
                    Perfil = PerfilUsuario.OPERADOR,
                    DataCriacao = dataCriacao,
                    DataAtualizacao = dataCriacao
                }
            );
        }
    }
}
