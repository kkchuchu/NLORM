﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NLORM.Core.Exceptions
{
    public class ParaErrorException : NLORMException
    {
        public ParaErrorException(string message,Object para):base(message)
        {
                    
        }

        public ParaErrorException(string message): base(message)
        {

        }
    }
}
