using System;
using SD = System.Drawing;
using System.Reflection;

using Rhino;
using Rhino.Geometry;
using Grasshopper.Kernel;

using Rhino.Runtime.Code;
using Rhino.Runtime.Code.Platform;

namespace RhinoCodePlatform.Rhino3D.Projects.Plugin.GH
{
  public sealed class AssemblyInfo : GH_AssemblyInfo
  {
    public override Guid Id { get; } = new Guid("6da0feaa-7890-4dbc-83bf-5e88b064ea2c");

    public override string AssemblyName { get; } = "AECtech plugin.GH";
    public override string AssemblyVersion { get; } = "0.2.20654.8874";
    public override string AssemblyDescription { get; } = "This is the first project I publish in AECtech workship";
    public override string AuthorName { get; } = "Pedro Cortes";
    public override string AuthorContact { get; } = "pedro@mcneel.com";
    public override GH_LibraryLicense AssemblyLicense { get; } = GH_LibraryLicense.unset;
    public override SD.Bitmap AssemblyIcon { get; } = ProjectComponentPlugin.PluginIcon;
  }

  public class ProjectComponentPlugin : GH_AssemblyPriority
  {
    public static SD.Bitmap PluginIcon { get; }
    public static SD.Bitmap PluginCategoryIcon { get; }

    static readonly Guid s_rhinocode = new Guid("c9cba87a-23ce-4f15-a918-97645c05cde7");
    static readonly PlatformSpec s_rhino = new PlatformSpec("mcneel.rhino3d.rhino");
    static readonly IProjectServer s_projectServer = default;
    static readonly IProject s_project = default;

