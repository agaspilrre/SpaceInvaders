using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace org.poo.invaders.exceptions
{
    public class VelocidadErroneaException: Exception
    {
        public VelocidadErroneaException(string mensaje)
            : base(mensaje)
        {

        }
    }
}
