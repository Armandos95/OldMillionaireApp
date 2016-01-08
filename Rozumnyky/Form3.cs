using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.DirectX.AudioVideoPlayback;

namespace Rozumnyky
{
    public partial class Form3 : Form
    {
        private Question currentQuestion;
        private int qIndex;
        private int guaranteedSum = 0;
        private int takenSum = 0;
        private static Audio soundFF = new Audio(System.Windows.Forms.Application.StartupPath + "//sounds//fifty_fifty.mp3");
        private static Audio musicAudience = new Audio(System.Windows.Forms.Application.StartupPath + "//sounds//audience_voting.mp3");
        private static Audio soundAudienceEnd = new Audio(System.Windows.Forms.Application.StartupPath + "//sounds//audience_ending.mp3");
        private static Audio musicExpert = new Audio(System.Windows.Forms.Application.StartupPath + "//sounds//expert.mp3");
        private static Audio soundExpertEnd = new Audio(System.Windows.Forms.Application.StartupPath + "//sounds//expert_ending.mp3");
        private bool ifAudience = false;
        private bool ifExpert = false;
        private Dictionary<string, string> genCategoryArray = new Dictionary<string, string>();
        private string[] currCategoryArray = {"bio", "cinem", "econ", "game", "geog", "hist", "info", "ling", "liter", "math", "mus", 
                                             "other", "phil", "phyz", "psych", "sport", "tech", "tv"};
        private int categorySize;
        private List<string> playedQuestions = new List<string>();
        private string[] currentQuestions = new string[15];
        private bool switchRandomUsed = false;
        private int[] questionNumbers = new int[15];

        public static Audio musicDD1 = new Audio(System.Windows.Forms.Application.StartupPath + "//sounds//doubledip1.mp3");
        public static Audio musicDD2 = new Audio(System.Windows.Forms.Application.StartupPath + "//sounds//doubledip2.mp3");
        public static bool ifDoubleDip1 = false;
        public static bool ifDoubleDip2 = false;
        public static Audio tempMusic;

        public Form3(int cgs)
        {
            InitializeComponent();
            categorySize = cgs;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            genCategoryArray["bio"] = "Хімія та біологія";
            genCategoryArray["cinem"] = "Кінематограф";
            genCategoryArray["econ"] = "Економіка та статистика";
            genCategoryArray["game"] = "Комп'ютерні ігри";
            genCategoryArray["geog"] = "Географія";
            genCategoryArray["hist"] = "Історія";
            genCategoryArray["info"] = "Інформаційні технології";
            genCategoryArray["ling"] = "Лінгвістика";
            genCategoryArray["liter"] = "Література";
            genCategoryArray["math"] = "Математика";
            genCategoryArray["mus"] = "Музика";
            genCategoryArray["other"] = "Інші запитання";
            genCategoryArray["phil"] = "Релігія і філософія";
            genCategoryArray["phyz"] = "Фізика";
            genCategoryArray["psych"] = "Психологія і соціологія";
            genCategoryArray["sport"] = "Спорт";
            genCategoryArray["tech"] = "Техніка";
            genCategoryArray["tv"] = "Телебачення";

            labelQuestion.Parent = pictureQuestionBar;
            labelQuestion.Location = new Point(215, 19);
            pictureActiveLF.Parent = pictureQuestionBar;
            pictureActiveLF.Location = new Point(679, 180);
            using (var gp = new GraphicsPath())
            {
                gp.AddEllipse(new Rectangle(0, 0, pictureActiveLF.Width - 1, pictureActiveLF.Height - 1));
                pictureActiveLF.Region = new Region(gp);
            }
            pictureAnswerA.Parent = pictureQuestionBar;
            pictureAnswerA.Location = new Point(155, 148);
            labelAnswerA.Parent = pictureAnswerA;
            labelAnswerA.Location = new Point(105, 16);
            pictureAnswerB.Parent = pictureQuestionBar;
            pictureAnswerB.Location = new Point(771, 148);
            labelAnswerB.Parent = pictureAnswerB;
            labelAnswerB.Location = new Point(105, 16);
            pictureAnswerC.Parent = pictureQuestionBar;
            pictureAnswerC.Location = new Point(155, 240);
            labelAnswerC.Parent = pictureAnswerC;
            labelAnswerC.Location = new Point(105, 16);
            pictureAnswerD.Parent = pictureQuestionBar;
            pictureAnswerD.Location = new Point(771, 240);
            labelAnswerD.Parent = pictureAnswerD;
            labelAnswerD.Location = new Point(105, 16);
        }

