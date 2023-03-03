using System;

namespace Level1
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            Number1.Program.Start();
        }
    }
}
namespace Number1
{
    public static class Program
    {
        /// Результаты соревнований по прыжкам в длину определяются по сумме двух попыток.
        /// В протоколе для каждого участника указываются: фамилия, общество, результаты
        /// первой и второй попыток. Вывести протокол в виде таблицы с заголовком в порядке
        /// занятых мест.

        public static void Start()
        {
            Student[] plr = new Student[6];
            for (int i = 0; i < plr.Length; i++)
            {
                plr[i] = new Student($"F{i+1}",$"Society{i+1}", i+1, i+2);
            }
            for (int i = 0; i < plr.Length - 1; i++)
            {
                double lmax = plr[i].srez;
                int pmax = i;
                for (int j = i + 1; j < plr.Length; j++)
                {
                    Student temp;
                    if (plr[j].srez > plr[i].srez)
                    {
                        temp = plr[i];
                        plr[i] = plr[j];
                        plr[j] = temp;
                    }
                }
            }
            for (int i = 0; i < plr.Length; i++)
            {
                Console.WriteLine("Фамилия: {0}\t" + "Общество: {1}\t" + "Результаты: {2}, {3}", plr[i].lstname, plr[i].teamname, plr[i].rez1, plr[i].rez2);
            }
        }
    }

    public struct Student
    {
        public string lstname;
        public string teamname;
        public double rez1, rez2;
        public double srez;
        public Student(string lstname1, string teamname1, double rez11, double rez21)
        {
            lstname = lstname1;
            teamname = teamname1;
            rez1 = rez11;
            rez2 = rez21;
            srez = rez11+rez21;
        }
    }
}

namespace Number4
{
    public static class Program
    {
        /// Результаты соревнований по прыжкам в высоту определяются по
        /// лучшей из двух попыток. Вывести список участников в порядке занятых мест

        public static void Start()
        {
            Player[] plr = new Player[6];
            for (int i = 0; i < plr.Length; i++)
            {
                plr[i] = new Player($"F{i + 1}", i, i + 1);
            }

            for (int i = 0; i < plr.Length - 1; i++)
            {
                double lmax = plr[i].maxr;
                int pmax = i;
                for (int j = i + 1; j < plr.Length; j++)
                {
                    Player temp;
                    if (plr[j].maxr > plr[i].maxr)
                    {
                        temp = plr[i];
                        plr[i] = plr[j];
                        plr[j] = temp;
                    }
                }
            }

            for (int i = 0; i < plr.Length; i++)
            {
                Console.WriteLine("Фамилия {0}\t" + "Максимальный результат: {1}", plr[i].lstname, plr[i].maxr);
            }
        }
    }
    public struct Player
    {
        public string lstname;
        public double rez1, rez2;
        public double maxr;
        public Player(string lstname1, double rez11, double rez21)
        {
            lstname = lstname1;
            rez1 = rez11;
            rez2 = rez21;
            maxr = rez2;
            if (rez1 > rez2) maxr = rez1; 
        }
    }
}