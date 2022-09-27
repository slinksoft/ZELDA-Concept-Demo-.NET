using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ZeldaDemo
{
    public partial class game : Form
    {
        bool up, left, down, right;
        bool warped;
        bool courage;
        bool wisdom;
        bool power;
        bool gotsword;
        bool shootbeam;
        bool opend1;
        bool opens1;
        bool victory;
        bool attacking; // prevents movement while swinging sword
        int roomid;
        int health;
        int invinlimit;
        bool boomerangthrown;
        bool invincibility;
        int boomerangdirection; // 1 = up 2 = down 3 = left 4 = right
        int beamdirection; // 1 = up 2 = down 3 = left 4 = right
        int boomlimit;
        bool bombplaced;
        int bomblimit;
        int beamlimit;
        bool gotsecret;
        bool healthlow;
        bool gameover;
        int incave; // overworld bgm fix for exiting cave
        int warptoa1from; // value 1 = from sword cave ; value 2 from area 3 ; value 3 from shop 1 (USE FOR AREA 1 ONLY!!!)
        int warptoa3from; // value 1 = from area 1 ; value 2 = d1 (USE FOR AREA 3 ONLY!!!)
        int warptod1from; // value 1 = area 3 ; value 2 = d2 (USE FOR D1 ONLY!!!)
        int warptod2from;// value 1 = d1 ; value 2 = d3 (USE FOR D2 ONLY!!!)
        int bombcount;
        int rupeecount;
        bool gotboomerang;
        bool gettingtriforce;
        int currentpointerPlace;
        Point shieldCollsioncoords;
        int[] orHealth = new int[2];
        int[] orsystem = new int[2];
        int[] skelHealth = new int [3];
        int[] skelsystem = new int[3];
        int[] orrocksystem = new int[2];
        int[] benHealth = new int[2];
        int[] bensystem = new int[2];
        int[] benboomsystem = new int[2];
        int bossHealth;
        int bossystem;
        int bossflaresystem;
        int knightHealth;
        int knightSystem;
        bool swingdelay;
        System.Media.SoundPlayer swing = new System.Media.SoundPlayer("sfx/slashsfx.wav");
        System.Media.SoundPlayer hit = new System.Media.SoundPlayer("sfx/hit.wav");
        System.Media.SoundPlayer kill = new System.Media.SoundPlayer("sfx/kill.wav");
        System.Media.SoundPlayer deflect = new System.Media.SoundPlayer("sfx/deflect.wav");
        System.Media.SoundPlayer boomerangthrow = new System.Media.SoundPlayer("sfx/boomerangthrow.wav");
        System.Media.SoundPlayer bombdrop = new System.Media.SoundPlayer("sfx/bombdrop.wav");
        System.Media.SoundPlayer bombblow = new System.Media.SoundPlayer("sfx/bombblow.wav");
        System.Media.SoundPlayer getitem = new System.Media.SoundPlayer("sfx/getitem.wav");
        System.Media.SoundPlayer getrupee = new System.Media.SoundPlayer("sfx/getrupee.wav");
        System.Media.SoundPlayer beamsfx = new System.Media.SoundPlayer("sfx/beamsfx.wav");
        System.Media.SoundPlayer hurt = new System.Media.SoundPlayer("sfx/hurt.wav");
        System.Media.SoundPlayer lowhealth = new System.Media.SoundPlayer("sfx/lowhealth.wav");
        System.Media.SoundPlayer die = new System.Media.SoundPlayer("sfx/die.wav");
        System.Media.SoundPlayer secret = new System.Media.SoundPlayer("sfx/secret.wav");
        System.Media.SoundPlayer getheart = new System.Media.SoundPlayer("sfx/heart.wav");
        System.Media.SoundPlayer gettriforcee = new System.Media.SoundPlayer("sfx/gettriforce.wav");
        System.Media.SoundPlayer bossscream = new System.Media.SoundPlayer("sfx/bossscream.wav");
        System.Media.SoundPlayer titlebgm = new System.Media.SoundPlayer("sfx/titlebgm.wav");

        public game()
        {
            InitializeComponent();
            up = false;
            down = false;
            left = false;
            right = false;
            warped = false;
            gotsword = false;
            gotboomerang = false;
            gotsecret = false;
            courage = false;
            opend1 = false;
            bombcount = 0;
            health = 3;
            rupeecount = 0;
            currentpointerPlace = 0;
            swingdelay = false;
            gameover = true;
            victory = false;
            orHealth[0] = -42013;
            orHealth[1] = -42013;
            skelHealth[0] = -42013;
            skelHealth[1] = -42013;
            skelHealth[2] = -42013;
            benHealth[0] = -42013;
            benHealth[1] = -42013;
            knightHealth = -42013;
            bossHealth = -42013;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W && attacking == false && inventory.Visible == false && gameover == false)
            {
                up = true;
            }

            if (e.KeyCode == Keys.S && attacking == false && inventory.Visible == false && gameover == false)
            {
                down = true;
            }

            if (e.KeyCode == Keys.A && attacking == false && inventory.Visible == false && gameover == false)
            {
                left = true;
            }

            if (e.KeyCode == Keys.D && attacking == false && inventory.Visible == false && gameover == false)
            {
                right = true;
            }

            #region inventory function
            if (e.KeyCode == Keys.V && inventory.Visible == false && swingdelay == false && upani.Visible == false && downani.Visible == false && rightani.Visible == false && leftani.Visible == false && gameover == false && gettingtriforce == false)
            {
                inventory.Location = new System.Drawing.Point(130, 113);
                inventory.Visible = true;
                MoveandAniTimer.Stop();
                boomTimer.Stop();
                bombtimer.Stop();
                orTimer.Stop();
                skelTimer.Stop();
                boomerangthrow.Stop();
                beamTimer.Stop();
                benTimer.Stop();
                bossTimer.Stop();

            }
            else if (e.KeyCode == Keys.V && inventory.Visible == true && swingdelay == false && upani.Visible == false && downani.Visible == false && rightani.Visible == false && leftani.Visible == false && gameover == false && gettingtriforce == false)
            {
                inventory.Visible = false;
                inventory.Location = new System.Drawing.Point(10000, 10000);
                MoveandAniTimer.Start();
                if (boomerangthrown == true)
                {
                    boomTimer.Start();
                }

                if (bombplaced == true)
                {
                    bombtimer.Start();
                }

                if (shootbeam == true)
                {
                    beamTimer.Start();
                }
                if (orHealth[0] >= 1 || orHealth[1] >= 1)
                {
                    orTimer.Start();
                }

                if (skelHealth[0] >= 1 || skelHealth[1] >= 1 || skelHealth[2] >= 2)
                {
                    skelTimer.Start();
                }

                if (benHealth[0] >= 1 || benHealth[1] >= 1 || knightHealth >= 1)
                {
                    benTimer.Start();
                }

                if (bossHealth >= 1)
                {
                    bossTimer.Start();
                }
            }

            if (e.KeyCode == Keys.B && inventory.Visible == true && gameover == false)
            {
                if (currentpointerPlace == 0)
                {
                    currentpointerPlace = 1;
                }
                else if (currentpointerPlace == 1)
                {
                    currentpointerPlace = 0;
                }

                getrupee.Play();
            }
            #endregion


            #region shop buying function

            if (e.KeyCode == Keys.M && attacking == false && inventory.Visible == false && gameover == false)
            {
                if (upidle.Bounds.IntersectsWith(buyHeart.Bounds))
                {
                    if (rupeecount >= 10)
                    {
                        rupeecount -= 10;
                        if (health <= 2)
                        {
                            health += 1;
                        }
                        getheart.Play();
                    }
                    else if (rupeecount < 10)
                    {
                        MessageBox.Show("You do not have enough Rupees!", "Not Enough!");
                    }
                }
                else if (upidle.Bounds.IntersectsWith(buyBombs.Bounds))
                {
                    if (rupeecount >= 28)
                    {
                        rupeecount -= 28;
                        bombcount += 5;
                        getitem.Play();
                    }
                    else if (rupeecount < 28)
                    {
                        MessageBox.Show("You do not have enough Rupees!", "Not Enough!");
                    }
                }

            }
            #endregion


            if (e.KeyCode == Keys.N && gameover == false)
            {
                if (currentpointerPlace == 1 && bombcount >= 1 && bombplaced == false && swingdelay == false && upani.Visible == false && downani.Visible == false && rightani.Visible == false && leftani.Visible == false && inventory.Visible == false && gettingtriforce == false)
            {
                bombcount -= 1;
                bombCountDisplay.Text = Convert.ToString(bombcount);
                bombplaced = true;
                bomblimit = 0;
                bomb.Visible = true;

                if (leftidle.Visible == true)
                {
                    bomb.Location = upidle.Location;
                    bomb.Left -= 20;
                    bomb.Top += 2;
                }
                else if (rightidle.Visible == true)
                {
                    bomb.Location = upidle.Location;
                    bomb.Left += 30;
                    bomb.Top += 2;

                }
                else if (downidle.Visible == true)
                {
                    bomb.Location = upidle.Location;
                    bomb.Left += 2;
                    bomb.Top += 28;
                }
                else if (upidle.Visible == true)
                {
                    bomb.Location = upidle.Location;
                    bomb.Left += 2;
                    bomb.Top -= 28;
                }

                bombdrop.Play();
                bombtimer.Start();
            }

                else if (currentpointerPlace == 0 && boomerangthrown == false && gotboomerang == true && swingdelay == false && upani.Visible == false && downani.Visible == false && rightani.Visible == false && leftani.Visible == false && inventory.Visible == false && gettingtriforce == false)
            {
                boomerangthrown = true;
                boomlimit = 0;
                boomerang.Visible = true;

                if (leftidle.Visible == true)
                {
                    boomerangdirection = 3;
                    boomerang.Location = upidle.Location;
                    boomerang.Left -= 20;
                    boomerang.Top += 10;
                }
                else if (rightidle.Visible == true)
                {
                    boomerangdirection = 4;
                    boomerang.Location = upidle.Location;
                    boomerang.Left += 30;
                    boomerang.Top += 10;

                }
                else if (downidle.Visible == true)
                {
                    boomerangdirection = 2;
                    boomerang.Location = upidle.Location;
                    boomerang.Left += 2;
                    boomerang.Top += 32;
                }
                else if (upidle.Visible == true)
                {
                    boomerangdirection = 1;
                    boomerang.Location = upidle.Location;
                    boomerang.Left += 2;
                    boomerang.Top -= 14;
                }


                boomTimer.Start();
                boomerangthrow.PlayLooping();
            }
            }



            if (gotsword == true && e.KeyCode == Keys.Space && swingdelay == false && upani.Visible == false && downani.Visible == false && rightani.Visible == false && leftani.Visible == false && inventory.Visible == false && gettingtriforce == false && gameover == false)
            {
                swingdelay = true;
                attacking = true;
                swing.Play();

                if (leftidle.Visible == true)
                {
                    swingleft.Visible = true;
                    swingdown.Visible = false;
                    swingright.Visible = false;
                    swingup.Visible = false;
                    leftidle.Visible = false;
                    attackCollision.Location = upidle.Location;
                    attackCollision.Left -= 20;
                    attackCollision.Top += 10;
                }
                else if (rightidle.Visible == true)
                {
                    swingleft.Visible = false;
                    swingdown.Visible = false;
                    swingright.Visible = true;
                    swingup.Visible = false;
                    rightidle.Visible = false;
                    attackCollision.Location = upidle.Location;
                    attackCollision.Left += 30;
                    attackCollision.Top += 10;

                }
                else if (downidle.Visible == true)
                {
                    swingleft.Visible = false;
                    swingdown.Visible = true;
                    swingright.Visible = false;
                    swingup.Visible = false;
                    downidle.Visible = false;
                    attackCollision.Location = upidle.Location;
                    attackCollision.Left += 2;
                    attackCollision.Top += 32;
                }
                else if (upidle.Visible == true)
                {
                    swingleft.Visible = false;
                    swingdown.Visible = false;
                    swingright.Visible = false;
                    swingup.Visible = true;
                    upidle.Visible = false;
                    attackCollision.Location = upidle.Location;
                    attackCollision.Left += 2;
                    attackCollision.Top -= 14;
                }

                if (shootbeam == false && health >= 3)
                {
                    beamlimit = 0;
                    shootbeam = true;
                    if (swingleft.Visible == true)
                    {
                        beamdirection = 3;
                        swordbeamleft.Visible = true;
                        swordbeamdown.Visible = false;
                        swordbeamright.Visible = false;
                        swordbeamup.Visible = false;
                        swordbeamleft.Location = upidle.Location;
                        swordbeamleft.Left -= 20;
                        swordbeamleft.Top += 10;
                    }

                    else if (swingright.Visible == true)
                    {
                        beamdirection = 4;
                        swordbeamleft.Visible = false;
                        swordbeamdown.Visible = false;
                        swordbeamright.Visible = true;
                        swordbeamup.Visible = false;
                        swordbeamright.Location = upidle.Location;
                        swordbeamright.Left += 20;
                        swordbeamright.Top += 10;
                    }

                    else if (swingdown.Visible == true)
                    {
                        beamdirection = 2;
                        swordbeamleft.Visible = false;
                        swordbeamdown.Visible = true;
                        swordbeamright.Visible = false;
                        swordbeamup.Visible = false;
                        swordbeamdown.Location = upidle.Location;
                        swordbeamdown.Left += 7;
                        swordbeamdown.Top += 20;
                    }

                    else  if (swingup.Visible == true)
                    {
                        beamdirection = 1;
                        swordbeamleft.Visible = false;
                        swordbeamdown.Visible = false;
                        swordbeamright.Visible = false;
                        swordbeamup.Visible = true;
                        swordbeamup.Location = upidle.Location;
                        swordbeamup.Left += 2;
                        swordbeamup.Top -= 14;
                    }

                    beamsfx.Play();
                    beamTimer.Start();

                    
                }
                swingtimer1.Start();
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                up = false;
                if (inventory.Visible == false)
                {
                    upani.Visible = false;
                    leftani.Visible = false;
                    rightani.Visible = false;
                    downani.Visible = false;
                    upidle.Visible = true;
                    leftidle.Visible = false;
                    downidle.Visible = false;
                    rightidle.Visible = false;
                }
            }

            if (e.KeyCode == Keys.S)
            {
                down = false;
                if (inventory.Visible == false)
                {
                    upani.Visible = false;
                    leftani.Visible = false;
                    rightani.Visible = false;
                    downani.Visible = false;
                    upidle.Visible = false;
                    leftidle.Visible = false;
                    downidle.Visible = true;
                    rightidle.Visible = false;
                }
            }

            if (e.KeyCode == Keys.A)
            {
                left = false;
                if (inventory.Visible == false)
                {
                    upani.Visible = false;
                    leftani.Visible = false;
                    rightani.Visible = false;
                    downani.Visible = false;
                    upidle.Visible = false;
                    leftidle.Visible = true;
                    downidle.Visible = false;
                    rightidle.Visible = false;
                }
            }

            if (e.KeyCode == Keys.D)
            {
                right = false;
                if (inventory.Visible == false)
                {
                    upani.Visible = false;
                    leftani.Visible = false;
                    rightani.Visible = false;
                    downani.Visible = false;
                    upidle.Visible = false;
                    leftidle.Visible = false;
                    downidle.Visible = false;
                    rightidle.Visible = true;
                }
            }
        }

        private void MoveandAniTimer_Tick(object sender, EventArgs e)
        {
            #region area1 collision



            if (upidle.Bounds.IntersectsWith(a1o1.Bounds))
            {
                if (upidle.Left < a1o1.Left && upidle.Right > a1o1.Left)
                {
                    right = false;
                }
                if (upidle.Left < a1o1.Right && upidle.Right > a1o1.Right)
                {
                    left = false;
                }
                if (upidle.Top < a1o1.Top && upidle.Bottom > a1o1.Top)
                {
                    down = false;
                }
                if (upidle.Top < a1o1.Bottom && upidle.Bottom > a1o1.Bottom)
                {
                    up = false;
                }
            }

            if (upidle.Bounds.IntersectsWith(a1o2.Bounds))
            {
                if (upidle.Left < a1o2.Left && upidle.Right > a1o2.Left)
                {
                    right = false;
                }
                if (upidle.Left < a1o2.Right && upidle.Right > a1o2.Right)
                {
                    left = false;
                }
                if (upidle.Top < a1o2.Top && upidle.Bottom > a1o2.Top)
                {
                    down = false;
                }
                if (upidle.Top < a1o2.Bottom && upidle.Bottom > a1o2.Bottom)
                {
                    up = false;
                }
            }

            if (upidle.Bounds.IntersectsWith(a1o3.Bounds))
            {
                if (upidle.Left < a1o3.Left && upidle.Right > a1o3.Left)
                {
                    right = false;
                }
                if (upidle.Left < a1o3.Right && upidle.Right > a1o3.Right)
                {
                    left = false;
                }
                if (upidle.Top < a1o3.Top && upidle.Bottom > a1o3.Top)
                {
                    down = false;
                }
                if (upidle.Top < a1o3.Bottom && upidle.Bottom > a1o3.Bottom)
                {
                    up = false;
                }
            }

            if (upidle.Bounds.IntersectsWith(a1o4.Bounds))
            {
                if (upidle.Left < a1o4.Left && upidle.Right > a1o4.Left)
                {
                    right = false;
                }
                if (upidle.Left < a1o4.Right && upidle.Right > a1o4.Right)
                {
                    left = false;
                }
                if (upidle.Top < a1o4.Top && upidle.Bottom > a1o4.Top)
                {
                    down = false;
                }
                if (upidle.Top < a1o4.Bottom && upidle.Bottom > a1o4.Bottom)
                {
                    up = false;
                }
            }

            if (upidle.Bounds.IntersectsWith(a1o5.Bounds))
            {
                if (upidle.Left < a1o5.Left && upidle.Right > a1o5.Left)
                {
                    right = false;
                }
                if (upidle.Left < a1o5.Right && upidle.Right > a1o5.Right)
                {
                    left = false;
                }
                if (upidle.Top < a1o5.Top && upidle.Bottom > a1o5.Top)
                {
                    down = false;
                }
                if (upidle.Top < a1o5.Bottom && upidle.Bottom > a1o5.Bottom)
                {
                    up = false;
                }
            }

            if (upidle.Bounds.IntersectsWith(a1o6.Bounds))
            {
                if (upidle.Left < a1o6.Left && upidle.Right > a1o6.Left)
                {
                    right = false;
                }
                if (upidle.Left < a1o6.Right && upidle.Right > a1o6.Right)
                {
                    left = false;
                }
                if (upidle.Top < a1o6.Top && upidle.Bottom > a1o6.Top)
                {
                    down = false;
                }
                if (upidle.Top < a1o6.Bottom && upidle.Bottom > a1o6.Bottom)
                {
                    up = false;
                }
            }

                if (upidle.Bounds.IntersectsWith(a1o7.Bounds))
                {
                    if (upidle.Left < a1o7.Left && upidle.Right > a1o7.Left)
                    {
                        right = false;
                    }
                    if (upidle.Left < a1o7.Right && upidle.Right > a1o7.Right)
                    {
                        left = false;
                    }
                    if (upidle.Top < a1o7.Top && upidle.Bottom > a1o7.Top)
                    {
                        down = false;
                    }
                    if (upidle.Top < a1o7.Bottom && upidle.Bottom > a1o7.Bottom)
                    {
                        up = false;
                    }
                }

                if (upidle.Bounds.IntersectsWith(a1o8.Bounds))
                {
                    if (upidle.Left < a1o8.Left && upidle.Right > a1o8.Left)
                    {
                        right = false;
                    }
                    if (upidle.Left < a1o8.Right && upidle.Right > a1o8.Right)
                    {
                        left = false;
                    }
                    if (upidle.Top < a1o8.Top && upidle.Bottom > a1o8.Top)
                    {
                        down = false;
                    }
                    if (upidle.Top < a1o8.Bottom && upidle.Bottom > a1o8.Bottom)
                    {
                        up = false;
                    }
                }

                if (upidle.Bounds.IntersectsWith(a1o9.Bounds))
                {
                    if (upidle.Left < a1o9.Left && upidle.Right > a1o9.Left)
                    {
                        right = false;
                    }
                    if (upidle.Left < a1o9.Right && upidle.Right > a1o9.Right)
                    {
                        left = false;
                    }
                    if (upidle.Top < a1o9.Top && upidle.Bottom > a1o9.Top)
                    {
                        down = false;
                    }
                    if (upidle.Top < a1o9.Bottom && upidle.Bottom > a1o9.Bottom)
                    {
                        up = false;
                    }
                }

                if (upidle.Bounds.IntersectsWith(a1o10.Bounds))
                {
                    if (upidle.Left < a1o10.Left && upidle.Right > a1o10.Left)
                    {
                        right = false;
                    }
                    if (upidle.Left < a1o10.Right && upidle.Right > a1o10.Right)
                    {
                        left = false;
                    }
                    if (upidle.Top < a1o10.Top && upidle.Bottom > a1o10.Top)
                    {
                        down = false;
                    }
                    if (upidle.Top < a1o10.Bottom && upidle.Bottom > a1o10.Bottom)
                    {
                        up = false;
                    }
                }




            

            #endregion

            #region area2 collision

                if (upidle.Bounds.IntersectsWith(a2o1.Bounds))
                {
                    if (upidle.Left < a2o1.Left && upidle.Right > a2o1.Left)
                    {
                        right = false;
                    }
                    if (upidle.Left < a2o1.Right && upidle.Right > a2o1.Right)
                    {
                        left = false;
                    }
                    if (upidle.Top < a2o1.Top && upidle.Bottom > a2o1.Top)
                    {
                        down = false;
                    }
                    if (upidle.Top < a2o1.Bottom && upidle.Bottom > a2o1.Bottom)
                    {
                        up = false;
                    }
                }


                if (upidle.Bounds.IntersectsWith(a2o2.Bounds))
                {
                    if (upidle.Left < a2o2.Left && upidle.Right > a2o2.Left)
                    {
                        right = false;
                    }
                    if (upidle.Left < a2o2.Right && upidle.Right > a2o2.Right)
                    {
                        left = false;
                    }
                    if (upidle.Top < a2o2.Top && upidle.Bottom > a2o2.Top)
                    {
                        down = false;
                    }
                    if (upidle.Top < a2o2.Bottom && upidle.Bottom > a2o2.Bottom)
                    {
                        up = false;
                    }
                }



                if (upidle.Bounds.IntersectsWith(a2o3.Bounds))
                {
                    if (upidle.Left < a2o3.Left && upidle.Right > a2o3.Left)
                    {
                        right = false;
                    }
                    if (upidle.Left < a2o3.Right && upidle.Right > a2o3.Right)
                    {
                        left = false;
                    }
                    if (upidle.Top < a2o3.Top && upidle.Bottom > a2o3.Top)
                    {
                        down = false;
                    }
                    if (upidle.Top < a2o3.Bottom && upidle.Bottom > a2o3.Bottom)
                    {
                        up = false;
                    }
                }


                if (upidle.Bounds.IntersectsWith(a2o4.Bounds))
                {
                    if (upidle.Left < a2o4.Left && upidle.Right > a2o4.Left)
                    {
                        right = false;
                    }
                    if (upidle.Left < a2o4.Right && upidle.Right > a2o4.Right)
                    {
                        left = false;
                    }
                    if (upidle.Top < a2o4.Top && upidle.Bottom > a2o4.Top)
                    {
                        down = false;
                    }
                    if (upidle.Top < a2o4.Bottom && upidle.Bottom > a2o4.Bottom)
                    {
                        up = false;
                    }
                }


                if (upidle.Bounds.IntersectsWith(a2o5.Bounds))
                {
                    if (upidle.Left < a2o5.Left && upidle.Right > a2o5.Left)
                    {
                        right = false;
                    }
                    if (upidle.Left < a2o5.Right && upidle.Right > a2o5.Right)
                    {
                        left = false;
                    }
                    if (upidle.Top < a2o5.Top && upidle.Bottom > a2o5.Top)
                    {
                        down = false;
                    }
                    if (upidle.Top < a2o5.Bottom && upidle.Bottom > a2o5.Bottom)
                    {
                        up = false;
                    }
                }

                 #endregion

            #region area3 collision

                if (upidle.Bounds.IntersectsWith(a3o1.Bounds))
                {
                    if (upidle.Left < a3o1.Left && upidle.Right > a3o1.Left)
                    {
                        right = false;
                    }
                    if (upidle.Left < a3o1.Right && upidle.Right > a3o1.Right)
                    {
                        left = false;
                    }
                    if (upidle.Top < a3o1.Top && upidle.Bottom > a3o1.Top)
                    {
                        down = false;
                    }
                    if (upidle.Top < a3o1.Bottom && upidle.Bottom > a3o1.Bottom)
                    {
                        up = false;
                    }
                }

                if (upidle.Bounds.IntersectsWith(warpD1.Bounds))
                {
                    if (upidle.Left < warpD1.Left && upidle.Right > warpD1.Left)
                    {
                        right = false;
                    }
                    if (upidle.Left < warpD1.Right && upidle.Right > warpD1.Right)
                    {
                        left = false;
                    }
                    if (upidle.Top < warpD1.Top && upidle.Bottom > warpD1.Top)
                    {
                        down = false;
                    }
                    if (upidle.Top < warpD1.Bottom && upidle.Bottom > warpD1.Bottom)
                    {
                        up = false;
                    }
                }

                if (upidle.Bounds.IntersectsWith(a3o2.Bounds))
                {
                    if (upidle.Left < a3o2.Left && upidle.Right > a3o2.Left)
                    {
                        right = false;
                    }
                    if (upidle.Left < a3o2.Right && upidle.Right > a3o2.Right)
                    {
                        left = false;
                    }
                    if (upidle.Top < a3o2.Top && upidle.Bottom > a3o2.Top)
                    {
                        down = false;
                    }
                    if (upidle.Top < a3o2.Bottom && upidle.Bottom > a3o2.Bottom)
                    {
                        up = false;
                    }
                }

                if (upidle.Bounds.IntersectsWith(a3o3.Bounds))
                {
                    if (upidle.Left < a3o3.Left && upidle.Right > a3o3.Left)
                    {
                        right = false;
                    }
                    if (upidle.Left < a3o3.Right && upidle.Right > a3o3.Right)
                    {
                        left = false;
                    }
                    if (upidle.Top < a3o3.Top && upidle.Bottom > a3o3.Top)
                    {
                        down = false;
                    }
                    if (upidle.Top < a3o3.Bottom && upidle.Bottom > a3o3.Bottom)
                    {
                        up = false;
                    }
                }

                if (upidle.Bounds.IntersectsWith(a3o4.Bounds))
                {
                    if (upidle.Left < a3o4.Left && upidle.Right > a3o4.Left)
                    {
                        right = false;
                    }
                    if (upidle.Left < a3o4.Right && upidle.Right > a3o4.Right)
                    {
                        left = false;
                    }
                    if (upidle.Top < a3o4.Top && upidle.Bottom > a3o4.Top)
                    {
                        down = false;
                    }
                    if (upidle.Top < a3o4.Bottom && upidle.Bottom > a3o4.Bottom)
                    {
                        up = false;
                    }
                }

                if (upidle.Bounds.IntersectsWith(a3o5.Bounds))
                {
                    if (upidle.Left < a3o5.Left && upidle.Right > a3o5.Left)
                    {
                        right = false;
                    }
                    if (upidle.Left < a3o5.Right && upidle.Right > a3o5.Right)
                    {
                        left = false;
                    }
                    if (upidle.Top < a3o5.Top && upidle.Bottom > a3o5.Top)
                    {
                        down = false;
                    }
                    if (upidle.Top < a3o5.Bottom && upidle.Bottom > a3o5.Bottom)
                    {
                        up = false;
                    }
                }

                if (upidle.Bounds.IntersectsWith(a3o6.Bounds))
                {
                    if (upidle.Left < a3o6.Left && upidle.Right > a3o6.Left)
                    {
                        right = false;
                    }
                    if (upidle.Left < a3o6.Right && upidle.Right > a3o6.Right)
                    {
                        left = false;
                    }
                    if (upidle.Top < a3o6.Top && upidle.Bottom > a3o6.Top)
                    {
                        down = false;
                    }
                    if (upidle.Top < a3o6.Bottom && upidle.Bottom > a3o6.Bottom)
                    {
                        up = false;
                    }
                }

                if (upidle.Bounds.IntersectsWith(a3o7.Bounds))
                {
                    if (upidle.Left < a3o7.Left && upidle.Right > a3o7.Left)
                    {
                        right = false;
                    }
                    if (upidle.Left < a3o7.Right && upidle.Right > a3o7.Right)
                    {
                        left = false;
                    }
                    if (upidle.Top < a3o7.Top && upidle.Bottom > a3o7.Top)
                    {
                        down = false;
                    }
                    if (upidle.Top < a3o7.Bottom && upidle.Bottom > a3o7.Bottom)
                    {
                        up = false;
                    }
                }

                if (upidle.Bounds.IntersectsWith(a3o8.Bounds))
                {
                    if (upidle.Left < a3o8.Left && upidle.Right > a3o8.Left)
                    {
                        right = false;
                    }
                    if (upidle.Left < a3o8.Right && upidle.Right > a3o8.Right)
                    {
                        left = false;
                    }
                    if (upidle.Top < a3o8.Top && upidle.Bottom > a3o8.Top)
                    {
                        down = false;
                    }
                    if (upidle.Top < a3o8.Bottom && upidle.Bottom > a3o8.Bottom)
                    {
                        up = false;
                    }
                }

                if (upidle.Bounds.IntersectsWith(a3o9.Bounds))
                {
                    if (upidle.Left < a3o9.Left && upidle.Right > a3o9.Left)
                    {
                        right = false;
                    }
                    if (upidle.Left < a3o9.Right && upidle.Right > a3o9.Right)
                    {
                        left = false;
                    }
                    if (upidle.Top < a3o9.Top && upidle.Bottom > a3o9.Top)
                    {
                        down = false;
                    }
                    if (upidle.Top < a3o9.Bottom && upidle.Bottom > a3o9.Bottom)
                    {
                        up = false;
                    }
                }

                if (upidle.Bounds.IntersectsWith(a3o10.Bounds))
                {
                    if (upidle.Left < a3o10.Left && upidle.Right > a3o10.Left)
                    {
                        right = false;
                    }
                    if (upidle.Left < a3o10.Right && upidle.Right > a3o10.Right)
                    {
                        left = false;
                    }
                    if (upidle.Top < a3o10.Top && upidle.Bottom > a3o10.Top)
                    {
                        down = false;
                    }
                    if (upidle.Top < a3o10.Bottom && upidle.Bottom > a3o10.Bottom)
                    {
                        up = false;
                    }
                }

                if (upidle.Bounds.IntersectsWith(a3o11.Bounds))
                {
                    if (upidle.Left < a3o11.Left && upidle.Right > a3o11.Left)
                    {
                        right = false;
                    }
                    if (upidle.Left < a3o11.Right && upidle.Right > a3o11.Right)
                    {
                        left = false;
                    }
                    if (upidle.Top < a3o11.Top && upidle.Bottom > a3o11.Top)
                    {
                        down = false;
                    }
                    if (upidle.Top < a3o11.Bottom && upidle.Bottom > a3o11.Bottom)
                    {
                        up = false;
                    }
                }

                if (upidle.Bounds.IntersectsWith(a3o12.Bounds))
                {
                    if (upidle.Left < a3o12.Left && upidle.Right > a3o12.Left)
                    {
                        right = false;
                    }
                    if (upidle.Left < a3o12.Right && upidle.Right > a3o12.Right)
                    {
                        left = false;
                    }
                    if (upidle.Top < a3o12.Top && upidle.Bottom > a3o12.Top)
                    {
                        down = false;
                    }
                    if (upidle.Top < a3o12.Bottom && upidle.Bottom > a3o12.Bottom)
                    {
                        up = false;
                    }
                }

                if (upidle.Bounds.IntersectsWith(a3o13.Bounds))
                {
                    if (upidle.Left < a3o13.Left && upidle.Right > a3o13.Left)
                    {
                        right = false;
                    }
                    if (upidle.Left < a3o13.Right && upidle.Right > a3o13.Right)
                    {
                        left = false;
                    }
                    if (upidle.Top < a3o13.Top && upidle.Bottom > a3o13.Top)
                    {
                        down = false;
                    }
                    if (upidle.Top < a3o13.Bottom && upidle.Bottom > a3o13.Bottom)
                    {
                        up = false;
                    }
                }

                if (upidle.Bounds.IntersectsWith(a3o14.Bounds))
                {
                    if (upidle.Left < a3o14.Left && upidle.Right > a3o14.Left)
                    {
                        right = false;
                    }
                    if (upidle.Left < a3o14.Right && upidle.Right > a3o14.Right)
                    {
                        left = false;
                    }
                    if (upidle.Top < a3o14.Top && upidle.Bottom > a3o14.Top)
                    {
                        down = false;
                    }
                    if (upidle.Top < a3o14.Bottom && upidle.Bottom > a3o14.Bottom)
                    {
                        up = false;
                    }
                }

                if (upidle.Bounds.IntersectsWith(a3o15.Bounds))
                {
                    if (upidle.Left < a3o15.Left && upidle.Right > a3o15.Left)
                    {
                        right = false;
                    }
                    if (upidle.Left < a3o15.Right && upidle.Right > a3o15.Right)
                    {
                        left = false;
                    }
                    if (upidle.Top < a3o15.Top && upidle.Bottom > a3o15.Top)
                    {
                        down = false;
                    }
                    if (upidle.Top < a3o15.Bottom && upidle.Bottom > a3o15.Bottom)
                    {
                        up = false;
                    }
                }

                if (upidle.Bounds.IntersectsWith(a3o16.Bounds))
                {
                    if (upidle.Left < a3o16.Left && upidle.Right > a3o16.Left)
                    {
                        right = false;
                    }
                    if (upidle.Left < a3o16.Right && upidle.Right > a3o16.Right)
                    {
                        left = false;
                    }
                    if (upidle.Top < a3o16.Top && upidle.Bottom > a3o16.Top)
                    {
                        down = false;
                    }
                    if (upidle.Top < a3o16.Bottom && upidle.Bottom > a3o16.Bottom)
                    {
                        up = false;
                    }
                }

                if (upidle.Bounds.IntersectsWith(a3o17.Bounds))
                {
                    if (upidle.Left < a3o17.Left && upidle.Right > a3o17.Left)
                    {
                        right = false;
                    }
                    if (upidle.Left < a3o17.Right && upidle.Right > a3o17.Right)
                    {
                        left = false;
                    }
                    if (upidle.Top < a3o17.Top && upidle.Bottom > a3o17.Top)
                    {
                        down = false;
                    }
                    if (upidle.Top < a3o17.Bottom && upidle.Bottom > a3o17.Bottom)
                    {
                        up = false;
                    }
                }

                if (upidle.Bounds.IntersectsWith(a3o18.Bounds))
                {
                    if (upidle.Left < a3o18.Left && upidle.Right > a3o18.Left)
                    {
                        right = false;
                    }
                    if (upidle.Left < a3o18.Right && upidle.Right > a3o18.Right)
                    {
                        left = false;
                    }
                    if (upidle.Top < a3o18.Top && upidle.Bottom > a3o18.Top)
                    {
                        down = false;
                    }
                    if (upidle.Top < a3o18.Bottom && upidle.Bottom > a3o18.Bottom)
                    {
                        up = false;
                    }
                }

                if (upidle.Bounds.IntersectsWith(a3o19.Bounds))
                {
                    if (upidle.Left < a3o19.Left && upidle.Right > a3o19.Left)
                    {
                        right = false;
                    }
                    if (upidle.Left < a3o19.Right && upidle.Right > a3o19.Right)
                    {
                        left = false;
                    }
                    if (upidle.Top < a3o19.Top && upidle.Bottom > a3o19.Top)
                    {
                        down = false;
                    }
                    if (upidle.Top < a3o19.Bottom && upidle.Bottom > a3o19.Bottom)
                    {
                        up = false;
                    }
                }

                if (upidle.Bounds.IntersectsWith(a3o20.Bounds))
                {
                    if (upidle.Left < a3o20.Left && upidle.Right > a3o20.Left)
                    {
                        right = false;
                    }
                    if (upidle.Left < a3o20.Right && upidle.Right > a3o20.Right)
                    {
                        left = false;
                    }
                    if (upidle.Top < a3o20.Top && upidle.Bottom > a3o20.Top)
                    {
                        down = false;
                    }
                    if (upidle.Top < a3o20.Bottom && upidle.Bottom > a3o20.Bottom)
                    {
                        up = false;
                    }
                }

                if (upidle.Bounds.IntersectsWith(a3o21.Bounds))
                {
                    if (upidle.Left < a3o21.Left && upidle.Right > a3o21.Left)
                    {
                        right = false;
                    }
                    if (upidle.Left < a3o21.Right && upidle.Right > a3o21.Right)
                    {
                        left = false;
                    }
                    if (upidle.Top < a3o21.Top && upidle.Bottom > a3o21.Top)
                    {
                        down = false;
                    }
                    if (upidle.Top < a3o21.Bottom && upidle.Bottom > a3o21.Bottom)
                    {
                        up = false;
                    }
                }

                if (upidle.Bounds.IntersectsWith(a3o22.Bounds))
                {
                    if (upidle.Left < a3o22.Left && upidle.Right > a3o22.Left)
                    {
                        right = false;
                    }
                    if (upidle.Left < a3o22.Right && upidle.Right > a3o22.Right)
                    {
                        left = false;
                    }
                    if (upidle.Top < a3o22.Top && upidle.Bottom > a3o22.Top)
                    {
                        down = false;
                    }
                    if (upidle.Top < a3o22.Bottom && upidle.Bottom > a3o22.Bottom)
                    {
                        up = false;
                    }
                }

                if (upidle.Bounds.IntersectsWith(a3o23.Bounds))
                {
                    if (upidle.Left < a3o23.Left && upidle.Right > a3o23.Left)
                    {
                        right = false;
                    }
                    if (upidle.Left < a3o23.Right && upidle.Right > a3o23.Right)
                    {
                        left = false;
                    }
                    if (upidle.Top < a3o23.Top && upidle.Bottom > a3o23.Top)
                    {
                        down = false;
                    }
                    if (upidle.Top < a3o23.Bottom && upidle.Bottom > a3o23.Bottom)
                    {
                        up = false;
                    }
                }

                if (upidle.Bounds.IntersectsWith(a3o24.Bounds))
                {
                    if (upidle.Left < a3o24.Left && upidle.Right > a3o24.Left)
                    {
                        right = false;
                    }
                    if (upidle.Left < a3o24.Right && upidle.Right > a3o24.Right)
                    {
                        left = false;
                    }
                    if (upidle.Top < a3o24.Top && upidle.Bottom > a3o24.Top)
                    {
                        down = false;
                    }
                    if (upidle.Top < a3o24.Bottom && upidle.Bottom > a3o24.Bottom)
                    {
                        up = false;
                    }
                }

                if (upidle.Bounds.IntersectsWith(a3o25.Bounds))
                {
                    if (upidle.Left < a3o25.Left && upidle.Right > a3o25.Left)
                    {
                        right = false;
                    }
                    if (upidle.Left < a3o25.Right && upidle.Right > a3o25.Right)
                    {
                        left = false;
                    }
                    if (upidle.Top < a3o25.Top && upidle.Bottom > a3o25.Top)
                    {
                        down = false;
                    }
                    if (upidle.Top < a3o25.Bottom && upidle.Bottom > a3o25.Bottom)
                    {
                        up = false;
                    }
                }

                if (upidle.Bounds.IntersectsWith(a3o26.Bounds))
                {
                    if (upidle.Left < a3o26.Left && upidle.Right > a3o26.Left)
                    {
                        right = false;
                    }
                    if (upidle.Left < a3o26.Right && upidle.Right > a3o26.Right)
                    {
                        left = false;
                    }
                    if (upidle.Top < a3o26.Top && upidle.Bottom > a3o26.Top)
                    {
                        down = false;
                    }
                    if (upidle.Top < a3o26.Bottom && upidle.Bottom > a3o26.Bottom)
                    {
                        up = false;
                    }
                }

                #endregion

            #region d1, d2, and d3 collision



                if (upidle.Bounds.IntersectsWith(barricade.Bounds))
                {
                    if (upidle.Left < barricade.Left && upidle.Right > barricade.Left)
                    {
                        right = false;
                    }
                    if (upidle.Left < barricade.Right && upidle.Right > barricade.Right)
                    {
                        left = false;
                    }
                    if (upidle.Top < barricade.Top && upidle.Bottom > barricade.Top)
                    {
                        down = false;
                    }
                    if (upidle.Top < barricade.Bottom && upidle.Bottom > barricade.Bottom)
                    {
                        up = false;
                    }
                }

                if (upidle.Bounds.IntersectsWith(barricade2.Bounds))
                {
                    if (upidle.Left < barricade2.Left && upidle.Right > barricade2.Left)
                    {
                        right = false;
                    }
                    if (upidle.Left < barricade2.Right && upidle.Right > barricade2.Right)
                    {
                        left = false;
                    }
                    if (upidle.Top < barricade2.Top && upidle.Bottom > barricade2.Top)
                    {
                        down = false;
                    }
                    if (upidle.Top < barricade2.Bottom && upidle.Bottom > barricade2.Bottom)
                    {
                        up = false;
                    }
                }

                if (upidle.Bounds.IntersectsWith(d1o1.Bounds))
                {
                    if (upidle.Left < d1o1.Left && upidle.Right > d1o1.Left)
                    {
                        right = false;
                    }
                    if (upidle.Left < d1o1.Right && upidle.Right > d1o1.Right)
                    {
                        left = false;
                    }
                    if (upidle.Top < d1o1.Top && upidle.Bottom > d1o1.Top)
                    {
                        down = false;
                    }
                    if (upidle.Top < d1o1.Bottom && upidle.Bottom > d1o1.Bottom)
                    {
                        up = false;
                    }
                }


                if (upidle.Bounds.IntersectsWith(d1o2.Bounds))
                {
                    if (upidle.Left < d1o2.Left && upidle.Right > d1o2.Left)
                    {
                        right = false;
                    }
                    if (upidle.Left < d1o2.Right && upidle.Right > d1o2.Right)
                    {
                        left = false;
                    }
                    if (upidle.Top < d1o2.Top && upidle.Bottom > d1o2.Top)
                    {
                        down = false;
                    }
                    if (upidle.Top < d1o2.Bottom && upidle.Bottom > d1o2.Bottom)
                    {
                        up = false;
                    }
                }

                if (upidle.Bounds.IntersectsWith(d1o3.Bounds))
                {
                    if (upidle.Left < d1o3.Left && upidle.Right > d1o3.Left)
                    {
                        right = false;
                    }
                    if (upidle.Left < d1o3.Right && upidle.Right > d1o3.Right)
                    {
                        left = false;
                    }
                    if (upidle.Top < d1o3.Top && upidle.Bottom > d1o3.Top)
                    {
                        down = false;
                    }
                    if (upidle.Top < d1o3.Bottom && upidle.Bottom > d1o3.Bottom)
                    {
                        up = false;
                    }
                }

                if (upidle.Bounds.IntersectsWith(d1o4.Bounds))
                {
                    if (upidle.Left < d1o4.Left && upidle.Right > d1o4.Left)
                    {
                        right = false;
                    }
                    if (upidle.Left < d1o4.Right && upidle.Right > d1o4.Right)
                    {
                        left = false;
                    }
                    if (upidle.Top < d1o4.Top && upidle.Bottom > d1o4.Top)
                    {
                        down = false;
                    }
                    if (upidle.Top < d1o4.Bottom && upidle.Bottom > d1o4.Bottom)
                    {
                        up = false;
                    }
                }

                if (upidle.Bounds.IntersectsWith(d1o5.Bounds))
                {
                    if (upidle.Left < d1o5.Left && upidle.Right > d1o5.Left)
                    {
                        right = false;
                    }
                    if (upidle.Left < d1o5.Right && upidle.Right > d1o5.Right)
                    {
                        left = false;
                    }
                    if (upidle.Top < d1o5.Top && upidle.Bottom > d1o5.Top)
                    {
                        down = false;
                    }
                    if (upidle.Top < d1o5.Bottom && upidle.Bottom > d1o5.Bottom)
                    {
                        up = false;
                    }
                }

                if (upidle.Bounds.IntersectsWith(d1o6.Bounds))
                {
                    if (upidle.Left < d1o6.Left && upidle.Right > d1o6.Left)
                    {
                        right = false;
                    }
                    if (upidle.Left < d1o6.Right && upidle.Right > d1o6.Right)
                    {
                        left = false;
                    }
                    if (upidle.Top < d1o6.Top && upidle.Bottom > d1o6.Top)
                    {
                        down = false;
                    }
                    if (upidle.Top < d1o6.Bottom && upidle.Bottom > d1o6.Bottom)
                    {
                        up = false;
                    }
                }

                if (upidle.Bounds.IntersectsWith(d1o7.Bounds))
                {
                    if (upidle.Left < d1o7.Left && upidle.Right > d1o7.Left)
                    {
                        right = false;
                    }
                    if (upidle.Left < d1o7.Right && upidle.Right > d1o7.Right)
                    {
                        left = false;
                    }
                    if (upidle.Top < d1o7.Top && upidle.Bottom > d1o7.Top)
                    {
                        down = false;
                    }
                    if (upidle.Top < d1o7.Bottom && upidle.Bottom > d1o7.Bottom)
                    {
                        up = false;
                    }
                }

                if (upidle.Bounds.IntersectsWith(d1o8.Bounds))
                {
                    if (upidle.Left < d1o8.Left && upidle.Right > d1o8.Left)
                    {
                        right = false;
                    }
                    if (upidle.Left < d1o8.Right && upidle.Right > d1o8.Right)
                    {
                        left = false;
                    }
                    if (upidle.Top < d1o8.Top && upidle.Bottom > d1o8.Top)
                    {
                        down = false;
                    }
                    if (upidle.Top < d1o8.Bottom && upidle.Bottom > d1o8.Bottom)
                    {
                        up = false;
                    }
                }

                if (upidle.Bounds.IntersectsWith(d1o9.Bounds))
                {
                    if (upidle.Left < d1o9.Left && upidle.Right > d1o9.Left)
                    {
                        right = false;
                    }
                    if (upidle.Left < d1o9.Right && upidle.Right > d1o9.Right)
                    {
                        left = false;
                    }
                    if (upidle.Top < d1o9.Top && upidle.Bottom > d1o9.Top)
                    {
                        down = false;
                    }
                    if (upidle.Top < d1o9.Bottom && upidle.Bottom > d1o9.Bottom)
                    {
                        up = false;
                    }
                }

                if (upidle.Bounds.IntersectsWith(d1o10.Bounds))
                {
                    if (upidle.Left < d1o10.Left && upidle.Right > d1o10.Left)
                    {
                        right = false;
                    }
                    if (upidle.Left < d1o10.Right && upidle.Right > d1o10.Right)
                    {
                        left = false;
                    }
                    if (upidle.Top < d1o10.Top && upidle.Bottom > d1o10.Top)
                    {
                        down = false;
                    }
                    if (upidle.Top < d1o10.Bottom && upidle.Bottom > d1o10.Bottom)
                    {
                        up = false;
                    }
                }

                if (upidle.Bounds.IntersectsWith(d1o11.Bounds))
                {
                    if (upidle.Left < d1o11.Left && upidle.Right > d1o11.Left)
                    {
                        right = false;
                    }
                    if (upidle.Left < d1o11.Right && upidle.Right > d1o11.Right)
                    {
                        left = false;
                    }
                    if (upidle.Top < d1o11.Top && upidle.Bottom > d1o11.Top)
                    {
                        down = false;
                    }
                    if (upidle.Top < d1o11.Bottom && upidle.Bottom > d1o11.Bottom)
                    {
                        up = false;
                    }
                }

                #endregion

            #region shop collision

             if (upidle.Bounds.IntersectsWith(s1o1.Bounds))
            {
                if (upidle.Left < s1o1.Left && upidle.Right > s1o1.Left)
                {
                    right = false;
                }
                if (upidle.Left < s1o1.Right && upidle.Right > s1o1.Right)
                {
                    left = false;
                }
                if (upidle.Top < s1o1.Top && upidle.Bottom > s1o1.Top)
                {
                    down = false;
                }
                if (upidle.Top < s1o1.Bottom && upidle.Bottom > s1o1.Bottom)
                {
                    up = false;
                }
            }

             if (upidle.Bounds.IntersectsWith(s1o2.Bounds))
            {
                if (upidle.Left < s1o2.Left && upidle.Right > s1o2.Left)
                {
                    right = false;
                }
                if (upidle.Left < s1o2.Right && upidle.Right > s1o2.Right)
                {
                    left = false;
                }
                if (upidle.Top < s1o2.Top && upidle.Bottom > s1o2.Top)
                {
                    down = false;
                }
                if (upidle.Top < s1o2.Bottom && upidle.Bottom > s1o2.Bottom)
                {
                    up = false;
                }
            }

             if (upidle.Bounds.IntersectsWith(s1o3.Bounds))
            {
                if (upidle.Left < s1o3.Left && upidle.Right > s1o3.Left)
                {
                    right = false;
                }
                if (upidle.Left < s1o3.Right && upidle.Right > s1o3.Right)
                {
                    left = false;
                }
                if (upidle.Top < s1o3.Top && upidle.Bottom > s1o3.Top)
                {
                    down = false;
                }
                if (upidle.Top < s1o3.Bottom && upidle.Bottom > s1o3.Bottom)
                {
                    up = false;
                }
            }

             if (upidle.Bounds.IntersectsWith(s1o4.Bounds))
            {
                if (upidle.Left < s1o4.Left && upidle.Right > s1o4.Left)
                {
                    right = false;
                }
                if (upidle.Left < s1o4.Right && upidle.Right > s1o4.Right)
                {
                    left = false;
                }
                if (upidle.Top < s1o4.Top && upidle.Bottom > s1o4.Top)
                {
                    down = false;
                }
                if (upidle.Top < s1o4.Bottom && upidle.Bottom > s1o4.Bottom)
                {
                    up = false;
                }
            }

             if (upidle.Bounds.IntersectsWith(s1o5.Bounds))
            {
                if (upidle.Left < s1o5.Left && upidle.Right > s1o5.Left)
                {
                    right = false;
                }
                if (upidle.Left < s1o5.Right && upidle.Right > s1o5.Right)
                {
                    left = false;
                }
                if (upidle.Top < s1o5.Top && upidle.Bottom > s1o5.Top)
                {
                    down = false;
                }
                if (upidle.Top < s1o5.Bottom && upidle.Bottom > s1o5.Bottom)
                {
                    up = false;
                }
            }
#endregion


                if (up == true && down == false && left == false && right == false)
            {
                upani.Visible = true;
                leftani.Visible = false;
                rightani.Visible = false;
                downani.Visible = false;
                upidle.Visible = false;
                leftidle.Visible = false;
                downidle.Visible = false;
                rightidle.Visible = false;
                

                upani.Top -= 4;
                leftani.Top -= 4;
                rightani.Top -= 4;
                downani.Top -= 4;
                upidle.Top -= 4;
                leftidle.Top -= 4;
                downidle.Top -= 4;
                rightidle.Top -= 4;
                swingdown.Top -= 4;
                swingleft.Top -= 4;
                swingright.Top -= 4;
                swingup.Top -= 4;
            }

                if (down == true && up == false && left == false && right == false)
            {
                upani.Visible = false;
                leftani.Visible = false;
                rightani.Visible = false;
                downani.Visible = true;
                upidle.Visible = false;
                leftidle.Visible = false;
                downidle.Visible = false;
                rightidle.Visible = false;
         

                upani.Top += 4;
                leftani.Top += 4;
                rightani.Top += 4;
                downani.Top += 4;
                upidle.Top += 4;
                leftidle.Top += 4;
                downidle.Top += 4;
                rightidle.Top += 4;
                swingdown.Top += 4;
                swingleft.Top += 4;
                swingright.Top += 4;
                swingup.Top += 4;
            }

                if (right == true && left == false && up == false && down == false)
            {
                upani.Visible = false;
                leftani.Visible = false;
                rightani.Visible = true;
                downani.Visible = false;
                upidle.Visible = false;
                leftidle.Visible = false;
                downidle.Visible = false;
                rightidle.Visible = false;
               

                upani.Left += 4;
                leftani.Left += 4;
                rightani.Left += 4;
                downani.Left += 4;
                upidle.Left += 4;
                leftidle.Left += 4;
                downidle.Left += 4;
                rightidle.Left += 4;
                swingdown.Left += 4;
                swingleft.Left += 4;
                swingright.Left += 4;
                swingup.Left += 4;
            }

            if (left == true && right == false && up == false && down == false)
            {
                upani.Visible = false;
                leftani.Visible = true;
                rightani.Visible = false;
                downani.Visible = false;
                upidle.Visible = false;
                leftidle.Visible = false;
                downidle.Visible = false;
                rightidle.Visible = false;


                upani.Left -= 4;
                leftani.Left -= 4;
                rightani.Left -= 4;
                downani.Left -= 4;
                upidle.Left -= 4;
                leftidle.Left -= 4;
                downidle.Left -= 4;
                rightidle.Left -= 4;
                swingdown.Left -= 4;
                swingleft.Left -= 4;
                swingright.Left -= 4;
                swingup.Left -= 4;
            
            }
        }



        private void label1_Click(object sender, EventArgs e)
        {
            titlebgm.Stop();
            gameover = false;
            title.Location = new System.Drawing.Point(30000, 30000);
            overworldbgm.URL = "sfx/overworld.wav";
            overworldbgm.Ctlcontrols.play();
            overworldbgm.settings.setMode("loop", true);

            // 
            // a1o1
            // 
            this.a1o1.BackColor = System.Drawing.Color.DarkRed;
            this.a1o1.Location = new System.Drawing.Point(257, -5);
            this.a1o1.Name = "a1o1";
            this.a1o1.Size = new System.Drawing.Size(198, 164);
            this.a1o1.TabIndex = 0;

            // 
            // a1o2
            // 
            this.a1o2.BackColor = System.Drawing.Color.DarkRed;
            this.a1o2.Location = new System.Drawing.Point(1, 205);
            this.a1o2.Name = "a1o2";
            this.a1o2.Size = new System.Drawing.Size(50, 148);
            this.a1o2.TabIndex = 1;
            // 
            // a1o3
            // 
            this.a1o3.BackColor = System.Drawing.Color.DarkRed;
            this.a1o3.Location = new System.Drawing.Point(24, 306);
            this.a1o3.Name = "a1o3";
            this.a1o3.Size = new System.Drawing.Size(431, 44);
            this.a1o3.TabIndex = 2;
            // 
            // a1o4
            // 
            this.a1o4.BackColor = System.Drawing.Color.DarkRed;
            this.a1o4.Location = new System.Drawing.Point(394, 200);
            this.a1o4.Name = "a1o4";
            this.a1o4.Size = new System.Drawing.Size(61, 142);
            this.a1o4.TabIndex = 3;
            // 
            // a1o5
            // 
            this.a1o5.BackColor = System.Drawing.Color.DarkRed;
            this.a1o5.Location = new System.Drawing.Point(-10, -5);
            this.a1o5.Name = "a1o5";
            this.a1o5.Size = new System.Drawing.Size(49, 165);
            this.a1o5.TabIndex = 4;
            // 
            // a1o6
            // 
            this.a1o6.BackColor = System.Drawing.Color.DarkRed;
            this.a1o6.Location = new System.Drawing.Point(22, -12);
            this.a1o6.Name = "a1o6";
            this.a1o6.Size = new System.Drawing.Size(29, 155);
            this.a1o6.TabIndex = 5;
            // 
            // a1o7
            // 
            this.a1o7.BackColor = System.Drawing.Color.DarkRed;
            this.a1o7.Location = new System.Drawing.Point(45, -27);
            this.a1o7.Name = "a1o7";
            this.a1o7.Size = new System.Drawing.Size(29, 154);
            this.a1o7.TabIndex = 6;
            // 
            // a1o8
            // 
            this.a1o8.BackColor = System.Drawing.Color.DarkRed;
            this.a1o8.Location = new System.Drawing.Point(71, -5);
            this.a1o8.Name = "a1o8";
            this.a1o8.Size = new System.Drawing.Size(21, 112);
            this.a1o8.TabIndex = 7;
            // 
            // a1o9
            // 
            this.a1o9.BackColor = System.Drawing.Color.DarkRed;
            this.a1o9.Location = new System.Drawing.Point(80, -27);
            this.a1o9.Name = "a1o9";
            this.a1o9.Size = new System.Drawing.Size(25, 112);
            this.a1o9.TabIndex = 8;
            // 
            // a1o10
            // 
            this.a1o10.BackColor = System.Drawing.Color.DarkRed;
            this.a1o10.Location = new System.Drawing.Point(140, -30);
            this.a1o10.Name = "a1o10";
            this.a1o10.Size = new System.Drawing.Size(51, 89);
            this.a1o10.TabIndex = 9;
            // 
            // leftidle
            // 
            this.leftidle.BackColor = System.Drawing.Color.Transparent;
            this.leftidle.Image = global::ZeldaDemo.Properties.Resources.leftidle;
            this.leftidle.Location = new System.Drawing.Point(143, 190);
            this.leftidle.Name = "leftidle";
            this.leftidle.Size = new System.Drawing.Size(26, 29);
            this.leftidle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.leftidle.TabIndex = 10;
            this.leftidle.TabStop = false;
            this.leftidle.Visible = false;
            // 
            // rightidle
            // 
            this.rightidle.BackColor = System.Drawing.Color.Transparent;
            this.rightidle.Image = global::ZeldaDemo.Properties.Resources.rightidle;
            this.rightidle.Location = new System.Drawing.Point(143, 190);
            this.rightidle.Name = "rightidle";
            this.rightidle.Size = new System.Drawing.Size(26, 29);
            this.rightidle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.rightidle.TabIndex = 11;
            this.rightidle.TabStop = false;
            this.rightidle.Visible = false;
            // 
            // upidle
            // 
            this.upidle.BackColor = System.Drawing.Color.Transparent;
            this.upidle.Image = global::ZeldaDemo.Properties.Resources.upidle;
            this.upidle.Location = new System.Drawing.Point(143, 190);
            this.upidle.Name = "upidle";
            this.upidle.Size = new System.Drawing.Size(26, 29);
            this.upidle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.upidle.TabIndex = 12;
            this.upidle.TabStop = false;
            this.upidle.Visible = false;
            // 
            // downidle
            // 
            this.downidle.BackColor = System.Drawing.Color.Transparent;
            this.downidle.Image = global::ZeldaDemo.Properties.Resources.downidle;
            this.downidle.Location = new System.Drawing.Point(143, 190);
            this.downidle.Name = "downidle";
            this.downidle.Size = new System.Drawing.Size(26, 29);
            this.downidle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.downidle.TabIndex = 13;
            this.downidle.TabStop = false;
            // 
            // downani
            // 
            this.downani.BackColor = System.Drawing.Color.Transparent;
            this.downani.Image = global::ZeldaDemo.Properties.Resources.downani;
            this.downani.Location = new System.Drawing.Point(143, 190);
            this.downani.Name = "downani";
            this.downani.Size = new System.Drawing.Size(26, 29);
            this.downani.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.downani.TabIndex = 14;
            this.downani.TabStop = false;
            this.downani.Visible = false;
            // 
            // leftani
            // 
            this.leftani.BackColor = System.Drawing.Color.Transparent;
            this.leftani.Image = global::ZeldaDemo.Properties.Resources.leftani;
            this.leftani.Location = new System.Drawing.Point(143, 190);
            this.leftani.Name = "leftani";
            this.leftani.Size = new System.Drawing.Size(26, 29);
            this.leftani.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.leftani.TabIndex = 15;
            this.leftani.TabStop = false;
            this.leftani.Visible = false;
            // 
            // rightani
            // 
            this.rightani.BackColor = System.Drawing.Color.Transparent;
            this.rightani.Image = global::ZeldaDemo.Properties.Resources.rightani;
            this.rightani.Location = new System.Drawing.Point(143, 190);
            this.rightani.Name = "rightani";
            this.rightani.Size = new System.Drawing.Size(26, 29);
            this.rightani.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.rightani.TabIndex = 16;
            this.rightani.TabStop = false;
            this.rightani.Visible = false;
            // 
            // swingleft
            // 
            this.swingleft.Image = global::ZeldaDemo.Properties.Resources.swingleft;
            this.swingleft.Location = new System.Drawing.Point(143, 190);
            this.swingleft.Name = "swingleft";
            this.swingleft.Size = new System.Drawing.Size(42, 29);
            this.swingleft.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.swingleft.TabIndex = 32;
            this.swingleft.TabStop = false;
            this.swingleft.Visible = false;
            // 
            // swingup
            // 
            this.swingup.Image = global::ZeldaDemo.Properties.Resources.swingup;
            this.swingup.Location = new System.Drawing.Point(143, 190);
            this.swingup.Name = "swingup";
            this.swingup.Size = new System.Drawing.Size(26, 42);
            this.swingup.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.swingup.TabIndex = 31;
            this.swingup.TabStop = false;
            this.swingup.Visible = false;
            // 
            // swingright
            // 
            this.swingright.Image = global::ZeldaDemo.Properties.Resources.swingright;
            this.swingright.Location = new System.Drawing.Point(143, 190);
            this.swingright.Name = "swingright";
            this.swingright.Size = new System.Drawing.Size(42, 29);
            this.swingright.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.swingright.TabIndex = 30;
            this.swingright.TabStop = false;
            this.swingright.Visible = false;
            // 
            // swingdown
            // 
            this.swingdown.Image = global::ZeldaDemo.Properties.Resources.swingdown;
            this.swingdown.Location = new System.Drawing.Point(143, 190);
            this.swingdown.Name = "swingdown";
            this.swingdown.Size = new System.Drawing.Size(26, 42);
            this.swingdown.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.swingdown.TabIndex = 29;
            this.swingdown.TabStop = false;
            this.swingdown.Visible = false;
            // 
            // upani
            // 
            this.upani.BackColor = System.Drawing.Color.Transparent;
            this.upani.Image = global::ZeldaDemo.Properties.Resources.upani;
            this.upani.Location = new System.Drawing.Point(143, 190);
            this.upani.Name = "upani";
            this.upani.Size = new System.Drawing.Size(26, 29);
            this.upani.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.upani.TabIndex = 17;
            this.upani.TabStop = false;
            this.upani.Visible = false;

            // 
            // warpS1
            // 
            this.warpS1.BackColor = System.Drawing.Color.Black;
            this.warpS1.Location = new System.Drawing.Point(294, 130);
            this.warpS1.Name = "warpS1";
            this.warpS1.Size = new System.Drawing.Size(35, 37);
            this.warpS1.TabIndex = 146;
            this.warpS1.Visible = false;


            if (opens1 == true)
            {
                warpS1.Visible = true;
            }
            else if (opens1 == false)
            {
                warpS1.Visible = false;
            }
            // 
            // warpA2
            // 
            this.warpA2.BackColor = System.Drawing.Color.Cyan;
            this.warpA2.Location = new System.Drawing.Point(111, 23);
            this.warpA2.Name = "warpA2";
            this.warpA2.Size = new System.Drawing.Size(26, 44);
            this.warpA2.TabIndex = 18;
            this.warpA2.Visible = false;
            // 
            // warpA3
            // 
            this.warpA3.BackColor = System.Drawing.Color.Cyan;
            this.warpA3.Location = new System.Drawing.Point(198, -1);
            this.warpA3.Name = "warpA3";
            this.warpA3.Size = new System.Drawing.Size(50, 22);
            this.warpA3.TabIndex = 74;
            this.warpA3.Visible = false;



            this.BackgroundImage = global::ZeldaDemo.Properties.Resources.a1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BackColor = System.Drawing.Color.Khaki;
            label1.Location = new System.Drawing.Point(30000, 30000);

        }
            


        private void warpTimer_Tick(object sender, EventArgs e)
        {
            rupeeCountDisplay.Text = Convert.ToString(rupeecount);
            bombCountDisplay.Text = Convert.ToString(bombcount);

            if (upidle.Bounds.IntersectsWith(warpA1fS1.Bounds))
            {
                roomid = 1;
                warptoa1from = 3;
                warped = true;
                // wipe shop 1
                s1o1.Location = new System.Drawing.Point(30000, 30000);
                s1o2.Location = new System.Drawing.Point(30000, 30000);
                s1o3.Location = new System.Drawing.Point(30000, 30000);
                s1o4.Location = new System.Drawing.Point(30000, 30000);
                s1o5.Location = new System.Drawing.Point(30000, 30000);
                buyBombs.Location = new System.Drawing.Point(30000, 30000);
                buyHeart.Location = new System.Drawing.Point(30000, 30000);
                warpA1fS1.Location = new System.Drawing.Point(30000, 30000);
                boomerang.Location = new System.Drawing.Point(21000, 21200);
                bomb.Location = new System.Drawing.Point(31121, 31212);
                explosion.Location = new System.Drawing.Point(23200, 21300);
                swordbeamdown.Location = new System.Drawing.Point(12100, 23100);
                swordbeamleft.Location = new System.Drawing.Point(12100, 23100);
                swordbeamright.Location = new System.Drawing.Point(12100, 23100);
                swordbeamup.Location = new System.Drawing.Point(12100, 23100);
                bombtimer.Stop();
                boomTimer.Stop();
                beamTimer.Stop();
                bombplaced = false;
                boomerangthrown = false;
                shootbeam = false;
                boomerangthrow.Stop();
            }


            if (upidle.Bounds.IntersectsWith(warpS1.Bounds) && opens1 == true)
            {
                roomid = 7;
                warped = true;
                // wipe out area 1
                a1o1.Location = new System.Drawing.Point(30000, 30000);
                a1o2.Location = new System.Drawing.Point(30000, 30000);
                a1o3.Location = new System.Drawing.Point(30000, 30000);
                a1o4.Location = new System.Drawing.Point(30000, 30000);
                a1o5.Location = new System.Drawing.Point(30000, 30000);
                a1o6.Location = new System.Drawing.Point(30000, 30000);
                a1o7.Location = new System.Drawing.Point(30000, 30000);
                a1o8.Location = new System.Drawing.Point(30000, 30000);
                a1o9.Location = new System.Drawing.Point(30000, 30000);
                a1o10.Location = new System.Drawing.Point(30000, 30000);
                warpA2.Location = new System.Drawing.Point(30000, 30000);
                warpA3.Location = new System.Drawing.Point(30000, 30000);
                or1d.Location = new System.Drawing.Point(30000, 30000);
                or1u.Location = new System.Drawing.Point(30000, 30000);
                or2l.Location = new System.Drawing.Point(30000, 30000);
                or2r.Location = new System.Drawing.Point(30000, 30000);
                or1rock.Location = new System.Drawing.Point(30000, 30000);
                or2rock.Location = new System.Drawing.Point(30000, 30000);
                rupee.Location = new System.Drawing.Point(30000, 30000);
                bombDropp.Location = new System.Drawing.Point(30000, 30000);
                boomerang.Location = new System.Drawing.Point(21000, 21200);
                bomb.Location = new System.Drawing.Point(30000, 30000);
                explosion.Location = new System.Drawing.Point(23200, 21300);
                swordbeamdown.Location = new System.Drawing.Point(12100, 23100);
                swordbeamleft.Location = new System.Drawing.Point(12100, 23100);
                swordbeamright.Location = new System.Drawing.Point(12100, 23100);
                swordbeamup.Location = new System.Drawing.Point(12100, 23100);
                warpS1.Location = new System.Drawing.Point(30000, 30000);
                victory1message.Location = new System.Drawing.Point(30000, 30000);
                victory2message.Location = new System.Drawing.Point(30000, 30000);
                bombtimer.Stop();
                boomTimer.Stop();
                beamTimer.Stop();
                bombplaced = false;
                boomerangthrown = false;
                shootbeam = false;
                boomerangthrow.Stop();
            }
            if (upidle.Bounds.IntersectsWith(warpd2fd3.Bounds))
            {
                warped = true;
                roomid = 5;
                warptod2from = 2;
                // wipe d3
                d1o1.Location = new System.Drawing.Point(30000, 30000);
                d1o2.Location = new System.Drawing.Point(30000, 30000);
                d1o3.Location = new System.Drawing.Point(30000, 30000);
                d1o4.Location = new System.Drawing.Point(30000, 30000);
                d1o5.Location = new System.Drawing.Point(30000, 30000);
                d1o6.Location = new System.Drawing.Point(30000, 30000);
                d1o7.Location = new System.Drawing.Point(30000, 30000);
                d1o8.Location = new System.Drawing.Point(30000, 30000);
                d1o9.Location = new System.Drawing.Point(30000, 30000);
                d1o10.Location = new System.Drawing.Point(30000, 30000);
                d1o11.Location = new System.Drawing.Point(30000, 30000);
                warpd2fd3.Location = new System.Drawing.Point(30000, 30000);
                boss.Location = new System.Drawing.Point(30000, 30000);
                bossflare1.Location = new System.Drawing.Point(30000, 30000);
                bossflare2.Location = new System.Drawing.Point(30000, 30000);
                bossflare3.Location = new System.Drawing.Point(30000, 30000);
                boomerang.Location = new System.Drawing.Point(21000, 21200);
                bomb.Location = new System.Drawing.Point(30000, 30000);
                explosion.Location = new System.Drawing.Point(23200, 21300);
                swordbeamdown.Location = new System.Drawing.Point(12100, 23100);
                swordbeamleft.Location = new System.Drawing.Point(12100, 23100);
                swordbeamright.Location = new System.Drawing.Point(12100, 23100);
                swordbeamup.Location = new System.Drawing.Point(12100, 23100);
                triforce.Location = new System.Drawing.Point(30000, 30000);
                bombtimer.Stop();
                boomTimer.Stop();
                beamTimer.Stop();
                bombplaced = false;
                boomerangthrown = false;
                shootbeam = false;
                boomerangthrow.Stop();
            }

         if (upidle.Bounds.IntersectsWith(warpD3.Bounds))
         {
             warped = true;
             roomid = 6;
             // wipe d2
                d1o1.Location = new System.Drawing.Point(30000, 30000);
                d1o2.Location = new System.Drawing.Point(30000, 30000);
                d1o3.Location = new System.Drawing.Point(30000, 30000);
                d1o4.Location = new System.Drawing.Point(30000, 30000);
                d1o5.Location = new System.Drawing.Point(30000, 30000);
                d1o6.Location = new System.Drawing.Point(30000, 30000);
                d1o7.Location = new System.Drawing.Point(30000, 30000);
                warpd1fd2.Location = new System.Drawing.Point(30000, 30000);
                heartdrop.Location = new System.Drawing.Point(30000, 30000);
                heartDrop2.Location = new System.Drawing.Point(30000, 30000);
                knightl.Location = new System.Drawing.Point(28700, 28000);
                knightr.Location = new System.Drawing.Point(28700, 28000);
                ben1d.Location = new System.Drawing.Point(28700, 28000);
                ben1u.Location = new System.Drawing.Point(28700, 28000);
                ben2d.Location = new System.Drawing.Point(28700, 28000);
                ben2u.Location = new System.Drawing.Point(28700, 28000);
                unlockBoomerang.Location = new System.Drawing.Point(28700, 28700);
                unlocker.Location = new System.Drawing.Point(30000, 30000);
                barricade2.Location = new System.Drawing.Point(30000, 30000);
                warpD3.Location = new System.Drawing.Point(30000, 30000);
                boome1.Location = new System.Drawing.Point(30000, 30000);
                boome2.Location = new System.Drawing.Point(30000, 30000);
                boomerang.Location = new System.Drawing.Point(21000, 21200);
                bomb.Location = new System.Drawing.Point(30000, 30000);
                explosion.Location = new System.Drawing.Point(23200, 21300);
                swordbeamdown.Location = new System.Drawing.Point(12100, 23100);
                swordbeamleft.Location = new System.Drawing.Point(12100, 23100);
                swordbeamright.Location = new System.Drawing.Point(12100, 23100);
                swordbeamup.Location = new System.Drawing.Point(12100, 23100);
                bombtimer.Stop();
                boomTimer.Stop();
                beamTimer.Stop();
                bombplaced = false;
                boomerangthrown = false;
                shootbeam = false;
                boomerangthrow.Stop();
         }

            
            if (upidle.Bounds.IntersectsWith(warpd1fd2.Bounds))
            {
                warped = true;
                roomid = 4;
                warptod1from = 2;
                // wipe d2
                d1o1.Location = new System.Drawing.Point(30000, 30000);
                d1o2.Location = new System.Drawing.Point(30000, 30000);
                d1o3.Location = new System.Drawing.Point(30000, 30000);
                d1o4.Location = new System.Drawing.Point(30000, 30000);
                d1o5.Location = new System.Drawing.Point(30000, 30000);
                d1o6.Location = new System.Drawing.Point(30000, 30000);
                d1o7.Location = new System.Drawing.Point(30000, 30000);
                warpd1fd2.Location = new System.Drawing.Point(30000, 30000);
                heartdrop.Location = new System.Drawing.Point(30000, 30000);
                heartDrop2.Location = new System.Drawing.Point(30000, 30000);
                knightl.Location = new System.Drawing.Point(28700, 28000);
                knightr.Location = new System.Drawing.Point(28700, 28000);
                ben1d.Location = new System.Drawing.Point(28700, 28000);
                ben1u.Location = new System.Drawing.Point(28700, 28000);
                ben2d.Location = new System.Drawing.Point(28700, 28000);
                ben2u.Location = new System.Drawing.Point(28700, 28000);
                boome1.Location = new System.Drawing.Point(30000, 30000);
                boome2.Location = new System.Drawing.Point(30000, 30000);
                unlockBoomerang.Location = new System.Drawing.Point(28700, 28700);
                unlocker.Location = new System.Drawing.Point(30000, 30000);
                barricade2.Location = new System.Drawing.Point(30000, 30000);
                warpD3.Location = new System.Drawing.Point(30000, 30000);
                boomerang.Location = new System.Drawing.Point(21000, 21200);
                bomb.Location = new System.Drawing.Point(30000, 30000);
                explosion.Location = new System.Drawing.Point(23200, 21300);
                swordbeamdown.Location = new System.Drawing.Point(12100, 23100);
                swordbeamleft.Location = new System.Drawing.Point(12100, 23100);
                swordbeamright.Location = new System.Drawing.Point(12100, 23100);
                swordbeamup.Location = new System.Drawing.Point(12100, 23100);
                bombtimer.Stop();
                boomTimer.Stop();
                beamTimer.Stop();
                bombplaced = false;
                boomerangthrown = false;
                shootbeam = false;
                boomerangthrow.Stop();

            }

            if (upidle.Bounds.IntersectsWith(warpD2.Bounds))
            {
                warped = true;
                roomid = 5;
                warptod2from = 1;
                // wipe d1
                d1o1.Location = new System.Drawing.Point(30000, 30000);
                d1o2.Location = new System.Drawing.Point(30000, 30000);
                d1o3.Location = new System.Drawing.Point(30000, 30000);
                d1o4.Location = new System.Drawing.Point(30000, 30000);
                d1o5.Location = new System.Drawing.Point(30000, 30000);
                d1o6.Location = new System.Drawing.Point(30000, 30000);
                d1o7.Location = new System.Drawing.Point(30000, 30000);
                skel1.Location = new System.Drawing.Point(27000, 30000);
                skel2.Location = new System.Drawing.Point(27000, 30000);
                skel3.Location = new System.Drawing.Point(27000, 30000);
                barricade.Location = new System.Drawing.Point(30000, 30000);
                warpa3fd1.Location = new System.Drawing.Point(30000, 30000);
                heartdrop.Location = new System.Drawing.Point(30000, 30000);
                warpD2.Location = new System.Drawing.Point(30000, 30000);
                boomerang.Location = new System.Drawing.Point(21000, 21200);
                bomb.Location = new System.Drawing.Point(30000, 30000);
                explosion.Location = new System.Drawing.Point(23200, 21300);
                swordbeamdown.Location = new System.Drawing.Point(12100, 23100);
                swordbeamleft.Location = new System.Drawing.Point(12100, 23100);
                swordbeamright.Location = new System.Drawing.Point(12100, 23100);
                swordbeamup.Location = new System.Drawing.Point(12100, 23100);
                bombtimer.Stop();
                boomTimer.Stop();
                beamTimer.Stop();
                bombplaced = false;
                boomerangthrown = false;
                shootbeam = false;
                boomerangthrow.Stop();
            }


            if (upidle.Bounds.IntersectsWith(warpa3fd1.Bounds))
            {
                warped = true;
                roomid = 3;
                warptoa3from = 2;
                // wipe d1
                d1o1.Location = new System.Drawing.Point(30000, 30000);
                d1o2.Location = new System.Drawing.Point(30000, 30000);
                d1o3.Location = new System.Drawing.Point(30000, 30000);
                d1o4.Location = new System.Drawing.Point(30000, 30000);
                d1o5.Location = new System.Drawing.Point(30000, 30000);
                d1o6.Location = new System.Drawing.Point(30000, 30000);
                d1o7.Location = new System.Drawing.Point(30000, 30000);
                skel1.Location = new System.Drawing.Point(27000, 30000);
                skel2.Location = new System.Drawing.Point(27000, 30000);
                skel3.Location = new System.Drawing.Point(27000, 30000);
                barricade.Location = new System.Drawing.Point(30000, 30000);
                warpa3fd1.Location = new System.Drawing.Point(30000, 30000);
                heartdrop.Location = new System.Drawing.Point(30000, 30000);
                warpD2.Location = new System.Drawing.Point(30000, 30000);
                boomerang.Location = new System.Drawing.Point(21000, 21200);
                bomb.Location = new System.Drawing.Point(30000, 30000);
                explosion.Location = new System.Drawing.Point(23200, 21300);
                swordbeamdown.Location = new System.Drawing.Point(12100, 23100);
                swordbeamleft.Location = new System.Drawing.Point(12100, 23100);
                swordbeamright.Location = new System.Drawing.Point(12100, 23100);
                swordbeamup.Location = new System.Drawing.Point(12100, 23100);
                bombtimer.Stop();
                boomTimer.Stop();
                beamTimer.Stop();
                bombplaced = false;
                boomerangthrown = false;
                shootbeam = false;
                boomerangthrow.Stop();
            }

            if (upidle.Bounds.IntersectsWith(warpD1.Bounds) && opend1 == true)
            {
                warped = true;
                roomid = 4;
                warptod1from = 1;

                // wipe out area 3
                a3o1.Location = new System.Drawing.Point(30000, 30000);
                a3o2.Location = new System.Drawing.Point(30000, 30000);
                a3o3.Location = new System.Drawing.Point(30000, 30000);
                a3o4.Location = new System.Drawing.Point(30000, 30000);
                a3o5.Location = new System.Drawing.Point(30000, 30000);
                a3o6.Location = new System.Drawing.Point(30000, 30000);
                a3o7.Location = new System.Drawing.Point(30000, 30000);
                a3o8.Location = new System.Drawing.Point(30000, 30000);
                a3o9.Location = new System.Drawing.Point(30000, 30000);
                a3o10.Location = new System.Drawing.Point(30000, 30000);
                a3o11.Location = new System.Drawing.Point(30000, 30000);
                a3o12.Location = new System.Drawing.Point(30000, 30000);
                a3o13.Location = new System.Drawing.Point(30000, 30000);
                a3o14.Location = new System.Drawing.Point(30000, 30000);
                a3o15.Location = new System.Drawing.Point(30000, 30000);
                a3o16.Location = new System.Drawing.Point(30000, 30000);
                a3o17.Location = new System.Drawing.Point(30000, 30000);
                a3o18.Location = new System.Drawing.Point(30000, 30000);
                a3o19.Location = new System.Drawing.Point(30000, 30000);
                a3o20.Location = new System.Drawing.Point(30000, 30000);
                a3o21.Location = new System.Drawing.Point(30000, 30000);
                a3o22.Location = new System.Drawing.Point(30000, 30000);
                a3o23.Location = new System.Drawing.Point(30000, 30000);
                a3o24.Location = new System.Drawing.Point(30000, 30000);
                a3o25.Location = new System.Drawing.Point(30000, 30000);
                a3o26.Location = new System.Drawing.Point(30000, 30000);
                warpA1f3.Location = new System.Drawing.Point(30000, 30000);
                or1d.Location = new System.Drawing.Point(30000, 30000);
                or1u.Location = new System.Drawing.Point(30000, 30000);
                or2l.Location = new System.Drawing.Point(30000, 30000);
                or2r.Location = new System.Drawing.Point(30000, 30000);
                or1rock.Location = new System.Drawing.Point(30000, 30000);
                or2rock.Location = new System.Drawing.Point(30000, 30000);
                warpD1.Location = new System.Drawing.Point(30000, 30000);
                rupee.Location = new System.Drawing.Point(30000, 30000);
                bombDropp.Location = new System.Drawing.Point(30000, 30000);
                boomerang.Location = new System.Drawing.Point(21000, 21200);
                bomb.Location = new System.Drawing.Point(30000, 30000);
                explosion.Location = new System.Drawing.Point(23200, 21300);
                swordbeamdown.Location = new System.Drawing.Point(12100, 23100);
                swordbeamleft.Location = new System.Drawing.Point(12100, 23100);
                swordbeamright.Location = new System.Drawing.Point(12100, 23100);
                swordbeamup.Location = new System.Drawing.Point(12100, 23100);
                bombtimer.Stop();
                boomTimer.Stop();
                beamTimer.Stop();
                bombplaced = false;
                boomerangthrown = false;
                shootbeam = false;
                boomerangthrow.Stop();
            }
            
            
            if (upidle.Bounds.IntersectsWith(warpA2.Bounds))
            {
                warped = true;
                roomid = 2;

                //wipe out area 1
                a1o1.Location = new System.Drawing.Point(30000, 30000);
                a1o2.Location = new System.Drawing.Point(30000, 30000);
                a1o3.Location = new System.Drawing.Point(30000, 30000);
                a1o4.Location = new System.Drawing.Point(30000, 30000);
                a1o5.Location = new System.Drawing.Point(30000, 30000);
                a1o6.Location = new System.Drawing.Point(30000, 30000);
                a1o7.Location = new System.Drawing.Point(30000, 30000);
                a1o8.Location = new System.Drawing.Point(30000, 30000);
                a1o9.Location = new System.Drawing.Point(30000, 30000);
                a1o10.Location = new System.Drawing.Point(30000, 30000);
                warpA2.Location = new System.Drawing.Point(30000, 30000);
                warpA3.Location = new System.Drawing.Point(30000, 30000);
                or1d.Location = new System.Drawing.Point(30000, 30000);
                or1u.Location = new System.Drawing.Point(30000, 30000);
                or2l.Location = new System.Drawing.Point(30000, 30000);
                or2r.Location = new System.Drawing.Point(30000, 30000);
                or1rock.Location = new System.Drawing.Point(30000, 30000);
                or2rock.Location = new System.Drawing.Point(30000, 30000);
                rupee.Location = new System.Drawing.Point(30000, 30000);
                bombDropp.Location = new System.Drawing.Point(30000, 30000);
                boomerang.Location = new System.Drawing.Point(21000, 21200);
                bomb.Location = new System.Drawing.Point(30000, 30000);
                explosion.Location = new System.Drawing.Point(23200, 21300);
                swordbeamdown.Location = new System.Drawing.Point(12100, 23100);
                swordbeamleft.Location = new System.Drawing.Point(12100, 23100);
                swordbeamright.Location = new System.Drawing.Point(12100, 23100);
                swordbeamup.Location = new System.Drawing.Point(12100, 23100);
                warpS1.Location = new System.Drawing.Point(30000, 30000);
                victory1message.Location = new System.Drawing.Point(30000, 30000);
                victory2message.Location = new System.Drawing.Point(30000, 30000);
                bombtimer.Stop();
                boomTimer.Stop();
                beamTimer.Stop();
                bombplaced = false;
                boomerangthrown = false;
                shootbeam = false;
                boomerangthrow.Stop();
                
            }

            if (upidle.Bounds.IntersectsWith(warpA3.Bounds))
            {
                warped = true;
                roomid = 3;
                warptoa3from = 1;

                // wipe out area 1
                a1o1.Location = new System.Drawing.Point(30000, 30000);
                a1o2.Location = new System.Drawing.Point(30000, 30000);
                a1o3.Location = new System.Drawing.Point(30000, 30000);
                a1o4.Location = new System.Drawing.Point(30000, 30000);
                a1o5.Location = new System.Drawing.Point(30000, 30000);
                a1o6.Location = new System.Drawing.Point(30000, 30000);
                a1o7.Location = new System.Drawing.Point(30000, 30000);
                a1o8.Location = new System.Drawing.Point(30000, 30000);
                a1o9.Location = new System.Drawing.Point(30000, 30000);
                a1o10.Location = new System.Drawing.Point(30000, 30000);
                warpA2.Location = new System.Drawing.Point(30000, 30000);
                warpA3.Location = new System.Drawing.Point(30000, 30000);
                or1d.Location = new System.Drawing.Point(30000, 30000);
                or1u.Location = new System.Drawing.Point(30000, 30000);
                or2l.Location = new System.Drawing.Point(30000, 30000);
                or2r.Location = new System.Drawing.Point(30000, 30000);
                or1rock.Location = new System.Drawing.Point(30000, 30000);
                or2rock.Location = new System.Drawing.Point(30000, 30000);
                rupee.Location = new System.Drawing.Point(30000, 30000);
                bombDropp.Location = new System.Drawing.Point(30000, 30000);
                boomerang.Location = new System.Drawing.Point(21000, 21200);
                bomb.Location = new System.Drawing.Point(30000, 30000);
                explosion.Location = new System.Drawing.Point(23200, 21300);
                swordbeamdown.Location = new System.Drawing.Point(12100, 23100);
                swordbeamleft.Location = new System.Drawing.Point(12100, 23100);
                swordbeamright.Location = new System.Drawing.Point(12100, 23100);
                swordbeamup.Location = new System.Drawing.Point(12100, 23100);
                warpS1.Location = new System.Drawing.Point(30000, 30000);
                victory1message.Location = new System.Drawing.Point(30000, 30000);
                victory2message.Location = new System.Drawing.Point(30000, 30000);
                bombtimer.Stop();
                boomTimer.Stop();
                beamTimer.Stop();
                bombplaced = false;
                boomerangthrown = false;
                shootbeam = false;
                boomerangthrow.Stop();

            }


            if (upidle.Bounds.IntersectsWith(warpA1f3.Bounds))
            {
                warped = true;
                roomid = 1;
                warptoa1from = 2;

                // wipe out area 3
                a3o1.Location = new System.Drawing.Point(30000, 30000);
                a3o2.Location = new System.Drawing.Point(30000, 30000);
                a3o3.Location = new System.Drawing.Point(30000, 30000);
                a3o4.Location = new System.Drawing.Point(30000, 30000);
                a3o5.Location = new System.Drawing.Point(30000, 30000);
                a3o6.Location = new System.Drawing.Point(30000, 30000);
                a3o7.Location = new System.Drawing.Point(30000, 30000);
                a3o8.Location = new System.Drawing.Point(30000, 30000);
                a3o9.Location = new System.Drawing.Point(30000, 30000);
                a3o10.Location = new System.Drawing.Point(30000, 30000);
                a3o11.Location = new System.Drawing.Point(30000, 30000);
                a3o12.Location = new System.Drawing.Point(30000, 30000);
                a3o13.Location = new System.Drawing.Point(30000, 30000);
                a3o14.Location = new System.Drawing.Point(30000, 30000);
                a3o15.Location = new System.Drawing.Point(30000, 30000);
                a3o16.Location = new System.Drawing.Point(30000, 30000);
                a3o17.Location = new System.Drawing.Point(30000, 30000);
                a3o18.Location = new System.Drawing.Point(30000, 30000);
                a3o19.Location = new System.Drawing.Point(30000, 30000);
                a3o20.Location = new System.Drawing.Point(30000, 30000);
                a3o21.Location = new System.Drawing.Point(30000, 30000);
                a3o22.Location = new System.Drawing.Point(30000, 30000);
                a3o23.Location = new System.Drawing.Point(30000, 30000);
                a3o24.Location = new System.Drawing.Point(30000, 30000);
                a3o25.Location = new System.Drawing.Point(30000, 30000);
                a3o26.Location = new System.Drawing.Point(30000, 30000);
                warpA1f3.Location = new System.Drawing.Point(30000, 30000);
                or1d.Location = new System.Drawing.Point(30000, 30000);
                or1u.Location = new System.Drawing.Point(30000, 30000);
                or2l.Location = new System.Drawing.Point(30000, 30000);
                or2r.Location = new System.Drawing.Point(30000, 30000);
                or1rock.Location = new System.Drawing.Point(30000, 30000);
                or2rock.Location = new System.Drawing.Point(30000, 30000);
                warpD1.Location = new System.Drawing.Point(30000, 30000);
                rupee.Location = new System.Drawing.Point(30000, 30000);
                bombDropp.Location = new System.Drawing.Point(30000, 30000);
                boomerang.Location = new System.Drawing.Point(21000, 21200);
                bomb.Location = new System.Drawing.Point(30000, 30000);
                explosion.Location = new System.Drawing.Point(23200, 21300);
                swordbeamdown.Location = new System.Drawing.Point(12100, 23100);
                swordbeamleft.Location = new System.Drawing.Point(12100, 23100);
                swordbeamright.Location = new System.Drawing.Point(12100, 23100);
                swordbeamup.Location = new System.Drawing.Point(12100, 23100);
                bombtimer.Stop();
                boomTimer.Stop();
                beamTimer.Stop();
                bombplaced = false;
                boomerangthrown = false;
                shootbeam = false;
                boomerangthrow.Stop();
            }


            if (upidle.Bounds.IntersectsWith(warpA1.Bounds))
            {
                warped = true;
                roomid = 1;
                warptoa1from = 1;
                // wipe out area 2
                a2o1.Location = new System.Drawing.Point(30000, 30000);
                a2o2.Location = new System.Drawing.Point(30000, 30000);
                a2o3.Location = new System.Drawing.Point(30000, 30000);
                a2o4.Location = new System.Drawing.Point(30000, 30000);
                a2o5.Location = new System.Drawing.Point(30000, 30000);
                warpA1.Location = new System.Drawing.Point(30000, 30000);
                sword.Location = new System.Drawing.Point(28000, 30000);
                rupee.Location = new System.Drawing.Point(30000, 30000);
                bombDropp.Location = new System.Drawing.Point(30000, 30000);
                boomerang.Location = new System.Drawing.Point(21000, 21200);
                bomb.Location = new System.Drawing.Point(30000, 30000);
                explosion.Location = new System.Drawing.Point(23200, 21300);
                swordbeamdown.Location = new System.Drawing.Point(12100, 23100);
                swordbeamleft.Location = new System.Drawing.Point(12100, 23100);
                swordbeamright.Location = new System.Drawing.Point(12100, 23100);
                swordbeamup.Location = new System.Drawing.Point(12100, 23100);
                secretr.Location = new System.Drawing.Point(30000, 30000);
                bombtimer.Stop();
                boomTimer.Stop();
                beamTimer.Stop();
                bombplaced = false;
                boomerangthrown = false;
                shootbeam = false;
                boomerangthrow.Stop();
            }

            if (upidle.Bounds.IntersectsWith(sword.Bounds))
            {
                gotsword = true;
                sword.Location = new System.Drawing.Point(29000, 30000);
                System.Media.SoundPlayer getswordsfx = new System.Media.SoundPlayer("sfx/swordsfx.wav");
                getswordsfx.Play();
            }

            if (upidle.Bounds.IntersectsWith(unlockBoomerang.Bounds))
            {
                gotboomerang = true;
                invenBoomerang.Visible = true;
                unlockBoomerang.Location = new System.Drawing.Point(29000, 30000);
                System.Media.SoundPlayer getswordsfx = new System.Media.SoundPlayer("sfx/swordsfx.wav");
                getswordsfx.Play();
            }

            if (upidle.Bounds.IntersectsWith(bombDropp.Bounds))
            {
                bombcount += 1;
                bombDropp.Location = new System.Drawing.Point(30000, 30000);
                bombCountDisplay.Text = Convert.ToString(bombcount);
                getitem.Play();
            }

            if (upidle.Bounds.IntersectsWith(rupee.Bounds))
            {
                rupeecount += 1;
                rupee.Location = new System.Drawing.Point(30000, 30000);
                rupeeCountDisplay.Text = Convert.ToString(rupeecount);
                getrupee.Play();
            }

            if (upidle.Bounds.IntersectsWith(heartdrop.Bounds))
            {
                if (health <= 2)
                {
                    health += 1;
                }

                getheart.Play();

                heartdrop.Location = new System.Drawing.Point(30000, 30000);
            }

            if (upidle.Bounds.IntersectsWith(heartDrop2.Bounds))
            {
                if (health <= 2)
                {
                    health += 1;
                }

                getheart.Play();

                heartDrop2.Location = new System.Drawing.Point(30000, 30000);
            }

            if (upidle.Bounds.IntersectsWith(triforce.Bounds))
            {
                MoveandAniTimer.Enabled = false;
                gettingtriforce = true;
                triforce.Location = new System.Drawing.Point(30000, 30000);
                overworldbgm.Ctlcontrols.stop();
                gettriforcee.Play();
                linkgettriforce.Location = upidle.Location;
                linkgettriforce.Visible = true;
                victoryTimer.Start();
                victory = true;

            }



            // inventory pointer function

            #region inven

            if (currentpointerPlace == 0)
            {
                invenPointer.Location = new System.Drawing.Point(46, 44);
            }
            else if (currentpointerPlace == 1)
            {
                invenPointer.Location = new System.Drawing.Point(88, 44);
            }
            #endregion
        }

        private void roomLoader_Tick(object sender, EventArgs e)
        {

            #region load shop 1

            if (roomid == 7 && warped == true)
            {

                orTimer.Stop();
                benTimer.Stop();
                skelTimer.Stop();
                bossTimer.Stop();
                incave = 1; // sets value to start up over world bgm again
                overworldbgm.Ctlcontrols.stop();

                // 
                // leftidle
                // 
                this.leftidle.BackColor = System.Drawing.Color.Transparent;
                this.leftidle.Image = global::ZeldaDemo.Properties.Resources.leftidle;
                this.leftidle.Location = new System.Drawing.Point(210, 276);
                this.leftidle.Name = "leftidle";
                this.leftidle.Size = new System.Drawing.Size(26, 29);
                this.leftidle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.leftidle.TabIndex = 10;
                this.leftidle.TabStop = false;
                // 
                // rightidle
                // 
                this.rightidle.BackColor = System.Drawing.Color.Transparent;
                this.rightidle.Image = global::ZeldaDemo.Properties.Resources.rightidle;
                this.rightidle.Location = new System.Drawing.Point(210, 276);
                this.rightidle.Name = "rightidle";
                this.rightidle.Size = new System.Drawing.Size(26, 29);
                this.rightidle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.rightidle.TabIndex = 11;
                this.rightidle.TabStop = false;
                // 
                // upidle
                // 
                this.upidle.BackColor = System.Drawing.Color.Transparent;
                this.upidle.Image = global::ZeldaDemo.Properties.Resources.upidle;
                this.upidle.Location = new System.Drawing.Point(210, 276);
                this.upidle.Name = "upidle";
                this.upidle.Size = new System.Drawing.Size(26, 29);
                this.upidle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.upidle.TabIndex = 12;
                this.upidle.TabStop = false;
                // 
                // downidle
                // 
                this.downidle.BackColor = System.Drawing.Color.Transparent;
                this.downidle.Image = global::ZeldaDemo.Properties.Resources.downidle;
                this.downidle.Location = new System.Drawing.Point(210, 276);
                this.downidle.Name = "downidle";
                this.downidle.Size = new System.Drawing.Size(26, 29);
                this.downidle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.downidle.TabIndex = 13;
                this.downidle.TabStop = false;
                // 
                // downani
                // 
                this.downani.BackColor = System.Drawing.Color.Transparent;
                this.downani.Image = global::ZeldaDemo.Properties.Resources.downani;
                this.downani.Location = new System.Drawing.Point(210, 276);
                this.downani.Name = "downani";
                this.downani.Size = new System.Drawing.Size(26, 29);
                this.downani.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.downani.TabIndex = 14;
                this.downani.TabStop = false;
                this.downani.Visible = false;
                // 
                // leftani
                // 
                this.leftani.BackColor = System.Drawing.Color.Transparent;
                this.leftani.Image = global::ZeldaDemo.Properties.Resources.leftani;
                this.leftani.Location = new System.Drawing.Point(210, 276);
                this.leftani.Name = "leftani";
                this.leftani.Size = new System.Drawing.Size(26, 29);
                this.leftani.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.leftani.TabIndex = 15;
                this.leftani.TabStop = false;
                this.leftani.Visible = false;
                // 
                // rightani
                // 
                this.rightani.BackColor = System.Drawing.Color.Transparent;
                this.rightani.Image = global::ZeldaDemo.Properties.Resources.rightani;
                this.rightani.Location = new System.Drawing.Point(210, 276);
                this.rightani.Name = "rightani";
                this.rightani.Size = new System.Drawing.Size(26, 29);
                this.rightani.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.rightani.TabIndex = 16;
                this.rightani.TabStop = false;
                this.rightani.Visible = false;
                // 
                // upani
                // 
                this.upani.BackColor = System.Drawing.Color.Transparent;
                this.upani.Image = global::ZeldaDemo.Properties.Resources.upani;
                this.upani.Location = new System.Drawing.Point(210, 276);
                this.upani.Name = "upani";
                this.upani.Size = new System.Drawing.Size(26, 29);
                this.upani.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.upani.TabIndex = 17;
                this.upani.TabStop = false;
                this.upani.Visible = false;
                // 
                // swingdown
                // 
                this.swingdown.BackColor = System.Drawing.Color.Transparent;
                this.swingdown.Image = global::ZeldaDemo.Properties.Resources.swingdown;
                this.swingdown.Location = new System.Drawing.Point(210, 276);
                this.swingdown.Name = "swingdown";
                this.swingdown.Size = new System.Drawing.Size(26, 42);
                this.swingdown.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.swingdown.TabIndex = 70;
                this.swingdown.TabStop = false;
                this.swingdown.Visible = false;
                // 
                // swingleft
                // 
                this.swingleft.BackColor = System.Drawing.Color.Transparent;
                this.swingleft.Image = global::ZeldaDemo.Properties.Resources.swingleft;
                this.swingleft.Location = new System.Drawing.Point(192, 276);
                this.swingleft.Name = "swingleft";
                this.swingleft.Size = new System.Drawing.Size(44, 29);
                this.swingleft.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.swingleft.TabIndex = 71;
                this.swingleft.TabStop = false;
                this.swingleft.Visible = false;
                // 
                // swingright
                // 
                this.swingright.BackColor = System.Drawing.Color.Transparent;
                this.swingright.Image = global::ZeldaDemo.Properties.Resources.swingright;
                this.swingright.Location = new System.Drawing.Point(210, 276);
                this.swingright.Name = "swingright";
                this.swingright.Size = new System.Drawing.Size(42, 29);
                this.swingright.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.swingright.TabIndex = 72;
                this.swingright.TabStop = false;
                this.swingright.Visible = false;
                // 
                // swingup
                // 
                this.swingup.BackColor = System.Drawing.Color.Transparent;
                this.swingup.Image = global::ZeldaDemo.Properties.Resources.swingup;
                this.swingup.Location = new System.Drawing.Point(210, 263);
                this.swingup.Name = "swingup";
                this.swingup.Size = new System.Drawing.Size(26, 42);
                this.swingup.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.swingup.TabIndex = 73;
                this.swingup.TabStop = false;
                this.swingup.Visible = false;
                // 
                // s1o1
                // 
                this.s1o1.BackColor = System.Drawing.Color.DarkRed;
                this.s1o1.Location = new System.Drawing.Point(0, -6);
                this.s1o1.Name = "s1o1";
                this.s1o1.Size = new System.Drawing.Size(54, 357);
                this.s1o1.TabIndex = 74;
                this.s1o1.Visible = false;
                // 
                // s1o2
                // 
                this.s1o2.BackColor = System.Drawing.Color.DarkRed;
                this.s1o2.Location = new System.Drawing.Point(0, -6);
                this.s1o2.Name = "s1o2";
                this.s1o2.Size = new System.Drawing.Size(457, 210);
                this.s1o2.TabIndex = 75;
                this.s1o2.Visible = false;
                // 
                // s1o3
                // 
                this.s1o3.BackColor = System.Drawing.Color.DarkRed;
                this.s1o3.Location = new System.Drawing.Point(393, 204);
                this.s1o3.Name = "s1o3";
                this.s1o3.Size = new System.Drawing.Size(64, 170);
                this.s1o3.TabIndex = 76;
                this.s1o3.Visible = false;
                // 
                // s1o4
                // 
                this.s1o4.BackColor = System.Drawing.Color.DarkRed;
                this.s1o4.Location = new System.Drawing.Point(44, 303);
                this.s1o4.Name = "s1o4";
                this.s1o4.Size = new System.Drawing.Size(156, 48);
                this.s1o4.TabIndex = 77;
                this.s1o4.Visible = false;
                // 
                // s1o5
                // 
                this.s1o5.BackColor = System.Drawing.Color.DarkRed;
                this.s1o5.Location = new System.Drawing.Point(247, 303);
                this.s1o5.Name = "s1o5";
                this.s1o5.Size = new System.Drawing.Size(156, 49);
                this.s1o5.TabIndex = 78;
                this.s1o5.Visible = false;
                // 
                // buyHeart
                // 
                this.buyHeart.BackColor = System.Drawing.Color.Lime;
                this.buyHeart.Location = new System.Drawing.Point(136, 221);
                this.buyHeart.Name = "buyHeart";
                this.buyHeart.Size = new System.Drawing.Size(40, 36);
                this.buyHeart.TabIndex = 79;
                this.buyHeart.Visible = false;
                // 
                // buyBombs
                // 
                this.buyBombs.BackColor = System.Drawing.Color.Lime;
                this.buyBombs.Location = new System.Drawing.Point(309, 211);
                this.buyBombs.Name = "buyBombs";
                this.buyBombs.Size = new System.Drawing.Size(40, 46);
                this.buyBombs.TabIndex = 80;
                this.buyBombs.Visible = false;
                // 
                // warpA1fS1
                // 
                this.warpA1fS1.BackColor = System.Drawing.Color.Cyan;
                this.warpA1fS1.Location = new System.Drawing.Point(202, 330);
                this.warpA1fS1.Name = "warpA1fS1";
                this.warpA1fS1.Size = new System.Drawing.Size(40, 18);
                this.warpA1fS1.TabIndex = 81;
                this.warpA1fS1.Visible = false;


                this.BackgroundImage = global::ZeldaDemo.Properties.Resources.shop;
                this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                this.BackColor = System.Drawing.Color.Black;

                warped = false;
            }

            #endregion

            #region load dungeon 3


            if (roomid == 6 && warped == true)
            {
                incave = 1;
                if (victory == false)
                {
                    bossHealth = 50;
                    bossTimer.Start();
                }
                bossystem = 0;
                benTimer.Stop();
                skelTimer.Stop();
                orTimer.Stop();

                // 
                // leftidle
                // 
                this.leftidle.BackColor = System.Drawing.Color.Transparent;
                this.leftidle.Image = global::ZeldaDemo.Properties.Resources.leftidle;
                this.leftidle.Location = new System.Drawing.Point(211, 254);
                this.leftidle.Name = "leftidle";
                this.leftidle.Size = new System.Drawing.Size(26, 29);
                this.leftidle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.leftidle.TabIndex = 10;
                this.leftidle.TabStop = false;
                // 
                // rightidle
                // 
                this.rightidle.BackColor = System.Drawing.Color.Transparent;
                this.rightidle.Image = global::ZeldaDemo.Properties.Resources.rightidle;
                this.rightidle.Location = new System.Drawing.Point(211, 254);
                this.rightidle.Name = "rightidle";
                this.rightidle.Size = new System.Drawing.Size(26, 29);
                this.rightidle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.rightidle.TabIndex = 11;
                this.rightidle.TabStop = false;
                // 
                // upidle
                // 
                this.upidle.BackColor = System.Drawing.Color.Transparent;
                this.upidle.Image = global::ZeldaDemo.Properties.Resources.upidle;
                this.upidle.Location = new System.Drawing.Point(211, 254);
                this.upidle.Name = "upidle";
                this.upidle.Size = new System.Drawing.Size(26, 29);
                this.upidle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.upidle.TabIndex = 12;
                this.upidle.TabStop = false;
                // 
                // downidle
                // 
                this.downidle.BackColor = System.Drawing.Color.Transparent;
                this.downidle.Image = global::ZeldaDemo.Properties.Resources.downidle;
                this.downidle.Location = new System.Drawing.Point(211, 254);
                this.downidle.Name = "downidle";
                this.downidle.Size = new System.Drawing.Size(26, 29);
                this.downidle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.downidle.TabIndex = 13;
                this.downidle.TabStop = false;
                // 
                // downani
                // 
                this.downani.BackColor = System.Drawing.Color.Transparent;
                this.downani.Image = global::ZeldaDemo.Properties.Resources.downani;
                this.downani.Location = new System.Drawing.Point(211, 254);
                this.downani.Name = "downani";
                this.downani.Size = new System.Drawing.Size(26, 29);
                this.downani.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.downani.TabIndex = 14;
                this.downani.TabStop = false;
                this.downani.Visible = false;
                // 
                // leftani
                // 
                this.leftani.BackColor = System.Drawing.Color.Transparent;
                this.leftani.Image = global::ZeldaDemo.Properties.Resources.leftani;
                this.leftani.Location = new System.Drawing.Point(211, 254);
                this.leftani.Name = "leftani";
                this.leftani.Size = new System.Drawing.Size(26, 29);
                this.leftani.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.leftani.TabIndex = 15;
                this.leftani.TabStop = false;
                this.leftani.Visible = false;
                // 
                // rightani
                // 
                this.rightani.BackColor = System.Drawing.Color.Transparent;
                this.rightani.Image = global::ZeldaDemo.Properties.Resources.rightani;
                this.rightani.Location = new System.Drawing.Point(211, 254);
                this.rightani.Name = "rightani";
                this.rightani.Size = new System.Drawing.Size(26, 29);
                this.rightani.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.rightani.TabIndex = 16;
                this.rightani.TabStop = false;
                this.rightani.Visible = false;
                // 
                // upani
                // 
                this.upani.BackColor = System.Drawing.Color.Transparent;
                this.upani.Image = global::ZeldaDemo.Properties.Resources.upani;
                this.upani.Location = new System.Drawing.Point(211, 254);
                this.upani.Name = "upani";
                this.upani.Size = new System.Drawing.Size(26, 29);
                this.upani.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.upani.TabIndex = 17;
                this.upani.TabStop = false;
                this.upani.Visible = false;
                // 
                // swingdown
                // 
                this.swingdown.BackColor = System.Drawing.Color.Transparent;
                this.swingdown.Image = global::ZeldaDemo.Properties.Resources.swingdown;
                this.swingdown.Location = new System.Drawing.Point(211, 254);
                this.swingdown.Name = "swingdown";
                this.swingdown.Size = new System.Drawing.Size(26, 42);
                this.swingdown.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.swingdown.TabIndex = 70;
                this.swingdown.TabStop = false;
                this.swingdown.Visible = false;
                // 
                // swingleft
                // 
                this.swingleft.BackColor = System.Drawing.Color.Transparent;
                this.swingleft.Image = global::ZeldaDemo.Properties.Resources.swingleft;
                this.swingleft.Location = new System.Drawing.Point(193, 254);
                this.swingleft.Name = "swingleft";
                this.swingleft.Size = new System.Drawing.Size(44, 29);
                this.swingleft.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.swingleft.TabIndex = 71;
                this.swingleft.TabStop = false;
                this.swingleft.Visible = false;
                // 
                // swingright
                // 
                this.swingright.BackColor = System.Drawing.Color.Transparent;
                this.swingright.Image = global::ZeldaDemo.Properties.Resources.swingright;
                this.swingright.Location = new System.Drawing.Point(211, 254);
                this.swingright.Name = "swingright";
                this.swingright.Size = new System.Drawing.Size(42, 29);
                this.swingright.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.swingright.TabIndex = 72;
                this.swingright.TabStop = false;
                this.swingright.Visible = false;
                // 
                // swingup
                // 
                this.swingup.BackColor = System.Drawing.Color.Transparent;
                this.swingup.Image = global::ZeldaDemo.Properties.Resources.swingup;
                this.swingup.Location = new System.Drawing.Point(211, 241);
                this.swingup.Name = "swingup";
                this.swingup.Size = new System.Drawing.Size(26, 42);
                this.swingup.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.swingup.TabIndex = 73;
                this.swingup.TabStop = false;
                this.swingup.Visible = false;

             // 
            // d1o1
            // 
            this.d1o1.BackColor = System.Drawing.Color.DarkRed;
            this.d1o1.Location = new System.Drawing.Point(312, 226);
            this.d1o1.Name = "d1o1";
            this.d1o1.Size = new System.Drawing.Size(63, 110);
            this.d1o1.TabIndex = 74;
            this.d1o1.Visible = false;
            // 
            // d1o3
            // 
            this.d1o3.BackColor = System.Drawing.Color.DarkRed;
            this.d1o3.Location = new System.Drawing.Point(372, -3);
            this.d1o3.Name = "d1o3";
            this.d1o3.Size = new System.Drawing.Size(82, 357);
            this.d1o3.TabIndex = 76;
            this.d1o3.Visible = false;
            // 
            // d1o5
            // 
            this.d1o5.BackColor = System.Drawing.Color.DarkRed;
            this.d1o5.Location = new System.Drawing.Point(249, -3);
            this.d1o5.Name = "d1o5";
            this.d1o5.Size = new System.Drawing.Size(188, 57);
            this.d1o5.TabIndex = 78;
            this.d1o5.Visible = false;
            // 
            // d1o6
            // 
            this.d1o6.BackColor = System.Drawing.Color.DarkRed;
            this.d1o6.Location = new System.Drawing.Point(12, 296);
            this.d1o6.Name = "d1o6";
            this.d1o6.Size = new System.Drawing.Size(188, 57);
            this.d1o6.TabIndex = 79;
            this.d1o6.Visible = false;
            // 
            // d1o7
            // 
            this.d1o7.BackColor = System.Drawing.Color.DarkRed;
            this.d1o7.Location = new System.Drawing.Point(249, 261);
            this.d1o7.Name = "d1o7";
            this.d1o7.Size = new System.Drawing.Size(188, 92);
            this.d1o7.TabIndex = 80;
            this.d1o7.Visible = false;
           
            // 
            // d1o2
            // 
            this.d1o2.BackColor = System.Drawing.Color.DarkRed;
            this.d1o2.Location = new System.Drawing.Point(-8, -4);
            this.d1o2.Name = "d1o2";
            this.d1o2.Size = new System.Drawing.Size(50, 357);
            this.d1o2.TabIndex = 75;
            this.d1o2.Visible = false;
            // 
            // d1o4
            // 
            this.d1o4.BackColor = System.Drawing.Color.DarkRed;
            this.d1o4.Location = new System.Drawing.Point(12, -3);
            this.d1o4.Name = "d1o4";
            this.d1o4.Size = new System.Drawing.Size(284, 57);
            this.d1o4.TabIndex = 77;
            this.d1o4.Visible = false;
            // 
            // d1o8
            // 
            this.d1o8.BackColor = System.Drawing.Color.DarkRed;
            this.d1o8.Location = new System.Drawing.Point(341, 191);
            this.d1o8.Name = "d1o8";
            this.d1o8.Size = new System.Drawing.Size(63, 110);
            this.d1o8.TabIndex = 141;
            this.d1o8.Visible = false;
            // 
            // d1o9
            // 
            this.d1o9.BackColor = System.Drawing.Color.DarkRed;
            this.d1o9.Location = new System.Drawing.Point(341, 54);
            this.d1o9.Name = "d1o9";
            this.d1o9.Size = new System.Drawing.Size(63, 110);
            this.d1o9.TabIndex = 142;
            this.d1o9.Visible = false;
            // 
            // d1o10
            // 
            this.d1o10.BackColor = System.Drawing.Color.DarkRed;
            this.d1o10.Location = new System.Drawing.Point(312, 19);
            this.d1o10.Name = "d1o10";
            this.d1o10.Size = new System.Drawing.Size(63, 110);
            this.d1o10.TabIndex = 143;
            this.d1o10.Visible = false;
            // 
            // d1o11
            // 
            this.d1o11.BackColor = System.Drawing.Color.DarkRed;
            this.d1o11.Location = new System.Drawing.Point(252, 50);
            this.d1o11.Name = "d1o11";
            this.d1o11.Size = new System.Drawing.Size(60, 42);
            this.d1o11.TabIndex = 144;
            this.d1o11.Visible = false;
            // 
            // warpd2fd3
            // 
            this.warpd2fd3.BackColor = System.Drawing.Color.Cyan;
            this.warpd2fd3.Location = new System.Drawing.Point(208, 305);
            this.warpd2fd3.Name = "warpd2fd3";
            this.warpd2fd3.Size = new System.Drawing.Size(28, 18);
            this.warpd2fd3.TabIndex = 81;
            this.warpd2fd3.Visible = false;


            if (victory == false)
            {
                // 
                // boss
                // 
                this.boss.BackColor = System.Drawing.Color.Transparent;
                this.boss.Image = global::ZeldaDemo.Properties.Resources.boss;
                this.boss.Location = new System.Drawing.Point(252, 143);
                this.boss.Name = "boss";
                this.boss.Size = new System.Drawing.Size(49, 60);
                this.boss.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.boss.TabIndex = 145;
                this.boss.TabStop = false;
                bossscream.Play();
            }



            this.BackgroundImage = global::ZeldaDemo.Properties.Resources.d3;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BackColor = System.Drawing.Color.LightSeaGreen;
            warped = false;

            }
#endregion


            #region load dungeon 2

            if (roomid == 5 && warped == true)
            {
                incave = 1;
                knightHealth = 20;
                benHealth[0] = 5;
                benHealth[1] = 5;
                knightSystem = 0;
                bensystem[0] = 0;
                bensystem[1] = 0;
                benTimer.Start();
                orTimer.Stop();
                skelTimer.Stop();
                bossTimer.Stop();

                if (warptod2from == 1)
                {
                    // 
                    // leftidle
                    // 
                    this.leftidle.BackColor = System.Drawing.Color.Transparent;
                    this.leftidle.Image = global::ZeldaDemo.Properties.Resources.leftidle;
                    this.leftidle.Location = new System.Drawing.Point(211, 254);
                    this.leftidle.Name = "leftidle";
                    this.leftidle.Size = new System.Drawing.Size(26, 29);
                    this.leftidle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.leftidle.TabIndex = 10;
                    this.leftidle.TabStop = false;
                    // 
                    // rightidle
                    // 
                    this.rightidle.BackColor = System.Drawing.Color.Transparent;
                    this.rightidle.Image = global::ZeldaDemo.Properties.Resources.rightidle;
                    this.rightidle.Location = new System.Drawing.Point(211, 254);
                    this.rightidle.Name = "rightidle";
                    this.rightidle.Size = new System.Drawing.Size(26, 29);
                    this.rightidle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.rightidle.TabIndex = 11;
                    this.rightidle.TabStop = false;
                    // 
                    // upidle
                    // 
                    this.upidle.BackColor = System.Drawing.Color.Transparent;
                    this.upidle.Image = global::ZeldaDemo.Properties.Resources.upidle;
                    this.upidle.Location = new System.Drawing.Point(211, 254);
                    this.upidle.Name = "upidle";
                    this.upidle.Size = new System.Drawing.Size(26, 29);
                    this.upidle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.upidle.TabIndex = 12;
                    this.upidle.TabStop = false;
                    // 
                    // downidle
                    // 
                    this.downidle.BackColor = System.Drawing.Color.Transparent;
                    this.downidle.Image = global::ZeldaDemo.Properties.Resources.downidle;
                    this.downidle.Location = new System.Drawing.Point(211, 254);
                    this.downidle.Name = "downidle";
                    this.downidle.Size = new System.Drawing.Size(26, 29);
                    this.downidle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.downidle.TabIndex = 13;
                    this.downidle.TabStop = false;
                    // 
                    // downani
                    // 
                    this.downani.BackColor = System.Drawing.Color.Transparent;
                    this.downani.Image = global::ZeldaDemo.Properties.Resources.downani;
                    this.downani.Location = new System.Drawing.Point(211, 254);
                    this.downani.Name = "downani";
                    this.downani.Size = new System.Drawing.Size(26, 29);
                    this.downani.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.downani.TabIndex = 14;
                    this.downani.TabStop = false;
                    this.downani.Visible = false;
                    // 
                    // leftani
                    // 
                    this.leftani.BackColor = System.Drawing.Color.Transparent;
                    this.leftani.Image = global::ZeldaDemo.Properties.Resources.leftani;
                    this.leftani.Location = new System.Drawing.Point(211, 254);
                    this.leftani.Name = "leftani";
                    this.leftani.Size = new System.Drawing.Size(26, 29);
                    this.leftani.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.leftani.TabIndex = 15;
                    this.leftani.TabStop = false;
                    this.leftani.Visible = false;
                    // 
                    // rightani
                    // 
                    this.rightani.BackColor = System.Drawing.Color.Transparent;
                    this.rightani.Image = global::ZeldaDemo.Properties.Resources.rightani;
                    this.rightani.Location = new System.Drawing.Point(211, 254);
                    this.rightani.Name = "rightani";
                    this.rightani.Size = new System.Drawing.Size(26, 29);
                    this.rightani.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.rightani.TabIndex = 16;
                    this.rightani.TabStop = false;
                    this.rightani.Visible = false;
                    // 
                    // upani
                    // 
                    this.upani.BackColor = System.Drawing.Color.Transparent;
                    this.upani.Image = global::ZeldaDemo.Properties.Resources.upani;
                    this.upani.Location = new System.Drawing.Point(211, 254);
                    this.upani.Name = "upani";
                    this.upani.Size = new System.Drawing.Size(26, 29);
                    this.upani.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.upani.TabIndex = 17;
                    this.upani.TabStop = false;
                    this.upani.Visible = false;
                    // 
                    // swingdown
                    // 
                    this.swingdown.BackColor = System.Drawing.Color.Transparent;
                    this.swingdown.Image = global::ZeldaDemo.Properties.Resources.swingdown;
                    this.swingdown.Location = new System.Drawing.Point(211, 254);
                    this.swingdown.Name = "swingdown";
                    this.swingdown.Size = new System.Drawing.Size(26, 42);
                    this.swingdown.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.swingdown.TabIndex = 70;
                    this.swingdown.TabStop = false;
                    this.swingdown.Visible = false;
                    // 
                    // swingleft
                    // 
                    this.swingleft.BackColor = System.Drawing.Color.Transparent;
                    this.swingleft.Image = global::ZeldaDemo.Properties.Resources.swingleft;
                    this.swingleft.Location = new System.Drawing.Point(193, 254);
                    this.swingleft.Name = "swingleft";
                    this.swingleft.Size = new System.Drawing.Size(44, 29);
                    this.swingleft.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.swingleft.TabIndex = 71;
                    this.swingleft.TabStop = false;
                    this.swingleft.Visible = false;
                    // 
                    // swingright
                    // 
                    this.swingright.BackColor = System.Drawing.Color.Transparent;
                    this.swingright.Image = global::ZeldaDemo.Properties.Resources.swingright;
                    this.swingright.Location = new System.Drawing.Point(211, 254);
                    this.swingright.Name = "swingright";
                    this.swingright.Size = new System.Drawing.Size(42, 29);
                    this.swingright.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.swingright.TabIndex = 72;
                    this.swingright.TabStop = false;
                    this.swingright.Visible = false;
                    // 
                    // swingup
                    // 
                    this.swingup.BackColor = System.Drawing.Color.Transparent;
                    this.swingup.Image = global::ZeldaDemo.Properties.Resources.swingup;
                    this.swingup.Location = new System.Drawing.Point(211, 241);
                    this.swingup.Name = "swingup";
                    this.swingup.Size = new System.Drawing.Size(26, 42);
                    this.swingup.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.swingup.TabIndex = 73;
                    this.swingup.TabStop = false;
                    this.swingup.Visible = false;
                }

                else if (warptod2from == 2)
                {
                    // 
                    // leftidle
                    // 
                    this.leftidle.BackColor = System.Drawing.Color.Transparent;
                    this.leftidle.Image = global::ZeldaDemo.Properties.Resources.leftidle;
                    this.leftidle.Location = new System.Drawing.Point(210, 50);
                    this.leftidle.Name = "leftidle";
                    this.leftidle.Size = new System.Drawing.Size(26, 29);
                    this.leftidle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.leftidle.TabIndex = 10;
                    this.leftidle.TabStop = false;
                    // 
                    // rightidle
                    // 
                    this.rightidle.BackColor = System.Drawing.Color.Transparent;
                    this.rightidle.Image = global::ZeldaDemo.Properties.Resources.rightidle;
                    this.rightidle.Location = new System.Drawing.Point(210, 50);
                    this.rightidle.Name = "rightidle";
                    this.rightidle.Size = new System.Drawing.Size(26, 29);
                    this.rightidle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.rightidle.TabIndex = 11;
                    this.rightidle.TabStop = false;
                    // 
                    // upidle
                    // 
                    this.upidle.BackColor = System.Drawing.Color.Transparent;
                    this.upidle.Image = global::ZeldaDemo.Properties.Resources.upidle;
                    this.upidle.Location = new System.Drawing.Point(210, 50);
                    this.upidle.Name = "upidle";
                    this.upidle.Size = new System.Drawing.Size(26, 29);
                    this.upidle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.upidle.TabIndex = 12;
                    this.upidle.TabStop = false;
                    // 
                    // downidle
                    // 
                    this.downidle.BackColor = System.Drawing.Color.Transparent;
                    this.downidle.Image = global::ZeldaDemo.Properties.Resources.downidle;
                    this.downidle.Location = new System.Drawing.Point(210, 50);
                    this.downidle.Name = "downidle";
                    this.downidle.Size = new System.Drawing.Size(26, 29);
                    this.downidle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.downidle.TabIndex = 13;
                    this.downidle.TabStop = false;
                    // 
                    // downani
                    // 
                    this.downani.BackColor = System.Drawing.Color.Transparent;
                    this.downani.Image = global::ZeldaDemo.Properties.Resources.downani;
                    this.downani.Location = new System.Drawing.Point(210, 50);
                    this.downani.Name = "downani";
                    this.downani.Size = new System.Drawing.Size(26, 29);
                    this.downani.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.downani.TabIndex = 14;
                    this.downani.TabStop = false;
                    this.downani.Visible = false;
                    // 
                    // leftani
                    // 
                    this.leftani.BackColor = System.Drawing.Color.Transparent;
                    this.leftani.Image = global::ZeldaDemo.Properties.Resources.leftani;
                    this.leftani.Location = new System.Drawing.Point(210, 50);
                    this.leftani.Name = "leftani";
                    this.leftani.Size = new System.Drawing.Size(26, 29);
                    this.leftani.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.leftani.TabIndex = 15;
                    this.leftani.TabStop = false;
                    this.leftani.Visible = false;
                    // 
                    // rightani
                    // 
                    this.rightani.BackColor = System.Drawing.Color.Transparent;
                    this.rightani.Image = global::ZeldaDemo.Properties.Resources.rightani;
                    this.rightani.Location = new System.Drawing.Point(210, 50);
                    this.rightani.Name = "rightani";
                    this.rightani.Size = new System.Drawing.Size(26, 29);
                    this.rightani.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.rightani.TabIndex = 16;
                    this.rightani.TabStop = false;
                    this.rightani.Visible = false;
                    // 
                    // upani
                    // 
                    this.upani.BackColor = System.Drawing.Color.Transparent;
                    this.upani.Image = global::ZeldaDemo.Properties.Resources.upani;
                    this.upani.Location = new System.Drawing.Point(210, 50);
                    this.upani.Name = "upani";
                    this.upani.Size = new System.Drawing.Size(26, 29);
                    this.upani.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.upani.TabIndex = 17;
                    this.upani.TabStop = false;
                    this.upani.Visible = false;

                    // 
                    // swingdown
                    // 
                    this.swingdown.BackColor = System.Drawing.Color.Transparent;
                    this.swingdown.Image = global::ZeldaDemo.Properties.Resources.swingdown;
                    this.swingdown.Location = new System.Drawing.Point(210, 50);
                    this.swingdown.Name = "swingdown";
                    this.swingdown.Size = new System.Drawing.Size(26, 42);
                    this.swingdown.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.swingdown.TabIndex = 70;
                    this.swingdown.TabStop = false;
                    this.swingdown.Visible = false;
                    // 
                    // swingleft
                    // 
                    this.swingleft.BackColor = System.Drawing.Color.Transparent;
                    this.swingleft.Image = global::ZeldaDemo.Properties.Resources.swingleft;
                    this.swingleft.Location = new System.Drawing.Point(192, 50);
                    this.swingleft.Name = "swingleft";
                    this.swingleft.Size = new System.Drawing.Size(44, 29);
                    this.swingleft.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.swingleft.TabIndex = 71;
                    this.swingleft.TabStop = false;
                    this.swingleft.Visible = false;
                    // 
                    // swingright
                    // 
                    this.swingright.BackColor = System.Drawing.Color.Transparent;
                    this.swingright.Image = global::ZeldaDemo.Properties.Resources.swingright;
                    this.swingright.Location = new System.Drawing.Point(210, 50);
                    this.swingright.Name = "swingright";
                    this.swingright.Size = new System.Drawing.Size(42, 29);
                    this.swingright.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.swingright.TabIndex = 72;
                    this.swingright.TabStop = false;
                    this.swingright.Visible = false;
                    // 
                    // swingup
                    // 
                    this.swingup.BackColor = System.Drawing.Color.Transparent;
                    this.swingup.Image = global::ZeldaDemo.Properties.Resources.swingup;
                    this.swingup.Location = new System.Drawing.Point(210, 37);
                    this.swingup.Name = "swingup";
                    this.swingup.Size = new System.Drawing.Size(26, 42);
                    this.swingup.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.swingup.TabIndex = 73;
                    this.swingup.TabStop = false;
                    this.swingup.Visible = false;
                }

                // 
                // d1o1
                // 
                this.d1o1.BackColor = System.Drawing.Color.DarkRed;
                this.d1o1.Location = new System.Drawing.Point(189, 119);
                this.d1o1.Name = "d1o1";
                this.d1o1.Size = new System.Drawing.Size(63, 110);
                this.d1o1.TabIndex = 74;
                this.d1o1.Visible = false;
                // 
                // d1o2
                // 
                this.d1o2.BackColor = System.Drawing.Color.DarkRed;
                this.d1o2.Location = new System.Drawing.Point(-8, -4);
                this.d1o2.Name = "d1o2";
                this.d1o2.Size = new System.Drawing.Size(50, 357);
                this.d1o2.TabIndex = 75;
                this.d1o2.Visible = false;
                // 
                // d1o3
                // 
                this.d1o3.BackColor = System.Drawing.Color.DarkRed;
                this.d1o3.Location = new System.Drawing.Point(404, -4);
                this.d1o3.Name = "d1o3";
                this.d1o3.Size = new System.Drawing.Size(50, 357);
                this.d1o3.TabIndex = 76;
                this.d1o3.Visible = false;
                // 
                // d1o4
                // 
                this.d1o4.BackColor = System.Drawing.Color.DarkRed;
                this.d1o4.Location = new System.Drawing.Point(12, -3);
                this.d1o4.Name = "d1o4";
                this.d1o4.Size = new System.Drawing.Size(188, 57);
                this.d1o4.TabIndex = 77;
                this.d1o4.Visible = false;
                // 
                // d1o5
                // 
                this.d1o5.BackColor = System.Drawing.Color.DarkRed;
                this.d1o5.Location = new System.Drawing.Point(249, -3);
                this.d1o5.Name = "d1o5";
                this.d1o5.Size = new System.Drawing.Size(188, 57);
                this.d1o5.TabIndex = 78;
                this.d1o5.Visible = false;
                // 
                // d1o6
                // 
                this.d1o6.BackColor = System.Drawing.Color.DarkRed;
                this.d1o6.Location = new System.Drawing.Point(12, 296);
                this.d1o6.Name = "d1o6";
                this.d1o6.Size = new System.Drawing.Size(188, 57);
                this.d1o6.TabIndex = 79;
                this.d1o6.Visible = false;
                // 
                // d1o7
                // 
                this.d1o7.BackColor = System.Drawing.Color.DarkRed;
                this.d1o7.Location = new System.Drawing.Point(249, 296);
                this.d1o7.Name = "d1o7";
                this.d1o7.Size = new System.Drawing.Size(188, 57);
                this.d1o7.TabIndex = 80;
                this.d1o7.Visible = false;

                // 
                // warpd1fd2
                // 
                this.warpd1fd2.BackColor = System.Drawing.Color.Cyan;
                this.warpd1fd2.Location = new System.Drawing.Point(208, 305);
                this.warpd1fd2.Name = "warpd1fd2";
                this.warpd1fd2.Size = new System.Drawing.Size(28, 18);
                this.warpd1fd2.TabIndex = 81;
                this.warpd1fd2.Visible = false;

                this.BackgroundImage = global::ZeldaDemo.Properties.Resources.d2;
                this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                this.BackColor = System.Drawing.Color.LightSeaGreen;
                warped = false;

                // 
                // ben1d
                // 
                this.ben1d.BackColor = System.Drawing.Color.Transparent;
                this.ben1d.Image = global::ZeldaDemo.Properties.Resources.ben1;
                this.ben1d.Location = new System.Drawing.Point(104, 143);
                this.ben1d.Name = "ben1d";
                this.ben1d.Size = new System.Drawing.Size(25, 26);
                this.ben1d.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.ben1d.TabIndex = 87;
                this.ben1d.TabStop = false;
                // 
                // ben1u
                // 
                this.ben1u.BackColor = System.Drawing.Color.Transparent;
                this.ben1u.Image = global::ZeldaDemo.Properties.Resources.ben2;
                this.ben1u.Location = new System.Drawing.Point(104, 143);
                this.ben1u.Name = "ben1u";
                this.ben1u.Size = new System.Drawing.Size(25, 26);
                this.ben1u.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.ben1u.TabIndex = 88;
                this.ben1u.TabStop = false;
                // 
                // ben2u
                // 
                this.ben2u.BackColor = System.Drawing.Color.Transparent;
                this.ben2u.Image = global::ZeldaDemo.Properties.Resources.ben2;
                this.ben2u.Location = new System.Drawing.Point(320, 143);
                this.ben2u.Name = "ben2u";
                this.ben2u.Size = new System.Drawing.Size(25, 26);
                this.ben2u.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.ben2u.TabIndex = 90;
                this.ben2u.TabStop = false;
                // 
                // ben2d
                // 
                this.ben2d.BackColor = System.Drawing.Color.Transparent;
                this.ben2d.Image = global::ZeldaDemo.Properties.Resources.ben1;
                this.ben2d.Location = new System.Drawing.Point(320, 143);
                this.ben2d.Name = "ben2d";
                this.ben2d.Size = new System.Drawing.Size(25, 26);
                this.ben2d.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.ben2d.TabIndex = 89;
                this.ben2d.TabStop = false;
                // 
                // knightl
                // 
                this.knightl.BackColor = System.Drawing.Color.Transparent;
                this.knightl.Image = global::ZeldaDemo.Properties.Resources.knightl;
                this.knightl.Location = new System.Drawing.Point(172, 80);
                this.knightl.Name = "knightl";
                this.knightl.Size = new System.Drawing.Size(25, 26);
                this.knightl.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.knightl.TabIndex = 139;
                this.knightl.TabStop = false;
                // 
                // knightr
                // 
                this.knightr.BackColor = System.Drawing.Color.Transparent;
                this.knightr.Image = global::ZeldaDemo.Properties.Resources.knightr;
                this.knightr.Location = new System.Drawing.Point(172, 80);
                this.knightr.Name = "knightr";
                this.knightr.Size = new System.Drawing.Size(25, 26);
                this.knightr.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.knightr.TabIndex = 138;
                this.knightr.TabStop = false;

                if (wisdom == false)
                {
                    // 
                    // barricade2
                    // 
                    this.barricade2.BackColor = System.Drawing.Color.Transparent;
                    this.barricade2.Image = global::ZeldaDemo.Properties.Resources.barricade1;
                    this.barricade2.Location = new System.Drawing.Point(207, 32);
                    this.barricade2.Name = "barricade2";
                    this.barricade2.Size = new System.Drawing.Size(31, 34);
                    this.barricade2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.barricade2.TabIndex = 85;
                    this.barricade2.TabStop = false;
                }

                // 
                // unlocker
                // 
                this.unlocker.BackColor = System.Drawing.Color.Black;
                this.unlocker.Location = new System.Drawing.Point(239, 216);
                this.unlocker.Name = "unlocker";
                this.unlocker.Size = new System.Drawing.Size(1, 1);
                this.unlocker.TabIndex = 140;


                this.BackgroundImage = global::ZeldaDemo.Properties.Resources.d2;
                this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                this.BackColor = System.Drawing.Color.LightSeaGreen;
                warped = false;

                 // 
            // warpD3
            // 
            this.warpD3.BackColor = System.Drawing.Color.Cyan;
            this.warpD3.Location = new System.Drawing.Point(209, 9);
            this.warpD3.Name = "warpD3";
            this.warpD3.Size = new System.Drawing.Size(28, 18);
            this.warpD3.TabIndex = 145;
            this.warpD3.Visible = false;
            }

            #endregion


            #region load dunegon 1
            if (roomid == 4 && warped == true)
            {

                if (incave == 1 == false)
                {
                    overworldbgm.URL = "sfx/dungeon.wav";
                    overworldbgm.Ctlcontrols.play();
                    overworldbgm.settings.setMode("loop", true);
                }

                incave = 1;
                skelHealth[0] = 3;
                skelHealth[1] = 3;
                skelHealth[2] = 3;
                skelsystem[0] = 0;
                skelsystem[1] = 0;
                skelsystem[2] = 0;
                skelTimer.Start();
                benTimer.Stop();
                orTimer.Stop();
                bossTimer.Stop();


                if (warptod1from == 1)
                {
                    // 
                    // leftidle
                    // 
                    this.leftidle.BackColor = System.Drawing.Color.Transparent;
                    this.leftidle.Image = global::ZeldaDemo.Properties.Resources.leftidle;
                    this.leftidle.Location = new System.Drawing.Point(211, 254);
                    this.leftidle.Name = "leftidle";
                    this.leftidle.Size = new System.Drawing.Size(26, 29);
                    this.leftidle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.leftidle.TabIndex = 10;
                    this.leftidle.TabStop = false;
                    // 
                    // rightidle
                    // 
                    this.rightidle.BackColor = System.Drawing.Color.Transparent;
                    this.rightidle.Image = global::ZeldaDemo.Properties.Resources.rightidle;
                    this.rightidle.Location = new System.Drawing.Point(211, 254);
                    this.rightidle.Name = "rightidle";
                    this.rightidle.Size = new System.Drawing.Size(26, 29);
                    this.rightidle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.rightidle.TabIndex = 11;
                    this.rightidle.TabStop = false;
                    // 
                    // upidle
                    // 
                    this.upidle.BackColor = System.Drawing.Color.Transparent;
                    this.upidle.Image = global::ZeldaDemo.Properties.Resources.upidle;
                    this.upidle.Location = new System.Drawing.Point(211, 254);
                    this.upidle.Name = "upidle";
                    this.upidle.Size = new System.Drawing.Size(26, 29);
                    this.upidle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.upidle.TabIndex = 12;
                    this.upidle.TabStop = false;
                    // 
                    // downidle
                    // 
                    this.downidle.BackColor = System.Drawing.Color.Transparent;
                    this.downidle.Image = global::ZeldaDemo.Properties.Resources.downidle;
                    this.downidle.Location = new System.Drawing.Point(211, 254);
                    this.downidle.Name = "downidle";
                    this.downidle.Size = new System.Drawing.Size(26, 29);
                    this.downidle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.downidle.TabIndex = 13;
                    this.downidle.TabStop = false;
                    // 
                    // downani
                    // 
                    this.downani.BackColor = System.Drawing.Color.Transparent;
                    this.downani.Image = global::ZeldaDemo.Properties.Resources.downani;
                    this.downani.Location = new System.Drawing.Point(211, 254);
                    this.downani.Name = "downani";
                    this.downani.Size = new System.Drawing.Size(26, 29);
                    this.downani.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.downani.TabIndex = 14;
                    this.downani.TabStop = false;
                    this.downani.Visible = false;
                    // 
                    // leftani
                    // 
                    this.leftani.BackColor = System.Drawing.Color.Transparent;
                    this.leftani.Image = global::ZeldaDemo.Properties.Resources.leftani;
                    this.leftani.Location = new System.Drawing.Point(211, 254);
                    this.leftani.Name = "leftani";
                    this.leftani.Size = new System.Drawing.Size(26, 29);
                    this.leftani.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.leftani.TabIndex = 15;
                    this.leftani.TabStop = false;
                    this.leftani.Visible = false;
                    // 
                    // rightani
                    // 
                    this.rightani.BackColor = System.Drawing.Color.Transparent;
                    this.rightani.Image = global::ZeldaDemo.Properties.Resources.rightani;
                    this.rightani.Location = new System.Drawing.Point(211, 254);
                    this.rightani.Name = "rightani";
                    this.rightani.Size = new System.Drawing.Size(26, 29);
                    this.rightani.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.rightani.TabIndex = 16;
                    this.rightani.TabStop = false;
                    this.rightani.Visible = false;
                    // 
                    // upani
                    // 
                    this.upani.BackColor = System.Drawing.Color.Transparent;
                    this.upani.Image = global::ZeldaDemo.Properties.Resources.upani;
                    this.upani.Location = new System.Drawing.Point(211, 254);
                    this.upani.Name = "upani";
                    this.upani.Size = new System.Drawing.Size(26, 29);
                    this.upani.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.upani.TabIndex = 17;
                    this.upani.TabStop = false;
                    this.upani.Visible = false;
                    // 
                    // swingdown
                    // 
                    this.swingdown.BackColor = System.Drawing.Color.Transparent;
                    this.swingdown.Image = global::ZeldaDemo.Properties.Resources.swingdown;
                    this.swingdown.Location = new System.Drawing.Point(211, 254);
                    this.swingdown.Name = "swingdown";
                    this.swingdown.Size = new System.Drawing.Size(26, 42);
                    this.swingdown.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.swingdown.TabIndex = 70;
                    this.swingdown.TabStop = false;
                    this.swingdown.Visible = false;
                    // 
                    // swingleft
                    // 
                    this.swingleft.BackColor = System.Drawing.Color.Transparent;
                    this.swingleft.Image = global::ZeldaDemo.Properties.Resources.swingleft;
                    this.swingleft.Location = new System.Drawing.Point(193, 254);
                    this.swingleft.Name = "swingleft";
                    this.swingleft.Size = new System.Drawing.Size(44, 29);
                    this.swingleft.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.swingleft.TabIndex = 71;
                    this.swingleft.TabStop = false;
                    this.swingleft.Visible = false;
                    // 
                    // swingright
                    // 
                    this.swingright.BackColor = System.Drawing.Color.Transparent;
                    this.swingright.Image = global::ZeldaDemo.Properties.Resources.swingright;
                    this.swingright.Location = new System.Drawing.Point(211, 254);
                    this.swingright.Name = "swingright";
                    this.swingright.Size = new System.Drawing.Size(42, 29);
                    this.swingright.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.swingright.TabIndex = 72;
                    this.swingright.TabStop = false;
                    this.swingright.Visible = false;
                    // 
                    // swingup
                    // 
                    this.swingup.BackColor = System.Drawing.Color.Transparent;
                    this.swingup.Image = global::ZeldaDemo.Properties.Resources.swingup;
                    this.swingup.Location = new System.Drawing.Point(211, 241);
                    this.swingup.Name = "swingup";
                    this.swingup.Size = new System.Drawing.Size(26, 42);
                    this.swingup.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.swingup.TabIndex = 73;
                    this.swingup.TabStop = false;
                    this.swingup.Visible = false;
                }
                else if (warptod1from == 2)
                {
                    // 
                    // leftidle
                    // 
                    this.leftidle.BackColor = System.Drawing.Color.Transparent;
                    this.leftidle.Image = global::ZeldaDemo.Properties.Resources.leftidle;
                    this.leftidle.Location = new System.Drawing.Point(210, 56);
                    this.leftidle.Name = "leftidle";
                    this.leftidle.Size = new System.Drawing.Size(26, 29);
                    this.leftidle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.leftidle.TabIndex = 10;
                    this.leftidle.TabStop = false;
                    // 
                    // rightidle
                    // 
                    this.rightidle.BackColor = System.Drawing.Color.Transparent;
                    this.rightidle.Image = global::ZeldaDemo.Properties.Resources.rightidle;
                    this.rightidle.Location = new System.Drawing.Point(210, 56);
                    this.rightidle.Name = "rightidle";
                    this.rightidle.Size = new System.Drawing.Size(26, 29);
                    this.rightidle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.rightidle.TabIndex = 11;
                    this.rightidle.TabStop = false;
                    // 
                    // upidle
                    // 
                    this.upidle.BackColor = System.Drawing.Color.Transparent;
                    this.upidle.Image = global::ZeldaDemo.Properties.Resources.upidle;
                    this.upidle.Location = new System.Drawing.Point(210, 56);
                    this.upidle.Name = "upidle";
                    this.upidle.Size = new System.Drawing.Size(26, 29);
                    this.upidle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.upidle.TabIndex = 12;
                    this.upidle.TabStop = false;
                    // 
                    // downidle
                    // 
                    this.downidle.BackColor = System.Drawing.Color.Transparent;
                    this.downidle.Image = global::ZeldaDemo.Properties.Resources.downidle;
                    this.downidle.Location = new System.Drawing.Point(210, 56);
                    this.downidle.Name = "downidle";
                    this.downidle.Size = new System.Drawing.Size(26, 29);
                    this.downidle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.downidle.TabIndex = 13;
                    this.downidle.TabStop = false;
                    // 
                    // downani
                    // 
                    this.downani.BackColor = System.Drawing.Color.Transparent;
                    this.downani.Image = global::ZeldaDemo.Properties.Resources.downani;
                    this.downani.Location = new System.Drawing.Point(210, 56);
                    this.downani.Name = "downani";
                    this.downani.Size = new System.Drawing.Size(26, 29);
                    this.downani.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.downani.TabIndex = 14;
                    this.downani.TabStop = false;
                    this.downani.Visible = false;
                    // 
                    // leftani
                    // 
                    this.leftani.BackColor = System.Drawing.Color.Transparent;
                    this.leftani.Image = global::ZeldaDemo.Properties.Resources.leftani;
                    this.leftani.Location = new System.Drawing.Point(210, 56);
                    this.leftani.Name = "leftani";
                    this.leftani.Size = new System.Drawing.Size(26, 29);
                    this.leftani.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.leftani.TabIndex = 15;
                    this.leftani.TabStop = false;
                    this.leftani.Visible = false;
                    // 
                    // rightani
                    // 
                    this.rightani.BackColor = System.Drawing.Color.Transparent;
                    this.rightani.Image = global::ZeldaDemo.Properties.Resources.rightani;
                    this.rightani.Location = new System.Drawing.Point(210, 56);
                    this.rightani.Name = "rightani";
                    this.rightani.Size = new System.Drawing.Size(26, 29);
                    this.rightani.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.rightani.TabIndex = 16;
                    this.rightani.TabStop = false;
                    this.rightani.Visible = false;
                    // 
                    // upani
                    // 
                    this.upani.BackColor = System.Drawing.Color.Transparent;
                    this.upani.Image = global::ZeldaDemo.Properties.Resources.upani;
                    this.upani.Location = new System.Drawing.Point(210, 56);
                    this.upani.Name = "upani";
                    this.upani.Size = new System.Drawing.Size(26, 29);
                    this.upani.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.upani.TabIndex = 17;
                    this.upani.TabStop = false;
                    this.upani.Visible = false;

                    // 
                    // swingdown
                    // 
                    this.swingdown.BackColor = System.Drawing.Color.Transparent;
                    this.swingdown.Image = global::ZeldaDemo.Properties.Resources.swingdown;
                    this.swingdown.Location = new System.Drawing.Point(210, 56);
                    this.swingdown.Name = "swingdown";
                    this.swingdown.Size = new System.Drawing.Size(26, 42);
                    this.swingdown.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.swingdown.TabIndex = 70;
                    this.swingdown.TabStop = false;
                    this.swingdown.Visible = false;
                    // 
                    // swingleft
                    // 
                    this.swingleft.BackColor = System.Drawing.Color.Transparent;
                    this.swingleft.Image = global::ZeldaDemo.Properties.Resources.swingleft;
                    this.swingleft.Location = new System.Drawing.Point(192, 56);
                    this.swingleft.Name = "swingleft";
                    this.swingleft.Size = new System.Drawing.Size(44, 29);
                    this.swingleft.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.swingleft.TabIndex = 71;
                    this.swingleft.TabStop = false;
                    this.swingleft.Visible = false;
                    // 
                    // swingright
                    // 
                    this.swingright.BackColor = System.Drawing.Color.Transparent;
                    this.swingright.Image = global::ZeldaDemo.Properties.Resources.swingright;
                    this.swingright.Location = new System.Drawing.Point(210, 56);
                    this.swingright.Name = "swingright";
                    this.swingright.Size = new System.Drawing.Size(42, 29);
                    this.swingright.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.swingright.TabIndex = 72;
                    this.swingright.TabStop = false;
                    this.swingright.Visible = false;
                    // 
                    // swingup
                    // 
                    this.swingup.BackColor = System.Drawing.Color.Transparent;
                    this.swingup.Image = global::ZeldaDemo.Properties.Resources.swingup;
                    this.swingup.Location = new System.Drawing.Point(210, 43);
                    this.swingup.Name = "swingup";
                    this.swingup.Size = new System.Drawing.Size(26, 42);
                    this.swingup.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.swingup.TabIndex = 73;
                    this.swingup.TabStop = false;
                    this.swingup.Visible = false;
                }
            // 
            // d1o1
            // 
            this.d1o1.BackColor = System.Drawing.Color.DarkRed;
            this.d1o1.Location = new System.Drawing.Point(189, 119);
            this.d1o1.Name = "d1o1";
            this.d1o1.Size = new System.Drawing.Size(63, 110);
            this.d1o1.TabIndex = 74;
            this.d1o1.Visible = false;
            // 
            // d1o2
            // 
            this.d1o2.BackColor = System.Drawing.Color.DarkRed;
            this.d1o2.Location = new System.Drawing.Point(-8, -4);
            this.d1o2.Name = "d1o2";
            this.d1o2.Size = new System.Drawing.Size(50, 357);
            this.d1o2.TabIndex = 75;
            this.d1o2.Visible = false;
            // 
            // d1o3
            // 
            this.d1o3.BackColor = System.Drawing.Color.DarkRed;
            this.d1o3.Location = new System.Drawing.Point(404, -4);
            this.d1o3.Name = "d1o3";
            this.d1o3.Size = new System.Drawing.Size(50, 357);
            this.d1o3.TabIndex = 76;
            this.d1o3.Visible = false;
            // 
            // d1o4
            // 
            this.d1o4.BackColor = System.Drawing.Color.DarkRed;
            this.d1o4.Location = new System.Drawing.Point(12, -3);
            this.d1o4.Name = "d1o4";
            this.d1o4.Size = new System.Drawing.Size(188, 57);
            this.d1o4.TabIndex = 77;
            this.d1o4.Visible = false;
            // 
            // d1o5
            // 
            this.d1o5.BackColor = System.Drawing.Color.DarkRed;
            this.d1o5.Location = new System.Drawing.Point(249, -3);
            this.d1o5.Name = "d1o5";
            this.d1o5.Size = new System.Drawing.Size(188, 57);
            this.d1o5.TabIndex = 78;
            this.d1o5.Visible = false;
            // 
            // d1o6
            // 
            this.d1o6.BackColor = System.Drawing.Color.DarkRed;
            this.d1o6.Location = new System.Drawing.Point(12, 296);
            this.d1o6.Name = "d1o6";
            this.d1o6.Size = new System.Drawing.Size(188, 57);
            this.d1o6.TabIndex = 79;
            this.d1o6.Visible = false;
            // 
            // d1o7
            // 
            this.d1o7.BackColor = System.Drawing.Color.DarkRed;
            this.d1o7.Location = new System.Drawing.Point(249, 296);
            this.d1o7.Name = "d1o7";
            this.d1o7.Size = new System.Drawing.Size(188, 57);
            this.d1o7.TabIndex = 80;
            this.d1o7.Visible = false;
            // 
            // warpa3fd1
            // 
            this.warpa3fd1.BackColor = System.Drawing.Color.Cyan;
            this.warpa3fd1.Location = new System.Drawing.Point(208, 305);
            this.warpa3fd1.Name = "warpa3fd1";
            this.warpa3fd1.Size = new System.Drawing.Size(28, 18);
            this.warpa3fd1.TabIndex = 81;
            this.warpa3fd1.Visible = false;


            // 
            // skel1
            // 
            this.skel1.BackColor = System.Drawing.Color.Transparent;
            this.skel1.Image = global::ZeldaDemo.Properties.Resources.skelly;
            this.skel1.Location = new System.Drawing.Point(96, 153);
            this.skel1.Name = "skel1";
            this.skel1.Size = new System.Drawing.Size(25, 26);
            this.skel1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.skel1.TabIndex = 82;
            this.skel1.TabStop = false;
            // 
            // skel2
            // 
            this.skel2.BackColor = System.Drawing.Color.Transparent;
            this.skel2.Image = global::ZeldaDemo.Properties.Resources.skelly;
            this.skel2.Location = new System.Drawing.Point(172, 86);
            this.skel2.Name = "skel2";
            this.skel2.Size = new System.Drawing.Size(25, 26);
            this.skel2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.skel2.TabIndex = 83;
            this.skel2.TabStop = false;
            // 
            // skel3
            // 
            this.skel3.BackColor = System.Drawing.Color.Transparent;
            this.skel3.Image = global::ZeldaDemo.Properties.Resources.skelly;
            this.skel3.Location = new System.Drawing.Point(338, 153);
            this.skel3.Name = "skel3";
            this.skel3.Size = new System.Drawing.Size(25, 26);
            this.skel3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.skel3.TabIndex = 84;
            this.skel3.TabStop = false;

            // 
            // warpD2
            // 
            this.warpD2.BackColor = System.Drawing.Color.Cyan;
            this.warpD2.Location = new System.Drawing.Point(209, 19);
            this.warpD2.Name = "warpD2";
            this.warpD2.Size = new System.Drawing.Size(28, 18);
            this.warpD2.TabIndex = 86;
            this.warpD2.Visible = false;



            if (courage == false)
            {
                // 
                // barricade
                // 
                this.barricade.BackColor = System.Drawing.Color.Transparent;
                this.barricade.Image = global::ZeldaDemo.Properties.Resources.barricade1;
                this.barricade.Location = new System.Drawing.Point(207, 32);
                this.barricade.Name = "barricade";
                this.barricade.Size = new System.Drawing.Size(31, 34);
                this.barricade.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.barricade.TabIndex = 85;
                this.barricade.TabStop = false;
            }

            this.BackgroundImage = global::ZeldaDemo.Properties.Resources.d2;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BackColor = System.Drawing.Color.LightSeaGreen;
            warped = false;



            }

            #endregion


            #region load area 3

            if (roomid == 3 && warped == true)
            {
                
                orHealth[0] = 2;
                orHealth[1] = 2;
                orsystem[0] = 0;
                orsystem[0] = 0;
                orrocksystem[0] = 0;
                orrocksystem[0] = 0;
                or1u.Visible = false;
                or1d.Visible = true;
                or2l.Visible = false;
                or2r.Visible = true;
                orTimer.Start();
                benTimer.Stop();
                skelTimer.Stop();
                bossTimer.Stop();

                if (incave == 1) //overworld gbm fix
                {
                    overworldbgm.URL = "sfx/overworld.wav";
                    overworldbgm.Ctlcontrols.play();
                    overworldbgm.settings.setMode("loop", true);
                    incave = 0;
                }

                
                if (warptoa3from == 1)
                {
                // 
                // leftidle
                // 
                this.leftidle.BackColor = System.Drawing.Color.Transparent;
                this.leftidle.Image = global::ZeldaDemo.Properties.Resources.leftidle;
                this.leftidle.Location = new System.Drawing.Point(213, 264);
                this.leftidle.Name = "leftidle";
                this.leftidle.Size = new System.Drawing.Size(26, 29);
                this.leftidle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.leftidle.TabIndex = 10;
                this.leftidle.TabStop = false;
                // 
                // rightidle
                // 
                this.rightidle.BackColor = System.Drawing.Color.Transparent;
                this.rightidle.Image = global::ZeldaDemo.Properties.Resources.rightidle;
                this.rightidle.Location = new System.Drawing.Point(213, 264);
                this.rightidle.Name = "rightidle";
                this.rightidle.Size = new System.Drawing.Size(26, 29);
                this.rightidle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.rightidle.TabIndex = 11;
                this.rightidle.TabStop = false;
                // 
                // upidle
                // 
                this.upidle.BackColor = System.Drawing.Color.Transparent;
                this.upidle.Image = global::ZeldaDemo.Properties.Resources.upidle;
                this.upidle.Location = new System.Drawing.Point(213, 264);
                this.upidle.Name = "upidle";
                this.upidle.Size = new System.Drawing.Size(26, 29);
                this.upidle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.upidle.TabIndex = 12;
                this.upidle.TabStop = false;
                // 
                // downidle
                // 
                this.downidle.BackColor = System.Drawing.Color.Transparent;
                this.downidle.Image = global::ZeldaDemo.Properties.Resources.downidle;
                this.downidle.Location = new System.Drawing.Point(213, 264);
                this.downidle.Name = "downidle";
                this.downidle.Size = new System.Drawing.Size(26, 29);
                this.downidle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.downidle.TabIndex = 13;
                // 
                // downani
                // 
                this.downani.BackColor = System.Drawing.Color.Transparent;
                this.downani.Image = global::ZeldaDemo.Properties.Resources.downani;
                this.downani.Location = new System.Drawing.Point(213, 264);
                this.downani.Name = "downani";
                this.downani.Size = new System.Drawing.Size(26, 29);
                this.downani.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.downani.TabIndex = 14;
                this.downani.TabStop = false;
                this.downani.Visible = false;
                // 
                // leftani
                // 
                this.leftani.BackColor = System.Drawing.Color.Transparent;
                this.leftani.Image = global::ZeldaDemo.Properties.Resources.leftani;
                this.leftani.Location = new System.Drawing.Point(213, 264);
                this.leftani.Name = "leftani";
                this.leftani.Size = new System.Drawing.Size(26, 29);
                this.leftani.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.leftani.TabIndex = 15;
                this.leftani.TabStop = false;
                this.leftani.Visible = false;
                // 
                // rightani
                // 
                this.rightani.BackColor = System.Drawing.Color.Transparent;
                this.rightani.Image = global::ZeldaDemo.Properties.Resources.rightani;
                this.rightani.Location = new System.Drawing.Point(213, 264);
                this.rightani.Name = "rightani";
                this.rightani.Size = new System.Drawing.Size(26, 29);
                this.rightani.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.rightani.TabIndex = 16;
                this.rightani.TabStop = false;
                this.rightani.Visible = false;
                // 
                // upani
                // 
                this.upani.BackColor = System.Drawing.Color.Transparent;
                this.upani.Image = global::ZeldaDemo.Properties.Resources.upani;
                this.upani.Location = new System.Drawing.Point(213, 264);
                this.upani.Name = "upani";
                this.upani.Size = new System.Drawing.Size(26, 29);
                this.upani.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.upani.TabIndex = 17;
                this.upani.TabStop = false;
                this.upani.Visible = false;

                // 
                // swingdown
                // 
                this.swingdown.BackColor = System.Drawing.Color.Transparent;
                this.swingdown.Image = global::ZeldaDemo.Properties.Resources.swingdown;
                this.swingdown.Location = new System.Drawing.Point(213, 264);
                this.swingdown.Name = "swingdown";
                this.swingdown.Size = new System.Drawing.Size(26, 42);
                this.swingdown.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.swingdown.TabIndex = 70;
                this.swingdown.TabStop = false;
                this.swingdown.Visible = false;
                // 
                // swingleft
                // 
                this.swingleft.BackColor = System.Drawing.Color.Transparent;
                this.swingleft.Image = global::ZeldaDemo.Properties.Resources.swingleft;
                this.swingleft.Location = new System.Drawing.Point(195, 264);
                this.swingleft.Name = "swingleft";
                this.swingleft.Size = new System.Drawing.Size(44, 29);
                this.swingleft.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.swingleft.TabIndex = 71;
                this.swingleft.TabStop = false;
                this.swingleft.Visible = false;
                // 
                // swingright
                // 
                this.swingright.BackColor = System.Drawing.Color.Transparent;
                this.swingright.Image = global::ZeldaDemo.Properties.Resources.swingright;
                this.swingright.Location = new System.Drawing.Point(213, 264);
                this.swingright.Name = "swingright";
                this.swingright.Size = new System.Drawing.Size(42, 29);
                this.swingright.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.swingright.TabIndex = 72;
                this.swingright.TabStop = false;
                this.swingright.Visible = false;
                // 
                // swingup
                // 
                this.swingup.BackColor = System.Drawing.Color.Transparent;
                this.swingup.Image = global::ZeldaDemo.Properties.Resources.swingup;
                this.swingup.Location = new System.Drawing.Point(213, 251);
                this.swingup.Name = "swingup";
                this.swingup.Size = new System.Drawing.Size(26, 42);
                this.swingup.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.swingup.TabIndex = 73;
                this.swingup.TabStop = false;
                this.swingup.Visible = false;
                }

                else if (warptoa3from == 2)
                {
            // 
            // leftidle
            // 
            this.leftidle.BackColor = System.Drawing.Color.Transparent;
            this.leftidle.Image = global::ZeldaDemo.Properties.Resources.leftidle;
            this.leftidle.Location = new System.Drawing.Point(190, 77);
            this.leftidle.Name = "leftidle";
            this.leftidle.Size = new System.Drawing.Size(26, 29);
            this.leftidle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.leftidle.TabIndex = 10;
            this.leftidle.TabStop = false;
            // 
            // rightidle
            // 
            this.rightidle.BackColor = System.Drawing.Color.Transparent;
            this.rightidle.Image = global::ZeldaDemo.Properties.Resources.rightidle;
            this.rightidle.Location = new System.Drawing.Point(190, 77);
            this.rightidle.Name = "rightidle";
            this.rightidle.Size = new System.Drawing.Size(26, 29);
            this.rightidle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.rightidle.TabIndex = 11;
            this.rightidle.TabStop = false;
            // 
            // upidle
            // 
            this.upidle.BackColor = System.Drawing.Color.Transparent;
            this.upidle.Image = global::ZeldaDemo.Properties.Resources.upidle;
            this.upidle.Location = new System.Drawing.Point(190, 77);
            this.upidle.Name = "upidle";
            this.upidle.Size = new System.Drawing.Size(26, 29);
            this.upidle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.upidle.TabIndex = 12;
            this.upidle.TabStop = false;
            // 
            // downidle
            // 
            this.downidle.BackColor = System.Drawing.Color.Transparent;
            this.downidle.Image = global::ZeldaDemo.Properties.Resources.downidle;
            this.downidle.Location = new System.Drawing.Point(190, 77);
            this.downidle.Name = "downidle";
            this.downidle.Size = new System.Drawing.Size(26, 29);
            this.downidle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.downidle.TabIndex = 13;
            this.downidle.TabStop = false;
            // 
            // downani
            // 
            this.downani.BackColor = System.Drawing.Color.Transparent;
            this.downani.Image = global::ZeldaDemo.Properties.Resources.downani;
            this.downani.Location = new System.Drawing.Point(190, 77);
            this.downani.Name = "downani";
            this.downani.Size = new System.Drawing.Size(26, 29);
            this.downani.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.downani.TabIndex = 14;
            this.downani.TabStop = false;
            this.downani.Visible = false;
            // 
            // leftani
            // 
            this.leftani.BackColor = System.Drawing.Color.Transparent;
            this.leftani.Image = global::ZeldaDemo.Properties.Resources.leftani;
            this.leftani.Location = new System.Drawing.Point(190, 77);
            this.leftani.Name = "leftani";
            this.leftani.Size = new System.Drawing.Size(26, 29);
            this.leftani.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.leftani.TabIndex = 15;
            this.leftani.TabStop = false;
            this.leftani.Visible = false;
            // 
            // rightani
            // 
            this.rightani.BackColor = System.Drawing.Color.Transparent;
            this.rightani.Image = global::ZeldaDemo.Properties.Resources.rightani;
            this.rightani.Location = new System.Drawing.Point(190, 77);
            this.rightani.Name = "rightani";
            this.rightani.Size = new System.Drawing.Size(26, 29);
            this.rightani.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.rightani.TabIndex = 16;
            this.rightani.TabStop = false;
            this.rightani.Visible = false;
            // 
            // upani
            // 
            this.upani.BackColor = System.Drawing.Color.Transparent;
            this.upani.Image = global::ZeldaDemo.Properties.Resources.upani;
            this.upani.Location = new System.Drawing.Point(190, 77);
            this.upani.Name = "upani";
            this.upani.Size = new System.Drawing.Size(26, 29);
            this.upani.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.upani.TabIndex = 17;
            this.upani.TabStop = false;
            this.upani.Visible = false;

 // 
            // swingdown
            // 
            this.swingdown.BackColor = System.Drawing.Color.Transparent;
            this.swingdown.Image = global::ZeldaDemo.Properties.Resources.swingdown;
            this.swingdown.Location = new System.Drawing.Point(190, 77);
            this.swingdown.Name = "swingdown";
            this.swingdown.Size = new System.Drawing.Size(26, 42);
            this.swingdown.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.swingdown.TabIndex = 70;
            this.swingdown.TabStop = false;
            this.swingdown.Visible = false;
            // 
            // swingleft
            // 
            this.swingleft.BackColor = System.Drawing.Color.Transparent;
            this.swingleft.Image = global::ZeldaDemo.Properties.Resources.swingleft;
            this.swingleft.Location = new System.Drawing.Point(172, 77);
            this.swingleft.Name = "swingleft";
            this.swingleft.Size = new System.Drawing.Size(44, 29);
            this.swingleft.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.swingleft.TabIndex = 71;
            this.swingleft.TabStop = false;
            this.swingleft.Visible = false;
            // 
            // swingright
            // 
            this.swingright.BackColor = System.Drawing.Color.Transparent;
            this.swingright.Image = global::ZeldaDemo.Properties.Resources.swingright;
            this.swingright.Location = new System.Drawing.Point(190, 77);
            this.swingright.Name = "swingright";
            this.swingright.Size = new System.Drawing.Size(42, 29);
            this.swingright.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.swingright.TabIndex = 72;
            this.swingright.TabStop = false;
            this.swingright.Visible = false;
            // 
            // swingup
            // 
            this.swingup.BackColor = System.Drawing.Color.Transparent;
            this.swingup.Image = global::ZeldaDemo.Properties.Resources.swingup;
            this.swingup.Location = new System.Drawing.Point(190, 64);
            this.swingup.Name = "swingup";
            this.swingup.Size = new System.Drawing.Size(26, 42);
            this.swingup.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.swingup.TabIndex = 73;
            this.swingup.TabStop = false;
            this.swingup.Visible = false;
                }
                // 
                // or1u
                // 
                this.or1u.BackColor = System.Drawing.Color.Transparent;
                this.or1u.Image = global::ZeldaDemo.Properties.Resources.orup;
                this.or1u.Location = new System.Drawing.Point(27, 165);
                this.or1u.Name = "or1u";
                this.or1u.Size = new System.Drawing.Size(25, 26);
                this.or1u.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.or1u.TabIndex = 39;
                this.or1u.TabStop = false;
                this.or1u.Visible = false;
                // 
                // or1d
                // 
                this.or1d.BackColor = System.Drawing.Color.Transparent;
                this.or1d.Image = global::ZeldaDemo.Properties.Resources.ordown;
                this.or1d.Location = new System.Drawing.Point(27, 165);
                this.or1d.Name = "or1d";
                this.or1d.Size = new System.Drawing.Size(25, 26);
                this.or1d.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.or1d.TabIndex = 38;
                this.or1d.TabStop = false;
                // 
                // or2r
                // 
                this.or2r.BackColor = System.Drawing.Color.Transparent;
                this.or2r.Image = global::ZeldaDemo.Properties.Resources.orright;
                this.or2r.Location = new System.Drawing.Point(213, 203);
                this.or2r.Name = "or2r";
                this.or2r.Size = new System.Drawing.Size(25, 26);
                this.or2r.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.or2r.TabIndex = 42;
                this.or2r.TabStop = false;
                // 
                // or2l
                // 
                this.or2l.BackColor = System.Drawing.Color.Transparent;
                this.or2l.Image = global::ZeldaDemo.Properties.Resources.orleft;
                this.or2l.Location = new System.Drawing.Point(213, 203);
                this.or2l.Name = "or2l";
                this.or2l.Size = new System.Drawing.Size(25, 26);
                this.or2l.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.or2l.TabIndex = 41;
                this.or2l.TabStop = false;
                this.or2l.Visible = false;
                // 
                // a3o1
                // 
                this.a3o1.BackColor = System.Drawing.Color.DarkRed;
                this.a3o1.Location = new System.Drawing.Point(-5, 301);
                this.a3o1.Name = "a3o1";
                this.a3o1.Size = new System.Drawing.Size(202, 51);
                this.a3o1.TabIndex = 43;
                this.a3o1.Visible = false;
                // 
                // a3o2
                // 
                this.a3o2.BackColor = System.Drawing.Color.DarkRed;
                this.a3o2.Location = new System.Drawing.Point(252, 301);
                this.a3o2.Name = "a3o2";
                this.a3o2.Size = new System.Drawing.Size(202, 51);
                this.a3o2.TabIndex = 44;
                this.a3o2.Visible = false;
                // 
                // a3o3
                // 
                this.a3o3.BackColor = System.Drawing.Color.DarkRed;
                this.a3o3.Location = new System.Drawing.Point(-5, 235);
                this.a3o3.Name = "a3o3";
                this.a3o3.Size = new System.Drawing.Size(26, 66);
                this.a3o3.TabIndex = 45;
                this.a3o3.Visible = false;
                // 
                // a3o4
                // 
                this.a3o4.BackColor = System.Drawing.Color.DarkRed;
                this.a3o4.Location = new System.Drawing.Point(12, 267);
                this.a3o4.Name = "a3o4";
                this.a3o4.Size = new System.Drawing.Size(40, 63);
                this.a3o4.TabIndex = 46;
                this.a3o4.Visible = false;
                // 
                // a3o5
                // 
                this.a3o5.BackColor = System.Drawing.Color.DarkRed;
                this.a3o5.Location = new System.Drawing.Point(-19, 71);
                this.a3o5.Name = "a3o5";
                this.a3o5.Size = new System.Drawing.Size(31, 164);
                this.a3o5.TabIndex = 47;
                this.a3o5.Visible = false;
                // 
                // a3o6
                // 
                this.a3o6.BackColor = System.Drawing.Color.DarkRed;
                this.a3o6.Location = new System.Drawing.Point(-10, -11);
                this.a3o6.Name = "a3o6";
                this.a3o6.Size = new System.Drawing.Size(31, 127);
                this.a3o6.TabIndex = 48;
                this.a3o6.Visible = false;
                // 
                // a3o7
                // 
                this.a3o7.BackColor = System.Drawing.Color.DarkRed;
                this.a3o7.Location = new System.Drawing.Point(-5, 36);
                this.a3o7.Name = "a3o7";
                this.a3o7.Size = new System.Drawing.Size(42, 70);
                this.a3o7.TabIndex = 49;
                this.a3o7.Visible = false;
                // 
                // a3o8
                // 
                this.a3o8.BackColor = System.Drawing.Color.DarkRed;
                this.a3o8.Location = new System.Drawing.Point(21, -14);
                this.a3o8.Name = "a3o8";
                this.a3o8.Size = new System.Drawing.Size(31, 107);
                this.a3o8.TabIndex = 50;
                this.a3o8.Visible = false;
                // 
                // a3o9
                // 
                this.a3o9.BackColor = System.Drawing.Color.DarkRed;
                this.a3o9.Location = new System.Drawing.Point(43, -30);
                this.a3o9.Name = "a3o9";
                this.a3o9.Size = new System.Drawing.Size(140, 95);
                this.a3o9.TabIndex = 51;
                this.a3o9.Visible = false;
                // 
                // a3o10
                // 
                this.a3o10.BackColor = System.Drawing.Color.DarkRed;
                this.a3o10.Location = new System.Drawing.Point(222, -30);
                this.a3o10.Name = "a3o10";
                this.a3o10.Size = new System.Drawing.Size(232, 95);
                this.a3o10.TabIndex = 52;
                this.a3o10.Visible = false;
                // 
                // a3o11
                // 
                this.a3o11.BackColor = System.Drawing.Color.DarkRed;
                this.a3o11.Location = new System.Drawing.Point(249, 59);
                this.a3o11.Name = "a3o11";
                this.a3o11.Size = new System.Drawing.Size(59, 34);
                this.a3o11.TabIndex = 53;
                this.a3o11.Visible = false;
                // 
                // a3o12
                // 
                this.a3o12.BackColor = System.Drawing.Color.DarkRed;
                this.a3o12.Location = new System.Drawing.Point(176, -2);
                this.a3o12.Name = "a3o12";
                this.a3o12.Size = new System.Drawing.Size(59, 29);
                this.a3o12.TabIndex = 54;
                this.a3o12.Visible = false;
                // 
                // a3o13
                // 
                this.a3o13.BackColor = System.Drawing.Color.DarkRed;
                this.a3o13.Location = new System.Drawing.Point(252, 275);
                this.a3o13.Name = "a3o13";
                this.a3o13.Size = new System.Drawing.Size(53, 26);
                this.a3o13.TabIndex = 55;
                this.a3o13.Visible = false;
                // 
                // a3o14
                // 
                this.a3o14.BackColor = System.Drawing.Color.DarkRed;
                this.a3o14.Location = new System.Drawing.Point(397, 269);
                this.a3o14.Name = "a3o14";
                this.a3o14.Size = new System.Drawing.Size(57, 33);
                this.a3o14.TabIndex = 56;
                this.a3o14.Visible = false;
                // 
                // a3o15
                // 
                this.a3o15.BackColor = System.Drawing.Color.DarkRed;
                this.a3o15.Location = new System.Drawing.Point(417, 234);
                this.a3o15.Name = "a3o15";
                this.a3o15.Size = new System.Drawing.Size(37, 34);
                this.a3o15.TabIndex = 57;
                this.a3o15.Visible = false;
                // 
                // a3o16
                // 
                this.a3o16.BackColor = System.Drawing.Color.DarkRed;
                this.a3o16.Location = new System.Drawing.Point(391, 59);
                this.a3o16.Name = "a3o16";
                this.a3o16.Size = new System.Drawing.Size(63, 34);
                this.a3o16.TabIndex = 58;
                this.a3o16.Visible = false;
                // 
                // warpA1n2
                // 
                this.warpA1f3.BackColor = System.Drawing.Color.Cyan;
                this.warpA1f3.Location = new System.Drawing.Point(208, 334);
                this.warpA1f3.Name = "warpA1n2";
                this.warpA1f3.Size = new System.Drawing.Size(32, 16);
                this.warpA1f3.TabIndex = 74;
                this.warpA1f3.Visible = false;
                // 
                // a3o17
                // 
                this.a3o17.BackColor = System.Drawing.Color.DarkRed;
                this.a3o17.Location = new System.Drawing.Point(421, 82);
                this.a3o17.Name = "a3o17";
                this.a3o17.Size = new System.Drawing.Size(33, 152);
                this.a3o17.TabIndex = 59;
                this.a3o17.Visible = false;
                // 
                // a3o18
                // 
                this.a3o18.BackColor = System.Drawing.Color.DarkRed;
                this.a3o18.Location = new System.Drawing.Point(83, 103);
                this.a3o18.Name = "a3o18";
                this.a3o18.Size = new System.Drawing.Size(25, 26);
                this.a3o18.TabIndex = 60;
                this.a3o18.Visible = false;
                // 
                // a3o19
                // 
                this.a3o19.BackColor = System.Drawing.Color.DarkRed;
                this.a3o19.Location = new System.Drawing.Point(82, 171);
                this.a3o19.Name = "a3o19";
                this.a3o19.Size = new System.Drawing.Size(25, 27);
                this.a3o19.TabIndex = 61;
                this.a3o19.Visible = false;
                // 
                // a3o20
                // 
                this.a3o20.BackColor = System.Drawing.Color.DarkRed;
                this.a3o20.Location = new System.Drawing.Point(82, 239);
                this.a3o20.Name = "a3o20";
                this.a3o20.Size = new System.Drawing.Size(26, 25);
                this.a3o20.TabIndex = 62;
                this.a3o20.Visible = false;
                // 
                // a3o21
                // 
                this.a3o21.BackColor = System.Drawing.Color.DarkRed;
                this.a3o21.Location = new System.Drawing.Point(138, 239);
                this.a3o21.Name = "a3o21";
                this.a3o21.Size = new System.Drawing.Size(29, 26);
                this.a3o21.TabIndex = 63;
                this.a3o21.Visible = false;
                // 
                // a3o22
                // 
                this.a3o22.BackColor = System.Drawing.Color.DarkRed;
                this.a3o22.Location = new System.Drawing.Point(140, 172);
                this.a3o22.Name = "a3o22";
                this.a3o22.Size = new System.Drawing.Size(25, 27);
                this.a3o22.TabIndex = 64;
                this.a3o22.Visible = false;
                // 
                // a3o23
                // 
                this.a3o23.BackColor = System.Drawing.Color.DarkRed;
                this.a3o23.Location = new System.Drawing.Point(139, 103);
                this.a3o23.Name = "a3o23";
                this.a3o23.Size = new System.Drawing.Size(26, 27);
                this.a3o23.TabIndex = 65;
                this.a3o23.Visible = false;
                // 
                // a3o24
                // 
                this.a3o24.BackColor = System.Drawing.Color.DarkRed;
                this.a3o24.Location = new System.Drawing.Point(336, 105);
                this.a3o24.Name = "a3o24";
                this.a3o24.Size = new System.Drawing.Size(27, 22);
                this.a3o24.TabIndex = 66;
                this.a3o24.Visible = false;
                // 
                // a3o25
                // 
                this.a3o25.BackColor = System.Drawing.Color.DarkRed;
                this.a3o25.Location = new System.Drawing.Point(335, 173);
                this.a3o25.Name = "a3o25";
                this.a3o25.Size = new System.Drawing.Size(28, 20);
                this.a3o25.TabIndex = 67;
                this.a3o25.Visible = false;
                // 
                // a3o26
                // 
                this.a3o26.BackColor = System.Drawing.Color.DarkRed;
                this.a3o26.Location = new System.Drawing.Point(334, 242);
                this.a3o26.Name = "a3o26";
                this.a3o26.Size = new System.Drawing.Size(27, 21);
                this.a3o26.TabIndex = 68;
                this.a3o26.Visible = false;
                // 
                // warpD1
                // 
                this.warpD1.BackColor = System.Drawing.Color.Black;
                this.warpD1.Location = new System.Drawing.Point(183, 27);
                this.warpD1.Name = "warpD1";
                this.warpD1.Size = new System.Drawing.Size(39, 38);
                this.warpD1.TabIndex = 69;

                if (opend1 == true)
                {
                    warpD1.Visible = true;
                }
                else
                {
                    warpD1.Visible = false;
                }

                this.BackgroundImage = global::ZeldaDemo.Properties.Resources.a3;
                this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                this.BackColor = System.Drawing.Color.Khaki;
                warped = false;

            }

            #endregion


            #region load area 2

            if (roomid == 2 && warped == true)
            {
                orTimer.Stop();
                benTimer.Stop();
                skelTimer.Stop();
                bossTimer.Stop();
                incave = 1; // sets value to start up over world bgm again
                overworldbgm.Ctlcontrols.stop();


                // 
                // a2o1
                // 
                this.a2o1.BackColor = System.Drawing.Color.DarkRed;
                this.a2o1.Location = new System.Drawing.Point(54, 64);
                this.a2o1.Name = "a2o1";
                this.a2o1.Size = new System.Drawing.Size(338, 102);
                this.a2o1.TabIndex = 0;
                // 
                // a2o2
                // 
                this.a2o2.BackColor = System.Drawing.Color.DarkRed;
                this.a2o2.Location = new System.Drawing.Point(1, 0);
                this.a2o2.Name = "a2o2";
                this.a2o2.Size = new System.Drawing.Size(55, 353);
                this.a2o2.TabIndex = 1;
                // 
                // a2o3
                // 
                this.a2o3.BackColor = System.Drawing.Color.DarkRed;
                this.a2o3.Location = new System.Drawing.Point(12, 288);
                this.a2o3.Name = "a2o3";
                this.a2o3.Size = new System.Drawing.Size(182, 54);
                this.a2o3.TabIndex = 2;
                // 
                // a2o4
                // 
                this.a2o4.BackColor = System.Drawing.Color.DarkRed;
                this.a2o4.Location = new System.Drawing.Point(394, 9);
                this.a2o4.Name = "a2o4";
                this.a2o4.Size = new System.Drawing.Size(61, 333);
                this.a2o4.TabIndex = 3;
                // 
                // leftidle
                // 
                this.leftidle.BackColor = System.Drawing.Color.Transparent;
                this.leftidle.Image = global::ZeldaDemo.Properties.Resources.leftidle;
                this.leftidle.Location = new System.Drawing.Point(212, 240);
                this.leftidle.Name = "leftidle";
                this.leftidle.Size = new System.Drawing.Size(26, 29);
                this.leftidle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.leftidle.TabIndex = 10;
                this.leftidle.TabStop = false;
                // 
                // rightidle
                // 
                this.rightidle.BackColor = System.Drawing.Color.Transparent;
                this.rightidle.Image = global::ZeldaDemo.Properties.Resources.rightidle;
                this.rightidle.Location = new System.Drawing.Point(212, 240);
                this.rightidle.Name = "rightidle";
                this.rightidle.Size = new System.Drawing.Size(26, 29);
                this.rightidle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.rightidle.TabIndex = 11;
                this.rightidle.TabStop = false;
                // 
                // upidle
                // 
                this.upidle.BackColor = System.Drawing.Color.Transparent;
                this.upidle.Image = global::ZeldaDemo.Properties.Resources.upidle;
                this.upidle.Location = new System.Drawing.Point(212, 240);
                this.upidle.Name = "upidle";
                this.upidle.Size = new System.Drawing.Size(26, 29);
                this.upidle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.upidle.TabIndex = 12;
                this.upidle.TabStop = false;
                // 
                // downidle
                // 
                this.downidle.BackColor = System.Drawing.Color.Transparent;
                this.downidle.Image = global::ZeldaDemo.Properties.Resources.downidle;
                this.downidle.Location = new System.Drawing.Point(212, 240);
                this.downidle.Name = "downidle";
                this.downidle.Size = new System.Drawing.Size(26, 29);
                this.downidle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.downidle.TabIndex = 13;
                this.downidle.TabStop = false;

                // 
                // swingleft
                // 
                this.swingleft.Image = global::ZeldaDemo.Properties.Resources.swingleft;
                this.swingleft.Location = new System.Drawing.Point(194, 240);
                this.swingleft.Name = "swingleft";
                this.swingleft.Size = new System.Drawing.Size(42, 29);
                this.swingleft.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.swingleft.TabIndex = 32;
                this.swingleft.TabStop = false;
                this.swingleft.Visible = false;
                // 
                // swingup
                // 
                this.swingup.Image = global::ZeldaDemo.Properties.Resources.swingup;
                this.swingup.Location = new System.Drawing.Point(212, 227);
                this.swingup.Name = "swingup";
                this.swingup.Size = new System.Drawing.Size(26, 42);
                this.swingup.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.swingup.TabIndex = 31;
                this.swingup.TabStop = false;
                this.swingup.Visible = false;
                // 
                // swingright
                // 
                this.swingright.Image = global::ZeldaDemo.Properties.Resources.swingright;
                this.swingright.Location = new System.Drawing.Point(212, 240);
                this.swingright.Name = "swingright";
                this.swingright.Size = new System.Drawing.Size(42, 29);
                this.swingright.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.swingright.TabIndex = 30;
                this.swingright.TabStop = false;
                this.swingright.Visible = false;
                // 
                // swingdown
                // 
                this.swingdown.Image = global::ZeldaDemo.Properties.Resources.swingdown;
                this.swingdown.Location = new System.Drawing.Point(212, 240);
                this.swingdown.Name = "swingdown";
                this.swingdown.Size = new System.Drawing.Size(26, 42);
                this.swingdown.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.swingdown.TabIndex = 29;
                this.swingdown.TabStop = false;
                this.swingdown.Visible = false;
                // 
                // downani
                // 
                this.downani.BackColor = System.Drawing.Color.Transparent;
                this.downani.Image = global::ZeldaDemo.Properties.Resources.downani;
                this.downani.Location = new System.Drawing.Point(212, 240);
                this.downani.Name = "downani";
                this.downani.Size = new System.Drawing.Size(26, 29);
                this.downani.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.downani.TabIndex = 14;
                this.downani.TabStop = false;
                this.downani.Visible = false;
                // 
                // leftani
                // 
                this.leftani.BackColor = System.Drawing.Color.Transparent;
                this.leftani.Image = global::ZeldaDemo.Properties.Resources.leftani;
                this.leftani.Location = new System.Drawing.Point(212, 240);
                this.leftani.Name = "leftani";
                this.leftani.Size = new System.Drawing.Size(26, 29);
                this.leftani.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.leftani.TabIndex = 15;
                this.leftani.TabStop = false;
                this.leftani.Visible = false;
                // 
                // rightani
                // 
                this.rightani.BackColor = System.Drawing.Color.Transparent;
                this.rightani.Image = global::ZeldaDemo.Properties.Resources.rightani;
                this.rightani.Location = new System.Drawing.Point(212, 240);
                this.rightani.Name = "rightani";
                this.rightani.Size = new System.Drawing.Size(26, 29);
                this.rightani.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.rightani.TabIndex = 16;
                this.rightani.TabStop = false;
                this.rightani.Visible = false;
                // 
                // upani
                // 
                this.upani.BackColor = System.Drawing.Color.Transparent;
                this.upani.Image = global::ZeldaDemo.Properties.Resources.upani;
                this.upani.Location = new System.Drawing.Point(212, 240);
                this.upani.Name = "upani";
                this.upani.Size = new System.Drawing.Size(26, 29);
                this.upani.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.upani.TabIndex = 17;
                this.upani.TabStop = false;
                this.upani.Visible = false;
                // 
                // warpA1
                // 
                this.warpA1.BackColor = System.Drawing.Color.Cyan;
                this.warpA1.Location = new System.Drawing.Point(202, 309);
                this.warpA1.Name = "warpA1";
                this.warpA1.Size = new System.Drawing.Size(41, 25);
                this.warpA1.TabIndex = 18;
                this.warpA1.Visible = false;
                // 
                // a2o5
                // 
                this.a2o5.BackColor = System.Drawing.Color.DarkRed;
                this.a2o5.Location = new System.Drawing.Point(247, 288);
                this.a2o5.Name = "a2o5";
                this.a2o5.Size = new System.Drawing.Size(182, 54);
                this.a2o5.TabIndex = 19;

                if (gotsecret == false)
                {
                // 
                // secretr
                // 
                this.secretr.BackColor = System.Drawing.Color.Black;
                this.secretr.Location = new System.Drawing.Point(220, 132);
                this.secretr.Name = "secretr";
                this.secretr.Size = new System.Drawing.Size(1,1);
                this.secretr.TabIndex = 163;
                this.secretr.Visible = true;
                }

                if (gotsword == false)
                {
                    // 
                    // sword
                    // 
                    this.sword.BackColor = System.Drawing.Color.Transparent;
                    this.sword.Image = global::ZeldaDemo.Properties.Resources.sword;
                    this.sword.Location = new System.Drawing.Point(211, 169);
                    this.sword.Name = "sword";
                    this.sword.Size = new System.Drawing.Size(29, 41);
                    this.sword.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.sword.TabIndex = 18;
                    this.sword.TabStop = false;
                }



                this.BackgroundImage = global::ZeldaDemo.Properties.Resources.a2;
                this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                this.BackColor = System.Drawing.Color.Black;

                warped = false;
            }
            #endregion

            #region load area 1

            if (roomid == 1 && warped == true)
            {
                // octarockproperties
                orHealth[0] = 2;
                orHealth[1] = 2;
                orsystem[0] = 0;
                orsystem[0] = 0;
                orrocksystem[0] = 0;
                orrocksystem[0] = 0;
                orTimer.Start();
                benTimer.Stop();
                skelTimer.Stop();
                bossTimer.Stop();
                or1u.Visible = false;
                or1d.Visible = true;
                or2l.Visible = false;
                or2r.Visible = true;

                if (incave == 1) //overworld gbm fix
                {
                    overworldbgm.URL = "sfx/overworld.wav";
                    overworldbgm.Ctlcontrols.play();
                    overworldbgm.settings.setMode("loop", true);
                    incave = 0;
                }

                if (warptoa1from == 1)
                {
                    // 
                    // leftidle
                    // 
                    this.leftidle.BackColor = System.Drawing.Color.Transparent;
                    this.leftidle.Image = global::ZeldaDemo.Properties.Resources.leftidle;
                    this.leftidle.Location = new System.Drawing.Point(111, 81);
                    this.leftidle.Name = "leftidle";
                    this.leftidle.Size = new System.Drawing.Size(26, 29);
                    this.leftidle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.leftidle.TabIndex = 10;
                    this.leftidle.TabStop = false;
                    // 
                    // rightidle
                    // 
                    this.rightidle.BackColor = System.Drawing.Color.Transparent;
                    this.rightidle.Image = global::ZeldaDemo.Properties.Resources.rightidle;
                    this.rightidle.Location = new System.Drawing.Point(111, 81);
                    this.rightidle.Name = "rightidle";
                    this.rightidle.Size = new System.Drawing.Size(26, 29);
                    this.rightidle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.rightidle.TabIndex = 11;
                    this.rightidle.TabStop = false;
                    // 
                    // upidle
                    // 
                    this.upidle.BackColor = System.Drawing.Color.Transparent;
                    this.upidle.Image = global::ZeldaDemo.Properties.Resources.upidle;
                    this.upidle.Location = new System.Drawing.Point(111, 81);
                    this.upidle.Name = "upidle";
                    this.upidle.Size = new System.Drawing.Size(26, 29);
                    this.upidle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.upidle.TabIndex = 12;
                    this.upidle.TabStop = false;
                    // 
                    // downidle
                    // 
                    this.downidle.BackColor = System.Drawing.Color.Transparent;
                    this.downidle.Image = global::ZeldaDemo.Properties.Resources.downidle;
                    this.downidle.Location = new System.Drawing.Point(111, 81);
                    this.downidle.Name = "downidle";
                    this.downidle.Size = new System.Drawing.Size(26, 29);
                    this.downidle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.downidle.TabIndex = 13;
                    this.downidle.TabStop = false;

                    // 
                    // swingleft
                    // 
                    this.swingleft.Image = global::ZeldaDemo.Properties.Resources.swingleft;
                    this.swingleft.Location = new System.Drawing.Point(93, 81);
                    this.swingleft.Name = "swingleft";
                    this.swingleft.Size = new System.Drawing.Size(42, 29);
                    this.swingleft.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.swingleft.TabIndex = 32;
                    this.swingleft.TabStop = false;
                    this.swingleft.Visible = false;
                    // 
                    // swingup
                    // 
                    this.swingup.Image = global::ZeldaDemo.Properties.Resources.swingup;
                    this.swingup.Location = new System.Drawing.Point(111, 70);
                    this.swingup.Name = "swingup";
                    this.swingup.Size = new System.Drawing.Size(26, 42);
                    this.swingup.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.swingup.TabIndex = 31;
                    this.swingup.TabStop = false;
                    this.swingup.Visible = false;
                    // 
                    // swingright
                    // 
                    this.swingright.Image = global::ZeldaDemo.Properties.Resources.swingright;
                    this.swingright.Location = new System.Drawing.Point(111, 81);
                    this.swingright.Name = "swingright";
                    this.swingright.Size = new System.Drawing.Size(42, 29);
                    this.swingright.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.swingright.TabIndex = 30;
                    this.swingright.TabStop = false;
                    this.swingright.Visible = false;
                    // 
                    // swingdown
                    // 
                    this.swingdown.Image = global::ZeldaDemo.Properties.Resources.swingdown;
                    this.swingdown.Location = new System.Drawing.Point(111, 81);
                    this.swingdown.Name = "swingdown";
                    this.swingdown.Size = new System.Drawing.Size(26, 42);
                    this.swingdown.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.swingdown.TabIndex = 29;
                    this.swingdown.TabStop = false;
                    this.swingdown.Visible = false;
                    // 
                    // downani
                    // 
                    this.downani.BackColor = System.Drawing.Color.Transparent;
                    this.downani.Image = global::ZeldaDemo.Properties.Resources.downani;
                    this.downani.Location = new System.Drawing.Point(111, 81);
                    this.downani.Name = "downani";
                    this.downani.Size = new System.Drawing.Size(26, 29);
                    this.downani.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.downani.TabIndex = 14;
                    this.downani.TabStop = false;
                    this.downani.Visible = false;
                    // 
                    // leftani
                    // 
                    this.leftani.BackColor = System.Drawing.Color.Transparent;
                    this.leftani.Image = global::ZeldaDemo.Properties.Resources.leftani;
                    this.leftani.Location = new System.Drawing.Point(111, 81);
                    this.leftani.Name = "leftani";
                    this.leftani.Size = new System.Drawing.Size(26, 29);
                    this.leftani.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.leftani.TabIndex = 15;
                    this.leftani.TabStop = false;
                    this.leftani.Visible = false;
                    // 
                    // rightani
                    // 
                    this.rightani.BackColor = System.Drawing.Color.Transparent;
                    this.rightani.Image = global::ZeldaDemo.Properties.Resources.rightani;
                    this.rightani.Location = new System.Drawing.Point(111, 81);
                    this.rightani.Name = "rightani";
                    this.rightani.Size = new System.Drawing.Size(26, 29);
                    this.rightani.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.rightani.TabIndex = 16;
                    this.rightani.TabStop = false;
                    this.rightani.Visible = false;
                    // 
                    // upani
                    // 
                    this.upani.BackColor = System.Drawing.Color.Transparent;
                    this.upani.Image = global::ZeldaDemo.Properties.Resources.upani;
                    this.upani.Location = new System.Drawing.Point(111, 81);
                    this.upani.Name = "upani";
                    this.upani.Size = new System.Drawing.Size(26, 29);
                    this.upani.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.upani.TabIndex = 17;
                    this.upani.TabStop = false;
                    this.upani.Visible = false;
                }
                else if (warptoa1from == 2)
                {
                    // 
                    // leftidle
                    // 
                    this.leftidle.BackColor = System.Drawing.Color.Transparent;
                    this.leftidle.Image = global::ZeldaDemo.Properties.Resources.leftidle;
                    this.leftidle.Location = new System.Drawing.Point(211, 40);
                    this.leftidle.Name = "leftidle";
                    this.leftidle.Size = new System.Drawing.Size(26, 29);
                    this.leftidle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.leftidle.TabIndex = 10;
                    this.leftidle.TabStop = false;
                    // 
                    // rightidle
                    // 
                    this.rightidle.BackColor = System.Drawing.Color.Transparent;
                    this.rightidle.Image = global::ZeldaDemo.Properties.Resources.rightidle;
                    this.rightidle.Location = new System.Drawing.Point(211, 40);
                    this.rightidle.Name = "rightidle";
                    this.rightidle.Size = new System.Drawing.Size(26, 29);
                    this.rightidle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.rightidle.TabIndex = 11;
                    this.rightidle.TabStop = false;
                    this.rightidle.Visible = false;
                    // 
                    // upidle
                    // 
                    this.upidle.BackColor = System.Drawing.Color.Transparent;
                    this.upidle.Image = global::ZeldaDemo.Properties.Resources.upidle;
                    this.upidle.Location = new System.Drawing.Point(211, 40);
                    this.upidle.Name = "upidle";
                    this.upidle.Size = new System.Drawing.Size(26, 29);
                    this.upidle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.upidle.TabIndex = 12;
                    this.upidle.TabStop = false;
                    this.upidle.Visible = false;
                    // 
                    // downidle
                    // 
                    this.downidle.BackColor = System.Drawing.Color.Transparent;
                    this.downidle.Image = global::ZeldaDemo.Properties.Resources.downidle;
                    this.downidle.Location = new System.Drawing.Point(211, 40);
                    this.downidle.Name = "downidle";
                    this.downidle.Size = new System.Drawing.Size(26, 29);
                    this.downidle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.downidle.TabIndex = 13;
                    this.downidle.TabStop = false;
                    // 
                    // downani
                    // 
                    this.downani.BackColor = System.Drawing.Color.Transparent;
                    this.downani.Image = global::ZeldaDemo.Properties.Resources.downani;
                    this.downani.Location = new System.Drawing.Point(211, 40);
                    this.downani.Name = "downani";
                    this.downani.Size = new System.Drawing.Size(26, 29);
                    this.downani.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.downani.TabIndex = 14;
                    this.downani.TabStop = false;
                    this.downani.Visible = false;
                    // 
                    // leftani
                    // 
                    this.leftani.BackColor = System.Drawing.Color.Transparent;
                    this.leftani.Image = global::ZeldaDemo.Properties.Resources.leftani;
                    this.leftani.Location = new System.Drawing.Point(211, 40);
                    this.leftani.Name = "leftani";
                    this.leftani.Size = new System.Drawing.Size(26, 29);
                    this.leftani.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.leftani.TabIndex = 15;
                    this.leftani.TabStop = false;
                    this.leftani.Visible = false;
                    // 
                    // rightani
                    // 
                    this.rightani.BackColor = System.Drawing.Color.Transparent;
                    this.rightani.Image = global::ZeldaDemo.Properties.Resources.rightani;
                    this.rightani.Location = new System.Drawing.Point(211, 40);
                    this.rightani.Name = "rightani";
                    this.rightani.Size = new System.Drawing.Size(26, 29);
                    this.rightani.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.rightani.TabIndex = 16;
                    this.rightani.TabStop = false;
                    this.rightani.Visible = false;
                    // 
                    // upani
                    // 
                    this.upani.BackColor = System.Drawing.Color.Transparent;
                    this.upani.Image = global::ZeldaDemo.Properties.Resources.upani;
                    this.upani.Location = new System.Drawing.Point(211, 40);
                    this.upani.Name = "upani";
                    this.upani.Size = new System.Drawing.Size(26, 29);
                    this.upani.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.upani.TabIndex = 17;
                    this.upani.TabStop = false;
                    this.upani.Visible = false;

                    // 
                    // swingdown
                    // 
                    this.swingdown.BackColor = System.Drawing.Color.Transparent;
                    this.swingdown.Image = global::ZeldaDemo.Properties.Resources.swingdown;
                    this.swingdown.Location = new System.Drawing.Point(211, 40);
                    this.swingdown.Name = "swingdown";
                    this.swingdown.Size = new System.Drawing.Size(26, 42);
                    this.swingdown.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.swingdown.TabIndex = 70;
                    this.swingdown.TabStop = false;
                    this.swingdown.Visible = false;
                    // 
                    // swingleft
                    // 
                    this.swingleft.BackColor = System.Drawing.Color.Transparent;
                    this.swingleft.Image = global::ZeldaDemo.Properties.Resources.swingleft;
                    this.swingleft.Location = new System.Drawing.Point(193, 40);
                    this.swingleft.Name = "swingleft";
                    this.swingleft.Size = new System.Drawing.Size(44, 29);
                    this.swingleft.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.swingleft.TabIndex = 71;
                    this.swingleft.TabStop = false;
                    this.swingleft.Visible = false;
                    // 
                    // swingright
                    // 
                    this.swingright.BackColor = System.Drawing.Color.Transparent;
                    this.swingright.Image = global::ZeldaDemo.Properties.Resources.swingright;
                    this.swingright.Location = new System.Drawing.Point(211, 40);
                    this.swingright.Name = "swingright";
                    this.swingright.Size = new System.Drawing.Size(42, 29);
                    this.swingright.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.swingright.TabIndex = 72;
                    this.swingright.TabStop = false;
                    this.swingright.Visible = false;
                    // 
                    // swingup
                    // 
                    this.swingup.BackColor = System.Drawing.Color.Transparent;
                    this.swingup.Image = global::ZeldaDemo.Properties.Resources.swingup;
                    this.swingup.Location = new System.Drawing.Point(211, 27);
                    this.swingup.Name = "swingup";
                    this.swingup.Size = new System.Drawing.Size(26, 42);
                    this.swingup.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.swingup.TabIndex = 73;
                    this.swingup.TabStop = false;
                    this.swingup.Visible = false;
                }

                else if (warptoa1from == 3)
                {
                    // 
                    // leftidle
                    // 
                    this.leftidle.BackColor = System.Drawing.Color.Transparent;
                    this.leftidle.Image = global::ZeldaDemo.Properties.Resources.leftidle;
                    this.leftidle.Location = new System.Drawing.Point(300, 178);
                    this.leftidle.Name = "leftidle";
                    this.leftidle.Size = new System.Drawing.Size(26, 29);
                    this.leftidle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.leftidle.TabIndex = 10;
                    this.leftidle.TabStop = false;
                    // 
                    // rightidle
                    // 
                    this.rightidle.BackColor = System.Drawing.Color.Transparent;
                    this.rightidle.Image = global::ZeldaDemo.Properties.Resources.rightidle;
                    this.rightidle.Location = new System.Drawing.Point(300, 178);
                    this.rightidle.Name = "rightidle";
                    this.rightidle.Size = new System.Drawing.Size(26, 29);
                    this.rightidle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.rightidle.TabIndex = 11;
                    this.rightidle.TabStop = false;
                    // 
                    // upidle
                    // 
                    this.upidle.BackColor = System.Drawing.Color.Transparent;
                    this.upidle.Image = global::ZeldaDemo.Properties.Resources.upidle;
                    this.upidle.Location = new System.Drawing.Point(300, 178);
                    this.upidle.Name = "upidle";
                    this.upidle.Size = new System.Drawing.Size(26, 29);
                    this.upidle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.upidle.TabIndex = 12;
                    this.upidle.TabStop = false;
                    // 
                    // downidle
                    // 
                    this.downidle.BackColor = System.Drawing.Color.Transparent;
                    this.downidle.Image = global::ZeldaDemo.Properties.Resources.downidle;
                    this.downidle.Location = new System.Drawing.Point(300, 178);
                    this.downidle.Name = "downidle";
                    this.downidle.Size = new System.Drawing.Size(26, 29);
                    this.downidle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.downidle.TabIndex = 13;
                    this.downidle.TabStop = false;
                    // 
                    // downani
                    // 
                    this.downani.BackColor = System.Drawing.Color.Transparent;
                    this.downani.Image = global::ZeldaDemo.Properties.Resources.downani;
                    this.downani.Location = new System.Drawing.Point(300, 178);
                    this.downani.Name = "downani";
                    this.downani.Size = new System.Drawing.Size(26, 29);
                    this.downani.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.downani.TabIndex = 14;
                    this.downani.TabStop = false;
                    this.downani.Visible = false;
                    // 
                    // leftani
                    // 
                    this.leftani.BackColor = System.Drawing.Color.Transparent;
                    this.leftani.Image = global::ZeldaDemo.Properties.Resources.leftani;
                    this.leftani.Location = new System.Drawing.Point(300, 178);
                    this.leftani.Name = "leftani";
                    this.leftani.Size = new System.Drawing.Size(26, 29);
                    this.leftani.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.leftani.TabIndex = 15;
                    this.leftani.TabStop = false;
                    this.leftani.Visible = false;
                    // 
                    // rightani
                    // 
                    this.rightani.BackColor = System.Drawing.Color.Transparent;
                    this.rightani.Image = global::ZeldaDemo.Properties.Resources.rightani;
                    this.rightani.Location = new System.Drawing.Point(300, 178);
                    this.rightani.Name = "rightani";
                    this.rightani.Size = new System.Drawing.Size(26, 29);
                    this.rightani.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.rightani.TabIndex = 16;
                    this.rightani.TabStop = false;
                    this.rightani.Visible = false;
                    // 
                    // upani
                    // 
                    this.upani.BackColor = System.Drawing.Color.Transparent;
                    this.upani.Image = global::ZeldaDemo.Properties.Resources.upani;
                    this.upani.Location = new System.Drawing.Point(300, 178);
                    this.upani.Name = "upani";
                    this.upani.Size = new System.Drawing.Size(26, 29);
                    this.upani.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.upani.TabIndex = 17;
                    this.upani.TabStop = false;
                    this.upani.Visible = false;
                    // 
                    // swingdown
                    // 
                    this.swingdown.BackColor = System.Drawing.Color.Transparent;
                    this.swingdown.Image = global::ZeldaDemo.Properties.Resources.swingdown;
                    this.swingdown.Location = new System.Drawing.Point(300, 178);
                    this.swingdown.Name = "swingdown";
                    this.swingdown.Size = new System.Drawing.Size(26, 42);
                    this.swingdown.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.swingdown.TabIndex = 70;
                    this.swingdown.TabStop = false;
                    this.swingdown.Visible = false;
                    // 
                    // swingleft
                    // 
                    this.swingleft.BackColor = System.Drawing.Color.Transparent;
                    this.swingleft.Image = global::ZeldaDemo.Properties.Resources.swingleft;
                    this.swingleft.Location = new System.Drawing.Point(282, 178);
                    this.swingleft.Name = "swingleft";
                    this.swingleft.Size = new System.Drawing.Size(44, 29);
                    this.swingleft.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.swingleft.TabIndex = 71;
                    this.swingleft.TabStop = false;
                    this.swingleft.Visible = false;
                    // 
                    // swingright
                    // 
                    this.swingright.BackColor = System.Drawing.Color.Transparent;
                    this.swingright.Image = global::ZeldaDemo.Properties.Resources.swingright;
                    this.swingright.Location = new System.Drawing.Point(300, 178);
                    this.swingright.Name = "swingright";
                    this.swingright.Size = new System.Drawing.Size(42, 29);
                    this.swingright.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.swingright.TabIndex = 72;
                    this.swingright.TabStop = false;
                    this.swingright.Visible = false;
                    // 
                    // swingup
                    // 
                    this.swingup.BackColor = System.Drawing.Color.Transparent;
                    this.swingup.Image = global::ZeldaDemo.Properties.Resources.swingup;
                    this.swingup.Location = new System.Drawing.Point(300, 165);
                    this.swingup.Name = "swingup";
                    this.swingup.Size = new System.Drawing.Size(26, 42);
                    this.swingup.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.swingup.TabIndex = 73;
                    this.swingup.TabStop = false;
                    this.swingup.Visible = false;
                }

                // 
                // a1o1
                // 
                this.a1o1.BackColor = System.Drawing.Color.DarkRed;
                this.a1o1.Location = new System.Drawing.Point(257, -5);
                this.a1o1.Name = "a1o1";
                this.a1o1.Size = new System.Drawing.Size(198, 164);
                this.a1o1.TabIndex = 0;
                // 
                // warpA3
                // 
                this.warpA3.BackColor = System.Drawing.Color.Cyan;
                this.warpA3.Location = new System.Drawing.Point(198, -1);
                this.warpA3.Name = "warpA3";
                this.warpA3.Size = new System.Drawing.Size(50, 22);
                this.warpA3.TabIndex = 74;
                this.warpA3.Visible = false;
                // 
                // a1o2
                // 
                this.a1o2.BackColor = System.Drawing.Color.DarkRed;
                this.a1o2.Location = new System.Drawing.Point(1, 205);
                this.a1o2.Name = "a1o2";
                this.a1o2.Size = new System.Drawing.Size(50, 148);
                this.a1o2.TabIndex = 1;

                // 
                // or1u
                // 
                this.or1u.BackColor = System.Drawing.Color.Transparent;
                this.or1u.Image = global::ZeldaDemo.Properties.Resources.orup;
                this.or1u.Location = new System.Drawing.Point(204, 108);
                this.or1u.Name = "or1u";
                this.or1u.Size = new System.Drawing.Size(25, 26);
                this.or1u.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.or1u.TabIndex = 39;
                this.or1u.TabStop = false;
                this.or1u.Visible = false;
                // 
                // or1d
                // 
                this.or1d.BackColor = System.Drawing.Color.Transparent;
                this.or1d.Image = global::ZeldaDemo.Properties.Resources.ordown;
                this.or1d.Location = new System.Drawing.Point(204, 108);
                this.or1d.Name = "or1d";
                this.or1d.Size = new System.Drawing.Size(25, 26);
                this.or1d.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.or1d.TabIndex = 38;
                this.or1d.TabStop = false;


                // 
                // or2r
                // 
                this.or2r.BackColor = System.Drawing.Color.Transparent;
                this.or2r.Image = global::ZeldaDemo.Properties.Resources.orright;
                this.or2r.Location = new System.Drawing.Point(238, 244);
                this.or2r.Name = "or2r";
                this.or2r.Size = new System.Drawing.Size(25, 26);
                this.or2r.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.or2r.TabIndex = 42;
                this.or2r.TabStop = false;
                this.or2r.Visible = false;
                // 
                // or2l
                // 
                this.or2l.BackColor = System.Drawing.Color.Transparent;
                this.or2l.Image = global::ZeldaDemo.Properties.Resources.orleft;
                this.or2l.Location = new System.Drawing.Point(238, 244);
                this.or2l.Name = "or2l";
                this.or2l.Size = new System.Drawing.Size(25, 26);
                this.or2l.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.or2l.TabIndex = 41;
                this.or2l.TabStop = false;
                this.or2l.Visible = false;



                // 
                // a1o3
                // 
                this.a1o3.BackColor = System.Drawing.Color.DarkRed;
                this.a1o3.Location = new System.Drawing.Point(24, 306);
                this.a1o3.Name = "a1o3";
                this.a1o3.Size = new System.Drawing.Size(431, 44);
                this.a1o3.TabIndex = 2;
                // 
                // a1o4
                // 
                this.a1o4.BackColor = System.Drawing.Color.DarkRed;
                this.a1o4.Location = new System.Drawing.Point(394, 200);
                this.a1o4.Name = "a1o4";
                this.a1o4.Size = new System.Drawing.Size(61, 142);
                this.a1o4.TabIndex = 3;
                // 
                // a1o5
                // 
                this.a1o5.BackColor = System.Drawing.Color.DarkRed;
                this.a1o5.Location = new System.Drawing.Point(-10, -5);
                this.a1o5.Name = "a1o5";
                this.a1o5.Size = new System.Drawing.Size(49, 165);
                this.a1o5.TabIndex = 4;
                // 
                // a1o6
                // 
                this.a1o6.BackColor = System.Drawing.Color.DarkRed;
                this.a1o6.Location = new System.Drawing.Point(22, -12);
                this.a1o6.Name = "a1o6";
                this.a1o6.Size = new System.Drawing.Size(29, 155);
                this.a1o6.TabIndex = 5;
                // 
                // a1o7
                // 
                this.a1o7.BackColor = System.Drawing.Color.DarkRed;
                this.a1o7.Location = new System.Drawing.Point(45, -27);
                this.a1o7.Name = "a1o7";
                this.a1o7.Size = new System.Drawing.Size(29, 154);
                this.a1o7.TabIndex = 6;
                // 
                // a1o8
                // 
                this.a1o8.BackColor = System.Drawing.Color.DarkRed;
                this.a1o8.Location = new System.Drawing.Point(71, -5);
                this.a1o8.Name = "a1o8";
                this.a1o8.Size = new System.Drawing.Size(21, 112);
                this.a1o8.TabIndex = 7;
                // 
                // a1o9
                // 
                this.a1o9.BackColor = System.Drawing.Color.DarkRed;
                this.a1o9.Location = new System.Drawing.Point(80, -27);
                this.a1o9.Name = "a1o9";
                this.a1o9.Size = new System.Drawing.Size(25, 112);
                this.a1o9.TabIndex = 8;
                // 
                // a1o10
                // 
                this.a1o10.BackColor = System.Drawing.Color.DarkRed;
                this.a1o10.Location = new System.Drawing.Point(140, -30);
                this.a1o10.Name = "a1o10";
                this.a1o10.Size = new System.Drawing.Size(51, 89);
                this.a1o10.TabIndex = 9;

                // 
                // warpS1
                // 
                this.warpS1.BackColor = System.Drawing.Color.Black;
                this.warpS1.Location = new System.Drawing.Point(294, 130);
                this.warpS1.Name = "warpS1";
                this.warpS1.Size = new System.Drawing.Size(35, 37);
                this.warpS1.TabIndex = 146;
                this.warpS1.Visible = false;

                if (opens1 == true)
                {
                    warpS1.Visible = true;
                }
                else if (opens1 == false)
                {
                    warpS1.Visible = false;
                }
                
                // 
                // warpA2
                // 
                this.warpA2.BackColor = System.Drawing.Color.Cyan;
                this.warpA2.Location = new System.Drawing.Point(111, 23);
                this.warpA2.Name = "warpA2";
                this.warpA2.Size = new System.Drawing.Size(26, 44);
                this.warpA2.TabIndex = 18;
                this.warpA2.Visible = false;

                // 
                // victory1message
                // 
                this.victory1message.BackColor = System.Drawing.Color.DarkRed;
                this.victory1message.Location = new System.Drawing.Point(-5, 165);
                this.victory1message.Name = "victory1message";
                this.victory1message.Size = new System.Drawing.Size(10, 38);
                this.victory1message.TabIndex = 75;
                this.victory1message.Visible = false;
                // 
                // victory2message
                // 
                this.victory2message.BackColor = System.Drawing.Color.DarkRed;
                this.victory2message.Location = new System.Drawing.Point(445, 165);
                this.victory2message.Name = "victory2message";
                this.victory2message.Size = new System.Drawing.Size(10, 38);
                this.victory2message.TabIndex = 75;
                this.victory2message.Visible = false;



                this.BackgroundImage = global::ZeldaDemo.Properties.Resources.a1;
                this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                this.BackColor = System.Drawing.Color.Khaki;
                warped = false;
            }

            #endregion
        }

        private void swingtimer1_Tick(object sender, EventArgs e)
        {
            shieldCollisonTimer.Stop();
            shieldCollsioncoords = shieldCollision.Location;
            shieldCollision.Location = new System.Drawing.Point(15000, 15000);
            if (attackCollision.Bounds.IntersectsWith(or1d.Bounds))
            {
                orHealth[0] -= 1;
                attackCollision.Location = new System.Drawing.Point(28000, 30000);
                hit.Play();
            }

            if (attackCollision.Bounds.IntersectsWith(or2r.Bounds))
            {
                orHealth[1] -= 1;
                attackCollision.Location = new System.Drawing.Point(28000, 30000);
                hit.Play();
            }

            if (attackCollision.Bounds.IntersectsWith(skel1.Bounds))
            {
                skelHealth[0] -= 1;
                attackCollision.Location = new System.Drawing.Point(28000, 30000);
                hit.Play();
            }

            if (attackCollision.Bounds.IntersectsWith(skel2.Bounds))
            {
                skelHealth[1] -= 1;
                attackCollision.Location = new System.Drawing.Point(28000, 30000);
                hit.Play();
            }

            if (attackCollision.Bounds.IntersectsWith(skel3.Bounds))
            {
                skelHealth[2] -= 1;
                attackCollision.Location = new System.Drawing.Point(28000, 30000);
                hit.Play();
            }

            if (attackCollision.Bounds.IntersectsWith(knightr.Bounds))
            {
                knightHealth -= 1;
                attackCollision.Location = new System.Drawing.Point(28000, 30000);
                hit.Play();
            }

            if (attackCollision.Bounds.IntersectsWith(ben1d.Bounds))
            {
                benHealth[0] -= 1;
                attackCollision.Location = new System.Drawing.Point(28000, 30000);
                hit.Play();
            }

            if (attackCollision.Bounds.IntersectsWith(ben2d.Bounds))
            {
                benHealth[1] -= 1;
                attackCollision.Location = new System.Drawing.Point(28000, 30000);
                hit.Play();
            }

            if (attackCollision.Bounds.IntersectsWith(boss.Bounds))
            {
                bossHealth -= 1;
                attackCollision.Location = new System.Drawing.Point(28000, 30000);
                hit.Play();
            }

            swingtimer1.Stop();
            swingtimer2.Start();
        }

        private void swingtimer2_Tick(object sender, EventArgs e)
        {
            swingtimer2.Stop();
            shieldCollision.Location = shieldCollsioncoords;
            shieldCollisonTimer.Start();
            attackCollision.Location = new System.Drawing.Point(23212, 22421);

            if (swingleft.Visible == true)
            {
                upani.Visible = false;
                leftani.Visible = false;
                rightani.Visible = false;
                downani.Visible = false;
                upidle.Visible = false;
                leftidle.Visible = true;
                downidle.Visible = false;
                rightidle.Visible = false;
                swingleft.Visible = false;
            }
            else if (swingright.Visible == true)
            {
                upani.Visible = false;
                leftani.Visible = false;
                rightani.Visible = false;
                downani.Visible = false;
                upidle.Visible = false;
                leftidle.Visible = false;
                downidle.Visible = false;
                rightidle.Visible = true;
                swingright.Visible = false;
            }

            else if (swingdown.Visible == true)
            {
                upani.Visible = false;
                leftani.Visible = false;
                rightani.Visible = false;
                downani.Visible = false;
                upidle.Visible = false;
                leftidle.Visible = false;
                downidle.Visible = true;
                rightidle.Visible = false;
                swingdown.Visible = false;
            }

            else if (swingup.Visible == true)
            {
                upani.Visible = false;
                leftani.Visible = false;
                rightani.Visible = false;
                downani.Visible = false;
                upidle.Visible = true;
                leftidle.Visible = false;
                downidle.Visible = false;
                rightidle.Visible = false;
                swingup.Visible = false;
            }

            swingdelay = false;
            attacking = false;

        }

        private void enemydeathTimer_Tick(object sender, EventArgs e)
        {
            #region death check
            if (orHealth[0] <= 0 && orHealth[0] >= -98)
            {
                orHealth[0] = -99;
                bombDropp.Location = or1d.Location;
                or1d.Location = new System.Drawing.Point(30000, 30000);
                or1u.Location = new System.Drawing.Point(30000, 30000);
                kill.Play();
            }

            if (orHealth[1] <= 0 && orHealth[1] >= -98)
            {
                orHealth[1] = -99;
                rupee.Location = or2l.Location;
                or2l.Location = new System.Drawing.Point(30000, 30000);
                or2r.Location = new System.Drawing.Point(30000, 30000);
                kill.Play();
            }

            if (skelHealth[0] <= 0 && skelHealth[0] >= -98)
            {
                skelHealth[0] = -99;
                skel1.Location = new System.Drawing.Point(27700, 27700);
                kill.Play();
            }
            if (skelHealth[1] <= 0 && skelHealth[1] >= -98)
            {
                skelHealth[1] = -99;
                heartdrop.Location = skel2.Location;
                skel2.Location = new System.Drawing.Point(27700, 27700);
                kill.Play();
            }


            if (skelHealth[2] <= 0 && skelHealth[2] >= -98)
            {
                skelHealth[2] = -99;
                skel3.Location = new System.Drawing.Point(27700, 27700);
                kill.Play();
            }

            if (benHealth[0] <= 0 && benHealth[0] >= -98)
            {
                benHealth[0] = -99;
                ben1u.Location = new System.Drawing.Point(27700, 27700);
                ben1d.Location = new System.Drawing.Point(27700, 27700);
                kill.Play();
            }

            if (benHealth[1] <= 0 && benHealth[1] >= -98)
            {
                benHealth[1] = -99;

                if (gotboomerang == false)
                {
                    unlockBoomerang.Location = ben2u.Location;
                }
                else
                {
                    heartdrop.Location = ben2u.Location;
                }
                ben2u.Location = new System.Drawing.Point(27700, 27700);
                ben2d.Location = new System.Drawing.Point(27700, 27700);

                kill.Play();
            }

            if (knightHealth <= 0 && knightHealth >= -98)
            {
                knightHealth = -99;
                heartDrop2.Location = knightr.Location;
                knightl.Location = new System.Drawing.Point(27700, 27700);
                knightr.Location = new System.Drawing.Point(27700, 27700);
                kill.Play();
            }

            if (bossHealth <= 0 && bossHealth >= -98)
            {
                bossHealth = -99;
                triforce.Location = boss.Location;
                bossTimer.Stop();
                boss.Location = new System.Drawing.Point(27700, 27700);
                bossflare1.Location = new System.Drawing.Point(30000, 30000);
                bossflare2.Location = new System.Drawing.Point(30000, 30000);
                bossflare3.Location = new System.Drawing.Point(30000, 30000);
                kill.Play();

                if (power == false)
                {
                    power = true;
                    MessageBox.Show("You have gained the Power to slay the mighty dragon.", "Power");
                }
            }



            #endregion

            #region shield defelect
            if (or1rock.Bounds.IntersectsWith(shieldCollision.Bounds))
            {
                or1rock.Location = new System.Drawing.Point(30000, 30000);
                deflect.Play();
            }

            if (or2rock.Bounds.IntersectsWith(shieldCollision.Bounds))
            {
                or2rock.Location = new System.Drawing.Point(30000, 30000);
                deflect.Play();
            }

            if (boome1.Bounds.IntersectsWith(shieldCollision.Bounds))
            {
                boome1.Location = new System.Drawing.Point(30000, 30000);
                deflect.Play();
            }

            if (boome2.Bounds.IntersectsWith(shieldCollision.Bounds))
            {
                boome2.Location = new System.Drawing.Point(30000, 30000);
                deflect.Play();
            }

#endregion

        }

        private void orTimer_Tick(object sender, EventArgs e)
        {
            #region octarock 1
            if (orsystem[0] >= 20)
            {
                orsystem[0] = 1;
                or1d.Visible = true;
                or1u.Visible = false;
                or1d.Top += 4;
                or1u.Top += 4;
            }


            if (orsystem[0] < 10)
            {
                or1d.Top += 4;
                or1u.Top += 4;
                orsystem[0] += 1;
                or1d.Visible = true;
                or1u.Visible = false;
            }
            else if (orsystem[0] >= 10)
            {
                or1d.Top -= 4;
                or1u.Top -= 4;
                orsystem[0] += 1;
                or1d.Visible = false;
                or1u.Visible = true;
            }


            if (orsystem[0] == 10)
            {
                or1rock.Location = or1u.Location;
                or1rock.Left += 4;
                or1rock.Top += 20;
                orrocksystem[0] = 1;
            }
            else if (orsystem[0] == 20)
            {
                or1rock.Location = or1u.Location;
                or1rock.Left += 4;
                or1rock.Top -= 10;
                orrocksystem[0] = 2;
            }


            if (orrocksystem[0] == 1)
            {
                or1rock.Top += 18;
            }
            else if (orrocksystem[0] == 2)
            {
                or1rock.Top -= 18;
            }
            #endregion

  
            #region octarock 2
            if (orsystem[1] >= 20)
            {
                orsystem[1] = 1;
                or2r.Visible = true;
                or2l.Visible = false;
                or2r.Left += 4;
                or2l.Left += 4;
            }


            if (orsystem[1] < 10)
            {
                or2r.Left += 4;
                or2l.Left += 4;
                orsystem[1] += 1;
                or2r.Visible = true;
                or2l.Visible = false;
            }
            else if (orsystem[1] >= 10)
            {
                or2r.Left -= 4;
                or2l.Left -= 4;
                orsystem[1] += 1;
                or2r.Visible = false;
                or2l.Visible = true;
            }


            if (orsystem[1] == 10)
            {
                or2rock.Location = or2l.Location;
                or2rock.Left += 4;
                or2rock.Top += 6;
                orrocksystem[1] = 1;
                
            }
            else if (orsystem[1] == 20)
            {
                or2rock.Location = or2l.Location;
                or2rock.Left -= 4;
                or2rock.Top += 6;
                orrocksystem[1] = 2;
            }


            if (orrocksystem[1] == 1)
            {
                or2rock.Left += 18;
            }
            else if (orrocksystem[1] == 2)
            {
                or2rock.Left -= 18;
            }
            #endregion

        }

        private void boomTimer_Tick(object sender, EventArgs e)
        {
            if (boomerangdirection == 1)
            {
                boomerang.Top -= 12;
                boomlimit += 1;
            }

            else if (boomerangdirection == 2)
            {
                boomerang.Top += 12;
                boomlimit += 1;
            }

            else if (boomerangdirection == 3)
            {
                boomerang.Left -= 12;
                boomlimit += 1;
            }

            else if (boomerangdirection == 4)
            {
                boomerang.Left += 12;
                boomlimit += 1;
            }


            //attacks, not freezes
            #region attacking

            if (boomerang.Bounds.IntersectsWith(or1d.Bounds))
            {
                orHealth[0] -= 1;
                boomerang.Location = new System.Drawing.Point(28000, 30000);
                boomTimer.Stop();
                boomerangthrown = false;
                boomerang.Visible = false;
                boomerangthrow.Stop();
                hit.Play();
            }

            if (boomerang.Bounds.IntersectsWith(or2r.Bounds))
            {
                orHealth[1] -= 1;
                boomerang.Location = new System.Drawing.Point(28000, 30000);
                boomTimer.Stop();
                boomerangthrown = false;
                boomerang.Visible = false;
                boomerangthrow.Stop();
                hit.Play();
            }

            if (boomerang.Bounds.IntersectsWith(skel1.Bounds))
            {
                skelHealth[0] -= 1;
                boomerang.Location = new System.Drawing.Point(28000, 30000);
                boomTimer.Stop();
                boomerangthrown = false;
                boomerang.Visible = false;
                boomerangthrow.Stop();
                hit.Play();
            }

            if (boomerang.Bounds.IntersectsWith(skel2.Bounds))
            {
                skelHealth[1] -= 1;
                boomerang.Location = new System.Drawing.Point(28000, 30000);
                boomTimer.Stop();
                boomerangthrown = false;
                boomerang.Visible = false;
                boomerangthrow.Stop();
                hit.Play();
            }

            if (boomerang.Bounds.IntersectsWith(skel3.Bounds))
            {
                skelHealth[2] -= 1;
                boomerang.Location = new System.Drawing.Point(28000, 30000);
                boomTimer.Stop();
                boomerangthrown = false;
                boomerang.Visible = false;
                boomerangthrow.Stop();
                hit.Play();
            }

            if (boomerang.Bounds.IntersectsWith(ben1d.Bounds))
            {
                benHealth[0] -= 1;
                boomerang.Location = new System.Drawing.Point(28000, 30000);
                boomTimer.Stop();
                boomerangthrown = false;
                boomerang.Visible = false;
                boomerangthrow.Stop();
                hit.Play();
            }

            if (boomerang.Bounds.IntersectsWith(ben2d.Bounds))
            {
                benHealth[1] -= 1;
                boomerang.Location = new System.Drawing.Point(28000, 30000);
                boomTimer.Stop();
                boomerangthrown = false;
                boomerang.Visible = false;
                boomerangthrow.Stop();
                hit.Play();
            }

            if (boomerang.Bounds.IntersectsWith(knightr.Bounds))
            {
                knightHealth -= 1;
                boomerang.Location = new System.Drawing.Point(28000, 30000);
                boomTimer.Stop();
                boomerangthrown = false;
                boomerang.Visible = false;
                boomerangthrow.Stop();
                hit.Play();
            }

            if (boomerang.Bounds.IntersectsWith(boss.Bounds))
            {
                bossHealth -= 1;
                boomerang.Location = new System.Drawing.Point(28000, 30000);
                boomTimer.Stop();
                boomerangthrown = false;
                boomerang.Visible = false;
                boomerangthrow.Stop();
                hit.Play();
            }





            #endregion      

            if (boomlimit == 8) // stops boomerang function
            {
                boomerang.Location = new System.Drawing.Point(21000, 21000);
                boomTimer.Stop();
                boomerangthrown = false;
                boomerang.Visible = false;
                boomerangthrow.Stop();

            }
        }

        private void bombtimer_Tick(object sender, EventArgs e)
        {
            if (bomblimit == 20)
            {
                explosion.Location = bomb.Location;
                explosion.Left -= 15;
                explosion.Top -= 5;
                explosion.Visible = true;
                bomb.Location = new System.Drawing.Point(22000, 22000);
                bomb.Visible = false;
                bombblow.Play();
            }
            
            if (bomblimit == 40)
            {
                bombtimer.Stop();
                explosion.Visible = false;
                explosion.Location = new System.Drawing.Point(19900, 19900);
                bombplaced = false;
            }


            #region attack

            if (explosion.Bounds.IntersectsWith(or1d.Bounds))
            {
                orHealth[0] -= 2;
                explosion.Location = new System.Drawing.Point(28000, 30000);
                bombtimer.Stop();
                bombplaced = false;
                explosion.Visible = false;
                hit.Play();
            }

            if (explosion.Bounds.IntersectsWith(or2r.Bounds))
            {
                orHealth[1] -= 2;
                explosion.Location = new System.Drawing.Point(28000, 30000);
                bombtimer.Stop();
                bombplaced = false;
                explosion.Visible = false;
                hit.Play();
            }

            if (explosion.Bounds.IntersectsWith(skel1.Bounds))
            {
                skelHealth[0] -= 2;
                explosion.Location = new System.Drawing.Point(28000, 30000);
                bombtimer.Stop();
                bombplaced = false;
                explosion.Visible = false;
                hit.Play();
            }

            if (explosion.Bounds.IntersectsWith(skel2.Bounds))
            {
                skelHealth[1] -= 2;
                explosion.Location = new System.Drawing.Point(28000, 30000);
                bombtimer.Stop();
                bombplaced = false;
                explosion.Visible = false;
                hit.Play();
            }

            if (explosion.Bounds.IntersectsWith(skel3.Bounds))
            {
                skelHealth[2] -= 2;
                explosion.Location = new System.Drawing.Point(28000, 30000);
                bombtimer.Stop();
                bombplaced = false;
                explosion.Visible = false;
                hit.Play();
            }

            if (explosion.Bounds.IntersectsWith(ben1d.Bounds))
            {
                benHealth[0] -= 2;
                explosion.Location = new System.Drawing.Point(28000, 30000);
                bombtimer.Stop();
                bombplaced = false;
                explosion.Visible = false;
                hit.Play();
            }

            if (explosion.Bounds.IntersectsWith(ben2d.Bounds))
            {
                benHealth[1] -= 2;
                explosion.Location = new System.Drawing.Point(28000, 30000);
                bombtimer.Stop();
                bombplaced = false;
                explosion.Visible = false;
                hit.Play();
            }

            if (explosion.Bounds.IntersectsWith(knightr.Bounds))
            {
                knightHealth -= 2;
                explosion.Location = new System.Drawing.Point(28000, 30000);
                bombtimer.Stop();
                bombplaced = false;
                explosion.Visible = false;
                hit.Play();
            }
            if (explosion.Bounds.IntersectsWith(warpD1.Bounds) && opend1 == false)
            {
                opend1 = true;
                warpD1.Visible = true;
                secret.Play();
            }

            if (explosion.Bounds.IntersectsWith(warpS1.Bounds) && opens1 == false)
            {
                opens1 = true;
                warpS1.Visible = true;
                secret.Play();
            }

            if (explosion.Bounds.IntersectsWith(boss.Bounds))
            {
                bossHealth -= 2;
                explosion.Location = new System.Drawing.Point(28000, 30000);
                bombtimer.Stop();
                bombplaced = false;
                explosion.Visible = false;
                hit.Play();
            }

            #endregion

            bomblimit += 1;
        }

        private void beamTimer_Tick(object sender, EventArgs e)
        {

            if (beamdirection == 1)
            {
                swordbeamup.Top -= 16;
                beamlimit += 1;
            }

            else if (beamdirection == 2)
            {
                swordbeamdown.Top += 16;
                beamlimit += 1;
            }

            else if (beamdirection == 3)
            {
                swordbeamleft.Left -= 16;
                beamlimit += 1;
            }

            else if (beamdirection == 4)
            {
                swordbeamright.Left += 16;
                beamlimit += 1;
            }

            #region attacking for sword beam

            if (swordbeamdown.Bounds.IntersectsWith(or1d.Bounds) | swordbeamleft.Bounds.IntersectsWith(or1d.Bounds) | swordbeamright.Bounds.IntersectsWith(or1d.Bounds) | swordbeamup.Bounds.IntersectsWith(or1d.Bounds))
            {
                orHealth[0] -= 1;
                swordbeamdown.Location = new System.Drawing.Point(23200, 18000);
                swordbeamup.Location = new System.Drawing.Point(23200, 18000);
                swordbeamleft.Location = new System.Drawing.Point(23200, 18000);
                swordbeamright.Location = new System.Drawing.Point(23200, 18000);
                beamTimer.Stop();
                shootbeam = false;
                swordbeamup.Visible = false;
                swordbeamright.Visible = false;
                swordbeamleft.Visible = false;
                swordbeamdown.Visible = false;
                swing.Stop();
                hit.Play();
            }

            if (swordbeamdown.Bounds.IntersectsWith(or2r.Bounds) || swordbeamleft.Bounds.IntersectsWith(or2r.Bounds) || swordbeamright.Bounds.IntersectsWith(or2r.Bounds) || swordbeamup.Bounds.IntersectsWith(or2r.Bounds))
            {
                orHealth[1] -= 1;
                swordbeamdown.Location = new System.Drawing.Point(23200, 18000);
                swordbeamup.Location = new System.Drawing.Point(23200, 18000);
                swordbeamleft.Location = new System.Drawing.Point(23200, 18000);
                swordbeamright.Location = new System.Drawing.Point(23200, 18000);
                beamTimer.Stop();
                shootbeam = false;
                swordbeamup.Visible = false;
                swordbeamright.Visible = false;
                swordbeamleft.Visible = false;
                swordbeamdown.Visible = false;
                swing.Stop();
                hit.Play();
            }

            if (swordbeamdown.Bounds.IntersectsWith(skel1.Bounds) || swordbeamleft.Bounds.IntersectsWith(skel1.Bounds) || swordbeamright.Bounds.IntersectsWith(skel1.Bounds) || swordbeamup.Bounds.IntersectsWith(skel1.Bounds))
            {
                skelHealth[0] -= 1;
                swordbeamdown.Location = new System.Drawing.Point(23200, 18000);
                swordbeamup.Location = new System.Drawing.Point(23200, 18000);
                swordbeamleft.Location = new System.Drawing.Point(23200, 18000);
                swordbeamright.Location = new System.Drawing.Point(23200, 18000);
                beamTimer.Stop();
                shootbeam = false;
                swordbeamup.Visible = false;
                swordbeamright.Visible = false;
                swordbeamleft.Visible = false;
                swordbeamdown.Visible = false;
                swing.Stop();
                hit.Play();
            }

            if (swordbeamdown.Bounds.IntersectsWith(skel2.Bounds) || swordbeamleft.Bounds.IntersectsWith(skel2.Bounds) || swordbeamright.Bounds.IntersectsWith(skel2.Bounds) || swordbeamup.Bounds.IntersectsWith(skel2.Bounds))
            {
                skelHealth[1] -= 1;
                swordbeamdown.Location = new System.Drawing.Point(23200, 18000);
                swordbeamup.Location = new System.Drawing.Point(23200, 18000);
                swordbeamleft.Location = new System.Drawing.Point(23200, 18000);
                swordbeamright.Location = new System.Drawing.Point(23200, 18000);
                beamTimer.Stop();
                shootbeam = false;
                swordbeamup.Visible = false;
                swordbeamright.Visible = false;
                swordbeamleft.Visible = false;
                swordbeamdown.Visible = false;
                swing.Stop();
                hit.Play();
            }

            if (swordbeamdown.Bounds.IntersectsWith(skel3.Bounds) || swordbeamleft.Bounds.IntersectsWith(skel3.Bounds) || swordbeamright.Bounds.IntersectsWith(skel3.Bounds) || swordbeamup.Bounds.IntersectsWith(skel3.Bounds))
            {
                skelHealth[2] -= 1;
                swordbeamdown.Location = new System.Drawing.Point(23200, 18000);
                swordbeamup.Location = new System.Drawing.Point(23200, 18000);
                swordbeamleft.Location = new System.Drawing.Point(23200, 18000);
                swordbeamright.Location = new System.Drawing.Point(23200, 18000);
                beamTimer.Stop();
                shootbeam = false;
                swordbeamup.Visible = false;
                swordbeamright.Visible = false;
                swordbeamleft.Visible = false;
                swordbeamdown.Visible = false;
                swing.Stop();
                hit.Play();
            }

            if (swordbeamdown.Bounds.IntersectsWith(ben1u.Bounds) || swordbeamleft.Bounds.IntersectsWith(ben1u.Bounds) || swordbeamright.Bounds.IntersectsWith(ben1u.Bounds) || swordbeamup.Bounds.IntersectsWith(ben1u.Bounds))
            {
                benHealth[0] -= 1;
                swordbeamdown.Location = new System.Drawing.Point(23200, 18000);
                swordbeamup.Location = new System.Drawing.Point(23200, 18000);
                swordbeamleft.Location = new System.Drawing.Point(23200, 18000);
                swordbeamright.Location = new System.Drawing.Point(23200, 18000);
                beamTimer.Stop();
                shootbeam = false;
                swordbeamup.Visible = false;
                swordbeamright.Visible = false;
                swordbeamleft.Visible = false;
                swordbeamdown.Visible = false;
                swing.Stop();
                hit.Play();
            }

            if (swordbeamdown.Bounds.IntersectsWith(ben2u.Bounds) || swordbeamleft.Bounds.IntersectsWith(ben2u.Bounds) || swordbeamright.Bounds.IntersectsWith(ben2u.Bounds) || swordbeamup.Bounds.IntersectsWith(ben2u.Bounds))
            {
                benHealth[1] -= 1;
                swordbeamdown.Location = new System.Drawing.Point(23200, 18000);
                swordbeamup.Location = new System.Drawing.Point(23200, 18000);
                swordbeamleft.Location = new System.Drawing.Point(23200, 18000);
                swordbeamright.Location = new System.Drawing.Point(23200, 18000);
                beamTimer.Stop();
                shootbeam = false;
                swordbeamup.Visible = false;
                swordbeamright.Visible = false;
                swordbeamleft.Visible = false;
                swordbeamdown.Visible = false;
                swing.Stop();
                hit.Play();
            }

            if (swordbeamdown.Bounds.IntersectsWith(knightr.Bounds) || swordbeamleft.Bounds.IntersectsWith(knightr.Bounds) || swordbeamright.Bounds.IntersectsWith(knightr.Bounds) || swordbeamup.Bounds.IntersectsWith(knightr.Bounds))
            {
                knightHealth -= 1;
                swordbeamdown.Location = new System.Drawing.Point(23200, 18000);
                swordbeamup.Location = new System.Drawing.Point(23200, 18000);
                swordbeamleft.Location = new System.Drawing.Point(23200, 18000);
                swordbeamright.Location = new System.Drawing.Point(23200, 18000);
                beamTimer.Stop();
                shootbeam = false;
                swordbeamup.Visible = false;
                swordbeamright.Visible = false;
                swordbeamleft.Visible = false;
                swordbeamdown.Visible = false;
                swing.Stop();
                hit.Play();
            }

            if (swordbeamdown.Bounds.IntersectsWith(boss.Bounds) || swordbeamleft.Bounds.IntersectsWith(boss.Bounds) || swordbeamright.Bounds.IntersectsWith(boss.Bounds) || swordbeamup.Bounds.IntersectsWith(boss.Bounds))
            {
                bossHealth -= 1;
                swordbeamdown.Location = new System.Drawing.Point(23200, 18000);
                swordbeamup.Location = new System.Drawing.Point(23200, 18000);
                swordbeamleft.Location = new System.Drawing.Point(23200, 18000);
                swordbeamright.Location = new System.Drawing.Point(23200, 18000);
                beamTimer.Stop();
                shootbeam = false;
                swordbeamup.Visible = false;
                swordbeamright.Visible = false;
                swordbeamleft.Visible = false;
                swordbeamdown.Visible = false;
                swing.Stop();
                hit.Play();
            }

            #endregion

            if (beamlimit >= 12)
            {
                swordbeamdown.Location = new System.Drawing.Point(23200, 18000);
                swordbeamup.Location = new System.Drawing.Point(23200, 18000);
                swordbeamleft.Location = new System.Drawing.Point(23200, 18000);
                swordbeamright.Location = new System.Drawing.Point(23200, 18000);
                beamTimer.Stop();
                shootbeam = false;
                swordbeamup.Visible = false;
                swordbeamright.Visible = false;
                swordbeamleft.Visible = false;
                swordbeamdown.Visible = false;

            }

        }

        private void linksHealth_Tick(object sender, EventArgs e)
        {
            if (health == 3)
            {
                eheart1.Visible = false;
                eheart2.Visible = false;
                eheart3.Visible = false;
                heart1.Visible = true;
                heart2.Visible = true;
                heart3.Visible = true;
                healthlow = false;
                healthlowsfx.Ctlcontrols.stop();
            }
            else if (health == 2)
            {
                eheart1.Visible = true;
                eheart2.Visible = false;
                eheart3.Visible = false;
                heart1.Visible = false;
                heart2.Visible = true;
                heart3.Visible = true;
                healthlow = false;
                healthlowsfx.Ctlcontrols.stop();
            }

            else if (health == 1)
            {
                eheart1.Visible = true;
                eheart2.Visible = true;
                eheart3.Visible = false;
                heart1.Visible = false;
                heart2.Visible = false;
                heart3.Visible = true;

                if (healthlow == false)
                {
                    healthlow = true;
                    healthlowsfx.URL = "sfx/lowhealth.wav";
                    healthlowsfx.Ctlcontrols.play();
                    healthlowsfx.settings.setMode("loop", true);
                }
            }

            else if (health <= 0)
            {
                eheart1.Visible = true;
                eheart2.Visible = true;
                eheart3.Visible = true;
                heart1.Visible = false;
                heart2.Visible = false;
                heart3.Visible = false;
                upidle.Location = new System.Drawing.Point(12120, 12100);
                linksHealth.Stop();
                overworldbgm.Ctlcontrols.stop();
                healthlowsfx.Ctlcontrols.stop();
                gameoverscreen.Location = new System.Drawing.Point(0, 0);
                gameoverscreen.Visible = true;
                MoveandAniTimer.Stop();
                orTimer.Stop();
                skelTimer.Stop();
                benTimer.Stop();
                bossTimer.Stop();
                govertimer.Start();
                boomTimer.Stop();
                bombtimer.Stop();
                die.Play();
                gameover = true;
                
            }

            #region link getting hurt
            if (upidle.Bounds.IntersectsWith(or1d.Bounds) && invincibility == false)
            {
                health -= 1;
                hurt.Play();
                invincibility = true;
                invinTimer.Start();
                invinlimit = 0;
            }

            if (upidle.Bounds.IntersectsWith(or2r.Bounds) && invincibility == false)
            {
                health -= 1;
                hurt.Play();
                invincibility = true;
                invinTimer.Start();
                invinlimit = 0;
            }

            if (upidle.Bounds.IntersectsWith(or1rock.Bounds) && invincibility == false)
            {
                health -= 1;
                hurt.Play();
                invincibility = true;
                invinTimer.Start();
                invinlimit = 0;
                or1rock.Location = new System.Drawing.Point(26000, 26000);
            }

            if (upidle.Bounds.IntersectsWith(or2rock.Bounds) && invincibility == false)
            {
                health -= 1;
                hurt.Play();
                invincibility = true;
                invinTimer.Start();
                invinlimit = 0;
                or2rock.Location = new System.Drawing.Point(26000, 26000);

            }

            if (upidle.Bounds.IntersectsWith(skel1.Bounds) && invincibility == false)
            {
                health -= 1;
                hurt.Play();
                invincibility = true;
                invinTimer.Start();
                invinlimit = 0;

            }

            if (upidle.Bounds.IntersectsWith(skel2.Bounds) && invincibility == false)
            {
                health -= 1;
                hurt.Play();
                invincibility = true;
                invinTimer.Start();
                invinlimit = 0;

            }

            if (upidle.Bounds.IntersectsWith(skel3.Bounds) && invincibility == false)
            {
                health -= 1;
                hurt.Play();
                invincibility = true;
                invinTimer.Start();
                invinlimit = 0;

            }

            if (upidle.Bounds.IntersectsWith(ben1u.Bounds) && invincibility == false)
            {
                health -= 1;
                hurt.Play();
                invincibility = true;
                invinTimer.Start();
                invinlimit = 0;

            }

            if (upidle.Bounds.IntersectsWith(ben2u.Bounds) && invincibility == false)
            {
                health -= 1;
                hurt.Play();
                invincibility = true;
                invinTimer.Start();
                invinlimit = 0;

            }

            if (upidle.Bounds.IntersectsWith(boome1.Bounds) && invincibility == false)
            {
                health -= 1;
                hurt.Play();
                invincibility = true;
                boome1.Location = new System.Drawing.Point(27000, 27000);
                invinTimer.Start();
                invinlimit = 0;

            }

            if (upidle.Bounds.IntersectsWith(boome2.Bounds) && invincibility == false)
            {
                health -= 1;
                hurt.Play();
                invincibility = true;
                boome2.Location = new System.Drawing.Point(27000, 27000);
                invinTimer.Start();
                invinlimit = 0;

            }

            if (upidle.Bounds.IntersectsWith(knightr.Bounds) && invincibility == false)
            {
                health -= 1;
                hurt.Play();
                invincibility = true;
                invinTimer.Start();
                invinlimit = 0;

            }

            if (upidle.Bounds.IntersectsWith(boss.Bounds))
            {
                health -= 3;
                hurt.Play();
                invincibility = true;
                invinTimer.Start();
                invinlimit = 0;
            }

            if (upidle.Bounds.IntersectsWith(bossflare1.Bounds))
            {
                health -= 2;
                hurt.Play();
                invincibility = true;
                invinTimer.Start();
                invinlimit = 0;
                bossflare1.Location = new System.Drawing.Point(30000, 30000);
            }

            if (upidle.Bounds.IntersectsWith(bossflare2.Bounds))
            {
                health -= 2;
                hurt.Play();
                invincibility = true;
                invinTimer.Start();
                invinlimit = 0;
                bossflare2.Location = new System.Drawing.Point(30000, 30000);
            }

            if (upidle.Bounds.IntersectsWith(bossflare3.Bounds))
            {
                health -= 2;
                hurt.Play();
                invincibility = true;
                invinTimer.Start();
                invinlimit = 0;
                bossflare3.Location = new System.Drawing.Point(30000, 30000);
            }

            if (upidle.Bounds.IntersectsWith(victory1message.Bounds) && victory == true)
            {
                linksHealth.Stop();
                MessageBox.Show("42013. Such a value that has forsaken me. It is undefinied, and forever will be. Goodbye.", "null");
                Application.Exit();
            }

            if (upidle.Bounds.IntersectsWith(victory2message.Bounds) && victory == true)
            {
                linksHealth.Stop();
                MessageBox.Show("To my new soulmate, you now know who I am. I am a person who loves creating virutal worlds. Worlds that contain elements that do not exist. Coding allows us to make those worlds a reality. It's what I love doing.", "null");
                Application.Exit();
            }

        }
#endregion

        private void invinTimer_Tick(object sender, EventArgs e)
        {
            if (invinlimit >= 30)
            {
                invinTimer.Stop();
                invincibility = false;
            }

            invinlimit += 1;
        }

        private void govertimer_Tick(object sender, EventArgs e)
        {
            govertimer.Stop();
            MessageBox.Show("Getting hurt left and right. Your heart grows smaller, second by second. Trusts are broken day by day. The value 42013 is undefined. Please check it and try again.", "null");
            Application.Exit();
        }

        private void skelTimer_Tick(object sender, EventArgs e)
        {
            #region skel 1 - up and down
            if (skelsystem[0] >= 40)
            {
                skelsystem[0] = 1;
                skel1.Top += 4;
            }


            if (skelsystem[0] < 20)
            {
                skel1.Top += 4;
                skelsystem[0] += 1;
            }
            else if (skelsystem[0] >= 20)
            {
                skel1.Top -= 4;
                skelsystem[0] += 1;
            }


            #endregion

            #region skel 2 - left and right
            if (skelsystem[1] >= 40)
            {
                skelsystem[1] = 1;
                skel2.Left += 4;
            }


            if (skelsystem[1] < 20)
            {
                skel2.Left += 4;
                skelsystem[1] += 1;
            }
            else if (skelsystem[1] >= 20)
            {
                skel2.Left -= 4;
                skelsystem[1] += 1;
            }


            #endregion

            #region skel 3 - up and down
            if (skelsystem[2] >= 40)
            {
                skelsystem[2] = 1;
                skel3.Top += 4;
            }


            if (skelsystem[2] < 20)
            {
                skel3.Top += 4;
                skelsystem[2] += 1;
            }
            else if (skelsystem[2] >= 20)
            {
                skel3.Top -= 4;
                skelsystem[2] += 1;
            }


            #endregion
        }

        private void dungeonTimer_Tick(object sender, EventArgs e)
        {
            if (skelHealth[0] == -99 && skelHealth[1] == -99 && skelHealth[2] == -99 && courage == false)
            {
                courage = true;
                MessageBox.Show("You have gained the Courage to fight back the skeletons.", "Courage");
            }
            if (attackCollision.Bounds.IntersectsWith(unlocker.Bounds) && wisdom == false)
            {
                wisdom = true;
                MessageBox.Show("You have gained the Wisdom to see such a difference between this room and the previous room.", "Wisdom");
            }


            #region unlock next room in dungeon

            if (courage == true)
            {
                barricade.Location = new System.Drawing.Point(30000, 30000);
                barricade.Visible = false;
            }
            if (wisdom == true)
            {
                barricade2.Location = new System.Drawing.Point(30000, 30000);
                barricade2.Visible = false;
            }

#endregion
        }

        private void benTimer_Tick(object sender, EventArgs e)
        {
            #region ben 1
            if (bensystem[0] >= 20)
            {
                bensystem[0] = 1;
                ben1d.Visible = true;
                ben1u.Visible = false;
                ben1d.Top += 4;
                ben1u.Top += 4;
            }


            if (bensystem[0] < 10)
            {
                ben1d.Top += 4;
                ben1u.Top += 4;
                bensystem[0] += 1;
                ben1d.Visible = true;
                ben1u.Visible = false;
            }
            else if (bensystem[0] >= 10)
            {
                ben1d.Top -= 4;
                ben1u.Top -= 4;
                bensystem[0] += 1;
                ben1d.Visible = false;
                ben1u.Visible = true;
            }


            if (bensystem[0] == 10)
            {
                boome1.Location = ben1u.Location;
                boome1.Left += 4;
                boome1.Top += 20;
                benboomsystem[0] = 1;
            }
            else if (bensystem[0] == 20)
            {
                boome1.Location = ben1u.Location;
                boome1.Left += 4;
                boome1.Top -= 10;
                benboomsystem[0] = 2;
            }


            if (benboomsystem[0] == 1)
            {
                boome1.Top += 18;
            }
            else if (benboomsystem[0] == 2)
            {
                boome1.Top -= 18;
            }
            #endregion

            #region ben 2
            if (bensystem[1] >= 20)
            {
                bensystem[1] = 1;
                ben2d.Visible = true;
                ben2u.Visible = false;
                ben2d.Top += 4;
                ben2u.Top += 4;
            }


            if (bensystem[1] < 10)
            {
                ben2d.Top += 4;
                ben2u.Top += 4;
                bensystem[1] += 1;
                ben2d.Visible = true;
                ben2u.Visible = false;
            }
            else if (bensystem[1] >= 10)
            {
                ben2d.Top -= 4;
                ben2u.Top -= 4;
                bensystem[1] += 1;
                ben2d.Visible = false;
                ben2u.Visible = true;
            }


            if (bensystem[1] == 10)
            {
                boome2.Location = ben2u.Location;
                boome2.Left += 4;
                boome2.Top += 20;
                benboomsystem[1] = 1;
            }
            else if (bensystem[1] == 20)
            {
                boome2.Location = ben2u.Location;
                boome2.Left += 4;
                boome2.Top -= 10;
                benboomsystem[1] = 2;
            }


            if (benboomsystem[1] == 1)
            {
                boome2.Top += 18;
            }
            else if (benboomsystem[1] == 2)
            {
                boome2.Top -= 18;
            }
            #endregion


            if (knightSystem >= 40)
            {
                knightSystem = 1;
                knightr.Left += 4;
                knightl.Left += 4;
                knightr.Visible = true;
                knightl.Visible = false;
            }


            if (knightSystem < 20)
            {
                knightr.Left += 4;
                knightl.Left += 4;
                knightSystem += 1;
                knightr.Visible = true;
                knightl.Visible = false;
            }
            else if (knightSystem >= 20)
            {
                knightr.Left -= 4;
                knightl.Left -= 4;
                knightSystem += 1;
                knightr.Visible = false;
                knightl.Visible = true;
            }
        }

        private void bossTimer_Tick(object sender, EventArgs e)
        {
            if (bossystem >= 40)
            {
                bossystem = 1;
                boss.Left -= 4;
            }
            
            if (bossystem <= 20)
            {
                boss.Left -= 4;
                bossystem += 1;
            }

            if (bossystem >= 20)
            {
                boss.Left += 4;
                bossystem += 1;
            }

            if (bossystem == 40)
            {
                bossflare1.Location = boss.Location;
                bossflare1.Left -= 10;
                bossflare1.Top -= 40;
                bossflare2.Location = boss.Location;
                bossflare2.Left -= 10;
                bossflare2.Top += 20;
                bossflare3.Location = boss.Location;
                bossflare3.Left -= 10;
                bossflare3.Top += 80;
                bossflaresystem = 1;
                if (roomid == 6 && bossHealth >= 1)
                {
                    bossscream.Play();
                }
            }

            if (bossflaresystem == 1)
            {
                bossflare1.Left -= 24;
                bossflare2.Left -= 24;
                bossflare3.Left -= 24;
            }
                
        }

        private void victoryTimer_Tick(object sender, EventArgs e)
        {
            // wipe d3
            warped = true;
            roomid = 3;
            warptoa3from = 2;
            d1o1.Location = new System.Drawing.Point(30000, 30000);
            d1o2.Location = new System.Drawing.Point(30000, 30000);
            d1o3.Location = new System.Drawing.Point(30000, 30000);
            d1o4.Location = new System.Drawing.Point(30000, 30000);
            d1o5.Location = new System.Drawing.Point(30000, 30000);
            d1o6.Location = new System.Drawing.Point(30000, 30000);
            d1o7.Location = new System.Drawing.Point(30000, 30000);
            d1o8.Location = new System.Drawing.Point(30000, 30000);
            d1o9.Location = new System.Drawing.Point(30000, 30000);
            d1o10.Location = new System.Drawing.Point(30000, 30000);
            d1o11.Location = new System.Drawing.Point(30000, 30000);
            warpd2fd3.Location = new System.Drawing.Point(30000, 30000);
            boss.Location = new System.Drawing.Point(30000, 30000);
            bossflare1.Location = new System.Drawing.Point(30000, 30000);
            bossflare2.Location = new System.Drawing.Point(30000, 30000);
            bossflare3.Location = new System.Drawing.Point(30000, 30000);
            linkgettriforce.Location = new System.Drawing.Point(30000, 30000);
            triforce.Location = new System.Drawing.Point(30000, 30000);
            linkgettriforce.Visible = false;
            MoveandAniTimer.Enabled = true;
            victoryTimer.Stop();
            gettingtriforce = false;
            triforceDisplay.Visible = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            credits openmofo = new credits();
            openmofo.Show();
            titlebgm.PlayLooping();
        }

        private void label1_MouseEnter(object sender, EventArgs e)
        {
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));

        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

        }

        private void shieldCollisonTimer_Tick(object sender, EventArgs e)
        {
            if (upani.Visible == true || upidle.Visible == true)
            {
                shieldCollision.Location = upidle.Location;
                shieldCollision.Left += 5;
                shieldCollision.Top -= 15;
            }
            else if (downani.Visible == true || downidle.Visible == true)
            {
                shieldCollision.Location = upidle.Location;
                shieldCollision.Left += 5;
                shieldCollision.Top += 25;
            }
            else if (rightani.Visible == true || rightidle.Visible == true)
            {
                shieldCollision.Location = upidle.Location;
                shieldCollision.Left += 25;
                shieldCollision.Top += 12;
            }
            else if (leftani.Visible == true || leftidle.Visible == true)
            {
                shieldCollision.Location = upidle.Location;
                shieldCollision.Left -= 12;
                shieldCollision.Top += 12;
            }

        }

        private void secretr_Click(object sender, EventArgs e)
        {
            if (gotsecret == false)
                secretr.Location = new System.Drawing.Point(30000, 30000);
            MessageBox.Show("It's a secret to everybody.", "null");
            rupeecount += 500;
            gotsecret = true;
        }

    }
}



