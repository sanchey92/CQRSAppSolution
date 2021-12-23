using System.Collections.Generic;
using Application.Core;
using Domain;
using MediatR;

namespace Application.Activities.Queries
{
    public class GetActivityListQuery : IRequest<Result<List<Activity>>>
    {
        
    }
}