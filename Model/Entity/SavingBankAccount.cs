using Contracts;


namespace Models
{
    public class SavingBankAccount : BankAccount
    {
        public static new int bankAccountsTotal = 0;
        private static decimal fixTaxOnTransaction;
        private static decimal percentageTaxOnTransaction;
        private static decimal taxOnTransactionMinWithDrawl;

        public static decimal TaxOnTransactionMinWithDrawl { get { return taxOnTransactionMinWithDrawl; } set { taxOnTransactionMinWithDrawl = value; } }
        public static decimal FixTaxOnTransaction { get { return fixTaxOnTransaction; } set { FixTaxOnTransaction = value; } }
        public static decimal PercentageTaxOnTransaction { get { return percentageTaxOnTransaction; } set { PercentageTaxOnTransaction = value; } }

        public SavingBankAccount(string bankAccountTitle, decimal openingBalance = 500) : base(bankAccountTitle, BankAccountType.Saving, openingBalance)
        {
            bankAccountsTotal++;
        }
        public static int BankAccountsTotal { get { return bankAccountsTotal; } }
        public override bool WithDraw(decimal amount)
        {
            decimal tax = 0;
            if (amount > taxOnTransactionMinWithDrawl)
            {
                tax = percentageTaxOnTransaction * amount;
                tax += fixTaxOnTransaction;
            }

            if (AccountBalance < amount + tax)
            {
                return false;
            }
            amount = amount + tax;
            return base.WithDraw(amount);
        }
        public override bool PayIn(decimal amount, bool isCancelledTransactionPayBack = false)
        {
            if (isCancelledTransactionPayBack)
            {
                amount = amount + amount * PercentageTaxOnTransaction + FixTaxOnTransaction;
            }
            return base.PayIn(amount);
        }
    }
}
