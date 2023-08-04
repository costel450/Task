using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task
{
    public partial class Form4 : Form
    {
        private string adobe = @"C:\Program Files (x86)\Adobe\Acrobat Reader DC\Reader\AcroRd32.exe";

        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
           

        }
        private void data(string filepath )

        {
            Form1 form1 = new Form1();
            string x = form1.ExtractImagesAndTextFromPDFPage(filepath);



        }

        private void extract_data(string filepath)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            OpenFileDialog openPdf = new OpenFileDialog();
            openPdf.Filter = "PDF files|*.pdf|All files|*.*";

            if (openPdf.ShowDialog() == DialogResult.OK)
            {
                string filePath = openPdf.FileName;
                Process.Start(adobe, filePath);


            }

        }
    }
}
