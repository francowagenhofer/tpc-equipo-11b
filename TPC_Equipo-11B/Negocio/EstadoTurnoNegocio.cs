using Datos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class EstadoTurnoNegocio
    {
        public List<EstadoTurno> ListarEstadosTurno()
        {
            List<EstadoTurno> lista = new List<EstadoTurno>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IDEstadoTurno, Nombre, Activo FROM EstadoTurno");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    EstadoTurno aux = new EstadoTurno();
                    aux.Id = (int)datos.Lector["IDEstadoTurno"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
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
        public int ObtenerIdEstadoTurno(string nombre)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT IDEstadoTurno FROM EstadosTurno WHERE Nombre = @Nombre AND Activo = 1");
                datos.setearParametro("@Nombre", nombre);
                datos.ejecutarLectura();
                if (datos.Lector.Read())
                {
                    return (int)datos.Lector["IDEstadoTurno"];
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
        public void AgregarEstadoTurno(EstadoTurno nuevoEstadoTurno)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO EstadosTurno (Nombre, Activo) VALUES (@Nombre, 1)");
                datos.setearParametro("@Nombre", nuevoEstadoTurno.Nombre);
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
        public void ReactivarEstadoTurno(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE EstadosTurno SET Activo = 1 WHERE IDEstadoTurno = @Id");
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
        public void ModificarEstadoTurno(EstadoTurno estadoTurno)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE EstadosTurno SET Nombre = @Nombre WHERE IDEstadoTurno = @Id");
                datos.setearParametro("@Nombre", estadoTurno.Nombre);
                datos.setearParametro("@Id", estadoTurno.Id);
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
        public void EliminarEstadoTurno(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE EstadosTurno SET Activo = 0 WHERE IDEstadoTurno = @Id");
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
