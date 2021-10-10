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
    public partial class FClientes : Form
    {
        int idSelected = 0;
        public FClientes()
        {
            InitializeComponent();
        }

        private void FClientes_Load(object sender, EventArgs e)
        {
            LogicaConsultas Logica = new LogicaConsultas();
            this.dataGridView1.DataSource = Logica.ListarClientes();
            this.dataGridView1.Refresh();
            activedFormEdicion();
        }

        private void Buscar_Click(object sender, EventArgs e)
        {
            LogicaConsultas Logica = new LogicaConsultas();
            this.dataGridView1.DataSource = Logica.BuscarCliente(textBox11.Text);
            this.dataGridView1.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (validateFormCrear())
            {
                LogicaConsultas Logica = new LogicaConsultas(); string resp;
                resp = Logica.GuardarCliente(textBox1.Text, textBox2.Text, textBox3.Text, textBox5.Text, textBox4.Text);
                if (resp.ToUpper().Contains("ÉXITO"))
                {
                    MessageBox.Show("Se ha grabado con éxito", resp, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = Logica.ListarClientes();
                    dataGridView1.Refresh();
                    resetFormCrear();
                }
                else
                    MessageBox.Show(resp, "Error al grabar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("Se necesita llenar todos los campos", "Error al grabar", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
        private void resetFormCrear()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";

        }
        //Valida el form de crear
        private bool validateFormCrear()
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "")
                return false;
            return true;
        }
        //Selecciona algun item del dataGrid
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
                    textBox6.Text = selectedRow.Cells[1].Value.ToString();
                    textBox7.Text = selectedRow.Cells[3].Value.ToString();
                    textBox8.Text = selectedRow.Cells[5].Value.ToString();
                    textBox9.Text = selectedRow.Cells[4].Value.ToString();
                    textBox10.Text = selectedRow.Cells[2].Value.ToString();
                    if(!textBox9.Enabled)
                        activedFormEdicion();
                }
                else if (textBox9.Enabled)
                {
                    activedFormEdicion();
                    resetFormEdicion();
                    idSelected = 0;
                }
            }
            else if (textBox9.Enabled)
            {
                activedFormEdicion();
                resetFormEdicion();
                idSelected = 0;
            }
        }

        //Bloquea/Activa el form de edición, según se requiera
        public void activedFormEdicion()
        {
            textBox6.Enabled = !textBox6.Enabled;
            textBox7.Enabled = !textBox7.Enabled;
            textBox8.Enabled = !textBox8.Enabled;
            textBox9.Enabled = !textBox9.Enabled;
            textBox10.Enabled = !textBox10.Enabled;
            button2.Enabled = !button2.Enabled;
            button3.Enabled = !button3.Enabled;
        }
        //Limpia el form de edición
        public void resetFormEdicion()
        {
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LogicaConsultas Logica = new LogicaConsultas(); string resp;
            resp = Logica.EliminarCliente(idSelected);
            if (resp.ToUpper().Contains("ÉXITO"))
            {
                MessageBox.Show("Se ha eliminado con éxito", resp, MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = Logica.ListarClientes();
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
                resp = Logica.EditarCliente(idSelected ,textBox6.Text, textBox7.Text, textBox8.Text, textBox9.Text, textBox10.Text);
                if (resp.ToUpper().Contains("ÉXITO"))
                {
                    MessageBox.Show("Se ha editado con éxito", resp, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = Logica.ListarClientes();
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
            if (textBox6.Text == "" || textBox7.Text == "" || textBox8.Text == "" || textBox9.Text == "" || textBox10.Text == "")
                return false;
            return true;
        }
    }
}
