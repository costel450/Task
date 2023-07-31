using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task
{
    public partial class Form2 : Form
    {
        public Form2(string text,string data,string cod,string provider)
        {
            InitializeComponent();
            TextBox textBox2 = new TextBox();
            textBox2.Location = new Point(20, 20);
            textBox2.Multiline = true;
            textBox2.Height = 25;
            textBox2.Width = 120;
            textBox2.Visible = true;
            textBox2.Text = text;

            Controls.Add(textBox2);

            TextBox textBox3 = new TextBox();
            textBox3.Location = new Point(200, 20);
            textBox3.Multiline = true;
            textBox3.Height = 25;
            textBox3.Width = 70;
            textBox3.Visible = true;
            textBox3.Text = data;
            Controls.Add(textBox3);
           

            TextBox textBox4 = new TextBox();
            textBox4.Location = new Point(400, 20);
            textBox4.Multiline = true;
            textBox4.Height = 25;
            textBox4.Width = 70;
            textBox4.Visible = true;
            textBox4.Text = cod;
            Controls.Add(textBox4);

            TextBox textBox5 = new TextBox();
            textBox5.Location = new Point(20, 120);
            textBox5.Multiline = true;
            textBox5.Height = 25;
            textBox5.Width = 120;
            textBox5.Visible = true;
            textBox5.Text = provider;
            Controls.Add(textBox5);

            this.FormClosing += Form2_FormClosing;

        }
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                //Form3 form3 = new Form3();
                //form3.Show();
            }


        }
       
        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}

