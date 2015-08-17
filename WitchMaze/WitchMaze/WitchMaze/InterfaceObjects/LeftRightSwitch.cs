using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WitchMaze.InterfaceObjects
{
    class LeftRightSwitch : InterfaceObject
    {
        Icon[] iconsInside;//die Icons innen
        Button Left;
        Button Right;
        bool selected = false;
        int pointer;

        /// <summary>
        /// Creates a new LeftRight Switch.
        /// </summary>
        /// <param name="_position">Position where the Switch should be</param>
        /// <param name="texturePaths">TexturePaths of the Icons that should be inside the Switch. The First Element of the Array will be displayt first</param>
        public LeftRightSwitch(Vector2 _position, String[] texturePaths)
        {
            pointer = 0;
            position = _position;
            iconsInside = new Icon[texturePaths.Length];
            Left = new Button(new Vector2(position.X, position.Y), "Textures/CharacterSelection/LeftNotSelected", "Textures/CharacterSelection/LeftSelected");
            for (int i = 0; i < texturePaths.Length; i++)
                iconsInside[i] = new Icon(new Vector2(position.X + Left.getWidth(), position.Y), texturePaths[i]);
            Left.setPosition(new Vector2(position.X, position.Y + (iconsInside[pointer].getHeight() / 2) - Left.getHeight() / 2));

            Right = new Button(new Vector2(position.X + iconsInside[0].getWidth() + Left.getWidth(), position.Y + (iconsInside[pointer].getHeight() / 2) - Left.getHeight() / 2), "Textures/CharacterSelection/RightNotSelected", "Textures/CharacterSelection/RightSelected");

        }

        /// <summary>
        /// switches the element inside one to the right
        /// </summary>
        public void switchRight()
        {
            pointer++;
            if (pointer >= iconsInside.Length)
                pointer = 0;
        }
        /// <summary>
        /// switches the element inside one to the left
        /// </summary>
        public void switchLeft()
        {
            pointer--;
            if (pointer < 0)
                pointer = iconsInside.Length - 1;
        }
        /// <summary>
        /// sets the Switch to Selected
        /// </summary>
        public void setSelected()
        {
            selected = true;
            Left.setSelected();
            Right.setSelected();
        }
        /// <summary>
        /// sets the Switch to not selected
        /// </summary>
        public void setNotSelected()
        {
            selected = false;
            Left.setNotSelected();
            Right.setNotSelected();
        }
        /// <summary>
        /// returns the state of the Switch, if selected or not
        /// </summary>
        /// <returns>true if selected, false if not</returns>
        public bool isSelected()
        {
            return selected;
        }

        /// <summary>
        /// returns the index of the array of the LeftRightSwitch
        /// </summary>
        /// <returns></returns>
        public int getDisplayedIndex()
        {
            return pointer;
        }

        public override float getWidth()
        {
            return Left.getWidth() + iconsInside[pointer].getWidth() + Right.getWidth();
        }

        public override float getHeight()
        {
            return Left.getHeight();
        }

        public override void draw()
        {
            Left.draw();
            iconsInside[pointer].draw();
            Right.draw();
        }
    }
}
