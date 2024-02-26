using HRLeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRLeaveManagement.Persistence.DatabaseContext.Configurations;
public class LeaveTypeConfiguration : IEntityTypeConfiguration<LeaveType>
{
    public void Configure(EntityTypeBuilder<LeaveType> builder)
    {

        builder.HasData(
                new LeaveType
                {
                    Id = Guid.NewGuid(),
                    Name = "Vacation",
                    DefaultDays = 10,
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow
                }
            );
    }
}