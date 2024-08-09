using DongleSimulator.Domain.Entities;
using DongleSimulator.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DongleSimulator.Infra.Database.Seeds;

public class CreateAdminSeed : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        var admin = new User
        {
            Id = 1,
            Email = "dglsim@email.com",
            Name = "dongle_admin",
            Password = "f5ed6428e4f6316d18b3c8dd27320d35f402084c18222575e36d5a1b7beeb9c2b1fdc878f66bcd4757e70973212281d94be330bad505524543cf30ff46fb4f8b", // dongle123456
            UserIdentifier = Guid.NewGuid(),
            Role = UserRole.Admin,
            CreatedAt = DateTime.UtcNow
        };
        builder.HasData(admin);
    }
}