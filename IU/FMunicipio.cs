using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using BLL;

namespace IU
{
    public partial class FMunicipio : Form
    {
        int idSelected = 0;
        public FMunicipio()
        {
            InitializeComponent();
        }

        private void FMunicipio_Load(object sender, EventArgs e)
        {
            LogicaConsultas Logica = new LogicaConsultas();
            this.dataGridView1.DataSource = Logica.ListarMunicipios();
            this.dataGridView1.Refresh();
            this.comboBox1.DataSource = Logica.ListarDepartamentos();
            this.comboBox1.DisplayMember = "nombre_departamento";
            this.comboBox1.ValueMember = "DepartamentoId";
            this.comboBox2.DataSource = Logica.ListarDepartamentos();
            this.comboBox2.DisplayMember = "nombre_departamento";
            this.comboBox2.ValueMember = "DepartamentoId";
            activedFormEdicion();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LogicaConsultas Logica = new LogicaConsultas();
            this.dataGridView1.DataSource = Logica.BuscarMunicipio(textBox3.Text);
            this.dataGridView1.Refresh();
        }

        private void activedFormEdicion()
        {
            textBox2.Enabled = !textBox2.Enabled;
            comboBox2.Enabled = !comboBox2.Enabled;
            button2.Enabled = !button2.Enabled;
            button4.Enabled = !button4.Enabled;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text!="")
            {
                LogicaConsultas Logica = new LogicaConsultas(); string resp;
                resp = Logica.GuardarMunicipio(textBox1.Text, ((int)comboBox1.SelectedValue).ToString());
                if (resp.ToUpper().Contains("ÉXITO"))
                {
                    MessageBox.Show("Se ha grabado con éxito", resp, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = Logica.ListarMunicipios();
                    dataGridView1.Refresh();
                    textBox1.Text = "";
                }
                else
                    MessageBox.Show(resp, "Error al grabar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("Se necesita llenar todos los campos", "Error al grabar", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index != -1)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[index];
                string valor = selectedRow.Cells[0].Value.ToString();
                if (valor != "")
                {
                    idSelected = Convert.ToInt32(selectedRow.Cells[0].Value.ToString());
                    textBox2.Text = selectedRow.Cells[1].Value.ToString();
                    comboBox2.SelectedIndex = comboBox2.FindStringExact(selectedRow.Cells[2].Value.ToString());
                    if (!textBox2.Enabled)
                        activedFormEdicion();
                }
                else if (textBox2.Enabled)
                {
                    activedFormEdicion();
                    textBox2.Text = "";
                    idSelected = 0;
                }
            }
            else if (textBox2.Enabled)
            {
                activedFormEdicion();
                textBox2.Text = "";
                idSelected = 0;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                LogicaConsultas Logica = new LogicaConsultas(); string resp;
                resp = Logica.EditarMunicipio(idSelected.ToString() ,textBox2.Text, ((int)comboBox2.SelectedValue).ToString());
                if (resp.ToUpper().Contains("ÉXITO"))
                {
                    MessageBox.Show("Se ha grabado con éxito", resp, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = Logica.ListarMunicipios();
                    dataGridView1.Refresh();
                    textBox2.Text = "";
                    activedFormEdicion();
                }
                else
                    MessageBox.Show(resp, "Error al grabar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("Se necesita llenar todos los campos", "Error al grabar", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LogicaConsultas Logica = new LogicaConsultas(); string resp;
            resp = Logica.EliminarMunicipio(idSelected.ToString());
            if (resp.ToUpper().Contains("ÉXITO"))
            {
                MessageBox.Show("Se ha eliminado con éxito", resp, MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = Logica.ListarMunicipios();
                dataGridView1.Refresh();
                textBox2.Text = "";
                activedFormEdicion();
                idSelected = 0;
            }
            else
                MessageBox.Show(resp, "Error al eliminar", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
