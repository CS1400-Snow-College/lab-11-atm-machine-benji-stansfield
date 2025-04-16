/*Benji Stansfield, 04-09-25*/
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
