using VRFEngine.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace VRFEngine.Repository.Interface
{
    public interface ICRUDRepository
    {
        /// <summary>
        /// Gets all the elements.
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <returns>List of elements</returns>
        IEnumerable<T> GetAll<T>() where T : ModelBase;

        /// <summary>
        /// Finds one element based on the Id.
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="id">Id of the element</param>
        /// <returns>One or null element</returns>
        T Find<T>(Guid id) where T : ModelBase;

        /// <summary>
        /// Finds one element (with the given includes) based on the Id.
        /// </summary>
        /// <param name="id">The id of the element to find</param>
        /// <param name="listNavigationPropertyPath">The given includes</param>
        /// <returns>One element (with the given includes) based on the Id.</returns>
        T Find<T>(Guid id, params Expression<Func<T, ModelBase>>[] listNavigationPropertyPath) where T : ModelBase;

        /// <summary>
        /// Updates one element.
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="t">Entity to update</param>
        /// <returns>Updated entity</returns>
        T Update<T>(T t) where T : ModelBase;

        /// <summary>
        /// Creates one element.
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="t">Entity to create</param>
        /// <returns>Created entity</returns>
        T Create<T>(T t) where T : ModelBase;

        /// <summary>
        /// Return true if the entity with the given <paramref name="id"/> exists. False otherwise.
        /// </summary>
        /// <returns>True if the entity with the given <paramref name="id"/> exists. False otherwise.</returns>
        bool Exists<T>(Guid id) where T : ModelBase;

        //bool IsUnique<T>(Func<T, bool> predicate) where T : ModelBase;

        /// <summary>
        /// Searches for multiple elements.
        /// Used by autocomplete.
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="predicate">Predicate for the search condition.</param>
        /// <returns>List of elements</returns>
        IEnumerable<T> Search<T>(Func<T, bool> predicate) where T : ModelBase;

    }
}