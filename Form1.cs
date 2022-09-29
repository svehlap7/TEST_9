using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace TEST_BINAR
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int posledni;

        private void button1_Click(object sender, EventArgs e)
        {
            FileStream stream = new FileStream(@"..\..\cisla.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            BinaryWriter binarpis = new BinaryWriter(stream);
            BinaryReader binarcti = new BinaryReader(stream);
            Random random = new Random();

            for (int i = 0; i < 10; i++)
            {
                int cisla = random.Next(-10, 11);
                binarpis.Write(cisla);
                posledni = cisla;
            }

            binarpis.Seek(0, SeekOrigin.Begin);
            listBox1.Items.Clear();

            while (binarcti.BaseStream.Position < binarcti.BaseStream.Length)
            {
                int x = binarcti.ReadInt32();
                listBox1.Items.Add(x);
            }
            stream.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();

            FileStream stream = new FileStream(@"..\..\cisla.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            BinaryReader binarcti = new BinaryReader(stream);
            BinaryWriter binarpis = new BinaryWriter(stream);

             binarpis.Seek(0, SeekOrigin.Begin);

            while (binarcti.BaseStream.Position < binarcti.BaseStream.Length)
            {
                int cisla = binarcti.ReadInt32();
                if (posledni % 2 != 0)
                {
                    if (cisla % 2 == 0)
                    {
                        cisla = cisla + 1;
                    }
                }
                else
                {
                    if (cisla % 2 != 0)
                    {
                        cisla = cisla * 2;
                    }
                }
                binarpis.BaseStream.Position -= 4;
                binarpis.Write(cisla);
            }
            stream.Close();

            FileStream stream2 = new FileStream(@"..\..\cisla.dat", FileMode.Open, FileAccess.Read);
            BinaryReader binarcti2= new BinaryReader(stream2);

            while (binarcti2.BaseStream.Position < binarcti2.BaseStream.Length)
            {
                int cisla = binarcti2.ReadInt32();
                listBox2.Items.Add(cisla);
            }
            stream2.Close();
        }
    }
}
