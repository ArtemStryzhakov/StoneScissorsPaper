using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace StoneScissorsPaper
{
    public partial class Form1 : Form
    {
        TextBox txtB;
        Label lbl_pl;
        Label lbl_bot;
        PictureBox pic_pl;
        PictureBox pic_bot;
        PictureBox game;
        Label score;
        Label score_2;
        int counter;
        int counter_2;
        string readWin;
        Label lbl_win;
        Button bd_1;
        Button bd_2;
        Button bd_3;
        Button btn_game;
        string rep;
        string rep_2;
        List<string> list_img;
        Button buttonSound;
        int sound = 0;

        public Form1()
        {          
            list_img = new List<string>();
            list_img.Add("stone.png");
            list_img.Add("scirrors.png");
            list_img.Add("paper.png");

            this.Size = new Size(500, 300);
            this.Text = "Stone / Scirrors / Paper";
            this.BackgroundImage = new Bitmap(@"../../Backgrounds/background1.jpg");
            
            Label lbl = new Label();
            lbl.Text = "Enter your nickName:";
            lbl.Location = new Point(190, 70);
            lbl.Size = new Size(110, 15);
            lbl.BackColor = Color.Red;
            
            txtB = new TextBox();
            txtB.Location = new Point(125, 90);
            txtB.Size = new Size(250, 30);
                        
            Button btn = new Button();
            btn.Size = new Size(150, 30);
            btn.Location = new Point(170, 120);
            btn.Text = "Continue";
            btn.BackColor = Color.FromArgb(153, 0, 0);
            btn.Click += Btn_Click;

            // -------------------Label which shows Player's name---------------------------
            lbl_pl = new Label();
            lbl_pl.Location = new Point(300, 100);
            lbl_pl.Font = new Font(Font.FontFamily, 20);
            lbl_pl.BackColor = Color.FromArgb(51, 0, 51);
            lbl_pl.ForeColor = Color.White;
            lbl_pl.TextChanged += Lbl_pl_TextChanged;
            // -----------------------------------------------------------------------------

            lbl_bot = new Label();
            lbl_bot.Text = "Bot Zhora";
            lbl_bot.Location = new Point(1000, 100);
            lbl_bot.Size = new Size(133, 30);
            lbl_bot.BackColor = Color.FromArgb(51, 0, 51);
            lbl_bot.ForeColor = Color.White;
            lbl_bot.Font = new Font(Font.FontFamily, 20);

            // Add elements
            this.Controls.Add(txtB);
            this.Controls.Add(lbl);
            this.Controls.Add(btn);
        }

        private void ButtonEl()
        {
            bd_1 = new Button();
            bd_2 = new Button();
            bd_3 = new Button();

            bd_1.Text = "Rock";
            bd_1.Font = new Font(Font.FontFamily, 20);
            bd_1.Location = new Point(490, 400);
            bd_1.Size = new Size(120, 60);
            bd_1.BackColor = Color.Purple;
            bd_1.ForeColor = Color.White;
            bd_1.Click += Bd_1_Click;

            bd_2.Text = "Scirrors";
            bd_2.Font = new Font(Font.FontFamily, 20);
            bd_2.Location = new Point(640, 400);
            bd_2.Size = new Size(120, 60);
            bd_2.BackColor = Color.Purple;
            bd_2.ForeColor = Color.White;
            bd_2.Click += Bd_2_Click;

            bd_3.Text = "Paper";
            bd_3.Font = new Font(Font.FontFamily, 20);
            bd_3.Location = new Point(790, 400);
            bd_3.Size = new Size(120, 60);
            bd_3.BackColor = Color.Purple;
            bd_3.ForeColor = Color.White;
            bd_3.Click += Bd_3_Click;

            this.Controls.Add(bd_1);
            this.Controls.Add(bd_2);
            this.Controls.Add(bd_3);
        }

        private void reset()
        {
            Button reset = new Button();
            reset.Text = "Restart game";
            reset.Location = new Point(610, 380);
            reset.Font = new Font(Font.FontFamily, 20);
            reset.Size = new Size(190, 60);
            reset.BackColor = Color.Purple;
            reset.ForeColor = Color.White;
            reset.Click += Reset_Click;

            this.Controls.Add(reset);
        }

        private void Reset_Click(object sender, EventArgs e)
        {
            Form1 From = new Form1();
            From.Show();
            this.Hide();

            using (var soundPlayer = new SoundPlayer(@"../../sounds/The Musty Scent Of Fresh Pate.wav"))
            {
                sound = 0;
                soundPlayer.Stop();
            }
        }

        private void winner()
        {
            if (counter == 3)
            {
                readWin = File.ReadAllText(@"../../player.txt");
                lbl_win = new Label();
                game.ImageLocation = (@"../../images/win.gif");
                game.SizeMode = PictureBoxSizeMode.StretchImage;
                lbl_win.Text = readWin;

                MessageBox.Show($"Congratulations! Win player {lbl_win.Text} \r {lbl_bot.Text} {counter_2}:{counter} {lbl_pl.Text}", "Message", MessageBoxButtons.OK);
                bd_1.Hide();
                bd_2.Hide();
                bd_3.Hide();
                reset();

            }
            else if (counter_2 == 3)
            {
                lbl_win = new Label();
                game.ImageLocation = (@"../../images/lose.gif");
                game.SizeMode = PictureBoxSizeMode.StretchImage;
                lbl_win.Text = lbl_bot.Text;

                MessageBox.Show($"Sorry, You lost! Win {lbl_win.Text} \r{lbl_bot.Text} {counter_2}:{counter} {lbl_pl.Text}", "Message", MessageBoxButtons.OK);
                bd_1.Hide();
                bd_2.Hide();
                bd_3.Hide();
                reset();
            }           
        }

        private void Bd_1_Click(object sender, EventArgs e)
        {
            rep = list_img[0];

            Random rnd_img = new Random();
            int randig_2 = rnd_img.Next(list_img.Count);

            rep_2 = list_img[randig_2];
            pic_pl.ImageLocation = ($"../../images/{rep}");
            pic_bot.ImageLocation = ($"../../images/{rep_2}");

            game.ImageLocation = (@"../../images/thegame.gif");

            if ((rep == list_img[0] && rep_2 == list_img[1]) || (rep == list_img[1] && rep_2 == list_img[2]) || (rep == list_img[2] && rep_2 == list_img[0]))
            {
                counter++;
                score.Text = counter.ToString();
            }
            else if ((rep == list_img[0] && rep_2 == list_img[2]) || (rep == list_img[1] && rep_2 == list_img[0]) || (rep == list_img[2] && rep_2 == list_img[1]))
            {
                counter_2++;
                score_2.Text = counter_2.ToString();
            }

            winner();
        }
        
        private void Bd_2_Click(object sender, EventArgs e)
        {
            rep = list_img[1];

            Random rnd_img = new Random();
            int randig_2 = rnd_img.Next(list_img.Count);

            rep_2 = list_img[randig_2];
            pic_pl.ImageLocation = ($"../../images/{rep}");
            pic_bot.ImageLocation = ($"../../images/{rep_2}");

            game.ImageLocation = (@"../../images/thegame.gif");

            if ((rep == list_img[0] && rep_2 == list_img[1]) || (rep == list_img[1] && rep_2 == list_img[2]) || (rep == list_img[2] && rep_2 == list_img[0]))
            {
                counter++;
                score.Text = counter.ToString();
            }
            else if ((rep == list_img[0] && rep_2 == list_img[2]) || (rep == list_img[1] && rep_2 == list_img[0]) || (rep == list_img[2] && rep_2 == list_img[1]))
            {
                counter_2++;
                score_2.Text = counter_2.ToString();
            }

            winner();
        }
        
        private void Bd_3_Click(object sender, EventArgs e)
        {
            rep = list_img[2];

            Random rnd_img = new Random();
            int randig_2 = rnd_img.Next(list_img.Count);

            rep_2 = list_img[randig_2];
            pic_pl.ImageLocation = ($"../../images/{rep}");
            pic_bot.ImageLocation = ($"../../images/{rep_2}");

            game.ImageLocation = (@"../../images/thegame.gif");

            if ((rep == list_img[0] && rep_2 == list_img[1]) || (rep == list_img[1] && rep_2 == list_img[2]) || (rep == list_img[2] && rep_2 == list_img[0]))
            {
                counter++;
                score.Text = counter.ToString();
            }
            else if ((rep == list_img[0] && rep_2 == list_img[2]) || (rep == list_img[1] && rep_2 == list_img[0]) || (rep == list_img[2] && rep_2 == list_img[1]))
            {
                counter_2++;
                score_2.Text = counter_2.ToString();
            }

            winner();
        }

        private void Lbl_pl_TextChanged(object sender, EventArgs e)
        {
            Size size_pl = TextRenderer.MeasureText(lbl_pl.Text, lbl_pl.Font);
            lbl_pl.Size = new Size(size_pl.Width, size_pl.Height);
        }

        private void Btn_Click(object sender, EventArgs e)
        {   
            this.Controls.Clear();

            // Button Sound-------------------------------------------

            buttonSound = new Button();
            buttonSound.Size = new Size(52, 52);
            buttonSound.Location = new Point(1370, 10);
            buttonSound.BackgroundImage = new Bitmap(@"../../images/sound_on.png");
            buttonSound.Click += ButtonSound_Click;

            this.Controls.Add(buttonSound);

            List<string> list = new List<string>();  // List of random NickName
            list.Add("Cralonn");
            list.Add("Kon");
            list.Add("Manniron");
            list.Add("Pekeyol");
            list.Add("Iell");

            Random rnd = new Random();
            int numb = rnd.Next(list.Count);

            using (StreamWriter writer = new StreamWriter(@"../../player.txt"))  // Write file using StreamWriter
            {
                if (String.IsNullOrEmpty(txtB.Text))
                {
                    writer.WriteLine(list[numb]);
                }
                else
                {
                    writer.WriteLine(txtB.Text);
                }                          
            }
           
            this.Size = new Size(1440, 900);
            this.BackgroundImage = new Bitmap(@"../../Backgrounds/background2.jpg");
          
            string readText = File.ReadAllText(@"../../player.txt");  // Read file 
            lbl_pl.Text = readText;

            pic_pl = new PictureBox();
            pic_pl.Location = new Point(270, 170);
            pic_pl.Size = new Size(158, 154);
            pic_pl.Image = Image.FromFile(@"../../images/orig.png");
            pic_pl.SizeMode = PictureBoxSizeMode.StretchImage;

            pic_bot = new PictureBox();
            pic_bot.Location = new Point(985, 170);
            pic_bot.Size = new Size(158, 154);
            pic_bot.Image = Image.FromFile(@"../../images/orig.png");
            pic_bot.SizeMode = PictureBoxSizeMode.StretchImage;

            game = new PictureBox();
            game.Location = new Point(400, 470);
            game.Size = new Size(600, 345);
            game.BackColor = Color.Purple;

            counter = 0;
            score = new Label();
            score.Location = new Point(600, 220);
            score.Text = counter.ToString();
            score.Font = new Font(Font.FontFamily, 40);
            score.Size = new Size(50, 60);
            score.BackColor = Color.FromArgb(51, 0, 51);
            score.ForeColor = Color.White;

            counter_2 = 0;
            score_2 = new Label();
            score_2.Location = new Point(760, 220);
            score_2.Text = counter_2.ToString();
            score_2.Font = new Font(Font.FontFamily, 40);
            score_2.Size = new Size(50, 60);
            score_2.BackColor = Color.FromArgb(51, 0, 51);
            score_2.ForeColor = Color.White;

            Label score_name = new Label();
            score_name.Text = "Score:";
            score_name.Font = new Font(Font.FontFamily, 20);
            score_name.BackColor = Color.FromArgb(51, 0, 51);
            score_name.ForeColor = Color.White;
            score_name.Location = new Point(660, 170);
            score_name.Size = new Size(95, 35);

            btn_game = new Button();
            btn_game.Location = new Point(600, 400);
            btn_game.Size = new Size(200, 50);
            btn_game.Text = "Start";
            btn_game.BackColor = Color.FromArgb(76, 0, 153);
            btn_game.Font = new Font(Font.FontFamily, 20);
            btn_game.ForeColor = Color.White;
            btn_game.Click += Btn_game_Click;

            this.Controls.Add(lbl_pl);
            this.Controls.Add(lbl_bot);
            this.Controls.Add(pic_pl);
            this.Controls.Add(pic_bot);
            this.Controls.Add(btn_game);
            this.Controls.Add(game);
            this.Controls.Add(score);
            this.Controls.Add(score_2);
            this.Controls.Add(score_name);
                        
        }

        private void ButtonSound_Click(object sender, EventArgs e)
        {
            using (var soundPlayer = new SoundPlayer(@"../../sounds/The Musty Scent Of Fresh Pate.wav"))
            {
                if (sound == 0)
                {
                    sound++;
                    soundPlayer.Play();
                    buttonSound.BackgroundImage = new Bitmap(@"../../images/sound_off.png");
                }
                else
                {
                    sound = 0;
                    soundPlayer.Stop();
                    buttonSound.BackgroundImage = new Bitmap(@"../../images/sound_on.png");
                }                
            }
        }

        private void Btn_game_Click(object sender, EventArgs e)
        {
            ButtonEl();
            btn_game.Hide();
            game.ImageLocation = (@"../../images/thegame.gif");
        }
    }
}