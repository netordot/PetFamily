using CSharpFunctionalExtensions;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Providers;
using PetFamily.SharedKernel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Volunteers.Application.Commands.SoftDeleteVolunteer
{
    public class SoftDeleteVolunteerHandler : ICommandHandler<Guid, SoftDeleteVolunteerCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IVolunteerRepository _volunteerRepository;

        public SoftDeleteVolunteerHandler(
            IUnitOfWork unitOfWork,
            IVolunteerRepository volunteerRepository)
        {
            _unitOfWork = unitOfWork;
            _volunteerRepository = volunteerRepository;
        }
        public async Task<Result<Guid, ErrorList>> Handle(SoftDeleteVolunteerCommand command, CancellationToken cancellation)
        {
            var volunteer = await _volunteerRepository.GetById(command.Id, cancellation);
            if (volunteer.Value == null)
            {
                return command.Id;
            }

            volunteer.Value.Delete();

            // далльше будет только через юнит оф ворк, по другому и не получится

            _volunteerRepository.Save(volunteer.Value);
            await _unitOfWork.SaveChanges(cancellation);

            return command.Id;
        }
    }
}
