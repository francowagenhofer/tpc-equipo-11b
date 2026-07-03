using Datos;
using Dominio;
using System;
using System.Collections.Generic;

namespace Negocio
{
    public class MedicoNegocio
    {
        public Medico ObtenerMedicoPorId(int idMedico)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                    SELECT
                        M.IDMedico,
                        M.IDUsuario,
                        M.IDEspecialidad,
                        M.Matricula,
                        M.Activo,

                        U.Nombre,
                        U.Apellido,
                        U.Email,
                        U.Telefono,
                        U.Username,
                        U.ImagenUrl,

                        E.Nombre AS Especialidad

                    FROM Medicos M

                    INNER JOIN Usuarios U
                        ON M.IDUsuario = U.IDUsuario

                    INNER JOIN Especialidades E
                        ON M.IDEspecialidad = E.IDEspecialidad

                    WHERE M.IDMedico = @idMedico");

                datos.setearParametro("@idMedico", idMedico);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Medico medico = new Medico();

                    medico.Id = (int)datos.Lector["IDMedico"];
                    medico.UsuarioId = (int)datos.Lector["IDUsuario"];
                    medico.EspecialidadId = (int)datos.Lector["IDEspecialidad"];
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
                        ? (string)datos.Lector["Telefono"]
                        : string.Empty;

                    medico.Usuario.ImagenUrl =
                        datos.Lector["ImagenUrl"] != DBNull.Value
                        ? (string)datos.Lector["ImagenUrl"]
                        : string.Empty;

                    medico.Especialidad = new Especialidad();
                    medico.Especialidad.Id = (int)datos.Lector["IDEspecialidad"];
                    medico.Especialidad.Nombre = (string)datos.Lector["Especialidad"];

                    // Cargar obras sociales del médico
                    MedicoObraSocialNegocio negocioMedicoOS = new MedicoObraSocialNegocio();
                    medico.ObrasSociales = negocioMedicoOS.ListarObrasSocialesPorMedico(medico.Id);

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
        
