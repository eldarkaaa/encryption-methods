using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZI.Lab1
{
    public partial class Form3 : Form
    {
        private string alphabet = ("абвгдеёжзийклмнопрстуфхцчшщъыьэюя");
        public string coded;
        private char empty = ' ';
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int k;
            string message;
            k = (Convert.ToInt32(textBox1.Text));
            message = richTextBox1.Text;

            for (int i = 0; i < message.Length; i++)
            {
                if (message[i] == empty)
                {
                    coded += " ";
                }
                else
                    for (int j = 0; j < alphabet.Length; j++)
                    {
                        if (message[i] == alphabet[j])
                        {
                            if (j - k >= 0)
                            {
                                coded += decoding(message[i], alphabet[j - k]);
                            }
                            else
                            {
                                while (j - k < 0)
                                {
                                    coded += decoding(message[i], alphabet[j + 32 - k + 1]);
                                    break;
                                }
                                k = (Convert.ToInt32(textBox1.Text));
                            }
                        }
                    }
            }
            richTextBox2.Text = coded;
        }
        public char decoding(char message, char alphabet)
        {
            message = alphabet;
            return message;
        }
        private void шифрованиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            this.Close();
            form2.Show();

        }
    }
}
