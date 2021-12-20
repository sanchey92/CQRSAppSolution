using Domain;
using MediatR;

namespace Application.Activities.Commands.EditActivity
{
    public class EditActivityCommand : IRequest
    {
        public Activity Activity { get; set; }
    }
}