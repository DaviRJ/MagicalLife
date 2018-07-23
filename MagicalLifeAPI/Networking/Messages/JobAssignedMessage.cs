﻿using MagicalLifeAPI.Entities;
using MagicalLifeAPI.Entity.AI.Job;
using MagicalLifeAPI.Networking.Serialization;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicalLifeAPI.Networking.Messages
{
    /// <summary>
    /// Used to inform the client of a certain <see cref="MagicalLifeAPI.Entities.Living"/>'s new job.
    /// </summary>
    [ProtoContract]
    public class JobAssignedMessage : BaseMessage
    {
        /// <summary>
        /// The living that the <see cref="Job"/> was assigned to.
        /// </summary>
        [ProtoMember(1)]
        public Living Worker { get; set; }

        /// <summary>
        /// The <see cref="Job"/> that the worker/living was assigned.
        /// </summary>
        [ProtoMember(2)]
        public Job Task { get; set; }

        public JobAssignedMessage(Living worker, Job task) : base(5)
        {
            this.Worker = worker;
            this.Task = task;
        }

        public JobAssignedMessage()
        {
        }
    }
}
