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

namespace CLI.Controllers
{
    public class CLIController : Controller
    {
        public static void interpet(Commands input)
        {
            if(input == Commands.help)
            {
                Console.WriteLine("List of Commands:");
                Console.WriteLine("Help: Displays a list of commands with brief explanations");
                Console.WriteLine("Add_Class: Creates a new class if name is valid. Input an name after the promt");
                Console.WriteLine("Add_field: Add a field to a class. Input the class name after the promt, the input name");
                Console.WriteLine("And type after their promt");
                Console.WriteLine("Add_Meth: Add a method to a class. Input the class name after the promt, the input name");
                Console.WriteLine("And return type");
                Console.WriteLine("Rem_Class:");
                Console.WriteLine("Rem_field:");
                Console.WriteLine("Rem_Meth:");
                Console.WriteLine("relat:");
                Console.WriteLine("Rem_relat:");
                Console.WriteLine("save:");
                Console.WriteLine("load:");
                Console.WriteLine("import:");
                Console.WriteLine("export:");
            }
        }
    }
}