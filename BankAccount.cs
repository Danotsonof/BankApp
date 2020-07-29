using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp
{
    class BankAccount
    {
		public static List<Customer> Customers = new List<Customer>();
		public static List<Account> Accounts = new List<Account>();
		
		public static int accountNumberSeed = 1111111111;
		public static int idNumber = 0001;

		// Account and Customer creation for new users
		public BankAccount(string firstName, string lastName, string email, string type, decimal amount)
        {
			var owner = $"{firstName} {lastName}";
			var acctNumber = accountNumberSeed++;
			var id = idNumber++;
			var note = "Initial Deposit";

			var account = new Account(id, type, acctNumber, owner, amount, note);
			Accounts.Add(account);

			var customer = new Customer(id, owner, email, acctNumber);
			Customers.Add(customer);

            Console.WriteLine($"Account Created with ID: {id}");
        }
	}
}
