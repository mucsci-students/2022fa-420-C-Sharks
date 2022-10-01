using Microsoft.Build.Framework;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using NuGet.Protocol;
using System;
using System.Net;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using CLI.Models;
using CLI.Controllers;
using System.Web.Mvc;
using MongoDB.Bson.Serialization.Attributes;

namespace CLI.Controllers
{
    public class CLIController : Controller
    {
        public static void interpet(Commands input)
        {
            if(input == Commands.help)
            {
                Console.WriteLine("List of Commands:");
                Console.WriteLine("Help: Displays a list of commands with brief explanations.");
                Console.WriteLine("Add_Class: Creates a new class if name is valid. Input an name after the promt.");
                Console.WriteLine("Add_field: Add a field to a class. Input the class name after the promt, the input name.");
                Console.WriteLine("Add type after the promt for each.");
                Console.WriteLine("Add_Meth: Add a method to a class. Input the class name after the promt, the input name,");
                Console.WriteLine("return type, and enter parameters, after the promt for each.");
                Console.WriteLine("when you have no parameters or none are left enter N.");
                Console.WriteLine("Rem_Class: Remove a class. Input an name after the promt.");
                Console.WriteLine("Rem_field: Remove a field from a class. Input the class name and field name after their promt.");
                Console.WriteLine("Rem_Meth: Remove a method from a class. Input the class name and method name after their promt.");
                Console.WriteLine("relat: Add a relation beween classes. Input the to class name, from class name, and the relation");
                Console.WriteLine("type after their promt.");
                Console.WriteLine("Rem_relat: Remove a relation between classes. Input the to class name, and from class name after their promt.");
                Console.WriteLine("save: Saves the UML model to the database");
                Console.WriteLine("load: Loads a UML model from the database");
                Console.WriteLine("import: Load a UML model from a JSON file");
                Console.WriteLine("export: Saves a UML as a JSON file locally");
            }
            else if(input == Commands.add_class)
            {

            }
            else if(input == Commands.add_field)
            {

            }
            else if(input == Commands.add_meth)
            {

            }
            else if(input == Commands.rem_class)
            {

            }
            else if(input == Commands.rem_field)
            {

            }
            else if(input == Commands.rem_meth)
            {

            }
            else if(input == Commands.relat)
            {

            }
            else if(input == Commands.rem_relat)
            {

            }
            else if(input == Commands.save)
            {

            }
            else if(input == Commands.load)
            {

            }
            else if(input == Commands.import)
            {

            }
            else if(input == Commands.export)
            {

            }
        }
    }
}