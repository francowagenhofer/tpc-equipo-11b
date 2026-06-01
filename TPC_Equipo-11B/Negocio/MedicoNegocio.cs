using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using Datos;


namespace Negocio
{
    public class MedicoNegocio
    {

        public List<Medico> ListarMedicos()
        {

            List<Medico> lista = new List<Medico>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(
                    "SELECT M.IDMedico, M.IDUsuario, M.Matricula, M.Activo, " +
                    "U.Nombre, U.Apellido, U.Email, U.Telefono, " +
                    "E.IDEspecialidad, E.Nombre AS Especialidad " +
                    "FROM Medicos M " +
                    "INNER JOIN Usuarios U ON M.IDUsuario = U.IDUsuario " +
                    "INNER JOIN MedicoEspecialidad ME ON M.IDMedico = ME.IDMedico " +
                    "INNER JOIN Especialidades E ON ME.IDEspecialidad = E.IDEspecialidad " +
                    "WHERE M.Activo = 1");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Medico aux = new Medico();

                    aux.Id = (int)datos.Lector["IDMedico"];
                    aux.UsuarioId = (int)datos.Lector["IDUsuario"];
                    aux.Matricula = (string)datos.Lector["Matricula"];
                    aux.Activo = (bool)datos.Lector["Activo"];

                    aux.Usuario = new Usuario();
                    aux.Usuario.Id = (int)datos.Lector["IDUsuario"];
                    aux.Usuario.Nombre = (string)datos.Lector["Nombre"];
                    aux.Usuario.Apellido = (string)datos.Lector["Apellido"];
                    aux.Usuario.Email = (string)datos.Lector["Email"];
                    aux.Usuario.Telefono = datos.Lector["Telefono"] != DBNull.Value
                        ? (string)datos.Lector["Telefono"]
                        : "";

                    aux.Especialidad = new Especialidad();
                    aux.Especialidad.Id = (int)datos.Lector["IDEspecialidad"];
                    aux.Especialidad.Nombre = (string)datos.Lector["Especialidad"];

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

        //public bool AgregarMedico(Medico nuevoMedico) { 

        //	AccesoDatos datos = new AccesoDatos();
        //	try
        //	{
        //		datos.setearConsulta("INSERT INTO Usuarios (Nombre, Apellido, Email, Telefono, Username, PasswordHash, IDRol, Activo) VALUES (@nombre, @apellido, @email, @telefono, @username, @passHash, 3, 1)");
        //              datos.setearParametro("@nombre", nuevoMedico.Usuario.Nombre);
        //              datos.setearParametro("@apellido", nuevoMedico.Usuario.Apellido);
        //              datos.setearParametro("@email", nuevoMedico.Usuario.Email);
        //              datos.setearParametro("@telefono", string.IsNullOrEmpty(nuevoMedico.Usuario.Telefono) ? (object)DBNull.Value : nuevoMedico.Usuario.Telefono);
        //              datos.setearParametro("@username", nuevoMedico.Usuario.Username);
        //              datos.setearParametro("@passHash", nuevoMedico.Usuario.Password);

        //		datos.ejecutarAccion();
        //		datos.cerrarConexion();

        //		int idUsuarioGenerado = 0;
        //		datos = new AccesoDatos();
        //		datos.setearConsulta("SELECT IDUsuario FROM Usuarios WHERE Username = @username");
        //		datos.setearParametro("@username", nuevoMedico.Usuario.Username);
        //		datos.ejecutarLectura();

        //		if (datos.Lector.Read()) {

        //			idUsuarioGenerado = (int)datos.Lector["IDUsuario"];

        //		}
        //		datos.cerrarConexion();

        //		if (idUsuarioGenerado == 0) 
        //			return false;

        //		datos = new AccesoDatos();
        //		datos.setearConsulta("INSERT INTO Medicos (IDUsuario, Matricula, Activo) VALUES (@idUsuario, @matricula, 1)");
        //		datos.setearParametro("@idUsuario", idUsuarioGenerado);
        //		datos.setearParametro("@matricula", nuevoMedico.Matricula);

        //		datos.ejecutarAccion();
        //		return true;


        //          }
        //	catch (Exception ex)
        //	{

        //		throw ex;
        //	}
        //	finally 
        //	{
        //		datos.cerrarConexion();
        //	}
        //      }

        public bool AgregarMedico(Medico medico)
        {
            try
            {
                int idUsuario = AgregarUsuario(medico.Usuario);
                int idMedico = AgregarRegistroMedico(idUsuario, medico.Matricula);
                AgregarEspecialidad(idMedico, medico.Especialidad.Id);

                return true;
            }
            catch
            {
                throw;
            }
        }

        private int AgregarUsuario(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"INSERT INTO Usuarios (Nombre, Apellido, Email, Telefono, Username, PasswordHash, IDRol, Activo)
                                        VALUES (@nombre, @apellido, @email, @telefono, @username, @passHash, 3, 1);
                                        SELECT SCOPE_IDENTITY(); ");

                datos.setearParametro("@nombre", usuario.Nombre);
                datos.setearParametro("@apellido", usuario.Apellido);
                datos.setearParametro("@email", usuario.Email);
                datos.setearParametro("@telefono",
                    string.IsNullOrEmpty(usuario.Telefono)
                    ? (object)DBNull.Value
                    : usuario.Telefono);

                datos.setearParametro("@username", usuario.Username);
                datos.setearParametro("@passHash", usuario.Password);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                    return Convert.ToInt32(datos.Lector[0]);

                throw new Exception("No se pudo obtener el ID del usuario.");
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        private int AgregarRegistroMedico(int idUsuario, string matricula)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO Medicos (IDUsuario, Matricula, Activo) VALUES (@idUsuario, @matricula, 1); SELECT SCOPE_IDENTITY();");

                datos.setearParametro("@idUsuario", idUsuario);
                datos.setearParametro("@matricula", matricula);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                    return Convert.ToInt32(datos.Lector[0]);

                throw new Exception("No se pudo obtener el ID del médico.");
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        private void AgregarEspecialidad(int idMedico, int idEspecialidad)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO MedicoEspecialidad (IDMedico, IDEspecialidad) VALUES (@idMedico, @idEspecialidad)");

                datos.setearParametro("@idMedico", idMedico);
                datos.setearParametro("@idEspecialidad", idEspecialidad);

                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
