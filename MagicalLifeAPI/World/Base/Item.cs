﻿using MagicalLifeAPI.Asset;
using MagicalLifeAPI.GUI;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace MagicalLifeAPI.World.Base
{
    /// <summary>
    /// Represents almost everything in a movable/harvested form.
    /// </summary>
    [ProtoContract]
    public abstract class Item : HasTexture
    {
        /// <summary>
        /// The name of this <see cref="Item"/>.
        /// </summary>
        [ProtoMember(1)]
        public string Name { get; }

        [ProtoMember(2)]
        public int Durability { get; set; }

        /// <summary>
        /// The item's resistance to fire. 0 is no resistance, 1 is completely immune to fire damage.
        /// </summary>
        //[ProtoMember(3)]
        //public AttributeFloat FireResistance { get; set; }

        /// <summary>
        /// The description and lore of this item. Is not revealed until the item has been identified, unless it never needed identification.
        /// </summary>
        [ProtoMember(4)]
        public List<string> Lore { get; set; }

        /// <summary>
        /// The maximum number of this item that may be contained in the same stack.
        /// Must be greater than or equal to one.
        /// </summary>
        [ProtoMember(6)]
        public int StackableLimit { get; set; }

        /// <summary>
        /// How many identical items are currently being clumped and held be this class.
        /// </summary>
        [ProtoMember(7)]
        public int CurrentlyStacked { get; set; }

        /// <summary>
        /// The ID that describes this item to the <see cref="ItemRegistry"/>.
        /// </summary>
        public int ItemID { get; private set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="name">The display name of the item.</param>
        /// <param name="durability">The durability of the item.</param>
        /// <param name="lore">Any text accompanying the item.</param>
        /// <param name="location">The location of this item.</param>
        /// <param name="stackableLimit">How many items of this kind can be in one stack.</param>
        /// <param name="count">How many of this item to create into a stack.</param>
        /// <param name="itemID">The ID of this item.</param>
        public Item(string name, int durability, List<string> lore, int stackableLimit, int count, int itemID)
        {
            this.Name = name;
            this.Durability = durability;
            this.Lore = lore;
            this.StackableLimit = stackableLimit;
            this.CurrentlyStacked = count;
            this.ItemID = itemID;
            this.TextureIndex = AssetManager.GetTextureIndex(this.GetTextureName());
            this.Validate();
        }

        public Item()
        {
            this.TextureIndex = AssetManager.GetTextureIndex(this.GetTextureName());
        }

        protected void Validate()
        {
            if (this.CurrentlyStacked < 1)
            {
                throw new Exception("Error: Cannot have an item with 0 items");
            }
            if (this.StackableLimit < 1)
            {
                throw new Exception("Error: Must be able to stack at least one item");
            }
        }

        public abstract string GetTextureName();

        /// <summary>
        /// Combines two items and returns the result.
        /// Do not continue to use "one" or "two" afterwards, as this function reuses them for performance reasons.
        /// Doing so would cause unknown behavior.
        /// </summary>
        /// <param name="one"></param>
        /// <param name="two"></param>
        /// <param name="overflow">If the amount of the two items is too much, then this parameter is used to store the overflow. Otherwise, it is null.</param>
        /// <returns></returns>
        public static Item Combine(Item one, Item two, out Item overflow)
        {
            //Check to make sure the items are compatible, otherwise modders/hackers/bugs could be exploited to transmute cheap items into rarer things.
            if (one.GetType() == two.GetType())
            {
                one.CurrentlyStacked += two.CurrentlyStacked;

                int extra = one.CurrentlyStacked - one.StackableLimit;
                if (extra > 0)
                {
                    two.CurrentlyStacked = extra;
                    overflow = two;
                }
                else
                {
                    overflow = null;
                }

                return one;
            }
            else
            {
                throw new Exception("Error: Combining two items of different types is impossible to store.");
            }
        }
    }
}