using Models;


namespace Contracts
{
    public interface IBankAccountHandler
    {
        void Add(BankAccount bankAccount);
        BankAccount? GetBankAccount(string Code, bool ignoreCase = false);
        BankAccount[] GetBankAccounts();
    }
}