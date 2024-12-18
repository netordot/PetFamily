using PetFamily.Core.Dtos.VolunteerRequest;
using PetFamily.VolunteerRequest.Application.Queries.GetVolunteerRequestsForParticipant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.VolunteerRequest.Presentation.VolunteerRequest.Contracts
{
    public record GetWithPaginationForUserRequest(int Page,
        int PageSize,
        string? FirstName,
        string? SecondName,
        string? LastName,
        string? Email,
        string? PhoneNumber,
        string? Description,
        VolunteerRequestStatusDto? VolunteerRequestStatus,
        int? Experience,
        string? RejectionComment)
    {
        public GetVolunteerRequestsForParticipantQuery ToQuery(Guid participantId)
        {
            return new GetVolunteerRequestsForParticipantQuery(
                Page,
                PageSize,
                participantId,
                FirstName,
                SecondName,
                LastName,
                Email,
                PhoneNumber,
                Description,
                VolunteerRequestStatus,
                Experience,
                RejectionComment);
        }
    }
}
