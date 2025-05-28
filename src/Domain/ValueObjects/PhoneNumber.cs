using System.Text.RegularExpressions;

namespace Domain.ValueObjects;

public partial record PhoneNumber
{
      private const string patterns = @"^\+?\d{1,4}?[-.\s]?\(?\d{1,3}?\)?[-.\s]?\d{1,4}[-.\s]?\d{1,4}[-.\s]?\d{1,4}$";
    

    private PhoneNumber(string value) => Value = value;

    public string Value { get; init; }

    [GeneratedRegex(patterns)]
    private static partial Regex PhoneNumberRegex();

    public static PhoneNumber? Create(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return null;
        }

        return new PhoneNumber(value);
    }
}
