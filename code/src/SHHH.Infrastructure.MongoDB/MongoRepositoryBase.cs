// <copyright file="MongoRepositoryBase.cs" company="SHHH Innovations">
// Copyright ©  2013
// </copyright>

namespace SHHH.Infrastructure.MongoDB
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using global::MongoDB.Driver;
    using global::MongoDB.Driver.Linq;

    /// <summary>
    /// Sets up some base functionality for a repository class
    /// </summary>
    /// <typeparam name="T">The entity the repository works with</typeparam>
    public class MongoRepositoryBase<T>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="MongoRepositoryBase{T}" /> class.
        /// </summary>
        /// <param name="collectionName">Name of the collection.</param>
        protected MongoRepositoryBase(string collectionName)
        {
            this.CollectionName = collectionName;
        }
        #endregion

        #region Private Fields
        /// <summary>
        /// Gets or sets the collection name
        /// </summary>
        private string CollectionName { get; set; }
        #endregion

        #region Public Methods
        /// <summary>
        /// Attaches the specified objects.
        /// </summary>
        /// <param name="objects">The objects to attach.</param>
        /// <exception cref="System.InvalidOperationException">Thrown when the save fails</exception>
        public void Attach(params T[] objects)
        {
            try
            {
                foreach (var item in objects)
                {
                    this.GetCollection().Save(item);
                }
            }
            catch (WriteConcernException x)
            {
                throw new InvalidOperationException(x.Message, x);
            }
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Gets the collection.
        /// </summary>
        /// <returns>The <see cref="MongoCollection"/> for <see cref="MongoRepositoryBase{T}"/> objects</returns>
        protected MongoCollection GetCollection()
        {
            return MongoMapper.Database.GetCollection<T>(this.CollectionName);
        }

        /// <summary>
        /// Gets the queryable collection.
        /// </summary>
        /// <returns>An <see cref="IQueryable{T}"/>.</returns>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
        protected IQueryable<T> GetQueryable()
        {
            return this.GetCollection().AsQueryable<T>();
        }
        #endregion
    }
}
