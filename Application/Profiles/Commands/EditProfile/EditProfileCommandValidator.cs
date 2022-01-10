using FluentValidation;

namespace Application.Profiles.Commands.EditProfile
{
    public class EditProfileCommandValidator : AbstractValidator<EditProfileCommand>
    {
        public EditProfileCommandValidator()
        {
            RuleFor(x => x.DisplayName).NotEmpty();
        }
    }
}