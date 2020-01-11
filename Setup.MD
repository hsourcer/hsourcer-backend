
## Getting Started
Use these instructions to get the project up and running.

### Prerequisites
You will need the following tools:

* [Visual Studio Code or 2017](https://www.visualstudio.com/downloads/)
* [.NET Core SDK 2.2](https://www.microsoft.com/net/download/dotnet-core/2.2)

### Setup
Follow these steps to get your development environment set up:
  0. After renaming the project with powershell scripts.It seems that it messed up encoding for some files e.g:
	-gitignore, -it wasn't working at all.
	-HSourcer.WebUI/wwroot/api/specification.json - it messed up the Swagger.

  1. Clone the repository
  2. At the root directory, restore required packages by running:
     ```
     dotnet restore
     ```
  3. Next, build the solution by running:
     ```
     dotnet build
     ```
  4. Next, within the `HSourcer.WebUI\ClientApp` directory, launch the front end by running:
     ```
     npm start
     ```
  5. Once the front end has started, within the `HSourcer.WebUI` directory, launch the back end by running:
     ```
     dotnet run
     ```
  Launch [http://localhost:52468/](http://localhost:52468/) in your browser to view the Web UI
  Launch [http://localhost:52468/swagger](http://localhost:52468/api) for swagger
  
## Technologies
* .NET Core 2.2
* .NET Core Identity 2.2
* Entity Framework Core 2.2
