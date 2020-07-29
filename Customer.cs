using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp
{
    public class Customer
    {
        public int ID { get; set; }
        public string Owner { get; set; }
        public string Email { get; set; }
        public List<int> Numbers = new List<int>();

        public Customer(int id, string owner, string email, int number)
        {
            this.ID = id;
            this.Owner = owner;
            this.Email = email;
            this.Numbers.Add(number);
        }

        public void AddAccount(string type, decimal amount)
        {
            foreach (var item in BankAccount.Accounts)
            {
                if (item.CustomerID == this.ID)
                {
                    var owner = item.Owner;
                    var acctNumber = BankAccount.accountNumberSeed++;
                    var note = "Initial Deposit";

                    var account = new Account(item.CustomerID, type, acctNumber, owner, amount, note);
                    BankAccount.Accounts.Add(account);

                    this.Numbers.Add(acctNumber);
                    Console.WriteLine("Account Added.");
                    return;
                }
            }
            return;
        }
    }
}
