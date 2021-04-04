# Commander API

This API will allow one to add command line "commands" and then use WebAPI to fetch them. It is quite useful when one can not remember all command line arguments. Sometimes it becomes difficult to find those on google as well. One can make whole repository of their command line argument.

Example - 

    {
        "id": 1,
        "howTo": "How to create migration in .net core",
        "line": "dotnet ef migrations add <Name of Migration>"
    }

Make sure to change the connectionstring with your creds in appsettings.json file.

"ConnectionStrings": 
  {
    "CommanderConnection" : "Server=localhost;Initial Catalog=WebApi; ID=<INSERT YOUR USER ID>;Password=<INSERT YOUR PASSWORD>;"
  }