        private void lockAnswerButtons()
        {
            buttonAnswerA.Enabled = false;
            buttonAnswerB.Enabled = false;
            buttonAnswerC.Enabled = false;
            buttonAnswerD.Enabled = false;
            buttonTakeMoney.Enabled = false;
            buttonPhotoShow.Enabled = false;
            buttonAudioPlay.Enabled = false;
            buttonAudioStop.Enabled = false;
            buttonPhotoShow.Enabled = false;
            buttonPhotoShow.Visible = false;
            buttonAudioPlay.Enabled = false;
            buttonAudioPlay.Visible = false;
            buttonAudioStop.Enabled = false;
            buttonAudioStop.Visible = false;
            buttonCheckAnswer.Enabled = true;
        }

        private void unlockAnswerButtons()
        {
            buttonAnswerA.Enabled = true;
            buttonAnswerB.Enabled = true;
            buttonAnswerC.Enabled = true;
            buttonAnswerD.Enabled = true;
            buttonTakeMoney.Enabled = true;
            buttonCheckAnswer.Enabled = false;
        }

        private void unlockAnswerButtonsDD()
        {
            buttonAnswerA.Enabled = true;
            buttonAnswerB.Enabled = true;
            buttonAnswerC.Enabled = true;
            buttonAnswerD.Enabled = true;
            buttonCheckAnswer.Enabled = false;
        }

        private void lockAnswerAndCheckButtons()
        {
            buttonAnswerA.Enabled = true;
            buttonAnswerB.Enabled = true;
            buttonAnswerC.Enabled = true;
            buttonAnswerD.Enabled = true;
            buttonPhotoShow.Enabled = false;
            buttonPhotoShow.Visible = false;
            buttonAudioPlay.Enabled = false;
            buttonAudioPlay.Visible = false;
            buttonAudioStop.Enabled = false;
            buttonAudioStop.Visible = false;
            buttonTakeMoney.Enabled = true;
            buttonCheckAnswer.Enabled = true;
        }

        private void buttonAnswer_Click(object sender, EventArgs e)
        {
            lockAnswerButtons();
            currentQuestion.lockAnswer(Convert.ToChar(((Button)sender).Name.Replace("buttonAnswer", "")));
        }

        public static System.Drawing.Bitmap getPictureRes(string str)
        {
            return (System.Drawing.Bitmap)Properties.Resources.ResourceManager.GetObject(str, Properties.Resources.Culture);
        }

        private void buttonCheckAnswer_Click(object sender, EventArgs e)
        {
            if (ifDoubleDip1) currentQuestion.checkAnswerDD();
            else currentQuestion.checkAnswer();
            buttonCheckAnswer.Enabled = false;
            string picName;
            if (currentQuestion.ifAnsweredCorrectly == true)
            {
                picName = "win_" + qIndex.ToString();
                picturePrize.Image = getPictureRes(picName);
                pictureQuestionBar.Visible = false;
                picturePrize.Visible = true;
                takenSum = qIndex;
                if (qIndex == 5 || qIndex == 10) guaranteedSum = qIndex;
                if (qIndex < 15) buttonNextQuestion.Enabled = true;
                else labelPrize.Visible = true;
                pictureActiveLF.Visible = false;
                if (ifDoubleDip1 || ifDoubleDip2)
                {
                    pictureLF1.Image = getPictureRes("x2_used");
                    if (qIndex < 5) tempMusic.Play();
                }
                ifDoubleDip1 = false;
                ifDoubleDip2 = false;
            }
            else if (ifDoubleDip1)
            {
                ifDoubleDip1 = false;
                ifDoubleDip2 = true;
                unlockAnswerButtonsDD();
            }
            else
            {
                picName = "win_" + guaranteedSum.ToString();
                picturePrize.Image = getPictureRes(picName);
                pictureQuestionBar.Visible = false;
                picturePrize.Visible = true;
                labelPrize.Visible = true;
                if (ifDoubleDip1 || ifDoubleDip2)
                    pictureLF1.Image = getPictureRes("x2_used");
                ifDoubleDip2 = false;
            }

        }

