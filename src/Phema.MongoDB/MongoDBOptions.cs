using System;
using System.Collections.Generic;

using MongoDB.Driver;

namespace Phema.MongoDB
{
	internal sealed class MongoDBOptions
	{
		public MongoDBOptions()
		{
			Databases = new Dictionary<string, Func<IServiceProvider, IMongoDatabase>>();
		}
		
		public IDictionary<string, Func<IServiceProvider, IMongoDatabase>> Databases { get; }
	}
}