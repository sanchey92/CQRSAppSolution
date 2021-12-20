using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistence;

namespace Application.Activities.Commands.CreateActivity
{
    public class CreateActivityHandler : IRequestHandler<CreateActivityCommand>
    {
        private readonly DataContext _context;

        public CreateActivityHandler(DataContext context) => _context = context;

        public async Task<Unit> Handle(CreateActivityCommand request,
            CancellationToken cancellationToken)
        {
            _context.Activities.Add(request.Activity);
            await _context.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}