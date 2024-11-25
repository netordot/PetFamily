using System.Net;
using CSharpFunctionalExtensions;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetFamily.Core.Models;
using PetFamily.SharedKernel.Enums;
using PetFamily.SharedKernel.ValueObjects;
namespace PetFamily.Core.Extensions;

public static class ResponseExtensions
{
    public static ActionResult ToResponse(this Error error)
    {
        var statusCode = GetStatusCodes(error.Type);


        var responseError = new ResponseError(error.Code, error.Message, null);
        var envelope = Envelope.Error(error.ToErrorList());

        return new ObjectResult(envelope)
        {
            StatusCode = statusCode
        };
    }

    public static ActionResult ToResponse(this ErrorList errorList)
    {
        if (!errorList.Any())
        {
            return new ObjectResult(null)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }

        var distinctErrorTypes = errorList.Select(x => x.Type).Distinct().ToList();

        var statusCode = distinctErrorTypes.Count > 1
            ? StatusCodes.Status500InternalServerError
            : GetStatusCodes(distinctErrorTypes.First());

        var envelope = Envelope.Error(errorList);

        return new ObjectResult(envelope)
        {
            StatusCode = statusCode
        };
    }

    private static int GetStatusCodes(ErrorType errorType)
    =>
        errorType switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Failure => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status500InternalServerError
        };





    //public static ActionResult ToValidationErrorResponse(this ValidationResult result)
    //{
    //    if (result.IsValid)
    //    {
    //        throw new InvalidOperationException("Result cannot be succeed");
    //    }

    //    var validationErrors = result.Errors;

    //    List<ResponseError> responseErrors = [];

    //    foreach (var validationError in validationErrors)
    //    {
    //        var errorMessage = validationError.ErrorMessage;

    //        var error = Error.Deserialize(errorMessage);

    //        var responseError = new ResponseError
    //        (error.Code,
    //            error.Message,
    //            validationError.PropertyName);

    //        responseErrors.Add(responseError);
    //    }

    //    var envelope = Envelope.Error(responseErrors);

    //    return new ObjectResult(envelope)
    //    {
    //        StatusCode = StatusCodes.Status400BadRequest
    //    };
    //}
}