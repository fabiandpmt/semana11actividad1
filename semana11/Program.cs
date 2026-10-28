using System;

using System.Collections.Generic;

using System.IO;

using System.Linq;

using System.Text;

using System.Threading.Tasks;



namespace ConsoleApp1

{

    internal class ActividadadSem11

    {

        static int max_estudiantes = 40;

        static int max_cursos = 4;

        static string[] nombre_de_cursos = { "matematicas", "programacion", "Base de datos", "Fisica" };

        static String[] estudiantes = new string[max_estudiantes];

        static double[,] matrizNotas = new double[max_estudiantes, max_cursos];

        static int total_estudiantes = 0;

        static string rutaArchivo = "notas_cursos.txt";

        public static void RegistrarEstudiantes()

        {

            Console.WriteLine("Registro de estudiantes");

            if (total_estudiantes >= max_estudiantes)

            {

                Console.WriteLine("Cápacidad maxima de estudiantes alcanzada");

                return;

            }

            string nombre = Validador.LeerTexto("Ingresar nombres y apellidos:").ToUpper();

            estudiantes[total_estudiantes] = nombre;

            Console.WriteLine("Ingresar notas para cada curso(0-20)");

            for (int i = 0; i < max_cursos; i++)

            {

                matrizNotas[total_estudiantes, i] = Validador.LeerDecimal($"-{nombre_de_cursos[i]}:");

            }

            total_estudiantes++;

            Console.WriteLine("Estudiante Registro..!");



        }

        public static void GuardarArchivo()

        {

            Console.WriteLine("GUARDAR ARCHIVO");

            if (total_estudiantes == 0)

            {

                Console.WriteLine("no hay registros por guardar");

                return;

            }

            using (StreamWriter escribir = new StreamWriter(rutaArchivo))

            {

                for (int i = 0; i < total_estudiantes; i++)

                {

                    string linea = estudiantes[i];

                    for (int j = 0; j < max_cursos; j++)

                    {

                        linea += $",{matrizNotas[i, j]}";

                        //linea += "," + matrizNotas[i,j];

                    }

                    escribir.WriteLine(linea);

                }

            }

            Console.WriteLine("Registro guardados");

        }

        public static void CargarRegistros()

        {

            Console.WriteLine("Cargar registros desde archivo");

            if (!File.Exists(rutaArchivo))

            {

                Console.WriteLine("No encontro el Archivo");

                return;

            }

            total_estudiantes = 0;

            using (StreamReader lector = new StreamReader(rutaArchivo))

            {

                string linea;

                while ((linea = lector.ReadLine()) != null && total_estudiantes < max_estudiantes)

                {

                    string[] partes = linea.Split(',');

                    if (partes.Length == max_cursos + 1)

                    {

                        estudiantes[total_estudiantes] = partes[0];

                        for (int i = 0; i < max_cursos; i++)

                        {

                            matrizNotas[total_estudiantes, i] = Convert.ToDouble(partes[i + 1]);

                        }

                        total_estudiantes++;

                    }

                }

            }

            Console.WriteLine("Se cargaron " + total_estudiantes + " con sus notas");

        }

        public static void MostrarRegistros()

        {

            Console.WriteLine(new string('=', 100));

            Console.WriteLine("\t\t REPORTE GENERAL DE NOTAS POR CURSOS");

            Console.WriteLine(new string('=', 100));

            if (total_estudiantes == 0)

            {

                Console.WriteLine("No hay registros");

                return;

            }

            Console.Write($"{"ESTUDIANTE",-25}|");

            for (int i = 0; i < max_cursos; i++)

            {

                Console.Write($"{nombre_de_cursos[i],-12}|");

            }

            Console.WriteLine("PROMEDIO");

            Console.WriteLine(new string('-', 100));

            for (int i = 0; i < total_estudiantes; i++)

            {

                Console.Write($"{estudiantes[i],-25}|");

                double suma = 0;

                for (int j = 0; j < max_cursos; j++)

                {

                    Console.Write($"{matrizNotas[i, j],-12:F1}|");

                    suma += matrizNotas[i, j];

                }

                double promedio = suma / max_cursos;

                string estado = promedio >= 12 ? "APROBADO" : "DESAPROBADO";

                Console.WriteLine($"{promedio:F2}({estado})");

            }

        }

        static void Main(string[] args)

        {

            int opc = 0;

            while (opc != 5)

            {

                Console.Clear();

                Console.WriteLine("[1] Registrar estudiante");

                Console.WriteLine("[2] Guardar en Archivo");

                Console.WriteLine("[3] Cargar el Archivo");

                Console.WriteLine("[4] Mostrar los Registros");

                Console.WriteLine("[5] salir");

                opc = Validador.LeerEnteros("Seleccione la opcion [1-5]:");

                switch (opc)

                {

                    case 1:

                        RegistrarEstudiantes(); break;

                    case 2:

                        GuardarArchivo(); break;

                    case 3:

                        CargarRegistros(); break;

                    case 4:

                        MostrarRegistros(); break;

                    case 5:

                        Console.WriteLine("Muchos exitos para tu examen final"); break;

                }

                Console.ReadKey();

            }

        }

    }

}