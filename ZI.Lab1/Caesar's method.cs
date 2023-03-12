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
    public partial class Form2 : Form
    {
        private string alphabet = ("абвгдеёжзийклмнопрстуфхцчшщъыьэюя");
        private char empty = ' ';
        public string coded;
        public Form2()
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
                    coded += ' ';
                }
                else
                    for (int j = 0; j < alphabet.Length; j++)
                    {
                        if (message[i] == alphabet[j])
                        {
                            if (j + k < 33)
                            {
                                coded += coding(message[i], alphabet[j + k]);
                            }
                            else
                            {
                                while (j + k >= 33)
                                {
                                    k -= 33;
                                }
                                coded += coding(message[i], alphabet[j + k]);
                                k = (Convert.ToInt32(textBox1.Text));
                            }
                        }
                    }
            }
            richTextBox2.Text = coded;
        }
         private char coding(char message, char alphabet)
        {
            message = alphabet;
            return message;
        }

        private void дешифрованиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            this.Close();
            form3.Show();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
