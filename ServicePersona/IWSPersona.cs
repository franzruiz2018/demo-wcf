using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServicePersona
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IWSPersona" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IWSPersona
    {
        [OperationContract]
        Persona ObtenerPersona(string dni);

        [OperationContract]
        Persona BuscarAlumno(int dni);
    }


   [DataContract]
    public class Persona:BaseRespuesta
    {
       [DataMember]
        public string Nombre { get; set; }
       [DataMember]
        public int Edad { get; set; }
       [DataMember]
       public int Dni { get; set; }
        public string Secreto { get; set; }
    }
    [DataContract]
    public class BaseRespuesta
    {
        [DataMember]
        public string MensajeRespuesta { get; set; }
        [DataMember]
        public string Error { get; set; }
    }

}
