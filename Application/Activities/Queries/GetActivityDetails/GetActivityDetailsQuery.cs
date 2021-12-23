using System;
using Application.Core;
using Domain;
using MediatR;

namespace Application.Activities.Queries.GetActivityDetails
{
    public class GetActivityDetailsQuery : IRequest<Result<Activity>>
    {
        public Guid Id { get; set; }
    }
}