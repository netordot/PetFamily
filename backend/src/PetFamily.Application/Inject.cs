using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application.Photos.GetPhoto;
using PetFamily.Application.Photos.RemovePhoto;
using PetFamily.Application.Photos.UploadPhoto;
using PetFamily.Application.Species;
using PetFamily.Application.Volunteers.AddPet;
using PetFamily.Application.Volunteers.Create;
using PetFamily.Application.Volunteers.CreateVolunteer;
using PetFamily.Application.Volunteers.Delete;
using PetFamily.Application.Volunteers.UpdateRequisites;
using PetFamily.Application.Volunteers.UpdateSocials;
using PetFamily.Application.Volunteers.UpdateVolunteer;

namespace PetFamily.Application;

public static class Inject
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICreateVolunteerService, CreateVolunteerService>();
        services.AddScoped<IUpdateVolunteerService, UpdateVolunteerService>();
        services.AddScoped<IUpdateSocialsService, UpdateSocialsService>();
        services.AddScoped<IUpdateRequisitesService, UpdateRequisitesService>();
        services.AddScoped<IDeleteVolunteerService, DeleteVolunteerService>();
        services.AddScoped<RemovePhotoService>();
        services.AddScoped<UploadPhotoService>();
        services.AddScoped<GetPhotoService>();
        services.AddScoped<AddPetService>();
        services.AddScoped<CreateSpeciesService>();


        services.AddValidatorsFromAssembly(typeof(Inject).Assembly);

        return services;
    }
}