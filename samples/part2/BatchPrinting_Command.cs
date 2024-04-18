
using System;
using System.IO;
using System.Linq;

using Rhino;
using Rhino.Display;
using Rhino.FileIO;

string path = string.Empty;

// 1a. Get the path thru the command line
var success = Rhino.Input.RhinoGet.GetString("Please select the folder where your files live in", false, ref path);
if (success != Rhino.Commands.Result.Success)
{
    Console.WriteLine("There was an error");
    return;
}

// 1b. Get the path using ETO, a cross-platform UI library
// var dialog = new Eto.Forms.SelectFolderDialog();
// dialog.Title = "Please select the folder where your files live in";

// if (dialog.ShowDialog(null) != Eto.Forms.DialogResult.Ok)
// {
//     Console.WriteLine("There was an error");
//     return;
// }
//path = dialog.Directory;

Console.WriteLine($"Printing from {path}!!");

// 2. Getting the files from the provided path
var allFiles = Directory.GetFiles(path).ToList();
var files = allFiles.Where(f => f.EndsWith(".3dm")).ToList();

Console.WriteLine($"Number of found files: {allFiles.Count}!!");
Console.WriteLine($"Number of files to print: {files.Count}!!");

// 3. Getting the complete file names and the export names
var destinations = files.Select(x => Path.Combine(path, x)).ToList();
var exportNames = destinations.Select(x => x.Replace(".3dm", ".pdf")).ToList();

// 4. Printing all Rhino files
for (int i = 0; i < destinations.Count; i++)
{

    // 4.1 Setting the modified flag to false to avoid the command question about saving the file
    RhinoDoc.ActiveDoc.Modified = false;

    // 4.2 Creating and running the opening script
    string script = string.Format("_-Open \"{0}\"", destinations[i]);
    RhinoApp.RunScript(script, false);

    var currentDoc = RhinoDoc.ActiveDoc;

    // 4.3 Creating a blank pdf file to store all layouts from the Rhino file
    var pdf = FilePdf.Create();
    var dpi = 600;

    // 4.4 Iterating all layouts and configuring settings to export to pdf
    var pages = currentDoc.Views.GetPageViews();
    foreach (RhinoPageView page in pages)
    {
        var capture = new ViewCaptureSettings(page, dpi);
        capture.OutputColor = 0;
        capture.TextDotPointSize = 8.0;
        capture.DefaultPrintWidthMillimeters = 0.2;
        pdf.AddPage(capture);
    }

    // 4.5 Saving our new pdf file
    pdf.Write(exportNames[i]);
}

