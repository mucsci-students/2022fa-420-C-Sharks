
# C-Sharks UML Editor Web App

Team C-Sharks' UML Editor is an application that allows the user to add, delete, and edit UML classes all through its web GUI. Each UML class can hold different attributes and relationships between each other, all of which can be added, deleted, and/or renamed. The editor can also save and load diagrams to be worked on later.

**Prerequisites**
[Visual Studio Community 2022](https://visualstudio.microsoft.com/vs/community/)

## Running the Application

1. In Visual Studio, navigate to File > Clone Repository and enter the link: <br> (https://github.com/mucsci-students/2022fa-420-C-Sharks)
2. After cloning the repository, open it as a project by expanding the UML folder and double clicking UML.sln
3. Once the project is open you can click the green play button ("UML") to run the program.

**Saving / Loading Files**
To save or load a diagram, use the Save and Load icons on the toolbar to do so through the web server. Once you click the save button in the toolbar text is generated in the first text box reflecting your changes. To save your changes to the server you need to copy the output to the bottom text box then click the save to server button. Alternatively, if you'd like to save your diagram locally, use the "Export" option in the _Help_ menu, and "Import" option to load a local file.

## Design Patterns
The UML Editor uses the following design patterns:
- **Model-View-Controller**: The structure of the UML Editor uses the MVC design pattern to separate the model of the UML diagram, the view of the web-GUI/CLI window, and the controller class which is responsible for handling all of the user input and updating the model and view accordingly.

- **Memento**: For undo and redo functionality, each time an action is made, a state is created. Undo will go backwards to the previous state of the of the diagram in the UML Editor, and Redo will move forward to the next state after using Undo.


- **Command/Parameterization**: In the CLI version of the UML Editor, the Command design pattern is used to parse user input. The Commands reads in the keywords given by the user and calls the appropriate function related to that command. This is done along with parameters, which are used to pass in the arguments given by the user to the function.

## Development Team

[Logan Eller](https://github.com/logan-eller) <br>
[Trevor Foresta](https://github.com/trevforesta) <br>
[Galen Hahn](https://github.com/Alfather-Bear) <br>
[Ben Schaeffer](https://github.com/Tactical12YearOld) <br>
[Jacob Smith](https://github.com/jdsmithmv)