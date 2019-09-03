using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace Shaolin_Defender
{
    class Coins
    {
         public List<Vector2> coinPos = new List<Vector2>(); // List to store the location of the coins
        public void addCoins()
        {
            coinPos.Add(new Vector2(554, 252));
            coinPos.Add(new Vector2(798, 184));
            coinPos.Add(new Vector2(1068, 474));
            coinPos.Add(new Vector2(578, 780));
            coinPos.Add(new Vector2(800, 500));
            coinPos.Add(new Vector2(1300, 580));
            coinPos.Add(new Vector2(1455, 800));
            coinPos.Add(new Vector2(1600, 550));
            coinPos.Add(new Vector2(2350, 730));
            coinPos.Add(new Vector2(2350, 350));
            coinPos.Add(new Vector2(2350, 1150));
        }
    }
}
