using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WitchMaze.InterfaceObjects
{
    class Clock : InterfaceObject
    {
        Text seconds;
        Text minutes;
        Text doppelpunkt;

        bool running;

        float elapsedTime;

        public Clock(Vector2 _position)
        {
            position = _position;
            minutes = new Text("00", position);
            doppelpunkt = new Text(" : ", minutes.getPosition() + new Vector2(minutes.getWidth(), 0));
            seconds = new Text("00", doppelpunkt.getPosition() + new Vector2(doppelpunkt.getWidth(), 0));
            elapsedTime = 0f;
        }

        public void start()
        {
            running = true;
        }

        public void stop()
        {
            running = false;
        }

        public void update(GameTime gameTime)
        {
            //check if clock is started jet
            if (!running)
                return;
            else
            {
                elapsedTime += gameTime.ElapsedGameTime.Milliseconds;
                int hseconds = (int)(elapsedTime / 1000) % 60;
                int hminutes = ((int)(elapsedTime / 1000) - hseconds) / 60;
                Console.WriteLine("s:" + hseconds);
                Console.WriteLine("m:" + hminutes);
                Console.WriteLine("e" + elapsedTime);

                if (hseconds < 10)
                    seconds.updateText("0" + hseconds.ToString());
                else
                    seconds.updateText(hseconds.ToString());
                if (hminutes < 10)
                    minutes.updateText("0" + hminutes.ToString());
                else
                    minutes.updateText(hminutes.ToString());
            }

        }
        public override float getHeight()
        {
            return minutes.getHeight() + doppelpunkt.getHeight() + seconds.getHeight();
        }
        public override float getWidth()
        {
            float max = 0;
            if(minutes.getWidth() > max)
                max = minutes.getWidth();
            if (doppelpunkt.getWidth() > max)
                max = doppelpunkt.getWidth();
            if (seconds.getWidth() > max)
                max = seconds.getWidth();
            return max;
        }

        public override void draw()
        {
            minutes.draw();
            doppelpunkt.draw();
            seconds.draw();
        }

    }
}
