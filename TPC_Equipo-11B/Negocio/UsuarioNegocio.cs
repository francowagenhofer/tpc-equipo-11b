using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using Datos;

namespace Negocio
{

    public class UsuarioNegocio
    {
        public Usuario ValidarLogin(string username, string password)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                                    SELECT
                                        u.IDUsuario,
                                        u.Nombre,
                                        u.Apellido,
                                        u.Email,
                                        u.Telefono,
                                        u.Username,
                                        u.PasswordHash,
                                        u.ImagenUrl,
                                        u.FechaAlta,
                                        u.IDRol,
                                        u.Activo,
                                        r.Nombre AS NombreRol
                                    FROM Usuarios u
                                    INNER JOIN Roles r
                                        ON u.IDRol = r.IDRol
                                    WHERE u.Username = @username
                                      AND u.PasswordHash = @password
                                      AND u.Activo = 1
                                      AND r.Activo = 1");

                datos.setearParametro("@username", username);
                datos.setearParametro("@password", password);

                datos.ejecutarLectura();

                if (!datos.Lector.Read())
                    return null;

                Usuario usuario = new Usuario();

                usuario.Id = (int)datos.Lector["IDUsuario"];
                usuario.Nombre = datos.Lector["Nombre"].ToString();
                usuario.Apellido = datos.Lector["Apellido"].ToString();
                usuario.Email = datos.Lector["Email"].ToString();
                usuario.Telefono = datos.Lector["Telefono"] != DBNull.Value ? datos.Lector["Telefono"].ToString() : "";
                usuario.Username = datos.Lector["Username"].ToString();
                usuario.Password = datos.Lector["PasswordHash"].ToString();
                usuario.ImagenUrl = datos.Lector["ImagenUrl"] != DBNull.Value ? datos.Lector["ImagenUrl"].ToString() : "";
                usuario.FechaAlta = (DateTime)datos.Lector["FechaAlta"];
                usuario.Activo = (bool)datos.Lector["Activo"];

                usuario.Rol = new Rol();
                usuario.Rol.Id = (int)datos.Lector["IDRol"];
                usuario.Rol.Nombre = datos.Lector["NombreRol"].ToString();

                return usuario;
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

