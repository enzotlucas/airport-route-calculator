# Airport route calculator
A system that allows you to create plane routes and calculate the best cost one

## Preparing the environment
Follow the steps to be able to run the application.

### Visual Studio
If you are using Visual Studio, execute the commands below on the Package Manager Console. </br>
To open the console, go to Tools > Library Package Manager > Package Manager Console.
Select the Infrastructure project, then run the command bellow:

```bash
# PowerShell:
Update-Database
```
Then, you are able to run the project.

-------------------------------------------------------------------

### Visual Studio Code
If you are using Visual Studio Code, execute the commands below on the terminal.
```bash
# .Net CLI:
dotnet ef database update
```
Then, you are able to run using the command:
```bash
dotnet run --project Airport.RouteCalculator.API
```

The project will open the Swagger on localhost:7275/swagger/index.html