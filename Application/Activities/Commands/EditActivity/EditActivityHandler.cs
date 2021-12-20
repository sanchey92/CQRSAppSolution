using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Persistence;

namespace Application.Activities.Commands.EditActivity
{
    public class EditActivityHandler : IRequestHandler<EditActivityCommand>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public EditActivityHandler(DataContext context, IMapper mapper)
            => (_context, _mapper) = (context, mapper);

        public async Task<Unit> Handle(EditActivityCommand request,
            CancellationToken cancellationToken)
        {
            var activity = await _context.Activities.FindAsync(request.Activity.Id);
            _mapper.Map(request.Activity, activity);
            await _context.SaveChangesAsync();
            return Unit.Value;
        }
    }
}