using System.Text.Json;
using System.Text.Json.Serialization;

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
        public List<Row> Rows { get; set; }
        private Scheme Scheme { get; init; }

        public SchemeData(Scheme scheme, string path)
        {
            Scheme = scheme;
            Rows = GetData(Scheme, path);
        }

        public static List<Row> GetData(Scheme scheme, string path)
        {
            string[] data = File.ReadAllLines(path);
            List<Row> rows = new();

            for (int i = 1; i < data.Length; i++)
            {
                if (WorkWithScheme.IsCorrespondsToScheme(scheme, data[i], i))
                {
                    rows.Add(new Row(scheme, data[i]));
                }
            }

            return rows;
        }

        public void PrintData()
        {
            Console.WriteLine($"Имя базы данных: { Scheme.Name }.");
            Console.WriteLine("Названия столбцов:");
            for (int i = 0; i < Scheme.Columns.Length; i++)
            {
                Console.Write(Scheme.Columns[i].Name + "  ");
            }
            Console.WriteLine("");
            foreach(Row row in Rows)
            {
                row.PrintRow();
            }
        }
    }

    class Row
    {
        private Dictionary<SchemeColumn, object> Data { get; set; } = new Dictionary<SchemeColumn, object>();

        public Row(Scheme scheme, string line)
        {
            string[] columnValues = line.Split(';');

            for (int i = 0; i < columnValues.Length; i++)
            {
                Data.Add(scheme.Columns[i], columnValues[i]);
            }
        }

        public void PrintRow()
        {
            foreach(KeyValuePair<SchemeColumn, object> column in Data)
            {
                Console.Write(column.Value + "  ");
            }

            Console.WriteLine();
        }
    }

    static class WorkWithScheme
    {
        public static Scheme ReadScheme(string path)
        {
            return JsonSerializer.Deserialize<Scheme>(File.ReadAllText(path));
        }

        public static bool IsCorrespondsToScheme(Scheme scheme, string line, int row)
        {
            string[] lineColumns = line.Split(';');

            if (scheme.Columns.Length != lineColumns.Length)
            {
                DisplayErrorMessage(false, row + 1, 0);
                return false;
            }
            else
            {
                bool isCorresponded = true;
                for (int i = 0; i < lineColumns.Length; i++)
                {
                    switch (scheme.Columns[i].Type)
                    {
                        case "int":
                            if (!int.TryParse(lineColumns[i], out int _))
                            {
                                DisplayErrorMessage(true, row + 1, i);
                                isCorresponded = false;
                            }
                            break;
                        case "float":
                            if (!float.TryParse(lineColumns[i], out float _))
                            {
                                DisplayErrorMessage(true, row + 1, i);
                                isCorresponded = false;
                            }
                            break;
                        case "double":
                            if (!double.TryParse(lineColumns[i], out double _))
                            {
                                DisplayErrorMessage(true, row + 1, i);
                                isCorresponded = false;
                            }
                            break;
                        case "bool":
                            if (!bool.TryParse(lineColumns[i], out bool _))
                            {
                                DisplayErrorMessage(true, row + 1, i);
                                isCorresponded = false;
                            }
                            break;
                        case "dateTime":
                            if (!DateTime.TryParse(lineColumns[i], out DateTime _))
                            {
                                DisplayErrorMessage(true, row + 1, i);
                                isCorresponded = false;
                            }
                            break;
                    }
                }

                return isCorresponded;
            }
        }

        public static void DisplayErrorMessage(bool isCorrectColumnCount, int row, int column)
        {
            if (isCorrectColumnCount)
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