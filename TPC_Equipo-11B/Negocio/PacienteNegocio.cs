using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using Datos;

namespace Negocio {
    public class PacienteNegocio {
        // Listado
        public List<Paciente> ListarPacientes(bool soloActivos = true)
        {
            List<Paciente> lista = new List<Paciente>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                      SELECT
                          P.IDPaciente,
                          P.IDUsuario,
                          P.DNI,
                          P.FechaNacimiento,
                          P.Direccion,
                          P.IDGenero,
                          P.IDObraSocial,
                          P.Activo,
                      
                          U.Nombre,
                          U.Apellido,
                          U.Email,
                          U.Telefono,
                          U.Username,
                          U.ImagenUrl,
                      
                          G.Descripcion AS Genero,
                          OS.Nombre AS ObraSocial
                      
                      FROM Pacientes P
                      
                      INNER JOIN Usuarios U
                          ON P.IDUsuario = U.IDUsuario
                      
                      LEFT JOIN Generos G
                          ON P.IDGenero = G.IDGenero
                      
                      LEFT JOIN ObrasSociales OS
                          ON P.IDObraSocial = OS.IDObraSocial
                     
                     WHERE (@soloActivos = 0 OR P.Activo = 1)
                      
                      ORDER BY U.Apellido, U.Nombre");
                datos.setearParametro("@soloActivos", soloActivos ? 1 : 0);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Paciente aux = new Paciente();

                    aux.Id = (int)datos.Lector["IDPaciente"];
                    aux.UsuarioId = (int)datos.Lector["IDUsuario"];
                    aux.DNI = (string)datos.Lector["DNI"];
                    aux.FechaNacimiento = (DateTime)datos.Lector["FechaNacimiento"];
                    aux.Direccion = datos.Lector["Direccion"] != DBNull.Value ? (string)datos.Lector["Direccion"] : "";

                    if (datos.Lector["IDGenero"] != DBNull.Value)
                    {
                        aux.Genero = new Genero();
                        aux.Genero.Id = (int)datos.Lector["IDGenero"];
                        aux.Genero.Descripcion = (string)datos.Lector["Genero"];
                    }

                    if (datos.Lector["IDObraSocial"] != DBNull.Value)
                    {
                        aux.ObraSocial = new ObraSocial();
                        aux.ObraSocial.Id = (int)datos.Lector["IDObraSocial"];
                        aux.ObraSocial.Nombre = (string)datos.Lector["ObraSocial"];
                    }
                    aux.Activo = (bool)datos.Lector["Activo"];

                    aux.Usuario = new Usuario();
                    aux.Usuario.Id = aux.UsuarioId;
                    aux.Usuario.Nombre = (string)datos.Lector["Nombre"];
                    aux.Usuario.Apellido = (string)datos.Lector["Apellido"];
                    aux.Usuario.Email = (string)datos.Lector["Email"];
                    aux.Usuario.Telefono = datos.Lector["Telefono"] != DBNull.Value ? (string)datos.Lector["Telefono"] : "";
                    aux.Usuario.Username = (string)datos.Lector["Username"];
                    aux.Usuario.ImagenUrl = datos.Lector["ImagenUrl"] != DBNull.Value ? (string)datos.Lector["ImagenUrl"] : "";

                    lista.Add(aux);
                }

                return lista;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public Paciente ObtenerPacientePorId(int idPaciente)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                      SELECT
                          P.IDPaciente,
                          P.IDUsuario,
                          P.DNI,
                          P.FechaNacimiento,
                          P.Direccion,
                          P.IDGenero,
                          P.IDObraSocial,
                          P.Activo,
                      
                          U.Nombre,
                          U.Apellido,
                          U.Email,
                          U.Telefono,
                          U.Username,
                          U.ImagenUrl,
                      
                          G.Descripcion AS Genero,
                          OS.Nombre AS ObraSocial
                      
                      FROM Pacientes P
                      
                      INNER JOIN Usuarios U
                          ON P.IDUsuario = U.IDUsuario
                      
                      LEFT JOIN Generos G
                          ON P.IDGenero = G.IDGenero
                      
                      LEFT JOIN ObrasSociales OS
                          ON P.IDObraSocial = OS.IDObraSocial

                     WHERE P.IDPaciente = @id");

                datos.setearParametro("@id", idPaciente);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Paciente aux = new Paciente();

                    aux.Id = (int)datos.Lector["IDPaciente"];
                    aux.UsuarioId = (int)datos.Lector["IDUsuario"];
                    aux.DNI = (string)datos.Lector["DNI"];
                    aux.FechaNacimiento = (DateTime)datos.Lector["FechaNacimiento"];
                    aux.Direccion = datos.Lector["Direccion"] != DBNull.Value ? (string)datos.Lector["Direccion"] : "";

