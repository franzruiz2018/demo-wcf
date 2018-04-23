using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServicePersona
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "WSPersona" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione WSPersona.svc o WSPersona.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class WSPersona : IWSPersona
    {

        public Persona ObtenerPersona(string dni)
        {
            if (dni == "0")
            {
                return new Persona() { Nombre = "Felipe", Edad = 99 };
            }
            else if (dni == "1")
            {
                return new Persona() { Nombre = "Patricia", Edad = 45 };
            }
            else
            {
                return new Persona() { Error="Persona no encontrada" };
            }
        }


        public Persona BuscarAlumno(int dni)
        {
            Repositorio.RepositorioAlumno _repositorio = new Repositorio.RepositorioAlumno();
            
            Repositorio.RepositorioAlumno.Alumno alumno=new Repositorio.RepositorioAlumno.Alumno();

            alumno = _repositorio.BuscarAlumno(dni);

            if (alumno.IdAlumno!=0)
            {
                return new Persona() { Nombre = alumno.NombreAlumno, Dni = alumno.DniAlumno };
            }
            else 
            {
                return new Persona() { Error = "Persona no encontrada" };
            }
                        
        }
    }
}
