namespace CurrentAccount.Domain.CurrentAccountAggregate;

public readonly record struct CurrentAccountId(Guid Value)
{
    public static CurrentAccountId Empty { get; } = default;
    public static CurrentAccountId Create() => new(Guid.NewGuid());
    public override string ToString() => Value.ToString();
}

public sealed class CurrentAccount
{
    public CurrentAccountId Id { get; private set; }
    public AccountIdentification CurrentAccountNumber { get; private set; }

    // Parameterless constructor for EF
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor.
    private CurrentAccount() { }
#pragma warning restore CS8618

    public static CurrentAccount Create(AccountIdentification currentAccountNumber)
    {
        CurrentAccount currentAccount = new()
        {
            Id = CurrentAccountId.Create(),
            CurrentAccountNumber = currentAccountNumber
        };

        return currentAccount;
    }
}
