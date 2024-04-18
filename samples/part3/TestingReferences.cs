// #! csharp
// r "../MyAmazingLibrary/bin/Debug/net7.0/MyAmazingLibrary.dll"
using MyAmazingLibrary;
using System;

var amazingWall = new MyAmazingLibrary.AmazingWall("Pedro Cortes", 10, 5, 0.2);
amazingWall.PrintInfo();
amazingWall.CreateWall();
Console.WriteLine($"Created wall with id: {amazingWall.Id}"); 
Console.WriteLine($"Created wall with prop: {amazingWall.Property}"); 
