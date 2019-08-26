using System;
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
        public int getScore()
        {
            return score;
        }
        public void reset()
        {
            score = 0;
            timer = 100.00f;
        }
    }
}
