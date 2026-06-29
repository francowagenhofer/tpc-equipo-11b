using Datos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio {
    public class AusenciaMedicoNegocio {
        public List<AusenciaMedico> ListarAusencias()
        {
            List<AusenciaMedico> lista = new List<AusenciaMedico>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(
                    @"SELECT
                          A.ID,
                          A.IDMedico,
                          A.Fecha,
                          A.Motivo,

                          M.Matricula,

                          U.IDUsuario,
                          U.Nombre,
                          U.Apellido

                    FROM AusenciasMedico A

                    INNER JOIN Medicos M
                           ON A.IDMedico = M.IDMedico

                    INNER JOIN Usuarios U
                           ON M.IDUsuario = U.IDUsuario

                    ORDER BY A.Fecha");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    AusenciaMedico aux = new AusenciaMedico();

                    aux.Id = (int)datos.Lector["ID"];
                    aux.IdMedico = (int)datos.Lector["IDMedico"];
                    aux.Fecha = (DateTime)datos.Lector["Fecha"];
                    aux.Motivo = (string)datos.Lector["Motivo"];

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

        public List<AusenciaMedico> ListarAusenciasPorMedico(int idMedico)
        {
            List<AusenciaMedico> lista = new List<AusenciaMedico>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(
                    @"SELECT
                           A.ID,
                           A.IDMedico,
                           A.Fecha,
                           A.Motivo,

                           M.Matricula,

                           U.IDUsuario,
                           U.Nombre,
                           U.Apellido

                     FROM AusenciasMedico A

                     INNER JOIN Medicos M
                            ON A.IDMedico = M.IDMedico

                     INNER JOIN Usuarios U
                            ON M.IDUsuario = U.IDUsuario

                     WHERE A.IDMedico = @IDMedico

                     ORDER BY A.Fecha");

                datos.setearParametro("@IDMedico", idMedico);

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    AusenciaMedico aux = new AusenciaMedico();

                    aux.Id = (int)datos.Lector["ID"];
                    aux.IdMedico = (int)datos.Lector["IDMedico"];
                    aux.Fecha = (DateTime)datos.Lector["Fecha"];
                    aux.Motivo = (string)datos.Lector["Motivo"];

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

        public AusenciaMedico ObtenerAusenciaPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(
                    @"SELECT
                          A.ID,
                          A.IDMedico,
                          A.Fecha,
                          A.Motivo,

                          M.Matricula,

                          U.IDUsuario,
                          U.Nombre,
                          U.Apellido

                    FROM AusenciasMedico A

                    INNER JOIN Medicos M
                           ON A.IDMedico = M.IDMedico

                    INNER JOIN Usuarios U
                           ON M.IDUsuario = U.IDUsuario

                    WHERE A.ID = @Id");

                datos.setearParametro("@Id", id);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    AusenciaMedico aux = new AusenciaMedico();

                    aux.Id = (int)datos.Lector["ID"];
                    aux.IdMedico = (int)datos.Lector["IDMedico"];
                    aux.Fecha = (DateTime)datos.Lector["Fecha"];
                    aux.Motivo = (string)datos.Lector["Motivo"];

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

        // Método auxiliar: verificar si el médico tiene turnos activos en una fecha
        public bool MedicoTieneTurnosEnFecha(int idMedico, DateTime fecha)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"
                    SELECT COUNT(*) 
                    FROM Turnos 
                    WHERE IDMedico = @IDMedico 
                      AND CAST(FechaHora AS DATE) = @Fecha 
                      AND IDEstadoTurno NOT IN (
                          SELECT IDEstadoTurno FROM EstadosTurno WHERE Nombre = 'Cancelado'
                      )");

                datos.setearParametro("@IDMedico", idMedico);
                datos.setearParametro("@Fecha", fecha.Date);

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

        public void AgregarAusencia(AusenciaMedico ausencia)
        {
            // 1. VALIDACIÓN: No permitir ausencias en el pasado
            if (ausencia.Fecha.Date < DateTime.Today)
            {
                throw new Exception("No se pueden registrar ausencias para fechas que ya pasaron.");
            }

            // 2. VALIDACIÓN: No permitir ausencias duplicadas
            if (TieneAusencia(ausencia.IdMedico, ausencia.Fecha))
            {
                throw new Exception("Ya existe una ausencia registrada para el médico en la fecha seleccionada.");
            }

            // 3. VALIDACIÓN: No permitir ausencias si hay turnos activos ese día
            if (MedicoTieneTurnosEnFecha(ausencia.IdMedico, ausencia.Fecha))
            {
                throw new Exception("No se puede registrar la ausencia porque el médico tiene turnos asignados para ese día. Debe cancelarlos o reprogramarlos primero.");
            }

            // Validación de motivo obligatorio
            if (string.IsNullOrWhiteSpace(ausencia.Motivo))
            {
                throw new Exception("Debe ingresar el motivo de la ausencia.");
            }

            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"INSERT INTO AusenciasMedico (IDMedico, Fecha, Motivo) VALUES (@IDMedico, @Fecha, @Motivo)");
                datos.setearParametro("@IDMedico", ausencia.IdMedico);
                datos.setearParametro("@Fecha", ausencia.Fecha);
                datos.setearParametro("@Motivo", ausencia.Motivo);

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

        public void EliminarAusencia(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("DELETE FROM AusenciasMedico WHERE ID = @Id");
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

        public bool TieneAusencia(int idMedico, DateTime fecha)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(
                    @"SELECT COUNT(*) FROM AusenciasMedico WHERE IDMedico = @IDMedico AND Fecha = @Fecha");

                datos.setearParametro("@IDMedico", idMedico);
                datos.setearParametro("@Fecha", fecha.Date);

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
