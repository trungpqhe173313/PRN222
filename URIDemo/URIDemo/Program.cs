﻿using System;
using static System.Console;

class Program
{
    static void Main(string[] args)
    {
        Uri info = new Uri("http://www.domain.com:80/info?id=123#fragment");
        Uri page = new Uri("http://www.domain.com/info/page.html");

        WriteLine($"Host: {info.Host}");
        WriteLine($"Port: {info.Port}");
        WriteLine($"PathAndQuery: {info.PathAndQuery}");
        WriteLine($"Query: {info.Query}");
        WriteLine($"Fragment: {info.Fragment}");
        WriteLine($"Default HTTP port: {page.Port}");
        WriteLine($"IsBaseOf: {info.IsBaseOf(page)}");

        Uri relative = info.MakeRelativeUri(page);
        WriteLine($"IsAbsoluteUri: {relative.IsAbsoluteUri}");
        WriteLine($"RelativeUri: {relative.ToString()}");

        ReadLine();
    }
}
