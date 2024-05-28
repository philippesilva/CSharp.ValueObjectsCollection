using System.Text.RegularExpressions;

namespace ValueObjectsCollection;

public readonly partial struct EmailAddress
{
    public readonly string Address;

    public EmailAddress(string address)
    {
        if (Validate(address))
            Address = address.Trim().ToLower();
        else
            throw new ArgumentException($"Invalid email '{address}'");
    }

    /// <summary>
    /// Validate e-mail | RFC 2822 Format
    /// </summary>
    /// <param name="address"></param>
    /// <returns></returns>
    public static bool Validate(string address) => RevexEmailAddress().IsMatch(address);

    [GeneratedRegex("\\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\\Z", RegexOptions.IgnoreCase, "pt-BR")]
    private static partial Regex RevexEmailAddress();

    public static implicit operator EmailAddress(string emailAddress) => new(emailAddress);

    public override string ToString() => Address;
}