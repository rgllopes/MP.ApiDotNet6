using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MP.ApiDotNet6.Domain.Entities;

namespace MP.ApiDotNet6.Infra.Data.Maps
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Produto");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("IdProduto")
                .UseIdentityColumn();

            builder.Property(x => x.CodErp)
                .HasColumnName("CodErp");

            builder.Property(x => x.Name)
                .HasColumnName("Nome");

            builder.Property(x => x.Price)
                .HasColumnName("Preco");

            //Relacionamento de um para muitos de acordo com a relação da base de dados
            builder.HasMany(x => x.Purchases)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId);
        }
    }
}
