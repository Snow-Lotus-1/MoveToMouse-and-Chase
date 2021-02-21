using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MouseMovementTest
{
    public partial class Form1 : Form
    {
        //Basic 
        Rectangle playerBox, enemyBox;
        Point cursorLocation;
        double cursorX, cursorY;

        public Form1()
        {
            InitializeComponent();

            //create playerBox
            playerBox = new Rectangle(100, 100, 100, 100);

            //create enemyBox
            enemyBox = new Rectangle(0, 0, 100, 100);
        }

        private void tmrMove_Tick(object sender, EventArgs e)
        {            
            //Checks if the playerBox is at the cursor location
            if ((playerBox.X + (playerBox.Width/2)) == (int)cursorX && (playerBox.Y + (playerBox.Height / 2)) == (int)cursorY)
            {
                tmrMove.Enabled = false;//if the playerBox is at the cursor location, disable timer to stop movement 
            }

            //if player is left of cursor more 10 pixals right
            if ((playerBox.X + (playerBox.Width / 2)) < cursorX)
            {
                playerBox.X = playerBox.X + 10;
            }
            //if player is right of cursor more 10 pixals left
            else if ((playerBox.X + (playerBox.Width / 2)) > cursorX)
            {
                playerBox.X = playerBox.X - 10;
            }

            //if player is above of cursor more 10 pixals down
            if ((playerBox.Y + (playerBox.Height / 2)) < cursorY)
            {
                playerBox.Y = playerBox.Y + 10;
            }
            //if player is below of cursor more 10 pixals up
            else if ((playerBox.Y + (playerBox.Height / 2)) > cursorY)
            {
                playerBox.Y = playerBox.Y - 10;
            }

            //refreshes rectangle location
            Refresh();
        }

        //enemy movement system 
        private void tmrEnemyMove_Tick(object sender, EventArgs e)
        {
            //if enemy is on the left side of player...
            if (enemyBox.X < playerBox.X)
            {
                enemyBox.X = enemyBox.X + 5;//move enemy 5 pixals right
            }
            //if enemy is on the right side of player...
            else if (enemyBox.X > playerBox.X)
            {
                enemyBox.X = enemyBox.X - 5;//move enemy 5 pixals left
            }

            //if enemy is on top of player...
            if (enemyBox.Y < playerBox.Y)
            {
                enemyBox.Y = enemyBox.Y + 5;//move enemy 5 pixals down
            }
            //if enemy is below the of player...
            else if (enemyBox.Y > playerBox.Y)
            {
                enemyBox.Y = enemyBox.Y - 5;//move enemy 5 pixals up
            }

            //refreshes rectangle location
            Refresh();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            //have cursorLocation be the location of cursors X and Y
            cursorLocation = e.Location;

            //split cursorLocation into cursorX and cursorY, in other words the cursor's X and Y location
            cursorX = cursorLocation.X;
            cursorY = cursorLocation.Y;            

            //*We want the cursor values to be rounded to the nearest tenth*

            //divide cursor X and Y by 100 to create decimals for rounding
            cursorX = cursorX / 100;
            cursorY = cursorY / 100;

            //rounds both cursor X and Y to first decimal place then muliply by 100
            cursorX = Math.Round(cursorX, 1) * 100;
            cursorY = Math.Round(cursorY, 1) * 100;

            //create temp cursor value to refrence orginal ones to later
            double tempCursorX = cursorX;
            double tempCursorY = cursorY;

            //ceiling the cursor value to make sure if there were any problems with rounding (EX: 9.999999) it is fixed
            //however, may lead to other problems (EX: 10.9)
            cursorX = Math.Ceiling(cursorX);
            cursorY = Math.Ceiling(cursorY);

          


            //have cursor values display to labels to check values during testing
            lblCursorX.Text = cursorX.ToString();
            lblCursorY.Text = cursorY.ToString();

            //enable timer to let the player move
            tmrMove.Enabled = true;
        }        
       
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            //have playerBox be green
            e.Graphics.FillRectangle(Brushes.Green, playerBox);

            //have enemyBox be red
            e.Graphics.FillRectangle(Brushes.Red, enemyBox);
        }
    }
}
