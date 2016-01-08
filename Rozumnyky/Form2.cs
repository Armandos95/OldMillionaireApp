using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.DirectX.AudioVideoPlayback;

namespace Rozumnyky
{
    public partial class Form2 : Form
    {
        private string[] fileRows;
        private int questionAmount;
        private int questionNumber;
        private static Audio fffShow = new Audio(System.Windows.Forms.Application.StartupPath + "//sounds//fff_show.mp3");
        private static Audio fffStart = new Audio(System.Windows.Forms.Application.StartupPath + "//sounds//fff_start.mp3");
        private static Audio fff = new Audio(System.Windows.Forms.Application.StartupPath + "//sounds//fff.mp3");
        private static Audio fffStop = new Audio(System.Windows.Forms.Application.StartupPath + "//sounds//fff_stop.mp3");
        private static Audio fffAnswer1 = new Audio(System.Windows.Forms.Application.StartupPath + "//sounds//fff_answer_1.mp3");
        private static Audio fffAnswer2 = new Audio(System.Windows.Forms.Application.StartupPath + "//sounds//fff_answer_2.mp3");
        private static Audio fffAnswer3 = new Audio(System.Windows.Forms.Application.StartupPath + "//sounds//fff_answer_3.mp3");
        private static Audio fffAnswer4 = new Audio(System.Windows.Forms.Application.StartupPath + "//sounds//fff_answer_4.mp3");
        private int fffTime = 20;
        private int pictureBoxPos = 1290;
        private System.Windows.Forms.PictureBox[] pictureArray = new System.Windows.Forms.PictureBox[4];
        private int currAnswerMoving = 0;
        private int picAnswer1Pos = 1290;
        private int picAnswer2Pos = 1290;
        private int picAnswer3Pos = 1290;
        private int picAnswer4Pos = 1290;

        public Form2(int qAmount)
        {
            InitializeComponent();
            questionAmount = qAmount;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            fileRows = File.ReadAllLines(Application.StartupPath + "//FFF.csv");
            labelQuestion.Parent = pictureQuestionBar;
            labelQuestion.Location = new Point(215, 19);
            labelAnswerA.Parent = pictureQuestionBar;
            labelAnswerA.Location = new Point(260, 164);
            labelAnswerB.Parent = pictureQuestionBar;
            labelAnswerB.Location = new Point(876, 164);
            labelAnswerC.Parent = pictureQuestionBar;
            labelAnswerC.Location = new Point(260, 256);
            labelAnswerD.Parent = pictureQuestionBar;
            labelAnswerD.Location = new Point(876, 256);
            labelCorrectQuestion.Parent = pictureBox2;
            labelCorrectQuestion.Location = new Point(5, 50);
            labelCorrectQuestion.BringToFront();
            labelCorrectA.Parent = pictureCorrectA;
            labelCorrectA.Location = new Point(105, 16);
            labelCorrectB.Parent = pictureCorrectB;
            labelCorrectB.Location = new Point(105, 16);
            labelCorrectC.Parent = pictureCorrectC;
            labelCorrectC.Location = new Point(105, 16);
            labelCorrectD.Parent = pictureCorrectD;
            labelCorrectD.Location = new Point(105, 16);
        }

        private void buttonChoose_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            questionNumber = rand.Next(questionAmount) + 1;
            labelQNumber.Text = "Номер запитання: " + questionNumber.ToString();
            buttonShowQuestion.Enabled = true;
        }

        private void buttonShowQuestion_Click(object sender, EventArgs e)
        {
            buttonChoose.Enabled = false;
            buttonShowQuestion.Enabled = false;
            buttonStart.Enabled = true;
            string[] textArray = null;
            textArray = fileRows[questionNumber-1].Split(';');
            for (int i = 0; i < 4; i++)
            {
                if (textArray[i + 5] == "A") pictureArray[i] = pictureCorrectA;
                if (textArray[i + 5] == "B") pictureArray[i] = pictureCorrectB;
                if (textArray[i + 5] == "C") pictureArray[i] = pictureCorrectC;
                if (textArray[i + 5] == "D") pictureArray[i] = pictureCorrectD;
            }
            pictureArray[0].Location = new Point(1290, 300);
            pictureArray[1].Location = new Point(1290, 390);
            pictureArray[2].Location = new Point(1290, 480);
            pictureArray[3].Location = new Point(1290, 570);
            labelQuestion.Text = textArray[0];
            labelCorrectQuestion.Text = textArray[0];
            labelAnswerA.Text = textArray[1];
            labelCorrectA.Text = textArray[1];
            labelAnswerB.Text = textArray[2];
            labelCorrectB.Text = textArray[2];
            labelAnswerC.Text = textArray[3];
            labelCorrectC.Text = textArray[3];
            labelAnswerD.Text = textArray[4];
            labelCorrectD.Text = textArray[4];
            pictureQuestionBar.Visible = true;
            labelQuestion.Visible = true;
            fffShow.Play();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            pictureQuestionBar.Image = Properties.Resources.bars;
            buttonStart.Enabled = false;
            buttonStop.Enabled = true; 
            fffShow.Stop();
            fffStart.Play();
            Thread.Sleep(2800);
            fff.Play();
            labelAnswerA.Visible = true;
            labelAnswerB.Visible = true;
            labelAnswerC.Visible = true;
            labelAnswerD.Visible = true;
            labelTime.Visible = true;
            labelTimer.Visible = true;
            timer1.Start();
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            fffTime = 0;
            labelTimer.Text = fffTime.ToString();
            fff.Stop();
            fffStop.Play();
            buttonStop.Enabled = false;
            buttonCorrectOrder.Enabled = true;
        }

