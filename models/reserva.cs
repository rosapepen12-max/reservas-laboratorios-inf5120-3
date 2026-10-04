namespace ReservasLaboratorios.Models;

public class Reserva
{
    public int Numero { get; }
    public Laboratorio Laboratorio { get; }
    public Usuario Usuario { get; }
    public DateTime Inicio { get; }
    public DateTime Fin { get; }
    public bool Cancelada { get; private set; }

    public Reserva(int numero, Laboratorio laboratorio, Usuario usuario, DateTime inicio, DateTime fin)
    {
        if (numero <= 0) throw new ArgumentOutOfRangeException(nameof(numero));
        if (fin <= inicio) throw new ArgumentException("La hora de fin debe ser posterior a la hora de inicio.");
        Numero = numero;
        Laboratorio = laboratorio;
        Usuario = usuario;
        Inicio = inicio;
        Fin = fin;
    }

    public bool SeCruzaCon(DateTime inicio, DateTime fin) => Inicio < fin && inicio < Fin;
    public void Cancelar() => Cancelada = true;
    public override string ToString() => $"Reserva #{Numero}: {Laboratorio.Codigo}, {Inicio:dd/MM/yyyy HH:mm}-{Fin:HH:mm}, usuario {Usuario.Matricula}" + (Cancelada ? " [CANCELADA]" : "");
}