                    if (datos.Lector["IDGenero"] != DBNull.Value)
                    {
                        aux.Genero = new Genero();
                        aux.Genero.Id = (int)datos.Lector["IDGenero"];
                        aux.Genero.Descripcion = (string)datos.Lector["Genero"];
                    }

                    if (datos.Lector["IDObraSocial"] != DBNull.Value)
                    {
                        aux.ObraSocial = new ObraSocial();
                        aux.ObraSocial.Id = (int)datos.Lector["IDObraSocial"];
                        aux.ObraSocial.Nombre = (string)datos.Lector["ObraSocial"];
                    }

                    aux.Activo = (bool)datos.Lector["Activo"];

                    aux.Usuario = new Usuario();
                    aux.Usuario.Id = aux.UsuarioId;
                    aux.Usuario.Nombre = (string)datos.Lector["Nombre"];
                    aux.Usuario.Apellido = (string)datos.Lector["Apellido"];
                    aux.Usuario.Email = (string)datos.Lector["Email"];
                    aux.Usuario.Telefono = datos.Lector["Telefono"] != DBNull.Value ? (string)datos.Lector["Telefono"] : "";
                    aux.Usuario.Username = (string)datos.Lector["Username"];
                    aux.Usuario.ImagenUrl = datos.Lector["ImagenUrl"] != DBNull.Value ? (string)datos.Lector["ImagenUrl"] : "";

                    return aux;
                }

                return null;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        // Alta 
        public void RegistrarPaciente(Paciente paciente)
        {
            AccesoDatos datosRol = new AccesoDatos();
            int idRolPaciente = 0;
            try
            {
                datosRol.setearConsulta("SELECT IDRol FROM Roles WHERE Nombre = 'Paciente'");
                datosRol.ejecutarLectura();

                if (datosRol.Lector.Read())
                {

                    idRolPaciente = (int)datosRol.Lector["IDRol"];

                }
            }
            catch (Exception ex)
            {

                throw new Exception("Error al obtener el rol del paciente: " + ex.Message);
            }
            finally
            {
                datosRol.cerrarConexion();
            }

            // Si encontramos el rol, se lo asignamos al usuario paciente
            if (idRolPaciente > 0)
            {
                paciente.Usuario.RolId = idRolPaciente;
            }
            else
            {
                throw new Exception("El rol 'Paciente' no está registrado en la base de datos.");
            }
            ValidarAlta(paciente);
            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
            int idUsuario = usuarioNegocio.RegistrarUsuario(paciente.Usuario);

            AgregarRegistroPaciente(idUsuario, paciente);
        }

        private int AgregarRegistroPaciente(int idUsuario, Paciente paciente)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                        INSERT INTO Pacientes (IDUsuario, DNI, FechaNacimiento, Direccion, IDObraSocial, IDGenero, Activo)
                        VALUES
                        (@idUsuario, @dni, @fechaNacimiento, @direccion, @idObraSocial, @idGenero, 1);
                        SELECT SCOPE_IDENTITY();");

                datos.setearParametro("@idUsuario", idUsuario);
                datos.setearParametro("@dni", paciente.DNI);
                datos.setearParametro("@fechaNacimiento", paciente.FechaNacimiento);
                datos.setearParametro("@direccion", string.IsNullOrWhiteSpace(paciente.Direccion) ? (object)DBNull.Value : paciente.Direccion);
                datos.setearParametro("@idObraSocial", paciente.ObraSocial != null ? (object)paciente.ObraSocial.Id : DBNull.Value);
                datos.setearParametro("@idGenero", paciente.Genero != null ? (object)paciente.Genero.Id : DBNull.Value);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                    return Convert.ToInt32(datos.Lector[0]);

