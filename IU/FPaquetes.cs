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
    public partial class FPaquetes : Form
    {
        int idSelected = 0;
        public FPaquetes()
        {
            InitializeComponent();
        }

        public void activedFormEdicion()
        {
            comboBox2.Enabled = !comboBox2.Enabled;
            textBox5.Enabled = !textBox5.Enabled;
            textBox6.Enabled = !textBox6.Enabled;
            textBox7.Enabled = !textBox7.Enabled;
            textBox8.Enabled = !textBox8.Enabled;
            button2.Enabled = !button2.Enabled;
        }

        private void FPaquetes_Load(object sender, EventArgs e)
        {
            LogicaConsultas Logica = new LogicaConsultas();
            this.dataGridView1.DataSource = Logica.ListarPaquetes();
            this.dataGridView1.Refresh();
            comboBox1.DataSource = Logica.ListarMunicipios();
            comboBox1.DisplayMember = "nombre_municipio";
            comboBox1.ValueMember = "MunicipioId";
            comboBox2.DataSource = Logica.ListarMunicipios();
            comboBox2.DisplayMember = "nombre_municipio";
            comboBox2.ValueMember = "MunicipioId";
            activedFormEdicion();
        }

        private void Buscar_Click(object sender, EventArgs e)
        {
            LogicaConsultas Logica = new LogicaConsultas();
            this.dataGridView1.DataSource = Logica.BuscaPaquetes(textBox9.Text);
            this.dataGridView1.Refresh();
        }

        private bool validateFormCrear()
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
                return false;
            return true;
        }

        private void resetFormCrear()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";

        }
        //Valida el form de crear

        private void button1_Click(object sender, EventArgs e)
        {
            if (validateFormCrear())
            {
                LogicaConsultas Logica = new LogicaConsultas(); string resp;
                resp = Logica.GuardarPaquetes(((int)comboBox1.SelectedValue).ToString(),  textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text);
                if (resp.ToUpper().Contains("ÉXITO"))
                {
                    MessageBox.Show("Se ha grabado con éxito", resp, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = Logica.ListarPaquetes();
                    dataGridView1.Refresh();
                    resetFormCrear();
                }
                else
                    MessageBox.Show(resp, "Error al grabar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("Se necesita llenar todos los campos", "Error al grabar", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void resetFormEdicion()
        {
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
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
                    comboBox2.SelectedIndex = comboBox1.FindStringExact(selectedRow.Cells[1].Value.ToString());
                    textBox5.Text = selectedRow.Cells[2].Value.ToString();
                    textBox6.Text = selectedRow.Cells[4].Value.ToString();
                    textBox7.Text = selectedRow.Cells[5].Value.ToString();
                    textBox8.Text = selectedRow.Cells[3].Value.ToString().Replace(',', '.');
                    if (!textBox5.Enabled)
                        activedFormEdicion();
                }
                else if (textBox5.Enabled)
                {
                    activedFormEdicion();
                    resetFormEdicion();
                    idSelected = 0;
                }
            }
            else if (textBox5.Enabled)
            {
                activedFormEdicion();
                resetFormEdicion();
                idSelected = 0;
            }
        }

        private bool validateFormEdicion()
        {
            if (textBox5.Text == "" || textBox6.Text == "" || textBox7.Text == "" || textBox8.Text == "")
                return false;
            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (validateFormEdicion())
            {
                LogicaConsultas Logica = new LogicaConsultas(); string resp;
                resp = Logica.EditarPaquete(idSelected.ToString(), ((int)comboBox2.SelectedValue).ToString(), textBox5.Text, textBox6.Text, textBox7.Text, textBox8.Text);
                if (resp.ToUpper().Contains("ÉXITO"))
                {
                    MessageBox.Show("Se ha editado con éxito", resp, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = Logica.ListarPaquetes();
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

        //Selecciona algun item del dataGrid
    }
}
