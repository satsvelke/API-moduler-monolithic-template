using System.ComponentModel.DataAnnotations;
using Nucleus.Utilities;

namespace Nucleus.Api.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public sealed class AesDecryptAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        try
        {
            if (value is not null)
            {
                var decryptedValue = string.Empty;

                if (value.ToString()!.IsValidBase64String())
                    decryptedValue = value.ToString()!.Decrypt();
                else decryptedValue = value.ToString();

                validationContext?.ObjectType?
                .GetProperty(validationContext.MemberName!)?
                .SetValue(validationContext.ObjectInstance, string.IsNullOrWhiteSpace(decryptedValue) ? value : decryptedValue, null);
            }
        }
        catch (System.Exception)
        {
            throw;
        }

        return null;
    }
}