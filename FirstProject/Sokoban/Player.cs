using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
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

        private Game1 game; //reference from Game1 to Player
        private bool keysReleased = true;

        //public Point Position
        //{
        //	get{return position;}
        //}

        public Player(Game1 game1, int x, int y) //constructor que dada a as posições guarda a sua posição
        {
            position = new Point(x, y);
            game = game1;
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState kState = Keyboard.GetState();
            if (keysReleased)
            {
                Point lastPosition = position;
                keysReleased = false;
                if ((kState.IsKeyDown(Keys.A)) || (kState.IsKeyDown(Keys.Left))) position.X--;
                else if ((kState.IsKeyDown(Keys.W)) || (kState.IsKeyDown(Keys.Up))) position.Y--;
                else if ((kState.IsKeyDown(Keys.S)) || (kState.IsKeyDown(Keys.Down))) position.Y++;
                else if ((kState.IsKeyDown(Keys.D)) || (kState.IsKeyDown(Keys.Right))) position.X++;
                else keysReleased = true;

                // destino é caixa?
                if (game.HasBox(position.X, position.Y))
                {
                    //     _ # Y 
                    // Y = x0 = 10
                    // # = x1 = 9
                    // delta = x1 - x0 = -1
                    // _ = x1 + delta
                    int deltaX = position.X - lastPosition.X;
                    int deltaY = position.Y - lastPosition.Y;
                    Point boxTarget = new Point(deltaX + position.X, deltaY + position.Y);
                    //  se sim, caixa pode mover-se?
                    if (game.FreeTile(boxTarget.X, boxTarget.Y))
                    {
                        for (int i = 0; i < game.boxes.Count; i++)
                        {
                            if (game.boxes[i].X == position.X && game.boxes[i].Y == position.Y)
                            {
                                game.boxes[i] = boxTarget;
                            }
                        }
                    }
                    else
                    {
                        position = lastPosition;
                    }
                }
                else
                {
                    //  se não é caixa, se não está livre, parado!
                    if (!game.FreeTile(position.X, position.Y))
                        position = lastPosition;
                }
            }
            else
            {
                if (kState.IsKeyUp(Keys.A) && kState.IsKeyUp(Keys.W) &&
                    kState.IsKeyUp(Keys.S) && kState.IsKeyUp(Keys.D) &&
                    kState.IsKeyUp(Keys.Left) && kState.IsKeyUp(Keys.Right) &&
                    kState.IsKeyUp(Keys.Up) && kState.IsKeyUp(Keys.Down))
                {
                    keysReleased = true;
                }
            }
        }
    }
}