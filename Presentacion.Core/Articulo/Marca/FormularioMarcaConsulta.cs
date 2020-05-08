﻿using Presentacion.FormulariosBase;
using Presentacion.FormulariosBase.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XCommerce.Servicios.Core.Articulo.Marca;

namespace Presentacion.Core.Articulo.Marca
{
    public partial class FormularioMarcaConsulta : FormularioBaseConsulta
    {
        private readonly IMarcaServicio _marcaServicio;

        public FormularioMarcaConsulta() : this(new MarcaServicio())
        {
            InitializeComponent();
        }

        public FormularioMarcaConsulta(IMarcaServicio marcaServicio)
        {
            _marcaServicio = marcaServicio;
        }

        public override void ResetearGrilla(DataGridView grilla)
        {
            base.ResetearGrilla(grilla);


            grilla.Columns["Descripcion"].Visible = true;
            grilla.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["Descripcion"].HeaderText = "Marca";

        }

        public override void ActualizarDatos(DataGridView grilla, string cadenaBuscar, CheckBox check, ToolStrip toolStrip)
        {

            base.ActualizarDatos(grilla, cadenaBuscar, check, toolStrip);

            if (check.Checked)
            {
                grilla.DataSource = _marcaServicio.ObtenerMarcaEliminada(cadenaBuscar);
                toolStrip.Enabled = false;
            }
            else
            {
                grilla.DataSource = _marcaServicio.ObtenerMarca(cadenaBuscar);
                toolStrip.Enabled = true;
            }
        }

        public override void EjecutarBtnEliminar()
        {
            base.EjecutarBtnEliminar();
            if (puedeEjecutarComando)
            {
                var FormularioABMMarca = new FormularioMarcaABM(TipoOperacion.Eliminar, entidadId);
                FormularioABMMarca.ShowDialog();
                ActualizarSegunOperacion(FormularioABMMarca.RealizoAlgunaOperacion);
            }
        }

        private void ActualizarSegunOperacion(bool realizoOperacion)
        {
            if (realizoOperacion)
            {
                ActualizarDatos(dgvGrilla, string.Empty, cbxEstaEliminado, BarraLateralBotones);
            }
        }

        public override void EjecutarBtnModificar()
        {
            base.EjecutarBtnModificar();
            if (puedeEjecutarComando)
            {
                var FormularioABMMarca = new FormularioMarcaABM(TipoOperacion.Modificar, entidadId);
                FormularioABMMarca.ShowDialog();
                ActualizarSegunOperacion(FormularioABMMarca.RealizoAlgunaOperacion);
            }
        }

        public override void EjecutarBtnNuevo()
        {
            var FormularioABMMarca = new FormularioMarcaABM(TipoOperacion.Nuevo);
            FormularioABMMarca.ShowDialog();
            ActualizarSegunOperacion(FormularioABMMarca.RealizoAlgunaOperacion);
        }
    }
}
