using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Domain.Pet
{
    public record SerialNumber
    {
        public int Value { get; }

        private SerialNumber(int value)
        {
            Value = value;
        }

        public static Result<SerialNumber, Error> Create(int value)
        {
            if(value <= 0)
            {
                return Errors.General.ValueIsInvalid("number");
            }

            return new SerialNumber(value);
        }
    }
}
