# Demo Application for json.net and DataTable/DataSet in NetCore 2.0 

This implementation is based on the source code of DataTableConverter and DataSetSonverter in Json.Net

It is created as a response to issue  [serializes a DataSet to JSON on dotnet core 2.0 #1409](https://github.com/JamesNK/Newtonsoft.Json/issues/1409)

Newtonsoft.Json version 10.0.3 dosn't contain support to DataTable/DataSet in dotnet 2.0 , so  serialization/deserialization can't use  DataTableConverter /DataSetConverter.
and you get inalid json string.

dotnet core 2.0  now support DataTableConverter /DataSetConverter ASIS.

I expect the next release of Newtonsoft.Json may support them with the final release of [ netcore 2.0](https://www.microsoft.com/net/download/core)

Until it's supported, you can use the library   name "DataTableCore2"

The project contain a demo console application with  a class library
 

 ## Known Issues
### Handling columns with DBNull
Columns in datatable with DBNull value raise an exception: System.PlatformNotSupportedException: Operation is not supported in this platform.

DBNull is supported in netstandard2 (netcore 2)
Review the source code  [public sealed class DBNull : ISerializable, IConvertible](https://source.dot.net/#System.Private.CoreLib/shared/System/DBNull.cs,7faae4cef0a3f251)

Json.net currently v10.0.3  handle DBNull internally with a compilation conditional flag `HAVE_ADO_NET` which is disabled  in netstandard1.3 version. 
Once  json.net is ported to netstandard2, DbNull may  be handled without firing exception.

If you serialized the dataset to xml  with table containing columns with DBNull value, `dataSet.WriteXml(textwriter) you get xml without error .


You can avoid this error by supplying settings to JsonConvert.SerializeObject to tell it how to handle null values:

          var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
           settings.Converters.Add(new DataSetConverter()); // using the accompanied library
           var json = JsonConvert.SerializeObject(dataSet, settings);


