// #! csharp
using System;

#if DEBUG
int a = 12;
#else
int a = 500;
#endif

void DoStuff()
{
    Console.WriteLine("Doing stuff");
    Console.WriteLine(a.ToString());
}

DoStuff();
