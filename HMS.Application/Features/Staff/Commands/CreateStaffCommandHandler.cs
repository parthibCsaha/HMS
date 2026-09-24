using HMS.Application.Common.Interfaces.Services;
using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HMS.Application.Features.Staff.Commands;

public class CreateStaffCommandHandler(
    IStaffRepository staffRepo,
    IUserRepository userRepo,
    ICodeGeneratorService codeGen,
    IUnitOfWork uow,
    ILogger<CreateStaffCommandHandler> logger
) : IRequestHandler<CreateStaffCommand, ApiResponse<Guid>>
{
    public async Task<ApiResponse<Guid>> Handle(CreateStaffCommand cmd, CancellationToken ct)
    {
        if (!await userRepo.ExistsAsync(cmd.UserId, ct))
        {
            throw new NotFoundException("User", cmd.UserId);
        }
        var code = await codeGen.GenerateCodeAsync("STF", ct);
        var staff = new Domain.Entities.Staff
        {
            UserId = cmd.UserId,
            StaffCode = code,
            StaffType = Enum.Parse<HMS.Domain.Enums.StaffType>(cmd.StaffType),
            DepartmentId = cmd.DepartmentId,
            WardId = cmd.WardId,
            Qualification = cmd.Qualification,
            JoiningDate = cmd.JoiningDate,
            Shift = cmd.Shift,
            IsActive = true,
        };
        await staffRepo.AddAsync(staff, ct);
        await uow.SaveChangesAsync(ct);
        logger.LogInformation("Staff {Code} created", code);
        return ApiResponse<Guid>.Success(staff.Id, "Staff created successfully.");
    }
}

