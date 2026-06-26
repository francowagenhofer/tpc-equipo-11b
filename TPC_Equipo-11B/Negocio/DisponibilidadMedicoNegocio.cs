using Datos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class DisponibilidadMedicoNegocio
    {
        public List<DisponibilidadMedico> ListarDisponibilidades()
        {
            List<DisponibilidadMedico> lista = new List<DisponibilidadMedico>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(
                    @"SELECT
                            D.IDDisponibilidad,
                            D.IDMedico,
                            D.DiaSemana,
                            D.HoraInicio,
                            D.HoraFin,
                            D.Activo,

                            U.IDUsuario,
                            U.Nombre,
                            U.Apellido,

                            M.Matricula

                     FROM DisponibilidadMedico D

                     INNER JOIN Medicos M
                           ON D.IDMedico = M.IDMedico

                     INNER JOIN Usuarios U
                           ON M.IDUsuario = U.IDUsuario

                     WHERE Activo = 1");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    DisponibilidadMedico aux = new DisponibilidadMedico();

                    aux.Id = (int)datos.Lector["IDDisponibilidad"];
                    aux.MedicoId = (int)datos.Lector["IDMedico"];
                    aux.DiaSemana = (int)datos.Lector["DiaSemana"];
                    aux.HoraInicio = (TimeSpan)datos.Lector["HoraInicio"];
                    aux.HoraFin = (TimeSpan)datos.Lector["HoraFin"];
                    aux.Activo = (bool)datos.Lector["Activo"];

                    aux.Medico = new Medico();
                    aux.Medico.Usuario = new Usuario();

                    aux.Medico.Id = (int)datos.Lector["IDMedico"];
                    aux.Medico.Matricula = (string)datos.Lector["Matricula"];
                    aux.Medico.Usuario.Id = (int)datos.Lector["IDUsuario"];
                    aux.Medico.Usuario.Nombre = (string)datos.Lector["Nombre"];
                    aux.Medico.Usuario.Apellido = (string)datos.Lector["Apellido"];

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

        public List<DisponibilidadMedico> ListarDisponibilidadesPorMedico(int idMedico)
        {
            List<DisponibilidadMedico> lista = new List<DisponibilidadMedico>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(
                    @"SELECT
                           D.IDDisponibilidad,
                           D.IDMedico,
                           D.DiaSemana,
                           D.HoraInicio,
                           D.HoraFin,
                           D.Activo,

                           U.IDUsuario,
                           U.Nombre,
                           U.Apellido,

                           M.Matricula

                     FROM DisponibilidadMedico D

                     INNER JOIN Medicos M
                            ON D.IDMedico = M.IDMedico

                     INNER JOIN Usuarios U
                            ON M.IDUsuario = U.IDUsuario

                     WHERE D.IDMedico = @IDMedico");

                datos.setearParametro("@IDMedico", idMedico);

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    DisponibilidadMedico aux = new DisponibilidadMedico();

                    aux.Id = (int)datos.Lector["IDDisponibilidad"];
                    aux.MedicoId = (int)datos.Lector["IDMedico"];
                    aux.DiaSemana = (int)datos.Lector["DiaSemana"];
                    aux.HoraInicio = (TimeSpan)datos.Lector["HoraInicio"];
                    aux.HoraFin = (TimeSpan)datos.Lector["HoraFin"];
                    aux.Activo = (bool)datos.Lector["Activo"];

                    aux.Medico = new Medico();
                    aux.Medico.Usuario = new Usuario();

                    aux.Medico.Id = (int)datos.Lector["IDMedico"];
                    aux.Medico.Matricula = (string)datos.Lector["Matricula"];
                    aux.Medico.Usuario.Id = (int)datos.Lector["IDUsuario"];
                    aux.Medico.Usuario.Nombre = (string)datos.Lector["Nombre"];
                    aux.Medico.Usuario.Apellido = (string)datos.Lector["Apellido"];

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

        public DisponibilidadMedico ObtenerPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(
                      @"SELECT
                            D.IDDisponibilidad,
                            D.IDMedico,
                            D.DiaSemana,
                            D.HoraInicio,
                            D.HoraFin,
                            D.Activo,

                            U.IDUsuario,
                            U.Nombre,
                            U.Apellido,

                            M.Matricula

                      FROM DisponibilidadMedico D

                      INNER JOIN Medicos M
                             ON D.IDMedico = M.IDMedico

                      INNER JOIN Usuarios U
                             ON M.IDUsuario = U.IDUsuario

                      WHERE D.IDDisponibilidad = @Id");

                datos.setearParametro("@Id", id);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    DisponibilidadMedico aux = new DisponibilidadMedico();

                    aux.Id = (int)datos.Lector["IDDisponibilidad"];
                    aux.MedicoId = (int)datos.Lector["IDMedico"];
                    aux.DiaSemana = (int)datos.Lector["DiaSemana"];
                    aux.HoraInicio = (TimeSpan)datos.Lector["HoraInicio"];
                    aux.HoraFin = (TimeSpan)datos.Lector["HoraFin"];
                    aux.Activo = (bool)datos.Lector["Activo"];

                    aux.Medico = new Medico();
                    aux.Medico.Usuario = new Usuario();

                    aux.Medico.Id = (int)datos.Lector["IDMedico"];
                    aux.Medico.Matricula = (string)datos.Lector["Matricula"];
                    aux.Medico.Usuario.Id = (int)datos.Lector["IDUsuario"];
                    aux.Medico.Usuario.Nombre = (string)datos.Lector["Nombre"];
                    aux.Medico.Usuario.Apellido = (string)datos.Lector["Apellido"];

                    return aux;
                }

                return null;
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

        public void AgregarDisponibilidad(DisponibilidadMedico disponibilidad)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO DisponibilidadMedico (IDMedico, DiaSemana, HoraInicio, HoraFin, Activo) VALUES (@IDMedico, @DiaSemana, @HoraInicio, @HoraFin, 1)");

                datos.setearParametro("@IDMedico", disponibilidad.MedicoId);
                datos.setearParametro("@DiaSemana", disponibilidad.DiaSemana);
                datos.setearParametro("@HoraInicio", disponibilidad.HoraInicio);
                datos.setearParametro("@HoraFin", disponibilidad.HoraFin);

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

        public void ModificarDisponibilidad(DisponibilidadMedico disponibilidad)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE DisponibilidadMedico SET DiaSemana = @DiaSemana, HoraInicio = @HoraInicio, HoraFin = @HoraFin WHERE IDDisponibilidad = @Id");

                datos.setearParametro("@DiaSemana", disponibilidad.DiaSemana);
                datos.setearParametro("@HoraInicio", disponibilidad.HoraInicio);
                datos.setearParametro("@HoraFin", disponibilidad.HoraFin);
                datos.setearParametro("@Id", disponibilidad.Id);

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

        public void EliminarDisponibilidad(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE DisponibilidadMedico SET Activo = 0 WHERE IDDisponibilidad = @Id");

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

        public bool ExisteSuperposicion(DisponibilidadMedico disponibilidad)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(
                    @"SELECT COUNT(*)
                      FROM DisponibilidadMedico
                      WHERE IDMedico = @IDMedico
                      AND DiaSemana = @DiaSemana
                      AND Activo = 1
                      AND IDDisponibilidad <> @Id
                      AND (@HoraInicio < HoraFin)
                      AND (@HoraFin > HoraInicio)");

                datos.setearParametro("@IDMedico", disponibilidad.MedicoId);
                datos.setearParametro("@DiaSemana", disponibilidad.DiaSemana);
                datos.setearParametro("@HoraInicio", disponibilidad.HoraInicio);
                datos.setearParametro("@HoraFin", disponibilidad.HoraFin);
                datos.setearParametro("@Id", disponibilidad.Id);

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

        public bool TieneDisponibilidad(int idMedico)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta( @"SELECT COUNT(*) FROM DisponibilidadMedico WHERE IDMedico = @IDMedico AND Activo = 1");
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
