using System.Collections.Generic;
using Application.Activities.DTOs;
using Application.Core;
using MediatR;

namespace Application.Activities.Queries
{
    public class GetActivityListQuery : IRequest<Result<List<ActivityDto>>>
    {
        
    }
}