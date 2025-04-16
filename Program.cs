/*Benji Stansfield, 04-09-25, Lab 11 "ATM Machine"*/

Console.Clear();

string[] lines = File.ReadAllLines("bank.txt");
int attempts = 3;
bool loggedIn = false;
decimal balance = 0;

while (!loggedIn && attempts > 0)
{
    Console.Write("Username: ");
    string usernameInput = Console.ReadLine();
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
            loggedIn = true;
            break;
        }
        else
        {
            Console.WriteLine("Username or pin not recognized.");
            attempts--;
            Console.WriteLine($"{attempts} attempts remaining");
            break;
        }
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
            Withdraw(balance);
            break;
        case 3:
            Deposit(balance);
            break;
        case 5:
            QuickWithdraw40(balance);
            break;
        case 6:
            QuickWithdraw100(balance);
            break;
    }

}

static decimal CheckBalance(decimal balance)
{
    Console.WriteLine($"Your current balance is {balance}.");
    return balance;
}

static decimal Withdraw(decimal balance)
{
    Console.Write("How much money would you like to withdraw?: ");
    int withdrawalAmmount = Convert.ToInt32(Console.ReadLine());
    if (withdrawalAmmount <= balance)
    {
        balance = balance - withdrawalAmmount;
        Console.Write($"You have taken out ${withdrawalAmmount}. Your remaining balance is {balance}.");
        return balance;
    }
    else
    {
        Console.WriteLine("You do not have sufficient funds.");
        return balance;
    }
}

static decimal Deposit(decimal balance)
{
    Console.Write("How much money would you like to deposit?: ");
    int depositAmmount = Convert.ToInt32(Console.ReadLine());
    balance = balance + depositAmmount;
    Console.Write($"You have deposited ${depositAmmount}. Your remaining balance is {balance}.");
    return balance;
}

static decimal QuickWithdraw40(decimal balance)
{
    if (balance >= 40)
    {
        balance = balance - 40;
        Console.WriteLine($"Transaction successful. Remaining balance is ${balance}.");
        return balance;
    }
    else
    {
        Console.WriteLine("Insufficient funds.");
        return balance;
    }
}

static decimal QuickWithdraw100(decimal balance)
{
    if (balance >= 100)
    {
        balance = balance - 100;
        Console.WriteLine($"Transaction successful. Remaining balance is ${balance}.");
        return balance;
    }
    else
    {
        Console.WriteLine("Insufficient funds.");
        return balance;
    }
}
