using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using Datos;
using System.ComponentModel;

namespace Negocio {
    public class TurnoNegocio {
        public List<Turno> ListarTurnos()
        {
            List<Turno> lista = new List<Turno>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"
                    SELECT 
                        T.IDTurno,
                        T.Codigo,
                        T.FechaHora,
                        T.IDEstadoTurno,
                        ET.Nombre AS EstadoNombre,
                    
                        P.IDPaciente,
                        P.DNI,
                    
                        U_Pac.Nombre AS PacienteNombre,
                        U_Pac.Apellido AS PacienteApellido,
                    
                        M.IDMedico,
                        M.Matricula,
                    
                        U_Med.Nombre AS MedicoNombre,
                        U_Med.Apellido AS MedicoApellido
                    
                    FROM Turnos T
                    
                    INNER JOIN Pacientes P 
                        ON T.IDPaciente = P.IDPaciente
                    
                    INNER JOIN Usuarios U_Pac
                        ON P.IDUsuario = U_Pac.IDUsuario
                    
                    INNER JOIN Medicos M 
                        ON T.IDMedico = M.IDMedico
                    
                    INNER JOIN Usuarios U_Med
                        ON M.IDUsuario = U_Med.IDUsuario
                    
                    INNER JOIN EstadoTurno ET 
                        ON T.IDEstadoTurno = ET.IDEstadoTurno
                    
                    ORDER BY T.FechaHora ASC");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Turno aux = new Turno();

                    aux.Id = (int)datos.Lector["IDTurno"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.FechaHora = (DateTime)datos.Lector["FechaHora"];

                    aux.EstadoTurno = new EstadoTurno();
                    aux.EstadoTurno.Id = (int)datos.Lector["IDEstadoTurno"];
                    aux.EstadoTurno.Nombre = (string)datos.Lector["EstadoNombre"];

                    aux.Paciente = new Paciente();
                    aux.Paciente.Id = (int)datos.Lector["IDPaciente"];
                    aux.Paciente.DNI = (string)datos.Lector["DNI"];

                    aux.Paciente.Usuario = new Usuario();
                    aux.Paciente.Usuario.Nombre = (string)datos.Lector["PacienteNombre"];
                    aux.Paciente.Usuario.Apellido = (string)datos.Lector["PacienteApellido"];

                    aux.Medico = new Medico();
                    aux.Medico.Id = (int)datos.Lector["IDMedico"];
                    aux.Medico.Matricula = (string)datos.Lector["Matricula"];

                    aux.Medico.Usuario = new Usuario();
                    aux.Medico.Usuario.Nombre = (string)datos.Lector["MedicoNombre"];
                    aux.Medico.Usuario.Apellido = (string)datos.Lector["MedicoApellido"];

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

        public List<Turno> ListarTurnosPorMedico(int idMedico)
        {
            List<Turno> lista = new List<Turno>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                    SELECT
                        T.IDTurno,
                        T.Codigo,
                        T.FechaHora,
                    
                        T.IDEstadoTurno,
                        ET.Nombre EstadoNombre,
        
                        T.IDEspecialidad,
                        E.Nombre AS EspecialidadNombre,
                        E.Descripcion AS EspecialidadDescripcion,
                    
                        P.IDPaciente,
                        P.DNI,
                        P.IDObraSocial,
                    
                        OS.Nombre AS ObraSocial,
                    
                        U_Pac.Nombre PacienteNombre,
                        U_Pac.Apellido PacienteApellido,
                    
                        M.IDMedico,
                        M.Matricula,
                    
                        U_Med.Nombre MedicoNombre,
                        U_Med.Apellido MedicoApellido
                    
                    FROM Turnos T
                    
                    INNER JOIN Pacientes P
                        ON T.IDPaciente = P.IDPaciente
                    
                    INNER JOIN Usuarios U_Pac
                        ON P.IDUsuario = U_Pac.IDUsuario
                    
                    LEFT JOIN ObrasSociales OS
                        ON P.IDObraSocial = OS.IDObraSocial
                    
                    INNER JOIN Medicos M
                        ON T.IDMedico = M.IDMedico
                    
                    INNER JOIN Usuarios U_Med
                        ON M.IDUsuario = U_Med.IDUsuario
                    
                    INNER JOIN EstadoTurno ET
                        ON T.IDEstadoTurno = ET.IDEstadoTurno

                    LEFT JOIN Especialidades E
                        ON T.IDEspecialidad = E.IDEspecialidad
                    
                    WHERE T.IDMedico = @IDMedico
                    
                    ORDER BY T.FechaHora");

                datos.setearParametro("@IDMedico", idMedico);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Turno aux = new Turno();

                    aux.Id = (int)datos.Lector["IDTurno"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.FechaHora = (DateTime)datos.Lector["FechaHora"];

                    aux.EstadoTurno = new EstadoTurno();
                    aux.EstadoTurno.Id = (int)datos.Lector["IDEstadoTurno"];
                    aux.EstadoTurno.Nombre = (string)datos.Lector["EstadoNombre"];

                    // CAMBIO: protegido con null check porque ahora es LEFT JOIN
                    aux.Especialidad = new Especialidad();
                    if (datos.Lector["IDEspecialidad"] != DBNull.Value)
                    {
                        aux.Especialidad.Id = (int)datos.Lector["IDEspecialidad"];
                        aux.Especialidad.Nombre = datos.Lector["EspecialidadNombre"].ToString();
                        aux.Especialidad.Descripcion = datos.Lector["EspecialidadDescripcion"] != DBNull.Value
                            ? datos.Lector["EspecialidadDescripcion"].ToString() : "";
                    }

                    aux.Paciente = new Paciente();
                    aux.Paciente.Id = (int)datos.Lector["IDPaciente"];
                    aux.Paciente.DNI = (string)datos.Lector["DNI"];
                    aux.Paciente.ObraSocial = new ObraSocial();

                    if (datos.Lector["ObraSocial"] != DBNull.Value)
                        aux.Paciente.ObraSocial.Nombre = datos.Lector["ObraSocial"].ToString();

                    aux.Paciente.Usuario = new Usuario();
                    aux.Paciente.Usuario.Nombre = (string)datos.Lector["PacienteNombre"];
                    aux.Paciente.Usuario.Apellido = (string)datos.Lector["PacienteApellido"];

                    aux.Medico = new Medico();
                    aux.Medico.Id = (int)datos.Lector["IDMedico"];
                    aux.Medico.Matricula = (string)datos.Lector["Matricula"];

                    aux.Medico.Usuario = new Usuario();
                    aux.Medico.Usuario.Nombre = (string)datos.Lector["MedicoNombre"];
                    aux.Medico.Usuario.Apellido = (string)datos.Lector["MedicoApellido"];

                    lista.Add(aux);
                }

                return lista;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public List<Turno> ListarTurnosPorPaciente(int idPaciente)
        {
            List<Turno> lista = new List<Turno>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                    SELECT
                        T.IDTurno,
                        T.Codigo,
                        T.FechaHora,

                        T.IDEstadoTurno,
                        ET.Nombre AS EstadoNombre,

                        T.IDEspecialidad,
                        E.Nombre AS EspecialidadNombre,

                        P.IDPaciente,
                        P.DNI,
                        P.IDObraSocial,

                        OS.Nombre AS ObraSocial,

                        U_Pac.IDUsuario AS IDUsuarioPaciente,
                        U_Pac.Nombre AS PacienteNombre,
                        U_Pac.Apellido AS PacienteApellido,

                        M.IDMedico,
                        M.Matricula,

                        U_Med.IDUsuario AS IDUsuarioMedico,
                        U_Med.Nombre AS MedicoNombre,
                        U_Med.Apellido AS MedicoApellido

                    FROM Turnos T

                    INNER JOIN Pacientes P
                        ON T.IDPaciente = P.IDPaciente

                    INNER JOIN Usuarios U_Pac
                        ON P.IDUsuario = U_Pac.IDUsuario

                    LEFT JOIN ObrasSociales OS
                        ON P.IDObraSocial = OS.IDObraSocial

                    INNER JOIN Medicos M
                        ON T.IDMedico = M.IDMedico

                    INNER JOIN Usuarios U_Med
                        ON M.IDUsuario = U_Med.IDUsuario

                    INNER JOIN EstadoTurno ET
                        ON T.IDEstadoTurno = ET.IDEstadoTurno

                    LEFT JOIN Especialidades E
                        ON T.IDEspecialidad = E.IDEspecialidad

                    WHERE T.IDPaciente = @IDPaciente

                    ORDER BY T.FechaHora");

                datos.setearParametro("@IDPaciente", idPaciente);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Turno aux = new Turno();

                    aux.Id = (int)datos.Lector["IDTurno"];
                    aux.Codigo = datos.Lector["Codigo"] != DBNull.Value ? datos.Lector["Codigo"].ToString() : "";
                    aux.FechaHora = (DateTime)datos.Lector["FechaHora"];

                    aux.EstadoTurno = new EstadoTurno();
                    aux.EstadoTurno.Id = (int)datos.Lector["IDEstadoTurno"];
                    aux.EstadoTurno.Nombre = datos.Lector["EstadoNombre"].ToString();

                    aux.Especialidad = new Especialidad();
                    if (datos.Lector["IDEspecialidad"] != DBNull.Value)
                    {
                        aux.Especialidad.Id = (int)datos.Lector["IDEspecialidad"];
                        aux.Especialidad.Nombre = datos.Lector["EspecialidadNombre"].ToString();
                    }

                    aux.Paciente = new Paciente();
                    aux.Paciente.Id = (int)datos.Lector["IDPaciente"];
                    aux.Paciente.DNI = datos.Lector["DNI"].ToString();

                    aux.Paciente.ObraSocial = new ObraSocial();
                    if (datos.Lector["ObraSocial"] != DBNull.Value)
                        aux.Paciente.ObraSocial.Nombre = datos.Lector["ObraSocial"].ToString();

                    aux.Paciente.Usuario = new Usuario();
                    aux.Paciente.Usuario.Id = (int)datos.Lector["IDUsuarioPaciente"];
                    aux.Paciente.Usuario.Nombre = datos.Lector["PacienteNombre"].ToString();
                    aux.Paciente.Usuario.Apellido = datos.Lector["PacienteApellido"].ToString();

                    aux.Medico = new Medico();
                    aux.Medico.Id = (int)datos.Lector["IDMedico"];
                    aux.Medico.Matricula = datos.Lector["Matricula"].ToString();

                    aux.Medico.Usuario = new Usuario();
                    aux.Medico.Usuario.Id = (int)datos.Lector["IDUsuarioMedico"];
                    aux.Medico.Usuario.Nombre = datos.Lector["MedicoNombre"].ToString();
                    aux.Medico.Usuario.Apellido = datos.Lector["MedicoApellido"].ToString();

                    lista.Add(aux);
                }

                return lista;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public Turno ObtenerTurnoPorId(int idTurno)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                    SELECT
                        T.IDTurno,
                        T.Codigo,
                        T.FechaHora,

                        ET.IDEstadoTurno,
                        ET.Nombre AS EstadoNombre,

                        P.IDPaciente,
                        P.DNI,
                        P.FechaNacimiento,
                        P.Direccion,
                        P.IDObraSocial,

                        G.Descripcion AS Genero,

                        U_P.Nombre AS PacienteNombre,
                        U_P.Apellido AS PacienteApellido,
                        U_P.Telefono AS PacienteTelefono,

                        OS.Nombre AS ObraSocial,

                        M.IDMedico,
                        M.Matricula,

                        U_M.Nombre AS MedicoNombre,
                        U_M.Apellido AS MedicoApellido,

                        E.IDEspecialidad,
                        E.Nombre AS Especialidad

                    FROM Turnos T

                    INNER JOIN EstadoTurno ET
                        ON ET.IDEstadoTurno = T.IDEstadoTurno

                    INNER JOIN Pacientes P
                        ON P.IDPaciente = T.IDPaciente

                    INNER JOIN Usuarios U_P
                        ON U_P.IDUsuario = P.IDUsuario

                    LEFT JOIN ObrasSociales OS
                        ON OS.IDObraSocial = P.IDObraSocial

                    LEFT JOIN Generos G
                        ON G.IDGenero = P.IDGenero

                    INNER JOIN Medicos M
                        ON M.IDMedico = T.IDMedico

                    INNER JOIN Usuarios U_M
                        ON U_M.IDUsuario = M.IDUsuario

                    LEFT JOIN Especialidades E
                        ON E.IDEspecialidad = T.IDEspecialidad

                    WHERE T.IDTurno = @Id");

                datos.setearParametro("@Id", idTurno);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Turno turno = new Turno();

                    turno.Id = (int)datos.Lector["IDTurno"];
                    turno.Codigo = datos.Lector["Codigo"].ToString();
                    turno.FechaHora = (DateTime)datos.Lector["FechaHora"];

                    turno.PacienteId = (int)datos.Lector["IDPaciente"];
                    turno.MedicoId = (int)datos.Lector["IDMedico"];

                    turno.EstadoTurno = new EstadoTurno
                    {
                        Id = (int)datos.Lector["IDEstadoTurno"],
                        Nombre = datos.Lector["EstadoNombre"].ToString()
                    };

                    turno.Paciente = new Paciente();
                    turno.Paciente.Id = (int)datos.Lector["IDPaciente"];
                    turno.Paciente.DNI = datos.Lector["DNI"].ToString();
                    turno.Paciente.FechaNacimiento = datos.Lector["FechaNacimiento"] != DBNull.Value
                        ? (DateTime)datos.Lector["FechaNacimiento"] : DateTime.MinValue;
                    turno.Paciente.Direccion = datos.Lector["Direccion"] != DBNull.Value
                        ? datos.Lector["Direccion"].ToString() : "";
                    turno.Paciente.ObraSocialId = datos.Lector["IDObraSocial"] != DBNull.Value
                        ? (int)datos.Lector["IDObraSocial"] : 0;
                    turno.Paciente.Genero = datos.Lector["Genero"] != DBNull.Value
                        ? new Genero { Descripcion = datos.Lector["Genero"].ToString() } : null;

                    turno.Paciente.Usuario = new Usuario();
                    turno.Paciente.Usuario.Nombre = datos.Lector["PacienteNombre"].ToString();
                    turno.Paciente.Usuario.Apellido = datos.Lector["PacienteApellido"].ToString();
                    turno.Paciente.Usuario.Telefono = datos.Lector["PacienteTelefono"] != DBNull.Value
                        ? datos.Lector["PacienteTelefono"].ToString() : "";

                    turno.Paciente.ObraSocial = new ObraSocial();
                    turno.Paciente.ObraSocial.Nombre = datos.Lector["ObraSocial"] != DBNull.Value
                        ? datos.Lector["ObraSocial"].ToString() : "-";

                    turno.Medico = new Medico();
                    turno.Medico.Id = (int)datos.Lector["IDMedico"];
                    turno.Medico.Matricula = datos.Lector["Matricula"].ToString();

                    turno.Medico.Usuario = new Usuario();
                    turno.Medico.Usuario.Nombre = datos.Lector["MedicoNombre"].ToString();
                    turno.Medico.Usuario.Apellido = datos.Lector["MedicoApellido"].ToString();

                    turno.Especialidad = new Especialidad();
                    if (datos.Lector["IDEspecialidad"] != DBNull.Value)
                    {
                        turno.Especialidad.Id = (int)datos.Lector["IDEspecialidad"];
                        turno.Especialidad.Nombre = datos.Lector["Especialidad"].ToString();
                    }

                    return turno;
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

        public Turno ObtenerTurnoPorCodigo(string codigo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"
                    SELECT 
                        T.IDTurno,
                        T.Codigo,
                        T.FechaHora,
                        T.IDEstadoTurno,
                        ET.Nombre AS EstadoNombre,
                        P.IDPaciente,
                        U_Pac.Nombre AS PacienteNombre,
                        U_Pac.Apellido AS PacienteApellido,
                        M.IDMedico,
                        U_Med.Nombre AS MedicoNombre,
                        U_Med.Apellido AS MedicoApellido
                    FROM Turnos T
                    INNER JOIN Pacientes P ON T.IDPaciente = P.IDPaciente
                    INNER JOIN Usuarios U_Pac ON P.IDUsuario = U_Pac.IDUsuario
                    INNER JOIN Medicos M ON T.IDMedico = M.IDMedico
                    INNER JOIN Usuarios U_Med ON M.IDUsuario = U_Med.IDUsuario
                    INNER JOIN EstadoTurno ET ON T.IDEstadoTurno = ET.IDEstadoTurno
                    WHERE T.Codigo = @codigo");

                datos.setearParametro("@codigo", codigo);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Turno turno = new Turno();
                    turno.Id = (int)datos.Lector["IDTurno"];
                    turno.Codigo = (string)datos.Lector["Codigo"];
                    turno.FechaHora = (DateTime)datos.Lector["FechaHora"];
                    turno.PacienteId = (int)datos.Lector["IDPaciente"];
                    turno.MedicoId = (int)datos.Lector["IDMedico"];

                    turno.Paciente = new Paciente();
                    turno.Paciente.Usuario = new Usuario();
                    turno.Paciente.Usuario.Nombre = (string)datos.Lector["PacienteNombre"];
                    turno.Paciente.Usuario.Apellido = (string)datos.Lector["PacienteApellido"];

                    turno.Medico = new Medico();
                    turno.Medico.Usuario = new Usuario();
                    turno.Medico.Usuario.Nombre = (string)datos.Lector["MedicoNombre"];
                    turno.Medico.Usuario.Apellido = (string)datos.Lector["MedicoApellido"];

                    turno.EstadoTurno = new EstadoTurno();
                    turno.EstadoTurno.Id = (int)datos.Lector["IDEstadoTurno"];
                    turno.EstadoTurno.Nombre = (string)datos.Lector["EstadoNombre"];

                    return turno;
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

        public bool AgregarTurno(Turno nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"
                    INSERT INTO Turnos
                    (
                        Codigo,
                        IDPaciente,
                        IDMedico,
                        FechaHora,
                        IDEstadoTurno,
                        FechaCreacion
                    )
                    VALUES
                    (
                        @codigo,
                        @idPaciente,
                        @idMedico,
                        @fechaHora,
                        2,
                        GETDATE()
                    )");

                datos.setearParametro("@codigo", nuevo.Codigo);
                datos.setearParametro("@idPaciente", nuevo.PacienteId);
                datos.setearParametro("@idMedico", nuevo.MedicoId);
                datos.setearParametro("@fechaHora", nuevo.FechaHora);

                datos.ejecutarAccion();
                return true;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public bool ModificarTurno(Turno modificado)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"
                    UPDATE Turnos 
                    SET 
                        IDPaciente = @idPaciente, 
                        IDMedico = @idMedico, 
                        FechaHora = @fechaHora, 
                        FechaModificacion = GETDATE() 
                    WHERE IDTurno = @id");

                datos.setearParametro("@idPaciente", modificado.PacienteId);
                datos.setearParametro("@idMedico", modificado.MedicoId);
                datos.setearParametro("@fechaHora", modificado.FechaHora);
                datos.setearParametro("@id", modificado.Id);

                datos.ejecutarAccion();
                return true;
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

        public class ResultadoConfirmacion {
            public bool Exito { get; set; }
            public bool YaEstabaConfirmado { get; set; }
            public string Mensaje { get; set; }
            public Turno Turno { get; set; }
        }

        public ResultadoConfirmacion ConfirmarTurnoPorCodigo(string codigo)
        {
            Turno turno = ObtenerTurnoPorCodigo(codigo);

            if (turno == null)
                return new ResultadoConfirmacion { Exito = false, Mensaje = "No se encontró ningún turno registrado con el código ingresado." };

            if (turno.EstadoTurno != null && turno.EstadoTurno.Nombre.ToLower() == "cancelado")
                return new ResultadoConfirmacion { Exito = false, Mensaje = "Este turno fue cancelado y no puede confirmarse.", Turno = turno };

            if (turno.EstadoTurno != null && turno.EstadoTurno.Nombre.ToLower() == "confirmado")
                return new ResultadoConfirmacion { Exito = true, YaEstabaConfirmado = true, Mensaje = "Este turno ya había sido confirmado anteriormente.", Turno = turno };

            if (turno.FechaHora < DateTime.Now)
                return new ResultadoConfirmacion { Exito = false, Mensaje = "Este turno ya pasó su fecha y hora, por lo que el enlace ya no es válido.", Turno = turno };

            if (turno.FechaCreacion != DateTime.MinValue && (DateTime.Now - turno.FechaCreacion).TotalHours > 48)
                return new ResultadoConfirmacion { Exito = false, Mensaje = "El enlace de confirmación venció (las confirmaciones tienen un plazo de 48 horas).", Turno = turno };

            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"
                    UPDATE Turnos 
                    SET IDEstadoTurno = 2, FechaModificacion = @fechaMod 
                    WHERE Codigo = @codigo AND IDEstadoTurno NOT IN (3)");

                datos.setearParametro("@codigo", codigo);
                datos.setearParametro("@fechaMod", DateTime.Now);
                int filasAfectadas = datos.ejecutarAccion();

                if (filasAfectadas == 0)
                    return new ResultadoConfirmacion { Exito = false, Mensaje = "No se pudo confirmar el turno. Es posible que su estado haya cambiado.", Turno = turno };

                return new ResultadoConfirmacion { Exito = true, Mensaje = "Turno confirmado con éxito.", Turno = turno };
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

        public void ConfirmarTurno(int idTurno)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"
                    UPDATE Turnos
                    SET IDEstadoTurno = (
                        SELECT IDEstadoTurno
                        FROM EstadoTurno
                        WHERE Nombre = 'Confirmado' OR Nombre = 'confirmado'
                    )
                    WHERE IDTurno = @IDTurno");

                datos.setearParametro("@IDTurno", idTurno);
                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public bool CancelarTurno(int idTurno)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Turnos SET IDEstadoTurno = 3, FechaModificacion = @fechaMod WHERE IDTurno = @id");
                datos.setearParametro("@id", idTurno);
                datos.setearParametro("@fechaMod", DateTime.Now);
                datos.ejecutarAccion();
                return true;
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

        public void FinalizarTurno(int idTurno)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Turnos SET IDEstadoTurno = 6, FechaModificacion = @fechaMod WHERE IDTurno = @id");
                datos.setearParametro("@id", idTurno);
                datos.setearParametro("@fechaMod", DateTime.Now);
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

        public List<DateTime> ObtenerHorasOcupadas(int idMedico, DateTime fecha)
        {
            List<DateTime> ocupadas = new List<DateTime>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"
                    SELECT FechaHora 
                    FROM Turnos
                    WHERE IDMedico = @idMedico
                    AND CAST(FechaHora AS DATE) = @fecha
                    AND IDEstadoTurno <> 3");

                datos.setearParametro("@idMedico", idMedico);
                datos.setearParametro("@fecha", fecha.Date);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                    ocupadas.Add((DateTime)datos.Lector["FechaHora"]);

                return ocupadas;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public bool MedicoDisponibleEnFechaHora(int idMedico, DateTime fechaHora)
        {
            DisponibilidadMedicoNegocio dispNegocio = new DisponibilidadMedicoNegocio();
            AusenciaMedicoNegocio ausenciaNegocio = new AusenciaMedicoNegocio();

            if (ausenciaNegocio.TieneAusencia(idMedico, fechaHora.Date))
                return false;

            List<DisponibilidadMedico> disponibilidades = dispNegocio.ListarDisponibilidadesPorMedico(idMedico);
            bool medicoTieneConfiguracion = disponibilidades.Any(d => d.Activo);

            if (!medicoTieneConfiguracion)
                return false;

            int diaSemanaSistema = ((int)fechaHora.DayOfWeek == 0) ? 7 : (int)fechaHora.DayOfWeek;
            TimeSpan horaConsulta = fechaHora.TimeOfDay;

            return disponibilidades.Any(d =>
                d.Activo &&
                d.DiaSemana == diaSemanaSistema &&
                horaConsulta >= d.HoraInicio &&
                horaConsulta < d.HoraFin
            );
        }

        public bool PacienteDisponibleEnFechaHora(int idPaciente, DateTime fechaHora, int idTurnoActual = 0)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"
                    SELECT COUNT(*) 
                    FROM Turnos 
                    WHERE IDPaciente = @idPaciente 
                    AND FechaHora = @fechaHora 
                    AND IDEstadoTurno <> 3
                    AND IDTurno <> @idTurnoActual");

                datos.setearParametro("@idPaciente", idPaciente);
                datos.setearParametro("@fechaHora", fechaHora);
                datos.setearParametro("@idTurnoActual", idTurnoActual);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                    return Convert.ToInt32(datos.Lector[0]) == 0;

                return true;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


        // ─── Dashboard ────────────────────────────────────────────────────────

        public int CantidadTurnosHoy()
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"SELECT COUNT(*) AS Total FROM Turnos WHERE CAST(FechaHora AS DATE) = CAST(GETDATE() AS DATE)");
                datos.ejecutarLectura();
                if (datos.Lector.Read()) return (int)datos.Lector["Total"];
                return 0;
            }
            catch (Exception ex) { throw ex; }
            finally { datos.cerrarConexion(); }
        }

        public int CantidadPendientes()
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"SELECT COUNT(*) AS Total FROM Turnos T INNER JOIN EstadoTurno E ON T.IDEstadoTurno = E.IDEstadoTurno WHERE E.Nombre = 'Pendiente'");
                datos.ejecutarLectura();
                if (datos.Lector.Read()) return (int)datos.Lector["Total"];
                return 0;
            }
            catch (Exception ex) { throw ex; }
            finally { datos.cerrarConexion(); }
        }

        public int CantidadConfirmados()
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"SELECT COUNT(*) AS Total FROM Turnos T INNER JOIN EstadoTurno E ON T.IDEstadoTurno = E.IDEstadoTurno WHERE E.Nombre = 'Confirmado'");
                datos.ejecutarLectura();
                if (datos.Lector.Read()) return (int)datos.Lector["Total"];
                return 0;
            }
            catch (Exception ex) { throw ex; }
            finally { datos.cerrarConexion(); }
        }

        public int CantidadCanceladosHoy()
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"SELECT COUNT(*) AS Total FROM Turnos T INNER JOIN EstadoTurno E ON T.IDEstadoTurno = E.IDEstadoTurno WHERE E.Nombre = 'Cancelado' AND CAST(T.FechaHora AS DATE) = CAST(GETDATE() AS DATE)");
                datos.ejecutarLectura();
                if (datos.Lector.Read()) return (int)datos.Lector["Total"];
                return 0;
            }
            catch (Exception ex) { throw ex; }
            finally { datos.cerrarConexion(); }
        }

        public List<Turno> ListarTurnosHoy()
        {
            return ListarTurnos()
                .Where(x => x.FechaHora.Date == DateTime.Today)
                .OrderBy(x => x.FechaHora)
                .ToList();
        }

        public int CantidadTurnosConfirmadosHoy()
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"SELECT COUNT(*) AS Total FROM Turnos T INNER JOIN EstadoTurno E ON T.IDEstadoTurno = E.IDEstadoTurno WHERE E.Nombre = 'Confirmado' AND CAST(T.FechaHora AS DATE) = CAST(GETDATE() AS DATE)");
                datos.ejecutarLectura();
                if (datos.Lector.Read()) return (int)datos.Lector["Total"];
                return 0;
            }
            catch (Exception ex) { throw ex; }
            finally { datos.cerrarConexion(); }
        }

        public int CantidadTurnosPendientesHoy()
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"SELECT COUNT(*) AS Total FROM Turnos T INNER JOIN EstadoTurno E ON T.IDEstadoTurno = E.IDEstadoTurno WHERE E.Nombre = 'Pendiente' AND CAST(T.FechaHora AS DATE) = CAST(GETDATE() AS DATE)");
                datos.ejecutarLectura();
                if (datos.Lector.Read()) return (int)datos.Lector["Total"];
                return 0;
            }
            catch (Exception ex) { throw ex; }
            finally { datos.cerrarConexion(); }
        }

        public int CantidadTurnosCreadosHoy()
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"SELECT COUNT(*) AS Total FROM Turnos WHERE CAST(FechaCreacion AS DATE) = CAST(GETDATE() AS DATE)");
                datos.ejecutarLectura();
                if (datos.Lector.Read()) return (int)datos.Lector["Total"];
                return 0;
            }
            catch (Exception ex) { throw ex; }
            finally { datos.cerrarConexion(); }
        }

        public int CantidadTurnosReprogramadosHoy()
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"SELECT COUNT(*) AS Total FROM Turnos T INNER JOIN EstadoTurno E ON T.IDEstadoTurno = E.IDEstadoTurno WHERE E.Nombre = 'Reprogramado' AND CAST(T.FechaModificacion AS DATE) = CAST(GETDATE() AS DATE)");
                datos.ejecutarLectura();
                if (datos.Lector.Read()) return (int)datos.Lector["Total"];
                return 0;
            }
            catch (Exception ex) { throw ex; }
            finally { datos.cerrarConexion(); }
        }

        public int CantidadTurnosHoyMedico(int idMedico)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"SELECT COUNT(*) AS Total FROM Turnos WHERE IDMedico = @idMedico AND CAST(FechaHora AS DATE) = CAST(GETDATE() AS DATE)");
                datos.setearParametro("@idMedico", idMedico);
                datos.ejecutarLectura();
                if (datos.Lector.Read()) return (int)datos.Lector["Total"];
                return 0;
            }
            catch (Exception ex) { throw ex; }
            finally { datos.cerrarConexion(); }
        }

        public int CantidadPendientesMedico(int idMedico)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"SELECT COUNT(*) AS Total FROM Turnos T INNER JOIN EstadoTurno E ON T.IDEstadoTurno = E.IDEstadoTurno WHERE T.IDMedico = @idMedico AND E.Nombre = 'Pendiente'");
                datos.setearParametro("@idMedico", idMedico);
                datos.ejecutarLectura();
                if (datos.Lector.Read()) return (int)datos.Lector["Total"];
                return 0;
            }
            catch (Exception ex) { throw ex; }
            finally { datos.cerrarConexion(); }
        }

        public int CantidadFinalizadosMedico(int idMedico)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"SELECT COUNT(*) AS Total FROM Turnos T INNER JOIN EstadoTurno E ON T.IDEstadoTurno = E.IDEstadoTurno WHERE T.IDMedico = @idMedico AND E.Nombre = 'Finalizado'");
                datos.setearParametro("@idMedico", idMedico);
                datos.ejecutarLectura();
                if (datos.Lector.Read()) return (int)datos.Lector["Total"];
                return 0;
            }
            catch (Exception ex) { throw ex; }
            finally { datos.cerrarConexion(); }
        }

        public int CantidadCanceladosMedico(int idMedico)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"SELECT COUNT(*) AS Total FROM Turnos T INNER JOIN EstadoTurno E ON T.IDEstadoTurno = E.IDEstadoTurno WHERE T.IDMedico = @idMedico AND E.Nombre = 'Cancelado'");
                datos.setearParametro("@idMedico", idMedico);
                datos.ejecutarLectura();
                if (datos.Lector.Read()) return (int)datos.Lector["Total"];
                return 0;
            }
            catch (Exception ex) { throw ex; }
            finally { datos.cerrarConexion(); }
        }

        public int CantidadFinalizadosHoyMedico(int idMedico)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"SELECT COUNT(*) AS Total FROM Turnos T INNER JOIN EstadoTurno E ON T.IDEstadoTurno = E.IDEstadoTurno WHERE T.IDMedico = @idMedico AND E.Nombre = 'Finalizado' AND CAST(T.FechaHora AS DATE) = CAST(GETDATE() AS DATE)");
                datos.setearParametro("@idMedico", idMedico);
                datos.ejecutarLectura();
                if (datos.Lector.Read()) return (int)datos.Lector["Total"];
                return 0;
            }
            catch (Exception ex) { throw ex; }
            finally { datos.cerrarConexion(); }
        }

        public int CantidadPacientesAtendidos(int idMedico)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"SELECT COUNT(DISTINCT IDPaciente) AS Total FROM Turnos T INNER JOIN EstadoTurno E ON T.IDEstadoTurno = E.IDEstadoTurno WHERE T.IDMedico = @idMedico AND E.Nombre = 'Finalizado'");
                datos.setearParametro("@idMedico", idMedico);
                datos.ejecutarLectura();
                if (datos.Lector.Read()) return (int)datos.Lector["Total"];
                return 0;
            }
            catch (Exception ex) { throw ex; }
            finally { datos.cerrarConexion(); }
        }

        public int CantidadAusentesMedico(int idMedico)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"SELECT COUNT(*) AS Total FROM Turnos T INNER JOIN EstadoTurno E ON T.IDEstadoTurno = E.IDEstadoTurno WHERE T.IDMedico = @idMedico AND E.Nombre = 'Ausente'");
                datos.setearParametro("@idMedico", idMedico);
                datos.ejecutarLectura();
                if (datos.Lector.Read()) return (int)datos.Lector["Total"];
                return 0;
            }
            catch (Exception ex) { throw ex; }
            finally { datos.cerrarConexion(); }
        }

        public Turno ObtenerProximoTurnoMedico(int idMedico)
        {
            List<Turno> lista = ListarTurnosPorMedico(idMedico);
            return lista
                .Where(x => x.FechaHora >= DateTime.Now &&
                            (x.EstadoTurno.Nombre == "Pendiente" || x.EstadoTurno.Nombre == "Confirmado"))
                .OrderBy(x => x.FechaHora)
                .FirstOrDefault();
        }

        public List<Turno> ListarAgendaHoyMedico(int idMedico)
        {
            return ListarTurnosPorMedico(idMedico)
                .Where(x => x.FechaHora.Date == DateTime.Today)
                .OrderBy(x => x.FechaHora)
                .ToList();
        }

        public int CantidadPendientesPaciente(int idPaciente)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"SELECT COUNT(*) AS Total FROM Turnos T INNER JOIN EstadoTurno E ON T.IDEstadoTurno = E.IDEstadoTurno WHERE T.IDPaciente = @idPaciente AND E.Nombre = 'Pendiente'");
                datos.setearParametro("@idPaciente", idPaciente);
                datos.ejecutarLectura();
                if (datos.Lector.Read()) return (int)datos.Lector["Total"];
                return 0;
            }
            catch (Exception ex) { throw ex; }
            finally { datos.cerrarConexion(); }
        }

        public int CantidadFinalizadosPaciente(int idPaciente)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"SELECT COUNT(*) AS Total FROM Turnos T INNER JOIN EstadoTurno E ON T.IDEstadoTurno = E.IDEstadoTurno WHERE T.IDPaciente = @idPaciente AND E.Nombre = 'Finalizado'");
                datos.setearParametro("@idPaciente", idPaciente);
                datos.ejecutarLectura();
                if (datos.Lector.Read()) return (int)datos.Lector["Total"];
                return 0;
            }
            catch (Exception ex) { throw ex; }
            finally { datos.cerrarConexion(); }
        }

        public string ObtenerFechaProximoControl(int idPaciente)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"SELECT TOP 1 FechaHora FROM Turnos WHERE IDPaciente = @idPaciente AND FechaHora >= GETDATE() ORDER BY FechaHora");
                datos.setearParametro("@idPaciente", idPaciente);
                datos.ejecutarLectura();
                if (datos.Lector.Read())
                    return ((DateTime)datos.Lector["FechaHora"]).ToString("dd/MM/yyyy");
                return "Sin turno";
            }
            catch (Exception ex) { throw ex; }
            finally { datos.cerrarConexion(); }
        }

        public Turno ObtenerProximoTurnoPaciente(int idPaciente)
        {
            return ListarTurnosPorPaciente(idPaciente)
                .Where(x => x.FechaHora >= DateTime.Now)
                .OrderBy(x => x.FechaHora)
                .FirstOrDefault();
        }

        public List<Turno> ListarUltimosTurnosPaciente(int idPaciente)
        {
            return ListarTurnosPorPaciente(idPaciente)
                .OrderByDescending(x => x.FechaHora)
                .Take(10)
                .ToList();
        }

        public int CantidadTurnosPaciente(int idPaciente)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"SELECT COUNT(*) AS Total FROM Turnos WHERE IDPaciente = @idPaciente");
                datos.setearParametro("@idPaciente", idPaciente);
                datos.ejecutarLectura();
                if (datos.Lector.Read()) return (int)datos.Lector["Total"];
                return 0;
            }
            catch (Exception ex) { throw ex; }
            finally { datos.cerrarConexion(); }
        }
    }
}