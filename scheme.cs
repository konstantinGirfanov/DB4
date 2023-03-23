using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Data.Common;
using System.Net.Http.Headers;

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

            public Row(SchemeColumn column, string line)
            {
                string[] lines = line.Split(';');

                for(int i = 0; i < lines.Length; i++)
                {
                    Data.Add(column, lines[i]);
                }
            }
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
                bool isCorresponded = true;
                for(int i = 0; i < lineColumns.Length; i++)
                {
                    switch(scheme.Columns[i].Type)
                    {
                        case "int":
                            if (!int.TryParse(lineColumns[i], out int _))
                            {
                                DisplayErrorMessage(true, row, i);
                                isCorresponded = false;
                            }
                            break;
                        case "float":
                            if (!float.TryParse(lineColumns[i], out float _))
                            {
                                DisplayErrorMessage(true, row, i);
                                isCorresponded = false;
                            }
                            break;
                        case "double":
                            if (!double.TryParse(lineColumns[i], out double _))
                            {
                                DisplayErrorMessage(true, row, i);
                                isCorresponded = false;
                            }
                            break;
                        case "bool":
                            if (!bool.TryParse(lineColumns[i], out bool _))
                            {
                                DisplayErrorMessage(true, row, i);
                                isCorresponded = false;
                            }
                            break;
                        case "dateTime":
                            if (!DateTime.TryParse(lineColumns[i], out DateTime _))
                            {
                                DisplayErrorMessage(true, row, i);
                                isCorresponded = false;
                            }
                            break;
                    }
                }

                return isCorresponded;
            }
        }

        public static Dictionary<SchemeColumn, object> GetData(Scheme scheme, string path)
        {
            string[] data = File.ReadAllLines(path);
            Dictionary<SchemeColumn, object> result = new();

            for (int i = 1; i < data.Length; i++)
            {
                if (IsCorrespondsToScheme(scheme, data[i], i))
                {

                }
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