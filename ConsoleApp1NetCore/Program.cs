using System;
using System.Data;
using System.IO;
using System.Text;
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
                NullValueHandling = NullValueHandling.Ignore,
                //MissingMemberHandling = MissingMemberHandling.Ignore
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
            Console.WriteLine("Testing DataSetConverter in NetCore2 with DBNull column ");
            DataSet dataSet = new DataSet("dataset");
            dataSet.Namespace = "NetFrameWork";
            DataTable dataTable = CreateDataTable("table1");
            dataSet.Tables.Add(dataTable);

            Console.WriteLine("****** XML dataset Serialized *******");
            StringBuilder sb = new StringBuilder();
            TextWriter tw = new StringWriter(sb);
            dataSet.WriteXml(tw);
            Console.WriteLine(sb);

            ITraceWriter traceWriter = new MemoryTraceWriter();
            var settings = new JsonSerializerSettings
            {
                TraceWriter = traceWriter,
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore,
                //MissingMemberHandling = MissingMemberHandling.Ignore
            };

            settings.Converters.Add(new DataSetConverter()); ;
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


            for (int i = 0; i < 4; i++)
            {
                DataRow newRow = dataTable.NewRow();
                if (i % 2 == 0)
                    newRow["id"] = DBNull.Value;
                else
                    newRow["id"] = i;
                newRow["item"] = "item " + i;
                dataTable.Rows.Add(newRow);

            }

            return dataTable;
        }
    }
}
