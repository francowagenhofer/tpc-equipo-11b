using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using Datos;


namespace Negocio {
    public class MedicoNegocio {

        public List<Medico> ListarMedicos(){

            List<Medico> lista = new List<Medico>();
            AccesoDatos datos = new AccesoDatos();

			try
			{
				datos.setearConsulta("SELECT M.IDMedico, M.IDUsuario, M.Matricula, M.Activo, U.Nombre, U.Apellido, U.Email, U.Telefono FROM Medicos M INNER JOIN Usuarios U ON M.IDUsuario = U.IDUsuario WHERE M.Activo = 1");
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
                    aux.Usuario.Telefono = datos.Lector["Telefono"] != DBNull.Value ? (string)datos.Lector["Telefono"] : "";

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


		public bool ArgregarMedico(Medico nuevoMedico) { 
			
			AccesoDatos datos = new AccesoDatos();
			try
			{

				datos.setearConsulta("INSERT INTO Usuarios (Nombre, Apellido, Email, Telefono, Username, PasswordHash, IDRol, Activo) VALUES (@nombre, @apellido, @email, @telefono, @username, @passHash, 3, 1)");
                datos.setearParametro("@nombre", nuevoMedico.Usuario.Nombre);
                datos.setearParametro("@apellido", nuevoMedico.Usuario.Apellido);
                datos.setearParametro("@email", nuevoMedico.Usuario.Email);
                datos.setearParametro("@telefono", string.IsNullOrEmpty(nuevoMedico.Usuario.Telefono) ? (object)DBNull.Value : nuevoMedico.Usuario.Telefono);
                datos.setearParametro("@username", nuevoMedico.Usuario.Username);
                datos.setearParametro("@passHash", nuevoMedico.Usuario.Password);

				datos.ejecutarAccion();
				datos.cerrarConexion();

				int idUsuarioGenerado = 0;
				datos = new AccesoDatos();
				datos.setearConsulta("SELECT IDUsuario FROM Usuarios WHERE Username = @username");
				datos.setearParametro("@username", nuevoMedico.Usuario.Username);
				datos.ejecutarLectura();

				if (datos.Lector.Read()) {

					idUsuarioGenerado = (int)datos.Lector["IDUsuario"];
				 
				}
				datos.cerrarConexion();

				if (idUsuarioGenerado == 0) 
					return false;

				datos = new AccesoDatos();
				datos.setearConsulta("INSERT INTO Medicos (IDUsuario, Matricula, Activo) VALUES (@idUsuario, @matricula, 1)");
				datos.setearParametro("@idUsuario", idUsuarioGenerado);
				datos.setearParametro("@matricula", nuevoMedico.Matricula);

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
