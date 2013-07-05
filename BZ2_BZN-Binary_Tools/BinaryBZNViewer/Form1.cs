using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BinaryBZNFile;

namespace BinaryBZNViewer
{
    public partial class Form1 : Form
    {
        string filename;

        public Form1()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            if (!e.Cancel)
            {
                filename = openFileDialog1.FileName;
                loadSelectedFile();
            }
        }

        private void loadSelectedFile()
        {
            if (System.IO.File.Exists(filename))
            {
                // unload anything loaded?
                //listBox1.Items.Clear();
                // />

                BinaryBZN BZNFile = new BinaryBZN(System.IO.File.OpenRead(filename));

                //VersionField;
                //Version;
                //SaveTypeField;
                //SaveType;
                //BinarySaveField;
                //BinarySave;
                listBox1.Items.Clear();
                listBox1.BeginUpdate();
                foreach(Field field in BZNFile.fields)
                {
                    listBox1.Items.Add(field);
                }
                listBox1.EndUpdate();
            }
        }
    }
}
