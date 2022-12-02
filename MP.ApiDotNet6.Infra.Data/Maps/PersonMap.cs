﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MP.ApiDotNet6.Domain.Entities;

namespace MP.ApiDotNet6.Infra.Data.Maps
{
    public class PersonMap : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("Pessoa");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("IdPessoa")
                .UseIdentityAlwaysColumn();

            builder.Property(x => x.Document)
                .HasColumnName("Documento");

            builder.Property(x => x.Name)
                .HasColumnName("Nome");

            builder.Property(x => x.Phone)
                .HasColumnName("Celular");

            //Relacionamento de um para muitos de acordo com a relação da base de dados
            builder.HasMany(c => c.Purchases)
                .WithOne(p => p.Person)
                .HasForeignKey(c => c.PersonId);
        }
    }
}
