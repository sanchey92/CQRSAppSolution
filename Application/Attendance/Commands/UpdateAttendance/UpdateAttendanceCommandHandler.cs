using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Attendance.Commands.UpdateAttendance
{
    public class UpdateAttendanceCommandHandler 
        : IRequestHandler<UpdateAttendanceCommand, Result<Unit>>
    {
        private readonly DataContext _dataContext;
        private readonly IUserAccessor _userAccessor;

        public UpdateAttendanceCommandHandler(DataContext dataContext, 
            IUserAccessor userAccessor) 
            => (_dataContext, _userAccessor) = (dataContext, userAccessor);

            public async Task<Result<Unit>> Handle(UpdateAttendanceCommand request, 
            CancellationToken cancellationToken)
            {
                var activity = await _dataContext.Activities
                    .Include(a => a.Attendees).ThenInclude(u => u.AppUser)
                    .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (activity == null) return null;

                var user = await _dataContext.Users.FirstOrDefaultAsync(x => 
                x.UserName == _userAccessor.GetUsername(), cancellationToken);

                if (user == null) return null;

                var hostUsername = activity.Attendees.FirstOrDefault(x => x.IsHost)?.AppUser?.UserName;
                var attendance = activity.Attendees.FirstOrDefault(x => x.AppUser.UserName == user.UserName);

                if (activity != null && hostUsername == user.UserName)
                    activity.IsCanceled = !activity.IsCanceled;

                if (attendance != null && hostUsername != user.UserName)
                    activity.Attendees.Remove(attendance);

                if (attendance == null)
                {
                    attendance = new ActivityAttendee
                    {
                        AppUser = user,
                        Activity = activity,
                        IsHost = false,
                    };
                    
                    activity.Attendees.Add(attendance);
                }

                var result = await _dataContext.SaveChangesAsync(cancellationToken) > 0;

                return result 
                    ? Result<Unit>.Success(Unit.Value) 
                    : Result<Unit>.Failure("Problem Updating Attendance");

            }
    }
}