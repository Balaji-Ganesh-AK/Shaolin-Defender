using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace Shaolin_Defender
{
    class FireBall
    {
        public List<Vector2> fireStickPos = new List<Vector2>();
        public List<Vector2> fireStickPosMoving = new List<Vector2>();
        public void addFireBall()
        {
            fireStickPos.Add(new Vector2(558, 500));
            fireStickPos.Add(new Vector2(470, 700));
            fireStickPos.Add(new Vector2(900, 600));
            fireStickPos.Add(new Vector2(800, 370));
            fireStickPos.Add(new Vector2(670, 200));
            fireStickPos.Add(new Vector2(1455, 920));
            fireStickPos.Add(new Vector2(1455, 660));
            //fireStickPos.Add(new Vector2(672, 700));
            //fireStickPos.Add(new Vector2(752, 700));
            fireStickPos.Add(new Vector2(2780, 1000));
            fireStickPos.Add(new Vector2(2700, 1000));
            fireStickPos.Add(new Vector2(2630, 1000));
            //fireStickPos.Add(new Vector2(2520, 1000));
            fireStickPos.Add(new Vector2(2460, 1000));
            fireStickPos.Add(new Vector2(2400, 1000));
            fireStickPos.Add(new Vector2(2320, 1000));
            fireStickPos.Add(new Vector2(2260, 1000));
            //fireStickPos.Add(new Vector2(2160, 1000));
            fireStickPos.Add(new Vector2(2080, 1000));
            fireStickPos.Add(new Vector2(2080, 1000));
            fireStickPos.Add(new Vector2(2000, 1000));
            fireStickPos.Add(new Vector2(1920, 1000));

            //fireStickPos.Add(new Vector2(752, 700));
            fireStickPos.Add(new Vector2(2780, 500));
            fireStickPos.Add(new Vector2(2700, 500));
            fireStickPos.Add(new Vector2(2630, 500));
            //fireStickPos.Add(new Vector2(2520, 1000));
            fireStickPos.Add(new Vector2(2460, 500));
            fireStickPos.Add(new Vector2(2400, 500));
            fireStickPos.Add(new Vector2(2320, 500));
            fireStickPos.Add(new Vector2(2260, 500));
            //fireStickPos.Add(new Vector2(2160, 1000));
            fireStickPos.Add(new Vector2(2080, 500));
            fireStickPos.Add(new Vector2(2080, 500));
            fireStickPos.Add(new Vector2(2000, 500));
            fireStickPos.Add(new Vector2(1920, 500));
            //moving fire ball
            fireStickPosMoving.Add(new Vector2(1300, 900));
            fireStickPosMoving.Add(new Vector2(1600, 600));
            fireStickPosMoving.Add(new Vector2(2350, 975));
        }
    }
}
