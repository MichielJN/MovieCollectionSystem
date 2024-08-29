using MovieCollectionSystem.abstractions;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCollectionSystem.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : TableData, new()
    {
        SQLiteConnection connection;
        public string StatusMessage { get; set; }

        public BaseRepository() 
        {
            connection = new SQLiteConnection(Constants.DatabasePath, Constants.flags);
            connection.CreateTable<T>();
        }

        public void SaveEntity(T entity)
        {
            int result = 0;
            try
            {
                if (entity.Id == 0)
                {
                    result = connection.Insert(entity);
                    StatusMessage = $"{result} row(s) added";
                }
                else
                {
                    result = connection.Update(entity);
                    StatusMessage = $"{result} row(s) updated";
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }
        }

        public void SaveEntityWithChildren(T entity)
        {
            try
            {
                if (entity.Id == 0)
                {
                    connection.InsertWithChildren(entity, true);
                }
                else
                {
                    connection.UpdateWithChildren(entity);
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }
        }

        public T? GetEntity(int id)
        {
            try
            {
                return connection.Table<T>().FirstOrDefault(x => x.Id == id);
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }
            return null;
        }

        public T? GetEntityWithChildren(int id)
        {
            try
            {
                return connection.GetAllWithChildren<T>().FirstOrDefault(x => x.Id == id);
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }
            return null;
        }

        public List<T>? GetEntities()
        {
            try
            {
                return connection.Table<T>().ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }
            return null;
        }

        public List<T>? GetEntitiesWithChildren()
        {
            try
            {
                List<T> entities = connection.GetAllWithChildren<T>().ToList();
                return connection.GetAllWithChildren<T>().ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }
            return null;
        }

        public void DeleteEntity(T entity)
        {
            try
            {
                connection.Delete(entity);
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }
        }

        public void DeleteEntityWithChildren(T entity)
        {
            try
            {
                connection.Delete(entity, true);
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }
        }

        public void Dispose()
        {
            connection.Dispose();
        }
    }
}
