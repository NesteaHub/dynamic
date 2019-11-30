using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static int Minim(int q, int w, int[,]e)
        {
            int r = e.GetLength(0)-1;
            int s = int.MaxValue;
            int v = int.MaxValue;
            int sv = int.MaxValue;
            int z = q - 1;      //1ш на север
            if ((z > 0) && (z < r))     
                s = e[z, w];
            
            z = w + 1;      //1ш на восток
            if ((z > 0) && (z < r))     
                v = e[q, z];

            int zi = q - 1;     //1ш на с-в
            int zj = w + 1;
            if (((zi > 0) && (zi < r)) && ((zj > 0) && (zj < r)))
                sv = e[zi, zj];

            z = q - 2;      //2ш на север
            if ((z >= 0) && (z <= r))
                s = s + e[z, w];

            z = w + 2;      //2ш на восток
            if ((z >= 0) && (z <= r))
                v = v + e[q, z];

            int z1 = q - 2;      //2ш на с-в
            int z2 = w + 2;
            if (((z1 >= 0) && (z1 <= r)) && ((z2 >= 0) && (z2 <= r)))
                sv = sv + e[z1, z2];

            int[] mmm = { s, v, sv };
            int a = mmm.Min();
            
            int rez;
            if (a == int.MaxValue)
                rez = 0;
            else rez = a;
            return rez;
        }
        static void Marshrut (int i, int j, int[,] mas, out string c, out int ii, out int jj)
        {
            int r = mas.GetLength(0) - 1;
            int s = int.MaxValue;
            int v = int.MaxValue;
            int sv = int.MaxValue;
            int ii1 = 0; int jj1 = 0;

            int z = i - 2;      //2ш на север
            if ((z >= 0) && (z <= r))   //Проверка, как был сделан переход.
                if ((mas[i, j] - mas[i - 1, j]) == mas[z, j])
                {
                    s = mas[z, j];
                    ii1 = z;
                    jj1 = j;
                }

            z = j + 2;      //2ш на восток
            if ((z >= 0) && (z <= r))        //Проверка, как был сделан переход.
                if ((mas[i, j] - mas[i, j + 1]) == mas[i, z])
                {
                    v = mas[i, z];
                    ii1 = i;
                    jj1 = z;
                }
                    
            int z1 = i - 2;            //Два шага на северо - восток
            int z2 = j + 2;
            if ((z1 >= 0) && (z1 <= r) && (z2 >= 1) && (z2 <= r))   //Проверка, как был сделан переход: по диагонали или вокруг.
                if ((mas[i, j] - mas[i - 1, j + 1]) == mas[z1, z2])
                {
                    sv = mas[z1, z2];
                    ii1 = z1;
                    jj1 = z2;
                }
                    

            c = "";
            int[] mmm = { s, sv, v };
            int m = mmm.Min();
            if (s == m)
                c = "s";
            else if (v == m)
                c = "v";
            else if (sv == m)
                c = "sv";

            ii = ii1;
            jj = jj1;
        }

        static void Main(string[] args)
        {
            int [,] mas = { { 0, 45, 0, 91, 0, 39, 0, 59, 0, 76, 0 }, 
                { 36, 82, 67, 150, 75, 158, 83, 52, 21, 74, 35 }, 
                { 0, 39, 0, 84, 0, 61, 0, 89, 0, 47, 0 }, 
                { 51, 112, 71, 98, 62, 92, 24, 50, 43, 156, 93 }, 
                { 0, 91, 0, 91, 0, 65, 0, 28, 0, 48, 0 }, 
                { 93, 182, 26, 98, 87, 134, 63, 126, 35, 166, 37 }, 
                { 0, 41, 0, 40, 0, 37, 0, 20, 0, 88, 0 }, 
                { 28, 148, 33, 130, 28, 130, 31, 188, 71, 128, 69 }, 
                { 0, 54, 0, 64, 0, 44, 0, 96, 0, 95, 0 }, 
                { 77, 96, 42, 64, 69, 100, 87, 162, 36, 100, 57 },
                { 0, 93, 0, 71, 0, 75, 0, 45, 0, 60, 0 } };
            int razm = mas.GetLength(0)-1;
            int z = 0;

            Console.WriteLine("Исходный массив: ");
            for (int i = 0; i <= razm; i++)
            {
                for (int j = 0; j <= razm; j++)
                    Console.Write(String.Format("{0,5}", mas[i, j]));
                Console.WriteLine();
            }
            Console.WriteLine();

            //Элементы выше и на главной диагонали
            for (int i = 0; i <= razm; i+=2)
            {
                for (int j = 0; j <= i; j += 2)
                {
                    z = razm - i + j;
                    mas[j, z] = Minim(j, z, mas);
                }
            }
            //Элементы ниже главной диагонали
            for (int i = 2; i <= razm - 1; i+=2)
            {
                for (int j = razm; j >= i; j -= 2)
                {
                    z = j - i;
                    mas[j, z] = Minim(j, z, mas);
                }
            }

            //Минимальная стоимость
            mas[razm, 0] = Minim(razm, 0, mas);
            int[,] dan_marsh = mas;
            int min_stoim = mas[razm, 0];

            Console.WriteLine("Преобразованный массив: ");
            for (int i = 0; i <= razm; i++)
            {
                for (int j = 0; j <= razm; j++)
                    Console.Write(String.Format("{0,5}", mas[i, j]));
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine(min_stoim);

            //Обратный проход
            string mar = "";
            int k = razm;
            int l = 0;
            string c = "";
            int ii, jj;
            while ((k > 0) && (l <= razm))
            {
                Marshrut(k, l, mas, out c, out ii, out jj);
                mar = mar + c + " ";
                k = ii;
                l = jj;
            }
            Console.WriteLine(mar);
        }
    }
}
