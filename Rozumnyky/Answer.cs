using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;
using Microsoft.DirectX.AudioVideoPlayback;

namespace Rozumnyky
{
    class Answer
    {
        private char ansLetter;
        private System.Windows.Forms.Label ansLabel;
        private System.Windows.Forms.PictureBox ansPic;
        private Question currentQuestion;

        public bool ansIfLocked = false;
        public bool ansIfCorrect;
        public bool ansIfMarked;

        public Answer(string ansText, char ansLetter, System.Windows.Forms.PictureBox ansPic, System.Windows.Forms.Label ansLabel,
            bool ansIfCorrect, bool ansIfMarked, Question currentQuestion)
        {
            this.ansPic = ansPic;
            this.ansLabel = ansLabel;
            this.ansLabel.Text = ansText;
            this.ansLetter = ansLetter;
            this.ansIfCorrect = ansIfCorrect;
            this.ansIfMarked = ansIfMarked;
            this.currentQuestion = currentQuestion;
        }

        public void toBlank()
        {
            ansPic.Image = Form3.getPictureRes("blank");
            ansLabel.Text = "";
        }

        public void toBlankDD(Audio qFinal, Audio DD2)
        {
            toBlank();
            qFinal.Stop();
            DD2.Play();
        }

        public void toFinal(Audio qMusic, Audio qFinal, int qNumber)
        {
            if (qMusic.Playing == true && qNumber > 5) qMusic.Stop();
            qFinal.Play();
            ansLabel.ForeColor = Color.Black;
            ansPic.Image = Form3.getPictureRes(ansLetter + "_final");
            ansIfLocked = true;
        }

        public void toCorrect(Audio qMusic, Audio qFinal, Audio qCorrect, Audio qWrong, int qNumber)
        {
            if (ansIfCorrect == true)
            {
                if (ansIfCorrect == ansIfLocked)
                {
                    currentQuestion.ifAnsweredCorrectly = true;
                    if (qNumber == 5) qMusic.Stop();
                    if (qFinal.Playing == true) qFinal.Stop();
                    qCorrect.Play();
                    ansLabel.ForeColor = Color.Black;
                    for (int i = 1; i <= 11; i++)
                    {
                        if (i % 2 == 1)
                        {
                            ansPic.Image = Form3.getPictureRes(ansLetter + "_correct");
                            ansPic.Refresh();
                            Thread.Sleep(100);
                        }
                        else
                        {
                            ansPic.Image = Form3.getPictureRes(ansLetter + "_final");
                            ansPic.Refresh();
                            Thread.Sleep(100);
                        }
                    }
                    if (qMusic.Stopped && qNumber < 5) qMusic.Play();
                }
                else
                {
                    qMusic.Stop();
                    if (qFinal.Playing == true) qFinal.Stop();
                    qWrong.Play();
                    for (int i = 1; i <= 11; i++)
                    {
                        if (i % 2 == 1)
                        {
                            ansPic.Image = Form3.getPictureRes(ansLetter + "_correct");
                            ansLabel.ForeColor = Color.Black;
                            ansPic.Refresh();
                            Thread.Sleep(250);
                        }
                        else
                        {
                            ansPic.Image = Form3.getPictureRes(ansLetter + "_normal");
                            ansLabel.ForeColor = Color.White;
                            ansPic.Refresh();
                            Thread.Sleep(250);
                        }
                    }
                }
            }
        }
    }
}
