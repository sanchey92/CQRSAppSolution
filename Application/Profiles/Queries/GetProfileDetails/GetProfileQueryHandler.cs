using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Profiles.Queries.GetProfileDetails
{
    public class GetProfileQueryHandler : IRequestHandler<GetProfileQuery, Result<Profile>>
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public GetProfileQueryHandler(DataContext dataContext, IMapper mapper)
            => (_dataContext, _mapper) = (dataContext, mapper);

        public async Task<Result<Profile>> Handle(GetProfileQuery request, 
            CancellationToken cancellationToken)
        {
            var user = await _dataContext.Users
                .ProjectTo<Profile>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(x => x.Username == request.Username, cancellationToken);

            return user == null 
                ? null 
                : Result<Profile>.Success(user);
        }
    }
}