    static readonly Guid s_projectId = new Guid("6da0feaa-7890-4dbc-83bf-5e88b064ea2c");
    static readonly string s_projectData = "ewogICJpZCI6ICI2ZGEwZmVhYS03ODkwLTRkYmMtODNiZi01ZTg4YjA2NGVhMmMiLAogICJpZGVudGl0eSI6IHsKICAgICJuYW1lIjogIkFFQ3RlY2ggcGx1Z2luIiwKICAgICJ2ZXJzaW9uIjogIjAuMiIsCiAgICAicHVibGlzaGVyIjogewogICAgICAiZW1haWwiOiAicGVkcm9AbWNuZWVsLmNvbSIsCiAgICAgICJuYW1lIjogIlBlZHJvIENvcnRlcyIsCiAgICAgICJjb21wYW55IjogIk1jTmVlbCBFdXJvcGUiCiAgICB9LAogICAgImRlc2NyaXB0aW9uIjogIlRoaXMgaXMgdGhlIGZpcnN0IHByb2plY3QgSSBwdWJsaXNoIGluIEFFQ3RlY2ggd29ya3NoaXAiLAogICAgImNvcHlyaWdodCI6ICJDb3B5cmlnaHQgXHUwMEE5IDIwMjQgUGVkcm8gQ29ydGVzIiwKICAgICJsaWNlbnNlIjogIk1JVCIsCiAgICAiaW1hZ2UiOiB7CiAgICAgICJsaWdodCI6IHsKICAgICAgICAidHlwZSI6ICJzdmciLAogICAgICAgICJkYXRhIjogIlBITjJaeUI0Yld4dWN6MGlhSFIwY0RvdkwzZDNkeTUzTXk1dmNtY3ZNakF3TUM5emRtY2lJSGRwWkhSb1BTSXhaVzBpSUdobGFXZG9kRDBpTVdWdElpQjJhV1YzUW05NFBTSXdJREFnTWpRZ01qUWlQanh3WVhSb0lHWnBiR3c5SW01dmJtVWlJSE4wY205clpUMGlJekF3Wm1Zd01DSWdjM1J5YjJ0bExXeHBibVZqWVhBOUluSnZkVzVrSWlCemRISnZhMlV0YkdsdVpXcHZhVzQ5SW5KdmRXNWtJaUJ6ZEhKdmEyVXRkMmxrZEdnOUlqSWlJR1E5SW0weE1DQXlNR3cwTFRFMmJUUWdOR3cwSURSc0xUUWdORTAySURFMmJDMDBMVFJzTkMwMElpOFx1MDAyQlBDOXpkbWNcdTAwMkIiCiAgICAgIH0sCiAgICAgICJkYXJrIjogewogICAgICAgICJ0eXBlIjogInN2ZyIsCiAgICAgICAgImRhdGEiOiAiUEhOMlp5QjRiV3h1Y3owaWFIUjBjRG92TDNkM2R5NTNNeTV2Y21jdk1qQXdNQzl6ZG1jaUlIZHBaSFJvUFNJeFpXMGlJR2hsYVdkb2REMGlNV1Z0SWlCMmFXVjNRbTk0UFNJd0lEQWdNalFnTWpRaVBqeHdZWFJvSUdacGJHdzlJbTV2Ym1VaUlITjBjbTlyWlQwaUl6QXdabVl3TUNJZ2MzUnliMnRsTFd4cGJtVmpZWEE5SW5KdmRXNWtJaUJ6ZEhKdmEyVXRiR2x1WldwdmFXNDlJbkp2ZFc1a0lpQnpkSEp2YTJVdGQybGtkR2c5SWpJaUlHUTlJbTB4TUNBeU1HdzBMVEUyYlRRZ05HdzBJRFJzTFRRZ05FMDJJREUyYkMwMExUUnNOQzAwSWk4XHUwMDJCUEM5emRtY1x1MDAyQiIKICAgICAgfQogICAgfQogIH0sCiAgInNldHRpbmdzIjogewogICAgImJ1aWxkUGF0aCI6ICJmaWxlOi8vL1VzZXJzL2x1aXMuZnJhZ3VhZGEvZGV2L2FlY3RlY2hfMjAyNF9zY3JpcHRlZGl0b3Ivc2FtcGxlcy9wYXJ0My9wcm9qZWN0L2J1aWxkL3JoOCIsCiAgICAiYnVpbGRUYXJnZXQiOiB7CiAgICAgICJhcHBOYW1lIjogIlJoaW5vM0QiLAogICAgICAiYXBwVmVyc2lvbiI6IHsKICAgICAgICAibWFqb3IiOiA4CiAgICAgIH0sCiAgICAgICJ0aXRsZSI6ICJSaGlubzNEICg4LiopIiwKICAgICAgInNsdWciOiAicmg4IgogICAgfSwKICAgICJwdWJsaXNoVGFyZ2V0IjogewogICAgICAidGl0bGUiOiAiTWNOZWVsIFlhayBTZXJ2ZXIiCiAgICB9CiAgfSwKICAiY29kZXMiOiBbXQp9";
    static readonly string _iconData = "ewogICJsaWdodCI6IHsKICAgICJ0eXBlIjogInN2ZyIsCiAgICAiZGF0YSI6ICJQSE4yWnlCNGJXeHVjejBpYUhSMGNEb3ZMM2QzZHk1M015NXZjbWN2TWpBd01DOXpkbWNpSUhkcFpIUm9QU0l4WlcwaUlHaGxhV2RvZEQwaU1XVnRJaUIyYVdWM1FtOTRQU0l3SURBZ01qUWdNalFpUGp4d1lYUm9JR1pwYkd3OUltNXZibVVpSUhOMGNtOXJaVDBpSXpBd1ptWXdNQ0lnYzNSeWIydGxMV3hwYm1WallYQTlJbkp2ZFc1a0lpQnpkSEp2YTJVdGJHbHVaV3B2YVc0OUluSnZkVzVrSWlCemRISnZhMlV0ZDJsa2RHZzlJaklpSUdROUltMHhNQ0F5TUd3MExURTJiVFFnTkd3MElEUnNMVFFnTkUwMklERTJiQzAwTFRSc05DMDBJaThcdTAwMkJQQzl6ZG1jXHUwMDJCIgogIH0sCiAgImRhcmsiOiB7CiAgICAidHlwZSI6ICJzdmciLAogICAgImRhdGEiOiAiUEhOMlp5QjRiV3h1Y3owaWFIUjBjRG92TDNkM2R5NTNNeTV2Y21jdk1qQXdNQzl6ZG1jaUlIZHBaSFJvUFNJeFpXMGlJR2hsYVdkb2REMGlNV1Z0SWlCMmFXVjNRbTk0UFNJd0lEQWdNalFnTWpRaVBqeHdZWFJvSUdacGJHdzlJbTV2Ym1VaUlITjBjbTlyWlQwaUl6QXdabVl3TUNJZ2MzUnliMnRsTFd4cGJtVmpZWEE5SW5KdmRXNWtJaUJ6ZEhKdmEyVXRiR2x1WldwdmFXNDlJbkp2ZFc1a0lpQnpkSEp2YTJVdGQybGtkR2c5SWpJaUlHUTlJbTB4TUNBeU1HdzBMVEUyYlRRZ05HdzBJRFJzTFRRZ05FMDJJREUyYkMwMExUUnNOQzAwSWk4XHUwMDJCUEM5emRtY1x1MDAyQiIKICB9Cn0=";

