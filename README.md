# Phema.MongoDB
C# MongoDB driver wapper for AspNetCore

```csharp
services.AddPhemaMongoDB(settings => {})
  .AddDatabase("database", options =>
    options.AddCollection<Model>("models", settings => {}),
    settings => {});

var models = provider.GetRequiredService<IMongoCollection<Model>>();

await models.InsertOneAsync(...);
```
