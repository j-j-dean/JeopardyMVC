/*********************************************************************
 * 
 * JeopardyLibrary - IJeopardyRepo interface
 * 
 * This class defines the methods for interfacing with the Entity 
 * Framework to perform database operations - Add, Delete, Update, 
 * Get, Execute Query
 * 
 *********************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace JeopardyLibrary.Repos
{
    /******************************************************************
     * The interface for interacting with the Jeopardy database
     * ***************************************************************/
    public interface IJeopardyRepo<T>
    {
        int Add(T entity);
        int Add(IList<T> entities);
        int Update(T entity);
        int Update(IList<T> entities);
        int Delete(int id);
        int Delete(T entity);
        T GetOne(int? id);
        List<T> GetSome(Expression<Func<T, bool>> where);
        List<T> GetAll();
        List<T> GetAll<TSortField>(Expression<Func<T, TSortField>> orderBy, bool ascending);
        List<T> ExecuteQuery(string sql);
        List<T> ExecuteQuery(string sql, object[] sqlParametersObjects);
    }
}