        // Listado 
        public List<Medico> ListarMedicos()
        {
            List<Medico> lista = new List<Medico>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                    SELECT
                        M.IDMedico,
                        M.IDUsuario,
                        M.IDEspecialidad,
                        M.Matricula,
                        M.Activo,

                        U.Nombre,
                        U.Apellido,
                        U.Email,
                        U.Telefono,

                        E.Nombre AS Especialidad

                    FROM Medicos M

                    INNER JOIN Usuarios U
                        ON M.IDUsuario = U.IDUsuario

                    INNER JOIN Especialidades E
                        ON M.IDEspecialidad = E.IDEspecialidad

                    ORDER BY
                        M.Activo DESC,
                        U.Apellido,
                        U.Nombre");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Medico aux = new Medico();

                    aux.Id = (int)datos.Lector["IDMedico"];
                    aux.UsuarioId = (int)datos.Lector["IDUsuario"];
                    aux.EspecialidadId = (int)datos.Lector["IDEspecialidad"];
                    aux.Matricula = (string)datos.Lector["Matricula"];
                    aux.Activo = (bool)datos.Lector["Activo"];

                    aux.Usuario = new Usuario();
                    aux.Usuario.Id = (int)datos.Lector["IDUsuario"];
                    aux.Usuario.Nombre = (string)datos.Lector["Nombre"];
                    aux.Usuario.Apellido = (string)datos.Lector["Apellido"];
                    aux.Usuario.Email = (string)datos.Lector["Email"];
                    aux.Usuario.Telefono = datos.Lector["Telefono"] != DBNull.Value ? (string)datos.Lector["Telefono"] : string.Empty;

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
        public List<Medico> ListarPorEspecialidadYObraSocial(int idEspecialidad, int idObraSocial)
        {
            List<Medico> lista = new List<Medico>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                    SELECT DISTINCT
                        M.IDMedico,
                        M.IDUsuario,
                        M.IDEspecialidad,
                        M.Matricula,
                        M.Activo,

                        U.Nombre,
                        U.Apellido,
                        U.Email,
                        U.Telefono,

                        E.Nombre AS Especialidad

                    FROM Medicos M

                    INNER JOIN Usuarios U
                        ON M.IDUsuario = U.IDUsuario

                    INNER JOIN Especialidades E
                        ON M.IDEspecialidad = E.IDEspecialidad

                    INNER JOIN MedicoObraSocial MOS
                        ON M.IDMedico = MOS.IDMedico

                    WHERE
                        M.Activo = 1
                        AND M.IDEspecialidad = @idEspecialidad
                        AND MOS.IDObraSocial = @idObraSocial

                    ORDER BY
                        U.Apellido,
                        U.Nombre");

                datos.setearParametro("@idEspecialidad", idEspecialidad);
                datos.setearParametro("@idObraSocial", idObraSocial);

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Medico aux = new Medico();

                    aux.Id = (int)datos.Lector["IDMedico"];
                    aux.UsuarioId = (int)datos.Lector["IDUsuario"];
                    aux.EspecialidadId = (int)datos.Lector["IDEspecialidad"];
                    aux.Matricula = (string)datos.Lector["Matricula"];
                    aux.Activo = (bool)datos.Lector["Activo"];

                    aux.Usuario = new Usuario();
                    aux.Usuario.Id = (int)datos.Lector["IDUsuario"];
                    aux.Usuario.Nombre = (string)datos.Lector["Nombre"];
                    aux.Usuario.Apellido = (string)datos.Lector["Apellido"];
                    aux.Usuario.Email = (string)datos.Lector["Email"];
                    aux.Usuario.Telefono = datos.Lector["Telefono"] != DBNull.Value ? datos.Lector["Telefono"].ToString() : "";

                    aux.Especialidad = new Especialidad();
                    aux.Especialidad.Id = (int)datos.Lector["IDEspecialidad"];
                    aux.Especialidad.Nombre = datos.Lector["Especialidad"].ToString();

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

        // Alta
        public void RegistrarMedico(Medico medico)
        {
            ValidarAlta(medico);

            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
            MedicoObraSocialNegocio medicoObraSocialNegocio = new MedicoObraSocialNegocio();

            int idUsuario = usuarioNegocio.RegistrarUsuario(medico.Usuario);
            int idMedico = AgregarRegistroMedico(idUsuario, medico.Especialidad.Id, medico.Matricula);

            foreach (ObraSocial obra in medico.ObrasSociales)
                medicoObraSocialNegocio.AsociarObraSocial(idMedico, obra.Id);
        }
        private int AgregarRegistroMedico(int idUsuario, int idEspecialidad, string matricula)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                    INSERT INTO Medicos
                        (IDUsuario, IDEspecialidad, Matricula, Activo)
                    VALUES
                        (@idUsuario, @idEspecialidad, @matricula, 1);

                    SELECT SCOPE_IDENTITY();");

                datos.setearParametro("@idUsuario", idUsuario);
                datos.setearParametro("@idEspecialidad", idEspecialidad);
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
        public void ReactivarMedico(int idMedico)
        {
            ValidarReactivacion(idMedico);

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
            MedicoObraSocialNegocio medicoObraSocialNegocio = new MedicoObraSocialNegocio();

            usuarioNegocio.ModificarUsuario(medico.Usuario);

            ModificarRegistroMedico(medico);

            medicoObraSocialNegocio.EliminarTodasLasObrasSociales(medico.Id);

            foreach (ObraSocial obra in medico.ObrasSociales)
                medicoObraSocialNegocio.AsociarObraSocial(medico.Id, obra.Id);
        }
        private void ModificarRegistroMedico(Medico medico)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                    UPDATE Medicos
                    SET
                        Matricula = @matricula,
                        IDEspecialidad = @idEspecialidad
                    WHERE IDMedico = @idMedico");

                datos.setearParametro("@idMedico", medico.Id);
                datos.setearParametro("@matricula", medico.Matricula);
                datos.setearParametro("@idEspecialidad", medico.Especialidad.Id);

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

            if (medico.ObrasSociales == null || medico.ObrasSociales.Count == 0)
                throw new Exception("Debe seleccionar al menos una obra social.");
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

            if (medico.ObrasSociales == null || medico.ObrasSociales.Count == 0)
                throw new Exception("Debe seleccionar al menos una obra social.");
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
        private void ValidarReactivacion(int idMedico)
        {
            Medico medico = ObtenerMedicoPorId(idMedico);

            if (medico == null)
                throw new Exception("El médico no existe.");

            if (medico.Activo)
                throw new Exception("El médico ya se encuentra activo.");
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

        // Metodos para el dashboard
        public int CantidadMedicos()
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT COUNT(*) AS Total FROM Medicos WHERE Activo = 1");
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                    return (int)datos.Lector["Total"];

                return 0;
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
