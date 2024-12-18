using CSharpFunctionalExtensions;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Providers;
using PetFamily.Discussion.Domain.ValueObjects;
using PetFamily.SharedKernel.Constraints;
using PetFamily.SharedKernel.Id;
using PetFamily.SharedKernel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Discussion.Application.Commands.CreateDiscussion
{
    public class CreateDiscussionHandler : ICommandHandler<Guid, CreateDiscussionCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDiscussionWriteDbContext _writeDbContext;
        private readonly IDiscussionsRepository _discussionRepository;

        public CreateDiscussionHandler(
            [FromKeyedServices(ModuleNames.Discussion)]IUnitOfWork unitOfWork,
            IDiscussionWriteDbContext writeDbContext,
            IDiscussionsRepository repository)
        {
            _unitOfWork = unitOfWork;
            _writeDbContext = writeDbContext;
            _discussionRepository = repository;
        }
        public async Task<Result<Guid, ErrorList>> Handle(CreateDiscussionCommand command, CancellationToken cancellation)
        {
            // валидация
            var discussionId = DiscussionId.NewDiscussionId;
            var users = new Users(command.UserId, command.AdminId);

            var discussion = Discussion.Domain.AggregateRoot.Discussion.Create(
                discussionId,
                command.RelationId,
                users);

            await _discussionRepository.Add(discussion, cancellation);

            await _unitOfWork.SaveChanges(cancellation);

            return discussion.Id.Value;

        }
    }
}
