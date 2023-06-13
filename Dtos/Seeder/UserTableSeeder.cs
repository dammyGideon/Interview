using interview.Database.Entity;
using interview.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace interview.Dtos.Seeder;

public class UserTableSeeder : IEntityTypeConfiguration<UsersEntity>
{
    public void Configure(EntityTypeBuilder<UsersEntity> builder)
    {

        builder.HasData(
            new UsersEntity {
                Id= 1,
                Email="ajayegidolas@gmail.com",
                Password=PasswordHelper.EncryptPassword("12345678")
            }
        );   
    }
}