using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.DirectX.AudioVideoPlayback;

namespace Rozumnyky
{
    class Question
    {
        private int qNumber;
        private System.Windows.Forms.PictureBox qPic;
        private System.Windows.Forms.Label qLab;
        private Answer qAnswerA;
        private Answer qAnswerB;
        private Answer qAnswerC;
        private Answer qAnswerD;
        private Audio qLD;
        private Audio qFinal;
        private Audio qCorrect;
        private Audio qWrong;

        public Audio qMusic;
        public Audio qFileAudio;
        public string mediaLocation;
        public bool ifAnsweredCorrectly = false;
        public string markedAnswers = "";
        private string qType;

        public Question(int qNumber, string qText, System.Windows.Forms.PictureBox qPic, System.Windows.Forms.Label qLab,
            System.Windows.Forms.PictureBox ansPicA, System.Windows.Forms.PictureBox ansPicB, System.Windows.Forms.PictureBox ansPicC,
            System.Windows.Forms.PictureBox ansPicD, System.Windows.Forms.Label ansLabA, System.Windows.Forms.Label ansLabB,
            System.Windows.Forms.Label ansLabC, System.Windows.Forms.Label ansLabD, string medLoc, Form3 frm3)
        {
            this.qNumber = qNumber;
            this.qPic = qPic;
            this.qLab = qLab;
            string[] textArray = null;
            textArray = qText.Split(';');
            this.qLab.Text = textArray[0];
            bool[] corrArray = { false, false, false, false };
            bool[] markedArray = { false, false, false, false };
            byte[] asciiBytes = Encoding.ASCII.GetBytes(textArray[5] + textArray[6] + textArray[7]);
            corrArray[asciiBytes[0] - 65] = true;
            markedArray[asciiBytes[1] - 65] = true;
            markedArray[asciiBytes[2] - 65] = true;
            qAnswerA = new Answer(textArray[1], 'A', ansPicA, ansLabA, corrArray[0], markedArray[0], this);
            qAnswerB = new Answer(textArray[2], 'B', ansPicB, ansLabB, corrArray[1], markedArray[1], this);
            qAnswerC = new Answer(textArray[3], 'C', ansPicC, ansLabC, corrArray[2], markedArray[2], this);
            qAnswerD = new Answer(textArray[4], 'D', ansPicD, ansLabD, corrArray[3], markedArray[3], this);
            
            qType = textArray[8];
            mediaLocation = medLoc;
            if (qType == "audio") frm3.activateAudioButtons();
            if (qType == "photo") frm3.activatePhotoButton();

            if (qNumber == 1 || qNumber > 5)
                qLD = new Audio(System.Windows.Forms.Application.StartupPath + "//sounds//LD_" + qNumber.ToString() + ".mp3");
            else
                qLD = null;
            if (qNumber == 1)
            {
                qMusic = new Audio(System.Windows.Forms.Application.StartupPath + "//sounds//question_1.mp3");
                Form3.tempMusic = qMusic;
            }
            else if (qNumber > 1 && qNumber <= 5)
                qMusic = Form3.tempMusic;
            else
                qMusic = new Audio(System.Windows.Forms.Application.StartupPath + "//sounds//question_" + qNumber.ToString() + ".mp3");
            qFinal = new Audio(System.Windows.Forms.Application.StartupPath + "//sounds//final_" + qNumber.ToString() + ".mp3");
            qCorrect = new Audio(System.Windows.Forms.Application.StartupPath + "//sounds//correct_" + qNumber.ToString() + ".mp3");
            qWrong = new Audio(System.Windows.Forms.Application.StartupPath + "//sounds//wrong_" + qNumber.ToString() + ".mp3");
        }

        public void showQuestion()
        {
            if (qLD != null)
            {
                qLD.Play();
                Thread.Sleep(5000);
                qLD.Stop();
            }
            qPic.Visible = true;
            if (qNumber == 1 || qNumber > 5) qMusic.Play();
            qMusic.Ending += new EventHandler(qMusicEnded);
        }

        private void qMusicEnded(object sender, EventArgs e)
        {
            qMusic.CurrentPosition = 0;
        }

