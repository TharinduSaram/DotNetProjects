# DotNetProjectWithEFCore
#This project is sample .Net Core 6.0 WebAPI project, which used Entity Framework core and Scaffolding Database methods to create a API project
#Steps :
#Add Connection String  :
#Server=localhost;Database=Northwind;Uid=root;Pwd=******

#Packages to install :
#dotnet add package Microsoft.EntityFrameworkCore.Design
#dotnet add package Pomelo.EntityFrameworkCore.MySql

#Install & Update dotnet EF tool :
#dotnet tool install --global dotnet-ef
#dotnet tool update --global dotnet-ef

#Scaffold MySQL Database :
#dotnet ef dbcontext scaffold Name=NorthwindDB Pomelo.EntityFrameworkCore.MySql --output-dir Models --context-dir Data --namespace Northwind.Models --context-namespace Northwind.Data --context NorthwindContext -f --#no-onconfiguringdotnet 
#add package Microsoft.AspNetCore.Mvc.NewtonsoftJson
