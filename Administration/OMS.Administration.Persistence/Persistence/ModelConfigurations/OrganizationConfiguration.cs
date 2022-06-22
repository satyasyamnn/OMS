using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OMS.Administration.Api.Infrastructure.Persistence;
using OMS.Administration.Domain.Entities;

namespace OMS.Administration.Infrasturcture.Persistence.ModelConfigurations
{
    public class OrganizationConfiguration : ModelConfigurationBase<Organization>
    {
        protected override void SetKeyFields(EntityTypeBuilder<Organization> builder)
        {
            builder.HasKey(organization => organization.Id);
        }
        protected override void SetRequiredFields(EntityTypeBuilder<Organization> builder)
        {
            builder.Property(organization => organization.Name).IsRequired();
            builder.Property(organization => organization.Email).IsRequired();
        }
        protected override void SetLengthFields(EntityTypeBuilder<Organization> builder)
        {
            builder.Property(organization => organization.Id).HasMaxLength(ColumnTypes.Text50.Length);
            builder.Property(organization => organization.Name).HasMaxLength(ColumnTypes.Text100.Length);
            builder.Property(organization => organization.Email).HasMaxLength(ColumnTypes.Text50.Length);
            builder.Property(organization => organization.TaxIdenfier).HasMaxLength(ColumnTypes.Text50.Length);
        }

        protected override void SetAdditionalInformation(EntityTypeBuilder<Organization> builder)
        {
            builder.OwnsOne(org => org.OfficeAddress).Property(e => e.Street).IsRequired().HasMaxLength(ColumnTypes.Text50.Length);
            builder.OwnsOne(org => org.OfficeAddress).Property(e => e.City).IsRequired().HasMaxLength(ColumnTypes.Text20.Length);
            builder.OwnsOne(org => org.OfficeAddress).Property(e => e.State).IsRequired().HasMaxLength(ColumnTypes.Text50.Length);
            builder.OwnsOne(org => org.OfficeAddress).Property(e => e.Country).IsRequired().HasMaxLength(ColumnTypes.Text20.Length);
            builder.OwnsOne(org => org.OfficeAddress).Property(e => e.ZipCode).IsRequired();
        }
    }
}
