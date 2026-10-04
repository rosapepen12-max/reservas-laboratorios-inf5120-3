using System.Globalization;
using ReservasLaboratorios.Models;
using ReservasLaboratorios.Services;

var gestor = new GestorReservas();
// Datos iniciales para que la demostración pueda comenzar de inmediato.
gestor.RegistrarLaboratorio(new Laboratorio("LAB-101", "Laboratorio de Redes", 24));
gestor.RegistrarLaboratorio(new Laboratorio("LAB-202", "Laboratorio de Programación", 30));
gestor.RegistrarUsuario(new Usuario("A001", "Ana Pérez", "ana@universidad.edu"));
gestor.RegistrarUsuario(new Usuario("A002", "Luis Gómez", "luis@universidad.edu"));

while (true)
{
    Console.WriteLine("\n=== SISTEMA DE RESERVAS DE LABORATORIOS ===");
    Console.WriteLine("1. Listar laboratorios  2. Registrar laboratorio  3. Registrar usuario");
    Console.WriteLine("4. Crear reserva  5. Cancelar reserva  6. Consultar por laboratorio/fecha");
    Console.WriteLine("7. Consultar reservas de usuario  0. Salir");
    var opcion = LeerEntero("Opción: ");

    try
    {
        switch (opcion)
        {
            case 0: return;
            case 1:
                if (!gestor.Laboratorios.Any()) Console.WriteLine("No hay laboratorios registrados.");
                foreach (var laboratorio in gestor.Laboratorios) Console.WriteLine(laboratorio + (laboratorio.Activo ? "" : " [INACTIVO]"));
                break;
            case 2:
                var nuevoLab = new Laboratorio(LeerTexto("Código: "), LeerTexto("Nombre: "), LeerEntero("Capacidad: "));
                Console.WriteLine(gestor.RegistrarLaboratorio(nuevoLab) ? "Laboratorio registrado." : "Ese código ya existe.");
                break;
            case 3:
                var nuevoUsuario = new Usuario(LeerTexto("Matrícula: "), LeerTexto("Nombre: "), LeerTexto("Correo: "));
                Console.WriteLine(gestor.RegistrarUsuario(nuevoUsuario) ? "Usuario registrado." : "Esa matrícula ya existe.");
                break;
            case 4:
                var codigo = LeerTexto("Código de laboratorio: ");
                var matricula = LeerTexto("Matrícula del usuario: ");
                var inicio = LeerFecha("Inicio (dd/MM/yyyy HH:mm): ");
                var fin = LeerFecha("Fin (dd/MM/yyyy HH:mm): ");
                var resultado = gestor.CrearReserva(codigo, matricula, inicio, fin);
                Console.WriteLine(resultado.Mensaje);
                if (resultado.Reserva is not null) Console.WriteLine(resultado.Reserva);
                break;
            case 5:
                Console.WriteLine(gestor.CancelarReserva(LeerEntero("Número de reserva: ")) ? "Reserva cancelada." : "Reserva activa no encontrada.");
                break;
            case 6:
                codigo = LeerTexto("Código de laboratorio: ");
                var fecha = LeerFecha("Fecha (dd/MM/yyyy HH:mm): ").Date;
                Imprimir(gestor.ConsultarPorLaboratorio(codigo, fecha));
                break;
            case 7:
                Imprimir(gestor.ConsultarPorUsuario(LeerTexto("Matrícula: ")));
                break;
            default: Console.WriteLine("Opción inválida. Elige una opción del menú."); break;
        }
    }
    catch (ArgumentException ex) { Console.WriteLine($"Dato inválido: {ex.Message}"); }
}

static string LeerTexto(string mensaje)
{
    while (true)
    {
        Console.Write(mensaje);
        var valor = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(valor)) return valor.Trim();
        Console.WriteLine("El valor no puede estar vacío.");
    }
}

static int LeerEntero(string mensaje)
{
    while (true)
    {
        Console.Write(mensaje);
        if (int.TryParse(Console.ReadLine(), out var valor)) return valor;
        Console.WriteLine("Escribe un número entero válido.");
    }
}

static DateTime LeerFecha(string mensaje)
{
    while (true)
    {
        Console.Write(mensaje);
        if (DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy HH:mm", CultureInfo.GetCultureInfo("es-DO"), DateTimeStyles.None, out var fecha)) return fecha;
        Console.WriteLine("Formato inválido. Ejemplo: 25/10/2026 14:30.");
    }
}

static void Imprimir(IEnumerable<Reserva> reservas)
{
    var lista = reservas.ToList();
    if (lista.Count == 0) Console.WriteLine("No se encontraron reservas.");
    else foreach (var reserva in lista) Console.WriteLine(reserva);
}
