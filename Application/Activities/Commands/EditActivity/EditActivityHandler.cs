using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using AutoMapper;
using MediatR;
using Persistence;

namespace Application.Activities.Commands.EditActivity
{
    public class EditActivityHandler : IRequestHandler<EditActivityCommand, Result<Unit>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public EditActivityHandler(DataContext context, IMapper mapper)
            => (_context, _mapper) = (context, mapper);

        public async Task<Result<Unit>> Handle(EditActivityCommand request,
            CancellationToken cancellationToken)
        {
            var activity = await _context.Activities.FindAsync(request.Activity.Id);
           
            // if (activity == null) return null;
           
            _mapper.Map(request.Activity, activity);
            
            var result = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (!result) return Result<Unit>.Failure("Failed to update activity");

            return Result<Unit>.Success(Unit.Value);
        }
    }
}