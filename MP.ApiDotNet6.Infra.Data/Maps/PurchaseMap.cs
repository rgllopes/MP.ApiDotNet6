﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MP.ApiDotNet6.Domain.Entities;

namespace MP.ApiDotNet6.Infra.Data.Maps
{
    internal class PurchaseMap : IEntityTypeConfiguration<Purchase>
    {
        public void Configure(EntityTypeBuilder<Purchase> builder)
        {
            builder.ToTable("Compra");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("IdCompra")
                .UseIdentityColumn();

            builder.Property(x => x.PersonId)
                .HasColumnName("IdPessoa");

            builder.Property(x => x.ProductId)
                .HasColumnName("IdProduto");

            builder.Property(x => x.Date)
                .HasColumnName("DataCompra");

            //Relacionamento de um para muitos de acordo com a relação da base de dados
            builder.HasOne(x => x.Person)
                .WithMany(x => x.Purchases);

            builder.HasOne(x => x.Product)
                .WithMany(x => x.Purchases);
        }
    }
}
