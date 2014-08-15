using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BinaryBZNFile;
using System.IO;

namespace BinaryBZNViewer
{
    public partial class Form1 : Form
    {
        private string filename;
        private IBinaryBZN BZNFile;
        private bool EnableListSelectCallback = true;

        public Form1()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            if (!e.Cancel)
            {
                filename = openFileDialog1.FileName;
                loadSelectedFile();
            }
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            if (!e.Cancel)
            {
                filename = saveFileDialog1.FileName;
                saveSelectedFile();
            }
        }

        private void loadSelectedFile()
        {
            if (System.IO.File.Exists(filename))
            {
                if (Path.GetExtension(filename).ToLowerInvariant() == ".bin")
                {
                    BZNFile = new N64BZN(System.IO.File.OpenRead(filename));
                }else{
                    BZNFile = new BinaryBZN(System.IO.File.OpenRead(filename), bz1Mode.Checked);
                }

                saveFileDialog1.FileName = filename;
                saveToolStripMenuItem.Enabled = true;

                listBox1.Items.Clear();
                listBox1.BeginUpdate();
                foreach(Field field in BZNFile.fields)
                {
                    listBox1.Items.Add(field);
                }
                listBox1.EndUpdate();
            }
        }

        private void saveSelectedFile()
        {
            BZNFile.save(System.IO.File.OpenWrite(filename));
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            fieldIndex.Text = listBox1.SelectedIndex.ToString();
            if (listBox1.SelectedItem != null && EnableListSelectCallback)
            {
                byte[] data = ((Field)listBox1.SelectedItem).GetRawRef();
                if (data != null)
                {
                    hexBox1.ByteProvider = new Be.Windows.Forms.DynamicByteProvider(data);
                    hexBox1.ByteProvider.Changed += new EventHandler(ByteProvider_Changed);
                }
                else
                {
                    hexBox1.ByteProvider = null;
                }
            }
        }

        private void ByteProvider_Changed(object sender, EventArgs e)
        {
            ((Field)listBox1.SelectedItem).SetRawRef(((Be.Windows.Forms.DynamicByteProvider)sender).Bytes.ToArray());
            EnableListSelectCallback = false;
            listBox1.SuspendLayout();
            int index = listBox1.SelectedIndex;
            listBox1.Items[index] = listBox1.Items[index];
            listBox1.ResumeLayout();
            EnableListSelectCallback = true;
        }
    }
}
