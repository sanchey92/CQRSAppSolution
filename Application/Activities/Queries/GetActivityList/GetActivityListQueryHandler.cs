using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities.Queries
{
    public class GetActivityListQueryHandler :
        IRequestHandler<GetActivityListQuery, Result<List<Activity>>>
    {
        private readonly DataContext _context;

        public GetActivityListQueryHandler(DataContext context) => _context = context;

        public async Task<Result<List<Activity>>> Handle(GetActivityListQuery request,
            CancellationToken cancellationToken)
        {
            return Result<List<Activity>>
                .Success(await _context.Activities.ToListAsync(cancellationToken));
        }
    }
}