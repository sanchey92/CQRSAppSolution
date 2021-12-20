using System;
using Domain;
using MediatR;

namespace Application.Activities.Queries.GetActivityDetails
{
    public class GetActivityDetailsQuery : IRequest<Activity>
    {
        public Guid Id { get; set; }
    }
}