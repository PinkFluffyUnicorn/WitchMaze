using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WitchMaze.ownFunctions
{
    /// <summary>
    /// contains all collision tests
    /// </summary>
    class Collision
    {
        /// <summary>
        /// returns if a circle is colliding with a rectangle
        /// </summary>
        /// <param name="circleCenter">the center point of the circle</param>
        /// <param name="r">radius of the circle</param>
        /// <param name="topLeftRectangle">the top left point of the rectangle</param>
        /// <param name="w">the width of the rectangle</param>
        /// <param name="h">the height of the rectangle</param>
        /// <returns></returns>
        public static bool circleRectangleCollision(Vector2 circleCenter, float r, Vector2 topLeftRectangle, float w, float h)
        {
            ////Fall 1: Kreismittelpunkt liegt im rechteck //fall passiert in unserem beispiel quasi immer...
            //if (circleCenter.X < topLeftRectangle.X //linker rand
            //    && circleCenter.X > topLeftRectangle.X + w //rechter rand
            //    && circleCenter.Y > topLeftRectangle.Y //unterer rand
            //    && circleCenter.Y < topLeftRectangle.Y + h)//oberer rand
            //    return true;

            //Fall2: ein außenpunkt des rechteckes liegt im kreis
            Vector2 tl = topLeftRectangle;
            Vector2 tr = new Vector2(tl.X + w, tl.Y);
            Vector2 bl = new Vector2(tl.X, tl.Y - h);
            Vector2 br = new Vector2(tl.X + w, tl.Y - h);
            float bTL = (float)Math.Sqrt((circleCenter.X - tl.X) * (circleCenter.X - tl.X) + (circleCenter.Y - tl.Y) * (circleCenter.Y - tl.Y));
            float bTR = (float)Math.Sqrt((circleCenter.X - tr.X) * (circleCenter.X - tr.X) + (circleCenter.Y - tr.Y) * (circleCenter.Y - tr.Y));
            float bBL = (float)Math.Sqrt((circleCenter.X - bl.X) * (circleCenter.X - bl.X) + (circleCenter.Y - bl.Y) * (circleCenter.Y - bl.Y));
            float bBR = (float)Math.Sqrt((circleCenter.X - br.X) * (circleCenter.X - br.X) + (circleCenter.Y - br.Y) * (circleCenter.Y - br.Y));
            if (bTL < r || bTR < r || bBL < r || bBR < r)
                return true;

            //Fall 3: kreis überschneidet eine kante des rechtecks, ohne einen eckpunkt zu treffen
            //obere kante
            //untere kante
            if (circleCenter.X > topLeftRectangle.X && circleCenter.X < topLeftRectangle.X + w)//prüfen ob es im richtigen x bereich liegt
            {
                //oben
                float dy = Math.Abs(topLeftRectangle.Y - circleCenter.Y);
                if (dy < r)
                    return true;
                //unten
                dy = Math.Abs(topLeftRectangle.Y - h - circleCenter.Y);
                if (dy < r)
                    return true;
            }
            //rechte kante
            //linke kante
            if (circleCenter.Y < topLeftRectangle.Y && circleCenter.Y > topLeftRectangle.Y - h)//prüfen ob es im richtigen x bereich liegt
            {
                //links
                float dx = Math.Abs(topLeftRectangle.X - circleCenter.X);
                if (dx < r)
                    return true;
                //rechts
                dx = Math.Abs(topLeftRectangle.X + w - circleCenter.X);//- oder +, das st hier die frage
                if (dx < r)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// returns if two circles are colliding with each other
        /// </summary>
        /// <param name="circleCenter1">center of Circle1</param>
        /// <param name="r1">redius of Circle2</param>
        /// <param name="circleCenter2">center of Circle2</param>
        /// <param name="r2">radius of Cirlce2</param>
        /// <returns></returns>
        public static bool circleCirlceCollision(Vector2 circleCenter1, float r1, Vector2 circleCenter2, float r2)
        {
            float dCircles = (float)Math.Sqrt((circleCenter1.X - circleCenter2.X) * (circleCenter1.X - circleCenter2.X) + (circleCenter1.Y - circleCenter2.Y) * (circleCenter1.Y - circleCenter2.Y));
            if (dCircles < r1 + r2)
                return true;
            return false;
        }
    }
}
