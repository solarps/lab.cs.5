using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab.cs._5
{

    //Многоадресный и одноадресный делегат;

    delegate void Multicast();
    delegate void Unicast();

    class Program
    {

        //Свойства класса, принимающие изначальные значения;
        private string Journal { get; set; } = "Вокруг света";
        private string Book { get; set; } = "Война и мир";
        private string PrintEdition { get; set; } = "Ведомости";
        private string Textbook { get; set; } = "Литература";

        //Конструктор с параметрами для переопределения значений если пользователь захочет их изменить;
        public Program(string NewJournal, string NewBook, string NewPrintEdition, string NewTextbook)
        {
            Journal = NewJournal;
            Book = NewBook;
            PrintEdition = NewPrintEdition;
            Textbook = NewTextbook;
        }
        public Program()
        { }
        static void Main(string[] args)
        {
            Program program = new Program();
            Unicast one_for_all = program.Input;
            one_for_all();
        }

        //Конструктора класса Inform принимают значения полей и записывают их в свойства;
        public void Input()
        {
            Information inform = new Information(Journal, Book, PrintEdition, Textbook);
            Write(inform);
        }
        static void Write(IJournal Journal)
        {
            Multicast writeInfo = Journal.Write_Journal;
            writeInfo();
        }
    }
    class Information : IJournal
    {
        private string Journal { get; set; }
        private string Book { get; set; }
        private string PrintEdition { get; set; }
        private string Textbook { get; set; }


        public Information(string journal, string book, string printEdition, string textbook)
        {
            Journal = journal;
            Book = book;
            PrintEdition = printEdition;
            Textbook = textbook;
        }
        public Information()
        { }
        void IJournal.Write_Journal()
        {
            Console.WriteLine($"\nДанные об журнале:\nЖурнал: " + Journal);
            Console.WriteLine($"Книга: " + Book);
            Console.WriteLine($"Печатное издание: " + PrintEdition);
            Console.WriteLine($"Учебник: " + Textbook + "\n");
            Console.Write("Если вы хотите изменить данные нажмите 1, если нет - нажмите любое другое число.\n");

            if (Convert.ToInt32(Console.ReadLine()) == 1)
            {
                ChangeInfo write = new ChangeInfo();
                Write_1(write);
            }
            else
            {
                Console.WriteLine("Вы отказались от изменений\n");
                Console.ReadLine();
            }
        }
        static void Write_1(ChangeInfo1 write)
        {
            Unicast writeInfo = write.Change_Info;
            writeInfo();
        }
    }
    interface IJournal
    {
        void Write_Journal();
    }
    class ChangeInfo : ChangeInfo1
    {
        private string NewJournal { get; set; }
        private string NewBook { get; set; }
        private string NewPrintEdition { get; set; }
        private string NewTextbook { get; set; }

        public ChangeInfo()
        { }

        void ChangeInfo1.Change_Info()
        {

            Console.Write("\nВведите новое название журнала: ");
            NewJournal = Console.ReadLine();
            Console.Write("Введите новое название книги: ");
            NewBook = Console.ReadLine();
            Console.Write("Введите новое название печатного издания: ");
            NewPrintEdition = Console.ReadLine();
            Console.Write("Введите новое название учебника: ");
            NewTextbook = Console.ReadLine();
            Program program = new Program(NewJournal, NewBook, NewPrintEdition, NewTextbook);
            Unicast inputInfo = program.Input;
            inputInfo();
        }
    }

    interface ChangeInfo1
    {
        void Change_Info();
    }
}