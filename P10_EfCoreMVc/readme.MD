# Download nugget packages by right clicking on dependencies 

        dotnet add package Microsoft.EntityFrameworkCore --version 9.0.1
        dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 9.0.1
        dotnet add package Microsoft.EntityFrameworkCore.Tools --version 9.0.1
        dotnet add package CloudinaryDotNet --version 1.27.2
        dotnet add package System.IdentityModel.Tokens.Jwt --version 8.3.1
        dotnet add package dotenv.net --version 3.2.1
        dotnet add package BCrypt.Net-Next --version 4.0.3





# Installing EfCore tools globally 

	dotnet tool install --global dotnet-ef


# To check the list of tools installed globally 
        dotnet tool list --global

# Commands for Efcore sql migrations for vscode if using dotnet tool

        dotnet ef migrations add "message"
        dotnet ef database update



# Commands for Efcore sql migrations 

 use this command in nuget package console 

         Add-Migration "message"  

        

 // for adding migrations ..after successfully using this command you 
will be able to see migrations folder into the solution explorer.....but still no data base will
 be updated unlesss u run the second command  which is as 


         Update-Database
        




# if there are more than 1 db context in datafolder then 

        Add-Migration "message"  -Context "dbContextName"

after this if u will check the server explorer you will see data base updated and new tables formed and
datatyes autommatically given to the columns 




#  command for creating a unique id in c sharp interactive


         Console.WriteLine (Guid.NewGuid())