                throw new Exception("No se pudo obtener el ID del paciente.");
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void ReactivarPaciente(int idPaciente)
        {
            ValidarReactivacion(idPaciente);

            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"UPDATE Pacientes SET Activo = 1 WHERE IDPaciente = @id");
                datos.setearParametro("@id", idPaciente);
                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        // Modificación
        public void ModificarPaciente(Paciente paciente)
        {
            ValidarModificacion(paciente);

            UsuarioNegocio negocio = new UsuarioNegocio();
            paciente.Usuario.Id = paciente.UsuarioId;
            negocio.ModificarUsuario(paciente.Usuario);

            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                    UPDATE Pacientes
                    SET
                        DNI = @dni,
                        FechaNacimiento = @fechaNacimiento,
                        Direccion = @direccion,
                        IDObraSocial = @idObraSocial,
                        IDGenero = @idGenero
                    WHERE IDPaciente = @idPaciente");

                datos.setearParametro("@idPaciente", paciente.Id);
                datos.setearParametro("@dni", paciente.DNI);
                datos.setearParametro("@fechaNacimiento", paciente.FechaNacimiento);
                datos.setearParametro("@direccion", string.IsNullOrWhiteSpace(paciente.Direccion) ? (object)DBNull.Value : paciente.Direccion);
                datos.setearParametro("@idObraSocial", paciente.ObraSocial != null ? (object)paciente.ObraSocial.Id : DBNull.Value);
                datos.setearParametro("@idGenero", paciente.Genero != null ? (object)paciente.Genero.Id : DBNull.Value);

                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        // Baja lógica 
        public void EliminarPaciente(int idPaciente)
        {
            ValidarEliminacion(idPaciente);

            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"UPDATE Pacientes SET Activo = 0 WHERE IDPaciente = @id");
                datos.setearParametro("@id", idPaciente);
                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        // Validaciones
        private void ValidarAlta(Paciente paciente)
        {
            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();

            usuarioNegocio.ValidarAlta(paciente.Usuario);

            if (string.IsNullOrWhiteSpace(paciente.DNI))
                throw new Exception("Debe ingresar el DNI.");

            // 1. VALIDACIÓN: Formato numérico del DNI (7 u 8 números)
            if (!System.Text.RegularExpressions.Regex.IsMatch(paciente.DNI, @"^\d{7,8}$"))
                throw new Exception("El DNI debe contener únicamente números (entre 7 y 8 dígitos).");

            if (ExisteDNI(paciente.DNI))
                throw new Exception("El DNI ya está registrado.");

            if (paciente.FechaNacimiento == DateTime.MinValue)
                throw new Exception("Debe ingresar la fecha de nacimiento.");

            // 2. VALIDACIÓN: Fecha de nacimiento lógica (no futura)
            if (paciente.FechaNacimiento > DateTime.Today)
                throw new Exception("La fecha de nacimiento no puede ser una fecha futura.");

            if (paciente.Genero == null || paciente.Genero.Id == 0)
                throw new Exception("Debe seleccionar un género.");

            if (paciente.ObraSocial == null || paciente.ObraSocial.Id == 0)
                throw new Exception("Debe seleccionar una obra social.");
        }

        private void ValidarModificacion(Paciente paciente)
        {
            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();

            usuarioNegocio.ValidarModificacion(paciente.Usuario);

            if (string.IsNullOrWhiteSpace(paciente.DNI))
                throw new Exception("Debe ingresar el DNI.");

            // 3. VALIDACIÓN: Formato numérico del DNI al modificar
            if (!System.Text.RegularExpressions.Regex.IsMatch(paciente.DNI, @"^\d{7,8}$"))
                throw new Exception("El DNI debe contener únicamente números (entre 7 y 8 dígitos).");

            if (ExisteDNI(paciente.DNI, paciente.Id))
                throw new Exception("El DNI ya está registrado.");

            if (paciente.FechaNacimiento == DateTime.MinValue)
                throw new Exception("Debe ingresar la fecha de nacimiento.");

            // 4. VALIDACIÓN: Fecha de nacimiento lógica al modificar
            if (paciente.FechaNacimiento > DateTime.Today)
                throw new Exception("La fecha de nacimiento no puede ser una fecha futura.");

            if (paciente.Genero == null || paciente.Genero.Id == 0)
                throw new Exception("Debe seleccionar un género.");

            if (paciente.ObraSocial == null || paciente.ObraSocial.Id == 0)
                throw new Exception("Debe seleccionar una obra social.");
        }

        private void ValidarEliminacion(int idPaciente)
        {
            Paciente paciente = ObtenerPacientePorId(idPaciente);

            if (paciente == null)
                throw new Exception("El paciente no existe.");

            if (!paciente.Activo)
                throw new Exception("El paciente ya se encuentra inactivo.");

            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"SELECT COUNT(*) FROM Turnos WHERE IDPaciente = @idPaciente AND FechaHora >= GETDATE()");
                datos.setearParametro("@idPaciente", idPaciente);
                datos.ejecutarLectura();

                datos.Lector.Read();
                int cantidad = Convert.ToInt32(datos.Lector[0]);

                if (cantidad > 0)
                    throw new Exception("No se puede eliminar el paciente porque tiene turnos activos o futuros.");
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        private void ValidarReactivacion(int idPaciente)
        {
            Paciente paciente = ObtenerPacientePorId(idPaciente);

            if (paciente == null)
                throw new Exception("El paciente no existe.");

            if (paciente.Activo)
                throw new Exception("El paciente ya se encuentra activo.");
        }

        private bool ExisteDNI(string dni)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"SELECT IDPaciente FROM Pacientes WHERE DNI = @dni");
                datos.setearParametro("@dni", dni);
                datos.ejecutarLectura();

                return datos.Lector.Read();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        private bool ExisteDNI(string dni, int idPaciente)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"SELECT IDPaciente FROM Pacientes WHERE DNI = @dni AND IDPaciente <> @idPaciente");
                datos.setearParametro("@dni", dni);
                datos.setearParametro("@idPaciente", idPaciente);

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
