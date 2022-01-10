using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Activities.DTOs;
using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities.Queries
{
    public class GetActivityListQueryHandler :
        IRequestHandler<GetActivityListQuery, Result<List<ActivityDto>>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public GetActivityListQueryHandler(DataContext context, IMapper mapper)
            => (_context, _mapper) = (context, mapper);

        public async Task<Result<List<ActivityDto>>> Handle(GetActivityListQuery request,
            CancellationToken cancellationToken)
        {
            var activities = await _context.Activities
                .ProjectTo<ActivityDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            
            return Result<List<ActivityDto>>.Success(activities);
        }
    }
}