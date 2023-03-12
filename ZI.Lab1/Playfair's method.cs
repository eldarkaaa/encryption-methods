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
    public partial class Playfair_s_method : Form
    {
        private const string APLHABET_ARRAY = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
        private const string PACIFIER_SYMBOL = "X";
        private char[,] keyMatrix = new char[5, 5];

        private readonly int SquareCells;

        public Playfair_s_method()
        {
            InitializeComponent();
            this.SquareCells = 5;
            this.dataGridView1.ColumnCount = this.dataGridView1.RowCount = this.SquareCells;
        }

        private void Result_button_Click(object sender, EventArgs e)
        {
            string inputString = this.Input_richTextBox.Text.Replace(" ", "").ToUpper();

            string digramString = this.GetDigram(inputString);

            this.Output_richTextBox.Text = this.Cipher(digramString, this.Decrypt_radioButton.Checked);

        }

        private void SetMatrix_button_Click(object sender, EventArgs e)
        {
            string keyString = this.Key_textBox.Text.Replace(" ", "");
            this.SetMatrix(keyString);
        }

        private string RemoveAllDuplicates(string inputStr)
        {
            string resultStr = "";
            HashSet<char> uniqueChars = new HashSet<char>(inputStr);

            foreach (char c in uniqueChars)
            {
                resultStr += c;
            }

            return resultStr;
        }

        private void SetMatrix(string key)
        {
            key += APLHABET_ARRAY;
            key = key.ToUpper().Replace("J", "I");

            key = this.RemoveAllDuplicates(key);
            for (int i = 0; i < 25; i++)
            {
                this.keyMatrix[(i % 5), (i / 5)] = key[i];
                this.dataGridView1[(i % 5), (i / 5)].Value = key[i];
            }
        }

        private int[] GetPosition(char[,] matrix, char symbol)
        {
            int[] result = new int[2];

            if (symbol == 'J')
            {
                return this.GetPosition(matrix, 'I');
            }

            for (int index = 0; index < 25; index++)
            {
                int i = index % 5;
                int j = index / 5;

                if (matrix[i, j] == symbol)
                {
                    result[0] = i;
                    result[1] = j;
                }
            }

            return result;
        }

        private string GetDigram(string inputStr)
        {
            string result = "";

            if (inputStr.Length % 2 != 0)
            {
                inputStr += PACIFIER_SYMBOL;
            }

            for (int i = 0; i < inputStr.Length - 1; i++)
            {
                result += inputStr[i];
                if (inputStr[i] == inputStr[i + 1])
                {
                    result += PACIFIER_SYMBOL;
                }
            }

            result += inputStr[inputStr.Length - 1];

            if (result.Length % 2 != 0)
            {
                result += PACIFIER_SYMBOL;
            }

            return result;
        }

        private string Cipher(string inputStr, bool Decode)
        {
            string result = "";

            for (int index = 0; index < inputStr.Length - 1; index += 2)
            {
                char firstSymbol = inputStr[index];
                char secondSymbol = inputStr[index + 1];

                int[] firstPos = GetPosition(keyMatrix, firstSymbol);
                int[] secondPos = GetPosition(keyMatrix, secondSymbol);

                if (!Decode)
                {
                    if (firstPos[0] == secondPos[0])
                    {
                        result += new string(this.SameColumn(firstPos, secondPos));
                    }
                    else if (firstPos[1] == secondPos[1])
                    {
                        result += new string(this.SameRow(firstPos, secondPos));
                    }
                    else
                    {
                        result += new string(this.DifferentRowColumn(firstPos, secondPos));
                    }
                }
                else
                {
                    if (firstPos[0] == secondPos[0])
                    {
                        result += new string(this.DecodeSameColumn(firstPos, secondPos));
                    }
                    else if (firstPos[1] == secondPos[1])
                    {
                        result += new string(this.DecodeSameRow(firstPos, secondPos));
                    }
                    else
                    {
                        result += new string(this.DecodeDifferentRowColumn(firstPos, secondPos));
                    }
                }


            }

            return result;
        }

        private char[] SameRow(int[] pos1, int[] pos2)
        {
            char[] result = new char[2];

            char firstSymbol = this.keyMatrix[this.Mod(pos1[0]), pos1[1]];
            char secondSymbol = this.keyMatrix[this.Mod(pos2[0]), pos2[1]];

            result[0] = firstSymbol;
            result[1] = secondSymbol;

            return result;
        }
        private char[] SameColumn(int[] pos1, int[] pos2)
        {
            char[] result = new char[2];

            char firstSymbol = this.keyMatrix[pos1[0], this.Mod(pos1[1])];
            char secondSymbol = this.keyMatrix[pos2[0], this.Mod(pos2[1])];

            result[0] = firstSymbol;
            result[1] = secondSymbol;

            return result;
        }

        private char[] DifferentRowColumn(int[] pos1, int[] pos2)
        {
            char[] result = new char[2];

            char firstSymbol = this.keyMatrix[pos2[0], pos1[1]];
            char secondSymbol = this.keyMatrix[pos1[0], pos2[1]];

            result[0] = firstSymbol;
            result[1] = secondSymbol;

            return result;
        }

        private int Mod(int x)
        {
            return (x + 1) % this.SquareCells;
        }

        private char[] DecodeSameRow(int[] pos1, int[] pos2)
        {
            char[] result = new char[2];

            char firstSymbol = this.keyMatrix[this.ReverseMod(pos1[0]), pos1[1]];
            char secondSymbol = this.keyMatrix[this.ReverseMod(pos2[0]), pos2[1]];

            result[0] = firstSymbol;
            result[1] = secondSymbol;

            return result;
        }
        private char[] DecodeSameColumn(int[] pos1, int[] pos2)
        {
            char[] result = new char[2];

            char firstSymbol = this.keyMatrix[pos1[0], this.ReverseMod(pos1[1])];
            char secondSymbol = this.keyMatrix[pos2[0], this.ReverseMod(pos2[1])];

            result[0] = firstSymbol;
            result[1] = secondSymbol;

            return result;
        }

        private char[] DecodeDifferentRowColumn(int[] pos1, int[] pos2)
        {
            char[] result = new char[2];

            char firstSymbol = this.keyMatrix[pos2[0], pos1[1]];
            char secondSymbol = this.keyMatrix[pos1[0], pos2[1]];


            result[0] = firstSymbol;
            result[1] = secondSymbol;

            return result;
        }
        private int ReverseMod(int x)
        {
            int result = (x - 1) % this.SquareCells;

            if (result < 0)
            {
                result += this.SquareCells;
            }

            return result;
        }
    }
}

