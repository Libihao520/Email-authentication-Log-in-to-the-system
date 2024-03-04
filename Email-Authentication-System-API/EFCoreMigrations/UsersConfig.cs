using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Entitys;

namespace EFCoreMigrations;

 class UsersConfig:IEntityTypeConfiguration<users>
{
    public void Configure(EntityTypeBuilder<users> builder)
    {
        builder.ToTable("users");
    }
}