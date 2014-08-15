﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace NLORM.Core.Exceptions
{
    [Serializable]
    public class NLORMException : Exception,ISerializable
    {

        public NLORMException(string message) :base(message)
        {
        }
    }
}