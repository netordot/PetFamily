using CSharpFunctionalExtensions;
using PetFamily.SharedKernel.Other;
using PetFamily.SharedKernel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Volunteers.Domain.ValueObjects
{
    public record Position
    {
        public int Value { get; }

        private Position(int value)
        {
            Value = value;
        }

        private Position()
        {

        }

        public Result<Position, Error> Forward()
           => Create(Value + 1);

        public Result<Position, Error> Backward()
           => Create(Value - 1);


        public static Result<Position, Error> Create(int value)
        {
            if (value <= 0)
            {
                return Errors.General.ValueIsInvalid("number");
            }

            return new Position(value);
        }
    }
}
