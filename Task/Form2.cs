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
        public Form2(string text, string data, string cod, string provider, string exp,string moneda,string valoare)
        {
            InitializeComponent();
            TextBox textBox2 = new TextBox();
            Label label2 = new Label();
            label2.Location = new Point(5, 25);
            label2.Text = "MRN";
            textBox2.Location = new Point(55, 20);
            textBox2.Multiline = true;
            textBox2.Height = 25;
            textBox2.Width = 120;
            textBox2.Visible = true;
            textBox2.Text = text;

            Controls.Add(textBox2);
            Controls.Add(label2);
            TextBox textBox3 = new TextBox();
            textBox3.Location = new Point(250, 20);
            textBox3.Multiline = true;
            textBox3.Height = 25;
            textBox3.Width = 70;
            textBox3.Visible = true;
            textBox3.Text = data;
            Label label3 = new Label();
            label3.Location = new Point(215, 25);
            label3.Text = "Date";

            Controls.Add(textBox3);
            Controls.Add(label3);


            TextBox textBox4 = new TextBox();
            Label label4 = new Label();
            label4.Location = new Point(350, 25);
            label4.Text = "HS";
            textBox4.Location = new Point(400, 20);
            textBox4.Multiline = true;
            textBox4.Height = 25;
            textBox4.Width = 70;
            textBox4.Visible = true;
            textBox4.Text = cod;
            Controls.Add(textBox4);
            Controls.Add(label4);

            TextBox textBox5 = new TextBox();
            textBox5.Location = new Point(55, 120);
            Label label5 = new Label();

            label5.Location = new Point(5, 120);
            label5.Text = "Provider";
            textBox5.Multiline = true;
            textBox5.Height = 35;
            textBox5.Width = 120;
            textBox5.Visible = true;
            textBox5.Text = provider;
            textBox5.TextAlign = HorizontalAlignment.Center;
            textBox5.Font = new Font("Times New Roman", 7);
            Controls.Add(textBox5);
            Controls.Add(label5);

            TextBox textBox6 = new TextBox();
            textBox6.Location = new Point(250, 120);
            Label label6 = new Label();

            label6.Location = new Point(215, 120);
            label6.Text = "Tara";
            textBox6.Multiline = true;
            textBox6.Height = 25;
            textBox6.Width = 75;
            textBox6.Visible = true;
            textBox6.Text = exp;
            textBox6.TextAlign = HorizontalAlignment.Center;
            Controls.Add(textBox6);
            Controls.Add(label6);

            TextBox textBox7 = new TextBox();
            textBox7.Location = new Point(400, 120);
            Label label7 = new Label();

            label7.Location = new Point(350, 120);
            label7.Text = "Currency";
            textBox7.Multiline = true;
            textBox7.Height = 25;
            textBox7.Width = 75;
            textBox7.Visible = true;
            textBox7.Text = moneda;
            textBox7.TextAlign = HorizontalAlignment.Center;
            Controls.Add(textBox7);
            Controls.Add(label7);

            TextBox textBox8 = new TextBox();
            textBox8.Location = new Point(55, 215);
            Label label8 = new Label();

            label8.Location = new Point(5, 215);
            label8.Text = "Value";
            textBox8.Multiline = true;
            textBox8.Height = 25;
            textBox8.Width = 75;
            textBox8.Visible = true;
            textBox8.Text = valoare;
            textBox8.TextAlign = HorizontalAlignment.Center;
            Controls.Add(textBox8);
            Controls.Add(label8);



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

