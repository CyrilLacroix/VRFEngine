using VRFEngine.Data;
using VRFEngine.Model;
using VRFEngine.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace VRFEngine.Repository.Implementation
{
    public class CRUDRepository : ICRUDRepository
    {
        protected readonly DataContext Context;
        protected readonly ILoggerService Logger;
        protected readonly DateTime Now = DateTime.Now;

        protected readonly string User;

        public CRUDRepository(DataContext dataContext, ILoggerService loggerService, IHttpContextAccessor httpContext)
        {
            Context = dataContext;
            Logger = loggerService;
            User = httpContext.HttpContext.User?.Identity?.Name;
        }

        /// <summary>
        /// Gets all the elements.
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <returns>List of elements</returns>
        public IEnumerable<T> GetAll<T>() where T : ModelBase
        {
            Logger.Init(nameof(CRUDRepository), nameof(GetAll), User);
            Logger.Info("Start");

            try
            {
                IEnumerable<T> ts = Context.Set<T>().AsEnumerable();

                Logger.Info("Success");
                return ts;
            }
            catch (Exception exception)
            {
                Logger.Error("Failure", exception);
                throw exception;
            }
        }

        /// <summary>
        /// Finds one element based on the Id.
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="id">Id of the element</param>
        /// <returns>One or null element</returns>
        public T Find<T>(Guid id) where T : ModelBase
        {
            Logger.Init(nameof(CRUDRepository), nameof(Find), User);
            Logger.Info("Start");

            try
            {
                T t = Context.Set<T>().Find(id);

                Logger.Info("Success");
                return t;
            }
            catch (Exception exception)
            {
                Logger.Error("Failure", exception);
                throw exception;
            }
        }

        /// <summary>
        /// Finds one element (with the given includes) based on the Id.
        /// </summary>
        /// <param name="id">The id of the element to find</param>
        /// <param name="listNavigationPropertyPath">The given includes</param>
        /// <returns>One element (with the given includes) based on the Id.</returns>
        public virtual T Find<T>(Guid id, params Expression<Func<T, ModelBase>>[] listNavigationPropertyPath) where T : ModelBase
        {
            Logger.Init(nameof(CRUDRepository), nameof(Find), User);
            Logger.Info("Start");

            try
            {
                Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<T, ModelBase> previousInclude = Context.Set<T>().Include(listNavigationPropertyPath[0]);

                if (listNavigationPropertyPath.Length > 1)
                {
                    for (int i = 1; i < listNavigationPropertyPath.Length; i++)
                    {
                        previousInclude = previousInclude.Include(listNavigationPropertyPath[i]);
                    }
                }

                Logger.Info("Success");
                return previousInclude.Single(x => x.Id == id);
            }
            catch (Exception exception)
            {
                Logger.Error("Failure", exception);
                throw exception;
            }
        }

        /// <summary>
        /// Updates one element.
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="t">Entity to update</param>
        /// <returns>Updated entity</returns>
        public T Update<T>(T t) where T : ModelBase
        {
            Logger.Init(nameof(CRUDRepository), nameof(Update), User);
            Logger.Info("Start");

            try
            {
                t.Modified = Now;
                t.ModifiedBy = User;

                EntityEntry<T> entry = Context.Entry<T>(t);
                entry.State = EntityState.Modified;
                Context.Entry(t).Property("Created").IsModified = false;
                Context.Entry(t).Property("CreatedBy").IsModified = false;

                Context.SaveChanges();
                entry.State = EntityState.Detached;

                Logger.Info("Success");
                return t;
            }
            catch (Exception exception)
            {
                Logger.Error("Failure", exception);
                throw exception;
            }
        }

        /// <summary>
        /// Creates one element.
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="t">Entity to create</param>
        /// <returns>Created entity</returns>
        public T Create<T>(T t) where T : ModelBase
        {
            Logger.Init(nameof(CRUDRepository), nameof(Create), User);
            Logger.Info("Start");

            try
            {
                t.Created = Now;
                t.Modified = Now;
                t.CreatedBy = User;
                t.ModifiedBy = User;

                EntityEntry<T> entry = Context.Set<T>().Add(t);

                Context.SaveChanges();
                entry.State = EntityState.Detached;

                Logger.Info("Success");
                return entry.Entity;
            }
            catch (Exception exception)
            {
                Logger.Error("Failure", exception);
                throw exception;
            }
        }

        /// <summary>
        /// Return true if the entity with the given <paramref name="id"/> exists. False otherwise.
        /// </summary>
        /// <returns>True if the entity with the given <paramref name="id"/> exists. False otherwise.</returns>
        public bool Exists<T>(Guid id) where T : ModelBase
        {
            Logger.Init(nameof(CRUDRepository), nameof(Exists), User);
            Logger.Info("Start");

            try
            {
                bool result = Context.Set<T>().Any(entity => entity.Id == id);
                Logger.Info("Success");
                return result;
            }
            catch (Exception exception)
            {
                Logger.Error("Failure", exception);
                throw exception;
            }
        }

        //public bool IsUnique<T>(Func<T, bool> predicate) where T : ModelBase
        //{
        //    Logger.Init(nameof(CRUDRepository), nameof(IsUnique), User);
        //    Logger.Info("Start");

        //    try
        //    {
        //        bool result = !Context.Set<T>().Any(predicate);
        //        Logger.Info("Success");
        //        return result;
        //    }
        //    catch (Exception exception)
        //    {
        //        Logger.Error("Failure", exception);
        //        throw exception;
        //    }
        //}

        /// <summary>
        /// Searches for multiple elements.
        /// Used by autocomplete.
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="predicate">Predicate for the search condition.</param>
        /// <returns>List of elements</returns>
        public IEnumerable<T> Search<T>(Func<T, bool> predicate) where T : ModelBase
        {
            Logger.Init(nameof(CRUDRepository), nameof(Search), User);
            Logger.Info("Start");

            try
            {
                IEnumerable<T> results = Context.Set<T>().Where(predicate);
                Logger.Info("Success");
                return results;
            }
            catch (Exception exception)
            {
                Logger.Error("Failure", exception);
                throw exception;
            }
        }
    }
}