        private void startQuestion(string question, int qNumber)
        {
            unlockAnswerButtons();
            string picName = "tree_" + qIndex.ToString();
            pictureMoneyTree.Image = getPictureRes(picName);
            pictureMoneyTree.Refresh();
            string tempCat;
            if (!switchRandomUsed) tempCat = currCategoryArray[qIndex - 1];
            else
            {
                tempCat = currCategoryArray[15];
                switchRandomUsed = false;
            }
            currentQuestion = new Question(qIndex, question, pictureQuestionBar, labelQuestion, pictureAnswerA, pictureAnswerB,
                pictureAnswerC, pictureAnswerD, labelAnswerA, labelAnswerB, labelAnswerC, labelAnswerD, 
                qIndex.ToString() + "\\" + tempCat + "\\" + qNumber.ToString(), this);
            labelLevel.Text = "Рівень: " + qIndex.ToString();
            labelLevel.Visible = true;
            labelCategory.Text = "Категорія: " + genCategoryArray[tempCat];
            labelCategory.Visible = true;
            labelNumber.Text = "Номер запитання: " + qNumber.ToString();
            labelNumber.Visible = true;
            currentQuestion.showQuestion();
        }

        private void hideQuestion()
        {
            pictureQuestionBar.Visible = false;
            picturePrize.Visible = false;
            labelPrize.Visible = false;
            pictureAnswerA.Image = Properties.Resources.A_normal;
            pictureAnswerB.Image = Properties.Resources.B_normal;
            pictureAnswerC.Image = Properties.Resources.C_normal;
            pictureAnswerD.Image = Properties.Resources.D_normal;
            labelAnswerA.ForeColor = Color.White;
            labelAnswerB.ForeColor = Color.White;
            labelAnswerC.ForeColor = Color.White;
            labelAnswerD.ForeColor = Color.White;
            labelLevel.Visible = false;
            labelCategory.Visible = false;
            labelNumber.Visible = false;
            lockAnswerAndCheckButtons();
            currentQuestion.stopSomeMusic();
        }

        private void radioFiftyFifty_CheckedChanged(object sender, EventArgs e)
        {
            buttonFiftyFifty.Enabled = true;
            buttonDoubleDip.Enabled = false;
            pictureLF1.Image = Properties.Resources.fiftyfifty_normal;
            pictureLF1.Refresh();
        }

        private void radioDoubleDip_CheckedChanged(object sender, EventArgs e)
        {
            buttonFiftyFifty.Enabled = false;
            buttonDoubleDip.Enabled = true;
            pictureLF1.Image = Properties.Resources.x2_normal;
            pictureLF1.Refresh();
        }

        private void radioAudience_CheckedChanged(object sender, EventArgs e)
        {
            buttonAudience.Enabled = true;
            buttonExpert.Enabled = false;
            pictureLF2.Image = Properties.Resources.audience_normal;
            pictureLF2.Refresh();
        }

        private void radioExpert_CheckedChanged(object sender, EventArgs e)
        {
            buttonAudience.Enabled = false;
            buttonExpert.Enabled = true;
            pictureLF2.Image = Properties.Resources.expert_normal;
            pictureLF2.Refresh();
        }

        private void radioSwitchSame_CheckedChanged(object sender, EventArgs e)
        {
            buttonSwitchSame.Enabled = true;
            buttonSwitchRandom.Enabled = false;
            pictureLF3.Image = Properties.Resources.switch_1_normal;
            pictureLF3.Refresh();
        }

