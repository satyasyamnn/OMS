using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OMS.Administration.Infrasturcture.Persistence.ModelConfigurations
{
    public abstract class ModelConfigurationBase<T> : IEntityTypeConfiguration<T> where T : class
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            SetKeyFields(builder);
            SetRequiredFields(builder);
            SetLengthFields(builder);
            SetAdditionalInformation(builder);
        }
        protected abstract void SetKeyFields(EntityTypeBuilder<T> builder);
        protected abstract void SetRequiredFields(EntityTypeBuilder<T> builder);
        protected abstract void SetLengthFields(EntityTypeBuilder<T> builder);
        protected abstract void SetAdditionalInformation(EntityTypeBuilder<T> builder);
    }
}
