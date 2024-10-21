using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.Diagnostics;
using System.Windows.Forms.VisualStyles;

namespace SPEECHRECOGNITION
{
    public partial class Form1 : Form
    {
        SpeechSynthesizer Voice;
        Choices list;
        SpeechRecognitionEngine recEngine;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Voice = new SpeechSynthesizer();
            Voice.SelectVoiceByHints(VoiceGender.Female);

            list = new Choices();
            list.Add(new string[] {"hello","kohomada","what time is it","what is today","open browser"});

            Grammar gr = new Grammar(new GrammarBuilder(list));

            recEngine = new SpeechRecognitionEngine();
            try
            {
                recEngine.RequestRecognizerUpdate();
                recEngine.LoadGrammar(gr);
                recEngine.SpeechRecognized += RecEngine_SpeechRecognized;
                recEngine.SetInputToDefaultAudioDevice();
                recEngine.RecognizeAsync(RecognizeMode.Multiple);
            }
            catch
            {
                return;
            }
        }

        public void say(string sayText)
        {
            Voice.SpeakAsync(sayText);
        }

        private void RecEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string text = e.Result.Text;

            if (text == "hello")
                say("");
            if (text == "kohomada")
                say("keesi aulak naha");
            if (text == "what time is it")
                say(DateTime.Now.ToString("h:mm tt"));
            if (text == "what is today")
                say(DateTime.Now.ToString("yyyy/M/d"));
            if (text == "open browser")
                Process.Start("C:\\Program Files(x86)\\Microsoft\\Edge\\Application\\");
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
