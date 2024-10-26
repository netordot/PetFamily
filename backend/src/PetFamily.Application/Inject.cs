using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application.Photos.GetPhoto;
using PetFamily.Application.Photos.RemovePhoto;
using PetFamily.Application.Photos.UploadPhoto;
using PetFamily.Application.Species;
using PetFamily.Application.Species.AddBreeds;
using PetFamily.Application.Volunteers.AddPet;
using PetFamily.Application.Volunteers.AddPet.AddPhoto;
using PetFamily.Application.Volunteers.Create;
using PetFamily.Application.Volunteers.Delete;
using PetFamily.Application.Volunteers.UpdateMainInfo;
using PetFamily.Application.Volunteers.UpdateRequisites;
using PetFamily.Application.Volunteers.UpdateSocials;

namespace PetFamily.Application;

public static class Inject
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateVolunteerService>();
        services.AddScoped<UpdateVolunteerService>();
        services.AddScoped<UpdateSocialsService>();
        services.AddScoped<UpdateRequisitesService>();
        services.AddScoped<DeleteVolunteerService>();
        services.AddScoped<RemovePhotoService>();
        services.AddScoped<UploadPhotoService>();
        services.AddScoped<GetPhotoService>();
        services.AddScoped<AddPetService>();
        services.AddScoped<CreateSpeciesService>();
        services.AddScoped<AddBreedService>();
        services.AddScoped<AddPetFilesService>();


        services.AddValidatorsFromAssembly(typeof(Inject).Assembly);

        return services;
    }
}