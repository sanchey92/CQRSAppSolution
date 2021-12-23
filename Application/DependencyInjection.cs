using System.Reflection;
using Application.Activities.Commands.CreateActivity;
using Application.Core;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(typeof(MappingProfiles).Assembly);
            services.AddFluentValidation(config =>
                config.RegisterValidatorsFromAssemblyContaining<CreateActivityCommandValidator>());
            return services;
        }
    }
}