using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace JobsDataModel
{
    public class JobsDBContext: DbContext
    {
        private JobsEntities _context;

        public JobsDBContext() 
        { 
            _context = JobsEntities.NewJobsEntities; 
        }

        #region Public Generic Repository methods
        /// <summary>  
        /// Generic method to fetch all the records from db  
        /// </summary>  
        /// <returns></returns>  
        public virtual IEnumerable<TEntity> GetAll<TEntity>() where TEntity : class
        {
            return _context.Set<TEntity>().ToList();
        }

        /// <summary>  
        /// Generic method to fetch record based on id  
        /// </summary>  
        /// <param name="id"></param>  
        /// <returns></returns>  
        public virtual TEntity GetByID<TEntity>(object id) where TEntity : class
        {
            return _context.Set<TEntity>().Find(id);
        }

        /// <summary>  
        /// Generic Insert method for the entities  
        /// </summary>  
        /// <param name="entity"></param> 
        public virtual void Insert<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
        }

        /// <summary>  
        /// Generic update method for the entities  
        /// </summary>  
        /// <param name="entityToUpdate"></param>  
        public virtual void Update<TEntity>(TEntity entityToUpdate) where TEntity : class
        {
            _context.Set<TEntity>().Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
            _context.SaveChanges();
        }

        /// <summary>  
        /// Generic method to check if entity exists  
        /// </summary>  
        /// <param name="primaryKey"></param>  
        /// <returns></returns>  
        public bool Exists<TEntity>(object primaryKey) where TEntity : class
        {
            return _context.Set<TEntity>().Find(primaryKey) != null;
        }

        //public virtual IEnumerable<TEntity> GetBySearchString<TEntity>(string searchString) where TEntity : class
        //{
        //    var result = _context.Set<TEntity>().Where(s => s.)
        //}
        #endregion
    }
}
