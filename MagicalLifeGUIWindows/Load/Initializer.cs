﻿using MagicalLifeAPI.Filing;
using MagicalLifeAPI.Load;
using MagicalLifeAPI.Sound;
using MagicalLifeGUIWindows.Input;
using MagicalLifeGUIWindows.Input.History;
using System;

namespace MagicalLifeGUIWindows.Load
{
    public class Initializer : IGameLoader
    {
        public void InitialStartup()
        {
            SettingsManager.Initialize();

            if (SettingsManager.PlayerSettings.Settings.PlayerID == Guid.Empty)
            {
                SettingsManager.PlayerSettings.Settings.PlayerID = Guid.NewGuid();
            }

            InputHistory.Initialize();
            InputHandlers.Initialize();
            MusicPlayer.Init();
        }
    }
}