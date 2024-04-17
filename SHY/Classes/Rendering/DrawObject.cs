using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHY.Classes.Rendering
{
    internal interface DrawObject
    {
        public static SpriteBatch SpriteBatch;
        public static Vector2 WindowDimentions;
        void Draw(int x, int y);
    }


    
}
