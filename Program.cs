/*Benji Stansfield, 04-09-25*/
Console.Clear();

string[] lines = File.ReadAllLines("bank.txt");
foreach(string line in lines)
{
    string[] parts = line.Split(',');

    string username = parts[0];
    string pin = parts[1];
    int balance = Convert.ToInt32(parts[2]);
}
