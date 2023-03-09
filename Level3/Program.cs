using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Threading;
using Number1;
using System.Diagnostics;

namespace Level3
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Number6.Program.Start();
        }
    }
}

namespace Number1
{
    public static class Program
    {
        /// Результаты сессии содержат оценки 5 экзаменов по каждой группе. Определить средний балл для трех групп студентов одного
        /// потока и выдать список групп в порядке убывания среднего балла. Результаты вывести в виде таблицы с заголовком.

        public static void Start()
        {
            Group[] groups = new Group[3];
            int Ind = 0;
            for (int i = 0; i < groups.Length; i++)
            {
                groups[i] = Group.GroupGenerate(i, 15 + i * 5, (i + 3));
                groups[i].Students = SupportMethods.StudentsListSort(groups[i].Students);
                Ind = SupportMethods.SearchForTheFirstUntested(groups[i].Students);
                groups[i].DeleteBelowIndex(Ind);
                groups[i].RefrashAverMark();
            }
            
            groups = SupportMethods.GroupsSort(groups);
            
            Console.WriteLine("Group\tAverage mark");
            foreach (var group in groups)
            {
                Console.WriteLine($"{group.GroupName}\t{group.AverMark}");
            }
            
            Console.WriteLine(" ");
            foreach (var group in groups)
            {
                SupportMethods.PrintStudents(group);
            }
        }
    }
    public struct Student
    {
        public string Lastname;
        public int[] Marks;
        public double AverMark;
        public Student(string lastname, int[] marks)
        {
            Lastname = lastname;
            Marks = marks; 
            double sum = 0;
            foreach (int mark in Marks) 
                if (mark >= 3) sum += mark; else {sum = 0; break;} 
            AverMark = sum / Marks.Length;
        }
    }

    public struct Group
    {
        public string GroupName;
        public List<Student> Students;
        public double AverMark;

        public Group(string groupname, List<Student> students)
        {
            GroupName = groupname;
            Students = students;
            double sum = 0;
            foreach (var student in Students) sum += student.AverMark;
            AverMark = sum / Students.Count;
        }

        public void RefrashAverMark()
        {
            double sum = 0;
            foreach (var student in Students) sum += student.AverMark;
            AverMark = sum / Students.Count;
        }

        public static Group GroupGenerate(int GroupNumber, int CountOfStudent, int rang)
        {
            List<Student> students = new List<Student>();
            int stmark = 0;
            for (int i = 0; i < CountOfStudent; i++)
            {
                stmark = (i % 3) + 2;
                students.Add(new Student($"F{i + 1}", new int[5] { rang, 5, stmark, stmark, rang }));
            }
            return new Group($"Group{GroupNumber+1}", students);
        }

        public void DeleteBelowIndex(int Ind)
        {
            for (int j = Ind; j < Students.Count; j++)
            {
                Students.RemoveAt(j);
                j--;
            }
        }
    }

    public static class SupportMethods
    {
        public static Group[] GroupsSort(Group[] groups)
        {
            for (int i = 0; i < groups.Length - 1; i++)
            {
                Group temp;
                for (int j = i + 1; j < groups.Length; j++)
                {
                    if (groups[j].AverMark > groups[i].AverMark)
                    {
                        temp = groups[i];
                        groups[i] = groups[j];
                        groups[j] = temp;
                    }
                }
            }

            return groups;
        }

        public static List<Student> StudentsListSort(List<Student> list)
        {
            for (int i = 0; i < list.Count - 1; i++)
            { 
                Student temp; 
                for (int j = i + 1; j < list.Count; j++) 
                { 
                    if (list[j].AverMark > list[i].AverMark) 
                    { 
                        temp = list[i]; 
                        list[i] = list[j]; 
                        list[j] = temp;
                    }
                }
            }
            return list;
        }

        public static void PrintStudents(Group groups)
        {
            Console.WriteLine(groups.GroupName);
            foreach (var student in groups.Students)
            {
                Console.WriteLine($"{student.Lastname}\tAverage mark: {student.AverMark}");
            }
            Console.WriteLine();
        }

