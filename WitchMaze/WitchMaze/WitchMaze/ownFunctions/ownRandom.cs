﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WitchMaze.ownFunctions
{
    class ownRandom
    {


        Random rnd = new Random();


        public ownRandom()
        {

        }

        /// <summary>
        /// own Randomfunction
        /// </summary>
        /// <param name="IntervalStart">start of Interval, is included in random Numbers</param>
        /// <param name="IntervalEnde">end of Interval, is included in random Numbers</param>
        public double ownRandomFunction(double IntervalStart, double IntervalEnde)
        {
            // Intervall so shiften, dass es bei null anfängt und nachher zurückshiften
            if(IntervalEnde > IntervalStart)
            {
                double help = IntervalEnde;
                IntervalEnde = IntervalStart;
                IntervalStart = help;
            }
            double range = IntervalEnde - IntervalStart;

            double result = randomFunction() * range;
            result += IntervalStart;
            //zurückshiften
            return result;
        }


        /// <summary>
        /// Method which does the actual Random for a number between 0 and 1 
        /// </summary>
        /// <returns></returns>
        private double randomFunction()
        {

            Int32 w = 352617;//rnd.Next(0, Int32.MaxValue);
            Int32 x = 7352849;// rnd.Next(0, Int32.MaxValue);
            Int32 y = 1253802;// rnd.Next(0, Int32.MaxValue);
            Int32 z = rnd.Next(0, Int32.MaxValue);

           

            Int32 t = x ^ (x << 19);
            x = y; y = z; z = w;
            w = w ^ (w >> 7) ^ t ^ (z >> 13);


            return (double)w / (double)Int32.MaxValue;
        }
    }
}
