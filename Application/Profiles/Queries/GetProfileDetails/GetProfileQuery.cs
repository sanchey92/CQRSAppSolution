using Application.Core;
using MediatR;

namespace Application.Profiles.Queries.GetProfileDetails
{
    public class GetProfileQuery : IRequest<Result<Profile>>
    {
        public string Username { get; set; }
    }
}