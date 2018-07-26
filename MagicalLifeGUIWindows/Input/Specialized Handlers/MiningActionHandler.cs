﻿using MagicalLifeAPI.Entity.AI.Job;
using MagicalLifeAPI.Entity.AI.Job.Jobs;
using MagicalLifeAPI.Networking.Client;
using MagicalLifeAPI.Networking.Messages;
using MagicalLifeAPI.World;
using MagicalLifeAPI.World.Data;
using MagicalLifeGUIWindows.Input.History;
using MagicalLifeGUIWindows.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicalLifeGUIWindows.Input.Specialized_Handlers
{
    public class MiningActionHandler
    {
        public MiningActionHandler()
        {
            InputHistory.InputAdded += this.InputHistory_InputAdded;
        }

        private void InputHistory_InputAdded()
        {
            HistoricalInput last = InputHistory.History.Last();

            if (last.ActionSelected == ActionSelected.Mine)
            {
                foreach (MagicalLifeAPI.GUI.Selectable item in last.Selected)
                {
                    Tile tile = World.GetTile(RenderingPipe.Dimension, item.MapLocation.X, item.MapLocation.Y);
                    MineJob job = new MineJob(tile.MapLocation);
                    tile.ImpendingAction = ActionSelected.Mine;
                    ClientSendRecieve.Send(new JobCreatedMessage(job));
                }
            }
        }
    }
}
