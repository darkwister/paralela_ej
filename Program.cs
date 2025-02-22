using System;
using System.Numerics;
namespace paralela_ej{
    class Program{
        static void Main(string[] args){
            Console.WriteLine("Ejemplos de el uso de cada tipo:");
            functionSISD(); //Operacion SISD
            functionSIMD(); //Operacion SIMD
            functionMISD(); //Operacion MISD
            functionMIMD(); //Operacion MIMD
        }
        static void functionSISD(){
            int num_a = 2, num_b = 3;
            int sum = num_a + num_b;//Operacion secuencial
            Console.WriteLine("Resultado:" + sum);
        }
        static void functionSIMD(){
            //Porque me estaba dando excepciones en la consola, agrege este if
            if (!Vector.IsHardwareAccelerated) {
              Console.WriteLine("SIMD no es compatible.");
            }
            //Instancia el tamaño de los arreglos
            int vectorSize = Vector<int>.Count;
            int[] array1 = new int[vectorSize];
            int[] array2 = new int[vectorSize];

            // Llenar los arrays con datos
            for (int i = 0; i < vectorSize; i++) {
            array1[i] = i + 1;  // 1, 2, 3, 4, ...
            array2[i] = (i + 1) * 2;  // 2, 4, 6, 8, ...
            }

            //Crea los vectores
            Vector<int> vector1 = new Vector<int>(array1);
            Vector<int> vector2 = new Vector<int>(array2);

            // Operación SIMD 
            Vector<int> resultVector = vector1 + vector2;

            // Mostrar el resultado
            Console.WriteLine("Resultado SIMD:");
            for (int i = 0; i < vectorSize; i++){
                Console.WriteLine(resultVector[i]);
            }
        }
        static void TablaMultiplicar(int num){
            for (int i = 0; i <= 12; i++){
                Console.WriteLine($"{i+1}. {num} x {i} = {num * i}");
            } 
        }
        static void procesar(int id, int value){
            Console.WriteLine($"Hilo {id} procesando valor: {value*2}");
        }
        static int potencia(int num) => num*10;

        static void functionMISD(){
            int data = 10;
            Parallel.Invoke(
                () => Console.WriteLine("Raiz cuadrada: " + Math.Sqrt(data)),
                () => Console.WriteLine($"Cuadrado: {data * data}"),
                () => TablaMultiplicar(data),
                () => Console.WriteLine($"Potencia a la 10: {potencia(data)}")
            );
        }
        static void functionMIMD(){
            //Instancio los hilos
            Thread thread1 = new Thread(() => procesar(1,5));
            Thread thread2 = new Thread(() => Console.WriteLine(potencia(2).ToString()));
            Thread thread3 = new Thread(() => TablaMultiplicar(8));
            //Inicializo los hilos
            thread1.Start();
            thread2.Start();
            thread3.Start();
            //Espero a los hilos
            thread1.Join();
            thread2.Join();
            thread3.Join();
        }

    }

}