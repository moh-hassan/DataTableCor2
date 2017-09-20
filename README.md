# Demo Application for json.net and DataTable/DataSet in NetCore 2.0 

This implementation is based on the source code of DataTableConverter and DataSetSonverter in Json.Net

It is created as a response to issue  [serializes a DataSet to JSON on dotnet core 2.0 #1409](https://github.com/JamesNK/Newtonsoft.Json/issues/1409)

Newtonsoft.Json version 10.0.3 dosn't contain support to DataTable/DataSet in dotnet 2.0 , so  serialization/deserialization can't use  DataTableConverter /DataSetConverter.
and you get inalid json string.

dotnet core 2.0  now support DataTableConverter /DataSetConverter ASIS.

I expect the next release of Newtonsoft.Json may support them with the final release of [ netcore 2.0](https://www.microsoft.com/net/download/core)

Until it's supported, you can use the library   name "DataTableCore2"

The project contain a demo console application with  a class library
 
