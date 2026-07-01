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
        public List<HistoriaClinica> ListarHC()
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(ConsultaHistoriaClinica() + @" ORDER BY HC.Fecha DESC");

                datos.ejecutarLectura();

                List<HistoriaClinica> lista = new List<HistoriaClinica>();

                while (datos.Lector.Read())
                    lista.Add(MapearHistoriaClinica(datos));

                return lista;
            }
            catch
            {
                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public List<HistoriaClinica> ListarHCPorPaciente(int idPaciente)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(ConsultaHistoriaClinica() + @" WHERE HC.IDPaciente = @IDPaciente ORDER BY HC.Fecha DESC");
                datos.setearParametro("@IDPaciente", idPaciente);

                datos.ejecutarLectura();

                List<HistoriaClinica> lista = new List<HistoriaClinica>();

                while (datos.Lector.Read())
                    lista.Add(MapearHistoriaClinica(datos));

                return lista;
            }
            catch
            {
                throw;
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
                datos.setearConsulta(ConsultaHistoriaClinica() + @" WHERE HC.IDMedico = @IDMedico ORDER BY HC.Fecha DESC");
                datos.setearParametro("@IDMedico", idMedico);
                datos.ejecutarLectura();

                List<HistoriaClinica> lista = new List<HistoriaClinica>();

                while (datos.Lector.Read())
                    lista.Add(MapearHistoriaClinica(datos));

                return lista;
            }
            catch
            {
                throw;
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
                datos.setearConsulta(ConsultaHistoriaClinica() + @" WHERE HC.IDHistoriaClinica = @ID");
                datos.setearParametro("@ID", id);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                    return MapearHistoriaClinica(datos);

                return null;
            }
            catch
            {
                throw;
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
                datos.setearConsulta(ConsultaHistoriaClinica() + @" WHERE HC.IDTurno = @IDTurno");
                datos.setearParametro("@IDTurno", idTurno);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                    return MapearHistoriaClinica(datos);

                return null;
            }
            catch
            {
                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        private string ConsultaHistoriaClinica()
        {
            return @"
                SELECT

                HC.IDHistoriaClinica,
                HC.Fecha,
                HC.Diagnostico,
                HC.Tratamiento,
                HC.Observaciones,
                HC.Activo,

                P.IDPaciente,
                P.DNI,
                P.FechaNacimiento,

                UP.Nombre AS PacienteNombre,
                UP.Apellido AS PacienteApellido,
                UP.Telefono,

                OS.Nombre AS ObraSocial,

                G.Descripcion AS Genero,

                M.IDMedico,
                M.Matricula,

                UM.Nombre AS MedicoNombre,
                UM.Apellido AS MedicoApellido,

                T.IDTurno,
                T.Codigo,

                E.Nombre AS Especialidad

                FROM HistoriaClinica HC

                INNER JOIN Pacientes P
                    ON HC.IDPaciente = P.IDPaciente

                INNER JOIN Usuarios UP
                    ON P.IDUsuario = UP.IDUsuario

                LEFT JOIN ObrasSociales OS
                    ON P.IDObraSocial = OS.IDObraSocial

                LEFT JOIN Generos G
                    ON P.IDGenero = G.IDGenero

                INNER JOIN Medicos M
                    ON HC.IDMedico = M.IDMedico

                INNER JOIN Usuarios UM
                    ON M.IDUsuario = UM.IDUsuario

                LEFT JOIN Turnos T
                    ON HC.IDTurno = T.IDTurno

                LEFT JOIN Especialidades E
                    ON T.IDEspecialidad = E.IDEspecialidad
            ";
        }
        private HistoriaClinica MapearHistoriaClinica(AccesoDatos datos)
        {
            HistoriaClinica aux = new HistoriaClinica();

            aux.Paciente = new Paciente();
            aux.Paciente.Usuario = new Usuario();
            aux.Paciente.ObraSocial = new ObraSocial();
            aux.Paciente.Genero = new Genero();

            aux.Medico = new Medico();
            aux.Medico.Usuario = new Usuario();
            aux.Medico.Especialidad = new Especialidad();

            aux.Turno = new Turno();

            // Historia Clínica
            aux.Id = (int)datos.Lector["IDHistoriaClinica"];
            aux.Fecha = (DateTime)datos.Lector["Fecha"];
            aux.Diagnostico = datos.Lector["Diagnostico"].ToString();
            aux.Tratamiento = datos.Lector["Tratamiento"] as string;
            aux.Observaciones = datos.Lector["Observaciones"] as string;
            aux.Activo = (bool)datos.Lector["Activo"];

            // Paciente
            aux.Paciente.Id = (int)datos.Lector["IDPaciente"];
            aux.Paciente.DNI = datos.Lector["DNI"].ToString();

            if (datos.Lector["FechaNacimiento"] != DBNull.Value)
                aux.Paciente.FechaNacimiento = (DateTime)datos.Lector["FechaNacimiento"];

            aux.Paciente.Usuario.Nombre = datos.Lector["PacienteNombre"].ToString();
            aux.Paciente.Usuario.Apellido = datos.Lector["PacienteApellido"].ToString();

            if (datos.Lector["Telefono"] != DBNull.Value)
                aux.Paciente.Usuario.Telefono = datos.Lector["Telefono"].ToString();

            if (datos.Lector["ObraSocial"] != DBNull.Value)
                aux.Paciente.ObraSocial.Nombre = datos.Lector["ObraSocial"].ToString();

            if (datos.Lector["Genero"] != DBNull.Value)
                aux.Paciente.Genero.Descripcion = datos.Lector["Genero"].ToString();

            // Médico
            aux.Medico.Id = (int)datos.Lector["IDMedico"];
            aux.Medico.Matricula = datos.Lector["Matricula"].ToString();

            aux.Medico.Usuario.Nombre = datos.Lector["MedicoNombre"].ToString();
            aux.Medico.Usuario.Apellido = datos.Lector["MedicoApellido"].ToString();

            // Turno
            if (datos.Lector["IDTurno"] != DBNull.Value)
            {
                aux.Turno.Id = (int)datos.Lector["IDTurno"];
                aux.Turno.Codigo = datos.Lector["Codigo"].ToString();
            }

            // Especialidad
            if (datos.Lector["Especialidad"] != DBNull.Value)
                aux.Medico.Especialidad.Nombre = datos.Lector["Especialidad"].ToString();

            return aux;
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
    }
}
