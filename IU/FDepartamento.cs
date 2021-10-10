using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;

namespace IU
{
    public partial class FDepartamento : Form
    {
        int idSelected = 0;
        public FDepartamento()
        {
            InitializeComponent();
        }

        private void FDepartamento_Load(object sender, EventArgs e)
        {
            LogicaConsultas Logica = new LogicaConsultas();
            this.dataGridView1.DataSource = Logica.ListarDepartamentos();
            this.dataGridView1.Refresh();
            activedFormEdicion();
        }
        public void activedFormEdicion()
        {
            textBox2.Enabled = !textBox2.Enabled;
            button2.Enabled = !button2.Enabled;
            button4.Enabled = !button4.Enabled;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (validateFormCrear())
            {
                LogicaConsultas Logica = new LogicaConsultas(); string resp;
                resp = Logica.GuardarDepartamento(textBox1.Text);
                if (resp.ToUpper().Contains("ÉXITO"))
                {
                    MessageBox.Show("Se ha grabado con éxito", resp, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = Logica.ListarDepartamentos();
                    dataGridView1.Refresh();
                    resetFormCrear();
                }
                else
                    MessageBox.Show(resp, "Error al grabar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("Se necesita llenar todos los campos", "Error al grabar", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private bool validateFormCrear()
        {
            if (textBox1.Text == "")
                return false;
            return true;
        }
        private void resetFormCrear()
        {
            textBox1.Text = "";
        }
        private void button3_Click(object sender, EventArgs e)
        {
            LogicaConsultas Logica = new LogicaConsultas();
            this.dataGridView1.DataSource = Logica.BuscarDepartamento(textBox3.Text);
            this.dataGridView1.Refresh();
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
                    if (!textBox2.Enabled)
                        activedFormEdicion();
                }
                else if (textBox2.Enabled)
                {
                    activedFormEdicion();
                    resetFormEdicion();
                    idSelected = 0;
                }
            }
            else if (textBox2.Enabled)
            {
                activedFormEdicion();
                resetFormEdicion();
                idSelected = 0;
            }
        }
        private void resetFormEdicion()
        {
            textBox2.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LogicaConsultas Logica = new LogicaConsultas(); string resp;
            resp = Logica.EliminarDepartamento(idSelected);
            if (resp.ToUpper().Contains("ÉXITO"))
            {
                MessageBox.Show("Se ha eliminado con éxito", resp, MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = Logica.ListarDepartamentos();
                dataGridView1.Refresh();
                resetFormEdicion();
                activedFormEdicion();
                idSelected = 0;
            }
            else
                MessageBox.Show(resp, "Error al eliminar", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (validateFormEdicion())
            {
                LogicaConsultas Logica = new LogicaConsultas(); string resp;
                resp = Logica.EditarDepartamento(idSelected, textBox2.Text);
                if (resp.ToUpper().Contains("ÉXITO"))
                {
                    MessageBox.Show("Se ha editado con éxito", resp, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = Logica.ListarDepartamentos();
                    dataGridView1.Refresh();
                    resetFormEdicion();
                    activedFormEdicion();
                    idSelected = 0;
                }
                else
                    MessageBox.Show(resp, "Error al editar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("Se necesita llenar todos los campos", "Error al editar", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private bool validateFormEdicion()
        {
            if (textBox2.Text == "")
                return false;
            return true;
        }
    }
}
