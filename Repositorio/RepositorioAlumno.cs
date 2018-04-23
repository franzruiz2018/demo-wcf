using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;

namespace Repositorio
{
    public class RepositorioAlumno
    {
        public class Alumno
        {
            public int IdAlumno { get; set; }

            public string NombreAlumno { get; set; }

            public int DniAlumno { get; set; }

            public DateTime Registro { get; set; }

        }
        
        private SqlConnection _con;

        private void Conexion()
        {
            string conex ="server=.;database=DemoAlumno;user=sa;password=123";
            _con = new SqlConnection(conex);
        }
        
        public Alumno BuscarAlumno(int dni)
        {
            Conexion();
            
            Alumno a = new Alumno();
            a.IdAlumno = 0;
            using (SqlCommand command = new SqlCommand("sp_buscar_alumno", _con))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 0;
                command.Parameters.AddWithValue("@dni", dni);
                _con.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows) { 
                    if (reader.Read())
                         {
                            a.IdAlumno = Convert.ToInt32(reader["IdAlumno"]);
                            a.NombreAlumno = Convert.ToString(reader["NombreAlumno"]);
                            a.DniAlumno = Convert.ToInt32(reader["DniAlumno"]);
                            a.Registro = Convert.ToDateTime(reader["Registro"]);  
                           }
                }
                _con.Close();
            }
            return a;
        }
    }
}
