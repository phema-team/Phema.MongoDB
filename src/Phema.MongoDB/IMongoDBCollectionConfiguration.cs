using System;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

using MongoDB.Driver;

namespace Phema.MongoDB
{
	public interface IMongoDBCollectionConfiguration
	{
		IMongoDBCollectionConfiguration AddCollection<TEntity>(
			string collection,
			Action<MongoCollectionSettings> options = null);
	}
	
	internal sealed class MongoDBColletionConfiguration : IMongoDBCollectionConfiguration
	{
		private readonly IServiceCollection services;
		private readonly string database;

		public MongoDBColletionConfiguration(IServiceCollection services, string database)
		{
			this.services = services;
			this.database = database;
		}

		public IMongoDBCollectionConfiguration AddCollection<TEntity>(
			string collection,
			Action<MongoCollectionSettings> options = null)
		{
			var settings = new MongoCollectionSettings();
			
			options?.Invoke(settings);
			
			services.TryAddScoped(sp =>
			{
				var factory = sp.GetRequiredService<IOptions<MongoDBOptions>>()
					.Value
					.Databases[database];

				return factory(sp).GetCollection<TEntity>(collection, settings);
			});
			
			return this;
		}
	}
}