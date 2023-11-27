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

    /// <summary>
    /// Gets a value indicating whether the item is grabbable.
    /// </summary>
    public bool IsGrabbable => Base.grabbable;

    /// <summary>
    /// Gets a value indicating whether the item is equipped.
    /// </summary>
    public bool IsPocketed => Base.isPocketed;

    /// <summary>
    /// Gets the name of the item.
    /// </summary>
    public string Name => Base.itemProperties.itemName;

    /// <summary>
    /// Gets the carry weight of the item.
    /// </summary>
    public float Weight => Base.itemProperties.weight;

    /// <summary>
    /// Gets a value indicating whether the item is scrap.
    /// </summary>
    public bool IsScrap => Base.itemProperties.isScrap;

    /// <summary>
    /// Gets a value indicating whether the item is battery powered.
    /// </summary>
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
