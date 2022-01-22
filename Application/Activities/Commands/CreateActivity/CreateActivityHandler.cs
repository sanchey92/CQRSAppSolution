using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities.Commands.CreateActivity
{
    public class CreateActivityHandler : IRequestHandler<CreateActivityCommand, Result<Unit>>
    {
        private readonly DataContext _context;
        private readonly IUserAccessor _userAccessor;

        public CreateActivityHandler(DataContext context, IUserAccessor userAccessor)
            => (_context, _userAccessor) = (context, userAccessor);

        public async Task<Result<Unit>> Handle(CreateActivityCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => 
                x.UserName == _userAccessor.GetUsername(), cancellationToken);

            var attendee = new ActivityAttendee
            {
                AppUser = user,
                Activity = request.Activity,
                IsHost = true
            };
            
            request.Activity.Attendees.Add(attendee);
            _context.Activities.Add(request.Activity);
            
            var result = await _context.SaveChangesAsync(cancellationToken) > 0;

            return !result 
                ? Result<Unit>.Failure("Failed to create activity") 
                : Result<Unit>.Success(Unit.Value);
        }
    }
}