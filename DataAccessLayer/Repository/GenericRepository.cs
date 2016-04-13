
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        #region Constants

        /// <summary>
        /// Exception Source: GenericRepository
        /// </summary>
        public const String EXCEPTION_SOURCE = "GenericRepository";

        #endregion

        #region Members and Properties

        private RentalCarsDBContext dbContext = null;

        /// <summary>
        /// Entity-Framework DBContext 'RentalCarsDBContext'
        /// </summary>
        protected RentalCarsDBContext DbContext
        {
            get
            {
                if (this.dbContext == null)
                    this.dbContext = new RentalCarsDBContext();

                return dbContext;
            }
        }

        #endregion

        #region Abstract CRUD Methods

        /// <summary>
        /// Create a new instance of the Entity T into the DbContext
        /// </summary>
        /// <param name="pEntity">Entity to Create</param>
        public T Create(T pEntity)
        {
            try
            {
                this.DbContext.Set<T>().Add(pEntity);
                this.DbContext.SaveChanges();
            }
            catch (Exception e)
            {
                EventLog.WriteEntry(EXCEPTION_SOURCE, e.Message, EventLogEntryType.Error);
            }
            return pEntity;
        }

        /// <summary>
        /// Delete the instance of the Entity T in the DbContext
        /// </summary>
        /// <param name="pEntity">Entity to Delete</param>
        public void Delete(T pEntity)
        {
            try
            {
                this.DbContext.Set<T>().Remove(pEntity);
                this.DbContext.SaveChanges();
            }
            catch (Exception e)
            {
                EventLog.WriteEntry(EXCEPTION_SOURCE, e.Message, EventLogEntryType.Error);
            }
        }

        /// <summary>
        /// Get the entire list of the Entity T selected
        /// </summary>
        public IEnumerable<T> GetAll()
        {
            return this.DbContext.Set<T>().ToList();
        }

        /// <summary>
        /// Get one instance of the selected Entity T by ID
        /// </summary>
        /// <param name="pID">Entity ID</param>
        public T GetByID(long pID)
        {
            T pEntity = null;

            try
            {
                pEntity = this.DbContext.Set<T>().Find(pID);
            }
            catch (Exception e)
            {
                EventLog.WriteEntry(EXCEPTION_SOURCE, e.Message, EventLogEntryType.Error);
            }
            return pEntity;
        }

        /// <summary>
        /// Update the Entity T in the DbContext
        /// </summary>
        /// <param name="pEntity">Entity to Update</param>
        public T Update(T pEntity)
        {
            try
            {
                this.DbContext.Entry<T>(pEntity).State = EntityState.Modified;
                this.DbContext.SaveChanges();
            }
            catch (Exception e)
            {
                EventLog.WriteEntry(EXCEPTION_SOURCE, e.Message, EventLogEntryType.Error);
            }
            return pEntity;
        }

        #endregion
    }
}
