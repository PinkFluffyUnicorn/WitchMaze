using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WitchMaze.ownFunctions;

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

        public float getTotalMilliseconds() { return elapsedTime; }

        public void start()
        {
            running = true;
        }

        public void stop()
        {
            running = false;
        }

        public void update(ownGameTime gameTime)
        {
            //check if clock is started jet
            if (!running)
                return;
            else
            {
                elapsedTime += gameTime.getElapsedGameTime();
                int hseconds = (int)(elapsedTime / 1000) % 60;
                int hminutes = ((int)(elapsedTime / 1000) - hseconds) / 60;

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
            return (minutes.getHeight() + doppelpunkt.getHeight() + seconds.getHeight()) / 3;
        }
        public override float getWidth()
        {
            return minutes.getHeight() + doppelpunkt.getHeight() + seconds.getHeight();
        }

        public override void setPosition(Vector2 p)
        {
            minutes.setPosition(p);
            doppelpunkt.setPosition(new Vector2(p.X + minutes.getWidth(), p.Y));
            seconds.setPosition(new Vector2(p.X + minutes.getWidth() + doppelpunkt.getWidth(), p.Y));
            position = p;
        }

        public override void setIndividualScale(float _individualScale)
        {
            individualScale = _individualScale;
            minutes.setIndividualScale(_individualScale);
            seconds.setIndividualScale(_individualScale);
            doppelpunkt.setIndividualScale(_individualScale);
        }

        public override void draw()
        {

            minutes.draw();
            doppelpunkt.draw();
            seconds.draw();
        }

    }
}
