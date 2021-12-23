using Application.Core;
using Domain;
using MediatR;

namespace Application.Activities.Commands.CreateActivity
{
    public class CreateActivityCommand : IRequest<Result<Unit>>
    {
        public Activity Activity { get; set; }
    }
}