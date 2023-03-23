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
            string path = "readers.json";
            Scheme test = WorkWithScheme.ReadScheme(path);
            Console.WriteLine(JsonSerializer.Serialize(test));
        }
    }
}