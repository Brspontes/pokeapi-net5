<center><h1>API With .Net 5 - For Tests </h1></center>

<p align="center">
<img src="https://user-images.githubusercontent.com/14333695/110218879-e19a9600-7e9a-11eb-9315-aa79a9be8817.png" width="100" />
</p>

<p align="justify">
This API is development with .NET 5,  to test the new features of the application.

This first feature we have two gets methods that perform consultation on the https://pokeapi.co/, this methods
these methods return basic information for a simple pokedex, the api is still in development
</p>


<h2>Technologies</h2>
<ol>
	<li><a href="https://docs.microsoft.com/pt-br/dotnet/core/dotnet-five">.NET 5</a></li>
	<li><a href="https://automapper.org/">Auto Mapper</a></li>
	<li><a href="https://docs.microsoft.com/pt-br/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-5.0&tabs=visual-studio">Swagger</a></li>
	<li><a href="https://restsharp.dev/">Restsharp</a></li>
</ol>

<h2>Usage</h2>
First clonning repository using

```bash
git clone https://github.com/Brspontes/pokeapi-net5.git

cd pokeapi-net5
```
<br />
In current folder, before compilation, restore packages with

```bash
dotnet restore
```
Go to folder
```bash
cd src/Pokemon-Api

dotnet run

info: Microsoft.Hosting.Lifetime[0]
      Now listening on: https://localhost:5001
info: Microsoft.Hosting.Lifetime[0]
      Now listening on: http://localhost:5000
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Development
info: Microsoft.Hosting.Lifetime[0]
      Content root path: E:\GIT\api-pokenon-net5\src\Pokemon-Api
```

Go to:  https://localhost:5001/swagger

Swagger opnend

![swagger](https://user-images.githubusercontent.com/14333695/110219452-1c51fd80-7e9e-11eb-841d-724b2c2aee58.png)
<h2>License</h2>
<a href="https://github.com/herbsjs/herbs2gql/blob/master/LICENSE">MIT</a>
