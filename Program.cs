using System;
using System.Collections.Generic;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        const int totalAsientos = 30;
        Queue<string> filaEspera = new Queue<string>();
        List<string> asientosAsignados = new List<string>();

        Console.WriteLine("=== SIMULADOR DE ATRACCIÓN - PARQUE DE DIVERSIONES ===");
        Console.WriteLine($"Capacidad de la atracción: {totalAsientos} asientos\n");

        bool salir = false;
        while (!salir)
        {
            Console.WriteLine("\n--- MENÚ ---");
            Console.WriteLine("1. Registrar persona en la fila");
            Console.WriteLine("2. Asignar siguiente asiento (subir a la atracción)");
            Console.WriteLine("3. Reporte: Ver fila de espera actual");
            Console.WriteLine("4. Reporte: Ver asientos ya asignados");
            Console.WriteLine("5. Salir");
            Console.WriteLine("6. Analizar tiempo de ejecución (prueba automática de 30 personas)");
            Console.Write("Opción: ");
            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    Console.Write("Nombre: ");
                    string nombre = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(nombre))
                    {
                        filaEspera.Enqueue(nombre);
                        Console.WriteLine($" -> {nombre} se unió a la fila.");
                    }
                    else
                    {
                        Console.WriteLine("Nombre inválido, intente de nuevo.");
                    }
                    break;

                case "2":
                    if (asientosAsignados.Count >= totalAsientos)
                    {
                        Console.WriteLine("¡Todos los asientos ya fueron vendidos!");
                    }
                    else if (filaEspera.Count == 0)
                    {
                        Console.WriteLine("No hay nadie en la fila.");
                    }
                    else
                    {
                        string persona = filaEspera.Dequeue();
                        asientosAsignados.Add(persona);
                        Console.WriteLine($"Asiento {asientosAsignados.Count}: {persona}");
                    }
                    break;

                case "3":
                    Console.WriteLine($"\n[REPORTE] Personas en espera ({filaEspera.Count}):");
                    if (filaEspera.Count == 0)
                    {
                        Console.WriteLine("  (vacía)");
                    }
                    else
                    {
                        int pos = 1;
                        foreach (string pEsp in filaEspera)
                            Console.WriteLine($"  {pos++}. {pEsp}");
                    }
                    break;

                case "4":
                    Console.WriteLine($"\n[REPORTE] Asientos asignados ({asientosAsignados.Count}/{totalAsientos}):");
                    if (asientosAsignados.Count == 0)
                    {
                        Console.WriteLine("  (ninguno todavía)");
                    }
                    else
                    {
                        for (int i = 0; i < asientosAsignados.Count; i++)
                            Console.WriteLine($"  Asiento {i + 1}: {asientosAsignados[i]}");
                    }
                    break;

                case "5":
                    salir = true;
                    Console.WriteLine("Saliendo del sistema...");
                    break;

                case "6":
                    AnalizarTiempoEjecucion(totalAsientos);
                    break;

                default:
                    Console.WriteLine("Opción inválida, intente de nuevo.");
                    break;
            }
        }
    }

    // Prueba automática: mide el tiempo real de las operaciones Enqueue/Dequeue,
    // sin depender de la velocidad de escritura del usuario.
    static void AnalizarTiempoEjecucion(int totalAsientos)
    {
        Console.WriteLine("\n=== ANÁLISIS DE TIEMPO DE EJECUCIÓN ===");
        Console.WriteLine($"Simulando {totalAsientos} registros y asignaciones automáticas...\n");

        Queue<string> filaPrueba = new Queue<string>();
        List<string> asignadosPrueba = new List<string>();

        // Medir Enqueue (registro de personas)
        Stopwatch swEnqueue = Stopwatch.StartNew();
        for (int i = 1; i <= totalAsientos; i++)
        {
            filaPrueba.Enqueue("Persona" + i);
        }
        swEnqueue.Stop();

        // Medir Dequeue (asignación de asientos)
        Stopwatch swDequeue = Stopwatch.StartNew();
        while (filaPrueba.Count > 0)
        {
            string persona = filaPrueba.Dequeue();
            asignadosPrueba.Add(persona);
        }
        swDequeue.Stop();

        Console.WriteLine($"Tiempo total en registrar {totalAsientos} personas (Enqueue): {swEnqueue.Elapsed.TotalMilliseconds:F4} ms");
        Console.WriteLine($"Tiempo total en asignar {totalAsientos} asientos (Dequeue): {swDequeue.Elapsed.TotalMilliseconds:F4} ms");
        Console.WriteLine($"Tiempo promedio por operación Enqueue: {(swEnqueue.Elapsed.TotalMilliseconds / totalAsientos):F6} ms");
        Console.WriteLine($"Tiempo promedio por operación Dequeue: {(swDequeue.Elapsed.TotalMilliseconds / totalAsientos):F6} ms");
    }
}