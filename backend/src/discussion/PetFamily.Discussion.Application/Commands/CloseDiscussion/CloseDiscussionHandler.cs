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

namespace PetFamily.Discussion.Application.Commands.CloseDiscussion
{
    public class CloseDiscussionHandler : ICommandHandler<CloseDiscussionCommand>
    {
        private readonly IDiscussionWriteDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDiscussionsRepository _repository;

        public CloseDiscussionHandler(
            IDiscussionWriteDbContext context,
            [FromKeyedServices(ModuleNames.Discussion)] IUnitOfWork unitOfWork,
            IDiscussionsRepository repository)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _repository = repository;
        }
        public async Task<UnitResult<ErrorList>> Handle(CloseDiscussionCommand command, CancellationToken cancellation)
        {
            var discussion = await _repository.GetById(command.DiscussionId, cancellation);
            if(discussion.IsFailure)
            {
                return discussion.Error.ToErrorList();
            }

            if(discussion.Value.Users.UsersExists(command.Adminid) == false)
            {
                return Error.Failure("admin.unattached",
                    $"admin does not take part in discussion with id: {command.DiscussionId}").ToErrorList();
            }

            discussion.Value.Close();

            await _unitOfWork.SaveChanges(cancellation);

            return Result.Success<ErrorList>();
        }
    }
}
