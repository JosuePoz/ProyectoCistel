using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IU
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FClientes fclientes = new FClientes();
            fclientes.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FDepartamento fDepartamento = new FDepartamento();
            fDepartamento.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FMunicipio fmunicipio = new FMunicipio();
            fmunicipio.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FEnvios fEnvios = new FEnvios();
            fEnvios.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FPaquetes fPaquetes = new FPaquetes();
            fPaquetes.Show();
        }
    }
}
