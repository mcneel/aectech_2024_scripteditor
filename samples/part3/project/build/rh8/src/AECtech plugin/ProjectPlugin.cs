using System;

using Rhino;
using Rhino.Commands;
using Rhino.PlugIns;

using Rhino.Runtime.Code;
using Rhino.Runtime.Code.Platform;

namespace RhinoCodePlatform.Rhino3D.Projects.Plugin
{
  public class ProjectPlugin : PlugIn
  {
    static readonly Guid s_rhinocode = new Guid("c9cba87a-23ce-4f15-a918-97645c05cde7");
    static readonly PlatformSpec s_rhino = new PlatformSpec("mcneel.rhino3d.rhino");

    static readonly Guid s_projectId = new Guid("6da0feaa-7890-4dbc-83bf-5e88b064ea2c");
    static readonly string s_projectData = "ew0KICAiaWQiOiAiNmRhMGZlYWEtNzg5MC00ZGJjLTgzYmYtNWU4OGIwNjRlYTJjIiwNCiAgImlkZW50aXR5Ijogew0KICAgICJuYW1lIjogIkFFQ3RlY2ggcGx1Z2luIiwNCiAgICAidmVyc2lvbiI6ICIwLjEiLA0KICAgICJwdWJsaXNoZXIiOiB7DQogICAgICAiZW1haWwiOiAicGVkcm9AbWNuZWVsLmNvbSIsDQogICAgICAibmFtZSI6ICJQZWRybyBDb3J0ZXMiLA0KICAgICAgImNvbXBhbnkiOiAiTWNOZWVsIEV1cm9wZSINCiAgICB9LA0KICAgICJkZXNjcmlwdGlvbiI6ICJUaGlzIGlzIHRoZSBmaXJzdCBwcm9qZWN0IEkgcHVibGlzaCBpbiBBRUN0ZWNoIHdvcmtzaGlwIiwNCiAgICAiY29weXJpZ2h0IjogIkNvcHlyaWdodCBcdTAwQTkgMjAyNCBQZWRybyBDb3J0ZXMiLA0KICAgICJsaWNlbnNlIjogIk1JVCINCiAgfSwNCiAgInNldHRpbmdzIjogew0KICAgICJidWlsZFBhdGgiOiAiZmlsZTovLy9DOi9Vc2Vycy9wZWRyby5jb3J0ZXMvRGVza3RvcC9NY05lZWwyMDI0LzAzX1NjcmlwdEVkaXRvcldTL2FlY3RlY2hfMjAyNF9zY3JpcHRlZGl0b3Ivc2FtcGxlcy9wYXJ0My9wcm9qZWN0L2J1aWxkL3JoOCIsDQogICAgImJ1aWxkVGFyZ2V0Ijogew0KICAgICAgImFwcE5hbWUiOiAiUmhpbm8zRCIsDQogICAgICAiYXBwVmVyc2lvbiI6IHsNCiAgICAgICAgIm1ham9yIjogOA0KICAgICAgfSwNCiAgICAgICJ0aXRsZSI6ICJSaGlubzNEICg4LiopIiwNCiAgICAgICJzbHVnIjogInJoOCINCiAgICB9LA0KICAgICJwdWJsaXNoVGFyZ2V0Ijogew0KICAgICAgInRpdGxlIjogIk1jTmVlbCBZYWsgU2VydmVyIg0KICAgIH0NCiAgfSwNCiAgImNvZGVzIjogWw0KICAgIHsNCiAgICAgICJpZCI6ICI5YmMyMDQ5Ny1lZmJmLTQzNGEtOTU3ZC0yNTVlNjVmZWU5ZWIiLA0KICAgICAgImxhbmd1YWdlIjogew0KICAgICAgICAiaWQiOiAiKi4qLmNzaGFycCIsDQogICAgICAgICJ2ZXJzaW9uIjogIiouKi4qIg0KICAgICAgfSwNCiAgICAgICJ0aXRsZSI6ICJCYXRjaFByaW50aW5nX0NvbW1hbmQiLA0KICAgICAgInRleHQiOiAiUkZGd01XTXliSFZhZVVKVVpWaE9NRnBYTURkRVVYQXhZekpzZFZwNVFsUmxXRTR3V2xjd2RWTlZPRGRFVVhBeFl6SnNkVnA1UWxSbFdFNHdXbGN3ZFZSSGJIVmpWSE5PUTJjd1MyUllUbkJpYldOblZXMW9jR0p0T0RkRVVYQXhZekpzZFZwNVFsTmhSMngxWW5rMVJXRllUbmRpUjBZMVQzY3dTMlJZVG5CaWJXTm5WVzFvY0dKdE9IVlNiV3h6V2xWc1VFOTNNRXRFVVhCNlpFaEtjR0p0WTJkalIwWXdZVU5CT1VsSVRqQmpiV3gxV25rMVJtSllRakJsVkhOT1EyY3dTMHg1T0dkTlYwVjFTVVZrYkdSRFFqQmhSMVZuWTBkR01HRkRRakJoU0VveFNVaFNiMXBUUW1waU1qRjBXVmMxYTBsSGVIQmliVlZPUTI1YWFHTnBRbnBrVjA1cVdsaE9la2xFTUdkVmJXaHdZbTA0ZFZOWE5YZGtXRkYxVlcxb2NHSnRPVWhhV0ZGMVVqSldNRlV6VW5saFZ6VnVTME5LVVdKSFZtaGpNbFZuWXpKV2MxcFhUakJKU0ZKdldsTkNiV0l5ZUd0YVdFbG5aREpvYkdOdFZXZGxWemt4WTJsQ2JXRlhlR3hqZVVKellWaGFiRWxIYkhWSmFYZG5XbTFHYzJNeVZYTkpTRXBzV21sQ2QxbFlVbTlMVkhOT1EyMXNiVWxEYUhwa1YwNXFXbGhPZWtsRFJUbEpSa3B2WVZjMWRreHJUblppVnpGb1ltMVNla3hzU214ak0xWnpaRU0xVkdSWFRtcGFXRTU2UzFFd1MyVjNNRXRKUTBGblNVVk9kbUp1VG5aaVIxVjFWak5LY0dSSFZrMWhWelZzUzBOS1ZXRkhWbmxhVTBJeldWaE5aMWxYTkdkYVdFcDVZak5KYVV0VWMwNURhVUZuU1VOQ2VWcFlVakZqYlRRM1JGRndPVVJSYjA1RGFUaDJTVVJHYVV4cFFraGFXRkZuWkVkb2JFbElRbWhrUjJkblpGaE9jR0p0WTJkU1ZsSlFURU5DYUVsSFRubGlNMDU2VEZoQ2MxbFlVbTFpTTBwMFNVWldTa2xIZUhCWmJrcG9ZMjVyVGtOcE9IWkpTRnBvWTJsQ2EyRlhSbk5pTW1OblVGTkNkVnBZWTJkU1dGSjJUR3RhZG1OdE1YcE1iRTVzWWtkV2FtUkZXblppUjFKc1kydFNjRmxYZUhaYWVXZHdUM2N3UzB4NU9HZGFSMnhvWWtjNWJreHNVbkJrUjNoc1NVUXdaMGxzUW5OYVYwWjZXbE5DZWxwWGVHeFpNMUZuWkVkb2JFbEhXblppUjFKc1kybENNMkZIVm5sYVUwSTFZak5XZVVsSFduQmlSMVo2U1VkNGNHUnRWV2RoVnpScFQzY3dTMFJSYjNaTWVVSndXbWxCYjFwSGJHaGlSemx1VEd4T2IySXpaRVZoVjBaellqSmpiMkp1Vm5OaVEydG5TVlF3WjFKWVVuWk1hMXAyWTIweGVreHJVbkJaVjNoMldqRktiR016Vm5Oa1F6VlFZWGxyVGtOcE9IWkpTSE5PUTJrNGRrbERRV2RKUTBKRVlqSTFlbUl5ZUd4TWJHUjVZVmhTYkZSSGJIVmFVMmRwVmtkb2JHTnRWV2RrTWtaNlNVZEdkVWxIVm5samJUbDVTV2xyTjBSUmIzWk1lVUZuU1VOQloyTnRWakJrV0VwMVQzY3dTMHg1T0dkbVVUQkxUSGs1ZDFsWVVtOUpSREJuV2tkc2FHSkhPVzVNYTFKd1kyMVdhbVJIT1hsbFZITk9RMmN3UzFFeU9YVmpNamx6V2xNMVdHTnRiREJhVlhod1ltMVZiMHBEU2xGamJXeDFaRWRzZFZwNVFtMWpiVGwwU1VoMGQxbFlVbTltVTBWb1NXbHJOMFJSYjA1RGFUaDJTVVJKZFVsRlpHeGtTRkp3WW0xaloyUkhhR3hKUjFwd1lrZFdla2xIV25saU1qQm5aRWRvYkVsSVFubGlNMXB3V2tkV2EwbElRbWhrUjJkT1EyNWFhR05wUW1oaVIzaEhZVmQ0YkdONVFUbEpSVkp3WTIxV2FtUkhPWGxsVXpWSVdsaFNSMkZYZUd4amVXaDNXVmhTYjB0VE5WVmlNSGh3WXpOUmIwdFVjMDVEYmxwb1kybENiV0ZYZUd4amVVRTVTVWRHYzJKRlduQmlSMVo2VEd4a2IxcFlTbXhMUjFsblVGUTBaMXBwTlVaaWJWSjZWakpzTUdGRFoybE1hazVyWWxOSmNFdFROVlZpTUhod1l6TlJiMHRVYzA1RFp6QkxVVEk1ZFdNeU9YTmFVelZZWTIxc01GcFZlSEJpYlZWdlNrTktUMlJYTVdsYVdFbG5ZakpaWjFwdE9URmliVkZuV20xc2MxcFlUVFpKU0hSb1lrZDRSMkZYZUd4amVUVkVZak5XZFdSSU1HaEpVMGx3VDNjd1MxRXlPWFZqTWpseldsTTFXR050YkRCYVZYaHdZbTFWYjBwRFNrOWtWekZwV2xoSloySXlXV2RhYld4eldsaE5aMlJIT0dkalNFcHdZbTVSTmtsSWRHMWhWM2hzWTNrMVJHSXpWblZrU0RCb1NWTkpjRTkzTUV0RVVXOTJUSGxCZWt4cFFraGFXRkl3WVZjMWJrbElVbTlhVTBKcVlqSXhkMkpIVmpCYVUwSnRZVmQ0YkVsSE5XaGlWMVo2U1VkR2RWcERRakJoUjFWbldsaG9kMkl6U2pCSlJ6Vm9ZbGRXZWtSUmNESlpXRWxuV2tkV2VtUkhiSFZaV0ZKd1lqSTFla2xFTUdkYWJXeHpXbGhOZFZVeVZuTmFWMDR3UzBobloxQlVOR2RWUjBZd1lVTTFSR0l5TVdsaFZ6VnNTMGhDYUdSSFozTkpTR2R3UzFNMVZXSXdlSEJqTTFGdlMxUnpUa051V21oamFVSnNaVWhDZG1OdVVrOVpWekZzWTNsQk9VbEhVbXhqTTFKd1ltMUdNR0ZYT1hWamVUVlVXbGQ0YkZrelVXOWxRMEU1VUdsQ05FeHNTbXhqUjNob1dUSlZiMGxwTkhwYVJ6QnBURU5CYVV4dVFtdGFhVWx3UzFNMVZXSXdlSEJqTTFGdlMxUnpUa05uTUV0TWVUaG5Ua00wWjFWSVNuQmlibEp3WW0xaloxbFhlSE5KUmtwdllWYzFka2xIV25CaVIxWjZSRkZ3YldJelNXZExSMngxWkVOQ2NFbEVNR2ROUkhObllWTkJPRWxIVW14ak0xSndZbTFHTUdGWE9YVmplVFZFWWpOV2RXUkVjMmRoVTNOeVMxRXdTMlYzTUV0RVVXOW5TVU5CWjB4NU9HZE9RelI0U1VaT2JHUklVbkJpYldOblpFZG9iRWxITVhaYVIyeHRZVmRXYTBsSFduTlpWMk5uWkVjNFoxcHRSbk5qTWxWblpFYzRaMWxZV25aaFYxRm5aRWRvYkVsSFRuWmlWekZvWW0xUloyTllWbXhqTTFKd1lqSTBaMWxYU25aa1dGRm5ZekpHTW1GWE5XNUpTRkp2V2xOQ2JXRlhlR3hFVVc5blNVTkJaMVZ0YUhCaWJUbEZZakpOZFZGWFRqQmhXRnBzVWtjNWFreHJNWFphUjJ4dFlWZFdhMGxFTUdkYWJVWnpZekpWTjBSUmIwNURhVUZuU1VOQmRreDVRVEJNYWtsblVUTktiRmxZVW5CaWJXTm5XVmMxYTBsSVNqRmliVFZ3WW0xaloyUkhhR3hKUnpsM1dsYzFjR0p0WTJkak1rNTVZVmhDTUVSUmIyZEpRMEZuWXpOU2VXRlhOVzVKU0U1cVkyMXNkMlJEUVRsSlNFNHdZMjFzZFZwNU5VZGlNMHAwV1ZoUmIwbHNPSFJVTTBKc1ltbENZMGx1YzNkbVZuZHBTV2wzWjFwSFZucGtSMngxV1ZoU2NHSXlOWHBYTW14a1MxUnpUa05wUVdkSlEwSlRZVWRzZFdJd1JuZGpRelZUWkZjMVZGa3pTbkJqU0ZGdll6Sk9lV0ZZUWpCTVEwSnRXVmQ0ZWxwVGF6ZEVVVzlPUTJsQlowbERRakpaV0VsbldUTldlV050Vm5Wa1JWSjJXWGxCT1VsR1NtOWhWelYyVWtjNWFreHJSbXBrUjJ3eVdsVlNkbGw2YzA1RFp6QkxTVU5CWjBsRE9IWkpSRkYxVFhsQ1JHTnRWbWhrUjJ4MVdubENhRWxIU25OWlZ6VnlTVWhDYTFwcFFtMWhWM2hzU1VoU2RrbElUakJpTTBwc1NVZEdjMkpEUW5OWldHeDJaRmhTZWtsSFdubGlNakJuWkVkb2JFbEdTbTloVnpWMlNVZGFjR0pIVlU1RGFVRm5TVU5DTWxsWVNXZGpSMUp0U1VRd1oxSnRiSE5hVmtKcldtazFSR050Vm1oa1IxVnZTMVJ6VGtOcFFXZEpRMEl5V1ZoSloxcElRbkJKUkRCblRtcEJkMDkzTUV0RVVXOW5TVU5CWjB4NU9HZE9RelF3U1VWc01GcFlTbWhrUjJ4MVdubENhR0pIZDJkaVIwWTFZak5XTUdONVFtaGliVkZuV1RJNWRWcHRiRzVrV0Vwd1ltMWpaMk15VmpCa1IyeDFXak5OWjJSSE9HZGFXR2gzWWpOS01FbElVblpKU0VKcldtY3dTMGxEUVdkSlNGcG9ZMmxDZDFsWFpHeGplVUU1U1VkT01XTnVTbXhpYmxKRllqSk5kVlp0Ykd4a00wMTFVakpXTUZWSFJtNWFWbHB3V2xoa2VrdERhemRFVVc5blNVTkJaMXB0T1hsYVYwWnFZVU5CYjFWdGFIQmliVGxSV1Zka2JGWnRiR3hrZVVKM1dWZGtiRWxIYkhWSlNFSm9XakpXZWt0Uk1FdEpRMEZuU1VoelRrTnBRV2RKUTBGblNVTkJaMlJ0Um5sSlIwNW9ZMGhTTVdOdFZXZFFVMEoxV2xoaloxWnRiR3hrTUU1b1kwaFNNV050VmxSYVdGSXdZVmMxYm1ONWFIZFpWMlJzVEVOQ2EyTkhhM0JQZHpCTFNVTkJaMGxEUVdkSlEwSnFXVmhDTUdSWVNteE1hemt4WkVoQ01XUkZUblppUnpsNVNVUXdaMDFFYzA1RGFVRm5TVU5CWjBsRFFXZFpNa1ozWkVoV2VWcFROVlZhV0dnd1VrYzVNRlZIT1hCaWJsSlVZVmh3YkVsRU1HZFBRelIzVDNjd1MwbERRV2RKUTBGblNVTkNhbGxZUWpCa1dFcHNUR3RTYkZwdFJqRmlTRkpSWTIxc2RXUkdaSEJhU0ZKdlZGZHNjMkpIYkhSYVdGSnNZMjVOWjFCVFFYZE1ha2szUkZGdlowbERRV2RKUTBGblNVaENhMXBwTlVKYVIxSlJXVmRrYkV0SFRtaGpTRkl4WTIxVmNFOTNNRXRKUTBGblNVZ3dUa05uTUV0SlEwRm5TVU00ZGtsRVVYVk9VMEpVV1ZoYWNHSnRZMmRpTTFaNVNVYzFiR1I1UW5kYVIxbG5XbTFzYzFwUk1FdEpRMEZuU1VoQ2ExcHBOVmhqYld3d1dsTm9iR1ZJUW5aamJsSlBXVmN4YkdNeGRIQllVMnMzUkZGd09VUlJiMDVEWnowOSINCiAgICB9DQogIF0NCn0=";

