﻿using Microsoft.AspNetCore.Http;

namespace PetFamily.Volunteers.Presentation.Volunteers.Contracts
{
    public record AddFilesRequest(IFormFileCollection files);
}