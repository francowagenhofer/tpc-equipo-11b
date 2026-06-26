using Datos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class MedicoObraSocialNegocio
    {
        public List<ObraSocial> ListarObrasSocialesPorMedico(int idMedico)
        {
            List<ObraSocial> lista = new List<ObraSocial>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(
                    @"SELECT
                          O.IDObraSocial,
                          O.Nombre,
                          O.TipoPlan,
                          O.Activo

                    FROM MedicoObraSocial MOS

                    INNER JOIN ObrasSociales O
                           ON MOS.IDObraSocial = O.IDObraSocial

                    WHERE MOS.IDMedico = @IDMedico
                    AND O.Activo = 1

                    ORDER BY O.Nombre");

                datos.setearParametro("@IDMedico", idMedico);

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

        public List<Medico> ListarMedicosPorObraSocial(int idObraSocial)
        {
            List<Medico> lista = new List<Medico>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(
                    @"SELECT

                          M.IDMedico,
                          M.Matricula,

                          U.IDUsuario,
                          U.Nombre,
                          U.Apellido,
                          U.Email

                    FROM MedicoObraSocial MOS

                    INNER JOIN Medicos M
                           ON MOS.IDMedico = M.IDMedico

                    INNER JOIN Usuarios U
                           ON M.IDUsuario = U.IDUsuario

                    WHERE MOS.IDObraSocial = @IDObraSocial
                    AND M.Activo = 1
                    AND U.Activo = 1

                    ORDER BY U.Apellido, U.Nombre");

                datos.setearParametro("@IDObraSocial", idObraSocial);

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Medico aux = new Medico();

                    aux.Usuario = new Usuario();

                    aux.Id = (int)datos.Lector["IDMedico"];
                    aux.Matricula = (string)datos.Lector["Matricula"];

                    aux.Usuario.Id = (int)datos.Lector["IDUsuario"];
                    aux.Usuario.Nombre = (string)datos.Lector["Nombre"];
                    aux.Usuario.Apellido = (string)datos.Lector["Apellido"];
                    aux.Usuario.Email = (string)datos.Lector["Email"];

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

        public void AsociarObraSocial(int idMedico, int idObraSocial)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"INSERT INTO MedicoObraSocial (IDMedico, IDObraSocial) VALUES (@IDMedico, @IDObraSocial)");

                datos.setearParametro("@IDMedico", idMedico);
                datos.setearParametro("@IDObraSocial", idObraSocial);

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

        public void DesasociarObraSocial(int idMedico, int idObraSocial)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(
                    @"DELETE
              FROM MedicoObraSocial
              WHERE IDMedico = @IDMedico
              AND IDObraSocial = @IDObraSocial");

                datos.setearParametro("@IDMedico", idMedico);
                datos.setearParametro("@IDObraSocial", idObraSocial);

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

        public bool AtiendeObraSocial(int idMedico, int idObraSocial)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"SELECT COUNT(*) FROM MedicoObraSocial WHERE IDMedico = @IDMedico AND IDObraSocial = @IDObraSocial");
                datos.setearParametro("@IDMedico", idMedico);
                datos.setearParametro("@IDObraSocial", idObraSocial);

                return datos.ejecutarEscalar() > 0;
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

        public bool TieneObrasSociales(int idMedico)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"SELECT COUNT(*) FROM MedicoObraSocial WHERE IDMedico = @IDMedico");
                datos.setearParametro("@IDMedico", idMedico);

                return datos.ejecutarEscalar() > 0;
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
