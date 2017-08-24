﻿namespace EarthWithMagicAPI.API.Party
{
    using System.Collections.Generic;
    using EarthWithMagicAPI.API.Creature;

    /// <summary>
    /// The party that the main character is traveling with. This includes the main character.
    /// </summary>
    public static class Party
    {
        public static List<ICreature> TheParty = new List<ICreature>();
    }
}