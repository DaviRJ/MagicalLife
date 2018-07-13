﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicalLifeAPI.InternalExceptions
{
    public class CollectionEmptyException : System.Exception
    {
        public CollectionEmptyException() : base("Collection empty!")
        {

        }

        public CollectionEmptyException(string msg) : base(msg)
        {

        }
    }
}
