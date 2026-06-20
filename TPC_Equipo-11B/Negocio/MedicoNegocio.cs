using Datos;
using Dominio;
using System;
using System.Collections.Generic;

namespace Negocio
{
    public class MedicoNegocio
    {
        // Listado
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
                    "ORDER BY M.Activo DESC, U.Apellido, U.Nombre");
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
        public Medico ObtenerMedicoPorId(int idMedico)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(
                    "SELECT M.IDMedico, M.IDUsuario, M.Matricula, M.Activo, " +
                    "U.Nombre, U.Apellido, U.Email, U.Telefono, U.Username, U.ImagenUrl, " +
                    "E.IDEspecialidad, E.Nombre AS Especialidad " +
                    "FROM Medicos M " +
                    "INNER JOIN Usuarios U ON M.IDUsuario = U.IDUsuario " +
                    "INNER JOIN MedicoEspecialidad ME ON M.IDMedico = ME.IDMedico " +
                    "INNER JOIN Especialidades E ON ME.IDEspecialidad = E.IDEspecialidad " +
                    "WHERE M.IDMedico = @idMedico");

                datos.setearParametro("@idMedico", idMedico);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Medico medico = new Medico();

                    medico.Id = (int)datos.Lector["IDMedico"];
                    medico.UsuarioId = (int)datos.Lector["IDUsuario"];
                    medico.Matricula = (string)datos.Lector["Matricula"];
                    medico.Activo = (bool)datos.Lector["Activo"];

                    medico.Usuario = new Usuario();
                    medico.Usuario.Id = (int)datos.Lector["IDUsuario"];
                    medico.Usuario.Nombre = (string)datos.Lector["Nombre"];
                    medico.Usuario.Apellido = (string)datos.Lector["Apellido"];
                    medico.Usuario.Email = (string)datos.Lector["Email"];
                    medico.Usuario.Username = (string)datos.Lector["Username"];

                    medico.Usuario.Telefono =
                        datos.Lector["Telefono"] != DBNull.Value
                        ? (string)datos.Lector["Telefono"] : "";
                   
                    medico.Usuario.ImagenUrl =
                        datos.Lector["ImagenUrl"] != DBNull.Value
                        ? (string)datos.Lector["ImagenUrl"] : "";


                    medico.Especialidad = new Especialidad();
                    medico.Especialidad.Id = (int)datos.Lector["IDEspecialidad"];
                    medico.Especialidad.Nombre = (string)datos.Lector["Especialidad"];

                    return medico;
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

        // Alta
        public void RegistrarMedico(Medico medico)
        {
            ValidarAlta(medico);

            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();

            int idUsuario = usuarioNegocio.RegistrarUsuario(medico.Usuario);
            int idMedico = AgregarRegistroMedico(idUsuario, medico.Matricula);

            AgregarEspecialidad(idMedico, medico.Especialidad.Id);
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
        public void ReactivarMedico(int idMedico)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"UPDATE Medicos SET Activo = 1 WHERE IDMedico = @id");
                datos.setearParametro("@id", idMedico);
                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        // Modificación
        public void ModificarMedico(Medico medico)
        {
            ValidarModificacion(medico);

            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
            usuarioNegocio.ModificarUsuario(medico.Usuario);
            ModificarRegistroMedico(medico);

            ModificarEspecialidad(medico.Id, medico.Especialidad.Id);
        }
        private void ModificarRegistroMedico(Medico medico)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Medicos SET Matricula = @matricula WHERE IDMedico = @idMedico");

                datos.setearParametro("@idMedico", medico.Id);
                datos.setearParametro("@matricula", medico.Matricula);

                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        private void ModificarEspecialidad(int idMedico, int idEspecialidad)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE MedicoEspecialidad SET IDEspecialidad = @idEspecialidad WHERE IDMedico = @idMedico");

                datos.setearParametro("@idMedico", idMedico);
                datos.setearParametro("@idEspecialidad", idEspecialidad);

                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        // Baja lógica
        public void EliminarMedico(int idMedico)
        {
            ValidarEliminacion(idMedico);

            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Medicos SET Activo = 0 WHERE IDMedico = @idMedico");
                datos.setearParametro("@idMedico", idMedico);
                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        // Validaciones
        private void ValidarAlta(Medico medico)
        {
            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();

            usuarioNegocio.ValidarAlta(medico.Usuario);

            if (string.IsNullOrWhiteSpace(medico.Matricula))
                throw new Exception("Debe ingresar la matrícula.");

            if (ExisteMatricula(medico.Matricula))
                throw new Exception("La matrícula ya está registrada.");

            if (medico.Especialidad == null || medico.Especialidad.Id == 0)
                throw new Exception("Debe seleccionar una especialidad.");
        }
        private void ValidarModificacion(Medico medico)
        {
            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();

            usuarioNegocio.ValidarModificacion(medico.Usuario);

            if (string.IsNullOrWhiteSpace(medico.Matricula))
                throw new Exception("Debe ingresar la matrícula.");

            if (ExisteMatricula(medico.Matricula, medico.Id))
                throw new Exception("La matrícula ya está registrada.");

            if (medico.Especialidad == null || medico.Especialidad.Id == 0)
                throw new Exception("Debe seleccionar una especialidad.");
        }
        private void ValidarEliminacion(int idMedico)
        {
            Medico medico = ObtenerMedicoPorId(idMedico);

            if (medico == null)
                throw new Exception("El médico no existe.");

            if (!medico.Activo)
                throw new Exception("El médico ya se encuentra inactivo.");

            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                    SELECT COUNT(*)
                    FROM Turnos
                    WHERE IDMedico = @idMedico
                    AND FechaHora >= GETDATE()");

                datos.setearParametro("@idMedico", idMedico);
                datos.ejecutarLectura();

                datos.Lector.Read();
                int cantidad = Convert.ToInt32(datos.Lector[0]);

                if (cantidad > 0)
                    throw new Exception("No se puede eliminar el médico porque tiene turnos activos o futuros.");
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        private bool ExisteMatricula(string matricula)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(
                    "SELECT IDMedico " +
                    "FROM Medicos " +
                    "WHERE Matricula = @matricula");

                datos.setearParametro("@matricula", matricula);

                datos.ejecutarLectura();

                return datos.Lector.Read();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        private bool ExisteMatricula(string matricula, int idMedico)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(
                    "SELECT IDMedico " +
                    "FROM Medicos " +
                    "WHERE Matricula = @matricula " +
                    "AND IDMedico <> @idMedico");

                datos.setearParametro("@matricula", matricula);
                datos.setearParametro("@idMedico", idMedico);

                datos.ejecutarLectura();

                return datos.Lector.Read();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }

}
