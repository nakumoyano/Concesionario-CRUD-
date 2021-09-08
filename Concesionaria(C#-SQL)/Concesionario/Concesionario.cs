using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Concesionario
{
    public partial class Concesionario : Form
    {
        Datos odatos = new Datos();
        const int tam = 100000000;
        Vehiculos[] aVehiculos = new Vehiculos[tam];
        private bool crearVehiculo = false;
        int c;
        public Concesionario()
        {
            InitializeComponent();
            for (int i = 0; i < tam; i++)
            {
                aVehiculos[i] = null;
            }
        }

        private void Concesionario_Load(object sender, EventArgs e)
        {
            Habilitar(false);
            CargarCombo(cmbModelo, "modelos");
            CargarCombo(cmbMarca, "marcas");
            CargarCombo(cmbColor, "colores");
            CargarLista("Concesionarias");
            btnNuevo.Focus();
        }

        private void CargarCombo(ComboBox combo, string nombreTabla)
        {
            DataTable tabla = new DataTable();
            tabla = odatos.consultarTabla(nombreTabla);
            combo.DataSource = tabla;
            combo.ValueMember = tabla.Columns[0].ColumnName;
            combo.DisplayMember = tabla.Columns[1].ColumnName;
        }

        private void CargarLista(string nombreTabla)
        {
            odatos.leerTabla(nombreTabla);
            c = 0;
            while(odatos.Lector1.Read())
            {
                Vehiculos v = new Vehiculos();
                if (!odatos.Lector1.IsDBNull(0))
                    v.Codigo = odatos.Lector1.GetInt32(0);
                if (!odatos.Lector1.IsDBNull(1))
                    v.Modelo = odatos.Lector1.GetInt32(1);
                if (!odatos.Lector1.IsDBNull(2))
                    v.Marca = odatos.Lector1.GetInt32(2);
                if (!odatos.Lector1.IsDBNull(3))
                    v.Precio = Convert.ToDouble(odatos.Lector1.GetDecimal(3));
                if (!odatos.Lector1.IsDBNull(4))
                    v.Puertas = odatos.Lector1.GetInt32(4);
                if (!odatos.Lector1.IsDBNull(5))
                    v.Color = odatos.Lector1.GetInt32(5);

                aVehiculos[c] = v;
                c++;
            }
            odatos.Lector1.Close();
            odatos.desconectar();

            lstConcesionarias.Items.Clear();

            for (int i = 0; i < c; i++)
            {
                lstConcesionarias.Items.Add(aVehiculos[i].ToString());
            }
        }

        private void Habilitar(bool x)
        {
            rbTres.Enabled = x;
            rbCinco.Enabled = x;
            cmbModelo.Enabled = x;
            cmbMarca.Enabled = x;
            txtPrecio.Enabled = x;
            cmbColor.Enabled = x;
            lstConcesionarias.Enabled = x;
            btnEditar.Enabled = x;
            btnNuevo.Enabled = x;
            btnGrabar.Enabled = x;
            btnCancelar.Enabled = x;
            btnEliminar.Enabled = x;
            btnActualizar.Enabled = !x;
        }

        private void Limpiar()
        {
            txtPrecio.Clear();
            cmbModelo.SelectedIndex = -1;
            cmbMarca.SelectedIndex = -1;
            cmbColor.SelectedIndex = -1;
        }

        private bool validarCampos()
        {
            bool ok = true;
            
            if(cmbModelo.SelectedItem == null)
            {
                ok = false;
                errorProvider1.SetError(cmbModelo, "Elegí un Modelo, porfavor");
            }

            if(cmbMarca.SelectedItem == null)
            {
                ok = false;
                errorProvider1.SetError(cmbMarca, "Elegí una Marca, porfavor");
            }

            if(txtPrecio.Text == "")
            {
                ok = false;
                errorProvider1.SetError(txtPrecio, "Ingrese un precio, porfavor");
            }

            if(cmbColor.SelectedItem == null)
            {
                ok = false;
                errorProvider1.SetError(cmbColor, "Elegí un color, porfavor");
            }

            return ok;
        }

        private void BorrarMensaje()
        {
            errorProvider1.SetError(cmbModelo, null);
            errorProvider1.SetError(cmbMarca, null);
            errorProvider1.SetError(txtPrecio, "");
            errorProvider1.SetError(cmbColor, null);
        }
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            Habilitar(true);
            crearVehiculo = true;
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            BorrarMensaje();

            //insert

            if(validarCampos())
            {
                string consultaSQL = "";

                if (crearVehiculo)
                {
                    Vehiculos v = new Vehiculos();
                    v.Modelo = (int)cmbModelo.SelectedValue;
                    v.Marca = (int)cmbMarca.SelectedValue;
                    v.Precio = int.Parse(txtPrecio.Text);
                    v.Color = (int)cmbColor.SelectedValue;

                    if (rbTres.Checked)
                    {
                        v.Puertas = 1;
                    }

                    if (rbCinco.Checked)
                    {
                        v.Puertas = 2;
                    }

                    consultaSQL = $"INSERT INTO concesionarias (id_modelo, id_marca, precio, puertas, id_color)VALUES ({v.Modelo},{v.Marca},{v.Precio},{v.Puertas},{v.Color})";

                    odatos.actualizar(consultaSQL);
                }

                //update

                else
                {
                    int i = lstConcesionarias.SelectedIndex;
                    aVehiculos[i].Modelo = (int)cmbModelo.SelectedValue;
                    aVehiculos[i].Marca = (int)cmbMarca.SelectedValue;
                    aVehiculos[i].Precio = int.Parse(txtPrecio.Text);
                    aVehiculos[i].Color = (int)cmbColor.SelectedValue;

                    if(rbTres.Checked)
                    {
                        aVehiculos[i].Puertas = 1;
                    }

                    if(rbCinco.Checked)
                    {
                        aVehiculos[i].Puertas = 2;
                    }

                    consultaSQL = $"UPDATE concesionarias SET id_modelo={aVehiculos[i].Modelo}, id_marca={aVehiculos[i].Marca}, precio={aVehiculos[i].Precio}, puertas={aVehiculos[i].Puertas}, id_color={aVehiculos[i].Color}" + $"WHERE id_concesionaria={aVehiculos[i].Codigo}";

                    odatos.actualizar(consultaSQL);
                }

            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Habilitar(true);
            crearVehiculo = true;
            rbTres.Checked = true;
            Limpiar();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Habilitar(true);
            crearVehiculo = false;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Habilitar(false);
            Limpiar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Estás seguro que desea eliminar este producto?", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                string consultaSQL = $"DELETE FROM concesionarias WHERE id_concesionaria={aVehiculos[lstConcesionarias.SelectedIndex].Codigo}";
                odatos.actualizar(consultaSQL);
                CargarLista("concesionarias");
                Habilitar(false);
            }
        }

        private void txtPrecio_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 255))
            {
                MessageBox.Show("Solo se pueden ingresar números", "Advertencia", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }


        private void Concesionario_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Estás seguro que queres salir?", "Saliendo",
                   MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                e.Cancel = false;
            else
                e.Cancel = true;
        }

        private void lstConcesionarias_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void cargarCampos(int posicion)
        {
            cmbModelo.SelectedValue = aVehiculos[posicion].Modelo;
            cmbMarca.SelectedValue = aVehiculos[posicion].Marca;
            txtPrecio.Text = aVehiculos[posicion].Precio.ToString();
            cmbColor.SelectedValue = aVehiculos[posicion].Color;

            if(aVehiculos[posicion].Puertas == 0)
            {
                rbTres.Checked = true;
            }

            if(aVehiculos[posicion].Puertas == 1)
            {
                rbCinco.Checked = true;
            }
        }
    }
}
