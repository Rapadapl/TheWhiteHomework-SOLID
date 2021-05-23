using System;
using System.Collections.Generic;

namespace Book
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowPosition(0, 0);
            Console.WindowHeight = Console.LargestWindowHeight;
            
            Console.WriteLine("Введите текст книги...");
            var a = Console.ReadLine();
            Console.Clear();
            List<string> lines = new List<string>();
            var words = a.Split(" ");
            string line = "";
            int s = 0;
            foreach (var word in words)
            {
                if (s + word.Length <= 36)
                {
                    line += word; line += " ";
                    s += word.Length; s++;
                }
                else
                {
                    lines.Add(line);
                    line = "";
                    s = 0;
                    line += word; line += " ";
                    s += word.Length; s++;
                }
            }

            int now = 0;
            int rows = 10;
            int maxrow = lines.Count;

            for (int i = now; i < Math.Clamp(now + rows, 0, maxrow); ++i)
            {
                Console.WriteLine(lines[i]);
            }
            now = Math.Clamp(now + rows, 0, maxrow);

            while (true)
            {
                while (Console.KeyAvailable) Console.ReadKey(true);
                var Input = Console.ReadKey();
                Console.WriteLine();

                switch (Input.Key)
                {
                    case ConsoleKey.RightArrow:
                        Console.Clear();
                        for (int i = now; i < Math.Clamp(now + rows, 0, maxrow); ++i)
                        {
                            Console.WriteLine(lines[i]);
                        }
                        now = Math.Clamp(now + rows, 0, maxrow);
                        break;
                    case ConsoleKey.LeftArrow:
                        Console.Clear();
                        for (int i = Math.Clamp(now - rows, 0, maxrow); i < now; ++i)
                        {
                            Console.WriteLine(lines[i]);
                        }
                        now = Math.Clamp(now - rows, 0, maxrow);
                        break;
                }
            }


        }
    }
}

