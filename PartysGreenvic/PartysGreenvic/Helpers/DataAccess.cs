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
            try
            {
                var config = DependencyService.Get<IConfig>();
                this.connection = new SQLiteConnection(config.Platform, Path.Combine(config.DirectoryBD, "PartysGreenvic.db3"));
                connection.CreateTable<Empleado>();
            }
            catch (Exception ex)
            {
                ex.ToString();
                return;
            }
        }
        public void InsertarEmpleado(Empleado empleado)
        {
            try
            {
                this.connection.Insert(empleado);
            }
            catch (Exception ex)
            {
                ex.ToString();
                return;
            }
        }
        public void BorrarEmpleado(Empleado empleado)
        {
            this.connection.Delete(empleado);
        }
        public void ActualizarEmpleado(Empleado empleado)
        {
            this.connection.Update(empleado);
        }
        public Empleado GetEmpleado(string Rut)
        {
            return connection.Table<Empleado>().FirstOrDefault(c => c.Rut == Rut);
        }
        public List<Empleado> GetEmpleados()
        {
            return connection.Table<Empleado>().OrderBy(c => c.Nombre).ToList();
        }
        public Empleado BuscarEmpleado(string Rut)
        {
            return connection.Table<Empleado>().FirstOrDefault(c => c.Rut == Rut);
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
