using Models;
using Handlers;

namespace AppConsole
{
    public static class Program
    {
       static void Main(string[] args)
        {
            BankAccountHandler bankAccountHandler = new BankAccountHandler(10);
            BankAccount[] bankAccounts;
            BankAccount? bankAccount = null;

            string? userSelectedMenuOption;
            string? customerAccountTitle;
            string? customerAccountType;
            string? customerAccountId;
            decimal customerAccountOpeningBalance;
            decimal customerTransactionAmount;
            string? TransferRequestAccountId;
            bool userValidChoiceFlag = true;

            do
            {
                Console.Clear();

                Console.WriteLine("- Welcome to Offshore Bank -");
                Console.WriteLine("                        feel your black money safe here");
                Console.WriteLine("///////////////////////////////////////////////////////");

                userSelectedMenuOption = "";
                customerAccountTitle = "";
                customerAccountType = "";
                customerAccountOpeningBalance = 0;
                customerAccountId = "";
                TransferRequestAccountId = "";

                Console.WriteLine("Press 1 for Create Account");
                Console.WriteLine("Press 2 for Search Account");
                Console.WriteLine("Press 3 for Deposit Amount");
                Console.WriteLine("Press 4 for Withdrawl Amount");
                Console.WriteLine("Press 5 for Transfer Transaction");
                Console.WriteLine("Press 6 to see all accounts");
                Console.WriteLine("Press any other key to exit");
                Console.WriteLine("");

                userSelectedMenuOption = Console.ReadLine().ToString();

                switch (userSelectedMenuOption)
                {
                    case "1":
                        if (BankAccount.BankAccountsTotal != bankAccountHandler.MaxBankAccountsCapacity)
                        {
                            Console.WriteLine("Enter Account Title");
                            customerAccountTitle = Console.ReadLine().ToString();

                            Console.WriteLine("Enter Opening Balance");
                            customerAccountOpeningBalance = Convert.ToDecimal(Console.ReadLine().ToString());

                            Console.WriteLine($"Enter Account Type: 1) for Saving 2) for Current Account");
                            customerAccountType = Console.ReadLine().ToString();

                            switch (customerAccountType)
                            {
                                case "1":
                                    bankAccountHandler.Add(new SavingBankAccount(customerAccountTitle, customerAccountOpeningBalance));
                                    Console.WriteLine();
                                    Console.WriteLine("Account Created Successfully...");
                                    break;
                                case "2":
                                    bankAccountHandler.Add(new CurrentBankAccount(customerAccountTitle, customerAccountOpeningBalance));
                                    Console.WriteLine();
                                    Console.WriteLine("Account Created Successfully...");
                                    break;
                                default:
                                    Console.WriteLine("Not a valid Account Type");
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Sorry! Bank is full.");
                        }
                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine("Enter the account code");
                        customerAccountId = Console.ReadLine().Trim();
                        Console.WriteLine($"Total accounts: {BankAccount.BankAccountsTotal}");
                        Console.WriteLine(bankAccountHandler.GetBankAccount(customerAccountId));
                        break;
                    case "3":
                        Console.Clear();
                        Console.WriteLine("Enter the account code for Deposit Amount");
                        customerAccountId = Console.ReadLine().Trim();
                        Console.WriteLine();
                        Console.WriteLine("Please enter amount to be deposited:");
                        customerTransactionAmount = Convert.ToDecimal(Console.ReadLine().Trim());
                        bankAccount = bankAccountHandler.GetBankAccount(customerAccountId);
                        Console.WriteLine();
                        if (bankAccount.PayIn(customerTransactionAmount))
                        {
                            Console.WriteLine("Amount Deposited successfully..");
                        }
                        else
                        {
                            Console.WriteLine("Not a valid Account Code.");
                        }
                        Console.WriteLine();
                        Console.WriteLine(bankAccount);
                        break;
                    case "4":
                        Console.Clear();
                        Console.WriteLine("Enter the account code for Withdrawl an Amount");
                        customerAccountId = Console.ReadLine().Trim();
                        Console.WriteLine();
                        Console.WriteLine("Please enter amount to be withdrawn:");
                        customerTransactionAmount = Convert.ToDecimal(Console.ReadLine().Trim());
                        bankAccount = bankAccountHandler.GetBankAccount(customerAccountId);
                        Console.WriteLine();
                        if (bankAccount.WithDraw(customerTransactionAmount))
                        {
                            Console.WriteLine("Amount withdrawn successfully..");
                        }
                        else
                        {
                            Console.WriteLine("Withdrawl unsuccessfull. Balance is lower than withdrawl amount (+ tax)");
                        }
                        Console.WriteLine();
                        Console.WriteLine(bankAccount);
                        break;
                    case "5":
                        Console.Clear();
                        Console.WriteLine("Enter Amount Sender Account Code");
                        TransferRequestAccountId = Console.ReadLine().Trim();
                        Console.WriteLine();
                        Console.WriteLine("Please enter to be Transferred:");
                        customerTransactionAmount = Convert.ToDecimal(Console.ReadLine().Trim());
                        Console.WriteLine();
                        Console.WriteLine("Enter Amount Receiver Account Code");
                        customerAccountId = Console.ReadLine().Trim();
                        Console.WriteLine();
                        if (bankAccountHandler.Transfer(bankAccountHandler.GetBankAccount(TransferRequestAccountId), bankAccountHandler.GetBankAccount(customerAccountId),customerTransactionAmount))
                        {
                            Console.WriteLine("Amount Transferred Successfully..");
                        }
                        else
                        {
                            Console.WriteLine("Transaction failed. either customer account(s) not valid OR Sender Account Balance is low.");
                        }
                        Console.WriteLine($"Sender is: ");
                        Console.WriteLine(bankAccountHandler.GetBankAccount(TransferRequestAccountId));
                        Console.WriteLine($"Reciver is: ");
                        Console.WriteLine(bankAccountHandler.GetBankAccount(customerAccountId));
                        break;
                    case "6":
                        Console.Clear();
                        Console.WriteLine($"Total accounts: {BankAccount.BankAccountsTotal}");
                        bankAccounts = bankAccountHandler.GetBankAccounts();
                        for (int i = 0; i <= BankAccount.BankAccountsTotal; i++)
                        {
                            Console.WriteLine(bankAccounts[i]);
                        }
                        break;
                    default:
                        userValidChoiceFlag = false;
                        break;
                }
                if (userValidChoiceFlag)
                {
                    Console.WriteLine();
                    Console.WriteLine("press any key to continue..");
                    Console.ReadKey();
                }
            } while (userValidChoiceFlag);
        }
    }
}
