namespace CurrentAccount.Domain.CurrentAccountAggregate;

public sealed record AccountIdentification
{
    private AccountIdentification(string value, AccountIdentificationType accountIdentificationType)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Account number cannot be null or empty.", nameof(value));

        IdentifierValue = value;
        AccountIdentificationType = accountIdentificationType;
    }

    public AccountIdentificationType AccountIdentificationType { get; private set; }
    public string IdentifierValue { get; }

    public static AccountIdentification From(string value, AccountIdentificationType accountIdentificationType)
    {
        return new AccountIdentification(value, accountIdentificationType);
    }
}

public enum AccountIdentificationType
{
    BBAN,
    IBAN
};