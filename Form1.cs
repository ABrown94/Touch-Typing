using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TouchTyping
{
    public partial class Form1 : Form
    {
        Button b;
        private string randomPhrase;
        DateTime start;
        DateTime end;
        int seconds = 0;

        Dictionary<char, Button> buttonDic = new Dictionary<char, Button>();


        //Dictionary<string, Keys> letters;

        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true;
            foreach (Control c in buttonsPanel.Controls)
            {
                Button button = (Button)c;
                buttonDic.Add(button.Tag.ToString()[0], button);
            }
        }
        private void easyButton_Click(object sender, EventArgs e)
        {
            b = (Button)sender;
            InputTextBox.Focus();
            start = DateTime.Now;       
            timer1.Start();

            if (sender == easyButton)
            {
                PhraseLabel.Text = GetEasyPhrase();
            }
            if(sender == intermediateButton)
            {
                PhraseLabel.Text = GetIntermediatePhrase();
            }
            if(sender == hardButton)
            {
                PhraseLabel.Text = GetHardPhrase();
            }
        }
        private string GetEasyPhrase()
        {
            Random phrase = new Random();
            string[] level1 = new string[] { "a f j l d g a s k l d f g", "k h l j d f g a g k d a", "w h i v p m f w c s h u f h", "h n l a d s w f t g j t h s b m", "g w g p f n w a x p g n e z q m n p z"  };
            randomPhrase = (level1[phrase.Next(0, level1.Length)]);
            return randomPhrase;
        }
        private string GetIntermediatePhrase()
        {
            Random phrase = new Random();
            string[] level2 = new string[] { "The cat is black", "Bake a cake for one hour", "If you call me I will help you", "It is very cold today", "Please tell me what time it is" };
            randomPhrase = (level2[phrase.Next(0, level2.Length)]);
            return randomPhrase;
        }
        private string GetHardPhrase()
        {
            Random phrase = new Random();
            string[] level3 = new string[] { "Do not go gentle into that good night", "Indicate the way to my habitual abode", "Once upon a midnight dreary while I pondered weak and weary" };
            randomPhrase = (level3[phrase.Next(0, level3.Length)]);
            return randomPhrase;
        }
        private void Form1_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Space || e.KeyData == Keys.Back || e.KeyData == Keys.ShiftKey)
            {
                b.BackColor = Color.White;
            }
            else
            {
                KeysConverter kc = new KeysConverter();
                string keyChar = kc.ConvertToString(e.KeyData);
                foreach (char c in keyChar)
                {
                    try
                    {
                        Button b = buttonDic[c];
                        b.BackColor = Color.Red;
                    }
                    catch (KeyNotFoundException ex)
                    {
                    }
                }
            }
        }
        private void Form1_KeyUp_1(object sender, KeyEventArgs e)
        {
            KeysConverter kc = new KeysConverter();
            string keyChar = kc.ConvertToString(e.KeyData);
            foreach (char c in keyChar)
            {
                try
                {
                    Button b = buttonDic[c];
                    b.BackColor = Color.White;
                }
                catch(KeyNotFoundException ex)
                {
                }
            }
            End();
        }
        public void End()
        {
            end = DateTime.Now;          
            double time = end.Subtract(start).TotalSeconds;
            string[] words = InputTextBox.Text.Split(' ');
            double wpm = 0;
            string original = PhraseLabel.Text;
            string userText = InputTextBox.Text;

            if (userText.Length == original.Length)
            {
                double percentage = 0;
                double correct = 0;
                for (int i = 0; i < original.Length; i++)
                {                 
                    if (original[i] == userText[i])
                    {
                         correct++;
                    }                                      
                }
                timer1.Stop();
                percentage = (correct / original.Length * 100);
                wpm = (words.Length / time * percentage);
                DialogResult result = MessageBox.Show("Great Job!\nWPM: " + wpm + "\nPercentage: " + percentage + "\nTime: " + time + "\n Would you like to try again?", "Touch Typing", MessageBoxButtons.YesNo);   
                if(result == DialogResult.Yes)
                {
                    InputTextBox.Text = "";
                    start = DateTime.Now;
                    seconds = 0;
                    timer1.Start();
                }
                else
                {
                    Application.Exit();
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            seconds += 1;
            timeLabel.Text = "Time: " + seconds;
        }
    }
}

