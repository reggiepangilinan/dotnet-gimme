using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Yow.Api.Configurations
{
    public static class FluentValidation {
        /// <summary>
        /// Add Fluent Validation
        /// </summary>
        /// <param name="mvcBuilder">Mvc builder.</param>
        internal static void AddFluentValidation (IMvcBuilder mvcBuilder) {
            mvcBuilder.AddFluentValidation (fv => {
                fv.RegisterValidatorsFromAssemblyContaining<Application.Exceptions.RecordNotFoundException> ();
            });
        }
    }
}