// See https://aka.ms/new-console-template for more information
using DotNetTraining.ConsoleApp;
using System.Data;
using System.Data.SqlClient;

Console.WriteLine("Hello, World!");

//AdoDotNetSample adoDotNetSample = new AdoDotNetSample();
//adoDotNetSample.RestoreDelete();

//AdoDotNetSampleTesting adoDotNetSampleTesting = new AdoDotNetSampleTesting();
//adoDotNetSampleTesting.GetById();

//DapperExample dapperExample = new DapperExample();

//dapperExample.Edit(8);

//EFCoreExample efCore = new EFCoreExample();
//efCore.Create("Testing EFCore", "EFCORE", "EFFs");

EFCoreExampleTesting efCore = new EFCoreExampleTesting();
efCore.Read();

Console.ReadKey();