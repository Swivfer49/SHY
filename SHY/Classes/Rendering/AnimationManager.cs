using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using System.Text.Json.Nodes;
using System.Security.Cryptography.X509Certificates;

namespace SHY.Classes.Rendering
{
    internal class AnimationManager
    {
        public int CurrentAnimationIndex;

        //list of frame index arrays
        //first int in each array is the time between frames
        private int[][] FrameIndexes;

        //dictionary to get the index of the array of frames that is for that animtion from FrameIndexes
        public Dictionary<string, int> animationIndexes;

        //all frames possible
        private Rectangle[] frames;

        private int MillisecondsBetweenFrames, frameNumber;
        public double Milliseconds;

        public AnimationManager(string filePath)
        {
            animationIndexes = new Dictionary<string, int>();

            string text = File.ReadAllText(filePath);
            JsonDocument document = JsonDocument.Parse(text);
            JsonElement root = document.RootElement;

            JsonElement framesJson = root.GetProperty("Frames");

            JsonElement[] framesJsonArray = framesJson.EnumerateArray().ToArray();

            //set the number of frames
            frames = new Rectangle[framesJsonArray.Length];

            //sets all frames to a rectangle based on one of the arrays in the json
            for (int i = 0; i < framesJsonArray.Length; i++)
            {
                //gets the array
                int[] rp = framesJsonArray[i].EnumerateArray().Select(x => x.GetInt32()).ToArray();
                //makes the rectangle frame
                frames[i] = new Rectangle(rp[0], rp[1], rp[2], rp[3]);
            }

            JsonElement animationsJson = root.GetProperty("Animations");
            JsonProperty[] animationsJsonArray = animationsJson.EnumerateObject().ToArray();

            FrameIndexes = new int[animationsJsonArray.Length][];

            for (int i = 0; i < animationsJsonArray.Length; i++)
            {
                FrameIndexes[i] = (animationsJsonArray[i].Value.EnumerateArray().Select(x => x.GetInt32()).ToArray());
                animationIndexes.Add(animationsJsonArray[i].Name, i);
            }
        }

        public void Update(double milliseconds)
        {
            Milliseconds += milliseconds;
            if (Milliseconds > MillisecondsBetweenFrames) 
            {
                frameNumber++;
                if(frameNumber >= FrameIndexes[CurrentAnimationIndex].Length - 1)
                {
                    frameNumber = 0;
                }

                Milliseconds -= MillisecondsBetweenFrames;
            }

        }

        public Rectangle CurrentFrame()
        {
            return frames[FrameIndexes[CurrentAnimationIndex][frameNumber+1]];
        }

        public void SetCurrentAnimation(string name)
        {
            SetCurrentAnimation(animationIndexes[name]);
        }

        public void SetCurrentAnimation(int index)
        {
            //set the animation index
            CurrentAnimationIndex = index;
            //set the time between frames
            MillisecondsBetweenFrames = FrameIndexes[index][0];
            //reset time
            Milliseconds = 0;
            //reset frame number
            frameNumber = 0;
        }

    }
}
