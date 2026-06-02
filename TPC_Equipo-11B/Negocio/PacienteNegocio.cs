using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using Datos;

namespace Negocio {
    public class PacienteNegocio {
        public List<Paciente> ListarPacientes()
        {
            List<Paciente> lista = new List<Paciente>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT IDPaciente, Nombre, Apellido, DNI, Email, Telefono, Direccion, ObraSocial, Activo FROM Pacientes WHERE Activo = 1 ORDER BY Apellido, Nombre");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Paciente aux = new Paciente();
                    aux.Id = (int)datos.Lector["IDPaciente"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Apellido = (string)datos.Lector["Apellido"];
                    aux.DNI = (string)datos.Lector["DNI"];
                    aux.Email = (string)datos.Lector["Email"];
                    aux.Telefono = (string)datos.Lector["Telefono"];
                    aux.Direccion = datos.Lector["Direccion"] != DBNull.Value ? (string)datos.Lector["Direccion"] : "";
                    aux.ObraSocial = datos.Lector["ObraSocial"] != DBNull.Value ? (string)datos.Lector["ObraSocial"] : "";
                    aux.Activo = (bool)datos.Lector["Activo"];
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
    }
}