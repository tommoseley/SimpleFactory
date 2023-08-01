using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.IO;
using SimpleFactory.Regions;
using SimpleFactory;

public static class SimpleFactrory
{
    public static void Main(String[] Args)
    {
        Runner runner = new();
        runner.Run();
    }
}
