namespace GestorClientes.Domain;

/// <summary>
/// Número de teléfono que pertenece a un cliente.
/// </summary>
public class Telefono
{
    public int Id { get; set; }

    public string Numero { get; set; } = string.Empty;

    public int ClienteId { get; set; }

    public virtual Cliente Cliente { get; set; } = null!;

    // TODO (Persona 2): agregar aquí la regla de negocio del formato del teléfono.

    public bool ValidarReglaNegocioTelefonoValido()
    {
        return Numero.Length == 8;
    }

}

 