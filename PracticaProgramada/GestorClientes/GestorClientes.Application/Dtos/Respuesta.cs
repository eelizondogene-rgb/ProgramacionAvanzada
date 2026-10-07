namespace GestorClientes.Application.Dtos
{
    // Patrón Result: estandariza la respuesta de los servicios (dato + mensaje + código).
    public class Respuesta<T>
    {
        public bool EsCorrecto { get; set; }

        public string Mensaje { get; set; } = string.Empty;

        public int Codigo { get; set; }

        public T? Dato { get; set; }

        public Respuesta()
        {
            EsCorrecto = true;
            Mensaje = "Operacion realizada correctamente";
            Codigo = 200;
        }
    }
}
