using Contracts;
using Models;


namespace Handlers
{
    public class BankAccountHandler : IBankAccountHandler
    {
        private readonly BankAccount[] bankAccounts;
        private int index = 0;
        public int MaxBankAccountsCapacity { get; }
        public BankAccountHandler(int size)
        {
            bankAccounts = new BankAccount[size];
            MaxBankAccountsCapacity = size;
        }

        //Tax Rules
        public static void SetTaxRules()
        {
            SavingBankAccount.FixTaxOnTransaction = 0;
            SavingBankAccount.PercentageTaxOnTransaction = 0.06M;
            SavingBankAccount.TaxOnTransactionMinWithDrawl = 50000M;

            CurrentBankAccount.FixTaxOnTransaction = 50;
            CurrentBankAccount.PercentageTaxOnTransaction = 0.0M;
            CurrentBankAccount.TaxOnTransactionMinWithDrawl = 0;
        }

        // Create
        public void Add(BankAccount bankAccount)
        {
            bankAccounts[index] = bankAccount;
            index += 1;
        }

        // Search
        public BankAccount? GetBankAccount(string Code, bool ignoreCase = false)
        {
            BankAccount? bankAccount = null;

            for (int i = 0; i < index; i++)
            {
                if (string.Compare(bankAccounts[i].AccountId, Code, ignoreCase) == 0)
                {
                    bankAccount = bankAccounts[i];
                    break;
                }
            }
            return bankAccount;
        }

        // Return List
        public BankAccount[] GetBankAccounts()
        {
            return bankAccounts;
        }

        // Money Transfer
        public bool Transfer(BankAccount senderAccount, BankAccount receiverAccount, decimal amount)
        {
            bool withDrawlSuccessfull = false;

            BankAccount tempSenderAccount = null;
            BankAccount tempReceiverAccount = null;

            if (senderAccount.AccountType == BankAccountType.Current)
            {
                tempSenderAccount = senderAccount as CurrentBankAccount;
            }
            else if (senderAccount.AccountType == BankAccountType.Saving)
            {
                tempSenderAccount = senderAccount as SavingBankAccount;
            }

            if (receiverAccount.AccountType == BankAccountType.Current)
            {
                tempReceiverAccount = receiverAccount as CurrentBankAccount;
            }
            else if (receiverAccount.AccountType == BankAccountType.Saving)
            {
                tempReceiverAccount = receiverAccount as SavingBankAccount;
            }

            if (tempSenderAccount.WithDraw(amount))
            {
                if (!tempReceiverAccount.PayIn(amount))
                {
                    tempSenderAccount.PayIn(amount,true);
                }
                else
                {
                    withDrawlSuccessfull = true;
                }
            }
        
            return withDrawlSuccessfull;
        }
    }
}