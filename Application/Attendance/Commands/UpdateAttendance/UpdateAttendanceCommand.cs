using System;
using Application.Core;
using MediatR;

namespace Application.Attendance.Commands.UpdateAttendance
{
    public class UpdateAttendanceCommand : IRequest<Result<Unit>>
    {
        public Guid Id { get; set; }
    }
}