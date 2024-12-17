using CSharpFunctionalExtensions;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Providers;
using PetFamily.SharedKernel.Constraints;
using PetFamily.SharedKernel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.VolunteerRequest.Application.Commands.SetRequestForRemake
{
    public class SetRequestForRemakeHandler : ICommandHandler<SetRequestForRemakeCommand>
    {
        private readonly IVolunteerRequestRepository _requestRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SetRequestForRemakeHandler(IVolunteerRequestRepository requestRepository,
            [FromKeyedServices(ModuleNames.VolunteerRequest)]IUnitOfWork unitOfWork)
        {
            _requestRepository = requestRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<UnitResult<ErrorList>> Handle(SetRequestForRemakeCommand command, CancellationToken cancellation)
        {
            // находим реквест
            // меняем статус
            // сохраняем

            var request = await _requestRepository.GetById(command.DiscussionId, cancellation);
            if(request.IsFailure)
            {
                return request.Error.ToErrorList();
            }

            var result =  request.Value.SetOnRevision(command.RejectionComment);
            if(result.IsFailure)
            {
                return result.Error.ToErrorList();
            }

            await _unitOfWork.SaveChanges(cancellation);

            return Result.Success<ErrorList>();
        }
    }
}
