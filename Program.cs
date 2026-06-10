// ============================================================
//  SISTEMA DE REGISTRO DE ESTUDIANTES
//  Estructura de Datos - Guía 1
// ============================================================

using System;
using System.Collections.Generic;

// ── CLASE 1: Estudiante ──────────────────────────────────────
class Estudiante
{
    // Atributos
    public int    Id     { get; set; }
    public string Nombre { get; set; }
    public string Carrera{ get; set; }
    public double Nota   { get; set; }

    // Constructor
    public Estudiante(int id, string nombre, string carrera, double nota)
    {
        Id      = id;
        Nombre  = nombre;
        Carrera = carrera;
        Nota    = nota;
    }

    // Método: mostrar datos del estudiante
    public void Mostrar()
    {
        Console.WriteLine($"  ID: {Id} | Nombre: {Nombre,-20} | Carrera: {Carrera,-15} | Nota: {Nota:F1}");
    }

    // Método: indica si el estudiante aprobó
    public string Estado()
    {
        return Nota >= 7.0 ? "APROBADO" : "REPROBADO";
    }
}

// ── CLASE 2: Registro (gestiona la colección) ────────────────
class Registro
{
    // ESTRUCTURA 1: List<> — guarda todos los estudiantes en orden
    private List<Estudiante> listaEstudiantes = new List<Estudiante>();

    // ESTRUCTURA 2: Dictionary<> — acceso rápido por ID
    private Dictionary<int, Estudiante> dictPorId = new Dictionary<int, Estudiante>();

    // ── Operación 1: AGREGAR ─────────────────────────────────
    public void Agregar(int id, string nombre, string carrera, double nota)
    {
        if (dictPorId.ContainsKey(id))
        {
            Console.WriteLine($"\n  ⚠  Ya existe un estudiante con ID {id}.");
            return;
        }
        var est = new Estudiante(id, nombre, carrera, nota);
        listaEstudiantes.Add(est);       // agrega a la lista
        dictPorId.Add(id, est);          // agrega al diccionario
        Console.WriteLine($"\n  ✔  Estudiante '{nombre}' agregado correctamente.");
    }

    // ── Operación 2: LISTAR TODOS ────────────────────────────
    public void ListarTodos()
    {
        Console.WriteLine("\n  ══════════════════════════════════════════════════════");
        Console.WriteLine("   LISTA DE TODOS LOS ESTUDIANTES");
        Console.WriteLine("  ══════════════════════════════════════════════════════");

        if (listaEstudiantes.Count == 0)
        {
            Console.WriteLine("  (No hay estudiantes registrados)");
            return;
        }

        foreach (var est in listaEstudiantes)
        {
            est.Mostrar();
            Console.WriteLine($"         Estado: {est.Estado()}");
        }
        Console.WriteLine($"\n  Total registrados: {listaEstudiantes.Count}");
    }

    // ── Operación 3: BUSCAR por ID (usando Dictionary) ───────
    public void BuscarPorId(int id)
    {
        Console.WriteLine($"\n  Buscando ID {id}...");
        if (dictPorId.ContainsKey(id))
        {
            Console.WriteLine("  Estudiante encontrado:");
            dictPorId[id].Mostrar();
            Console.WriteLine($"  Estado: {dictPorId[id].Estado()}");
        }
        else
        {
            Console.WriteLine("  No se encontró ningún estudiante con ese ID.");
        }
    }

    // ── Operación extra: FILTRAR por carrera ─────────────────
    public void FiltrarPorCarrera(string carrera)
    {
        Console.WriteLine($"\n  Estudiantes de la carrera: {carrera}");
        Console.WriteLine("  ──────────────────────────────────────────────────");
        bool encontro = false;
        foreach (var est in listaEstudiantes)
        {
            if (est.Carrera.ToLower() == carrera.ToLower())
            {
                est.Mostrar();
                encontro = true;
            }
        }
        if (!encontro)
            Console.WriteLine("  No se encontraron estudiantes en esa carrera.");
    }
}

// ── PROGRAMA PRINCIPAL ───────────────────────────────────────
class Program
{
    static void Main(string[] args)
    {
        var registro = new Registro();

        // Datos de ejemplo precargados
        registro.Agregar(1, "Ana García",    "Sistemas",    9.5);
        registro.Agregar(2, "Luis Torres",   "Civil",       6.8);
        registro.Agregar(3, "María Pérez",   "Sistemas",    8.2);
        registro.Agregar(4, "Carlos Ruiz",   "Industrial",  5.0);
        registro.Agregar(5, "Sofía Mendoza", "Civil",       7.9);

        bool salir = false;

        while (!salir)
        {
            Console.WriteLine("\n  ╔══════════════════════════════╗");
            Console.WriteLine("  ║   SISTEMA DE ESTUDIANTES     ║");
            Console.WriteLine("  ╠══════════════════════════════╣");
            Console.WriteLine("  ║  1. Agregar estudiante        ║");
            Console.WriteLine("  ║  2. Listar todos              ║");
            Console.WriteLine("  ║  3. Buscar por ID             ║");
            Console.WriteLine("  ║  4. Filtrar por carrera       ║");
            Console.WriteLine("  ║  5. Salir                     ║");
            Console.WriteLine("  ╚══════════════════════════════╝");
            Console.Write("  Elige una opción: ");

            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    Console.Write("\n  ID: ");
                    int id = int.Parse(Console.ReadLine());
                    Console.Write("  Nombre: ");
                    string nombre = Console.ReadLine();
                    Console.Write("  Carrera: ");
                    string carrera = Console.ReadLine();
                    Console.Write("  Nota (0-10): ");
                    double nota = double.Parse(Console.ReadLine());
                    registro.Agregar(id, nombre, carrera, nota);
                    break;

                case "2":
                    registro.ListarTodos();
                    break;

                case "3":
                    Console.Write("\n  Ingresa el ID a buscar: ");
                    int buscarId = int.Parse(Console.ReadLine());
                    registro.BuscarPorId(buscarId);
                    break;

                case "4":
                    Console.Write("\n  Ingresa la carrera: ");
                    string carr = Console.ReadLine();
                    registro.FiltrarPorCarrera(carr);
                    break;

                case "5":
                    salir = true;
                    Console.WriteLine("\n  Hasta luego!\n");
                    break;

                default:
                    Console.WriteLine("\n  Opción no válida. Intenta de nuevo.");
                    break;
            }
        }
    }
}