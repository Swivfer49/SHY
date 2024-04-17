﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHY.Classes.Rendering
{
    //this class is for drawing a regular image to the screen
    internal class WorldImageDrawObject : DrawObject
    {
        public WorldImageDrawObject(Texture2D texture, int width, int height)
        {
            _texture = texture;
            Width = width;
            Height = height;
        }

        private Texture2D _texture;
        private int Width, Height;

        public void Draw(int x, int y)
        {
            //draws the image
            DrawObject.SpriteBatch.Draw(
                _texture, 
                //remap rectangle to camera
                Camera.RemapRectangle(  
                    new Rectangle(x, y, Width, Height),
                    DrawObject.WindowDimentions
                ), 
                Color.White
            );
        }
    }
}
