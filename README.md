![C-Sharks Logo](/UML/wwwroot/media/C-Sharks_logo.jpg)

# C-Sharks UML Editor Web App

Team C-Sharks' UML Editor is an application that allows the user to add, delete, and edit UML classes all through its web GUI. Each UML class can hold different attributes and relationships between each other, all of which can be added, deleted, and/or renamed. The editor can also save and load diagrams to be worked on later. Written in C# (Backend) and JavaScript (Frontend)

**Prerequisites**

[Visual Studio Community 2022](https://visualstudio.microsoft.com/vs/community/)

## Running the Application

1. In Visual Studio, navigate to File > Clone Repository and enter the link: <br> (https://github.com/mucsci-students/2022fa-420-C-Sharks)

2. After cloning the repository, open it as a project by expanding the UML folder and double clicking UML.sln

3. Once the project is open you can click the green play button ("UML") to run the program.

** Help Menu: **

## CLI Mode:

To launch the program in CLI mode, visit the 'CLI' page on the web GUI, and download the executable file. This program will run locally and offline, and you will be able to perform each one of the UML editor's functions through the command-line. The commands for this program are listed below:

### Commands:

- _Help_: Displays a list of commands with brief explanations.

- _Add_Class_: Creates a new class if name is valid. Input a name after the prompt.

- _Add_field_: Add a field to a class. Input the class name after the prompt, the input name. And type after the prompt for each.

- _Add_Meth_: Add a method to a class. Input the class name after the prompt, the input name, return type, and enter parameters, after the prompt for each.
  When you have no parameters, or none are left, enter 'N'.

- _relat_: Add a relation between classes. Input the 'to' class name, 'from' class name, and the relation type after their prompt.

- _Rem_Class_: Remove a class. Input an name after the prompt.

- _Rem_field_: Remove a field from a class. Input the class name and field name after their prompt.

- _Rem_Meth_: Remove a method from a class. Input the class name and method name after their prompt.

- _Rem_relat_: Remove a relation between classes. Input the to class name, and from class name after their prompt.

- _Mod_Class_: Modify a class name. Input a new name after the prompt.

- _Mod_field_: modify a field from a class. Input the to class name, input the new field name and new type name after their prompt.

- _Mod_Meth_: Remove a method from a class. Input the to class name, input the new method name and return type name after their prompt.

- _Mod_relat_: Modify a relation between classes. Input the to class name, and the new type.

- _List_classes_: Lists all classes currently added.

- _List_class_: Lists all fields and methods of a given class. Input the unique class name after the prompt.

- _List_relat_: Lists the relations currently added.

- _import_: Load a UML model from a JSON file.

- _export_: Saves a UML as a JSON file locally.

## Development Team

[Logan Eller](https://github.com/logan-eller) <br>

[Trevor Foresta](https://github.com/trevforesta) <br>

[Galen Hahn](https://github.com/Alfather-Bear) <br>

[Ben Schaeffer](https://github.com/Tactical12YearOld) <br>

[Jacob Smith](https://github.com/jdsmithmv)
