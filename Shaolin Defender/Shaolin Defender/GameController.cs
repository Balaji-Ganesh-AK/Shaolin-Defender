﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaolin_Defender
{
       
    class GameController
    {
       
        public int score;
        public float timer;
        public GameController()
        {
            score = 0;
            timer = 100.00f;
        }
        public void increaseScore()
        {
            score++;
        }
        public int checkWinState()
        {
           
            if (score == 11)
            {
                return 1;
            }
            else
                return 0;
        }
        public int getScore()
        {
            return score;
        }
        public void reset()
        {
            score = 0;
            timer = 100.00f;

        }
        public void resetTimer()
        {
            timer = 100.00f;
        }
        bool checkCondition()
        {

            return true;
        }
        
    }
}
