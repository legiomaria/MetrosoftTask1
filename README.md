# Requirement:
1. Visual Studio 2022.
2. Microsoft SQL Server.
3. Stable Internet Connection(due to the cdn for UI)


# Steps to run:
1. Clone the application from, https://github.com/legiomaria/MetrosoftTask1.git.
2.  Run the application.
3.  The application automatically runs it migrations, create relevant tables and seed the tables.
4. Rightclick on the solution name on Visual Studio.
5. Click on Properties.
6. Click on multiple startup projects.
7. On the WebApi dropdown, click on start.
8. On the MVC dropdown, click on start.
9. Click on Ok.
10. Then click on Start on Visual Studio to run the project. 

# ConnectionString:
 The connectionstring is found in the appsettings.json contained in the MetrosoftSearch.Api project and also
the Google Connectionstring. I suggest the server in the Connectionstring is replaced from the dot to that on your SSMS.
eg
Server=.;Database=MetroSoftTaskDb;TrustServerCertificate=true;Integrated Security=true;Trusted_Connection=True;
GoogleConnection : https://www.google.co.uk/search?num=100&q=

changed to server : YourServerName=.;Database=MetroSoftTaskDb;TrustServerCertificate=true;Integrated Security=true;Trusted_Connection=True; 
