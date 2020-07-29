using System;
using System.Linq;

namespace BankApp
{
    class Program
    {

        // entry point for app
        static void Main(string[] args)
        {
            bool launchApp = true;
            do
            {
                Console.WriteLine("Welcome to Bank.");
                Console.Write("Do you want to LOGIN or CREATE ACCOUNT or EXIT APP? Type L to Login or C to Create Account or E to Exit: ");
                var response_one = Console.ReadLine().ToLower();

                if (response_one == "c")
                {
                    var response_two = string.Empty;
                    do
                    {
                        Console.WriteLine();
                        Console.Write("Are you a New Customer or an Existing Customer? Input N or E for response. To go Back input B: ");
                        response_two = Console.ReadLine();
                        if (response_two.Equals("n", StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine();
                            Console.WriteLine("Input the your details accordingly:");
                            Console.Write("Firstname: ");
                            var firstName = Console.ReadLine();

                            Console.Write("Lastname: ");
                            var lastName = Console.ReadLine();

                            Console.Write("Email address: ");
                            var email = Console.ReadLine();

                            Console.Write("Account Type (S for savings or C for current): ");
                            string accountType = Console.ReadLine();

                            Console.Write("Opening Amount: ");
                            var amount = Console.ReadLine();

                            if(firstName != string.Empty &&
                                lastName != string.Empty &&
                                email != string.Empty &&
                                ((accountType.Equals("s", StringComparison.OrdinalIgnoreCase) && Convert.ToDecimal(amount) >= 100) ||
                                (accountType.Equals("c", StringComparison.OrdinalIgnoreCase) && Convert.ToDecimal(amount) >= 1000))
                                )
                            {
                                BankAccount account = new BankAccount(firstName, lastName, email, accountType.ToLower(), Convert.ToDecimal(amount));
                                Console.WriteLine();
                                //Console.WriteLine($"Account successfully opened. with ID {}");
                                Console.WriteLine();
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Wrong input format.");
                                Console.WriteLine();
                                break;
                            }
                        }
                        if (response_two.Equals("e", StringComparison.OrdinalIgnoreCase))
                        {
                            Console.Write("Input your User ID: ");
                            login();
                        }
                        if (response_two.Equals("b", StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine();
                            break;
                        }
                    } while (!response_two.Equals("n", StringComparison.OrdinalIgnoreCase) && 
                            !response_two.Equals("e", StringComparison.OrdinalIgnoreCase) &&
                            !response_two.Equals("b", StringComparison.OrdinalIgnoreCase));
                    
                }
                else if (response_one == "l")
                {
                    Console.Write("Input your User ID: ");
                    login();
                }
                else if (response_one == "e")
                {
                    Console.WriteLine();
                    break;
                }
                else
                {
                    Console.WriteLine("Kindly select the right option.");
                    Console.WriteLine();
                    continue;
                }
            } while (launchApp);
            
        }
        // login for user
        public static void login()
        {
            var user_id = Console.ReadLine();
            bool seen = false;
            
            foreach (var item in BankAccount.Customers)
            {
                if(Int16.Parse(user_id) == item.ID)
                {
                    foreach (var item2 in BankAccount.Accounts)
                    {
                        if (Int16.Parse(user_id) == item2.CustomerID)
                        {
                            seen = true;
                            Customer customer = item;
                            Account account = item2;
                            OperateAccount(customer , account);
                        }
                    }
                }
            }

            if (!seen)
            {
                Console.WriteLine("User profile not found.");
                return;
            }
        }

        // user login interface
        public static void OperateAccount(Customer customer, Account account)
        {
            do
            {
                Console.WriteLine($"Welcome to our App Mr./Mrs. {customer.Owner}");
                Console.WriteLine("What would you like to do today:");
                Console.WriteLine("To create another account, Press 1.");
                Console.WriteLine("To make deposit, Press 2.");
                Console.WriteLine("To make withdrawal, Press 3.");
                Console.WriteLine("To get balance, Press 4.");
                Console.WriteLine("To transfer to owned account, Press 5.");
                Console.WriteLine("To transfer to another customer's account, Press 6.");
                Console.WriteLine("To logout, press 7.");
                Console.Write("Your reply here: ");
                var response_two = Console.ReadLine();

                switch (response_two)
                {
                    case "1":
                        Console.WriteLine();
                        Console.Write("Input Account Type: S for Savings and C for Current.");
                        var acctType = Console.ReadLine();
                        Console.Write("Input Deposit Amount: ");
                        var initialDeposit = Console.ReadLine();
                        customer.AddAccount(acctType.ToLower(), Convert.ToDecimal(initialDeposit));
                        Console.WriteLine();
                        break;
                    case "2":
                        Console.WriteLine();
                        Console.Write("Input Deposit Amount: ");
                        var deposit = Console.ReadLine();
                        var personalNote = "Self deposit";
                        account.MakeDeposit(Convert.ToDecimal(deposit), personalNote);
                        Console.WriteLine();
                        break;
                    case "3":
                        Console.WriteLine();
                        Console.Write("Input Withdrawal Amount: ");
                        var withdraw = Console.ReadLine();
                        Console.Write("Input Transaction Note: ");
                        var withdrawNote = Console.ReadLine();
                        account.MakeWithdrawal(Convert.ToDecimal(withdraw), withdrawNote);
                        Console.WriteLine();
                        break;
                    case "4":
                        Console.WriteLine();
                        account.GetBalance();
                        Console.WriteLine();
                        break;
                    case "5":
                        Console.WriteLine();
                        Console.Write("Amount To Transfer: ");
                        var transferSelf = Console.ReadLine();
                        Console.Write("Input Account Number: ");
                        var myNumber = Console.ReadLine();
                        Console.Write("Transaction Note: ");
                        var transactionNote = Console.ReadLine();
                        account.MakeTransferToSelf(Convert.ToInt32(myNumber), Convert.ToDecimal(transferSelf), transactionNote);
                        Console.WriteLine();
                        break;
                    case "6":
                        Console.WriteLine();
                        Console.Write("Amount To Transfer: ");
                        var transferOther = Console.ReadLine();
                        Console.Write("Input Account Number: ");
                        var otherNumber = Console.ReadLine();
                        Console.Write("Transaction Note: ");
                        var transactionDetails = Console.ReadLine();
                        account.MakeTransferToOthers(Convert.ToInt32(otherNumber), Convert.ToDecimal(transferOther), transactionDetails);
                        Console.WriteLine();
                        break;
                    case "7":
                        Console.WriteLine("Logout");
                        break;
                    default:
                        break;
                }
                if (response_two == "7")
                {
                    break;
                }
            } while (true);
        }
    }
}
