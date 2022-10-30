using CLI.Models;
using CLI.Controllers;
using AutoCompleteUtils;
using ConsoleUtils;
using System.Web.Mvc;
//using AutoCompletUtils;
//Globals
List<string> CommandLST = new List<string>
{
        "exit",
        "add_class",
        "add_field",
        "add_meth",
        "rem_class",
        "rem_field",
        "rem_meth",
        "relat",
        "rem_relat",
        "list_class",
        "list_classes",
        "list_relat",
        "import",
        "export",
        "help",
        "print",
        "mod_class",
        "mod_field",
        "mod_meth",
        "mod_relat",
        "undo",
        "redo",
        "save",
        "load"
};
var curDat = DateTime.Now;
Console.WriteLine($"WELCOME TO C-SHARK UML COMMAND LINE INTERFACE {curDat:d}");
bool exitCon = false;
string UsrIpt = "";
Console.Title = "C-Sharks";
Commands Input = new Commands();
string autocom;
var cyclingAutoComplete = new CyclingAutoComplete();

Console.WriteLine("NEW USER? [y/n]");
string userBool = Console.ReadLine();
UserModel userModel = new UserModel();
if (userBool == "y")
{
    string user;
    string pass;
    Console.WriteLine("NEW USER SIGNUP");
    Console.WriteLine("USERNAME:");
    user = Console.ReadLine();
    Console.WriteLine("PASSWORD:");
    pass = Console.ReadLine();
    userModel = new UserModel() { Username = user, Password = pass};
    int status = CLIController.Signup(userModel);
    if(status == 0)
    {
        Console.WriteLine("USER CREATED");
    }
    if(status == 1)
    {
        while (status != 0)
        {
            Console.WriteLine("USERNAME ALREADY TAKEN");
            Console.WriteLine("TRY AGAIN");
            Console.WriteLine("USERNAME:");
            user = Console.ReadLine();
            Console.WriteLine("PASSWORD:");
            pass = Console.ReadLine();
            userModel = new UserModel() { Username = user, Password = pass };
            status = CLIController.Signup(userModel);
        }
        Console.WriteLine("USER CREATED");
        
    }
}
else if (userBool == "n")
{
    Console.WriteLine("PLEASE LOGIN");
    Console.WriteLine("USER:");
    string user = Console.ReadLine();
    Console.WriteLine("PASSWORD:");
    string pass = Console.ReadLine();
    userModel = new UserModel() { Username = user, Password = pass };
    LoginModel loginModel = CLIController.Login(userModel);

    if (loginModel.status == 0)
    {
        Console.Write("WELCOME BACK " + user + "\n");
    }
    if (loginModel.status == 1)
    {
        Console.WriteLine("INCORRECT CREDENTIALS");
    }
    if (loginModel.status == 2)
    {
        Console.WriteLine("USER NOT FOUND");
    }
}
else
{
    while (userBool != "y" || userBool != "n")
    {
        Console.WriteLine("INVALID INPUT TRY AGAIN");
        userBool = Console.ReadLine();
    }
}
Console.WriteLine("PLEASE ENTER A COMMAND:");
while (exitCon == false)
{ 
    var result = ConsoleExt.ReadKey();
    switch (result.Key)
    {
        case ConsoleKey.Enter:
            UsrIpt = result.LineBeforeKeyPress.Line;
            if (Enum.TryParse<Commands>(UsrIpt, true, out Input))
            {
                if (Input == Commands.exit)
                {
                    exitCon = true;
                }
                CLIController.interpet(Input);
                Console.WriteLine("PLEASE ENTER A COMMAND:");
            }
            else
            {
                Console.WriteLine("INVALID COMMAND TRY HELP:");
            }
            break;
        case ConsoleKey.Tab:
            autocom = cyclingAutoComplete.AutoComplete(result.LineBeforeKeyPress.LineBeforeCursor,CommandLST);
            ConsoleExt.SetLine(autocom);
            break;
    }
    //UsrIpt = Console.ReadLine();
    
}
