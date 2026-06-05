using Datos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class RolNegocio
    {
        public List<Rol> ListarRoles()
        {
            List<Rol> lista = new List<Rol>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IDRol, Nombre FROM Roles WHERE Activo = 1");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Rol aux = new Rol();
                    aux.Id = (int)datos.Lector["IDRol"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    lista.Add(aux);

                }

                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public int ObtenerIdRol(string nombre)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT IDRol FROM Roles WHERE Nombre = @Nombre AND Activo = 1");
                datos.setearParametro("@Nombre", nombre);
                datos.ejecutarLectura();
                if (datos.Lector.Read())
                {
                    return (int)datos.Lector["IDRol"];
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void AgregarRoles(Rol nuevoRol)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO Roles (Nombre, Activo) VALUES (@Nombre, 1)");
                datos.setearParametro("@Nombre", nuevoRol.Nombre);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void ReactivarRoles(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Roles SET Activo = 1 WHERE IDRol = @Id");
                datos.setearParametro("@Id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void ModificarRoles(Rol rol)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Roles SET Nombre = @Nombre WHERE IDRol = @Id");
                datos.setearParametro("@Nombre", rol.Nombre);
                datos.setearParametro("@Id", rol.Id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void EliminarRoles(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Roles SET Activo = 0 WHERE IDRol = @Id");
                datos.setearParametro("@Id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

    }
}
