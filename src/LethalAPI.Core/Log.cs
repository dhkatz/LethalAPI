// -----------------------------------------------------------------------
// <copyright file="Log.cs" company="LethalAPI">
// Copyright (c) LethalAPI. All rights reserved.
// Licensed under the GPL-3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace LethalAPI.Core;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using BepInEx;
using BepInEx.Bootstrap;
using BepInEx.Logging;

public static class Log
{
    private static ManualLogSource Logger { get; } = Plugin.Logger;

    static Log()
    {
        AssemblyNameReplacements = new ConcurrentDictionary<string, string>();
        AssemblyNameReplacements.TryAdd("UnityEngine.CoreModule", "Unity");
    }

    public enum LogType
    {
        Info,
        Debug,
        Warning,
        Error,
        Fatal,
    }

    private static ConcurrentDictionary<string, string> AssemblyNameReplacements { get; }

    private static readonly Dictionary<string, string> LogTemplates = new()
    {
        { "Info", "{time} &7[&b&6{type}&B&7] &7[&b&2{prefix}&B&7]&r {msg}" },
        { "Debug", "{time} &7[&b&6{type}&B&7] &7[&b&2{prefix}&B&7]&r {msg}" },
        { "Warning", "{time} &7[&b&6{type}&B&7] &7[&b&2{prefix}&B&7]&r {msg}" },
        { "Error", "{time} &7[&b&6{type}&B&7] &7[&b&2{prefix}&B&7]&r {msg}" },
        { "Fatal", "{time} &7[&b&6{type}&B&7] &7[&b&2{prefix}&B&7]&r {msg}" },
    };

    public static void Info(string message, string plugin = "")
    {
        Message(LogType.Info, message, plugin);
    }

    public static void Debug(string message, string plugin = "", bool enabled = true)
    {
        Message(LogType.Debug, message, plugin, enabled);
    }

    public static void Warning(string message, string plugin = "")
    {
        Message(LogType.Warning, message, plugin);
    }

    public static void Error(string message, string plugin = "")
    {
        Message(LogType.Error, message, plugin);
    }

    public static void Fatal(string message, string plugin = "")
    {
        Message(LogType.Fatal, message, plugin);
    }

    public static void Message(LogType logType, string message, string plugin = "", bool enabled = true)
    {
        if (!enabled)
        {
            return;
        }

        plugin = !string.IsNullOrEmpty(plugin) ? plugin : GetCallingPlugin(GetCallingMethod());

        var template = LogTemplates[logType.ToString()];
        var time = DateTime.Now.ToString("[g (ss.fff)s]");
        var type = logType.ToString();
        var prefix = plugin;

        var log = template.Replace("{time}", time)
            .Replace("{type}", type)
            .Replace("{prefix}", prefix)
            .Replace("{msg}", message);

        Logger.Log((LogLevel)62, log);
    }

    private static string GetCallingPlugin(MethodBase method)
    {
        try
        {
            if (method.DeclaringType?.Assembly is null)
            {
                return "Unknown";
            }

            var assembly = method.DeclaringType.Assembly;

            PluginInfo? plugin = null;
            if (Chainloader.PluginInfos is not null)
            {
                // FirstOrDefault keeps throwing a NullReferenceException. This doesnt throw an exception so we will use it.
                plugin = Chainloader.PluginInfos
                    .Where(x => x.Value?.Instance is not null)
                    .First(x => x.Value.Instance.GetType().Assembly == assembly)
                    .Value;
            }

            string name;
            if (plugin is null)
            {
                name = assembly.GetName().Name;
                if (string.IsNullOrEmpty(name))
                {
                    return "Unknown";
                }

                if (AssemblyNameReplacements.ContainsKey(name))
                {
                    AssemblyNameReplacements.TryGetValue(name, out name);
                }
            }
            else
            {
                name = plugin.Metadata.Name;
            }

            return name!;
        }
        catch (Exception e)
        {
            return $"Unknown ({e.Message})";
        }
    }

    private static MethodBase GetCallingMethod(int skip = 0)
    {
        var stack = new StackTrace(2 + skip);
        return stack.GetFrame(0).GetMethod();
    }
}
