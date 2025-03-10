using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    class Player
    {
        // Current player position in the matrix (multiply by tileSize prior to drawing)

        private Point position; //Point = Vector2, mas são inteiros
        public Point Position => position; //auto função (equivalente a ter só get sem put) - AUTOPROPERTY

        //public Point Position
        //{
        //	get{return position;}
        //}

        public Player(int x, int y) //constructor que dada a as posições guarda a sua posição
        {
            position = new Point(x, y);
        }

    }
}
