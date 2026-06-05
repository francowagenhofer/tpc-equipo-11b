using Datos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ObraSocialNegocio
    {
        public List<ObraSocial> ListarObrasSociales()
        {
            List<ObraSocial> lista = new List<ObraSocial>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IDObraSocial, Nombre FROM ObrasSociales WHERE Activo = 1");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    ObraSocial aux = new ObraSocial();
                    aux.Id = (int)datos.Lector["IDObraSocial"];
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
        public int ObtenerIdObraSocial(string nombre)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT IDObraSocial FROM ObrasSociales WHERE Nombre = @Nombre AND Activo = 1");
                datos.setearParametro("@Nombre", nombre);
                datos.ejecutarLectura();
                if (datos.Lector.Read())
                {
                    return (int)datos.Lector["IDObraSocial"];
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
        public void AgregarObraSocial(ObraSocial nuevaObraSocial)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO ObrasSociales (Nombre, Activo) VALUES (@Nombre, 1)");
                datos.setearParametro("@Nombre", nuevaObraSocial.Nombre);
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
        public void ReactivarObraSocial(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE ObrasSociales SET Activo = 1 WHERE IDObraSocial = @Id");
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
        public void ModificarObraSocial(ObraSocial obraSocial)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE ObrasSociales SET Nombre = @Nombre WHERE IDObraSocial = @Id");
                datos.setearParametro("@Nombre", obraSocial.Nombre);
                datos.setearParametro("@Id", obraSocial.Id);
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
        public void EliminarObraSocial(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE ObrasSociales SET Activo = 0 WHERE IDObraSocial = @Id");
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
    