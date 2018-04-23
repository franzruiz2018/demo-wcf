# Ejemplo de un conjunto de aplicaciones que nos permiten consumir un web service desde una aplicacion de escritorio y web

## Creación de un Proyecto de tipo biblioteca de clases Repositorio

Creación de una clase RepositorioAlumno el cual nos permite crear la entidad alumno y asi mismo contiene un metodo que nos permite extraer datos de la base de datos

```
 public class RepositorioAlumno
    {
      
    }

```

Se crea la clase alumno

```
        public class Alumno
        {
            public int IdAlumno { get; set; }
            public string NombreAlumno { get; set; }
            public int DniAlumno { get; set; }
            public DateTime Registro { get; set; }
        }
```

Se crea el método BuscarAlumno el cual nos permite buscar a 1 alumno que cooincidan con el DNI

```
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

```

## Creación de un Proyecto de tipo Aplicacion de Servicio WCF llamdo ServicePersona

Se crea una Servicio WCF de nombre WSPersona

```
 [ServiceContract]
    public interface IWSPersona
    {
        [OperationContract]
        Persona ObtenerPersona(string dni);

        [OperationContract]
        Persona BuscarAlumno(int dni);
    }

```

Se crea la clase persona y se hace público sus propiedades con [DataMenber] a su ves hereda de la clase BaseRespuesta sus propiedades

> File: IWSPersona.cs

```
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
```

```
    [DataContract]
    public class BaseRespuesta
    {
        [DataMember]
        public string MensajeRespuesta { get; set; }
        [DataMember]
        public string Error { get; set; }
    }

```

> File: WSPersona.cs

```
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
```

## Creación de un Proyecto de tipo Winform llamado ClienteWS

Se agrega la referencia de servicio y se crea una interfaz para buscar segun el DNI en el metodo ObtenerPersona del servicio web creado

```
 private void btnBuscar_Click(object sender, EventArgs e)
        {
            lblMensaje.Text = "";
            var dni = txtIdenficacion.Text;

            using (WSPersona.WSPersonaClient client = new WSPersona.WSPersonaClient())
            {
                var persona = client.ObtenerPersona(dni);
                txtNombre.Text = persona.Nombre;
                txtEdad.Text = persona.Edad.ToString();
                lblMensaje.Text = persona.Error;
            }

        }


```
Se agrega la referencia de servicio y se crea una interfaz para buscar segun el DNI en el metodo BuscarAlumno del servicio web creado
```
lblMensaje.Text = "";

            var dni = Convert.ToInt32( txtIdenficacion.Text);

            using (WSPersona.WSPersonaClient cliente = new WSPersona.WSPersonaClient())
            {
                var alumno = cliente.BuscarAlumno(dni);
                txtNombre.Text = alumno.Nombre;
                txtDNI.Text = alumno.Dni.ToString();
                lblMensaje.Text = alumno.Error;
            }
```


## Creación de un Proyecto MVC llamado WebAlumno

Se agrega la referencia de servicio y se crea una interfaz para buscar segun el DNI en el metodo ObtenerPersona del servicio web creado

Se crea un controlador Alumno 
```
 public class AlumnoController : Controller
    {
        //
        // GET: /Alumno/
        public ActionResult Index()
        {       

            return View();
        }
        [HttpPost]
        public ActionResult Index(int id)
        {
            using (WSAlumno.WSPersonaClient cliente = new WSAlumno.WSPersonaClient())
            {
                var alumno=cliente.BuscarAlumno(id);
                ViewBag.Dni= alumno.Dni;
                ViewBag.Nombre = alumno.Nombre;
                ViewBag.Error = alumno.Error;
            }

            return View();
        }
	}
```
En la vista 
```
@using (Html.BeginForm(null, null, FormMethod.Post, new { name = "registro", id = "registro" }))
{ 
    <div class="form-horizontal">
        DNI: <input type="text" name="id" id="id" />

        <input type="submit" value="Buscar" />
        </div>
}
<h3>Resultados</h3>

<p>Nombre: @ViewBag.Nombre</p>
<p>Error: @ViewBag.Error</p>

```