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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList; // Set the drop-down style
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged; // Subscribe to the event
            this.FormClosing += Form3_FormClosing;

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                string selectedValue = comboBox1.SelectedItem.ToString();
                if (selectedValue == "Declaratie Vamala")
                {
                    // Show Form1
                    Form1 form1 = new Form1();
                    this.Hide();
                    form1.ShowDialog();
                   



                }
            }
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {

        }
        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
            }


        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}
