using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Nucleus.Api;


[AttributeUsage(AttributeTargets.Property)]
public sealed class ValidateEmailAttribute : ValidationAttribute
{

    public override bool IsValid(object? value)
    {
        if (value is null || string.IsNullOrWhiteSpace(value.ToString()))
            return false;

        return Regex.IsMatch(value!.ToString()!, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
    }
}
