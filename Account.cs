using System;
using System.Collections.Generic;
using System.Text;

namespace MicroBankApp
{
    class Account
    {
        public string Type { get; set; }
        public int AccountNumber { get; set; }
        public string Owner { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public string Note { get; set; }
        public int CustomerID { get; set; }
        public DateTime DateCreated { get; set; }

		private List<Transaction> allTransactions = new List<Transaction>();

		//private static int accountNumberSeed = BankAccount.accountNumberSeed;

		public Account(int id, string type, int acctNumber, string owner, decimal amount, string note)
        {
			this.CustomerID = id;
            this.Type = type;
            this.AccountNumber = acctNumber;
            this.Owner = owner;
			this.MakeDeposit(amount, DateTime.Now, note);
			this.Note = note;
        }

		public void MakeDeposit(decimal amount, DateTime date, string note)
		{
			if (amount <= 0)
			{
				throw new ArgumentOutOfRangeException(nameof(amount), "Amount of deposit must be positive");
			}
			var deposit = new Transaction(amount, date, note);
			allTransactions.Add(deposit);
			this.Balance += amount;
		}
		public void MakeWithdrawal(decimal amount, DateTime date, string note)
		{
			if (amount <= 0)
			{
				throw new ArgumentOutOfRangeException(nameof(amount), "Amount of withdrawal must be positive");
			}
			if ((Balance - amount < 100 && this.Type == "Savings") || (Balance - amount < 0 && this.Type == "Current"))
			{
				throw new InvalidOperationException("Not sufficient funds for this withdrawal");
			}
			var withdrawal = new Transaction(-amount, date, note);
			allTransactions.Add(withdrawal);
			this.Balance -= amount;
		}

		public void GetBalance()
        {
            Console.WriteLine($"Your balance is: {this.Balance}");
        }

		public void MakeTransferToSelf(int number, int amount, string note)
        {
			foreach (var item in BankAccount.Accounts)
			{
				if (this.CustomerID == item.CustomerID)
				{
                    if(this.AccountNumber != number)
                    {
						this.MakeWithdrawal(amount, DateTime.Now, note);
						item.MakeDeposit(amount, DateTime.Now, note);
                        Console.WriteLine("Money successfully transferred.");
						break;
                    }
				}
			}
			Console.WriteLine("Account not found.");
			return;
		}

		public void MakeTransferToOthers(int number, int amount, string note)
		{
            foreach (var item in BankAccount.Accounts)
            {
				if (item.AccountNumber == number)
				{
					this.MakeWithdrawal(amount, DateTime.Now, note);
					item.MakeDeposit(amount, DateTime.Now, note);
					Console.WriteLine("Money successfully transferred.");
					break;
				}
			}
			Console.WriteLine("Account not found.");
			return;
		}

		public string GetAccountHistory()
		{
			var report = new StringBuilder();

			//HEADER
			report.AppendLine("Date\t\tAmount\tNote");
			foreach (var item in allTransactions)
			{
				//ROWS
				report.AppendLine($"{item.Date.ToShortDateString()}\t{item.Amount}\t{item.Notes}");
			}
			return report.ToString();
		}

	}
}
