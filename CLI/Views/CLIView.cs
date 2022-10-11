using CLI.Models;
using CLI.Controllers;
Console.WriteLine("WELCOME TO C-SHARK UML COMMAND LINE INTERFACE");
bool exitCon = false;
string UsrIpt;
Commands Input = new Commands();
while (exitCon == false)
{
    Console.WriteLine("PLEASE ENTER A COMMAND:");
    UsrIpt = Console.ReadLine();
    if (Enum.TryParse<Commands>(UsrIpt, true, out Input))
    {
        if (Input == Commands.exit)
        {
            exitCon = true;
        }
        CLIController.interpet(Input);
    }
    else
    {
        Console.WriteLine("INVALID COMMAND TRY HELP:");
    }
}
