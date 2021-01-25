/*********************************************************************
 * 
 * JeopardyLibrary - BaseRepo class
 * 
 * This class contains the methods for performing database operations
 * using the Entity Framework - Add, Delete, Update, Get, Execute Query
 * 
 *********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using JeopardyLibrary.EF;
using JeopardyLibrary.Models.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace JeopardyLibrary.Repos
{
    /*********************************************************************
     * Reusable class for referencing database objects
     * *******************************************************************/
    public class BaseRepo<T> : IDisposable,IJeopardyRepo<T> where T:EntityBase, new()
    {
        private readonly DbSet<T> _table; // database table
        private readonly JeopardyContext _db; // database reference
        protected JeopardyContext Context => _db;

        public BaseRepo() : this(new JeopardyContext())
        {
        }

        /******************************************************************
         * Sets the Entity Framework database context and table references
         * ***************************************************************/
        public BaseRepo(JeopardyContext context)
        {
            _db = context;
            _table = _db.Set<T>();
        }

        public void Dispose()
        {
            _db?.Dispose();
        }

        /******************************************************************
         * Add a table entry to the database
         * ***************************************************************/
        public int Add(T entity)
        {
            _table.Add(entity);
            return SaveChanges();
        }

        /******************************************************************
         * Add a list of table entries to the database
         * ***************************************************************/
        public int Add(IList<T> entities)
        {
            _table.AddRange(entities);
            return SaveChanges();
        }

        /******************************************************************
         * Update a table entry in the database
         * ***************************************************************/
        public int Update(T entity)
        {
            _table.Update(entity);
            return SaveChanges();
        }

        /******************************************************************
         * Update a list of table entries in the database
         * ***************************************************************/
        public int Update(IList<T> entities)
        {
            _table.UpdateRange(entities);
            return SaveChanges();
        }

        /******************************************************************
         * Delete a table entry by id
         * ***************************************************************/
        public int Delete(int id)
        {
            _db.Entry(new T() { Id = id }).State = EntityState.Deleted;
            return SaveChanges();
        }

        /******************************************************************
         * Delete a referenced table entry
         * ***************************************************************/
        public int Delete(T entity)
        {
            _db.Entry(entity).State = EntityState.Deleted;
            return SaveChanges();
        }

        /******************************************************************
         * Find a table entry by id in the database
         * ***************************************************************/
        public T GetOne(int? id) => _table.Find(id);

        /******************************************************************
         * Find all the table entries in a particular table in the database
         * ***************************************************************/
        public virtual List<T> GetAll() => _table.ToList();

        /******************************************************************
         * Find an ordered list of table entries (ascending or descending)
         * ****************************************************************/
        public List<T> GetAll<TSortField>(Expression<Func<T, TSortField>> orderBy, bool ascending)
            => (ascending ? _table.OrderBy(orderBy) : _table.OrderByDescending(orderBy)).ToList();
        
        /******************************************************************
         * Find a conditional list of table entries
         * ***************************************************************/
        public List<T> GetSome(Expression<Func<T, bool>> where)
            => _table.Where(where).ToList();

        /******************************************************************
         * Execute a Query to the table in the database
         * ***************************************************************/
        public List<T> ExecuteQuery(string sql) => _table.FromSqlRaw(sql).ToList();
        public List<T> ExecuteQuery(string sql, object[] sqlParameterObjects) =>
            _table.FromSqlRaw(sql, sqlParameterObjects).ToList();

        /******************************************************************
         * Save changes to the database
         * ***************************************************************/
        internal int SaveChanges()
        {
            try
            {
                return _db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine("(BaseRepo-SaveChanges) Error saving changes to database");
                throw;
            }
        }
    }
}
