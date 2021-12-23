using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities.Queries.GetActivityDetails
{
    public class GetActivityDetailsHandler
        : IRequestHandler<GetActivityDetailsQuery, Result<Activity>>
    {
        private readonly DataContext _context;

        public GetActivityDetailsHandler(DataContext context) => _context = context;

        public async Task<Result<Activity>> Handle(GetActivityDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var activity = await _context.Activities.FindAsync(request.Id);
            return Result<Activity>.Success(activity);
        }
    }
}