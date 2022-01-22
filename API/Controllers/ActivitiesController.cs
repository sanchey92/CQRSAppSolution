using System;
using System.Threading.Tasks;
using Application.Activities.Commands.CreateActivity;
using Application.Activities.Commands.DeleteActivity;
using Application.Activities.Commands.EditActivity;
using Application.Activities.Queries;
using Application.Activities.Queries.GetActivityDetails;
using Application.Attendance.Commands.UpdateAttendance;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ActivitiesController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetActivities()
            => HandleResult(await Mediator.Send(new GetActivityListQuery()));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetActivityById(Guid id)
            => HandleResult(await Mediator.Send(new GetActivityDetailsQuery {Id = id}));

        [HttpPost]
        public async Task<IActionResult> CreateActivity(Activity activity)
            => HandleResult(await Mediator.Send(new CreateActivityCommand {Activity = activity}));

        [Authorize(Policy = "IsActivityHost")]
        [HttpPut("{id}")]
        public async Task<IActionResult> EditEntity(Guid id, Activity activity)
        {
            activity.Id = id;
            return HandleResult(await Mediator.Send(new EditActivityCommand {Activity = activity}));
        }

        [Authorize(Policy = "IsActivityHost")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(Guid id)
        {
            return HandleResult(await Mediator.Send(new DeleteActivityCommand {Id = id}));
        }

        [HttpPost("{id}/attend")]
        public async Task<IActionResult> Attend(Guid id)
        {
            return HandleResult(await Mediator.Send(new UpdateAttendanceCommand {Id = id}));
        }
    }
}