namespace LethalAPI.API.Features.Items;

extern alias LethalCompany;
using System.Collections.Generic;
using Interfaces;
using BoomboxItem = LethalCompany::BoomboxItem;
using GrabbableObject = LethalCompany::GrabbableObject;

/// <summary>
/// A wrapper class for <see cref="LethalCompany::GrabbableObject"/>
/// </summary>
public class Item : IWrapper<GrabbableObject>
{
    protected Item(GrabbableObject @base)
    {
        Base = @base;
    }

    /// <summary>
    /// Gets the base <see cref="GrabbableObject"/> instance.
    /// </summary>
    public GrabbableObject Base { get; }

    public bool IsGrabbable => Base.grabbable;

    public bool IsPocketed => Base.isPocketed;

    public string Name => Base.itemProperties.itemName;

    public float Weight => Base.itemProperties.weight;

    public bool IsScrap => Base.itemProperties.isScrap;

    public bool IsPowered => Base.itemProperties.requiresBattery;

    /// <summary>
    /// 
    /// </summary>
    public void Equip() => Base.EquipItem();

    public void Pocket() => Base.PocketItem();

    internal static readonly Dictionary<GrabbableObject, Item> BaseToItem = new();

    /// <summary>
    /// Gets or creates an <see cref="Item"/> from a <see cref="GrabbableObject"/>.
    /// This method is used to cache <see cref="Item"/> instances.
    /// </summary>
    /// <param name="base">The <see cref="GrabbableObject"/> to get the <see cref="Item"/> from.</param>
    /// <returns>The <see cref="Item"/> instance.</returns>
    public static Item? Get(GrabbableObject? @base)
    {
        if (@base is null)
        {
            return null;
        }

        if (BaseToItem.TryGetValue(@base, out var item))
        {
            return item;
        }

        return @base switch
        {
            BoomboxItem boomboxItem => new Boombox(boomboxItem),
            _ => new Item(@base)
        };
    }
}
