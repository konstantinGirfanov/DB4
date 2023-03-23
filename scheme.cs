using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Data.Common;

namespace Schema
{
    class Scheme
    {
        [JsonPropertyName("name")]
        public string Name { get; init; }

        [JsonPropertyName("columns")]
        public SchemeColumn[] Columns { get; init; }

        public Scheme(string name, SchemeColumn[] columns)
        {
            Name = name;
            Columns = columns;
        }
    }

    class SchemeColumn
    {
        [JsonPropertyName("name")]
        public string Name { get; init; }

        [JsonPropertyName("type")]
        public string Type { get; init; }

        [JsonPropertyName("isPrimary")]
        public bool IsPrimary { get; init; }

        public SchemeColumn(string name, string type, bool isPrimary)
        {
            Name = name;
            Type = type;
            IsPrimary = isPrimary;
        }
    }

    class SchemeData
    {
        public List<Row> Rows { get; init; } = new List<Row>();
        private Scheme Scheme { get; init; }

        public class Row
        {
            public Dictionary<SchemeColumn, object> Data { get; set; }
        }

        public void AddRow(Row row)
        {
            Rows.Add(row);
        }
    }

    class WorkWithScheme
    {
        public static Scheme ReadScheme(string path)
        {
            return JsonSerializer.Deserialize<Scheme>(File.ReadAllText(path));
        }

        public static bool IsCorrespondsToScheme(Scheme scheme, string line, int row)
        {
            string[] lineColumns = line.Split(';');

            if(scheme.Columns.Length != lineColumns.Length)
            {
                DisplayErrorMessage(false, row, 0);
                return false;
            }
            else
            {
                return true;
            }
        }

        public static void DisplayErrorMessage(bool isCorrectColumnCount, int row, int column)
        {
            if(isCorrectColumnCount)
            {
                Console.WriteLine($"Данные не совпадают в {row} строке {column} столбце.");
            }
            else
            {
                Console.WriteLine($"Количество столбцов не совпадает в {row} строке.");
            }
        }
    }
}