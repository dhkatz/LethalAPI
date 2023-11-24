namespace LethalAPI.API.Features.Items;

extern alias LethalCompany;
using Interfaces;
using UnityEngine;
using BoomboxItem = LethalCompany::BoomboxItem;

/// <summary>
/// A wrapper for the <see cref="BoomboxItem"/> class.
/// </summary>
public class Boombox : Item, IWrapper<BoomboxItem>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Boombox"/> class.
    /// </summary>
    /// <param name="boombox">The <see cref="BoomboxItem"/> being wrapped.</param>
    public Boombox(BoomboxItem boombox)
        : base(boombox)
    {
        Base = boombox;
    }

    /// <summary>
    /// Gets the base <see cref="BoomboxItem"/> instance.
    /// </summary>
    public new BoomboxItem Base { get; }

    /// <summary>
    /// Gets a value indicating whether the boombox is playing music.
    /// </summary>
    public bool IsPlaying => Base.isPlayingMusic;

    /// <summary>
    /// Gets the current <see cref="AudioSource"/> of the boombox.
    /// </summary>
    public AudioSource AudioSource => Base.boomboxAudio;

    /// <summary>
    /// Starts playing the boombox.
    /// </summary>
    public void Start()
    {
        Base.ItemActivate(true);
    }

    /// <summary>
    /// Stops playing the boombox.
    /// </summary>
    public void Stop()
    {
        Base.ItemActivate(false);
    }
}
