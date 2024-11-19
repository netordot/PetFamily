using CSharpFunctionalExtensions;
using Microsoft.Extensions.FileProviders;
using Moq;
using PetFamily.Application.Database;
using PetFamily.Application.Providers;
using PetFamily.Domain.Pet.Breed;
using PetFamily.Domain.Pet;
using PetFamily.Domain.Pet.Species;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Errors;
using PetFamily.Domain.Shared.Mails;
using PetFamily.Domain.Shared.PhoneNumber;
using PetFamily.Domain.Volunteer;
using FluentAssertions;
using FileInfo = PetFamily.Core.Providers.FileInfo;
using PetFamily.Core.Messaging;
using PetFamily.Application.PetManagement.Commands.Volunteers;
using PetFamily.Application.PetManagement.Commands.Volunteers.AddPet.AddPhoto;
using PetFamily.Core.Providers;
using PetFamily.SharedKernel.Id;
using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.Application.Tests
{
    public class AddPetFilesServiceTests
    {
        // ��������������� ������
        private Volunteer CreateVolunteer()
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
            var petList = new List<Pet>();


            var volunteer = Volunteer.Create(
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
            var species = Domain.Pet.Species.Species.Create("������", breedList, SpeciesId.NewSpeciesId);
            var breed = Breed.Create("��������", BreedId.NewBreedId, species.Value.Id);

            species.Value.AddBreed(breed.Value);

            var speciesBreed = new SpeciesBreed(species.Value.Id, breed.Value.Id.Value);

            return speciesBreed;
        }

        private void CreatePet(Volunteer volunteer)
        {
            var speciesBreed = CreateSpeciesBreed();
            PetStatus petStatus = PetStatus.NeedsHelp;

            var pet = Pet.Create
            ("testPet",
            speciesBreed,
            "black",
            "erer",
            "erer",
            volunteer.Number,
            volunteer.Address,
            volunteer.Requisites.ToList(),
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


        [Fact]
        public async Task Add_Pet_Files_Service_Test()
        {
            // arrange

            var volunteer = CreateVolunteer();
            CreatePet(volunteer);
            var ownedPet = volunteer.Pets[0];

            var stream = new MemoryStream();
            var fileName = "testpic.jpg";
            var file = new FileDto(stream, null, fileName);

            List<FileDto> files = [file, file];

            List<FilePath> filePaths = [
                FilePath.Create(fileName).Value,
                FilePath.Create(fileName).Value];

            var ct = new CancellationTokenSource().Token;

            var mockRepository = new Mock<IVolunteerRepository>();
            mockRepository
                .Setup(r => r.GetById(volunteer.Id, ct))
                .ReturnsAsync(Result.Success<Volunteer, Error>(volunteer));

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(u => u.SaveChanges(ct))
                .Returns(Task.CompletedTask);

            var mockFileProvider = new Mock<Providers.IFileProvider>();
            mockFileProvider
                .Setup(f => f.UploadFile(It.IsAny<List<FileData>>(), ct))
                .ReturnsAsync(Result.Success<IReadOnlyList<FilePath>, Error>(filePaths));
            // возможно плохо мокнуто, пересмотреть
            var mockMessageQueue = new Mock<IMessageQueue<IEnumerable<FileInfo>>>();
            mockMessageQueue.Setup(m => m.ReadAsync(ct));

            var handler = new AddPetFilesHandler
                (mockRepository.Object,
                mockFileProvider.Object,
                mockUnitOfWork.Object,
                mockMessageQueue.Object
                );




            var command = new AddFileCommand(files);

            // act

           var result = await handler.Handle(ownedPet.Id.Value, volunteer.Id, command, ct);


            // assert

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(ownedPet.Id.Value);
        }
    }
}