        private void buttonCorrectOrder_Click(object sender, EventArgs e)
        {
            pictureQuestionBar.Visible = false;
            buttonCorrectOrder.Enabled = false;
            timer2.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            fffTime--;
            labelTimer.Text = fffTime.ToString();
            if (fffTime == 0)
            {
                timer1.Stop();
                buttonStop.Enabled = false;
                buttonCorrectOrder.Enabled = true;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            pictureBox2.Left = pictureBox2.Left - 10;
            pictureBoxPos -= 10;
            if (pictureBoxPos<=880)
            {
                timer2.Stop();
                fffAnswer1.Play();
                currAnswerMoving = 1;
                timer3.Start();
                pictureCorrectA.Visible = true;
                pictureCorrectB.Visible = true;
                pictureCorrectC.Visible = true;
                pictureCorrectD.Visible = true;
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            if (currAnswerMoving == 1)
            {
                pictureArray[0].Left = pictureArray[0].Left - 20;
                picAnswer1Pos -= 20;
                if (picAnswer1Pos <= 840)
                {
                    currAnswerMoving = 2;
                    Thread.Sleep(1000);
                    fffAnswer2.Play();
                }
            }
            if (currAnswerMoving == 2)
            {
                pictureArray[1].Left = pictureArray[1].Left - 20;
                picAnswer2Pos -= 20;
                if (picAnswer2Pos <= 840)
                {
                    currAnswerMoving = 3;
                    Thread.Sleep(1000);
                    fffAnswer3.Play();
                }
            }
            if (currAnswerMoving == 3)
            {
                pictureArray[2].Left = pictureArray[2].Left - 20;
                picAnswer3Pos -= 20;
                if (picAnswer3Pos <= 840)
                {
                    currAnswerMoving = 4;
                    Thread.Sleep(1000);
                    fffAnswer4.Play();
                }
            }
            if (currAnswerMoving == 4)
            {
                pictureArray[3].Left = pictureArray[3].Left - 20;
                picAnswer4Pos -= 20;
                if (picAnswer4Pos <= 840)
                {
                    timer3.Stop();
                }
            }
        }

        private void resetForm()
        {
            buttonChoose.Enabled = true;
            buttonShowQuestion.Enabled = false;
            buttonStart.Enabled = false;
            buttonCorrectOrder.Enabled = false;
            buttonStop.Enabled = false;
            labelQNumber.Text = "Номер запитання: ";
            labelTimer.Text = "20";
            labelTime.Visible = false;
            labelTimer.Visible = false;
            fffTime = 20;
            pictureQuestionBar.Visible = false;
            labelQuestion.Visible = false;
            labelAnswerA.Visible = false;
            labelAnswerB.Visible = false;
            labelAnswerC.Visible = false;
            labelAnswerD.Visible = false;
            timer1.Stop();
            timer2.Stop();
            timer3.Stop();
            fffShow.Stop();
            fffStart.Stop();
            fff.Stop();
            fffStop.Stop();
            fffAnswer1.Stop();
            fffAnswer2.Stop();
            fffAnswer3.Stop();
            fffAnswer4.Stop();
            pictureBox2.Location = new Point(1290, 0);
            pictureCorrectA.Location = new Point(1290, 300);
            pictureCorrectB.Location = new Point(1290, 390);
            pictureCorrectC.Location = new Point(1290, 480);
            pictureCorrectD.Location = new Point(1290, 570);
            pictureBoxPos = 1290;
            currAnswerMoving = 0;
            picAnswer1Pos = 1290;
            picAnswer2Pos = 1290;
            picAnswer3Pos = 1290;
            picAnswer4Pos = 1290;
            pictureQuestionBar.Image = Properties.Resources.bars_empty;
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            resetForm();
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            resetForm();
        }
    }
}
