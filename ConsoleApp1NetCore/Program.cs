using System;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters.Core2;
using Newtonsoft.Json.Serialization;

namespace ConsoleApp1NetCore
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World! DataTable in NetCore2 ");
            Main1();
            Main2();
        }

        static void Main1()
        {
            Console.WriteLine("Testing DataTableConverter in NetCore2 ");
            DataTable dataTable = CreateDataTable("table1");
            ITraceWriter traceWriter = new MemoryTraceWriter();
            var settings = new JsonSerializerSettings
            {
                TraceWriter = traceWriter,
                Formatting = Newtonsoft.Json.Formatting.Indented,
            };

            settings.Converters.Add(new DataTableConverter());
            var json = JsonConvert.SerializeObject(dataTable, settings);
            Console.WriteLine("****** Serialized json *******");
            Console.WriteLine(json);
            Console.WriteLine("****** json Trace *******");
            Console.WriteLine(traceWriter);

        }
        static void Main2()
        {
            Console.WriteLine("Testing DataSetConverter in NetCore2 ");
            DataSet dataSet = new DataSet("dataset");
            dataSet.Namespace = "NetFrameWork";
            DataTable dataTable = CreateDataTable("table1");
            dataSet.Tables.Add(dataTable);
            ITraceWriter traceWriter = new MemoryTraceWriter();
            var settings = new JsonSerializerSettings
            {
                TraceWriter = traceWriter,
                Formatting = Formatting.Indented,
            };

            settings.Converters.Add(new DataSetConverter());;
            var json = JsonConvert.SerializeObject(dataSet, settings);
            Console.WriteLine("****** Serialized json *******");
            Console.WriteLine(json);
            Console.WriteLine("****** json Trace *******");
            Console.WriteLine(traceWriter);

        }
        static DataTable CreateDataTable(string name)
        {
            DataTable dataTable = new DataTable(name);
            dataTable.Columns.Add("id", typeof(int));
            dataTable.Columns.Add("item", typeof(string));


            for (int i = 0; i < 2; i++)
            {
                DataRow newRow = dataTable.NewRow();
                newRow["id"] =i;
                newRow["item"] = "item " + i;
                dataTable.Rows.Add(newRow);

            }
           

            return dataTable;




        }
    }
}
