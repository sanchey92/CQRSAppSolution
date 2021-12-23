using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using MediatR;
using Persistence;

namespace Application.Activities.Commands.CreateActivity
{
    public class CreateActivityHandler : IRequestHandler<CreateActivityCommand, Result<Unit>>
    {
        private readonly DataContext _context;

        public CreateActivityHandler(DataContext context) => _context = context;

        public async Task<Result<Unit>> Handle(CreateActivityCommand request,
            CancellationToken cancellationToken)
        {
            _context.Activities.Add(request.Activity);
            var result = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (!result) return Result<Unit>.Failure("Failed to create activity");

            return Result<Unit>.Success(Unit.Value);
        }
    }
}