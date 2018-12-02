﻿using MagicalLifeAPI.Components.Generic.Renderable;
using MagicalLifeAPI.Entity.AI.Task;
using MagicalLifeAPI.Entity.AI.Task.Tasks;
using MagicalLifeAPI.World.Base;
using MagicalLifeAPI.World.Data;
using MagicalLifeGUIWindows.Input.History;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicalLifeGUIWindows.Input.Specialized_Handlers
{
    public class ChopActionHandler
    {
        public ChopActionHandler()
        {
            InputHistory.InputAdded += this.InputHistory_InputAdded;
        }

        private void InputHistory_InputAdded()
        {
            HistoricalInput last = InputHistory.History.Last();

            if (last.ActionSelected == ActionSelected.Chop)
            {
                foreach (MagicalLifeAPI.GUI.Selectable item in last.Selected)
                {
                    Tile tile = World.GetTile(RenderInfo.Dimension, item.MapLocation.X, item.MapLocation.Y);

                    if (tile.Resources != null && tile.ImpendingAction == ActionSelected.None)
                    {
                        HarvestTask task = new HarvestTask(tile.MapLocation, Guid.NewGuid());
                        tile.ImpendingAction = ActionSelected.Chop;
                        TaskManager.Manager.AddTask(task);
                    }
                }
            }
        }
    }
}
