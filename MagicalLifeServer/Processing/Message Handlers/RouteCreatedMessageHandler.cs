﻿using MagicalLifeAPI.DataTypes;
using MagicalLifeAPI.Entities;
using MagicalLifeAPI.Filing.Logging;
using MagicalLifeAPI.Networking.Messages;
using MagicalLifeAPI.Networking.Serialization;
using MagicalLifeAPI.Pathfinding;
using MagicalLifeAPI.World;
using MagicalLifeAPI.World.Data;
using System.Collections.Generic;
using System.Linq;

namespace MagicalLifeServer.Message_Handlers
{
    /// <summary>
    /// How the server handles receiving route data for a creature.
    /// </summary>
    public class RouteCreatedMessageHandler : MessageHandler
    {
        public RouteCreatedMessageHandler() : base(3)
        {
        }

        public override void HandleMessage(BaseMessage message)
        {
            RouteCreatedMessage msg = (RouteCreatedMessage)message;

            if (Validated(msg.Path, msg.Dimension))
            {
                Point2D location = msg.Path[0].Origin;
                Point2D chunkLocation = WorldUtil.CalculateChunkLocation(location);
                Chunk chunk = World.GetChunk(msg.Dimension, chunkLocation.X, chunkLocation.Y);

                Living l = chunk.Creatures.Where(t => t.MapLocation == location).ElementAt(0);

                if (l != null && l.ID == msg.LivingID)
                {
                    l.QueuedMovement.Clear();
                    MagicalLifeAPI.Util.Extensions.EnqueueCollection<PathLink>(l.QueuedMovement, msg.Path);
                }
            }
            else
            {
                MasterLog.DebugWriteLine("Server received invalid path");
            }
        }

        private bool Validated(List<PathLink> msg, int dimension)
        {
            foreach (PathLink item in msg)
            {
                bool a = World.Dimensions[dimension][item.Origin.X, item.Origin.Y].IsWalkable;
                bool b = World.Dimensions[dimension][item.Destination.X, item.Destination.Y].IsWalkable;

                if (a == false || b == false)
                {
                    return false;
                }
            }

            return true;
        }
    }
}