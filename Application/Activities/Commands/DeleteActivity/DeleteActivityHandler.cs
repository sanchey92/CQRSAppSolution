using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistence;

namespace Application.Activities.Commands.DeleteActivity
{
    public class DeleteActivityHandler : IRequestHandler<DeleteActivityCommand>
    {
        private readonly DataContext _context;

        public DeleteActivityHandler(DataContext context) => _context = context;

        public async Task<Unit> Handle(DeleteActivityCommand request,
            CancellationToken cancellationToken)
        {
            var activity = await _context.Activities.FindAsync(request.Id);
            _context.Remove(activity);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}