    static ProjectComponentPlugin()
    {
      Rhino.PlugIns.PlugIn.LoadPlugIn(s_rhinocode);

      // get platforms registry into a dynamic type to avoid using
      // the actual registry type. Otherwise when underlying api changes
      // it will throw an exception.
      dynamic projectRegistry = RhinoCode.Platforms;
      // get project server
      s_projectServer = projectRegistry.QueryLatest(s_rhino)?.ProjectServer;
      if (s_projectServer is null)
      {
        RhinoApp.WriteLine($"Error loading Grasshopper plugin. Missing \"{s_rhino}\" platform");
        return;
      }

      // get project
      var dctx = new InvokeContext
      {
        Inputs =
        {
          ["projectAssembly"] = typeof(ProjectComponentPlugin).Assembly,
          ["projectId"] = s_projectId,
          ["projectData"] = s_projectData,
        }
      };

      if (s_projectServer.TryInvoke("plugins/v1/deserialize", dctx)
            && dctx.Outputs.TryGet("project", out IProject project))
      {
        // server reports errors
        s_project = project;
      }

      // get icons
      if (!_iconData.Contains("ASSEMBLY-ICON"))
      {
        var ictx = new InvokeContext { Inputs = { ["iconData"] = _iconData } };
        if (s_projectServer.TryInvoke("plugins/v1/icon/gh/assembly", ictx)
              && ictx.Outputs.TryGet("icon", out SD.Bitmap icon))
        {
          // server reports errors
          PluginIcon = icon;
        }

        if (s_projectServer.TryInvoke("plugins/v1/icon/gh/category", ictx)
              && ictx.Outputs.TryGet("icon", out icon))
        {
          // server reports errors
          PluginCategoryIcon = icon;
        }
      }
    }

    public override GH_LoadingInstruction PriorityLoad()
    {
      Grasshopper.Instances.ComponentServer.AddCategorySymbolName("AECtech plugin", "AECtech plugin"[0]);

      if (PluginCategoryIcon != null)
        Grasshopper.Instances.ComponentServer.AddCategoryIcon("AECtech plugin", PluginCategoryIcon);

      return GH_LoadingInstruction.Proceed;
    }

    public static bool TryCreateScript(GH_Component ghcomponent, string serialized, out object script)
    {
      script = default;

      if (s_projectServer is null) return false;

      var dctx = new InvokeContext
      {
        Inputs =
        {
          ["component"] = ghcomponent,
          ["project"] = s_project,
          ["scriptData"] = serialized,
        }
      };

      if (s_projectServer.TryInvoke("plugins/v1/gh/deserialize", dctx))
      {
        return dctx.Outputs.TryGet("script", out script);
      }

      return false;
    }

    public static bool TryCreateScriptIcon(object script, out SD.Bitmap icon)
    {
      icon = default;

      if (s_projectServer is null) return false;

      var ictx = new InvokeContext
      {
        Inputs =
        {
          ["script"] = script,
        }
      };

      if (s_projectServer.TryInvoke("plugins/v1/icon/gh/script", ictx))
      {
        // server reports errors
        return ictx.Outputs.TryGet("icon", out icon);
      }

      return false;
    }

    public static void DisposeScript(GH_Component ghcomponent, object script)
    {
      if (script is null)
        return;

      var dctx = new InvokeContext
      {
        Inputs =
        {
          ["component"] = ghcomponent,
          ["project"] = s_project,
          ["script"] = script,
        }
      };

      if (!s_projectServer.TryInvoke("plugins/v1/gh/dispose", dctx))
        throw new Exception("Error disposing Grasshopper script component");
    }
  }
}
