using Schema;

namespace DB4
{
    class Program
    {
        public static void Main()
        {
            Console.WriteLine("Введите название схемы (например readers.json):");
            var schemeName = Console.ReadLine();

            var pathScheme = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\" + schemeName;
            Scheme scheme = WorkWithScheme.ReadScheme(pathScheme);

            Console.WriteLine("Введите название файла данных для схемы (например readersData.txt):");
            string dataName = Console.ReadLine();

            string pathData = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\" + dataName;
            SchemeData readersData = new(scheme);
            readersData.Rows = WorkWithScheme.GetData(scheme, pathData);

            readersData.PrintData();
        }
    }
}