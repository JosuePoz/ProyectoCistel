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
    public partial class FEnvios : Form
    {
        int idSelected = 0;
        public FEnvios()
        {
            InitializeComponent();
        }
        public void activedFormEdicion()
        {
            comboBox3.Enabled = !comboBox3.Enabled;
            comboBox4.Enabled = !comboBox4.Enabled;
            textBox2.Enabled = !textBox2.Enabled;
            checkBox2.Enabled = !checkBox2.Enabled;
            monthCalendar2.Enabled = !monthCalendar2.Enabled;
            button2.Enabled = !button2.Enabled;
        }

        private void FEnvios_Load(object sender, EventArgs e)
        {
            LogicaConsultas Logica = new LogicaConsultas();
            this.dataGridView1.DataSource = Logica.ListarEnvios();
            this.dataGridView1.Refresh();
            comboBox1.DataSource = Logica.ListarClientes();
            comboBox1.DisplayMember = "nombre_completo";
            comboBox1.ValueMember = "ClienteId";
            comboBox3.DataSource = Logica.ListarClientes();
            comboBox3.DisplayMember = "nombre_completo";
            comboBox3.ValueMember = "ClienteId";
            comboBox2.DataSource = Logica.ListarPaquetes();
            comboBox2.DisplayMember = "nombre_destinatario";
            comboBox2.ValueMember = "PaqueteId";
            comboBox4.DataSource = Logica.ListarPaquetes();
            comboBox4.DisplayMember = "nombre_destinatario";
            comboBox4.ValueMember = "PaqueteId";
            activedFormEdicion();
        }

        private void Buscar_Click(object sender, EventArgs e)
        {
            LogicaConsultas Logica = new LogicaConsultas();
            this.dataGridView1.DataSource = Logica.BuscarEnvios(monthCalendar3.SelectionStart.ToString("yyyy-MM-dd"));
            this.dataGridView1.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                LogicaConsultas Logica = new LogicaConsultas(); string resp;
                resp = Logica.GuardarEnvios(((int)comboBox1.SelectedValue).ToString(), ((int)comboBox2.SelectedValue).ToString(),monthCalendar1.SelectionStart.ToString("yyyy-MM-dd"), textBox1.Text, (Convert.ToInt32(checkBox1.Checked)).ToString());
                if (resp.ToUpper().Contains("ÉXITO"))
                {
                    MessageBox.Show("Se ha grabado con éxito", resp, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = Logica.ListarEnvios();
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
            checkBox1.Checked = false;

        }

        public void resetFormEdicion()
        {
            textBox2.Text = "";
            checkBox2.Checked = false;
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
                    comboBox3.SelectedIndex = comboBox1.FindStringExact(selectedRow.Cells[1].Value.ToString());
                    comboBox4.SelectedIndex = comboBox2.FindStringExact(selectedRow.Cells[2].Value.ToString());
                    monthCalendar2.SelectionStart = (DateTime)selectedRow.Cells[3].Value;
                    textBox2.Text = selectedRow.Cells[4].Value.ToString().Replace(',', '.');
                    checkBox2.Checked = (Boolean)selectedRow.Cells[5].Value;
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text!="")
            {
                LogicaConsultas Logica = new LogicaConsultas(); string resp;
                resp = Logica.EditarEnvios(idSelected.ToString(), ((int)comboBox3.SelectedValue).ToString(), ((int)comboBox4.SelectedValue).ToString(), monthCalendar2.SelectionStart.ToString("yyyy-MM-dd"), textBox2.Text, (Convert.ToInt32(checkBox2.Checked)).ToString());
                if (resp.ToUpper().Contains("ÉXITO"))
                {
                    MessageBox.Show("Se ha editado con éxito", resp, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = Logica.ListarEnvios();
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
    }
}
