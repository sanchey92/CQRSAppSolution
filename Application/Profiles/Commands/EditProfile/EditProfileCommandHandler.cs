using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Profiles.Commands.EditProfile
{
    public class EditProfileCommandHandler : IRequestHandler<EditProfileCommand, Result<Unit>>
    {
        private readonly DataContext _dataContext;
        private readonly IUserAccessor _userAccessor;

        public EditProfileCommandHandler(DataContext dataContext, IUserAccessor accessor)
            => (_dataContext, _userAccessor) = (dataContext, accessor);

        public async Task<Result<Unit>> Handle(EditProfileCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(x =>
                x.UserName == _userAccessor.GetUsername(), cancellationToken);

            user.Bio = request.Bio ?? user.Bio;
            user.DisplayName = request.DisplayName ?? user.DisplayName;

            var result = await _dataContext.SaveChangesAsync(cancellationToken) > 0;

            return result 
                ? Result<Unit>.Success(Unit.Value) 
                : Result<Unit>.Failure("Problem updating profile");
        }
    }
}