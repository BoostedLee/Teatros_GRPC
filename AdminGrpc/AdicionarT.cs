﻿using Grpc.Core;
using Grpc.Net.Client;
using Sistema_de_reserva_bilhetes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdminGrpc
{
    public partial class AdicionarT : Form
    {
        public AdicionarT()
        {
            InitializeComponent();
        }

        private void btncancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnguardar_Click(object sender, EventArgs e)
        {
            var input = new TeatroVerModelo
            {
                Nome = tbnome.Text,
                MoradaTeatro = tbmorada.Text,
                LocalizacaoTeatro = tblocal.Text,
                Telemovel = Convert.ToInt32(tbtelemovel.Text),
                Telefone = Convert.ToInt32(tbtelefone.Text),
                Email = tbemail.Text
            };
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new AdicionarTeatro.AdicionarTeatroClient(channel);
            var artur = new AdicionarUtilizador.AdicionarUtilizadorClient(channel);


            var reply = await client.GetNewTeatroAsync(input);
            var rep = MessageBox.Show(reply.Feedback, "Adicionar Teatro", MessageBoxButtons.OK);

            if (rep == DialogResult.OK)
            {
                this.Close();
            }

        }
    }
}
