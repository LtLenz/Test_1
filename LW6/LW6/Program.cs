using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Создать Сессию, содержащую зачеты и экзамены.
//Найти
//все экзамены по заданному предмету, 
//подсчитать общее количество испытаний в сессии
//количество тестов с заданным числом вопросов.

namespace LW6
{
    public abstract class Experience
    {
        public abstract double FinalExam();
        public abstract double Question();
    }
    public interface IExam
    {
        void Show();
        void Input();
    }

    public class Exams : Experience, IExam
    {
        private double _otvet;
        public Exams(string v, string v1, string v2)
        {
            Input();
        }

        public override double FinalExam()
        {
            double otvet = _otvet;
            Console.WriteLine("Вариант билета на экзамене: {0}", _otvet);
            return _otvet;
        }

        public override string ToString()
        {
            Show();
            return base.ToString();
        }
        public void Input()
        {
            Console.Write("Введите вариант билета на выпускном экзамене: ");
            _otvet = Convert.ToInt32(Console.ReadLine());
        }

        public void Show()
        {
            Console.WriteLine("\nИспытание\nВариант билета на экзамене: {0} \n", _otvet);
        }

        public override double Question()
        {
            return 0;
        }
    }

    public class Tests : Experience, IExam
    {
        private double _var;

        public Tests(string _v, string _v2, string _v3)
        {
            Input();
        }

        public override double Question()
        {
            double var = _var;
            Console.WriteLine("Вариант вопроса в тесте: {0}", _var);
            return _var;
        }

        public override string ToString()
        {
            Show();
            return base.ToString();
        }
        public void Input()
        {
            Console.Write("Введите вариант вопроса в тесте: ");
            _var = Convert.ToInt32(Console.ReadLine());
        }

        public void Show()
        {
            Console.WriteLine("Испытание\nВариант вопроса теста: {0}\n", _var);
        }

        public override double FinalExam()
        {
            return 0;
        }
    }

    sealed public class Printer
    {
        public Printer() { }
        public string Print(IExam obj)
        {
            return obj.ToString();
        }

    }

    public class Session<T>
    {
        private List<T> container;
        public Session()
        {
            container = new List<T>();
        }
        public T this[int number]
        {
            get { return container[number]; }
            set { container[number] = value; }
        }
        public void Push(T element)
        {
            container.Add(element);
        }
        public void Pop()
        {
            container.RemoveAt(container.Count - 1);
        }
        public int Size
        {
            get { return container.Count; }
        }
        public void Output()
        {
            Console.WriteLine("\nContainer: ");
            for (int i = 0; i < container.Count; i++)
            {
                Console.Write(container[i]);
            }
            Console.WriteLine();
        }
    }
    public class SessionController
    {
        public double TotalSquare = 0;
        public int TotalElements = 0;
        private Session<Experience> UIF;
        public SessionController(Session<Experience> ui)
        {
            this.UIF = ui;
        }
        public double GetTotalSquare()
        {
            for (int i = 0; i < UIF.Size; i++)
            {
                TotalSquare += UIF[i].FinalExam();
            }
            return TotalSquare;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Session<Experience> exp = new Session<Experience>();
            exp.Push(new Exams("варианты на экзамене", "первый", "второй"));
            exp.Push(new Tests("варианты на экзамене", "первый", "второй"));
            IExam exam = new Exams("варианты на экзамене", "первый", "второй");
            IExam que = new Tests("варианты на экзамене", "первый", "второй");

            Printer printer = new Printer();
            Console.WriteLine("\nIS-operator: {0}, {1}", exam is Exams, que is Tests);
            // обычное приведение, но с использование оператора as(выбрасывает не исключение, а null)
            IExam exam1 = exam as Exams;
            IExam que1 = que as Tests;
            printer.Print(exam);
            printer.Print(que);

            Console.ReadKey();
        }
    }
}
