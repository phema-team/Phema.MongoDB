# Phema.MongoDB

[![Nuget](https://img.shields.io/nuget/v/Phema.MongoDB.svg)](https://www.nuget.org/packages/Phema.MongoDB)

C# MongoDB driver wapper for `ASP.NET Core`

## Usage

```csharp
// Add
services.AddMongoDB()
  .AddDatabase("database",
    options => options.AddCollection<Model>("models"));

// Get
var models = provider.GetRequiredService<IMongoCollection<Model>>();

// Use
await models.InsertOneAsync(...);
```
