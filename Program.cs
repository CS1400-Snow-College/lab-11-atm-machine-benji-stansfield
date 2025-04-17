/*Benji Stansfield, 04-09-25, Lab 11 "ATM Machine"*/

using System.Diagnostics;
using System.Threading.Tasks.Dataflow;

/*tests*/
Debug.Assert(QuickWithdraw100(100, "tester") == 0 == true,"Quick 100 arithmetic off");
Debug.Assert(QuickWithdraw100(50, "tester") == 50 == true, "Quick 100 arithmetic off");
Debug.Assert(QuickWithdraw100(50, "tester") == -50 == false, "Negative number in quick 100");
Debug.Assert(Withdraw(100, 70, "tester") == 30 == true, "Withdraw arithmetic off");
Debug.Assert(Withdraw(100, 110, "tester") == -10 == false, "Negative number in withdraw");
Debug.Assert(Withdraw(50, -30, "tester") == 80 == false, "Cannot subtract a negative number");
Debug.Assert(Deposit(100, 25, "tester") == 125 == true, "Deposit arithmetic off");
Debug.Assert(Deposit(50, -30, "tester") == 20 == false, "Cannot add a negative number");

Console.Clear();

string[] lines = File.ReadAllLines("bank.txt");
int attempts = 3;
bool loggedIn = false;
decimal balance = 0;
string usernameInput = "";
bool userFound = false;

while (!loggedIn && attempts > 0)
{
    Console.Write("Username: ");
    usernameInput = Console.ReadLine();
    Console.Write("Pin #: ");
    string pinInput = Console.ReadLine();

    foreach(string line in lines)
    {
        string[] parts = line.Split(',');

        string username = parts[0];
        string pin = parts[1];

        if (usernameInput == username && pinInput == pin)
        {   
            balance = Convert.ToDecimal(parts[2]);
            Console.Write("Sign in successful");
            userFound = true;
            loggedIn = true;
            break;
        }
    }
    if (!userFound)
    {
        Console.WriteLine("Username or pin not recognized.");
        attempts--;
        Console.WriteLine($"{attempts} attempts remaining");
        break;
    }
}

while(loggedIn)
{
    Console.WriteLine(@"
    1 - Check Balance
    2 - Withdraw
    3 - Deposit
    4 - Display last 5 transactions
    5 - Quick Withdraw $40
    6 - Quick Withdraw $100
    7 - End current session
    ");
    Console.Write("What would you like to do? (type 1-7): ");
    int menuSelection = Convert.ToInt32(Console.ReadLine());

    switch (menuSelection)
    {
        case 1:
            CheckBalance(balance);
            break;
        case 2:
            Console.Write("How much money would you like to withdraw?: ");
            int withdrawalAmmount = Convert.ToInt32(Console.ReadLine());
            balance = Withdraw(balance, withdrawalAmmount, usernameInput);
            SaveBalance(usernameInput, balance, lines);
            break;
        case 3:
            Console.Write("How much money would you like to deposit?: ");
            int depositAmmount = Convert.ToInt32(Console.ReadLine());
            balance = Deposit(balance, depositAmmount, usernameInput);
            SaveBalance(usernameInput, balance, lines);
            break;
        case 4:
            ShowLastTransactions(usernameInput);
            break;
        case 5:
            balance = QuickWithdraw40(balance, usernameInput);
            SaveBalance(usernameInput, balance, lines);
            break;
        case 6:
            balance = QuickWithdraw100(balance, usernameInput);
            SaveBalance(usernameInput, balance, lines);
            break;
        case 7:
            Console.WriteLine("Session ended.");
            loggedIn = false;
            break;
        default:
            Console.WriteLine("Invalid option.");
            break;
    }

}

static decimal CheckBalance(decimal balance)
{
    Console.WriteLine($"Your current balance is {balance}.");
    return balance;
}

static decimal Withdraw(decimal balance, int withdrawalAmmount, string usernameInput)
{
    if (withdrawalAmmount <= balance && withdrawalAmmount > 0)
    {
        balance = balance - withdrawalAmmount;
        Console.Write($"You have taken out ${withdrawalAmmount}. Your remaining balance is {balance}.");
        File.AppendAllText($"{usernameInput}_transactions.txt", $"{DateTime.Now} - withdrew ${withdrawalAmmount}\n"); // I got the 'AppendAllText' online because I couldn't figure out how to add text without overwriting it. 'WriteAllText would overwrite instead of add.
        return balance;
    }
    else
    {
        Console.WriteLine("You do not have sufficient funds.");
        return balance;
    }
}

static decimal Deposit(decimal balance, int depositAmmount, string usernameInput)
{   
    if (depositAmmount > 0)
    {
        balance = balance + depositAmmount;
        Console.Write($"You have deposited ${depositAmmount}. Your remaining balance is {balance}.");
        File.AppendAllText($"{usernameInput}_transactions.txt", $"{DateTime.Now} - deposited ${depositAmmount}\n");
        return balance;
    }
    else
    {
        Console.WriteLine("Please input a real number.");
        return balance;
    }
}

static decimal QuickWithdraw40(decimal balance, string usernameInput)
{
    if (balance >= 40)
    {
        balance = balance - 40;
        Console.WriteLine($"Transaction successful. Remaining balance is ${balance}.");
        File.AppendAllText($"{usernameInput}_transactions.txt", $"{DateTime.Now} - quick withdrew $40\n");
        return balance;
    }
    else
    {
        Console.WriteLine("Insufficient funds.");
        return balance;
    }
}

static decimal QuickWithdraw100(decimal balance, string usernameInput)
{
    if (balance >= 100)
    {
        balance = balance - 100;
        Console.WriteLine($"Transaction successful. Remaining balance is ${balance}.");
        File.AppendAllText($"{usernameInput}_transactions.txt", $"{DateTime.Now} - quick withdrew $100\n");
        return balance;
    }
    else
    {
        Console.WriteLine("Insufficient funds.");
        return balance;
    }
}

static void SaveBalance(string username, decimal newBalance, string[] lines)
{
    for (int i = 0; i < lines.Length; i++)
    {
        string[] parts = lines[i].Split(',');
        if (parts[0] == username)
        {
            parts[2] = newBalance.ToString();
            lines[i] = string.Join(",", parts);
            break;
        }
    }
    File.WriteAllLines("bank.txt", lines);
}

static void ShowLastTransactions(string usernameInput)
{
    if (File.Exists($"{usernameInput}_transactions.txt"))
    {
        string[] transactions = File.ReadAllLines($"{usernameInput}_transactions.txt");
        int count = Math.Min(5, transactions.Length);
        for (int i = transactions.Length - count; i < transactions.Length; i++)
            Console.WriteLine(transactions[i]);
    }
    else
    {
        Console.Write("No transactions found.");
    }
}
