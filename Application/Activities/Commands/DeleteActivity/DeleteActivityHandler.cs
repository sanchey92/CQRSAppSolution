using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using MediatR;
using Persistence;

namespace Application.Activities.Commands.DeleteActivity
{
    public class DeleteActivityHandler : IRequestHandler<DeleteActivityCommand, Result<Unit>>
    {
        private readonly DataContext _context;

        public DeleteActivityHandler(DataContext context) => _context = context;

        public async Task<Result<Unit>> Handle(DeleteActivityCommand request,
            CancellationToken cancellationToken)
        {
           
            var activity = await _context.Activities.FindAsync(request.Id);
            
            _context.Remove(activity);

            var result = await _context.SaveChangesAsync(cancellationToken) > 0;
            
            if (!result) return Result<Unit>.Failure("Failed to delete the activity");
            
            return Result<Unit>.Success(Unit.Value);
        }
    }
}