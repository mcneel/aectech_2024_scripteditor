using System;

using Rhino;
using Rhino.Commands;

namespace RhinoCodePlatform.Rhino3D.Projects.Plugin
{
  [CommandStyle(Rhino.Commands.Style.ScriptRunner)]
  public class ProjectCommand_BatchPrinting_Command : Command
  {
    public Guid CommandId { get; } = new Guid("9bc20497-efbf-434a-957d-255e65fee9eb");

    public ProjectCommand_BatchPrinting_Command() { Instance = this; }

    public static ProjectCommand_BatchPrinting_Command Instance { get; private set; }

    public override string EnglishName => "BatchPrinting_Command";

    protected override Rhino.Commands.Result RunCommand(RhinoDoc doc, RunMode mode)
    {
      // NOTE:
      // Initialize() attempts to loads the core rhinocode plugin
      // and prepare the scripting platform. This call can not be in any static
      // ctors of Command or Plugin classes since plugins can not be loaded while
      // rhino is loading this plugin. The call has an initialized check and is
      // very fast after the first run.
      ProjectPlugin.Initialize();

      return ProjectPlugin.RunCode(this, CommandId, doc, mode);
    }
  }
}
