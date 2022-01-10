using System.Threading.Tasks;
using Application.Profiles.Commands.EditProfile;
using Application.Profiles.Queries.GetProfileDetails;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProfilesController : BaseApiController
    {
        [HttpGet("{username}")]
        public async Task<IActionResult> GetProfile(string username)
        {
            return HandleResult(await Mediator.Send(new GetProfileQuery {Username = username}));
        }

        [HttpPut]
        public async Task<IActionResult> EditProfileAsync(EditProfileCommand editProfileCommand)
        {
            return HandleResult(await Mediator.Send(editProfileCommand));
        }
    }
}