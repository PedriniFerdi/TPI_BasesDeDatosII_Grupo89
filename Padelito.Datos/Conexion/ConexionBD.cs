using System.Configuration;
using System.Data.SqlClient;

namespace Padelito.Datos.Conexion
{
    public class ConexionBD
    {
        private readonly string _cadenaConexion;

        public ConexionBD()
        {
            _cadenaConexion = ConfigurationManager.ConnectionStrings["PadelitoDB"].ConnectionString;
        }

        public SqlConnection CrearConexion()
        {
            return new SqlConnection(_cadenaConexion);
        }
    }
}
