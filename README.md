# Minimal APIs Demo

This repo contains documentation and demo code for a presentation on .Net 6 minimal APIs.  This code is a work in progress (maybe) and experimental.  I make no promises that anything works.  Getting it running should be pretty simple.

Pre-requisites:
- Visual Studio 2022
- .Net 6 SDK

To run the application:
1. Clone the repo and open the solution in VS2022
2. The first run should spin up a Sqlite database in your project root and seed it with 30 MLB teams
3. Navigate to the swagger docs to test endpoints. You can verify the database was created by executing the /teams endpoint



# Resources
[Minimal APIs Overview](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-6.0)

[Tutorial: Create a minimal web API with ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/tutorials/min-web-api?view=aspnetcore-6.0&tabs=visual-studio)

[Damian Edwards - Minimal API Playground](https://github.com/DamianEdwards/MinimalApiPlayground)

[Hammad Abbasi - Minimal APIs in .Net 6 - A Complete Guide](https://medium.com/geekculture/minimal-apis-in-net-6-a-complete-guide-beginners-advanced-fd64f4da07f5)



# Concepts
[Records](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/types/records) - introduced in C# 9, Records are used in the **Asp.Net Core Web Api** project template to return weather forecasts.

[Implicit Global Usings](https://dotnetcoretutorials.com/2021/08/31/implicit-using-statements-in-net-6/) - global using statements were introduced in C# 10