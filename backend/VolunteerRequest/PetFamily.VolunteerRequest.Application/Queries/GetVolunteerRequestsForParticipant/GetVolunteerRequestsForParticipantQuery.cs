using PetFamily.Core.Abstractions;
using PetFamily.Core.Dtos.PetManagement;
using PetFamily.Core.Dtos.VolunteerRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.VolunteerRequest.Application.Queries.GetVolunteerRequestsForParticipant
{
    public record GetVolunteerRequestsForParticipantQuery(
        int Page,
        int PageSize,
        Guid ParticipantId, 
        string? FirstName,
        string? SecondName,
        string? LastName,
        string? Email,
        string? PhoneNumber,
        string? Description,
        VolunteerRequestStatusDto? VolunteerRequestStatus,  
        int? Experience,
        string? RejectionComment
        ) : IQuery;
   
}