        public static int SearchForTheFirstUntested(List<Student> list)
        {
            int low = 0;
            int high = list.Count - 1;
            int Ind = 0;
            while (list[Ind].AverMark!=0)
            {
                Ind = (low + high) / 2;
                if (0 > list[Ind].AverMark) high = Ind - 1;
                else if (0 < list[Ind].AverMark) low = Ind + 1;
            } 
            while (list[Ind].AverMark == 0) Ind--;
            return (Ind+1);
        }
        
    }
}

namespace Number4
{
 public static class Program
    {
        /// Лыжные гонки проводятся отдельно для двух групп участников. Результаты соревнований заданы в виде фамилий участников и
        /// их результатов в каждой группе. Расположить результаты соревнований в каждой группе в порядке занятых мест.
        /// Объединить результаты обеих групп с сохранением упорядоченности и вывести в виде таблицы с заголовком.

        public static void Start()
        {
            List<Participant> TotalResult = new List<Participant>();
            List<Participant>[] groups = new List<Participant>[2];
            for (int i = 0; i < groups.Length; i++)
            {
                groups[i] = SupportMethods.GroupGenerate(i, 15 + i * 5, i+1);
                groups[i] = SupportMethods.ParticipantListSort(groups[i]);
                SupportMethods.PrintParticipant(groups[i]);
            }
            TotalResult = new List<Participant>(SupportMethods.CombiningTheResult(groups[0], groups[1]));
            SupportMethods.PrintParticipantTable(TotalResult);
        }
    }

    public struct Participant
    {
        public string Lastname;
        public string GroupName;
        public int Result;
        public Participant(string lastname, string groupname, int result)
        {
            Lastname = lastname;
            Result = result;
            GroupName = groupname;
        }
    }

    public static class SupportMethods
    {
        public static List<Participant> ParticipantListSort(List<Participant> list)
        {
            for (int i = 0; i < list.Count - 1; i++)
            { 
                Participant temp; 
                for (int j = i + 1; j < list.Count; j++) 
                { 
                    if (list[j].Result > list[i].Result) 
                    { 
                        temp = list[i]; 
                        list[i] = list[j]; 
                        list[j] = temp;
                    }
                }
            }
            return list;
        }
        public static List<Participant> CombiningTheResult(List<Participant> list1, List<Participant> list2)
        {
            List<Participant> fin = new List<Participant>();
            while (list1.Count > 0 & list2.Count > 0)
            {
                if (list1.Count == 0) fin.Add(list2[0]);
                else if (list2.Count == 0)
                    fin.Add(list1[0]);
                else if (list1[0].Result < list2[0].Result)
                {
                    fin.Add(list2[0]);
                    list2.RemoveAt(0);
                }
                else if (list1[0].Result > list2[0].Result)
                {
                    fin.Add(list1[0]);
                    list1.RemoveAt(0);
                }
                else
                {
                    fin.Add(list1[0]);
                    list1.RemoveAt(0);
                    fin.Add(list2[0]);
                    list2.RemoveAt(0);
                }
            }
            return fin;
        }
        public static void PrintParticipant(List<Participant> group)
        {
            Console.WriteLine(group[0].GroupName);
            foreach (var participant in group)
            {
                Console.WriteLine($"{participant.Lastname}\tResult: {participant.Result}");
            }
            Console.WriteLine();
        }
        public static void PrintParticipantTable(List<Participant> group)
        {
            foreach (var participant in group)
            {
                Console.WriteLine($"{participant.GroupName}\t{participant.Lastname}\tResult: {participant.Result}");
            }
            Console.WriteLine();
        }
        public static List<Participant> GroupGenerate(int GroupNumber, int CountOfParticipants, int rang)
        {
            List<Participant> participants = new List<Participant>();
            int stmark = 0;
            for (int i = 0; i < CountOfParticipants; i++)
            {
                stmark = (i % 3) + 2;
                participants.Add(new Participant($"F{i + 1}",$"Group{GroupNumber+1}",  ((i+1)*2)*((i%3)+1)));
            }
            return participants;
        }
    }
}

