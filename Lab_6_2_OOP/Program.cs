using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Lab_6_2_OOP
{
    abstract class ICourse
    {
        public abstract void AddTask(List<PracticeTask> task);
        public abstract void RemoveTask(List<PracticeTask> tasks);
    }


    class Course : ICourse
    {
        public string Name;
        public bool IsExam;

        public Course() { }

        public override void AddTask(List<PracticeTask> tasks)
        {
            Console.Write("Назва: ");
            string name = Console.ReadLine();
            Console.Write("Наявнiсть iспиту: ");
            bool isExam = Convert.ToBoolean(Console.ReadLine());
            Console.Write("Дата: ");
            string date = Console.ReadLine();
            Console.Write("Тема: ");
            string topic = Console.ReadLine();
            Console.Write("Кiлькiсть студентiв: ");
            int amountStudents = Convert.ToInt32(Console.ReadLine());

            tasks.Add(new PracticeTask(name, isExam, date, topic, amountStudents));
            Program.WriteDB(tasks);
        }

        public static void EditTask(List<PracticeTask> tasks)
        {
            Console.Write("Введiть тему заняття, яке бажаєте редагувати: ");
            string topic = Console.ReadLine();

            if (tasks.All(t => t.topic != topic))
            {
                Console.WriteLine("Заняття з такою темою не iснує!");
                return;
            }

            Console.WriteLine("");
            Console.WriteLine("Назва - 1");
            Console.WriteLine("Наявнiсть iспиту - 2");
            Console.WriteLine("Дата - 3");
            Console.WriteLine("Тема - 4");
            Console.WriteLine("Кiлькiсть студентiв - 5");
            Console.WriteLine("Назад - 0");

            Console.Write("Ваш вибiр: ");
            int k = Convert.ToInt32(Console.ReadLine());

            if (k == 1)
            {
                Console.Write("Нова назва: ");
                string newName = Console.ReadLine();
                tasks.FindAll(s => s.topic == topic).ForEach(x => x.name = newName);
                Program.WriteDB(tasks);
            }
            else if (k == 2)
            {
                Console.Write("Новий: ");
                bool newIsExam = Convert.ToBoolean(Console.ReadLine());
                tasks.FindAll(s => s.topic == topic).ForEach(x => x.isExam = newIsExam);
                Program.WriteDB(tasks);
            }
            else if (k == 3)
            {
                Console.Write("Нова дата: ");
                string newDate = Console.ReadLine();
                tasks.FindAll(s => s.topic == topic).ForEach(x => x.date = newDate);
                Program.WriteDB(tasks);
            }
            else if (k == 4)
            {
                Console.Write("Нова тема: ");
                string newTopic = Console.ReadLine();
                tasks.FindAll(s => s.topic == topic).ForEach(x => x.topic = newTopic);
                Program.WriteDB(tasks);
            }
            else if (k == 5)
            {
                Console.Write("Новий кiлькiсть студентiв: ");
                int newAmountStudents = Convert.ToInt32(Console.ReadLine());
                tasks.FindAll(s => s.topic == topic).ForEach(x => x.amountStudents = newAmountStudents);
                Program.WriteDB(tasks);
            }
            else if (k == 0) return;
        }

        public override void RemoveTask(List<PracticeTask> tasks)
        {
            Console.Write("Введiть тему заняття, яке бажаєте видалити: ");
            string topic = Console.ReadLine();
            if (tasks.All(t => t.topic != topic))
            {
                Console.WriteLine("Заняття з такою темою не iснує!");
                return;
            }
            var itemToDelete = tasks.Where(x => x.topic == topic).Select(x => x).First();
            tasks.Remove(itemToDelete);
            Program.WriteDB(tasks);
        }

        public static void ShowTasks(List<PracticeTask> tasks)
        {
            Console.WriteLine("+----------------------------+------------------+-----------------+-------------------------+---------------------+");
            Console.WriteLine("|        Назва курсу         | Наявнiсть iспиту |       Дата      |          Тема           | Кiлькiсть студентiв |");
            Console.WriteLine("+----------------------------+------------------+-----------------+-------------------------+---------------------+");
            foreach (PracticeTask item in tasks)
            {
                Console.WriteLine(String.Format("| {0,-26} | {1,-16} | {2,-15} | {3,-23} | {4,-19} |", item.name, item.isExam, item.date, item.topic, item.amountStudents));
            }
            Console.WriteLine("+----------------------------+------------------+-----------------+-------------------------+---------------------+");
        }



        static public void AverageAmountStudents(List<PracticeTask> tasks)
        {
            int i = 0;
            double p = 0;

            var resultList = tasks.FindAll(delegate (PracticeTask s) { return s.amountStudents > 0; });
            foreach (PracticeTask item in tasks)
            {
                p += item.amountStudents;
                i++;
            }
            p = p / i;
            Console.WriteLine("Середня кiлькiсть студентiв: " + Math.Round(p));

        }

        public static void MaxAmountStudents(List<PracticeTask> tasks)
        {
            int p = tasks.Max(point => point.amountStudents);

            var resultList = tasks.FindAll(item => item.amountStudents == p);
            Console.WriteLine("+----------------------------+------------------+-----------------+-------------------------+---------------------+");
            foreach (PracticeTask item in resultList)
            {
                Console.WriteLine(String.Format("| {0,-26} | {1,-16} | {2,-15} | {3,-23} | {4,-19} |", item.name, item.isExam, item.date, item.topic, item.amountStudents));
            }
            Console.WriteLine("+----------------------------+------------------+-----------------+-------------------------+---------------------+");
        }

        public static void SearchWithSubstring(List<PracticeTask> tasks)
        {
            Console.Write("Введiть пiдстроку: ");
            string srch = Console.ReadLine();

            var resultList = tasks.FindAll(delegate (PracticeTask s) { return s.topic.Contains(srch); });
            Console.WriteLine("+----------------------------+------------------+-----------------+-------------------------+---------------------+");
            foreach (PracticeTask item in resultList)
            {
                Console.WriteLine(String.Format("| {0,-26} | {1,-16} | {2,-15} | {3,-23} | {4,-19} |", item.name, item.isExam, item.date, item.topic, item.amountStudents));
            }
            Console.WriteLine("+----------------------------+------------------+-----------------+-------------------------+---------------------+");
        }



        public string name
        {
            get
            {
                return Name;
            }
            set
            {
                Name = value;
            }
        }
        public bool isExam
        {
            get
            {
                return IsExam;
            }
            set
            {
                IsExam = value;
            }
        }

    }
    class PracticeTask : Course
    {
        private string Date;
        private string Topic;
        private int AmountStudents;
        public PracticeTask() { }
        public PracticeTask(string name, bool isExam, string date, string topic, int amountStudents)
        {
            Name = name;
            IsExam = isExam;
            Date = date;
            Topic = topic;
            AmountStudents = amountStudents;
        }

        public string date
        {
            get
            {
                return Date;
            }
            set
            {
                Date = value;
            }
        }
        public string topic
        {
            get
            {
                return Topic;
            }
            set
            {
                Topic = value;
            }
        }
        public int amountStudents
        {
            get
            {
                return AmountStudents;
            }
            set
            {
                AmountStudents = value;
            }
        }
    }

    class Program
    {
        public static void WriteDB(List<PracticeTask> tasks)
        {
            string textRow;
            StreamWriter file = new StreamWriter("output.txt");
            foreach (PracticeTask t in tasks)
            {
                textRow = t.name + ";" + Convert.ToString(t.isExam) + ";" + t.date + ";" +
                    t.topic + ";" + Convert.ToString(t.amountStudents);
                file.WriteLine(textRow);
            }
            file.Close();
        }

        public static List<PracticeTask> ReadBD()
        {
            string textRow;
            string pName, sIsExam, pDate, pTopic, sAmountStudents;
            int pAmountStudents;
            bool pIsExam;
            int i, ip;

            List<PracticeTask> tasks = new List<PracticeTask>();

            StreamReader file = new StreamReader("output.txt");
            while (file.Peek() >= 0)
            {
                pName = ""; sIsExam = ""; pDate = ""; pTopic = ""; sAmountStudents = "";
                textRow = file.ReadLine();
                i = textRow.IndexOf(';') - 1;
                for (int j = 0; j <= i; j++) pName = pName + textRow[j];
                ip = i + 2;
                i = textRow.IndexOf(';', ip) - 1;
                for (int j = ip; j <= i; j++) sIsExam = sIsExam + textRow[j];
                ip = i + 2;
                i = textRow.IndexOf(';', ip) - 1;
                for (int j = ip; j <= i; j++) pDate = pDate + textRow[j];
                ip = i + 2;
                i = textRow.IndexOf(';', ip) - 1;
                for (int j = ip; j <= i; j++) pTopic = pTopic + textRow[j];
                ip = i + 2;
                for (int j = ip; j <= textRow.Length - 1; j++) sAmountStudents = sAmountStudents + textRow[j];

                pIsExam = Convert.ToBoolean(sIsExam);
                pAmountStudents = Convert.ToInt32(sAmountStudents);
                tasks.Add(new PracticeTask(pName, pIsExam, pDate, pTopic, pAmountStudents));
            }
            file.Close();
            return tasks;
        }

        static void Main(string[] args)
        {
            List<PracticeTask> tasks = ReadBD();

            while (true)
            {
                Console.WriteLine("");
                Console.WriteLine("Оберiть: ");
                Console.WriteLine("Додати запис - 1");
                Console.WriteLine("Редагувати запис - 2");
                Console.WriteLine("Видалити запис - 3");
                Console.WriteLine("Показати таблицю - 4");
                Console.WriteLine("Середня кiлькiсть студентiв - 5");
                Console.WriteLine("Заняття з максимальною кiлькiстю студентiв - 6");
                Console.WriteLine("Список тем з певним словом у назвi - 7");
                Console.WriteLine("Вийти - 0");

                Console.Write("Ваш вибiр: ");
                int k = Convert.ToInt32(Console.ReadLine());

                if (k == 1) tasks[0].AddTask(tasks);
                if (k == 2) PracticeTask.EditTask(tasks);
                else if (k == 3) tasks[0].RemoveTask(tasks);
                else if (k == 4) PracticeTask.ShowTasks(tasks);
                else if (k == 5) PracticeTask.AverageAmountStudents(tasks);
                else if (k == 6) PracticeTask.MaxAmountStudents(tasks);
                else if (k == 7) PracticeTask.SearchWithSubstring(tasks);
                else if (k == 0) break;
            }
        }
    }
}
