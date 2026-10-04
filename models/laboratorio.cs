namespace ReservasLaboratorios.Models;

public class Laboratorio
{
    public string Codigo { get; }
    public string Nombre { get; }
    public int Capacidad { get; }
    public bool Activo { get; private set; } = true;

    public Laboratorio(string codigo, string nombre, int capacidad)
    {
        if (string.IsNullOrWhiteSpace(codigo)) throw new ArgumentException("El código es obligatorio.");
        if (string.IsNullOrWhiteSpace(nombre)) throw new ArgumentException("El nombre es obligatorio.");
        if (capacidad <= 0) throw new ArgumentOutOfRangeException(nameof(capacidad), "La capacidad debe ser mayor que cero.");
        Codigo = codigo.Trim().ToUpperInvariant();
        Nombre = nombre.Trim();
        Capacidad = capacidad;
    }

    public void Desactivar() => Activo = false;
    public override string ToString() => $"{Codigo} - {Nombre} (capacidad: {Capacidad})";
}
