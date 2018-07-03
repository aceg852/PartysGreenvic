namespace PartysGreenvic.Helpers
{
    using Interfaces;
    using Models;
    using SQLite.Net;
    using SQLiteNetExtensions.Extensions;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Xamarin.Forms;
    class DataAccess : IDisposable
    {
        private SQLiteConnection connection;
        public DataAccess()
        {
            var config = DependencyService.Get<IConfig>();
            this.connection = new SQLiteConnection(config.Platform, Path.Combine(config.DirectoryBD, "PartysGreenvic.db3"));
            connection.CreateTable<UserLocal>();
        }
        public void InsertarEmpleado(Empleado empleado)
        {
            this.connection.Insert(empleado);
        }
        public void BorrarEmpleado(Empleado empleado)
        {
            this.connection.Delete(empleado);
        }
        public void ActualizarEmpleado(Empleado empleado)
        {
            this.connection.Update(empleado);
        }
        public Empleado GetEmpleado(int ID)
        {
            return connection.Table<Empleado>().FirstOrDefault(c => c.ID == ID);
        }
        public List<Empleado> GetEmpleados()
        {
            return connection.Table<Empleado>().OrderBy(c => c.Nombre).ToList();
        }
       









        public void Insert<T>(T model)
        {
            this.connection.Insert(model);
        }
        public void Update<T>(T model)
        {
            this.connection.Update(model);
        }
        public void Delete<T>(T model)
        {
            this.connection.Delete(model);
        }
        public T First<T>(bool WithChildren) where T : class
        {
            if (WithChildren)
            {
                return connection.GetAllWithChildren<T>().FirstOrDefault();
            }
            else
            {
                return connection.Table<T>().FirstOrDefault();
            }
        }
        public List<T> GetList<T>(bool WithChildren) where T : class
        {
            if (WithChildren)
            {
                return connection.GetAllWithChildren<T>().ToList();
            }
            else
            {
                return connection.Table<T>().ToList();
            }
        }
        public T Find<T>(int pk, bool WithChildren) where T : class
        {
            if (WithChildren)
            {
                return connection.GetAllWithChildren<T>().FirstOrDefault(m => m.GetHashCode() == pk);
            }
            else
            {
                return connection.Table<T>().FirstOrDefault(m => m.GetHashCode() == pk);
            }
        }
        public void Dispose()
        {
            connection.Dispose();
        }
    }
}