        private void radioSwitchRandom_CheckedChanged(object sender, EventArgs e)
        {
            buttonSwitchSame.Enabled = false;
            buttonSwitchRandom.Enabled = true;
            pictureLF3.Image = Properties.Resources.switch_2_normal;
            pictureLF3.Refresh();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            buttonStart.Enabled = false;
            qIndex = 1;
            groupLF1.Enabled = false;
            groupLF2.Enabled = false;
            groupLF3.Enabled = false;

            Random rand = new Random(DateTime.Now.Millisecond);
            currCategoryArray = currCategoryArray.OrderBy(x => rand.Next(DateTime.Now.Millisecond)).ToArray();
            string tempString;
            int tempInt;
            string[] tempFileRows;
            for (int i = 0; i < 15; i++)
            {
                do
                {
                    tempInt = rand.Next(categorySize) + 1;
                    tempString = (i + 1).ToString() + currCategoryArray[i] + tempInt.ToString();
                }
                while (playedQuestions.Contains(tempString));
                playedQuestions.Add(tempString);
                tempFileRows = File.ReadAllLines(Application.StartupPath + "//questions//" + (i + 1).ToString() + "//" + currCategoryArray[i] +
                        "//questions.txt");
                currentQuestions[i] = tempFileRows[tempInt - 1];
                questionNumbers[i] = tempInt;
            }

            startQuestion(currentQuestions[qIndex - 1], questionNumbers[qIndex - 1]);
        }

        private void cleanLifelines()
        {
            buttonFiftyFifty.Enabled = false;
            buttonDoubleDip.Enabled = false;
            buttonAudience.Enabled = false;
            buttonExpert.Enabled = false;
            buttonSwitchSame.Enabled = false;
            buttonSwitchRandom.Enabled = false;
            pictureLF1.Image = Properties.Resources.blank_normal;
            pictureLF1.Refresh();
            pictureLF2.Image = Properties.Resources.blank_normal;
            pictureLF2.Refresh();
            pictureLF3.Image = Properties.Resources.blank_normal;
            pictureLF3.Refresh();
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            cleanLifelines();
            hideQuestion();
            currentQuestion.stopAllMusic();
            pictureMoneyTree.Image = Properties.Resources.tree_blank;
            pictureMoneyTree.Refresh();
            pictureActiveLF.Visible = false;
            buttonStart.Enabled = true;
            buttonNextQuestion.Enabled = false;
            groupLF1.Enabled = true;
            groupLF2.Enabled = true;
            groupLF3.Enabled = true;
            guaranteedSum = 0;
            takenSum = 0;
        }

        private void buttonNextQuestion_Click(object sender, EventArgs e)
        {
            if (qIndex < 15)
            {
                hideQuestion();
                qIndex++;
                startQuestion(currentQuestions[qIndex - 1], questionNumbers[qIndex - 1]);
            }
            buttonNextQuestion.Enabled = false;
        }

        private void buttonTakeMoney_Click(object sender, EventArgs e)
        {
            hideQuestion();
            currentQuestion.stopAllMusic();
            Audio takeTheMoney;
            if (takenSum < 10)
                takeTheMoney = new Audio(System.Windows.Forms.Application.StartupPath + "//sounds//take_before10.mp3");
            else
                takeTheMoney = new Audio(System.Windows.Forms.Application.StartupPath + "//sounds//take_after10.mp3");
            takeTheMoney.Play();
            picturePrize.Visible = true;
            labelPrize.Visible = true;
        }

        private void buttonFiftyFifty_Click(object sender, EventArgs e)
        {
            currentQuestion.useFiftyFifty();
            string twoLetters = currentQuestion.markedAnswers;
            if (twoLetters.Contains('A')) buttonAnswerA.Enabled = false;
            if (twoLetters.Contains('B')) buttonAnswerB.Enabled = false;
            if (twoLetters.Contains('C')) buttonAnswerC.Enabled = false;
            if (twoLetters.Contains('D')) buttonAnswerD.Enabled = false;
            soundFF.Play();
            buttonFiftyFifty.Enabled = false;
            pictureLF1.Image = Properties.Resources.fiftyfifty_used;
            pictureLF1.Refresh();
        }

        private void buttonDoubleDip_Click(object sender, EventArgs e)
        {
            buttonDoubleDip.Enabled = false;
            buttonTakeMoney.Enabled = false;
            buttonPhotoShow.Enabled = false;
            buttonPhotoShow.Visible = false;
            buttonAudioPlay.Enabled = false;
            buttonAudioPlay.Visible = false;
            buttonAudioStop.Enabled = false;
            buttonAudioStop.Visible = false;
            ifDoubleDip1 = true;
            currentQuestion.stopAllMusic();
            musicDD1.Play();
            pictureActiveLF.Visible = true;
            pictureActiveLF.Image = getPictureRes("x2_normal");
            pictureActiveLF.BringToFront();
            pictureLF1.Image = getPictureRes("x2_active");
        }

