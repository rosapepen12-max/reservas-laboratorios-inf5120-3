using ReservasLaboratorios.Models;

namespace ReservasLaboratorios.Services;

public class GestorReservas
{
    private readonly List<Laboratorio> _laboratorios = [];
    private readonly List<Usuario> _usuarios = [];
    private readonly List<Reserva> _reservas = [];
    private int _siguienteNumero = 1;

    public IReadOnlyList<Laboratorio> Laboratorios => _laboratorios;
    public IReadOnlyList<Usuario> Usuarios => _usuarios;
    public IReadOnlyList<Reserva> Reservas => _reservas;

    public bool RegistrarLaboratorio(Laboratorio laboratorio)
    {
        if (_laboratorios.Any(x => x.Codigo == laboratorio.Codigo)) return false;
        _laboratorios.Add(laboratorio);
        return true;
    }

    public bool RegistrarUsuario(Usuario usuario)
    {
        if (_usuarios.Any(x => x.Matricula == usuario.Matricula)) return false;
        _usuarios.Add(usuario);
        return true;
    }

    public Laboratorio? BuscarLaboratorio(string codigo) => _laboratorios.FirstOrDefault(x => x.Codigo == codigo.Trim().ToUpperInvariant());
    public Usuario? BuscarUsuario(string matricula) => _usuarios.FirstOrDefault(x => x.Matricula == matricula.Trim().ToUpperInvariant());

    public (Reserva? Reserva, string Mensaje) CrearReserva(string codigoLaboratorio, string matricula, DateTime inicio, DateTime fin)
    {
        var laboratorio = BuscarLaboratorio(codigoLaboratorio);
        var usuario = BuscarUsuario(matricula);
        if (laboratorio is null || !laboratorio.Activo) return (null, "No existe un laboratorio activo con ese código.");
        if (usuario is null) return (null, "No existe un usuario con esa matrícula.");
        if (inicio <= DateTime.Now) return (null, "La reserva debe comenzar en el futuro.");
        if (fin <= inicio) return (null, "La hora de fin debe ser posterior a la hora de inicio.");
        if (fin.Date != inicio.Date) return (null, "La reserva debe comenzar y terminar el mismo día.");
        if (_reservas.Any(r => !r.Cancelada && r.Laboratorio.Codigo == laboratorio.Codigo && r.SeCruzaCon(inicio, fin)))
            return (null, "Ese laboratorio ya tiene una reserva que se cruza con el horario indicado.");

        var reserva = new Reserva(_siguienteNumero++, laboratorio, usuario, inicio, fin);
        _reservas.Add(reserva);
        return (reserva, "Reserva creada correctamente.");
    }

    public bool CancelarReserva(int numero)
    {
        var reserva = _reservas.FirstOrDefault(r => r.Numero == numero && !r.Cancelada);
        if (reserva is null) return false;
        reserva.Cancelar();
        return true;
    }

    public IEnumerable<Reserva> ConsultarPorLaboratorio(string codigo, DateTime fecha) =>
        _reservas.Where(r => !r.Cancelada && r.Laboratorio.Codigo == codigo.Trim().ToUpperInvariant() && r.Inicio.Date == fecha.Date)
            .OrderBy(r => r.Inicio);

    public IEnumerable<Reserva> ConsultarPorUsuario(string matricula) =>
        _reservas.Where(r => !r.Cancelada && r.Usuario.Matricula == matricula.Trim().ToUpperInvariant())
            .OrderBy(r => r.Inicio);
}
