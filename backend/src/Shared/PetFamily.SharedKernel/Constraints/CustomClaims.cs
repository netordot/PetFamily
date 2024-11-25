using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.SharedKernel.Constraints;

public static class CustomClaims
{
    public const string Role = nameof(Role);
    public const string Permission = nameof(Permission);
    public static string Id = nameof(Id);
    public static string Jti = nameof(Jti);
    public static string Email = nameof(Email);
}
