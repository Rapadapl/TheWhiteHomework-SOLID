using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace Book
{
    abstract class Printer
    {
        public abstract void Print(System.ConsoleKeyInfo a, Book book);
        public abstract void AnimatedPrint(System.ConsoleKeyInfo a, Book book);
    }



    class Book : Generator
    {
        //static int lines = 30;
        // static int columns = 36;
        private List<Page> pages;
        private int now = 0;
        private int max = 0;
        public Book(string a)
        {
            pages = generate(a);
            max = pages.Count;
        }

        public void GetPage()
        {
            //List<string> lines = new List<string>();
            if (now == 0)
            {
                Console.Write("{0, 26}", " ");
                Console.WriteLine("-------------------------------------------------------------------------------");
                var lines = pages[now].GetLines();
                int i = 0;
                foreach (var line in lines)
                {
                    Console.WriteLine("{0, 25} |{1, -38}|{2, -38}|", "  ", " ", line);
                    i++;
                }
                if (i != 30)
                    for (; i < 30; i++) Console.WriteLine("{0, 25} |{1, -38}|{2, -38}|", " ", " ", " ");
                Console.Write("{0, 26}", " ");
                Console.WriteLine("-------------------------------------------------------------------------------");
            }
            else
            {
                Console.Write("{0, 26}", " ");
                Console.WriteLine("-------------------------------------------------------------------------------");

                if (now + 1 < max)
                {
                    var lines1 = pages[now].GetLines();
                    var lines2 = pages[now + 1].GetLines();
                    int min, maxx, sign = 0;
                    if (lines1.Count > lines2.Count)
                    {
                        min = lines2.Count; sign = 1; maxx = lines1.Count;
                    }
                    else if (lines1.Count > lines2.Count)
                    {
                        min = lines1.Count; sign = 2; maxx = lines2.Count;
                    }
                    else
                    {
                        min = lines1.Count; maxx = lines2.Count;
                    }

                    int i = 0;
                    for (; i < min; i++)
                    {
                        Console.WriteLine("{0, 25} |{1, -38}|{2, -38}|", "  ", lines1[i], lines2[i]);

                    }
                    for (; i < max; i++)
                    {
                        if (sign == 1) Console.WriteLine("{0, 25} |{1, -38}|{2, -38}|", "  ", lines1[i], " ");
                        if (sign == 2) Console.WriteLine("{0, 25} |{1, -38}|{2, -38}|", "  ", " ", lines2[i]);
                    }
                    Console.Write("{0, 26}", " ");
                    Console.WriteLine("-------------------------------------------------------------------------------");
                }
                else
                {
                    var lines = pages[now].GetLines();
                    int i = 0;
                    foreach (var line in lines)
                    {
                        Console.WriteLine("{0, 25} |{1, -38}|{2, -38}|", "  ", line, " ");
                        i++;
                    }

                    if (i != 30)
                        for (; i < 30; i++) Console.WriteLine("{0, 25} |{1, -38}|{2, -38}|", " ", " ", " ");
                    Console.Write("{0, 26}", " ");
                    Console.WriteLine("-------------------------------------------------------------------------------");
                }

            }

        }

        public void NextPage()
        {
            if ((now == 0) && (now +1 < max)) now++;
            else if (now + 2 < max) now += 2;

        }
        public void PreviousPage()
        {
            if (now == 1) now--;
            else if (now - 2 > 0) 
                now -= 2;
        }
    }

    class Page
    {

        private List<string> lines;
        public Page(List<string> lines)
        {
            this.lines = lines;
        }

        public string GetLine(int num_line)
        {
            return lines[num_line];
        }

        public List<string> GetLines()
        {
            return lines;
        }
    }

    class Generator

    {
        protected List<Page> generate(string a)
        {
            string input = a;
            while (true)
            {
                if (a.Contains('\n')) a = a.Replace("\n", "");
                else break;
            }
            var temp = input.Split(" ");
            List<string> lines = new List<string>();
            List<Page> pages = new List<Page>();

            int s = 0;
            string line = "";
            foreach (var item in temp)
            {
                if (lines.Count == 30)
                {


                    pages.Add(new Page(lines.ToList()));
                    lines.Clear();
                    line = "";
                }

                if (s + item.Length <= 36)
                {
                    line += item; line += " ";
                    s += item.Length; s++;
                }
                else
                {
                    lines.Add(line);
                    line = "";
                    s = 0;
                    line += item; line += " ";
                    s += item.Length; s++;
                }
            }
            if (line != "") lines.Add(line);
            if (lines.Count != 0) pages.Add(new Page(lines));
            return pages;
        }
    }


    class Program : Printer
    {
        public override void AnimatedPrint(ConsoleKeyInfo a, Book book)
        {
            throw new NotImplementedException();
        }

        public override void Print(ConsoleKeyInfo a, Book book)
        {
            switch (a.Key)
            {
                case ConsoleKey.Escape:
                    Console.Clear();
                    Console.WriteLine();
                    Console.WriteLine("До свидания");
                    Environment.Exit(0);
                    break;
                case ConsoleKey.Q:
                    Console.Clear();
                    Console.WriteLine();
                    Console.WriteLine("До свидания");
                    Environment.Exit(0);
                    break;
                case ConsoleKey.RightArrow:
                    Console.Clear();
                    book.NextPage();
                    book.GetPage();
                    break;
                case ConsoleKey.LeftArrow:
                    Console.Clear();
                    book.PreviousPage();
                    book.GetPage();
                    break;
            }
        }



        public class ConsoleSpiner
        {
            int counter;
            public ConsoleSpiner()
            {
                counter = 0;
            }
            public void Turn()
            {
                Thread.Sleep(250);
                counter++;
                switch (counter % 4)
                {
                    case 0: Console.Write("/"); break;
                    case 1: Console.Write("-"); break;
                    case 2: Console.Write("\\"); break;
                    case 3: Console.Write("|"); break;
                }
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
            }
        }







        static void Main(string[] args)
        {
            Console.SetWindowPosition(0, 0);
            Console.WindowHeight = Console.LargestWindowHeight;
            ConsoleSpiner spin = new ConsoleSpiner();
            Console.Write("Загрузка... ");
            for (int i = 0; i < 12; ++i)
            {
                spin.Turn();
            }
            Console.Clear();
            Console.WriteLine("Введите текст книги...");
            var a = Console.ReadLine();
            Book book1 = new Book(a);
            Console.Clear();
            book1.GetPage();

                while (true)
                {
                    while (Console.KeyAvailable) Console.ReadKey(true);
                    var Input = Console.ReadKey();
                    Console.WriteLine();
                    Program program = new Program();
                    program.Print(Input, book1);
                }
            
        }
        
    }
}
