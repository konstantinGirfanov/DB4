using Schema;

namespace DB4
{
    class Program
    {
        public static void Main()
        {
            Console.WriteLine("Введите название схемы (например readers.json):");
            string schemeName = Console.ReadLine();
            var pathScheme = WorkWithFiles.GetSchemePath(schemeName);
            Scheme scheme = WorkWithScheme.ReadScheme(pathScheme);

            string pathData = WorkWithFiles.GetDataPath(schemeName);
            SchemeData readersData = new(scheme, pathData);

            readersData.PrintData();
        }
    }
}