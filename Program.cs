using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
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


        }
    }
}