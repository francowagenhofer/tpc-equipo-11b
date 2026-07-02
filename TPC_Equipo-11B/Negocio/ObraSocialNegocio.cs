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
                datos.setearConsulta(@"
                    SELECT
                        IDObraSocial,
                        Nombre,
                        TipoPlan,
                        Activo
                    FROM ObrasSociales
                    WHERE Activo = 1
                    ORDER BY Nombre, TipoPlan");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    ObraSocial aux = new ObraSocial();

                    aux.Id = (int)datos.Lector["IDObraSocial"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.TipoPlan = (string)datos.Lector["TipoPlan"];
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

        public void AgregarObraSocial(ObraSocial nuevaObraSocial)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO ObrasSociales (Nombre, TipoPlan, Activo)\r\nVALUES (@Nombre, @TipoPlan, 1)");
                datos.setearParametro("@Nombre", nuevaObraSocial.Nombre);
                datos.setearParametro("@TipoPlan", nuevaObraSocial.TipoPlan);

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
                datos.setearConsulta("UPDATE ObrasSociales SET Nombre = @Nombre, TipoPlan = @TipoPlan WHERE IDObraSocial = @Id");
                datos.setearParametro("@Nombre", obraSocial.Nombre);
                datos.setearParametro("@TipoPlan", obraSocial.TipoPlan);
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

        // Metodos para el dashboard
        public string ObtenerObraSocialMasUtilizada()
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                    SELECT TOP 1
                        OS.Nombre + ' (' + OS.TipoPlan + ')' AS ObraSocial
                    FROM Pacientes P
                    INNER JOIN ObrasSociales OS
                        ON P.IDObraSocial = OS.IDObraSocial
                    WHERE P.Activo = 1
                      AND OS.Activo = 1
                    GROUP BY OS.Nombre, OS.TipoPlan
                    ORDER BY COUNT(*) DESC");

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                    return datos.Lector["ObraSocial"].ToString();

                return "Sin datos";
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
    