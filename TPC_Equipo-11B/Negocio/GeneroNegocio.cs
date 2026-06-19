using Datos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class GeneroNegocio
    {
        public List<Genero> ListarGeneros()
        {
            List<Genero> lista = new List<Genero>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IDGenero, Descripcion, Activo FROM Generos");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Genero aux = new Genero();
                    aux.Id = (int)datos.Lector["IDGenero"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Activo = (bool)datos.Lector["Activo"];

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
        public int ObtenerIdGenero(string descripcion)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT IDGenero FROM Generos WHERE Descripcion = @Descripcion AND Activo = 1");
                datos.setearParametro("@Descripcion", descripcion);
                datos.ejecutarLectura();
                if (datos.Lector.Read())
                {
                    return (int)datos.Lector["IDGenero"];
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
        public void AgregarGenero(Genero nuevoGenero)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO Generos (Descripcion, Activo) VALUES (@Descripcion, 1)");
                datos.setearParametro("@Descripcion", nuevoGenero.Descripcion);
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
        public void ReactivarGenero(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Generos SET Activo = 1 WHERE IDGenero = @Id");
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
        public void ModificarGenero(Genero genero)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Generos SET Descripcion = @Descripcion WHERE IDGenero = @Id");
                datos.setearParametro("@Descripcion", genero.Descripcion);
                datos.setearParametro("@Id", genero.Id);
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
        public void EliminarGenero(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Generos SET Activo = 0 WHERE IDGenero = @Id");
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
