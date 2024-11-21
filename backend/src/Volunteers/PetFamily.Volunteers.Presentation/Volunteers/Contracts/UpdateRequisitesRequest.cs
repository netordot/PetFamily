using PetFamily.Application.PetManagement.Commands.Volunteers.UpdateRequisites;
using PetFamily.Volunteers.Application.Commands.UpdateRequisites;

namespace PetFamily.Volunteers.Presentation.Volunteers.Contracts
{
    public record UpdateRequisitesRequest(RequisiteListDto requisites);

}
