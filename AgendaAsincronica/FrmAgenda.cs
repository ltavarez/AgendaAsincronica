using AgendaAsincronica.CustomControlItem;
using BusinessLayer;
using Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgendaAsincronica
{
    public partial class FrmAgenda : Form
    {
        private ServicioPersona servicioPersona;
        private ServicioTipoContacto servicioTipoContacto;
        private int id;
        public FrmAgenda()
        {
            InitializeComponent();
            servicioPersona = new ServicioPersona();
            servicioTipoContacto = new ServicioTipoContacto();
            id = 0;
        }

        #region "Eventos"
        private async void FrmAgenda_Load(object sender, EventArgs e)
        {
            await LoadComboBox();
            await LoadContacto();
        }
        private async void BtnGuardar_Click(object sender, EventArgs e)
        {
            if(id == 0)
            {
                await AddPersona();
            }
            else
            {
                await EditPersona();
            }           
        }

        private async void DgvContactos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                id = Convert.ToInt32(DgvContactos.Rows[e.RowIndex].Cells[0].Value.ToString());
                BtnDeseleccionar.Visible = true;

                await LoadEditContacto();

            }
        }     

        private async void BtnEliminar_Click(object sender, EventArgs e)
        {
            await Eliminar();
        }

        private void BtnDeseleccionar_Click(object sender, EventArgs e)
        {
            Deseleccionar();
            Limpiar();
        }

        #endregion

        #region "Metodos privados"

        private async Task LoadEditContacto()
        {
            Persona personaEditar = await servicioPersona.GetById(id);

            TxtNombre.Text = personaEditar.Nombre;
            TxtApellido.Text = personaEditar.Apellido;
            TxtTelefono.Text = personaEditar.Telefono;
            var tipoContacto = await servicioTipoContacto.GetById(personaEditar.IdTipoContacto.Value);

            CbxTipoContactos.SelectedIndex = CbxTipoContactos.FindStringExact(tipoContacto?.Name);


        }

        private void Deseleccionar()
        {
            DgvContactos.ClearSelection();
            BtnDeseleccionar.Visible = false;
        }

        private async Task Eliminar()
        {
            if(id == 0)
            {
                MessageBox.Show("Debe seleccionar un contacto", "Notificacion");
            }
            else
            {
                DialogResult respuesta = MessageBox.Show("Esta seguro que desea eliminar este contacto", "Pregunta",MessageBoxButtons.OKCancel);

                if(respuesta == DialogResult.OK)
                {
                    bool resultado = await servicioPersona.Delete(id);

                    if (resultado)
                    {
                        MessageBox.Show("Se ha elimnado con exito", "Notificacion");
                        await LoadContacto();
                        Limpiar();
                        Deseleccionar();
                    }
                    else
                    {
                        MessageBox.Show("Ha ocurrido un error", "Error");
                    }
                }
            }

        }

        private async Task EditPersona()
        {

            ComboBoxItem selectedItem = (ComboBoxItem)CbxTipoContactos.SelectedItem;

            Persona persona = new Persona
            {
                Id = id,
                Nombre = TxtNombre.Text,
                Apellido = TxtApellido.Text,
                Telefono = TxtTelefono.Text,
                IdTipoContacto = Convert.ToInt32(selectedItem.Value)
            };

            bool resultado = await servicioPersona.Edit(persona);

            if (resultado)
            {
                MessageBox.Show("Se ha editado con exito", "Notificacion");
            }
            else
            {
                MessageBox.Show("Ha ocurrido un error", "Error");
            }

            Limpiar();
            Deseleccionar();
            await LoadContacto();
        }
        private async Task AddPersona()
        {

            ComboBoxItem selectedItem = (ComboBoxItem) CbxTipoContactos.SelectedItem;

            Persona persona = new Persona
            {
                Nombre = TxtNombre.Text,
                Apellido = TxtApellido.Text,
                Telefono = TxtTelefono.Text,
                IdTipoContacto = Convert.ToInt32(selectedItem.Value)
            };

            bool resultado = await servicioPersona.Add(persona);

            if (resultado)
            {
                MessageBox.Show("Se ha creado con exito", "Notificacion");
            }
            else
            {
                MessageBox.Show("Ha ocurrido un error", "Error");
            }

            Limpiar();
            await LoadContacto();
        }

        private void Limpiar()
        {
            TxtNombre.Clear();
            TxtApellido.Clear();
            TxtTelefono.Clear();

            TxtNombre.Focus();

            CbxTipoContactos.SelectedIndex = 0;

            id = 0;

        }

        private async Task LoadContacto()
        {
            BindingSource source = new BindingSource();
            source.DataSource = await servicioPersona.GetAllViewModel();

            DgvContactos.DataSource = source;
            DgvContactos.ClearSelection();
        }

        private async Task LoadComboBox()
        {
            ComboBoxItem OpcionPorDefecto = new ComboBoxItem
            {
                Text = "Seleccione una opcion",
                Value = null
            };

            CbxTipoContactos.Items.Add(OpcionPorDefecto);

            List<TipoContacto> listaTipo = await servicioTipoContacto.GetAll();

            listaTipo.ForEach(c => {

                ComboBoxItem item = new ComboBoxItem
                {
                    Text = c.Name,
                    Value = c.Id
                };

                CbxTipoContactos.Items.Add(item);

            });

            CbxTipoContactos.SelectedItem = OpcionPorDefecto;
        }





        #endregion

        private async void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            var i = await LongTask.StartLongTask();

            textBox1.Text = "Resultados es " + i;
            button1.Enabled = true;
        }

        private void holaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
