using System.Threading;
using System.Threading.Tasks;
using Application.Activities.DTOs;
using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities.Queries.GetActivityDetails
{
    public class GetActivityDetailsHandler
        : IRequestHandler<GetActivityDetailsQuery, Result<ActivityDto>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public GetActivityDetailsHandler(DataContext context, IMapper mapper) 
            => (_context, _mapper) = (context, mapper);

        public async Task<Result<ActivityDto>> Handle(GetActivityDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var activity = await _context.Activities
                .ProjectTo<ActivityDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == request.Id);
            
            return Result<ActivityDto>.Success(activity);
        }
    }
}