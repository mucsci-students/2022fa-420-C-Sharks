// See https://aka.ms/new-console-template for more information
Console.WriteLine("WELCOME TO C-SHARK UML COMMAND LINE INTERFACE");
bool exit = false;
while (exit == false)
{
    Console.WriteLine("PLEASE ENTER A COMMAND:");
    string userName = Console.ReadLine();
    if (userName == "exit")
    {
        exit = true;
    }
}
