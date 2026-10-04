namespace ReservasLaboratorios.Models;

public class Usuario
{
    public string Matricula { get; }
    public string Nombre { get; }
    public string Correo { get; }

    public Usuario(string matricula, string nombre, string correo)
    {
        if (string.IsNullOrWhiteSpace(matricula)) throw new ArgumentException("La matrícula es obligatoria.");
        if (string.IsNullOrWhiteSpace(nombre)) throw new ArgumentException("El nombre es obligatorio.");
        if (string.IsNullOrWhiteSpace(correo) || !correo.Contains('@')) throw new ArgumentException("Escribe un correo válido.");
        Matricula = matricula.Trim().ToUpperInvariant();
        Nombre = nombre.Trim();
        Correo = correo.Trim();
    }

    public override string ToString() => $"{Matricula} - {Nombre} ({Correo})";
}
