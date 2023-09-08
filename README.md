# BlazorDemo_WebAPI
## Description
https://github.com/Alle299/BlazorDemo <br/>
This is a Visual Studio 2022, C# .net Core 6.0 solution too demonstrate Frontend / backend functionality. <br/>
This solution concists of two projects, a Blazor CRUD user interface project, styled with Boostrap<br/>
and a REST Web-Api backend project, powered by a MSSql database.

The UI is a stand alone Blazor service web applications user interface for the Swagger documented backend web-api. <br/>The API-calls are secured with JWT Authentication and the back end project implements code first database migration functionality and repository pattern architecture.<br />

## Technologies
This demo covers uses the following technologies.<br/>
.Net Core WebAPI, JWT Authentication, Swagger, Dapper, Database migration, Repository pattern, Blazor, Json, javascript, Bootstrap.

## Installation
You will need access to a MSSQL databse server with Windows authentication access to run this out of the box in visual studio debuge mode and that the appsettings.json has the correct information for establishing a connection to your MSSQL server. Also make sure that make sure Sql Named pipes are enabled in Sql Server Configuration Manager.

## Instructions
Load the project into Visual Studio 2022 or later and run in debug mode, if all is well, it will open the "BlazorDemo_BlazorUI" and the "WebAPi .Net Core with JWT authentication" pages.</br>

**Observe. When in debug mode, please wait till both pages are fully loaded before interacting.**

**To login use the CoreApi/_auth/login and select one of the pre set User accounts here.**

**Username:** AleWah<br/>
**Password:** 299792<br/>
<sub>This has the roles "User","Admin","Superadmin"</sub>


**Username:** AleWah2<br/>
**Password:** 299792<br/>
<sub>This has the roles "Admin"</sub>


**Username:** AleWah3<br/>
**Password:** 299792<br/>
<sub>This has the roles "User"</sub>

With a successful login, you can find the JWT token in the APIs response and then go to the very top right of the swager page, click the green "Authentication" button, type "Bearer ####" and paste in the JWT token in place of the ####.
Then click Authenticate and close the popup. THis will give you 20 min access to the role secured Api's</br>

You can test this login by testing the APIs mentioned abowe. These can olo be copied into a thirsd party application like Postman and thested there.

## Motivation
### When to Use Dapper
Undoubtedly the biggest advantage of using Dapper is the performance gain. In scenarios where high performance is required, Dapper may be the best choice. However, it must be considered that when using Dapper, a longer development period will be necessary, as it is necessary to write the SQL queries that will be executed by Dapper. In addition, it doesn’t have, at least natively, the option of automatic migrations as there is in EF Core—for example, requiring creations/changes of databases and tables to be done via SQL scripts.
Persisting Data.

### Why JWT
Instead of storing information on the server after authentication, JWT creates a JSON web token and encodes, sterilizes, and adds a signature with a secret key that cannot be tampered with. This key is then sent back to the browser. Each time a request is sent, it verifies and sends the response back.

The main difference here is that the user’s state is not stored on the server, as the state is instead stored inside the token on the client-side.

JWT also allows us to use the same JSON Web Token in multiple servers that you can run without running into problems where one server has a certain session, and the other server doesn’t.

Most modern web applications use JWT for authentication reasons like scalability and mobile device authentication.

## Easy to miss things when setting up a solution like this.
* Make sure sql Named pipes are enabled in Sql Server Configuration Manager.
* If the Swagger-page is not properly shown/documented, go to the projects Properties/Build make sure that the "generate a file for API documentration" is checked.
* If the Bootstrap doesn't work properly, make sure you linked to the newest verison and that is is abowe all other .css links. 

## Mayor/Minor updates.
2023-08-06 - Added UI Blazor project<br/>
2023-08-10 - Added improved swagger documentation for backend<br/>
2023-08-22 - Added multi color console logging for backend<br/>
2023-09-08 - New refactured codbae with fill Dapper implementation<br/>


