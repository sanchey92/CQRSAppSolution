using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities.Queries.GetActivityDetails
{
    public class GetActivityDetailsHandler
        : IRequestHandler<GetActivityDetailsQuery, Activity>
    {
        private readonly DataContext _context;

        public GetActivityDetailsHandler(DataContext context) => _context = context;

        public async Task<Activity> Handle(GetActivityDetailsQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.Activities.FindAsync(request.Id);
        }
    }
}