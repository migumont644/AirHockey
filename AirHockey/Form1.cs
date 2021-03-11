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

// Miguel / Mar 11, 2021 / An Air Hockey program game
namespace AirHockey
{
    public partial class airHockeyTitle : Form
    {
        int strikers1X = 215;
        int strikers1Y = 150;
        int player1Score = 0;

        int strikers2X = 215;
        int strikers2Y = 590;
        int player2Score = 0;

        int strikersWidth = 50;
        int strikersHeight = 50;
        int strikersSpeed = 8;

        int puckX = 215;
        int puckY = 390;
        int puckXSpeed = 6;
        int puckYSpeed = 6;
        int puckWidth = 30;
        int puckHeight = 30;

        bool wDown = false;
        bool sDown = false;
        bool upArrowDown = false;
        bool downArrowDown = false;
        bool aDown = false;
        bool dDown = false;
        bool leftArrowDown = false;
        bool rightArrowDown = false;

        SoundPlayer scoreSound = new SoundPlayer(Properties.Resources.computerError);
        SoundPlayer stikerHitSound = new SoundPlayer(Properties.Resources.doorBell);

        Pen whitePen = new Pen(Color.White, 10);
        SolidBrush blueBrush = new SolidBrush(Color.DodgerBlue);
        SolidBrush redBrush = new SolidBrush(Color.Red);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        public airHockeyTitle()
        {
            InitializeComponent();
        }

