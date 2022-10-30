using Microsoft.Build.Framework;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NuGet.Protocol;
using System;
using System.Net;
using System.Collections.Generic;
using System.Numerics;
using System.IO;
using System.Text;
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
using JsonSerializer = System.Text.Json.JsonSerializer;
using System.Transactions;
using System.Diagnostics;
using CLI.Models.ViewModels;

namespace CLI.Controllers
{

    public class CLIController : Controller
    {
        static List<ScreenModel> OverScreen = new List<ScreenModel> { };
        static List<SingleRelationsModel> OverRelations = new List<SingleRelationsModel> { };
        DiagramModel LastUndone = new DiagramModel { };
        static List<DiagramModel> MomentoSave = new List<DiagramModel> { };
        static int undoCounter = 0;
        static int undoIndex = 0;
        static UserModel GLOBALuserModel = new UserModel();
        public static void interpet(Commands input)
        {
            if (undoCounter == 0)
            {
                addSave();
            }
            if (input == Commands.help)
            {
                Console.WriteLine("List of Commands:");
                Console.WriteLine(" ");
                Console.WriteLine("Help: Displays a list of commands with brief explanations.");
                Console.WriteLine(" ");
                Console.WriteLine("Add_Class: Creates a new class if name is valid. Input an name after the prompt.");
                Console.WriteLine(" ");
                Console.WriteLine("Add_field: Add a field to a class. Input the class name after the prompt, the input name.");
                Console.WriteLine("And type after the prompt for each.");
                Console.WriteLine(" ");
                Console.WriteLine("Add_Meth: Add a method to a class. Input the class name after the prompt, the input name,");
                Console.WriteLine("return type, and enter parameters, after the prompt for each.");
                Console.WriteLine("when you have no parameters or none are left enter N.");
                Console.WriteLine(" ");
                Console.WriteLine("relat: Add a relation beween classes. Input the to class name, from class name, and the relation");
                Console.WriteLine("type after their prompt.");
                Console.WriteLine(" ");
                Console.WriteLine("Rem_Class: Remove a class. Input an name after the prompt.");
                Console.WriteLine(" ");
                Console.WriteLine("Rem_field: Remove a field from a class. Input the class name and field name after their prompt.");
                Console.WriteLine(" ");
                Console.WriteLine("Rem_Meth: Remove a method from a class. Input the class name and method name after their prompt.");
                Console.WriteLine(" ");
                Console.WriteLine("Rem_relat: Remove a relation between classes. Input the to class name, and from class name after their prompt.");
                Console.WriteLine(" ");
                Console.WriteLine("Mod_Class: Modify a class name. Input a new name after the prompt.");
                Console.WriteLine(" ");
                Console.WriteLine("Mod_field: modify a field from a class. Input the to class name, input the new field name and new type ");
                Console.WriteLine("name after their prompt.");
                Console.WriteLine(" ");
                Console.WriteLine("Mod_Meth: Remove a method from a class. Input the to class name, input the new method name and return");
                Console.WriteLine("type name after their prompt.");
                Console.WriteLine(" ");
                Console.WriteLine("Mod_relat: Modify a relation between classes. Input the to class name, and the new type.");
                Console.WriteLine(" ");
                Console.WriteLine("List_classes: Lists all classes currently added");
                Console.WriteLine(" ");
                Console.WriteLine("List_class: Lists all fields and methods of a given class. Input the unique class name after the prompt");
                Console.WriteLine(" ");
                Console.WriteLine("List_relat: Lists the relations currently added");
                Console.WriteLine(" ");
                Console.WriteLine("save: Saves the UML model to the database");
                Console.WriteLine(" ");
                Console.WriteLine("load: Loads a UML model from the database");
                Console.WriteLine(" ");
                Console.WriteLine("import: Load a UML model from a JSON file");
                Console.WriteLine(" ");
                Console.WriteLine("export: Saves a UML as a JSON file locally");
                Console.WriteLine(" ");
                Console.WriteLine("type esc to exit any command");
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
            else if (input == Commands.mod_class)
            {
                Modclass();
            }
            else if (input == Commands.mod_field)
            {
                Modfield();
            }
            else if (input == Commands.mod_relat)
            {
                ModRel();
            }
            else if (input == Commands.mod_meth)
            {
                ModMeth();
            }
            else if (input == Commands.list_class)
            {
                ListClass();
            }
            else if (input == Commands.list_classes)
            {
                ListClasses();
            }
            else if (input == Commands.list_relat)
            {
                ListRelat();
            }
            else if (input == Commands.import)
            {
                importJson();
            }
            else if (input == Commands.export)
            {
                exportJson();
            }
            else if (input == Commands.print)
            {
                PrintArray();
            }
            else if (input == Commands.undo)
            {
                undo();
            }
            else if (input == Commands.redo)
            {
                redo();
            }
            /*else if (input == Commands.save)
            {
                save();
            }
            else if (input == Commands.load)
            {
                load();
            }*/

        }
        /// <summary>
        /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        public static void addCLSS()
        {
            bool Err = false;
            Console.WriteLine("Enter Name of class:");
            string Input = Console.ReadLine();
            if (Input == "esc")
            {
                return;
            }
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
                if (Input == "esc")
                {
                    return;
                }
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
            addSave();
        }
        /// <summary>
        /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        public static void addRel()
        {
            bool Err = true;
            Console.WriteLine("Enter Name of from class:");
            string InputRF = Console.ReadLine();
            if (InputRF == "esc")
            {
                return;
            }
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
                if (InputRF == "esc")
                {
                    return;
                }
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
            if (InputRT == "esc")
            {
                return;
            }
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
                if (InputRT == "esc")
                {
                    return;
                }
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
            if (InputRR == "esc")
            {
                return;
            }
            Console.WriteLine("Relation added:");
            OverRelations.Add(new SingleRelationsModel { source = InputRF, destination = InputRT, type = InputRR });
            addSave();
        }
        /// <summary>
        /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        public static void addfield()
        {
            bool Err = true;
            Console.WriteLine("Enter Name of class to add a field to:");
            string Input = Console.ReadLine();
            if (Input == "esc")
            {
                return;
            }
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
                if (Input == "esc")
                {
                    return;
                }
                for (CNT = 0; CNT < OverScreen.Count; CNT++)
                {
                    //Console.WriteLine("ERROR");
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
            Err = false;
            Console.WriteLine("Enter field name:");
            string InputN = Console.ReadLine();
            if (InputN == "esc")
            {
                return;
            }
            if (OverScreen[Hold].fields != null)
            {
                for (CNT = 0; CNT < OverScreen[Hold].fields.Length; CNT++)
                {
                    if (OverScreen[Hold].fields[CNT].name.Equals(InputN))
                    {
                        Err = true;
                        //Hold = CNT;
                    }
                }
            }
            while (Err)
            {
                Console.WriteLine("ERROR");
                Console.WriteLine("Enter a valid field name:");
                InputN = Console.ReadLine();
                if (InputN == "esc")
                {
                    return;
                }
                for (CNT = 0; CNT < OverScreen.Count; CNT++)
                {
                    if (OverScreen[CNT].fields[CNT].name.Equals(InputN))
                    {
                        Err = true;
                        //Hold = CNT;
                    }
                    else
                    {
                        Err = false;
                    }
                }
            }
            Console.WriteLine("Enter type name:");
            string InputT = Console.ReadLine();
            if (InputT == "esc")
            {
                return;
            }
            Console.WriteLine("Field added:");
            //static List<ScreenModel> OverScreen = new List<ScreenModel> { };
            List<Fields> tempt = new List<Fields> { };
            //List<Fields> Hold = new List<Fields> { };
            if (OverScreen[Hold].fields == null)
            {
                tempt.Add(new Fields { name = InputN, type = InputT });
                OverScreen[Hold].fields = tempt.ToArray();
                addSave();
            }
            else
            {
                tempt = OverScreen[Hold].fields.ToList();
                tempt.Add(new Fields { name = InputN, type = InputT });
                OverScreen[Hold].fields = tempt.ToArray();
                addSave();
            }
        }
        /// <summary>
        /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        public static void addmeth()
        {
            bool Err = true;
            Console.WriteLine("Enter Name of class to add a method to:");
            string Input = Console.ReadLine();
            if (Input == "esc")
            {
                return;
            }
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
                if (Input == "esc")
                {
                    return;
                }
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
            //Err = false;
            Console.WriteLine("Enter method name:");
            string InputM = Console.ReadLine();
            if (InputM == "esc")
            {
                return;
            }
            if (OverScreen[Hold].methods != null)
            {
                for (CNT = 0; CNT < OverScreen[Hold].methods.Length; CNT++)
                {
                    if (OverScreen[Hold].methods[CNT].name.Equals(InputM))
                    {
                        Err = true;
                        //Hold = CNT;
                    }
                }
            }
            while (Err)
            {
                Console.WriteLine("ERROR");
                Console.WriteLine("Enter a valid method name:");
                InputM = Console.ReadLine();
                if (InputM == "esc")
                {
                    return;
                }
                for (CNT = 0; CNT < OverScreen[Hold].methods.Length; CNT++)
                {
                    Console.WriteLine("ERROR");
                    if (OverScreen[Hold].methods[CNT].name.Equals(InputM))
                    {
                        Err = true;
                        //Hold = CNT;
                    }
                    else
                    {
                        Err = false;
                    }
                }
            }
            Console.WriteLine("Enter return type name:");
            string InputR = Console.ReadLine();
            if (Input == "esc")
            {
                return;
            }
            List<Fields> tempF = new List<Fields> { };
            List<Methods> tempM = new List<Methods> { };
            string InputN = "";
            string InputT;
            while (InputN != "N")
            {
                Console.WriteLine("Enter Parameter name, or 'N' if none or no more:");
                InputN = Console.ReadLine();
                if (InputN == "esc")
                {
                    return;
                }
                if (InputN != "N")
                {
                    Console.WriteLine("Enter type name:");
                    InputT = Console.ReadLine();
                    if (InputT == "esc")
                    {
                        return;
                    }
                    tempF.Add(new Fields { name = InputN, type = InputT });
                }
                Console.WriteLine(" ");
            }
            Console.WriteLine("meth added:");
            //static List<ScreenModel> OverScreen = new List<ScreenModel> { };
            if (OverScreen[Hold].methods == null)
            {
                tempM.Add(new Methods { name = InputM, return_type = InputR, @params = tempF.ToArray() });
                OverScreen[Hold].methods = tempM.ToArray();
                addSave();
            }
            else
            {
                tempM = OverScreen[Hold].methods.ToList();
                tempM.Add(new Methods { name = InputM, return_type = InputR, @params = tempF.ToArray() });
                OverScreen[Hold].methods = tempM.ToArray();
                addSave();
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
            if (Input == "esc")
            {
                return;
            }
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
                Console.WriteLine("Enter a valid class name:");
                Input = Console.ReadLine();
                if (Input == "esc")
                {
                    return;
                }
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
            addSave();
        }
        ///<summary>
        /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        public static void remField()
        {
            bool Err = false;
            Console.WriteLine("Enter Name of class:");
            string Input = Console.ReadLine();
            if (Input == "esc")
            {
                return;
            }
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
                if (Input == "esc")
                {
                    return;
                }
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
            if (Input == "esc")
            {
                return;
            }
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
                if (Input == "esc")
                {
                    return;
                }
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
            addSave();
        }
        /// <summary>
        /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        public static void remRel()
        {
            bool Err = false;
            Console.WriteLine("Enter Name of to class:");
            string InputT = Console.ReadLine();
            if (InputT == "esc")
            {
                return;
            }
            Console.WriteLine("Enter Name of from class:");
            string InputF = Console.ReadLine();
            if (InputT == "esc")
            {
                return;
            }
            Console.WriteLine("Enter type of relation:");
            string InputR = Console.ReadLine();
            if (InputR == "esc")
            {
                return;
            }
            int CNT;
            int Hold = 0;
            for (CNT = 0; CNT < OverRelations.Count; CNT++)
            {
                if ((OverRelations[CNT].source.Equals(InputF)) && (OverRelations[CNT].destination.Equals(InputT)) && (OverRelations[CNT].type.Equals(InputR)))
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
                if (InputT == "esc")
                {
                    return;
                }
                Console.WriteLine("Enter Valid Name of from class:");
                InputF = Console.ReadLine();
                if (InputT == "esc")
                {
                    return;
                }
                Console.WriteLine("Enter Valid type of relation:");
                InputR = Console.ReadLine();
                if (InputT == "esc")
                {
                    return;
                }
                for (CNT = 0; CNT < OverScreen.Count; CNT++)
                {
                    if ((OverRelations[CNT].source.Equals(InputF)) && (OverRelations[CNT].destination.Equals(InputT)) && (OverRelations[CNT].type.Equals(InputR)))
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
            addSave();
        }
        /// <summary>
        /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        public static void remMeth()
        {
            bool Err = false;
            Console.WriteLine("Enter Name of class:");
            string Input = Console.ReadLine();
            if (Input == "esc")
            {
                return;
            }
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
                if (Input == "esc")
                {
                    return;
                }
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
            if (Input == "esc")
            {
                return;
            }
            int HoldF = 0;
            for (CNT = 0; CNT < OverScreen[Hold].methods.Length; CNT++)
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
                if (Input == "esc")
                {
                    return;
                }
                for (CNT = 0; CNT < OverScreen[Hold].methods.Length; CNT++)
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
            Console.WriteLine("Method Removed:");
            List<Methods> tempt = new List<Methods> { };
            tempt = OverScreen[Hold].methods.ToList();
            tempt.RemoveAt(HoldF);
            OverScreen[Hold].methods = tempt.ToArray();
            addSave();
        }
        /// <summary>
        /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        public static void Modclass()
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
                    bool Err2 = false;
                    Err = true;
                    Hold = CNT;
                    Console.WriteLine("Enter new name of class:");
                    Input = Console.ReadLine();
                    //int CNT;
                    for (CNT = 0; CNT < OverScreen.Count; CNT++)
                    {
                        if ((OverScreen[CNT].name.Equals(Input)) && (CNT != Hold))
                        {
                            Err2 = true;
                        }
                    }
                    while (Err2)
                    {
                        Console.WriteLine("ERROR");
                        Console.WriteLine("Enter a unique class name:");
                        Input = Console.ReadLine();
                        for (CNT = 0; CNT < OverScreen.Count; CNT++)
                        {
                            if ((OverScreen[CNT].name.Equals(Input)) && (CNT != Hold))
                            {
                                Err2 = true;
                            }
                            else
                            {
                                Err2 = false;
                            }
                        }
                    }
                    ///Console.WriteLine(CNT);
                }
            }
            while (!Err)
            {
                Console.WriteLine("ERROR");
                Console.WriteLine("Enter a valid class name:");
                Input = Console.ReadLine();
                for (CNT = 0; CNT < OverScreen.Count; CNT++)
                {
                    if (OverScreen[CNT].name.Equals(Input))
                    {
                        bool Err2 = false;
                        Err = true;
                        Hold = CNT;
                        Console.WriteLine("Enter new name of class:");
                        Input = Console.ReadLine();
                        for (CNT = 0; CNT < OverScreen.Count; CNT++)
                        {
                            if ((OverScreen[CNT].name.Equals(Input))&&(CNT!=Hold))
                            {
                                Err2 = true;
                            }
                        }
                        while (Err2)
                        {
                            Console.WriteLine("ERROR");
                            Console.WriteLine("Enter a unique class name:");
                            Input = Console.ReadLine();
                            for (CNT = 0; CNT < OverScreen.Count; CNT++)
                            {
                                if ((OverScreen[CNT].name.Equals(Input)) && (CNT != Hold))
                                {
                                    Err2 = true;
                                }
                                else
                                {
                                    Err2 = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        Err = false;
                    }
                }
            }
            Console.WriteLine("Class modified:");
            OverScreen[Hold].name = Input;
            addSave();
        }
        /// <summary>
        /// Modify a given field in a given class
        /// </summary>
        public static void Modfield()
        {
            bool Err = false;
            string InputN = "";
            string InputT = "";
            Console.WriteLine("Enter Name of class:");
            string Input = Console.ReadLine();
            if (Input == "esc")
            {
                return;
            }
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
                if (Input == "esc")
                {
                    return;
                }
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
            InputN = Console.ReadLine();
            if (InputN == "esc")
            {
                return;
            }
            int HoldF = 0;
            for (CNT = 0; CNT < OverScreen[Hold].fields.Length; CNT++)
            {
                if (OverScreen[Hold].fields[CNT].name.Equals(InputN))
                {
                        bool Err2 = false;
                        Err = true;
                        HoldF = CNT;
                        Console.WriteLine("Enter new name of field:");
                        InputN = Console.ReadLine();
                    if (InputN == "esc")
                    {
                        return;
                    }
                    for (CNT = 0; CNT < OverScreen[Hold].fields.Length; CNT++)
                        {
                            if ((OverScreen[Hold].fields[CNT].name.Equals(InputN)) && (CNT != HoldF))
                            {
                                Err2 = true;
                            }
                        }
                        while (Err2)
                        {
                            Console.WriteLine("ERROR");
                            Console.WriteLine("Enter a unique class name:");
                            InputN = Console.ReadLine();
                        if (InputN == "esc")
                        {
                            return;
                        }
                        for (CNT = 0; CNT < OverScreen[Hold].fields.Length; CNT++)
                            {
                                if ((OverScreen[Hold].fields[CNT].name.Equals(InputN)) && (CNT != HoldF))
                                {
                                    Err2 = true;
                                }
                                else
                                {
                                    Err2 = false;
                                }
                            }
                        }
                    Console.WriteLine("Enter new type name:");
                    InputT = Console.ReadLine();
                    if (InputT == "esc")
                    {
                        return;
                    }
                    //Console.WriteLine("Field added:");
                }
            }
            while (!Err)
            {
                Console.WriteLine("ERROR");
                Console.WriteLine("Enter a valid field name:");
                Input = Console.ReadLine();
                if (Input == "esc")
                {
                    return;
                }
                for (CNT = 0; CNT < OverScreen[Hold].fields.Length; CNT++)
                {
                    if (OverScreen[Hold].fields[CNT].name.Equals(Input))
                    {
                        bool Err2 = false;
                        Err = true;
                        HoldF = CNT;
                        Console.WriteLine("Enter new name of field:");
                        InputN = Console.ReadLine();
                        if (InputN == "esc")
                        {
                            return;
                        }
                        for (CNT = 0; CNT < OverScreen[Hold].fields.Length; CNT++)
                        {
                            if ((OverScreen[Hold].fields[CNT].name.Equals(InputN)) && (CNT != HoldF))
                            {
                                Err2 = true;
                            }
                        }
                        while (Err2)
                        {
                            Console.WriteLine("ERROR");
                            Console.WriteLine("Enter a unique class name:");
                            InputN = Console.ReadLine();
                            if (InputN == "esc")
                            {
                                return;
                            }
                            for (CNT = 0; CNT < OverScreen[Hold].fields.Length; CNT++)
                            {
                                if ((OverScreen[Hold].fields[CNT].name.Equals(InputN)) && (CNT != HoldF))
                                {
                                    Err2 = true;
                                }
                                else
                                {
                                    Err2 = false;
                                }
                            }
                        }
                        Console.WriteLine("Enter new type name:");
                        InputT = Console.ReadLine();
                        if (InputT == "esc")
                        {
                            return;
                        }
                    }
                    else
                    {
                        Err = false;
                    }
                }
            }
            Console.WriteLine("Field Modified:");
            OverScreen[Hold].fields[HoldF].name = InputN;
            OverScreen[Hold].fields[HoldF].type = InputT;
            addSave();
        }
        /// <summary>
        /// Modify a given method in a given class
        /// </summary>
        public static void ModMeth()
        {
                bool Err = false;
                List<Fields> tempF = new List<Fields> { };
                Console.WriteLine("Enter Name of class:");
                string Input = Console.ReadLine();
                if (Input == "esc")
                {
                    return;
                }
                string InputM = "";
                string InputR = "";

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
                    if (Input == "esc")
                    {
                        return;
                    }
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
                if (Input == "esc")
                {
                    return;
                }
                int HoldF = 0;
                for (CNT = 0; CNT < OverScreen[Hold].methods.Length; CNT++)
                {
                    if (OverScreen[Hold].methods[CNT].name.Equals(Input))
                    {
                        Err = true;
                        HoldF = CNT;
                        bool Err2 = false;
                        Console.WriteLine("Enter new method name:");
                        InputM = Console.ReadLine();
                        if (InputM == "esc")
                        {
                            return;
                        }
                        if (OverScreen[Hold].methods != null)
                        {
                            for (CNT = 0; CNT < OverScreen[Hold].methods.Length; CNT++)
                            {
                                if (OverScreen[Hold].methods[CNT].name.Equals(InputM) && (CNT != HoldF))
                                {
                                    Err2 = true;
                                    HoldF = CNT;
                                }
                            }
                        }
                        while (Err2)
                        {
                            Console.WriteLine("ERROR");
                            Console.WriteLine("Enter a valid method name:");
                            InputM = Console.ReadLine();
                            if (InputM == "esc")
                            {
                                return;
                            }
                            for (CNT = 0; CNT < OverScreen[Hold].methods.Length; CNT++)
                            {
                                //Console.WriteLine("ERROR");
                                if (OverScreen[Hold].methods[CNT].name.Equals(InputM) && (CNT != HoldF))
                                {
                                    Err2 = true;
                                    HoldF = CNT;
                                }
                                else
                                {
                                    Err2 = false;
                                }
                            }
                        }
                        Console.WriteLine("Enter return type name:");
                        InputR = Console.ReadLine();
                        if (InputR == "esc")
                        {
                            return;
                        }
                    //List<Methods> tempM = new List<Methods> { };
                    string InputN = "";
                        string InputT;
                        while (InputN != "N")
                        {
                            Console.WriteLine("Enter Parameter name, or 'N' if done or no more:");
                            InputN = Console.ReadLine();
                            if (InputN == "esc")
                            {
                                return;
                            }
                            if (InputN != "N")
                            {
                                Console.WriteLine("Enter type name:");
                                InputT = Console.ReadLine();
                                if (InputT == "esc")
                                {
                                    return;
                                }
                                tempF.Add(new Fields { name = InputN, type = InputT });
                            }
                            //Console.WriteLine(" ");
                        }

                    }
                }
                while (!Err)
                {
                    Console.WriteLine("ERROR");
                    Console.WriteLine("Enter a valid meth name:");
                    Input = Console.ReadLine();
                    if (Input == "esc")
                    {
                        return;
                    }
                    for (CNT = 0; CNT < OverScreen[Hold].methods.Length; CNT++)
                    {
                        if (OverScreen[Hold].methods[CNT].name.Equals(Input))
                        {
                            Err = true;
                            HoldF = CNT;
                        bool Err2 = false;
                        Console.WriteLine("Enter new method name:");
                        InputM = Console.ReadLine();
                        if (InputM == "esc")
                        {
                            return;
                        }
                        //different logic for if null
                        if (OverScreen[Hold].methods != null)
                        {
                            for (CNT = 0; CNT < OverScreen[Hold].methods.Length; CNT++)
                            {
                                if (OverScreen[Hold].methods[CNT].name.Equals(InputM) && (CNT != HoldF))
                                {
                                    Err2 = true;
                                    HoldF = CNT;
                                }
                            }
                        }
                        //if activated loop until proper inout is validated
                        while (Err2)
                        {
                            Console.WriteLine("ERROR");
                            Console.WriteLine("Enter a valid method name:");
                            InputM = Console.ReadLine();
                            if (InputM == "esc")
                            {
                                return;
                            }
                            for (CNT = 0; CNT < OverScreen[Hold].methods.Length; CNT++)
                            {
                                //Console.WriteLine("ERROR");
                                if (OverScreen[Hold].methods[CNT].name.Equals(InputM) && (CNT != HoldF))
                                {
                                    Err2 = true;
                                    HoldF = CNT;
                                }
                                else
                                {
                                    Err2 = false;
                                }
                            }
                        }
                        Console.WriteLine("Enter return type name:");
                        InputR = Console.ReadLine();
                        if (InputR == "esc")
                        {
                            return;
                        }
                        //List<Methods> tempM = new List<Methods> { };
                        string InputN = "";
                        string InputT;
                        //if activated loop until N is input
                        while (InputN != "N")
                        {
                            Console.WriteLine("Enter Parameter name, or 'N' if done or no more:");
                            InputN = Console.ReadLine();
                            if (InputN == "esc")
                            {
                                return;
                            }
                            if (InputN != "N")
                            {
                                Console.WriteLine("Enter type name:");
                                InputT = Console.ReadLine();
                                if (InputT == "esc")
                                {
                                    return;
                                }
                                tempF.Add(new Fields { name = InputN, type = InputT });
                            }
                            //Console.WriteLine(" ");
                        }
                    }
                        else
                        {
                            Err = false;
                        }
                    }
                }
                Console.WriteLine("Field Modified:");
                //check if empty
                if (!tempF.Any())
                {
                    
                    OverScreen[Hold].methods[HoldF].name = InputM;
                    OverScreen[Hold].methods[HoldF].return_type = InputR;
                    addSave();
                }
                else
                {
                    
                    OverScreen[Hold].methods[HoldF].name = InputM;
                    OverScreen[Hold].methods[HoldF].return_type = InputR;
                    OverScreen[Hold].methods[HoldF].@params = tempF.ToArray();
                    addSave();
                }
        }
        /// <summary>
        /// Modify a given relation type between two given classes
        /// </summary>
        public static void ModRel()
        {
            //Init variables for use in the function
            bool Err = false;
            //ask for input
            Console.WriteLine("Enter Name of to class:");
            string InputT = Console.ReadLine();
            if (InputT == "esc")
            {
                return;
            }
            Console.WriteLine("Enter Name of from class:");
            string InputF = Console.ReadLine();
            if (InputF == "esc")
            {
                return;
            }
            Console.WriteLine("Enter type of relation:");
            string InputR = Console.ReadLine();
            if (InputR == "esc")
            {
                return;
            }
            string InputY = "";
            int CNT;
            int Hold = 0;
            for (CNT = 0; CNT < OverRelations.Count; CNT++)
            {
                //check to see if proper input
                if ((OverRelations[CNT].source.Equals(InputF)) && (OverRelations[CNT].destination.Equals(InputT)) && (OverRelations[CNT].type.Equals(InputR)))
                {
                    Err = true;
                    Hold = CNT;
                    Console.WriteLine("Enter new type of relation:");
                    InputY = Console.ReadLine();
                    if (InputY == "esc")
                    {
                        return;
                    }
                }
            }
            //if activated loop until proper inout is validated
            while (!Err)
            {
                
                Console.WriteLine("ERROR");
                Console.WriteLine("Enter Valid Name of to class:");
                InputT = Console.ReadLine();
                if (InputT == "esc")
                {
                    return;
                }
                Console.WriteLine("Enter Valid Name of from class:");
                InputF = Console.ReadLine();
                if (InputF == "esc")
                {
                    return;
                }
                Console.WriteLine("Enter Valid type of relation:");
                InputR = Console.ReadLine();
                if (InputR == "esc")
                {
                    return;
                }
                for (CNT = 0; CNT < OverScreen.Count; CNT++)
                {
                    if ((OverRelations[CNT].source.Equals(InputF)) && (OverRelations[CNT].destination.Equals(InputT)) && (OverRelations[CNT].type.Equals(InputR)))
                    {
                        Err = true;
                        Hold = CNT;
                        Console.WriteLine("Enter new type of relation:");
                        InputY = Console.ReadLine();
                        if (InputY == "esc")
                        {
                            return;
                        }
                    }
                    else
                    {
                        Err = false;
                    }
                }
            }
            Console.WriteLine("Relation Modified:");
            OverRelations[Hold].type = InputY;
            addSave();
        }
        /// <summary>
        /// debug command to print entire contents
        /// </summary>
        public static void PrintArray()
        {
            //create list
            List<ExportScreenModel> ExpSM = new List<ExportScreenModel>();
            //trace trough each item in count add to the export model
            for (int Cnt = 0; Cnt < OverScreen.Count(); Cnt++)
            {
                ExpSM.Add(new ExportScreenModel
                {
                    name = OverScreen[Cnt].name,
                    fields = OverScreen[Cnt].fields,
                    methods = OverScreen[Cnt].methods
                });

            }
            //trace trough each item in count add to the export model
            var Exp = new ExportModel { classes = ExpSM.ToArray(), relationships = OverRelations.ToArray() };
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(Exp, Formatting.Indented);
            Console.WriteLine(json);
        }

        /*
         * List all classes currently loaded to the diagram
         * 
         */
        public static void ListClasses()
        {

            Console.WriteLine("CLASSES: \n");
            for (int Cnt = 0; Cnt < OverScreen.Count(); Cnt++)
            {
                //for every object in the screen
                //write its name
                Console.WriteLine("{0}", OverScreen[Cnt].name);

            }
            Console.WriteLine("");

        }


        /*
         * List all relations currently loaded to the diagram 
         *
         */
        public static void ListRelat()
        {

            Console.WriteLine("RELATIONS \n");
            List<SingleRelationsModel> display = new List<SingleRelationsModel>();
            //make new relations model list to add currently loaded relations to 
            for (int Cnt = 0; Cnt < OverRelations.Count(); Cnt++)
            {
                display.Add(new SingleRelationsModel
                {
                    source = OverRelations[Cnt].source,
                    destination = OverRelations[Cnt].destination,
                    type = OverRelations[Cnt].type
                });

            }
            //json convert them and print them after adding to the relations model list
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(display, Formatting.Indented);
            Console.WriteLine(json);
            Console.WriteLine("");

        }


        /*
         * List all contents of a given class
         */
        public static void ListClass()
        {
            if (OverScreen.Count() > 0){
                bool Err = false;
                Console.WriteLine("Enter Name of class:");
                string Input = Console.ReadLine();
                if (Input == "esc")
                {
                    return;
                }
                int CNT;
                int Hold = 0;
                //check every name in the currently loaded screen
                for (CNT = 0; CNT < OverScreen.Count; CNT++)
                {
                    if (OverScreen[CNT].name.Equals(Input))
                    {
                        Err = true;
                        Hold = CNT;
                    }
                }
                //if not found
                while (!Err)
                {
                    Console.WriteLine("ERROR");
                    Console.WriteLine("Enter a unique class name:");
                    //then ask again
                    Input = Console.ReadLine();
                    if (Input == "esc")
                    {
                        return;
                    }
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
                //if found
                Err = false;
                Console.WriteLine("");
                List<ExportScreenModel> ExpSM = new List<ExportScreenModel>();
                //make a new export screen model and add the information 
                ExpSM.Add(new ExportScreenModel
                {
                    name = OverScreen[Hold].name,
                    fields = OverScreen[Hold].fields,
                    methods = OverScreen[Hold].methods
                });
                //then convert to string and print
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(ExpSM, Formatting.Indented);
                Console.WriteLine(json);
                Console.WriteLine("");
            }
            else
            {
                Console.WriteLine("ERROR");
                Console.WriteLine("Must add a class first or type 'import'");
                Console.WriteLine("");
            }

        }



        /*
         * Export a string version of the diagrams contents to a json file of a given name in 
         * 
         * Local Disk (C:)
         *     UML-Saves
         *         C-Sharks-Editor
         * 
         * 
         */
        public static void exportJson()
        {
            // Specify a name for your top-level folder.
            string folderName = @"c:\UML-Saves";

            // To create a string that specifies the path to a subfolder under your
            // top-level folder, add a name for the subfolder to folderName.
            string pathString = System.IO.Path.Combine(folderName, "C-Sharks-Editor");

            // can extend the depth of your path if you want to.
            // pathString = System.IO.Path.Combine(pathString, "SubSubFolder");

            // Create subfolder. This structure should be able to be seen in File Explorer after export finalizes
            // 
            //    Local Disk (C:)
            //        UML-Saves
            //            C-Sharks-Editor
            System.IO.Directory.CreateDirectory(pathString);

            // Create a file name for the file you want to create.
            Console.WriteLine("ENTER NAME OF FILE TO BE CREATED, WITHOUT FILE TYPE/EXTENSION");
            string fileName = Console.ReadLine();
            fileName += ".json";

            // Combine again adds file name to the path.
            pathString = System.IO.Path.Combine(pathString, fileName);

            // Verify path that was constructed, also informs user of export location.
            Console.WriteLine("PATH TO MY FILE: {0}\n", pathString);

            // Check that the file doesn't already exist. If it doesn't exist, create
            // the file and write the classes array, and the relationships array.
            // System.IO.File.Create will overwrite  file if it exists.
            // could happen with random file names, although unlikely.
            if (!System.IO.File.Exists(pathString))
            {
                using (System.IO.FileStream fs = System.IO.File.Create(pathString))
                {


                    List<ExportScreenModel> ExpSM = new List<ExportScreenModel>();
                    for (int Cnt = 0; Cnt < OverScreen.Count(); Cnt++)
                    {
                        ExpSM.Add(new ExportScreenModel
                        {
                            name = OverScreen[Cnt].name,
                            fields = OverScreen[Cnt].fields,
                            methods = OverScreen[Cnt].methods
                        });

                    }
                    var Exp = new ExportModel { classes = ExpSM.ToArray(), relationships = OverRelations.ToArray() };
                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(Exp, Formatting.Indented);
                    JsonSerializer.SerializeAsync(fs, Exp);
                }
            }
            else
            {
                Console.WriteLine("FILE \"{0}\" ALREADY EXISTS.", fileName);
                return;
            }
        }



        /*
         * import locally saved json file to the diagram
         * file must be stored in 
         * Local Disk (C:)
         *      UML-Saves
         *          C-Sharks-Editor
         * 
         */
        public static void importJson()
        {
            string filename;
            string folderName = @"c:\UML-Saves";
            folderName = System.IO.Path.Combine(folderName, "C-Sharks-Editor");
            Console.WriteLine("PLEASE ENTER VALID FILE NAME IN ( C:/UML-Saves/C-Sharks-Editor/ ) TO IMPORT SUCH AS 'MySavedFile.json'");
            filename = Console.ReadLine();
            filename = System.IO.Path.Combine(folderName, filename);
            //combine the folder used for saving with the given filename and make 
            List<Fields> fieldList = new List<Fields> { };
            if (System.IO.File.Exists(filename))
            {
                //if the file exists, make a new list of screenmodels 
                List<ScreenModel> ScreenList = new List<ScreenModel> { };
                //get the file input
                string jsonString = System.IO.File.ReadAllText(filename);
                //parse it IDK thats what galen said
                if (undoCounter >= 0)
                {
                    undoCounter = 0;
                    undoIndex = 0;
                    MomentoSave = new List<DiagramModel> { };
                }
                var json = JObject.Parse(jsonString);
                var jclasses = json["classes"];
                var jrelations = json["relationships"];
                foreach (JObject obj in jclasses)
                {
                    //for every class there is, it will need a custom method list and field list 
                    List<Methods> MethList = new List<Methods> { };
                    List<Fields> FieldList = new List<Fields> { };
                    var jmethods = obj["methods"];
                    //save the name of the class
                    var nameHoldClass = obj.GetValue("name").ToString();


                    //check each field in class
                    var jfield = obj["fields"];
                    foreach (JObject field in jfield)
                    {
                        FieldList.Add(new Fields
                        {
                            name = field.GetValue("name").ToString(),
                            type = field.GetValue("type").ToString()
                        });
                    }

                    //then check every method of a given class
                    foreach (JObject meth in jmethods)
                    {
                        //save the name and return type for the given method
                        List<Fields> parameters = new List<Fields> { };
                        var nameHoldMeth = meth.GetValue("name").ToString();
                        var retHold = meth.GetValue("return_type").ToString();
                        var paramets = meth["params"];


                        //then save a list of params 
                        foreach (JObject param in paramets)
                        {
                            parameters.Add(new Fields
                            {
                                name = param.GetValue("name").ToString(),
                                type = param.GetValue("type").ToString()
                            });
                        }
                        //and add a new method to the list using saved values and the parameterlist.toArray
                        MethList.Add(new Methods
                        {
                            name = nameHoldMeth,
                            return_type = retHold,
                            @params = parameters.ToArray()
                        });
                    }

                    //Ayo idk what to put in LOC and KEY
                    ScreenList.Add(new ScreenModel
                    {
                        name = nameHoldClass,
                        methods = MethList.ToArray(),
                        fields = FieldList.ToArray(),
                    });
                }
                List<SingleRelationsModel> relations = new List<SingleRelationsModel> { };
                foreach (JObject rel in jrelations)
                {
                    relations.Add(new SingleRelationsModel
                    {
                        source = rel.GetValue("source").ToString(),
                        destination = rel.GetValue("destination").ToString(),
                        type = rel.GetValue("type").ToString()
                    });
                }
                OverScreen = ScreenList;
                Console.WriteLine(" ");
                Console.WriteLine("{0} CLASSES IMPORTED", ScreenList.Count());
                Console.WriteLine("{0} RELATIONSHIPS IMPORTED", relations.Count());
                OverRelations = relations;
                addSave();
            }

            else
            {
                Console.WriteLine("FILE \"{0}\" DOES NOT EXIST, ENTER 'IMPORT' AND TRY AGAIN, OR ENTER 'HELP'", filename);
                return;
            }
        }

        public static void addSave()
        {

            MomentoSave.Add(new DiagramModel
            {
                screen = new ScreenModel[OverScreen.Count],
                relations = new SingleRelationsModel[OverRelations.Count]
            });

            if (undoIndex < undoCounter)
            {
                undoCounter = undoIndex;
            }


            /*ScreenModel[] SaveArray = new ScreenModel[undoIndex];
            SingleRelationsModel[] RelSave = new SingleRelationsModel[undoIndex];
            Array.Copy(OverScreen.ToArray(), SaveArray, OverScreen.Count);
            Array.Copy(OverRelations.ToArray(), RelSave, OverRelations.Count);*/


            //Array.Copy(OverScreen.ToArray(), MomentoSave[undoIndex].screen, OverScreen.Count);
            //Array.Copy(OverRelations.ToArray(), MomentoSave[undoIndex].relations, OverRelations.Count);
            for (int o = 0; o < OverScreen.Count; o++)//o for object
            {
                MomentoSave[undoIndex].screen[o] = new ScreenModel
                {
                    name = OverScreen[o].name,
                    methods = null,
                    fields = null,
                };

                if (OverScreen[o].methods is not null)
                {
                    Methods[] meth = new Methods[OverScreen[o].methods.Length];

                    for (int m = 0; m < OverScreen[o].methods.Length; m++)//m for method
                    {
                        meth[m] = new Methods { };
                        if (!OverScreen[o].methods[m].@params.Length.Equals(null))
                        {
                            Fields[] parm = new Fields[OverScreen[o].methods[m].@params.Length];

                            for (int p = 0; p < OverScreen[o].methods[m].@params.Length; p++)//p for param
                            {
                                parm[p] = OverScreen[o].methods[m].@params[p];
                            }
                            meth[m].name = OverScreen[o].methods[m].name;
                            meth[m].return_type = OverScreen[o].methods[m].return_type;
                            meth[m].@params = parm;
                        }
                        else
                        {
                            meth[m].name = OverScreen[o].methods[m].name;
                            meth[m].return_type = OverScreen[o].methods[m].return_type;
                        }

                    }
                    MomentoSave[undoIndex].screen[o].methods = meth;

                }

                if (OverScreen[o].fields is not null)
                {
                    Fields[] fld = new Fields[OverScreen[o].fields.Length];
                    for (int f = 0; f < OverScreen[o].fields.Length; f++)
                    {
                        fld[f] = new Fields { };
                        fld[f].name = OverScreen[o].fields[f].name;
                        fld[f].type = OverScreen[o].fields[f].type;
                    };
                    MomentoSave[undoIndex].screen[o].fields = fld;

                }



            }

            for (int i = 0; i < OverRelations.Count; i++)
            {
                MomentoSave[undoIndex].relations[i] = new SingleRelationsModel
                {
                    source = OverRelations[i].source,
                    destination = OverRelations[i].destination,
                    type = OverRelations[i].type
                };
            }

            undoCounter++;
            undoIndex++;

            //Console.WriteLine("{0} Counter", undoCounter);
            //Console.WriteLine("{0} Index", undoIndex);
        }

        public static void undo()
        {


            if (undoCounter == 0)
            {
                Console.WriteLine("NO PREVIOUS STATES TO LOAD");
                return;
            }
            else if (undoIndex == 0)
            {
                if (undoCounter == 1)
                {
                    Console.WriteLine("SILLY BILLY, YOU ONLY HAVE ONE CHANGE STORED BUT TRIED TO UNDO TWICE");
                }
                Console.WriteLine("ALREADY LOADED OLDEST STATE");
                return;
            }



            DiagramModel[] SaveArray;
            SaveArray = MomentoSave.ToArray();
            undoIndex--;

            if (undoIndex == 0)
            {
                OverScreen = new List<ScreenModel> { };
                OverRelations = new List<SingleRelationsModel> { };
                Console.WriteLine("EMPTY STATE LOADED");
                return;
            }
            OverScreen = SaveArray[undoIndex-1].screen.ToList();
            OverRelations = SaveArray[undoIndex].relations.ToList();
            Console.WriteLine("PREVIOUS STATE LOADED");
            //Console.WriteLine("{0} Counter", undoCounter);
            //Console.WriteLine("{0} Index", undoIndex);
        }


        static void redo()
        {
            if (undoIndex == undoCounter)
            {
                Console.WriteLine("ALREADY LOADED MOST RECENT STATE");
                return;
            }
            undoIndex++;
            DiagramModel[] SaveArray;
            SaveArray = MomentoSave.ToArray();
            OverScreen = SaveArray[undoIndex-1].screen.ToList();
            OverRelations = SaveArray[undoIndex-1].relations.ToList();
            Console.WriteLine("SUBSEQUENT STATE LOADED");
            //Console.WriteLine("{0} Counter", undoCounter);
            //Console.WriteLine("{0} Index", undoIndex);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Signup(UserModel model)
        {
            string connectionString = "mongodb+srv://CShark:5wulj7CrF1FTBpwi@umldb.7hgm9e0.mongodb.net/?retryWrites=true&w=majority";
            string databaseName = "uml_db";
            string collectionName = "users";
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(databaseName);
            var collection = db.GetCollection<UserModel>(collectionName);
            var results = collection.Find(x => x.Username == model.Username).ToList();
            // if no existing usernames exist the user can have that username and creates the user in db
            if (results.Count == 0)
            {
                GLOBALuserModel = model; 
                collection.InsertOne(model);

                return 0;
            }
            else
            {
                //TempData["Message"] = "Username Already Taken";
                return 1;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static LoginModel Login(UserModel model)
        {
            // connect to db and get a list of users
            string connectionString = "mongodb+srv://CShark:5wulj7CrF1FTBpwi@umldb.7hgm9e0.mongodb.net/?retryWrites=true&w=majority";
            string databaseName = "uml_db";
            string collectionName = "users";
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(databaseName);
            var collection = db.GetCollection<UserModel>(collectionName);

            // Look through DB and give me a list of usernames that match what the user provided.
            List<UserModel> userList = collection.Find(x => x.Username == model.Username).ToList();

            //var userID = userList[0]._id;
            // Convert that list to json
            //var json = collection.Find(x => x.Username == model.Username).ToJson();
            var index = userList.Count;

            // We are guaranteed that we have unique usernames from signup 
            if ((userList.Count > 0) && (userList[0].Password == model.Password))
            {

                // Create LoginModel with user info.               
                var login = new LoginModel { status = 0, User = new UserModel { _id = userList[0]._id, Username = userList[0].Username, Password = userList[0].Password } };
                //ViewData["UserModel"] = user;
                //var client2 = new MongoClient(connectionString);
                //var db2 = client2.GetDatabase(databaseName);
                //var collection2 = db2.GetCollection<DiagramModel>("diagrams");
                //var results2 = collection2.Find(x => x.Username == userID).ToList();
                //ViewData["list"] = results2;
                GLOBALuserModel = login.User;
                return login;
            }
            if ((userList.Count > 0) && !(userList[0].Password == model.Password))
            {
                //TempData["Message"] = "Incorrect password";
                return new LoginModel { status = 1 };
            }
            else
            {
                //TempData["Message"] = "User not found.";
                return new LoginModel { status = 2 };
            }

        }
        public static void load()
        {
            /*
            string connectionString = "mongodb+srv://CShark:5wulj7CrF1FTBpwi@umldb.7hgm9e0.mongodb.net/?retryWrites=true&w=majority";
            var databaseName = "uml_db";
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(databaseName);
            var collection = db.GetCollection<DiagramModel>("diagrams");
            GLOBALuserModel.Diagrams = collection.Find(x => x.UserID == GLOBALuserModel._id).ToList();
            */
        }
    }
}
    