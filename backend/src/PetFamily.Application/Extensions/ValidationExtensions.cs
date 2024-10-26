using FluentValidation.Results;
using PetFamily.Domain.Shared.Errors;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Extensions
{
    public static class ValidationExtensions
    {
        public static ErrorList ToErrorList(this FluentValidation.Results.ValidationResult validationResult )
        {
            var validationErrors = validationResult.Errors;

            var errors = from validationError in validationErrors
                         let errorMessage = validationError.ErrorMessage
                         let error = Error.Deserialize(errorMessage)
                         select Error.Validation(error.Code, error.Message, validationError.PropertyName);

            return new ErrorList(errors);
        }

    }
}
