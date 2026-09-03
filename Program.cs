using System;
using System.Collections.Generic;
using System.Diagnostics;

class Libro
{
    public string ISBN;
    public string Titulo;
    public string Autor;
    public string Genero;

    public Libro(string isbn, string titulo, string autor, string genero)
    {
        ISBN = isbn;
        Titulo = titulo;
        Autor = autor;
        Genero = genero;
    }
}

class Program
{
    static void Main()
    {
        // Diccionario principal: ISBN -> Libro (búsqueda directa O(1))
        Dictionary<string, Libro> catalogo = new Dictionary<string, Libro>();

        // Mapa que agrupa los títulos por género
        Dictionary<string, List<string>> librosPorGenero = new Dictionary<string, List<string>>();

        // Conjunto de géneros únicos registrados
        HashSet<string> generosDisponibles = new HashSet<string>();

        Console.WriteLine("=== SISTEMA DE REGISTRO DE LIBROS - BIBLIOTECA ===\n");

        bool salir = false;
        while (!salir)
        {
            Console.WriteLine("\n--- MENÚ ---");
            Console.WriteLine("1. Registrar libro");
            Console.WriteLine("2. Buscar libro por ISBN");
            Console.WriteLine("3. Reporte: Ver todos los libros registrados");
            Console.WriteLine("4. Reporte: Ver libros agrupados por género");
            Console.WriteLine("5. Reporte: Ver géneros disponibles");
            Console.WriteLine("6. Analizar tiempo de ejecución (prueba automática)");
            Console.WriteLine("7. Salir");
            Console.Write("Opción: ");
            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    Console.Write("ISBN: ");
                    string isbn = Console.ReadLine();

                    if (catalogo.ContainsKey(isbn))
                    {
                        Console.WriteLine("Ya existe un libro registrado con ese ISBN.");
                        break;
                    }

                    Console.Write("Título: ");
                    string titulo = Console.ReadLine();
                    Console.Write("Autor: ");
                    string autor = Console.ReadLine();
                    Console.Write("Género: ");
                    string genero = Console.ReadLine();

                    Libro nuevoLibro = new Libro(isbn, titulo, autor, genero);
                    catalogo.Add(isbn, nuevoLibro);
                    generosDisponibles.Add(genero);

                    if (!librosPorGenero.ContainsKey(genero))
                    {
                        librosPorGenero[genero] = new List<string>();
                    }
                    librosPorGenero[genero].Add(titulo);

                    Console.WriteLine($" -> Libro \"{titulo}\" registrado correctamente.");
                    break;

                case "2":
                    Console.Write("Ingrese el ISBN a buscar: ");
                    string isbnBuscado = Console.ReadLine();

                    if (catalogo.TryGetValue(isbnBuscado, out Libro libroEncontrado))
                    {
                        Console.WriteLine($"\nISBN: {libroEncontrado.ISBN}");
                        Console.WriteLine($"Título: {libroEncontrado.Titulo}");
                        Console.WriteLine($"Autor: {libroEncontrado.Autor}");
                        Console.WriteLine($"Género: {libroEncontrado.Genero}");
                    }
                    else
                    {
                        Console.WriteLine("No se encontró ningún libro con ese ISBN.");
                    }
                    break;

                case "3":
                    Console.WriteLine($"\n[REPORTE] Libros registrados ({catalogo.Count}):");
                    if (catalogo.Count == 0)
                    {
                        Console.WriteLine(" (no hay libros registrados)");
                    }
                    else
                    {
                        foreach (KeyValuePair<string, Libro> par in catalogo)
                        {
                            Libro l = par.Value;
                            Console.WriteLine($" - [{l.ISBN}] {l.Titulo} | {l.Autor} | {l.Genero}");
                        }
                    }
                    break;

                case "4":
                    Console.WriteLine($"\n[REPORTE] Libros agrupados por género ({librosPorGenero.Count} géneros):");
                    if (librosPorGenero.Count == 0)
                    {
                        Console.WriteLine(" (no hay libros registrados)");
                    }
                    else
                    {
                        foreach (KeyValuePair<string, List<string>> par in librosPorGenero)
                        {
                            Console.WriteLine($"\n {par.Key}:");
                            foreach (string t in par.Value)
                            {
                                Console.WriteLine($"   - {t}");
                            }
                        }
                    }
                    break;

                case "5":
                    Console.WriteLine($"\n[REPORTE] Géneros disponibles ({generosDisponibles.Count}):");
                    if (generosDisponibles.Count == 0)
                    {
                        Console.WriteLine(" (ninguno todavía)");
                    }
                    else
                    {
                        foreach (string g in generosDisponibles)
                        {
                            Console.WriteLine($" - {g}");
                        }
                    }
                    break;

                case "6":
                    AnalizarTiempoEjecucion();
                    break;

                case "7":
                    salir = true;
                    Console.WriteLine("Saliendo del sistema...");
                    break;

                default:
                    Console.WriteLine("Opción inválida, intente de nuevo.");
                    break;
            }
        }
    }

    // Prueba automática: mide el tiempo real de inserción y búsqueda en el
    // diccionario, sin depender de la velocidad de escritura del usuario.
    static void AnalizarTiempoEjecucion()
    {
        Console.WriteLine("\n=== ANÁLISIS DE TIEMPO DE EJECUCIÓN ===");

        int totalLibros = 1000;
        Dictionary<string, Libro> catalogoPrueba = new Dictionary<string, Libro>();
        HashSet<string> generosPrueba = new HashSet<string>();

        Console.WriteLine($"Simulando el registro de {totalLibros} libros...\n");

        // Medir inserción en el diccionario (Add)
        Stopwatch swInsercion = Stopwatch.StartNew();
        for (int i = 1; i <= totalLibros; i++)
        {
            string isbn = "ISBN-" + i;
            Libro libro = new Libro(isbn, "Libro " + i, "Autor " + (i % 50), "Género" + (i % 10));
            catalogoPrueba.Add(isbn, libro);
            generosPrueba.Add(libro.Genero);
        }
        swInsercion.Stop();

        // Medir búsqueda en el diccionario (TryGetValue)
        Stopwatch swBusqueda = Stopwatch.StartNew();
        for (int i = 1; i <= totalLibros; i++)
        {
            catalogoPrueba.TryGetValue("ISBN-" + i, out Libro _);
        }
        swBusqueda.Stop();

        Console.WriteLine($"Tiempo total en registrar {totalLibros} libros (Dictionary.Add): {swInsercion.Elapsed.TotalMilliseconds:F4} ms");
        Console.WriteLine($"Tiempo total en buscar {totalLibros} libros (Dictionary.TryGetValue): {swBusqueda.Elapsed.TotalMilliseconds:F4} ms");
        Console.WriteLine($"Tiempo promedio por inserción: {(swInsercion.Elapsed.TotalMilliseconds / totalLibros):F6} ms");
        Console.WriteLine($"Tiempo promedio por búsqueda: {(swBusqueda.Elapsed.TotalMilliseconds / totalLibros):F6} ms");
    }
}