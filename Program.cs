using Schema;

namespace DB4
{
    class Program
    {
        public static void Main()
        {
            Console.WriteLine("Введите название схемы (например readers.json):");
            var schemeName = Console.ReadLine();

            var pathScheme = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\" + "schemas" + "\\" + schemeName;

            Scheme scheme = WorkWithScheme.ReadScheme(pathScheme);

            Console.WriteLine("Введите название файла данных для схемы (например readersData.txt):");
            string dataName = Console.ReadLine();

            string pathData = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\" + "data" + "\\" + dataName;
            SchemeData readersData = new(scheme, pathData);

            readersData.PrintData();
        }
    }
}