        private void AirHockeyTitle_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = true;
                    break;
                case Keys.S:
                    sDown = true;
                    break;
                case Keys.A:
                    aDown = true;
                    break;
                case Keys.D:
                    dDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
            }
        }

        private void AirHockeyTitle_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = false;
                    break;
                case Keys.S:
                    sDown = false;
                    break;
                case Keys.A:
                    aDown = false;
                    break;
                case Keys.D:
                    dDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
            }
        }

        private void AirHockeyTitle_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(whitePen, 485, 400, -5, 400);
            e.Graphics.DrawArc(whitePen, 140, -80, 200, 200, 300, 300);
            e.Graphics.DrawArc(whitePen, 140, 680, 200, 200, 120, 300);
            e.Graphics.DrawEllipse(whitePen, 150, 310, 180, 180);

            e.Graphics.DrawLine(whitePen, 140, 5, 340, 5);
            e.Graphics.DrawLine(whitePen, 140, 795, 340, 795);

            e.Graphics.FillRectangle(whiteBrush, puckX, puckY, puckWidth, puckHeight);
            e.Graphics.FillRectangle(blueBrush, strikers1X, strikers1Y, strikersWidth, strikersHeight);
            e.Graphics.FillRectangle(redBrush, strikers2X, strikers2Y, strikersWidth, strikersHeight);        
          

        }

        private void GamerTimer_Tick(object sender, EventArgs e)
        {
            //move puck
            puckX += puckXSpeed;
            puckY += puckYSpeed;

            //move player 1 
            if (wDown == true && strikers1Y > 0)
            {
                strikers1Y -= strikersSpeed;
            }

            if (sDown == true && strikers1Y < this.Height / 2 - strikersHeight)
            {
                strikers1Y += strikersSpeed;
            }

            if (aDown == true && strikers1X > 0)
            {
                strikers1X -= strikersSpeed;
            }

            if (dDown == true && strikers1X < this.Width - strikersWidth)
            {
                strikers1X += strikersSpeed;
            }

            //move player 2 
            if (upArrowDown == true && strikers2Y > this.Height / 2)
            {
                strikers2Y -= strikersSpeed;
            }

            if (downArrowDown == true && strikers2Y < this.Height - strikersHeight)
            {
                strikers2Y += strikersSpeed;
            }

            if (leftArrowDown == true && strikers2X > 0)
            {
                strikers2X -= strikersSpeed;
            }

            if (rightArrowDown == true && strikers2X < this.Width - strikersWidth)
            {
                strikers2X += strikersSpeed;
            }

            //Check for puck collision with top/bottom
            if (puckY < 0 || puckY > this.Height - puckHeight)
            {
                puckYSpeed *= -1; 
            }

            //create Rectangles of objects on screen to be used for collision detection 
            Rectangle player1Rec = new Rectangle(strikers1X, strikers1Y, strikersWidth, strikersHeight);
            Rectangle player2Rec = new Rectangle(strikers2X, strikers2Y, strikersWidth, strikersHeight);
            Rectangle ballRec = new Rectangle(puckX+5, puckY, puckWidth-10, puckHeight);
            Rectangle ballLeftRec = new Rectangle(puckX, puckY, 2, puckHeight);
            Rectangle ballRightRec = new Rectangle(puckX + 28, puckY, 2, puckHeight);

            Rectangle player1Net = new Rectangle(140, 5, 200, 5);
            Rectangle player2Net = new Rectangle(140, 795, 200, 5);

            //check if ball hits either strikers. If it does change the direction 
            //and place the ball in front of the striker hit 
            if (player1Rec.IntersectsWith(ballRec) && puckY > strikers1Y)
            {
                stikerHitSound.Play();
                puckYSpeed *= -1;
                puckY = strikers1Y + strikersHeight + 1;
            }
            else if (player1Rec.IntersectsWith(ballRec) && puckY < strikers1Y)
            {
                stikerHitSound.Play();
                puckYSpeed *= -1;
                puckY = strikers1Y - strikersHeight - 1;
            }
            else if (player1Rec.IntersectsWith(ballLeftRec))
            {
                stikerHitSound.Play();
                puckXSpeed *= -1;
                puckX = strikers1X + strikersWidth + 1;              
            }
            else if (player1Rec.IntersectsWith(ballRightRec))
            {
                stikerHitSound.Play();
                puckXSpeed *= -1;
                puckX = strikers1X - strikersWidth - 1;
            }

            else if (player2Rec.IntersectsWith(ballRec) && puckY > strikers2Y)
            {
                stikerHitSound.Play();
                puckYSpeed *= -1;
                puckY = strikers2Y + strikersHeight + 1;
            }
            else if (player2Rec.IntersectsWith(ballRec) && puckY < strikers2Y)
            {
                stikerHitSound.Play();
                puckYSpeed *= -1;
                puckY = strikers2Y - strikersHeight - 1;
            }
            else if (player2Rec.IntersectsWith(ballLeftRec))
            {
                stikerHitSound.Play();
                puckXSpeed *= -1;
                puckX = strikers2X + strikersWidth + 1;
            }
            else if (player2Rec.IntersectsWith(ballRightRec))
            {
                stikerHitSound.Play();
                puckXSpeed *= -1;
                puckX = strikers2X - strikersWidth - 1;
            }

            // check if either player scores a poin
            ballRec = new Rectangle(puckX, puckY, puckWidth, puckHeight);
            if (puckX < 0)
            {
                puckXSpeed *= -1;
                puckX = 0 + puckWidth + 1;
            }
            else if (puckX > 460)
            {
                puckXSpeed *= -1;
                puckX = 460 - puckWidth - 1;
            }
            else if (player1Net.IntersectsWith(ballRec))
            {
                scoreSound.Play();

                player2Score++;

                p2ScoreLabel.Text = $"{player2Score}";

                puckX = 215;
                puckY = 390;

                strikers1Y = 150;
                strikers2Y = 590;

                strikers1X = 215;
                strikers2X = 215;

                puckYSpeed *= -1;
            }
            else if (player2Net.IntersectsWith(ballRec))
            {
                scoreSound.Play();

                player1Score++;

                p1ScoreLabel.Text = $"{player1Score}";

                puckX = 215;
                puckY = 390;

                strikers1Y = 150;
                strikers2Y = 590;

                strikers1X = 215;
                strikers2X = 215;

                puckYSpeed *= -1;
            }

            //Check if either player won
            if (player1Score == 3 || player2Score == 3)
            {
                gameTimer.Enabled = false;
            }

            Refresh();
        }
     }
  }

