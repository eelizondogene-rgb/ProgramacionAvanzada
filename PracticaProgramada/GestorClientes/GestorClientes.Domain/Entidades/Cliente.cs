namespace GestorClientes.Domain;

/// <summary>
/// Representa un cliente del negocio. Un cliente puede tener varios teléfonos.
/// </summary>
public class Cliente
{
    public int Id { get; set; }

    public string Cedula { get; set; } = string.Empty;

    public string Nombre { get; set; } = string.Empty;

    public string Apellidos { get; set; } = string.Empty;

    public virtual ICollection<Telefono> Telefonos { get; set; } = new List<Telefono>();

    // TODO (Persona 2): agregar aquí las reglas de negocio del cliente
    // (por ejemplo: cédula y nombre obligatorios, al menos un teléfono, etc.)

    public bool ValidarReglaNegocioDatosObligatorios()
    {
        return Cedula != ""
            && Nombre != "";
    }

    public bool ValidarReglaNegocioTieneTelefonos()
    {
        return Telefonos.Count > 0;
    }

    public bool ValidarReglasDeNegocio()
    {
        return ValidarReglaNegocioDatosObligatorios()
            && ValidarReglaNegocioTieneTelefonos();
    }
}
