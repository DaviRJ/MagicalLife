﻿using MagicalLifeAPI.Registry.ItemRegistry;
using MagicalLifeAPI.Universal;
using MagicalLifeAPI.World.Items;
using System;
using System.Collections.Generic;

namespace MagicalLifeAPI.Load
{
    public class ItemLoader : IGameLoader
    {
        public int GetTotalOperations()
        {
            return 1;
        }

        public void InitialStartup(ref int progress)
        {
            ItemRegistry.Initialize(new List<Type>()
            {
                typeof(StoneChunk)
            });
            progress++;
        }
    }
}