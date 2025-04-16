/*Benji Stansfield, 04-09-25, Lab 11 "ATM Machine"*/
Console.Clear();

string[] lines = File.ReadAllLines("bank.txt");
int attempts = 3;
bool loggedIn = false;

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
        string balance = parts[2];

        if (usernameInput == username && pinInput == pin)
        {
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
            CheckBalance();
            break;
    }

}

static void CheckBalance()
{
    Console.WriteLine($"Your current balance is {parts[2]}.");
}

static void Withdraw()
{
    Console.Write("How much money would you like to withdraw?: ");
    int withdrawalAmmount = Convert.ToInt32(Console.ReadLine());
    if (withdrawalAmmount <= balance)
    {
        balance = balance - withdrawalAmmount;
        Console.Write($"You have taken out ${withdrawalAmmount}. Your remaining balance is {balance}.");
        return;
    }
    else
    {
        Console.WriteLine("You do not have sufficient funds.");
        return;
    }
}

static void Deposit()
{
    Console.Write("How much money would you like to deposit?: ");
    int depositAmmount = Convert.ToInt32(Console.ReadLine());
    balance = balance + depositAmmount;
    Console.Write($"You have deposited ${depositAmmount}. Your remaining balance is {balance}.");
    return;
}

static void QuickWithdraw40()
{
    if (balance >= 40)
    {
        balance = balance - 40;
        Console.WriteLine($"Transaction successful. Remaining balance is ${balance}.");
        return;
    }
    else
    {
        Console.WriteLine("Insufficient funds.");
        return;
    }
}

static void QuickWithdraw100()
{
    if (balance >= 100)
    {
        balance = balance - 100;
        Console.WriteLine($"Transaction successful. Remaining balance is ${balance}.");
        return;
    }
    else
    {
        Console.WriteLine("Insufficient funds.");
        return;
    }
}