namespace Number6
{
    internal static class Program
    {
        /// Японская радиокомпания провела опрос радиослушателей по трем вопросам:
        /// а) какое животное вы связываете с Японией и японцами?
        /// б) какая черта характера присуща японцам больше всего?
        /// в) какой неодушевленный предмет или понятие вы связываете с Японией?
        /// Большинство опрошенных прислали ответы на все или часть вопросов. Составить программу получения первых пяти наиболее часто
        /// встречающихся ответов по каждому вопросу и доли (%) каждого такого ответа. Предусмотреть необходимость сжатия столбца ответов в
        /// случае отсутствия ответов на некоторые вопросы.

        public static void Start()
        {
            var doc = new StreamReader("Answers_N6.txt");
            var QuestionCounter = 1;
            while (!doc.EndOfStream)
            {
                var AllAnswers = new List<Answer>();
                var FinAnswers = new List<Answer>();
                var CurrentAnswers = doc.ReadLine().Split(';');
                AllAnswers = SupportMethods.FinListMaker(CurrentAnswers); 
                FinAnswers = SupportMethods.FinAnswersListMaker(AllAnswers);
                Console.WriteLine($"Question: {QuestionCounter}");
                SupportMethods.PrintParticipant(FinAnswers, CurrentAnswers.Length);
                QuestionCounter++;
                Console.WriteLine();
            }
        }
    }

    public struct Answer
    {
        public string Anss;
        public int Count;
        public Answer(string ans, int count = 1)
        {
            Anss = ans;
            Count = count;
        }
    }

    public static class SupportMethods
    {
        public static void PrintParticipant(List<Answer> answers, int AnsCount)
        {
            foreach (Answer answer in answers) Console.WriteLine($"Answer: {answer.Anss} - {answer.Count} \t Result: {(answer.Count*100)/AnsCount}%");
        }

        public static List<Answer> FinListMaker(string[] CurrentAnswers)
        {
            var AllAnswers = new List<Answer>();
            foreach (var answer in CurrentAnswers)
            {
                int index = AllAnswers.Select(x => x.Anss).ToList().IndexOf(answer);
                if (answer == "") continue;
                else if (index >= 0) AllAnswers[index] = new Answer(AllAnswers[index].Anss, AllAnswers[index].Count + 1);
                else AllAnswers.Add(new Answer(answer));
            }
            /*foreach (var answer in CurrentAnswers)
                {
                    if (answer == "") continue;
                    else if ((from a in AllAnswers select a.Anss).Contains(answer))
                    {
                        for (int AnswerInd = 0; AnswerInd < AllAnswers.Count; AnswerInd++)
                        {
                            if (AllAnswers[AnswerInd].Anss == answer)
                            {
                                AllAnswers[AnswerInd] = new Answer(AllAnswers[AnswerInd].Anss,
                                    AllAnswers[AnswerInd].Count + 1);
                                break;
                            }
                        }
                    }
                    else
                    {
                        AllAnswers.Add(new Answer(answer));
                    }
                }*/
            return AllAnswers;
        }

        public static List<Answer> FinAnswersListMaker(List<Answer> AllAnswers )
        {
            List<Answer> FinAnswers = new List<Answer>();
            while (FinAnswers.Count < 5)
            {
                int Ch = AllAnswers[0].Count;
                int ChInd = 0;
                for (int i = 0; i < AllAnswers.Count; i++)
                {
                    if (AllAnswers[i].Count >= Ch)
                    {
                        Ch = AllAnswers[i].Count;
                        ChInd = i;
                        FinAnswers.Add(AllAnswers[ChInd]);
                        AllAnswers.RemoveAt(ChInd);
                    }
                }
            }
            return FinAnswers;
        }
    }
}
