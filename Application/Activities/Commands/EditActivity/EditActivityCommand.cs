using Application.Core;
using Domain;
using MediatR;

namespace Application.Activities.Commands.EditActivity
{
    public class EditActivityCommand : IRequest<Result<Unit>>
    {
        public Activity Activity { get; set; }
    }
}