
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public abstract class GenericRepository<T> : IGenericRepository<T>, IDisposable where T : class
    {
        #region Constants

        /// <summary>
        /// Exception Source: GenericRepository
        /// </summary>
        public const String EXCEPTION_SOURCE = "GenericRepository";

        #endregion

        #region DataContext

        /// <summary>
        /// Entity Framework: RentalCars DB Context
        /// </summary>
        private RentalCarsDBContext rentalCarsDBContext;

        /// <summary>
        /// Contains the Entity Framework DBContext.
        /// </summary>
        protected RentalCarsDBContext RentalCarsDBContext
        {
            get
            {
                if (rentalCarsDBContext == null)
                    rentalCarsDBContext = new RentalCarsDBContext();

                return this.rentalCarsDBContext;
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
                this.RentalCarsDBContext.Set<T>().Add(pEntity);
                this.RentalCarsDBContext.SaveChanges();
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
                this.RentalCarsDBContext.Set<T>().Remove(pEntity);
                this.RentalCarsDBContext.SaveChanges();
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
            return this.RentalCarsDBContext.Set<T>().ToList();
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
                pEntity = this.RentalCarsDBContext.Set<T>().Find(pID);
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
                this.RentalCarsDBContext.Entry<T>(pEntity).State = EntityState.Modified;
                this.RentalCarsDBContext.SaveChanges();
            }
            catch (Exception e)
            {
                EventLog.WriteEntry(EXCEPTION_SOURCE, e.Message, EventLogEntryType.Error);
            }
            return pEntity;
        }

        #region IDisposable Support

        private bool disposed = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                    this.RentalCarsDBContext.Dispose();
                
                disposed = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        #endregion

        #endregion
    }
}
