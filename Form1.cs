using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarSurvivalGame
{
    public partial class Form1 : Form
    {
        // Global Variables used
        int CarSpeed = 5; // this is the speed in which the Player Car moves
        int RoadSpeed = 5; // The rate in which the road moves
        bool CarLeft; // Used to move the car left
        bool CarRight; // Used to move the car right
        int TrafficSpeed; // Determines how fast the traffic moveds
        int score; // 
        Random rnd = new Random();

        public Form1()
        {
            InitializeComponent();
            ResetButton();
        }
        private void ResetButton() // This function is used to reset or start a new game
        {
            trophy.Visible = false; //  Hides the trophy image when game starts
            StartButton.Enabled = false; // Disables the start button when the game is running.
            explosion.Visible = false; // Hides the explosion image.
            TrafficSpeed = 5; // this will set the traffic speed back to default
            RoadSpeed = 5; // this will set the road speed back to default
            score = 0; // reset score to zero

            // Reset player position
            player.Left = 161;
            player.Top = 286;

            CarLeft = false;
            CarRight = false;

            // Resets the enemy cars to default position:
            Enemy1.Left = 66;
            Enemy1.Top = -120;

            Enemy2.Left = 294;
            Enemy2.Top = -185;

            // Reset Road to default position
            roadTrack2.Left = -3;
            roadTrack2.Top = -222;
            roadTrack1.Left = -2;
            roadTrack1.Top = -638;

            //starts the game timer
            timer1.Start();
            

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            score++; // Increases our score, the longer we survive
            DistanceTrav.Text = " " + score; // Shows our score on the distance label

            roadTrack1.Top += RoadSpeed;
            roadTrack2.Top += RoadSpeed;

            // MOVING THE ROAD:
            // The following if statements are used to move the road track. If the road tracks are moved beyond a certain 
            // point, they will reset their position, creating an "illusion", or simply simulating an animation
            if (roadTrack1.Top > 630)
            {
                roadTrack1.Top = -630;
            }
            if (roadTrack2.Top > 630)
            {
                roadTrack2.Top = -630;
            }

            // CAR MOVEMENT:
            if (CarLeft) { player.Left -= CarSpeed; } // Moves the player to the left, CarLeft is true
            if (CarRight) { player.Left += CarSpeed; } // Moves the player to the left, CarRight is true

            // CHECKING BOUNDRIES: Used to ensure the player does not move offscreen
            if (player.Left < 1)
            {
                CarLeft = false; // Prevents the car from going offscren
            }
            else if (player.Left + player.Width > 380)
            {
                CarRight = false;
            }

            // ENEMY CARS MOVEMENTS:
            Enemy1.Top += TrafficSpeed;
            Enemy2.Top += TrafficSpeed;

            // ENEMEY RESPAWN and IMAGE CHANGING:

            // once the enemy car reaches the top of the panel, they will respawn
            if (Enemy1.Top > gamePanel.Height)
            {
                changeEnemy1(); // change the enemy car images, upon exiting the screen. Calls a function
                Enemy1.Left = rnd.Next(2, 160); // Gives them a random placement, on the left side when they respawn
                Enemy1.Top = rnd.Next(100, 200) * -1; // Gives a random placement, on the top left of the screen
            }

            if (Enemy2.Top > gamePanel.Height)
            { 
                changeEnemy2(); // change the enemy car images, upon exiting the screen. Calls a function
                Enemy2.Left = rnd.Next(185, 327); // Gives them a random placement, on the right side when they respawn
                Enemy2.Top = rnd.Next(100, 200) * -1; // Gives a random placement, on the top right of the screen
            }

            // GAME OVER CONDITION / COLLISION:
            if (player.Bounds.IntersectsWith(Enemy1.Bounds) || player.Bounds.IntersectsWith(Enemy2.Bounds))
            {
                GameOver(); // Triggeres Game Over function when the player touches an enemy car
            }

            // SPEEDING UP TRAFFIC:
            if (score > 1200 && score <2000) // If the score is over 100, but below 500
            {
                TrafficSpeed = 6;
                RoadSpeed = 7;
                MPH.Text = "60 ";
            }
            else if (score > 2000 && score < 2750) // If the score is over 500, but below 1000
            {
                TrafficSpeed = 7;
                RoadSpeed = 8;
                MPH.Text = "70 ";
            }
            else if (score > 2750) // If the score is over 1200
            {
                TrafficSpeed = 9;
                RoadSpeed = 10;
                MPH.Text = "80 ";
            }
        }

        private void moveCar(object sender, KeyEventArgs e)
        {
            // LEFT KEY: If the player has pressed the left key, then the CarLeft boolean is true.
            if(e.KeyCode == Keys.Left && player.Left > 0) 
            {
                CarLeft = true;
            }
            // RIGHT KEY: If the player has pressed the right key, then the CarRight boolean is true.
            if(e.KeyCode == Keys.Right && player.Left + player.Width < gamePanel.Width)
            {
                CarRight = true;
            }
        }

        private void stopCar(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) // If the LEFT key is up, set CarLeft to false
            {
                CarLeft = false;
            }
            if (e.KeyCode == Keys.Right) // If the LEFT key is up, set CarLeft to false
            {
                CarRight = false;
            }
        }
        private void changeEnemy1()
        {
            int num = rnd.Next(1, 8); // Generates a random number between 1 through 8
            // With a switch statement, we can generate another car image, based on the number given.

            switch(num) // Will check which number has been generated by the rnd, and assign an image based on a number.
            {
                case 1:
                    Enemy1.Image = Properties.Resources.Black_Car1;
                    break;
                case 2:
                    Enemy1.Image = Properties.Resources.BlueCar1;
                    break;
                case 3:
                    Enemy1.Image = Properties.Resources.bluecar2;
                    break;
                case 4:
                    Enemy1.Image = Properties.Resources.Firefighter1;
                    break;
                case 5:
                    Enemy1.Image = Properties.Resources.Orange_Car1;
                    break;
                case 6:
                    Enemy1.Image = Properties.Resources.PoliceCar1;
                    break;
                case 7:
                    Enemy1.Image = Properties.Resources.PoliceCar21;
                    break;
                case 8:
                    Enemy1.Image = Properties.Resources.RedCar1;
                    break;
                default:
                    break;

            }
        }

        private void changeEnemy2()
        {
            int num = rnd.Next(1, 8); // Generates a random number between 1 through 8
            // With a switch statement, we can generate another car image, based on the number given.

            switch (num) // Will check which number has been generated by the rnd, and assign an image based on a number.
            {
                case 1:
                    Enemy2.Image = Properties.Resources.Black_Car1;
                    break;
                case 2:
                    Enemy2.Image = Properties.Resources.BlueCar1;
                    break;
                case 3:
                    Enemy2.Image = Properties.Resources.bluecar2;
                    break;
                case 4:
                    Enemy2.Image = Properties.Resources.Firefighter1;
                    break;
                case 5:
                    Enemy2.Image = Properties.Resources.Orange_Car1;
                    break;
                case 6:
                    Enemy2.Image = Properties.Resources.PoliceCar1;
                    break;
                case 7:
                    Enemy2.Image = Properties.Resources.PoliceCar2;
                    break;
                case 8:
                    Enemy2.Image = Properties.Resources.RedCar1;
                    break;
                default:
                    break;
            }
        }

        private void GameOver()
        {
            trophy.Visible = true;
            timer1.Stop();
            StartButton.Enabled = true;

            explosion.Visible = true;
            player.Controls.Add(explosion);
            explosion.Location = new Point(-8, -5);
            explosion.BackColor = Color.Transparent;
            explosion.BringToFront();

            if (score < 500)
            {
                trophy.Image = Properties.Resources.OneStar;
            }
            else if (score > 1000)
            {
                trophy.Image = Properties.Resources.TwoStar;
            }
            else if (score > 1500)
            {
                trophy.Image = Properties.Resources.ThreeStar;
            }
            else if (score > 2500)
            {
                trophy.Image = Properties.Resources.FourStar;
            }
            else if (score > 3000)
            {
                trophy.Image = Properties.Resources.FiveStar;
            }

        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            ResetButton();
        }
    }
   
}
