﻿using MagicalLifeAPI.DataTypes.Attribute;
using MagicalLifeAPI.Entity.Experience;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace MagicalLifeAPI.Entity.Skills
{
    /// <summary>
    /// The skill of a creature in mining.
    /// </summary>
    [ProtoContract]
    public class HarvestingSkill : Skill
    {
        private static readonly string PublicDisplayName = "Harvesting";
        public static readonly string InternalIDName = "HarvestingSkill";

        public HarvestingSkill(ComboAttribute skillAmount, bool learnable)
            : base(PublicDisplayName, skillAmount, learnable, InternalIDName, new SqrtXPCalculator(12))
        {

        }

        protected HarvestingSkill()
        {
            //Protobuf-net constructor. 
        }
    }
}
