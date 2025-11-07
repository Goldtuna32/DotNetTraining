// See https://aka.ms/new-console-template for more information
using DotNetTraining.ConsoleApp;
using System.Data;
using System.Data.SqlClient;

Console.WriteLine("Hello, World!");

AdoDotNetSample adoDotNetSample = new AdoDotNetSample();
adoDotNetSample.RestoreDelete();

Console.ReadKey();