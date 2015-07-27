using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WitchMaze.InterfaceObjects
{
    abstract class InterfaceObject
    {
        //Klasse existiert im moment nur, damit man InterfacePbjects zusammen in eine Liste oder so tun kann... 
        protected Vector2 position;
        /// <summary>
        /// returns the width of the interfaceObject
        /// </summary>
        /// <returns></returns>
        public abstract float getWidth();
        /// <summary>
        /// returns the height of the interfaceOnject
        /// </summary>
        /// <returns></returns>
        public abstract float getHeight();
        public abstract void draw();
    }
}
