namespace LethalAPI.Core.API;

using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using BepInEx;
using BepInEx.Logging;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

/// <summary>
/// An API for loading asset bundles.
/// Asset bundles can be loaded from the Bundles directory.
/// </summary>
public static class Bundle
{
    private static readonly ConcurrentDictionary<string, Object> Assets = new ();
    private static readonly ManualLogSource Logger = new ("LethalAPI.Core");

    public delegate void OnAssetLoadedDelegate(Object asset);

    public delegate void OnAllAssetsLoadedDelegate(ConcurrentDictionary<string, Object> assets);

    [UsedImplicitly]
    public static OnAssetLoadedDelegate OnAssetLoaded;

    [UsedImplicitly]
    public static OnAllAssetsLoadedDelegate OnAllAssetsLoaded;

    /// <summary>
    /// Load all asset bundles from the Bundles directory.
    /// This will be called automatically by the library.
    /// </summary>
    public static void Load()
    {
        Logger.LogInfo("Loading all asset bundles...");

        var bundlesPath = Path.Combine(Paths.BepInExRootPath, "Bundles");
        if (!Directory.Exists(bundlesPath))
        {
            Logger.LogInfo("Creating Bundles directory...");
            Directory.CreateDirectory(bundlesPath);
        }

        var files = Directory.GetFiles(bundlesPath, "*", SearchOption.AllDirectories)
            .Where(f => !f.EndsWith(".manifest", StringComparison.CurrentCultureIgnoreCase))
            .ToList();

        if (files.Count == 0)
        {
            Logger.LogInfo("No asset bundles found, skipping...");
        }

        Logger.LogInfo($"Found {files.Count} asset bundles!");
        foreach (var file in files)
        {
            try
            {
                LoadBundle(file);
            }
            catch (Exception e)
            {
                Logger.LogError($"Failed to load asset bundle '{file}'! {e}");
            }
        }

        Logger.LogInfo("All asset bundles loaded!");
        OnAllAssetsLoadedInternal();
    }

    /// <summary>
    /// Attempt to load an asset bundle from the specified path.
    /// You will likely want to use <see cref="Load"/> instead.
    /// </summary>
    /// <param name="path"></param>
    public static void LoadBundle(string path)
    {
        var bundle = AssetBundle.LoadFromFile(path);
        var names = bundle.GetAllAssetNames();
        foreach (var name in names)
        {
            Logger.LogInfo("Loading asset '" + name + "'...");
            var obj = bundle.LoadAsset(name);
            if (obj != null)
            {
                var key = name.ToUpper();
                if (!Assets.TryAdd(key, obj))
                {
                    Logger.LogWarning($"Asset {key} already exists, skipping...");
                }
                else
                {
                    Logger.LogInfo($"Loaded asset '{key}'!");
                    OnAssetLoadedInternal(obj);
                }
            }
            else
            {
                Logger.LogWarning($"Failed to load asset '{name}'! Skipping...");
            }
        }
    }

    public static TAssetType? GetAsset<TAssetType>(string name) where TAssetType : Object
    {
        var key = name.ToUpper();
        if (Assets.TryGetValue(key, out var asset))
        {
            return asset as TAssetType;
        }

        return null;
    }

    private static void OnAssetLoadedInternal(Object asset)
    {
        OnAssetLoaded?.Invoke(asset);
    }

    private static void OnAllAssetsLoadedInternal()
    {
        OnAllAssetsLoaded?.Invoke(Assets);
    }
}
