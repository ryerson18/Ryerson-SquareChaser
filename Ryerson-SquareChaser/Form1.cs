using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace Ryerson_SquareChaser
{
    public partial class Form1 : Form
    {
        Rectangle player1 = new Rectangle(100, 120, 20, 20);
        Rectangle player2 = new Rectangle(100, 250, 20, 20);
        Rectangle boundaries = new Rectangle(50, 40, 350, 330);
        Rectangle point = new Rectangle(200, 210, 10, 10);
        Rectangle slow = new Rectangle(200, 210, 15, 15);


        Pen whitePen = new Pen(Color.White, 8);
        SolidBrush PurpleBrush = new SolidBrush(Color.MediumPurple);
        SolidBrush OrangeBrush = new SolidBrush(Color.DarkOrange);
        SolidBrush coralBrush = new SolidBrush(Color.Coral);
        SolidBrush limeBrush = new SolidBrush(Color.LightGreen);
        SolidBrush slowBrush = new SolidBrush(Color.Red);

        bool wDown = false;
        bool sDown = false;
        bool upArrowDown = false;
        bool downArrowDown = false;

        bool aDown = false;
        bool dDown = false;
        bool leftDown = false;
        bool rightDown = false;

        int player1Score = 0;
        int player2Score = 0;
        int player1Speed = 4;
        int player2Speed = 4;
        int x, y;

        int slowballspeedX = 6;
        int slowballspeedY = -4;

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(PurpleBrush, player1);
            e.Graphics.FillRectangle(OrangeBrush, player2);
            e.Graphics.DrawRectangle(whitePen, boundaries);
            e.Graphics.FillRectangle(limeBrush, point);
            e.Graphics.FillRectangle(slowBrush, slow);
        
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = true;
                    break;
                case Keys.S:
                    sDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
                case Keys.A:
                    aDown = true;
                    break;
                case Keys.D:
                    dDown = true;
                    break;
                case Keys.Left:
                    leftDown = true;
                    break;
                case Keys.Right:
                    rightDown = true;
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {

            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = false;
                    break;
                case Keys.S:
                    sDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
                case Keys.A:
                    aDown = false;
                    break;
                case Keys.D:
                    dDown = false;
                    break;
                case Keys.Left:
                    leftDown = false;
                    break;
                case Keys.Right:
                    rightDown = false;
                    break;
            }
        }

        private void timertick_Tick(object sender, EventArgs e)
        {
            // move red squre
            slow.X += slowballspeedX;
            slow.Y += slowballspeedY;

            Random randGen = new Random();

            //move player 1
            if (wDown == true && player1.Y > 45)
            {
                player1.Y -= player1Speed;
            }

            if (sDown == true && player1.Y < 345)
            {
                player1.Y += player1Speed;
            }

            if (dDown == true && player1.X < 375)
            {
                player1.X += player1Speed;
            }
            if (aDown == true && player1.X > 55)
            {
                player1.X -= player1Speed;
            }

            //move player 2
            if (upArrowDown == true && player2.Y > 45)
            {
                player2.Y -= player2Speed;
            }

            if (downArrowDown == true && player2.Y < 345)
            {
                player2.Y += player2Speed;
            }
            if (rightDown == true && player2.X < 375)
            {
                player2.X += player2Speed;
            }
            if (leftDown == true && player2.X > 55)
            {
                player2.X -= player2Speed;
            }

           //check if slow hit top or bottom wall and change direction if it does

            if (slow.Y < 45 || slow.Y > 370 - slow.Height)
           {
             slowballspeedY *= -1;  
           }

            // check to slow hit the side wall and chage direction

            if(slow.X < 55 || slow.X > 395 - slow.Width)
            {
              slowballspeedX *= -1;
            }


            // if the player reacts to the point
            if (player1.IntersectsWith(point))
            {
                player1Speed++;
                player1Score++;
                p1ScoreLabel.Text = $"{player1Score}";

                point.X = randGen.Next(50, 345);
                point.Y = randGen.Next(60, 360);

                SoundPlayer player = new SoundPlayer(Properties.Resources.good_square);

                player.Play();
            }

            if (player2.IntersectsWith(point))
            {
                player2Speed++;
                player2Score++;
                p2ScoreLabel.Text = $"{player2Score}";

                point.X = randGen.Next(50, 345);
                point.Y = randGen.Next(60, 360);

                SoundPlayer player = new SoundPlayer(Properties.Resources.good_square);

                player.Play();
            }
            // if player goes over red dot
            if (player1.IntersectsWith(slow))
            {
                player1Speed--;
                player1Score--;
                p1ScoreLabel.Text = $"{player1Score}";

                slow.X = randGen.Next(50, 345);
                slow.Y = randGen.Next(60, 360);

                SoundPlayer player = new SoundPlayer(Properties.Resources.bad_squre);

                player.Play();
            }

            if (player2.IntersectsWith(slow))
            {
                player2Speed--;
                player2Score--;
                p2ScoreLabel.Text = $"{player2Score}";

                slow.X = randGen.Next(50, 345);
                slow.Y = randGen.Next(60, 360);

                SoundPlayer player = new SoundPlayer(Properties.Resources.bad_squre);

                player.Play();
            }

            //if player has 5 points they win

            if (player1Score == 5)
            {
                timertick.Enabled = false;
                winLabel.Visible = true;
                winLabel.Text = "player 1 wins";

                SoundPlayer player = new SoundPlayer(Properties.Resources.win_sound);

                player.Play();
            }
            if (player2Score == 5)
            {
                timertick.Enabled = false;
                winLabel.Visible = true;
                winLabel.Text = "player 2 wins";

                SoundPlayer player = new SoundPlayer(Properties.Resources.win_sound);

                player.Play();
            }

            Refresh();

        }
        public Form1()
        {
            InitializeComponent();
        }
    }
}
