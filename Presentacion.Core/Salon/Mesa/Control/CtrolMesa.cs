﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XCommerce.AccesoDatos;
using Presentacion.Core.VentaSalon;
using XCommerce.Servicios.Core.Comprobante;
using Presentacion.Helpers;

namespace Presentacion.Core.Salon.Mesa.Control
{
    public partial class CtrolMesa : UserControl
    {
        private long _mesaID;
       

        public long mesaId
        {
            set
            {
                _mesaID = value;
            }
        }

   

        private int _numeroMesa;
        public int NumeroMesa
        {          
            set
            {
                _numeroMesa = value;
                lblNumeroMesa.Text = value.ToString();
            }                     
        }


        public decimal PrecioCosumido
        {
            set
            {
                lblPrecio.Text = value.ToString("C");
            }
        }
        private EstadoMesa estadoMesa;
        

        
        public EstadoMesa Estado
        {            
            set
            {
                menuAbrirMesa.Visible = false;
                menuCerrarMesa.Visible = false;
                estadoMesa = value;
                switch (value)
                {
                    case EstadoMesa.Abierta:
                        this.BackColor = Color.Green;
                        menuCerrarMesa.Visible = true;
                        break;
                    case EstadoMesa.Cerrada:
                        this.BackColor = Color.Red;
                        menuAbrirMesa.Visible = true;
                        break;
                    case EstadoMesa.FueraServicio:
                        this.BackColor = Color.Yellow;
                        break;
                    default:
                        this.BackColor = Color.White;
                        break;
                }
            }
        }

        private readonly IComprobanteSalonServicio _comprobanteSalonServicio;

        public CtrolMesa() : this(new ComprobanteSalonServicio())
        {
            InitializeComponent();
        }

        public CtrolMesa(IComprobanteSalonServicio comprobanteSalonServicio)
        {
            _comprobanteSalonServicio = comprobanteSalonServicio;
        }

        private void menuAbrirMesa_Click(object sender, EventArgs e)
        {
            if (estadoMesa == EstadoMesa.Abierta) return;


            _comprobanteSalonServicio.GenerarComprobanteSalon(_mesaID, DatosSistema.UsuarioId, 1);
            Estado = EstadoMesa.Abierta;


            var fComprobanteMesa = new FormularioComprobanteMesa(_mesaID,_numeroMesa);
            
            fComprobanteMesa.ShowDialog();
            
            
        }

        private void lblNumeroMesa_DoubleClick(object sender, EventArgs e)
        {
            if (estadoMesa != EstadoMesa.Abierta) return;

            var fComprobanteMesa = new FormularioComprobanteMesa(_mesaID,_numeroMesa);
          
            fComprobanteMesa.ShowDialog();
        }
    }
}
