using FluentAssertions;
using PetFamily.Application.Volunteers.SharedDtos;
using PetFamily.Domain.Pet;
using PetFamily.Domain.Pet.Breed;
using PetFamily.Domain.Pet.Species;
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
        // сверху будут лежать вспомогательные методы
        private Volunteer.Volunteer CreateVolunteer()
        {
            var fullName = new FullName("MASOASO", "ASAS", "ASASSASA");
            var email = Email.Create("sodksokdos@mail.co").Value;
            var phoneNumber = PhoneNumber.Create("222-11-22").Value;
            var address = new Address("MInsk", "Yakuba", 5, 5);
            var requisite = Requisite.Create("ririle", "222002012 120120102 ");
            var listReq = new List<Requisite>();
            listReq.Add(requisite.Value);
            var social = Social.Create("sdfnsdkjfksdj", "dsfksdjfkdsjfk");
            var socialList = new List<Social>();
            socialList.Add(social.Value);
            var petList = new List<Pet.Pet>();


            var volunteer = Volunteer.Volunteer.Create(
                fullName,
                email,
                "sodoosodiisodosid",
                10,
                phoneNumber,
                petList,
                address,
                null,
                null,
                VolunteerId.NewVolunteerId
                );

            return volunteer.Value;
        }

        private SpeciesBreed CreateSpeciesBreed()
        {
            var breedList = new List<Breed>();
            var species = Species.Create("—обака", breedList, SpeciesId.NewSpeciesId);
            var breed = Breed.Create("ƒворн€га", BreedId.NewBreedId, species.Value.Id);

            species.Value.AddBreed(breed.Value);

            var speciesBreed = new SpeciesBreed(species.Value.Id, breed.Value.Id.Value);

            return speciesBreed;
        }

        private void CreatePets(Volunteer.Volunteer volunteer)
        {
            var speciesBreed = CreateSpeciesBreed();
            PetStatus petStatus = PetStatus.NeedsHelp;

            for (int i = 1; i <= 7; i++)
            {
                var pet = Pet.Pet.Create
                (i.ToString(),
                speciesBreed,
                "black",
                "erer",
                "erer",
                volunteer.Number,
                volunteer.Address,
                volunteer.Requisites,
                petStatus,
                200,
                300,
                true,
                true,
                DateTime.Now,
                DateTime.Now,
                null,
                PetId.NewPetId
                );

                volunteer.AddPet(pet.Value);

            }

        }

        [Fact]
        public void Move_Pet_Same_Position_Returns_Same()
        {
            // arrange

            var volunteer = CreateVolunteer();
            CreatePets(volunteer);
            var thirdPet = volunteer.Pets[2];
            var position = Position.Create(3);

            // act

            volunteer.MovePet(thirdPet, position.Value);

            // assert

            thirdPet.Position.Should().Be(position.Value);
        }

        [Fact]
        public void Move_NonFirstAndlast_Position_Forward()
        {
            // arrange
            var volunteer = CreateVolunteer();
            CreatePets(volunteer);
            var firstPet = volunteer.Pets[0];
            var secondPet = volunteer.Pets[1];
            var thirdPet = volunteer.Pets[2];
            var forthPet = volunteer.Pets[3];
            var fifthPet = volunteer.Pets[4];
            var sixthPet = volunteer.Pets[5];
            var seventhPet = volunteer.Pets[6];

            var newPositionForSecond = Position.Create(6);

            // act

            volunteer.MovePet(secondPet, newPositionForSecond.Value);

            // assert

            secondPet.Position.Value.Should().Be(6);
            firstPet.Position.Value.Should().Be(1);
            sixthPet.Position.Value.Should().Be(5);
            fifthPet.Position.Value.Should().Be(4);
            forthPet.Position.Value.Should().Be(3);
            thirdPet.Position.Value.Should().Be(2);
            seventhPet.Position.Value.Should().Be(7);

        }

        [Fact]
        public void Move_NonFirstAndlast_Position_BackWard()
        {
            // arrange
            var volunteer = CreateVolunteer();
            CreatePets(volunteer);
            var firstPet = volunteer.Pets[0];
            var secondPet = volunteer.Pets[1];
            var thirdPet = volunteer.Pets[2];
            var forthPet = volunteer.Pets[3];
            var fifthPet = volunteer.Pets[4];
            var sixthPet = volunteer.Pets[5];
            var seventhPet = volunteer.Pets[6];

            var newPositionForFifrh = Position.Create(3);

            // act

            volunteer.MovePet(fifthPet, newPositionForFifrh.Value);

            // assert

            secondPet.Position.Value.Should().Be(2);
            firstPet.Position.Value.Should().Be(1);
            sixthPet.Position.Value.Should().Be(6);
            fifthPet.Position.Value.Should().Be(3);
            forthPet.Position.Value.Should().Be(5);
            thirdPet.Position.Value.Should().Be(4);
            seventhPet.Position.Value.Should().Be(7);

        }


    }
}