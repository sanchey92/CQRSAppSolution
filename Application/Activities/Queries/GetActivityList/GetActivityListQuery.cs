using System.Collections.Generic;
using Domain;
using MediatR;

namespace Application.Activities.Queries
{
    public class GetActivityListQuery : IRequest<List<Activity>>
    {
        
    }
}