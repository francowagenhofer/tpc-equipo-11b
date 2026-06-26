using Datos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class HistoriaClinicaNegocio
    {
        public List<HistoriaClinica> ListarHCPorPaciente(int idPaciente)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                    SELECT
                    
                    HC.IDHistoriaClinica,
                    HC.Fecha,
                    HC.Diagnostico,
                    HC.Tratamiento,
                    HC.Observaciones,
                    HC.Activo,
                    
                    P.IDPaciente,
                    
                    M.IDMedico,
                    M.Matricula,
                    
                    U.IDUsuario,
                    U.Nombre,
                    U.Apellido,
                    
                    T.IDTurno,
                    T.Codigo
                    
                    FROM HistoriaClinica HC
                    
                    INNER JOIN Pacientes P
                    ON HC.IDPaciente=P.IDPaciente
                    
                    INNER JOIN Medicos M
                    ON HC.IDMedico=M.IDMedico
                    
                    INNER JOIN Usuarios U
                    ON M.IDUsuario=U.IDUsuario
                    
                    LEFT JOIN Turnos T
                    ON HC.IDTurno=T.IDTurno
                    
                    WHERE HC.IDPaciente = @IDPaciente
                    ORDER BY HC.Fecha DESC");

                datos.setearParametro("@IDPaciente", idPaciente);
                datos.ejecutarLectura();

                List<HistoriaClinica> lista = new List<HistoriaClinica>();


                while (datos.Lector.Read())
                {

                    HistoriaClinica aux = new HistoriaClinica();

                    aux.Paciente = new Paciente();
                    aux.Medico = new Medico();
                    aux.Medico.Usuario = new Usuario();
                    aux.Turno = new Turno();

                    aux.Id = (int)datos.Lector["IDHistoriaClinica"];
                    aux.Fecha = (DateTime)datos.Lector["Fecha"];
                    aux.Diagnostico = (string)datos.Lector["Diagnostico"];
                    aux.Tratamiento = datos.Lector["Tratamiento"] as string;
                    aux.Observaciones = datos.Lector["Observaciones"] as string;
                    aux.Activo = (bool)datos.Lector["Activo"];

                    aux.Paciente.Id = (int)datos.Lector["IDPaciente"];

                    aux.Medico.Id = (int)datos.Lector["IDMedico"];
                    aux.Medico.Matricula = (string)datos.Lector["Matricula"];

                    aux.Medico.Usuario.Id = (int)datos.Lector["IDUsuario"];
                    aux.Medico.Usuario.Nombre = (string)datos.Lector["Nombre"];
                    aux.Medico.Usuario.Apellido = (string)datos.Lector["Apellido"];

                    if (!(datos.Lector["IDTurno"] is DBNull))
                    {
                        aux.Turno.Id = (int)datos.Lector["IDTurno"];
                        aux.Turno.Codigo = (string)datos.Lector["Codigo"];
                    }

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

        public List<HistoriaClinica> ListarHCPorMedico(int idMedico)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                    SELECT
                    
                    HC.IDHistoriaClinica,
                    HC.Fecha,
                    HC.Diagnostico,
                    HC.Tratamiento,
                    HC.Observaciones,
                    HC.Activo,
                    
                    P.IDPaciente,
                    
                    M.IDMedico,
                    M.Matricula,
                    
                    U.IDUsuario,
                    U.Nombre,
                    U.Apellido,
                    
                    T.IDTurno,
                    T.Codigo
                    
                    FROM HistoriaClinica HC
                    
                    INNER JOIN Pacientes P
                    ON HC.IDPaciente=P.IDPaciente
                    
                    INNER JOIN Medicos M
                    ON HC.IDMedico=M.IDMedico
                    
                    INNER JOIN Usuarios U
                    ON M.IDUsuario=U.IDUsuario
                    
                    LEFT JOIN Turnos T
                    ON HC.IDTurno=T.IDTurno
                    
                    WHERE HC.IDMedico = @IDMedico
                    ORDER BY HC.Fecha DESC");

                datos.setearParametro("@IdMedico", idMedico);
                datos.ejecutarLectura();

                List<HistoriaClinica> lista = new List<HistoriaClinica>();


                while (datos.Lector.Read())
                {

                    HistoriaClinica aux = new HistoriaClinica();

                    aux.Paciente = new Paciente();
                    aux.Medico = new Medico();
                    aux.Medico.Usuario = new Usuario();
                    aux.Turno = new Turno();

                    aux.Id = (int)datos.Lector["IDHistoriaClinica"];
                    aux.Fecha = (DateTime)datos.Lector["Fecha"];
                    aux.Diagnostico = (string)datos.Lector["Diagnostico"];
                    aux.Tratamiento = datos.Lector["Tratamiento"] as string;
                    aux.Observaciones = datos.Lector["Observaciones"] as string;
                    aux.Activo = (bool)datos.Lector["Activo"];

                    aux.Paciente.Id = (int)datos.Lector["IDPaciente"];

                    aux.Medico.Id = (int)datos.Lector["IDMedico"];
                    aux.Medico.Matricula = (string)datos.Lector["Matricula"];

                    aux.Medico.Usuario.Id = (int)datos.Lector["IDUsuario"];
                    aux.Medico.Usuario.Nombre = (string)datos.Lector["Nombre"];
                    aux.Medico.Usuario.Apellido = (string)datos.Lector["Apellido"];

                    if (!(datos.Lector["IDTurno"] is DBNull))
                    {
                        aux.Turno.Id = (int)datos.Lector["IDTurno"];
                        aux.Turno.Codigo = (string)datos.Lector["Codigo"];
                    }

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

        public HistoriaClinica ObtenerHCPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                    SELECT
                    
                    HC.IDHistoriaClinica,
                    HC.Fecha,
                    HC.Diagnostico,
                    HC.Tratamiento,
                    HC.Observaciones,
                    HC.Activo,
                    
                    P.IDPaciente,
                    
                    M.IDMedico,
                    M.Matricula,
                    
                    U.IDUsuario,
                    U.Nombre,
                    U.Apellido,
                    
                    T.IDTurno,
                    T.Codigo
                    
                    FROM HistoriaClinica HC
                    
                    INNER JOIN Pacientes P
                    ON HC.IDPaciente=P.IDPaciente
                    
                    INNER JOIN Medicos M
                    ON HC.IDMedico=M.IDMedico
                    
                    INNER JOIN Usuarios U
                    ON M.IDUsuario=U.IDUsuario
                    
                    LEFT JOIN Turnos T
                    ON HC.IDTurno=T.IDTurno
                    
                    WHERE HC.IDHistoriaClinica=@Id");

                datos.setearParametro("@Id", id);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    HistoriaClinica aux = new HistoriaClinica();

                    aux.Paciente = new Paciente();
                    aux.Medico = new Medico();
                    aux.Medico.Usuario = new Usuario();
                    aux.Turno = new Turno();

                    aux.Id = (int)datos.Lector["IDHistoriaClinica"];
                    aux.Fecha = (DateTime)datos.Lector["Fecha"];
                    aux.Diagnostico = (string)datos.Lector["Diagnostico"];
                    aux.Tratamiento = datos.Lector["Tratamiento"] as string;
                    aux.Observaciones = datos.Lector["Observaciones"] as string;
                    aux.Activo = (bool)datos.Lector["Activo"];

                    aux.Paciente.Id = (int)datos.Lector["IDPaciente"];

                    aux.Medico.Id = (int)datos.Lector["IDMedico"];
                    aux.Medico.Matricula = (string)datos.Lector["Matricula"];

                    aux.Medico.Usuario.Id = (int)datos.Lector["IDUsuario"];
                    aux.Medico.Usuario.Nombre = (string)datos.Lector["Nombre"];
                    aux.Medico.Usuario.Apellido = (string)datos.Lector["Apellido"];

                    if (!(datos.Lector["IDTurno"] is DBNull))
                    {
                        aux.Turno.Id = (int)datos.Lector["IDTurno"];
                        aux.Turno.Codigo = (string)datos.Lector["Codigo"];
                    }

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

        public HistoriaClinica ObtenerHCPorTurno(int idTurno) 
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                    SELECT
                    
                    HC.IDHistoriaClinica,
                    HC.Fecha,
                    HC.Diagnostico,
                    HC.Tratamiento,
                    HC.Observaciones,
                    HC.Activo,
                    
                    P.IDPaciente,
                    
                    M.IDMedico,
                    M.Matricula,
                    
                    U.IDUsuario,
                    U.Nombre,
                    U.Apellido,
                    
                    T.IDTurno,
                    T.Codigo
                    
                    FROM HistoriaClinica HC
                    
                    INNER JOIN Pacientes P
                    ON HC.IDPaciente=P.IDPaciente
                    
                    INNER JOIN Medicos M
                    ON HC.IDMedico=M.IDMedico
                    
                    INNER JOIN Usuarios U
                    ON M.IDUsuario=U.IDUsuario
                    
                    LEFT JOIN Turnos T
                    ON HC.IDTurno=T.IDTurno
                    
                    WHERE HC.IDTurno = @IDTurno");

                datos.setearParametro("@IDTurno", idTurno);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    HistoriaClinica aux = new HistoriaClinica();

                    aux.Paciente = new Paciente();
                    aux.Medico = new Medico();
                    aux.Medico.Usuario = new Usuario();
                    aux.Turno = new Turno();

                    aux.Id = (int)datos.Lector["IDHistoriaClinica"];
                    aux.Fecha = (DateTime)datos.Lector["Fecha"];
                    aux.Diagnostico = (string)datos.Lector["Diagnostico"];
                    aux.Tratamiento = datos.Lector["Tratamiento"] as string;
                    aux.Observaciones = datos.Lector["Observaciones"] as string;
                    aux.Activo = (bool)datos.Lector["Activo"];

                    aux.Paciente.Id = (int)datos.Lector["IDPaciente"];

                    aux.Medico.Id = (int)datos.Lector["IDMedico"];
                    aux.Medico.Matricula = (string)datos.Lector["Matricula"];

                    aux.Medico.Usuario.Id = (int)datos.Lector["IDUsuario"];
                    aux.Medico.Usuario.Nombre = (string)datos.Lector["Nombre"];
                    aux.Medico.Usuario.Apellido = (string)datos.Lector["Apellido"];

                    if (!(datos.Lector["IDTurno"] is DBNull))
                    {
                        aux.Turno.Id = (int)datos.Lector["IDTurno"];
                        aux.Turno.Codigo = (string)datos.Lector["Codigo"];
                    }

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

        public void AgregarHC(HistoriaClinica historia)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(
                    @"INSERT INTO HistoriaClinica
                    (
                        IDPaciente,
                        IDMedico,
                        IDTurno,
                        Diagnostico,
                        Tratamiento,
                        Observaciones,
                        Activo
                    )
                    VALUES
                    (
                        @IDPaciente,
                        @IDMedico,
                        @IDTurno,
                        @Diagnostico,
                        @Tratamiento,
                        @Observaciones,
                        1
                    )");

                datos.setearParametro("@IDPaciente", historia.Paciente.Id);
                datos.setearParametro("@IDMedico", historia.Medico.Id);
                datos.setearParametro("@IDTurno", historia.Turno != null ? (object)historia.Turno.Id : DBNull.Value);
                datos.setearParametro("@Diagnostico", historia.Diagnostico);
                datos.setearParametro("@Tratamiento", historia.Tratamiento);
                datos.setearParametro("@Observaciones", historia.Observaciones);

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

        public void ModificarHC(HistoriaClinica historia)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(
                    @"UPDATE HistoriaClinica
                     SET Diagnostico = @Diagnostico,
                         Tratamiento = @Tratamiento,
                         Observaciones = @Observaciones
                     WHERE IDHistoriaClinica = @Id");

                datos.setearParametro("@Diagnostico", historia.Diagnostico);
                datos.setearParametro("@Tratamiento", historia.Tratamiento);
                datos.setearParametro("@Observaciones", historia.Observaciones);
                datos.setearParametro("@Id", historia.Id);

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

        public void ReactivarHC(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"UPDATE HistoriaClinica SET Activo = 1 WHERE IDHistoriaClinica = @Id");
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

        public void EliminarHC(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"UPDATE HistoriaClinica SET Activo = 0 WHERE IDHistoriaClinica = @Id");

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

        public bool ExisteHCParaTurno(int idTurno)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"SELECT COUNT(*) FROM HistoriaClinica WHERE IDTurno=@IDTurno AND Activo=1");

                datos.setearParametro("@IDTurno", idTurno);

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

        //ExportarPdf
        //GenerarQr

    }
}
