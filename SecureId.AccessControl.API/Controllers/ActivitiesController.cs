using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecureId.AccessControl.API.DTOs;
using SecureId.AccessControl.API.IntegrationEvent.Activity;

namespace SecureId.AccessControl.API.Controllers
{
    public class ActivitiesController : BaseApiController
    {
        [Authorize(Roles = "SuperAdmin,Viewer,Client-User")]
        [HttpGet]
        public async Task<IActionResult> GetActivities() => 
            HandleResult(await Mediator.Send(new GetActivitiesEventHandler.Query()));
        [Authorize(Roles = "SuperAdmin,Viewer,Client-User")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetActivity(Guid id) => 
            HandleResult(await Mediator.Send(new GetSingleActivityEventHandler.Query { Id = id }));

        [HttpPost]
        public async Task<IActionResult> CreateActivity(ActivityDto activity) => 
            HandleResult(await Mediator.Send(new CreateActivityEventHandler.Command { activityRequest = activity }));
       

        [HttpPut("{id}")]
        public async Task<IActionResult> EditActivity(Guid id, ActivityDto activity) => 
            HandleResult(await Mediator.Send(new EditActivityEventHandler.Command { activityRequest = activity, Id = id }));

        [Authorize(Roles = "SuperAdmin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(Guid id) =>
            HandleResult(await Mediator.Send(new RemoveActivityEventHandler.Command { Id = id }));
       
    }
}
