using PetFamily.Application.Volunteers.SharedDtos;
using PetFamily.Domain.Pet;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Mails;
using PetFamily.Domain.Shared.PhoneNumber;
using PetFamily.Domain.Shared.Requisites;
using PetFamily.Domain.Volunteer;
using PetFamily.Domain.Volunteer.Details;

namespace PetFamily.Domain.Tests
{
    public class DomainTests
    {
        [Fact]
        public void Add_Pet_Return_Success_Increment_Position()
        {
            // arrange
            var fullName = new FullName("MASOASO", "ASAS", "ASASSASA");
            var email = Email.Create("sodksokdos@mail.co").Value;
            var phoneNumber = PhoneNumber.Create("222-11-22").Value;
            var address = new Address("MInsk", "Yakuba", 5, 5);
            var requisite = Requisite.Create("ririle", "222002012 120120102 ") ;
            var listReq = new List<Requisite>();
            listReq.Add(requisite.Value);
            var requisites = new Requisites(listReq);

            var social = Social.Create("dkjfkdkf", "sdfksd").Value;

            var socialList = new List<Social>();
            socialList.Add(social); 
            var details = new VolunteerDetails();

            var volunteer = Volunteer.Volunteer
                .Create(fullName, email, "hello my name is", 5, phoneNumber, null, address, requisites, details, VolunteerId.NewVolunteerId);
            // act

            // assert
        }
    }
}