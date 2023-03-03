using System;

namespace Level2
{
    internal class Program
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
        /// Студенты одной группы в сессию сдают четыре экзамена. Составить список студентов,
        /// средний балл которых по всем экзаменам не менее «4». Результаты вывести в виде
        /// таблицы с заголовком в порядке убывания среднего балла. 

        public static void Start()
        {
            Student[] students = new Student[10];
            for (int i = 0; i < students.Length; i++)
            {
                students[i] = new Student($"F{i + 1}", new double[4] { 5, 5, i + 1, i + 1 });
            }

            for (int i = 0; i < students.Length - 1; i++)
            {
                Student temp;
                for (int j = i + 1; j < students.Length; j++)
                {
                    if (students[j].srr > students[i].srr)
                    {
                        temp = students[i];
                        students[i] = students[j];
                        students[j] = temp;
                    }
                }
            }
            for (int i = 0; i < students.Length; i++)
            {
                if (students[i].srr >= 4)
                    Console.WriteLine("Фамилия студента: {0}\t" + "Средний балл: {1}", students[i].lstname, students[i].srr);
            }
        }
    }
    public struct Student
    {
        public string lstname;
        public double[] ocn;
        public double srr;
        public Student(string lstname1, double[] ocn1)
        {
            lstname = lstname1;
            ocn = ocn1;
            srr = 0;
            foreach (double i in ocn1) srr += i;
            srr /= ocn.Length;
        }
    }
}

namespace Number2
{
    public static class Program
    {
        /// Группа учащихся подготовительного отделения сдает выпускные экзамены по трем предметам (математика, физика, русский язык).
        /// 176 Учащийся, получивший «2», сразу отчисляется. Вывести список учащихся, успешно сдавших экзамены, в порядке убывания
        /// полученного ими среднего балла по результатам трех экзаменов
        public static void Start()
        { 
            Student[] stud = new Student[10]; 
            for (int i = 0; i < stud.Length; i++) 
            {
                stud[i] = new Student($"F{i+1}", new double[3] {5,i,3}); 
            }
            for (int i = 0; i < stud.Length - 1; i++) 
            { 
                Student temp; 
                for (int j = i + 1; j < stud.Length; j++) 
                { 
                    if (stud[j].srr > stud[i].srr) 
                    { 
                        temp = stud[i]; 
                        stud[i] = stud[j]; 
                        stud[j] = temp;
                    }
                }
            } 
            bool ch = true; 
            for (int i = 0; i < stud.Length; i++) 
            { 
                if (ch == true) for (int j = 0; j < stud[i].ocn.Length; j++) if (stud[i].ocn[j] <= 2) ch = false; 
                if (ch) Console.WriteLine("Фамилия студента: {0}\t" + "Средний балл: {1}", stud[i].lstname, stud[i].srr); 
            }
        }
    }
    public struct Student
    {
        public string lstname;
        public double[] ocn;
        public double srr;
        public Student(string lstname1, double[] ocn1)
        {
            lstname = lstname1;
            ocn = ocn1;
            srr = 0;
            for (int i = 0; i < ocn.Length; i++) srr += ocn[i];
            srr /= ocn.Length;
        }
    }
}
