# Sistema de Reservas de Laboratorios

**Asignatura:** INF-512  
**Sección:** INF5120-3  
**Estudiante:** Rossanna M. Pepén  
**Repositorio:** https://github.com/rosapepen12-max/reservas-laboratorios-inf5120-3

## Descripción

Aplicación de consola en C# para registrar laboratorios y usuarios, crear y cancelar reservas y consultar reservas por laboratorio, fecha o usuario. El sistema evita códigos y matrículas duplicados y rechaza horarios que se cruzan para el mismo laboratorio.

## Clases principales

- `Laboratorio`: representa un laboratorio y su capacidad.
- `Usuario`: representa a una persona que reserva.
- `Reserva`: relaciona un laboratorio, un usuario y un horario.
- `GestorReservas`: registra, busca y administra las reservas.
- `Program`: presenta el menú de consola.

## Contenedor

El gestor utiliza `List<T>` para administrar las listas variables de laboratorios, usuarios y reservas. La memoria técnica compara esta opción con `Array`.

## Ejecutar

Se requiere .NET 8. Desde la carpeta del proyecto:

```powershell
dotnet run