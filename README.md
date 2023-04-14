# Plandit
Plandit is a typical Todo Task application but with the ability to create multiple different Todo lists to represent your projects. The application is built using C#, ASP .NET, Python, and JavaScript. It uses Python to serialise and analyse data on a JSON file, which is then extracted with C# for a JavaScript front end. Conversly, information entered from the JavaScript front end goes through the C# controller for the Python to handle.

## Technologies
List of technologies or frameworks used in the project.
- C#
- .NET MAUI
- SQLite

## Installation
Steps to install the project:

1. Clone the repository
```
git clone https://github.com/ConnorEasterbrook/Plandit.git
```

2. Install dependencies if necessary
```
dotnet restore
```

## How to Run
If your IDE is unable to run the project with debug, use the steps below.

Steps to run the project:
1. Run the command
```
dotnet run
```

2. Open the browser and navigate to the localhost mentioned in output.

## Usage
Usage on this project is very simple. Press the button in the bottom right and enter your project details. You can then access the project by clicking on it and adding tasks through the same method.

## License
This project is licensed under the BSD 3-Clause License - see the LICENSE.md file for details.
