// <copyright file="MongoMapper.cs" company="SHHH Innovations">
// Copyright ©  2013
// </copyright>

namespace SHHH.Infrastructure.MongoDB
{
    using System;
    using global::MongoDB.Bson.Serialization;
    using global::MongoDB.Driver;

    /// <summary>
    /// Maps the domain objects and sets up the indexes for the database.
    /// </summary>
    public abstract class MongoMapper
    {
        /// <summary>
        /// Gets the database.
        /// </summary>
        /// <returns>The current <see cref="MongoDatabase"/>.</returns>
        public static MongoDatabase Database { get; private set; }

        /// <summary>
        /// Connects the database.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public static void ConnectDatabase(string connectionString)
        {
            var url = new MongoUrl(connectionString);
            var client = new MongoClient(url);
            MongoMapper.Database = client.GetServer().GetDatabase(url.DatabaseName);
        }

        /// <summary>
        /// Configures the database.
        /// </summary>
        public virtual void ConfigureDatabase()
        {
        }

        /// <summary>
        /// Registers the class map.
        /// </summary>
        /// <typeparam name="TClass">The type of the class.</typeparam>
        /// <param name="classMapInitializer">The class map initializer.</param>
        protected void RegisterClassMap<TClass>(Action<BsonClassMap<TClass>> classMapInitializer)
        {
            if (BsonClassMap.IsClassMapRegistered(typeof(TClass)))
            {
                return;
            }

            BsonClassMap<TClass>.RegisterClassMap(classMapInitializer);
        }
    }
}
