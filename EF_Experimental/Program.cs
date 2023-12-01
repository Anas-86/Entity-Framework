using EF_Experimental;
using System;

Northwind db = new();
Console.WriteLine($"Provider: {db.Database.ProviderName}");