using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHY.Classes.Rendering
{
    // stores the Transform information
    internal static class Camera
    {
        //the area viewed by the camera
        public static Rectangle CameraRectangle;
        private static Rectangle usedWindowRectangle;

        //to center in the camera on a point
        public static void SetCameraCenter(Point centralPoint)
        {
            CameraRectangle.X = (int)(centralPoint.X - CameraRectangle.Width * 0.5);
            CameraRectangle.Y = (int)(centralPoint.Y - CameraRectangle.Height * 0.5);
        }

        //turns a rectangle in world space into a rectangle relative to the camera
        public static Rectangle RemapRectangle(Rectangle rectangle)
        {
            FRectangle rect = new(rectangle);
            Rectangle result = new Rectangle(
                (int)(  (   (rect.X - CameraRectangle.X)    /   CameraRectangle.Width)  *   usedWindowRectangle.Width) + usedWindowRectangle.X,
                (int)(  (   (rect.Y - CameraRectangle.Y)    /   CameraRectangle.Height) *   usedWindowRectangle.Height) + usedWindowRectangle.Y,
                (int)(  (rect.Width / CameraRectangle.Width)    *   usedWindowRectangle.Width),
                (int)(  (rect.Height / CameraRectangle.Height)  *   usedWindowRectangle.Height)
            );
            return result;
        }

        //makes the camera not stretch sprites
        public static void RecalculateIdealRatioWindowRectangle(int width, int height)
        {
            //the camera ratio
            float WHRatio = (float)CameraRectangle.Width / (float)CameraRectangle.Height;
            //the width not including the padding
            float CameraSpaceWidth = WHRatio * height;
            //the length from the edge of the screen to the camera border
            float ScreenHorizontalPadding = (width - CameraSpaceWidth) * 0.5f;

            //in case the window is actually taller than ideal for whatever reason
            //indicated that the padding added to the width to make it fit the ideal camera ratio is negative
            if(ScreenHorizontalPadding < 0)
            {
                //same thing but reversed for height
                float HWRatio = (float)CameraRectangle.Height/ (float)CameraRectangle.Width;
                float CameraSpaceHeight = HWRatio * width;
                float ScreenVerticalPadding = (height - CameraSpaceHeight) * 0.5f;
                usedWindowRectangle= new Rectangle(0, (int)ScreenVerticalPadding, width, (int)CameraSpaceHeight);
            }else
            usedWindowRectangle = new Rectangle((int)ScreenHorizontalPadding, 0, (int)CameraSpaceWidth, height);
        }

        //rectangle using floats to avoid silly int division situations
        struct FRectangle
        {
            public float X; 
            public float Y; 
            public float Width;
            public float Height;
            public FRectangle(Rectangle r)
            {
                X = r.X;
                Y = r.Y;
                Width = r.Width;
                Height = r.Height;
            }
        }


    }
}