        public void lockAnswer(char ch)
        {
            if (Form3.ifDoubleDip1)
            {
                stopAllMusic();
                if (ch == 'A') qAnswerA.toFinal(Form3.musicDD1, qFinal, qNumber);
                if (ch == 'B') qAnswerB.toFinal(Form3.musicDD1, qFinal, qNumber);
                if (ch == 'C') qAnswerC.toFinal(Form3.musicDD1, qFinal, qNumber);
                if (ch == 'D') qAnswerD.toFinal(Form3.musicDD1, qFinal, qNumber);
            }
            else if (Form3.ifDoubleDip2)
            {
                stopAllMusic();
                if (ch == 'A') qAnswerA.toFinal(Form3.musicDD2, qFinal, qNumber);
                if (ch == 'B') qAnswerB.toFinal(Form3.musicDD2, qFinal, qNumber);
                if (ch == 'C') qAnswerC.toFinal(Form3.musicDD2, qFinal, qNumber);
                if (ch == 'D') qAnswerD.toFinal(Form3.musicDD2, qFinal, qNumber);
            }
            else
            {
                if (ch == 'A') qAnswerA.toFinal(qMusic, qFinal, qNumber);
                if (ch == 'B') qAnswerB.toFinal(qMusic, qFinal, qNumber);
                if (ch == 'C') qAnswerC.toFinal(qMusic, qFinal, qNumber);
                if (ch == 'D') qAnswerD.toFinal(qMusic, qFinal, qNumber);
            }
        }

        public void checkAnswer()
        {
            if (qNumber == 5) qMusic.Stop();
            qAnswerA.toCorrect(qMusic, qFinal, qCorrect, qWrong, qNumber);
            qAnswerB.toCorrect(qMusic, qFinal, qCorrect, qWrong, qNumber);
            qAnswerC.toCorrect(qMusic, qFinal, qCorrect, qWrong, qNumber);
            qAnswerD.toCorrect(qMusic, qFinal, qCorrect, qWrong, qNumber);
        }

        public void checkAnswerDD()
        {
            if (qAnswerA.ansIfLocked)
            {
                if (qAnswerA.ansIfCorrect)
                    qAnswerA.toCorrect(qMusic, qFinal, qCorrect, qWrong, qNumber);
                else
                    qAnswerA.toBlankDD(qFinal, Form3.musicDD2);
            }
            if (qAnswerB.ansIfLocked)
            {
                if (qAnswerB.ansIfCorrect)
                    qAnswerB.toCorrect(qMusic, qFinal, qCorrect, qWrong, qNumber);
                else
                    qAnswerB.toBlankDD(qFinal, Form3.musicDD2);
            }
            if (qAnswerC.ansIfLocked)
            {
                if (qAnswerC.ansIfCorrect)
                    qAnswerC.toCorrect(qMusic, qFinal, qCorrect, qWrong, qNumber);
                else
                    qAnswerC.toBlankDD(qFinal, Form3.musicDD2);
            }
            if (qAnswerD.ansIfLocked)
            {
                if (qAnswerD.ansIfCorrect)
                    qAnswerD.toCorrect(qMusic, qFinal, qCorrect, qWrong, qNumber);
                else
                    qAnswerD.toBlankDD(qFinal, Form3.musicDD2);
            }
        }

        public void stopSomeMusic()
        {
            if (qFinal.Playing == true) qFinal.Stop();
            if (qCorrect.Playing == true) qCorrect.Stop();
            if (qWrong.Playing == true) qWrong.Stop();
            Form3.musicDD1.Stop();
            Form3.musicDD2.Stop();
        }

        public void stopAllMusic()
        {
            if (qMusic.Playing == true) qMusic.Stop();
            if (qFinal.Playing == true) qFinal.Stop();
            if (qCorrect.Playing == true) qCorrect.Stop();
            if (qWrong.Playing == true) qWrong.Stop();
            Form3.musicDD1.Stop();
            Form3.musicDD2.Stop();
        }

        public void useFiftyFifty()
        {
            if (qAnswerA.ansIfMarked == true)
            {
                markedAnswers += 'A';
                qAnswerA.toBlank();
            }
            if (qAnswerB.ansIfMarked == true)
            {
                markedAnswers += 'B';
                qAnswerB.toBlank();
            }
            if (qAnswerC.ansIfMarked == true)
            {
                markedAnswers += 'C';
                qAnswerC.toBlank();
            }
            if (qAnswerD.ansIfMarked == true)
            {
                markedAnswers += 'D';
                qAnswerD.toBlank();
            }
        }
    }
}
