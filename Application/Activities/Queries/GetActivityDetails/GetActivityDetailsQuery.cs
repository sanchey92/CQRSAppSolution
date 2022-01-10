using System;
using Application.Activities.DTOs;
using Application.Core;
using Domain;
using MediatR;

namespace Application.Activities.Queries.GetActivityDetails
{
    public class GetActivityDetailsQuery : IRequest<Result<ActivityDto>>
    {
        public Guid Id { get; set; }
    }
}