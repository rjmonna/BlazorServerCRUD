# DotNetDemo

A showcase of different modern .NET and web technologies.

* The main part of the application is still Blazor Server;
* Goal is to create some kind of notification service which checks the Azure Table Store and then trigger things.

## Publishing

* dotnet publish DotNetDemo.Web/DotNetDemo.Web.csproj -o ../DotNetDemo.Web
* dotnet publish DotNetDemo.Api/DotNetDemo.Api.csproj -o ../DotNetDemo.Api
* tar -cjvf DotnetDemo.tar.bz2 ../DotNetDemo.*
* scp -i ~/.ssh/key DotNetDemo.tar.bz2 user@host:
* rm DotNetDemo.tar.bz2