        // Listado
        public List<Usuario> ListarUsuarios()
        {
            List<Usuario> lista = new List<Usuario>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                //datos.setearConsulta("SELECT IDUsuario, Nombre, Apellido, Email, Telefono, Username, PasswordHash, IdRol, ImagenUrl, FechaAlta, Activo FROM Usuarios");
                datos.setearConsulta("SELECT u.IDUsuario, u.Nombre, u.Apellido, u.Email,u. Telefono, u.Username, u.PasswordHash, u.IdRol, u.ImagenUrl, u.FechaAlta, u.Activo, r.Nombre AS Rol " +
                    "FROM Usuarios u INNER JOIN Roles r ON u.IdRol = r.IDRol");

                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Usuario aux = new Usuario();
                    aux.Id = (int)datos.Lector["IDUsuario"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Apellido = (string)datos.Lector["Apellido"];
                    aux.Email = (string)datos.Lector["Email"];
                    aux.Telefono = datos.Lector["Telefono"] != DBNull.Value ? (string)datos.Lector["Telefono"] : "";
                    aux.Username = (string)datos.Lector["Username"];
                    aux.Password = (string)datos.Lector["PasswordHash"];
                    aux.ImagenUrl = datos.Lector["ImagenUrl"] != DBNull.Value ? (string)datos.Lector["ImagenUrl"] : "";
                    aux.FechaAlta = (DateTime)datos.Lector["FechaAlta"];
                    aux.RolId = (int)datos.Lector["IDRol"];
                    aux.Activo = (bool)datos.Lector["Activo"];

                    Rol auxRol = new Rol();
                    auxRol.Id = (int)datos.Lector["IDRol"];
                    auxRol.Nombre = (string)datos.Lector["Rol"];
                    aux.Rol = auxRol;

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
        public Usuario ObtenerUsuarioPorId(int idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT IDUsuario, Nombre, Apellido, Email, Telefono, Username, PasswordHash, IdRol, ImagenUrl, FechaAlta, Activo FROM Usuarios WHERE IDUsuario = @id");
                datos.setearParametro("@id", idUsuario);
                datos.ejecutarLectura();
                if (datos.Lector.Read())
                {
                    Usuario aux = new Usuario();
                    aux.Id = (int)datos.Lector["IDUsuario"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Apellido = (string)datos.Lector["Apellido"];
                    aux.Email = (string)datos.Lector["Email"];
                    aux.Telefono = datos.Lector["Telefono"] != DBNull.Value ? (string)datos.Lector["Telefono"] : "";
                    aux.Username = (string)datos.Lector["Username"];
                    aux.Password = (string)datos.Lector["PasswordHash"];
                    aux.ImagenUrl = datos.Lector["ImagenUrl"] != DBNull.Value ? (string)datos.Lector["ImagenUrl"] : "";
                    aux.FechaAlta = (DateTime)datos.Lector["FechaAlta"];
                    aux.RolId = (int)datos.Lector["IDRol"];
                    aux.Activo = (bool)datos.Lector["Activo"];
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

        // Alta
        public int RegistrarUsuario(Usuario nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"INSERT INTO Usuarios (Nombre, Apellido, Email, Telefono, Username, ImagenUrl, PasswordHash, IDRol, Activo)
                                        VALUES (@nombre, @apellido, @email, @telefono, @username, @imagenUrl, @passHash, @idRol, 1); 
                                        SELECT SCOPE_IDENTITY(); ");

                datos.setearParametro("@nombre", nuevo.Nombre);
                datos.setearParametro("@apellido", nuevo.Apellido);
                datos.setearParametro("@email", nuevo.Email);
                datos.setearParametro("@telefono", string.IsNullOrEmpty(nuevo.Telefono) ? (object)DBNull.Value : nuevo.Telefono);
                datos.setearParametro("@imagenUrl", string.IsNullOrWhiteSpace(nuevo.ImagenUrl) ? (object)DBNull.Value : nuevo.ImagenUrl);
                datos.setearParametro("@username", nuevo.Username);
                datos.setearParametro("@passHash", nuevo.Password);
                datos.setearParametro("@idRol", nuevo.RolId);

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
        public void ReactivarUsuario(int idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Usuarios SET Activo = 1 WHERE IDUsuario = @id");
                datos.setearParametro("@id", idUsuario);
                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        // Modificación
        public void ModificarUsuario(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                    UPDATE Usuarios 
                    SET 
                        Nombre = @nombre, 
                        Apellido = @apellido, 
                        Email = @email, 
                        Telefono = @telefono, 
                        ImagenUrl = @imagenUrl, 
                        Username = @username 
                    WHERE IDUsuario = @idUsuario");

                datos.setearParametro("@idUsuario", usuario.Id);
                datos.setearParametro("@nombre", usuario.Nombre);
                datos.setearParametro("@apellido", usuario.Apellido);
                datos.setearParametro("@email", usuario.Email);
                datos.setearParametro("@telefono", string.IsNullOrEmpty(usuario.Telefono) ? (object)DBNull.Value : usuario.Telefono);
                datos.setearParametro("@imagenUrl", string.IsNullOrWhiteSpace(usuario.ImagenUrl) ? (object)DBNull.Value : usuario.ImagenUrl);
                datos.setearParametro("@username", usuario.Username);


                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void CambiarContraseña(int idUsuario, string passwordActual, string passwordNueva)
        {
            Usuario usuario = ObtenerUsuarioPorId(idUsuario);

            if (usuario == null)
                throw new Exception("El usuario no existe.");

            if (usuario.Password != passwordActual)
                throw new Exception("La contraseña actual es incorrecta.");

            if (string.IsNullOrWhiteSpace(passwordNueva))
                throw new Exception("Debe ingresar una nueva contraseña.");

            if (passwordNueva.Length < 4)
                throw new Exception("La nueva contraseña debe tener al menos 4 caracteres.");

            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"UPDATE Usuarios SET PasswordHash = @passwordNueva WHERE IDUsuario = @idUsuario");
                datos.setearParametro("@passwordNueva", passwordNueva);
                datos.setearParametro("@idUsuario", idUsuario);

                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        // Para administradores
        public void ResetearContraseña(int idUsuario, string passwordNueva)
        {
            if (string.IsNullOrWhiteSpace(passwordNueva))
                throw new Exception("Debe ingresar una nueva contraseña.");

            if (passwordNueva.Length < 4)
                throw new Exception("La nueva contraseña debe tener al menos 4 caracteres.");

            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@" UPDATE Usuarios SET PasswordHash = @passwordNueva WHERE IDUsuario = @idUsuario");

                datos.setearParametro("@passwordNueva", passwordNueva);
                datos.setearParametro("@idUsuario", idUsuario);

                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void CambiarRolUsuario(int idUsuario, int nuevoRolId, Usuario usuarioLogueado)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                if (usuarioLogueado.RolId != 1)
                    throw new Exception("No tiene permisos para cambiar el rol del usuario.");

                if (nuevoRolId <= 0)
                    throw new Exception("Rol inválido.");

                datos.setearConsulta(@" UPDATE Usuarios SET IDRol = @nuevoRol WHERE IDUsuario = @idUsuario");

                datos.setearParametro("@nuevoRol", nuevoRolId);
                datos.setearParametro("@idUsuario", idUsuario);

                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        // Baja lógica
        public void EliminarUsuario(int idUsuario)
        {
            ValidarEliminacion(idUsuario);

            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Usuarios SET Activo = 0 WHERE IDUsuario = @idUsuario");
                datos.setearParametro("@idUsuario", idUsuario);
                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        // Validaciones
        public void ValidarAlta(Usuario usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.Nombre))
                throw new Exception("Debe ingresar el nombre.");

            if (string.IsNullOrWhiteSpace(usuario.Apellido))
                throw new Exception("Debe ingresar el apellido.");

            if (string.IsNullOrWhiteSpace(usuario.Email))
                throw new Exception("Debe ingresar el email.");

            if (string.IsNullOrWhiteSpace(usuario.Username))
                throw new Exception("Debe ingresar un nombre de usuario.");

            if (string.IsNullOrWhiteSpace(usuario.Password))
                throw new Exception("Debe ingresar una contraseña.");

            if (ExisteUsername(usuario.Username))
                throw new Exception("El nombre de usuario ya existe.");

            if (ExisteEmail(usuario.Email))
                throw new Exception("El email ya está registrado.");
        }
        public void ValidarModificacion(Usuario usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.Nombre))
                throw new Exception("Debe ingresar el nombre.");

            if (string.IsNullOrWhiteSpace(usuario.Apellido))
                throw new Exception("Debe ingresar el apellido.");

            if (string.IsNullOrWhiteSpace(usuario.Username))
                throw new Exception("Debe ingresar un nombre de usuario.");

            if (string.IsNullOrWhiteSpace(usuario.Email))
                throw new Exception("Debe ingresar un email.");

            if (ExisteUsername(usuario.Username, usuario.Id))
                throw new Exception("El nombre de usuario ya existe.");

            if (ExisteEmail(usuario.Email, usuario.Id))
                throw new Exception("El email ya está registrado.");
        }
        public void ValidarEliminacion(int idUsuario)
        {
            Usuario usuario = ObtenerUsuarioPorId(idUsuario);

            if (usuario == null)
                throw new Exception("El usuario no existe.");

            if (!usuario.Activo)
                throw new Exception("El usuario ya se encuentra inactivo.");
        }

        private bool ExisteUsername(string username)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(
                    "SELECT IDUsuario " +
                    "FROM Usuarios " +
                    "WHERE Username = @username");

                datos.setearParametro("@username", username);

                datos.ejecutarLectura();

                return datos.Lector.Read();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        private bool ExisteEmail(string email)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(
                    "SELECT IDUsuario " +
                    "FROM Usuarios " +
                    "WHERE Email = @email");

                datos.setearParametro("@email", email);

                datos.ejecutarLectura();

                return datos.Lector.Read();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        private bool ExisteUsername(string username, int idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(
                    "SELECT IDUsuario " +
                    "FROM Usuarios " +
                    "WHERE Username = @username " +
                    "AND IDUsuario <> @idUsuario");

                datos.setearParametro("@username", username);
                datos.setearParametro("@idUsuario", idUsuario);

                datos.ejecutarLectura();

                return datos.Lector.Read();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        private bool ExisteEmail(string email, int idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(
                    "SELECT IDUsuario " +
                    "FROM Usuarios " +
                    "WHERE Email = @email " +
                    "AND IDUsuario <> @idUsuario");

                datos.setearParametro("@email", email);
                datos.setearParametro("@idUsuario", idUsuario);

                datos.ejecutarLectura();

                return datos.Lector.Read();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        // Validación de Email
        public bool EmailRegistrado(string email)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("Select count(*) from Usuarios where Email = @email");
                datos.setearParametro("@email", email);

                int count = (int)datos.ejecutarEscalar();
                return count > 0;
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
