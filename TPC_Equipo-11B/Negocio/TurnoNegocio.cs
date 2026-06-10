using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using Datos;

namespace Negocio
{
    public class TurnoNegocio
    {

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

                    // Paciente
                    aux.Paciente = new Paciente();
                    aux.Paciente.Id = (int)datos.Lector["IDPaciente"];
                    aux.Paciente.DNI = (string)datos.Lector["DNI"];

                    aux.Paciente.Usuario = new Usuario();
                    aux.Paciente.Usuario.Nombre = (string)datos.Lector["PacienteNombre"];
                    aux.Paciente.Usuario.Apellido = (string)datos.Lector["PacienteApellido"];

                    // Medico
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

        public bool AgregarTurno(Turno nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"
                    INSERT INTO Turnos (Codigo, IDPaciente, IDMedico, FechaHora, IDEstadoTurno, FechaCreacion) 
                    VALUES (@codigo, @idPaciente, @idMedico, @fechaHora, 1, GETDATE())");

                datos.setearParametro("@codigo", nuevo.Codigo);
                datos.setearParametro("@idPaciente", nuevo.PacienteId);
                datos.setearParametro("@idMedico", nuevo.MedicoId);
                datos.setearParametro("@fechaHora", nuevo.FechaHora);

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

    }
}



