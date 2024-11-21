using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.SharedKernel.Other;

public static class Errors
{
    public static class General
    {
        public static Error ValueIsInvalid(string? name = null)
        {
            var label = name ?? "value";
            return Error.Validation("value.is.invalid", $"{label} is invalid");
        }

        public static Error NotFound(Guid? id = null)
        {
            var forId = id == null ? "" : $"for id '{id}'";
            return Error.NotFound("record.not.found", $"record not found {forId}");
        }

        public static Error ValueIsRequired(string? name = null)
        {
            var label = name == null ? "" : " " + name + " ";
            return Error.Validation("length.is.invalid", $"invalid{label}");
        }

        public static Error AlreadyExists(string? name = null)
        {
            var label = name == null ? "" : " " + name + " ";
            return Error.Conflict("already.exists", $"invalid{label}");
        }
    }

    public static class User
    {
        public static Error InvalidCredentials(string? name = null)
        {
            var label = name ?? "value";
            return Error.Validation("credentials.invalid", $"invalid credentials");
        }
    }
}