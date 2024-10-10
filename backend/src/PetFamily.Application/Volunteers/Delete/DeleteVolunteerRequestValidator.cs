﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Volunteers.Delete
{
    public class DeleteVolunteerRequestValidator : AbstractValidator<DeleteVolunteerRequest>
    {
        public DeleteVolunteerRequestValidator()
        {
            RuleFor(d => d.Id).NotEmpty();
        }
    }
}
