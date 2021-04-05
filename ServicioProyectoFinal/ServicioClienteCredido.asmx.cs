using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;

namespace ServicioProyectoFinal
{
    /// <summary>
    /// Descripción breve de ServicioClienteCredido
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class ServicioClienteCredido : System.Web.Services.WebService
    {

        SqlConnection conexion;
        string query = string.Empty;
        [WebMethod]
        public bool Grabar(string numerocliente, string nombre1, string nombre2, string apellido1, string apellido2, string fecha, string domicilio, 
            string trabajo, string referencia, string eficiencia)
        {
            try
            {
                conexion = new SqlConnection("server=RENEGONZALEZ\\SQLEXPRESS ; database=Solicitud ; integrated security = true");
                conexion.Open();
                query = string.Format("insert into solicitudCredito values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}');",
                    numerocliente, nombre1, nombre2, apellido1, apellido2, fecha, domicilio, trabajo, referencia, eficiencia);
                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.ExecuteNonQuery();
                conexion.Close();
                return true;
            }
            catch (SqlException e)
            {
                return false;
            }
        }
        [WebMethod]
        public string ConsultarTrabajoDesempeñado(int valor)
        {
            string respuesta = string.Empty;
            try
            {
                conexion = new SqlConnection("server=RENEGONZALEZ\\SQLEXPRESS ; database=ClientesCoppel ; integrated security = true");
                conexion.Open();

                query = string.Format(" select eficienciaPago from ClientesCoppel.dbo.clientes where NumeroCliente = {0}", valor);
                SqlCommand cmd = new SqlCommand(query, conexion);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    respuesta = dr["eficienciaPago"].ToString().TrimStart().TrimEnd();
                }
                else
                {
                    respuesta = "no se encontro Cliente";
                }
                dr.Close();
            }
            catch (SqlException ex)
            {
                respuesta = ex.Message;
            }
            conexion.Close();
            return respuesta;
        }
    }
}
