using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Morse
{
    public partial class FormMain : Form
    {
        Dictionary<string, string> ENG_MORSE = new Dictionary<string, string>
        { { "A", ".-" }, { "B", "-..." }, { "C", "-.-." }, { "D", "-.." },
            { "E", "." }, { "F", "..-." }, { "G", "--." }, { "H", "...." },
            { "I", ".." }, { "J", ".---" }, { "K", "-.-" }, { "L", ".-.." },
            { "M", "--" }, { "N", "-." }, { "O", "---" }, { "P", ".--." },
            { "Q", "--.-" }, { "R", ".-." }, { "S", "..." }, { "T", "-" },
            { "U", "..-" }, { "V", "...-" }, { "W", ".--" }, { "X", "-..-" },
            { "Y", "-.--" }, { "Z", "--.." }, { "1", ".----" }, { "2", "..---" },
            { "3", "...--" }, { "4", "....-" }, { "5", "....." }, { "6", "-...." },
            { "7", "--..." }, { "8", "---.." }, { "9", "----." }, { "0", "-----" } };

        Dictionary<string, string> MORSE_ENG = new Dictionary<string, string>
        { {".-" , "A"},{"-..." , "B"},{"-.-." , "C"},{"-.." , "D"},{"." , "E"},
            {"..-." , "F"},{"--." , "G"},{"...." , "H"},{".." , "I"},{".---" , "J"},
            {"-.-" , "K"},{".-.." , "L"},{"--" , "M"},{"-." , "N"},{"---" , "O"},
            {".--." , "P"},{"--.-" , "Q"},{".-." , "R"},{"..." , "S"},{"-" , "T"},
            {"..-" , "U"},{"...-" , "V"},{".--" , "W"},{"-..-" , "X"},{"-.--" , "Y"},
            {"--.." , "Z"},{".----" , "1"},{"..---" , "2"},{"...--" , "3"},{"....-" , "4"},
            {"....." , "5"},{"-...." , "6"},{"--..." , "7"},{"---.." , "8"},{"----." , "9"},{"-----" , "0"}};

        Dictionary<string, string> RUS_MORSE = new Dictionary<string, string>
        { { "А", ".-" }, { "Б", "-..." }, { "Ц", "-.-." }, { "Д", "-.." },{ "Ъ", ".--.-." },
            { "Е", "." }, { "Ф", "..-." }, { "Г", "--." }, { "Х", "...." },{ "Ч", "---." },
            { "И", ".." }, { "Й", ".---" }, { "К", "-.-" }, { "Л", ".-.." },{ "Ш", "----" },
            { "М", "--" }, { "Н", "-." }, { "О", "---" }, { "П", ".--." },{ "Э", "..-.." },
            { "Щ", "--.-" }, { "Я", ".-.-" }, { "С", "..." }, { "Т", "-" },{ "Ю", "..--" },
            { "У", "..-" }, { "Ж", "...-" }, { "В", ".--" }, { "Ь", "-..-" }, { "Р", ".-." },
            { "Ы", "-.--" }, { "З", "--.." }, { "1", ".----" }, { "2", "..---" },
            { "3", "...--" }, { "4", "....-" }, { "5", "....." }, { "6", "-...." },
            { "7", "--..." }, { "8", "---.." }, { "9", "----." }, { "0", "-----" } };

        Dictionary<string, string> MORSE_RUS = new Dictionary<string, string>
        { { ".-", "А" }, { "-...", "Б" }, { "-.-.", "Ц" }, { "-..", "Д" }, { ".--.-.", "Ъ" },
            { ".", "Е" }, { "..-.", "Ф" }, { "--.", "Г" }, { "....", "Х" }, { "---.", "Ч" },
            { "..", "И" }, { ".---", "Й" }, { "-.-", "К" }, { ".-..", "Л" }, { "----", "Ш" },
            { "--", "М" }, { "-.", "Н" }, { "---", "О" }, { ".--.", "П" }, { "..-..", "Э" },
            { "--.-", "Щ" }, { ".-.-", "Я" }, { "...", "С" }, { "-", "Т" }, { "..--", "Ю" },
            { "..-", "У" }, { "...-", "Ж" }, { ".--", "В" }, { "-..-", "Ь" }, { ".-.", "Р" },
            { "-.--", "Ы" }, { "--..", "З" }, { ".----", "1" }, { "..---", "2" }, { "...--", "3" },
            { "....-", "4" }, { ".....", "5" }, { "-....", "6" }, { "--...", "7" }, { "---..", "8" },
            { "----.", "9" }, { "-----", "0" }};
        public FormMain()
        {
            InitializeComponent();
        }


        private void FormMain_Load(object sender, EventArgs e)
        {
            Button btn;
            foreach (var item in new string[]{".", "-" })
            {
                btn = new Button();
                btn.Text = item;
                btn.Height = 50;
                btn.Width = 50;
                btn.Click += new System.EventHandler(btnLetter_Click);
                flowLayoutPanelKeyBoard.Controls.Add(btn);
            }
            for (int i = 0; i < 26; i++)
            {
                btn = new Button();
                btn.Text = ((char)('A' + i)).ToString();
                btn.Height = 50;
                btn.Width = 50;
                btn.Click += new System.EventHandler(btnLetter_Click);
                flowLayoutPanelKeyBoard.Controls.Add(btn);
            }
            for (int i = 0; i < 10; i++)
            {
                btn = new Button();
                btn.Text = i.ToString();
                btn.Height = 50;
                btn.Width = 50;
                btn.Click += new System.EventHandler(btnLetter_Click);
                flowLayoutPanelKeyBoard.Controls.Add(btn);
            }
            radioButtonRussian.Checked = true;
        }

        private void btnLetter_Click(object sender, EventArgs e)
        {
            textBoxInputText.Text += (sender as Button).Text;
        }

        // Кодируем
        private void buttonEncode_Click(object sender, EventArgs e)
        {
            string resultString = "";
            string[] words = textBoxInputText.Text.Split(new char[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var word in words)
            {
                foreach (var letter in word)
                {
                    try
                    {
                        if (radioButtonEnglish.Checked)
                            resultString += ENG_MORSE[char.ToUpper(letter).ToString()] + " ";

                        else
                            resultString += RUS_MORSE[char.ToUpper(letter).ToString()] + " ";
                    }
                    catch (Exception)
                    {
                        textBoxResult.Text = "В тексте присутствуют недопустимые символы";
                        return;
                    }
                }
                resultString += "  ";//2 пробела
            }
            textBoxResult.Text = resultString;
        }
        //Раскодируем
        private void buttonDecode_Click(object sender, EventArgs e)
        {
            string resultString = "";
            string[] words = textBoxInputText.Text.Split(new string[] { "   ", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);// 3 пробела
            foreach (var word in words)
            {
                var temp = word.Split(' ');
                foreach (var letter in temp)
                {
                    try
                    {
                        if (radioButtonEnglish.Checked)
                            resultString += MORSE_ENG[letter];

                        else
                            resultString += MORSE_RUS[letter];
                    }
                    catch (Exception)
                    {
                        textBoxResult.Text = "В тексте присутствуют недопустимые символы";
                        return;
                    }
                }
                resultString += " ";
            }
            textBoxResult.Text = resultString;
        }

    }
}
