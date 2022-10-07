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
using System.Collections.Generic;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using CLI.Models;
using CLI.Controllers;
using System.Web.Mvc;
using MongoDB.Bson.Serialization.Attributes;
using System.Security.Permissions;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;
using UML.Models;
using System.Drawing.Printing;

namespace CLI.Controllers
{

    public class CLIController : Controller
    {
        static List<ScreenModel> OverScreen = new List<ScreenModel> { };
        static List<SingleRelationsModel> OverRelations = new List<SingleRelationsModel> { };
        public static void interpet(Commands input)
        {
            if (input == Commands.help)
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
            else if (input == Commands.add_class)
            {
                addCLSS();
            }
            else if (input == Commands.add_field)
            {
                addfield();
            }
            else if (input == Commands.add_meth)
            {
                addmeth();
            }
            else if (input == Commands.rem_class)
            {
                remClass();
            }
            else if (input == Commands.rem_field)
            {
                remField();
            }
            else if (input == Commands.rem_meth)
            {
                remMeth();
            }
            else if (input == Commands.relat)
            {
                addRel();
            }
            else if (input == Commands.rem_relat)
            {
                remRel();
            }
            else if (input == Commands.save)
            {

            }
            else if (input == Commands.load)
            {

            }
            else if (input == Commands.import)
            {

            }
            else if (input == Commands.export)
            {

            }
            else if (input == Commands.print)
            {
                PrintArray();
            }
        }
        /// <summary>
        /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        public static void addCLSS()
        {
            bool Err = false;
            Console.WriteLine("Enter Name of class:");
            string Input = Console.ReadLine();
            int CNT;
            for (CNT = 0; CNT < OverScreen.Count; CNT++)
            {
                if (OverScreen[CNT].name.Equals(Input))
                {
                    Err = true;
                }
            }
            while (Err)
            {
                Console.WriteLine("ERROR");
                Console.WriteLine("Enter a unique class name:");
                Input = Console.ReadLine();
                for (CNT = 0; CNT < OverScreen.Count; CNT++)
                {
                    if (OverScreen[CNT].name.Equals(Input))
                    {
                        Err = true;
                    }
                    else
                    {
                        Err = false;
                    }
                }
            }
            Console.WriteLine("Class added:");
            OverScreen.Add(new ScreenModel { name = Input });
        }
        /// <summary>
        /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        public static void addRel()
        {
            bool Err = true;
            Console.WriteLine("Enter Name of from class:");
            string InputRF = Console.ReadLine();
            int CNT;
            for (CNT = 0; CNT < OverScreen.Count; CNT++)
            {
                if (OverScreen[CNT].name.Equals(InputRF))
                {
                    Err = false;
                }
            }
            while (Err)
            {
                Console.WriteLine("ERROR");
                Console.WriteLine("Enter a valid class name:");
                InputRF = Console.ReadLine();
                for (CNT = 0; CNT < OverScreen.Count; CNT++)
                {
                    if (OverScreen[CNT].name.Equals(InputRF))
                    {
                        Err = false;
                    }
                    else
                    {
                        Err = true;
                    }
                }
            }
            Err = true;
            Console.WriteLine("Enter Name of to class:");
            string InputRT = Console.ReadLine();
            for (CNT = 0; CNT < OverScreen.Count; CNT++)
            {
                if (OverScreen[CNT].name.Equals(InputRT))
                {
                    Err = false;
                }
            }
            while (Err)
            {
                Console.WriteLine("ERROR");
                Console.WriteLine("Enter a valid class name:");
                InputRT = Console.ReadLine();
                for (CNT = 0; CNT < OverScreen.Count; CNT++)
                {
                    if (OverScreen[CNT].name.Equals(InputRT))
                    {
                        Err = false;
                    }
                    else
                    {
                        Err = true;
                    }
                }
            }
            Console.WriteLine("Enter type of relation:");
            string InputRR = Console.ReadLine();
            Console.WriteLine("Relation added:");
            OverRelations.Add(new SingleRelationsModel { source = InputRF, destinantion = InputRT, type = InputRR });
        }
        /// <summary>
        /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        public static void addfield()
        {
            bool Err = true;
            Console.WriteLine("Enter Name of class to add a field to:");
            string Input = Console.ReadLine();
            int Hold = 0;
            int CNT;
            for (CNT = 0; CNT < OverScreen.Count; CNT++)
            {
                if (OverScreen[CNT].name.Equals(Input))
                {
                    Err = false;
                    Hold = CNT;
                }
            }
            while (Err)
            {
                Console.WriteLine("ERROR");
                Console.WriteLine("Enter a valid class name:");
                Input = Console.ReadLine();
                for (CNT = 0; CNT < OverScreen.Count; CNT++)
                {
                    if (OverScreen[CNT].name.Equals(Input))
                    {
                        Err = false;
                        Hold = CNT;
                    }
                    else
                    {
                        Err = true;
                    }
                }
            }
            Console.WriteLine("Enter field name:");
            string InputN = Console.ReadLine();
            if (OverScreen[Hold].fields != null)
            {
                for (CNT = 0; CNT < OverScreen[Hold].fields.Length; CNT++)
                {
                    if (OverScreen[Hold].fields[CNT].name.Equals(InputN))
                    {
                        Err = false;
                        //Hold = CNT;
                    }
                }
            }
            else
            {
                Err = false;
            }
            while (Err)
            {
                Console.WriteLine("ERROR");
                Console.WriteLine("Enter a valid field name:");
                Input = Console.ReadLine();
                for (CNT = 0; CNT < OverScreen.Count; CNT++)
                {
                    if (OverScreen[CNT].fields[CNT].name.Equals(InputN))
                    {
                        Err = false;
                        //Hold = CNT;
                    }
                    else
                    {
                        Err = true;
                    }
                }
            }
            Console.WriteLine("Enter type name:");
            string InputT = Console.ReadLine();
            Console.WriteLine("Field added:");
            //static List<ScreenModel> OverScreen = new List<ScreenModel> { };
            List<Fields> tempt = new List<Fields> { };
            //List<Fields> Hold = new List<Fields> { };
            if (OverScreen[Hold].fields == null)
            {
                tempt.Add(new Fields { name = InputN, type = InputT });
                OverScreen[Hold].fields = tempt.ToArray();
            }
            else
            {
                tempt = OverScreen[Hold].fields.ToList();
                tempt.Add(new Fields { name = InputN, type = InputT });
                OverScreen[Hold].fields = tempt.ToArray();
            }
        }
        /// <summary>
        /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        public static void addmeth()
        {
            bool Err = true;
            Console.WriteLine("Enter Name of class to add a field to:");
            string Input = Console.ReadLine();
            int Hold = 0;
            int CNT;
            for (CNT = 0; CNT < OverScreen.Count; CNT++)
            {
                if (OverScreen[CNT].name.Equals(Input))
                {
                    Err = false;
                    Hold = CNT;
                }
            }
            while (Err)
            {
                Console.WriteLine("ERROR");
                Console.WriteLine("Enter a valid class name:");
                Input = Console.ReadLine();
                for (CNT = 0; CNT < OverScreen.Count; CNT++)
                {
                    if (OverScreen[CNT].name.Equals(Input))
                    {
                        Err = false;
                        Hold = CNT;
                    }
                    else
                    {
                        Err = true;
                    }
                }
            }
            Console.WriteLine("Enter method name:");
            string InputM = Console.ReadLine();
            if (OverScreen[Hold].fields != null)
            {
                for (CNT = 0; CNT < OverScreen[Hold].fields.Length; CNT++)
                {
                    if (OverScreen[Hold].methods[CNT].Equals(InputM))
                    {
                        Err = false;
                        //Hold = CNT;
                    }
                }
            }
            else
            {
                Err = false;
            }
            while (Err)
            {
                Console.WriteLine("ERROR");
                Console.WriteLine("Enter a valid method name:");
                Input = Console.ReadLine();
                for (CNT = 0; CNT < OverScreen.Count; CNT++)
                {
                    if (OverScreen[CNT].methods[CNT].name.Equals(InputM))
                    {
                        Err = false;
                        //Hold = CNT;
                    }
                    else
                    {
                        Err = true;
                    }
                }
            }
            Console.WriteLine("Enter return type name:");
            string InputR = Console.ReadLine();
            List<Fields> tempF = new List<Fields> { };
            List<Methods> tempM = new List<Methods> { };
            string InputN = "";
            string InputT;
            while (InputN != "N")
            {
                Console.WriteLine("Enter field name:");
                InputN = Console.ReadLine();
                if (InputN != "N")
                {
                    Console.WriteLine("Enter type name:");
                    InputT = Console.ReadLine();
                    tempF.Add(new Fields { name = InputN, type = InputT });
                }
            }
            Console.WriteLine("meth added:");
            //static List<ScreenModel> OverScreen = new List<ScreenModel> { };
            if (OverScreen[Hold].fields == null)
            {
                tempM.Add(new Methods { name = InputM, return_type = InputR, parameters = tempF.ToArray() });
                OverScreen[Hold].methods = tempM.ToArray();
            }
            else
            {
                tempM = OverScreen[Hold].methods.ToList();
                tempM.Add(new Methods { name = InputM, return_type = InputR, parameters = tempF.ToArray() });
                OverScreen[Hold].methods = tempM.ToArray();
            }
        }
        /// <summary>
        /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        public static void remClass()
        {
            bool Err = false;
            Console.WriteLine("Enter Name of class:");
            string Input = Console.ReadLine();
            int CNT;
            int Hold = 0;
            for (CNT = 0; CNT < OverScreen.Count; CNT++)
            {
                if (OverScreen[CNT].name.Equals(Input))
                {
                    Err = true;
                    Hold = CNT;
                }
            }
            while (!Err)
            {
                Console.WriteLine("ERROR");
                Console.WriteLine("Enter a unique class name:");
                Input = Console.ReadLine();
                for (CNT = 0; CNT < OverScreen.Count; CNT++)
                {
                    if (OverScreen[CNT].name.Equals(Input))
                    {
                        Err = true;
                        Hold = CNT;
                    }
                    else
                    {
                        Err = false;
                    }
                }
            }
            Console.WriteLine("Class Removed:");
            OverScreen.RemoveAt(Hold);
        }
        ///<summary>
        /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        public static void remField()
        {
            bool Err = false;
            Console.WriteLine("Enter Name of class:");
            string Input = Console.ReadLine();
            int CNT;
            int Hold = 0;
            for (CNT = 0; CNT < OverScreen.Count; CNT++)
            {
                if (OverScreen[CNT].name.Equals(Input))
                {
                    Err = true;
                    Hold = CNT;
                }
            }
            while (!Err)
            {
                Console.WriteLine("ERROR");
                Console.WriteLine("Enter a unique class name:");
                Input = Console.ReadLine();
                for (CNT = 0; CNT < OverScreen.Count; CNT++)
                {
                    if (OverScreen[CNT].name.Equals(Input))
                    {
                        Err = true;
                        Hold = CNT;
                    }
                    else
                    {
                        Err = false;
                    }
                }
            }
            Err = false;
            Console.WriteLine("Enter Name of Field:");
            Input = Console.ReadLine();
            int HoldF = 0;
            for (CNT = 0; CNT < OverScreen[Hold].fields.Length; CNT++)
            {
                if (OverScreen[Hold].fields[CNT].name.Equals(Input))
                {
                    Err = true;
                    HoldF = CNT;
                }
            }
            while (!Err)
            {
                Console.WriteLine("ERROR");
                Console.WriteLine("Enter a valid field name:");
                Input = Console.ReadLine();
                for (CNT = 0; CNT < OverScreen[Hold].fields.Length; CNT++)
                {
                    if (OverScreen[Hold].fields[CNT].name.Equals(Input))
                    {
                        Err = true;
                        HoldF = CNT;
                    }
                    else
                    {
                        Err = false;
                    }
                }
            }
            Console.WriteLine("Field Removed:");
            List<Fields> tempt = new List<Fields> { };
            tempt = OverScreen[Hold].fields.ToList();
            tempt.RemoveAt(HoldF);
            OverScreen[Hold].fields = tempt.ToArray();
        }
        /// <summary>
        /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        public static void remRel()
        {
            bool Err = false;
            Console.WriteLine("Enter Name of to class:");
            string InputT = Console.ReadLine();
            Console.WriteLine("Enter Name of from class:");
            string InputF = Console.ReadLine();
            Console.WriteLine("Enter type of relation:");
            string InputR = Console.ReadLine();
            int CNT;
            int Hold = 0;
            for (CNT = 0; CNT < OverRelations.Count; CNT++)
            {
                if ((OverRelations[CNT].source.Equals(InputT)) && (OverRelations[CNT].destinantion.Equals(InputT)) && (OverRelations[CNT].destinantion.Equals(InputT)))
                {
                    Err = true;
                    Hold = CNT;
                }
            }
            while (!Err)
            {
                Console.WriteLine("ERROR");
                Console.WriteLine("Enter Valid Name of to class:");
                InputT = Console.ReadLine();
                Console.WriteLine("Enter Valid Name of from class:");
                InputF = Console.ReadLine();
                Console.WriteLine("Enter Valid type of relation:");
                InputR = Console.ReadLine();
                for (CNT = 0; CNT < OverScreen.Count; CNT++)
                {
                    if ((OverRelations[CNT].source.Equals(InputT)) && (OverRelations[CNT].destinantion.Equals(InputT)) && (OverRelations[CNT].destinantion.Equals(InputT)))
                    {
                        Err = true;
                        Hold = CNT;
                    }
                    else
                    {
                        Err = false;
                    }
                }
            }
            Console.WriteLine("Relation Removed:");
            OverRelations.RemoveAt(Hold);
        }
        /// <summary>
        /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        ///
        public static void remMeth()
        {
            bool Err = false;
            Console.WriteLine("Enter Name of class:");
            string Input = Console.ReadLine();
            int CNT;
            int Hold = 0;
            for (CNT = 0; CNT < OverScreen.Count; CNT++)
            {
                if (OverScreen[CNT].name.Equals(Input))
                {
                    Err = true;
                    Hold = CNT;
                }
            }
            while (!Err)
            {
                Console.WriteLine("ERROR");
                Console.WriteLine("Enter a unique class name:");
                Input = Console.ReadLine();
                for (CNT = 0; CNT < OverScreen.Count; CNT++)
                {
                    if (OverScreen[CNT].name.Equals(Input))
                    {
                        Err = true;
                        Hold = CNT;
                    }
                    else
                    {
                        Err = false;
                    }
                }
            }
            Err = false;
            Console.WriteLine("Enter Name of Method:");
            Input = Console.ReadLine();
            int HoldF = 0;
            for (CNT = 0; CNT < OverScreen[Hold].fields.Length; CNT++)
            {
                if (OverScreen[Hold].methods[CNT].name.Equals(Input))
                {
                    Err = true;
                    HoldF = CNT;
                }
            }
            while (!Err)
            {
                Console.WriteLine("ERROR");
                Console.WriteLine("Enter a valid meth name:");
                Input = Console.ReadLine();
                for (CNT = 0; CNT < OverScreen[Hold].fields.Length; CNT++)
                {
                    if (OverScreen[Hold].methods[CNT].name.Equals(Input))
                    {
                        Err = true;
                        HoldF = CNT;
                    }
                    else
                    {
                        Err = false;
                    }
                }
            }
            Console.WriteLine("Field Removed:");
            List<Methods> tempt = new List<Methods> { };
            tempt = OverScreen[Hold].methods.ToList();
            tempt.RemoveAt(HoldF);
            OverScreen[Hold].methods = tempt.ToArray();
        }
        public static void PrintArray()
        {
            int CNT;
            Console.WriteLine("Objects");
            for (CNT = 0; CNT < OverScreen.Count; CNT++)
            {
                Console.WriteLine(OverScreen[CNT].ToString());
            }
            Console.WriteLine("Relations");
            for (CNT = 0; CNT < OverRelations.Count; CNT++)
            {
                Console.WriteLine(OverRelations[CNT].ToString());
            }
            Console.WriteLine();
        }
    }
}
    