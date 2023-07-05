﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StreamMasterDomain.Common;

public static class BuildInfo
{
    static BuildInfo()
    {
        var assembly = Assembly.GetExecutingAssembly();

        Version = assembly.GetName().Version;

        var attributes = assembly.GetCustomAttributes(true);

        Branch = "unknow";

        var config = attributes.OfType<AssemblyConfigurationAttribute>().FirstOrDefault();
        if (config != null)
        {
            Branch = config.Configuration;
        }

        Release = $"{Version}-{Branch}";
    }

    public static string AppName { get; } = "Stream Master";

    public static Version Version { get; }
    public static string Branch { get; }
    public static string Release { get; }

    public static DateTime BuildDateTime
    {
        get
        {
            var fileLocation = Assembly.GetCallingAssembly().Location;
            return new FileInfo(fileLocation).LastWriteTimeUtc;
        }
    }

    public static bool IsDebug
    {
        get
        {
#if DEBUG
            return true;
#else
                return false;
#endif
        }
    }
}