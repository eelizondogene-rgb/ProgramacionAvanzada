namespace GestorClientes.Application.Dtos
{
    public class ClienteDto
    {
        public int Id { get; set; }

        public string Cedula { get; set; } = string.Empty;

        public string Nombre { get; set; } = string.Empty;

        public string Apellidos { get; set; } = string.Empty;

        public List<TelefonoDto> Telefonos { get; set; } = new List<TelefonoDto>();
    }
}
