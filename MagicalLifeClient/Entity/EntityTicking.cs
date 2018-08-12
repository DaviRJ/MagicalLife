﻿using MagicalLifeAPI.Entity;
using MagicalLifeAPI.Entity.Movement;
using MagicalLifeAPI.World.Data;
using System.Collections.Generic;

namespace MagicalLifeClient.Entity
{
    /// <summary>
    /// Used to do client side entity operations every tick.
    /// </summary>
    public static class EntityTicking
    {
        public static void Initialize()
        {
            Client.ClientTick += Client_ClientTick;
        }

        private static void Client_ClientTick(object sender, ulong e)
        {
            //This needs to be changed, as the performance time WILL be awful.

            foreach (Dimension ii in World.Dimensions)
            {
                int chunkWidth = ii.Width;
                int chunkHeight = ii.Height;

                for (int chunkX = 0; chunkX < chunkWidth; chunkX++)
                {
                    for (int chunkY = 0; chunkY < chunkHeight; chunkY++)
                    {
                        Chunk chunk = ii.GetChunk(chunkX, chunkY);

                        foreach (KeyValuePair<System.Guid, Living> item in chunk.Creatures)
                        {
                            if (item.Value != null)
                            {
                                Living l = item.Value;
                                l.Movement.WearOff();

                                if (item.Value.QueuedMovement.Count > 0)
                                {
                                    EntityWorldMovement.MoveEntity(l);
                                }

                                if (l.Task != null)
                                {
                                    l.Task.DoJob(l);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}