    static bool s_initialized = false;
    static IProjectServer s_projectServer = default;
    static IProject s_project = default;

    public static void Initialize()
    {
      if (s_initialized)
        return;

      Rhino.PlugIns.PlugIn.LoadPlugIn(s_rhinocode);

      // get platforms registry into a dynamic type to avoid using
      // the actual registry type. Otherwise when underlying api changes
      // it will throw an exception.
      dynamic projectRegistry = RhinoCode.Platforms;
      // get project server
      s_projectServer = projectRegistry.QueryLatest(s_rhino)?.ProjectServer;
      if (s_projectServer is null)
      {
        RhinoApp.WriteLine($"Error loading plugin. Missing \"{s_rhino}\" platform");
        return;
      }

      // get project
      var dctx = new InvokeContext
      {
        Inputs =
        {
          ["projectAssembly"] = typeof(ProjectPlugin).Assembly,
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

      s_initialized = true;
    }

    public static ProjectPlugin Instance { get; private set; }

    public static Rhino.Commands.Result RunCode(Command command, Guid id, RhinoDoc doc, RunMode mode)
    {
      if (s_project is null)
      {
        RhinoApp.WriteLine($"Error running command {id}. Project deserializiation failed.");
        return Rhino.Commands.Result.Failure;
      }

      var rctx = new InvokeContext
      {
        Inputs =
        {
          ["command"] = command,
          ["project"] = s_project,
          ["projectId"] = id,
          ["doc"] = doc,
          ["runMode"] = mode,
        }
      };

      if (s_projectServer.TryInvoke("plugins/v1/run", rctx))
        return Rhino.Commands.Result.Success;

      // server reports error
      else
        return Rhino.Commands.Result.Failure;
    }

    public ProjectPlugin() { Instance = this; }
  }
}
