using Schema;

namespace DB4
{
    class Program
    {
        public static void Main()
        {
            string pathScheme = "readers.json";
            Scheme readers = WorkWithScheme.ReadScheme(pathScheme);

            string pathData = "readersData.json";
            SchemeData readersData = new(readers);

            readersData.Rows = WorkWithScheme.GetData(readers, pathData);

            readersData.PrintData();
        }
    }
}