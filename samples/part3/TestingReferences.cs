// #! csharp
// r "C:\Users\pedro.cortes\Desktop\McNeel2024\03_ScriptEditorWS\aectech_2024_scripteditor\samples\part2\MyAmazingLibrary.dll"

using MyAmazingLibrary;
using System;

var amazingWall = new MyAmazingLibrary.AmazingWall("Pedro Cortes", 10, 5, 0.2);
amazingWall.PrintInfo();
amazingWall.CreateWall();
//Console.WriteLine($"Created wall with id: {amazingWall.Id}"); 