        private void useLF2(ref bool ifLF, Audio musicLF, Audio soundLF, string st1, string st2, string st3,
            System.Windows.Forms.Button buttonLF)
        {
            if (!ifLF)
            {
                ifLF = true;
                currentQuestion.stopAllMusic();
                musicLF.Play();
                pictureActiveLF.Visible = true;
                pictureActiveLF.Image = getPictureRes(st1);
                pictureActiveLF.BringToFront();
                pictureLF2.Image = getPictureRes(st2);
            }
            else
            {
                musicLF.Stop();
                soundLF.Play();
                pictureActiveLF.Visible = false;
                pictureLF2.Image = getPictureRes(st3);
                Thread.Sleep(1500);
                currentQuestion.qMusic.Play();
                buttonLF.Enabled = false;
            }
        }

        private void buttonAudience_Click(object sender, EventArgs e)
        {
            useLF2(ref ifAudience, musicAudience, soundAudienceEnd, "audience_normal", "audience_active", "audience_used", buttonAudience);
        }

        private void buttonExpert_Click(object sender, EventArgs e)
        {
            useLF2(ref ifExpert, musicExpert, soundExpertEnd, "expert_normal", "expert_active", "expert_used", buttonExpert);
        }

        private void useLF3(int catIndex)
        {
            hideQuestion();
            currentQuestion.stopAllMusic();
            Random rand = new Random();
            string tempString;
            int tempInt;
            string[] tempFileRows;
            do
            {
                tempInt = rand.Next(categorySize) + 1;
                tempString = qIndex.ToString() + currCategoryArray[catIndex] + tempInt.ToString();
            }
            while (playedQuestions.Contains(tempString));
            playedQuestions.Add(tempString);
            tempFileRows = File.ReadAllLines(Application.StartupPath + "\\questions\\" + qIndex.ToString() + "\\" + currCategoryArray[catIndex] +
                        "\\questions.txt");
            startQuestion(tempFileRows[tempInt - 1], tempInt);
            if (qIndex > 1 && qIndex < 6) tempMusic.Play();
        }

        private void buttonSwitchSame_Click(object sender, EventArgs e)
        {
            buttonSwitchSame.Enabled = false;
            pictureLF3.Image = getPictureRes("switch_1_used");
            pictureLF3.Refresh();
            useLF3(qIndex - 1);
        }

        private void buttonSwitchRandom_Click(object sender, EventArgs e)
        {
            buttonSwitchRandom.Enabled = false;
            pictureLF3.Image = getPictureRes("switch_2_used");
            pictureLF3.Refresh();
            switchRandomUsed = true;
            useLF3(15);
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (currentQuestion != null) currentQuestion.stopAllMusic();
        }

        public void activatePhotoButton()
        {
            buttonPhotoShow.Enabled = true;
            buttonPhotoShow.Visible = true;
        }

        public void activateAudioButtons()
        {
            buttonAudioPlay.Enabled = true;
            buttonAudioPlay.Visible = true;
            buttonAudioStop.Visible = true;
        }

        private void buttonPhotoShow_Click(object sender, EventArgs e)
        {
            Form4 frm4 = new Form4(currentQuestion.mediaLocation);
            frm4.ShowDialog();
        }

        private void buttonAudioPlay_Click(object sender, EventArgs e)
        {
            currentQuestion.qMusic.Stop();
            currentQuestion.qFileAudio = new Audio(Application.StartupPath + "\\audio\\" + currentQuestion.mediaLocation + ".mp3");
            currentQuestion.qFileAudio.Play();
            buttonAudioPlay.Enabled = false;
            buttonAudioStop.Enabled = true;
        }

        private void buttonAudioStop_Click(object sender, EventArgs e)
        {
            currentQuestion.qFileAudio.Stop();
            currentQuestion.qMusic.Play();
            buttonAudioPlay.Enabled = true;
            buttonAudioStop.Enabled = false;
        }
    }
}
