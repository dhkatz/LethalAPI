namespace LethalAPI.Core.API;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BepInEx;
using BepInEx.Logging;
using UnityEngine;
using UnityEngine.Networking;

public static class Boombox
{
    internal static readonly List<AudioClip?> Clips = new ();
    private static readonly ManualLogSource Logger = new ("LethalAPI.Core");

    /// <summary>
    /// Loads all .wav files from the Assets/Boombox directory.
    /// This will be called automatically by the library.
    /// </summary>
    public static void Load()
    {
        Logger.LogInfo("Loading boombox clips...");

        var assetsPath = Path.Combine(Paths.BepInExRootPath, "Assets", "Boombox");
        if (!Directory.Exists(assetsPath))
        {
            Logger.LogInfo("Creating Assets/Boombox directory...");
            Directory.CreateDirectory(assetsPath);
        }

        var files = Directory.GetFiles(assetsPath, "*.wav", SearchOption.AllDirectories).ToList();
        if (files.Count == 0)
        {
            Logger.LogInfo("No boombox clips found, skipping...");
            return;
        }

        Logger.LogInfo($"Found {files.Count} boombox clips!");
        foreach (var file in files)
        {
            try
            {
                LoadClip(file);
            }
            catch (Exception e)
            {
                Logger.LogError($"Failed to load boombox clip '{file}'! {e.Message}");
            }
        }

        Logger.LogInfo("Finished loading boombox clips!");
    }

    public static void AddClip(AudioClip? clip)
    {
        if (clip == null)
        {
            Logger.LogError("Failed to add clip! Clip is null!");
            return;
        }

        Logger.LogInfo($"Adding clip '{clip.name}'...");

        Clips.Add(clip);
    }

    public static void LoadClip(string path)
    {
        Logger.LogInfo($"Loading clip '{path}'...");

        AudioClip? clip = null;
        var builder = new UriBuilder(path) { Scheme = Uri.UriSchemeFile };
        using var www = UnityWebRequestMultimedia.GetAudioClip(builder.Uri, AudioType.WAV);

        www.SendWebRequest();

        try
        {
            while (!www.isDone) { }
            clip = DownloadHandlerAudioClip.GetContent(www);
        }
        catch (Exception e)
        {
            Logger.LogError($"Failed to load clip! {e.Message}");
        }

        if (clip == null)
        {
            Logger.LogError($"Failed to load clip! {path}");
            return;
        }

        clip.name = Path.GetFileNameWithoutExtension(path);

        AddClip(clip);
    }
}
