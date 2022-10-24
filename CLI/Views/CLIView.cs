using CLI.Models;
using CLI.Controllers;
using AutoCompleteUtils;
using ConsoleUtils;
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
        "redo"
};
var curDat = DateTime.Now;
Console.WriteLine($"WELCOME TO C-SHARK UML COMMAND LINE INTERFACE {curDat:d}");
bool exitCon = false;
string UsrIpt = "";
Console.Title = "C-Sharks";
Commands Input = new Commands();
string autocom;
var cyclingAutoComplete = new CyclingAutoComplete();
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
