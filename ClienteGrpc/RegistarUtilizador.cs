﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Grpc.Net.Client;
using Sistema_de_reserva_bilhetes;

namespace ClienteGrpc
{
    public partial class RegistarUtilizador : Form
    { 
       // fazer a striong para ligar a base dados
       // string connectionString = @"Data Source = (local)\sql....;";

        public RegistarUtilizador()
        {
            InitializeComponent();
        }

        private void btncancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

     
     

        private void btnadicionar_Click(object sender, EventArgs e)
        {
            //Neste botão, apos a introdução dos dados do utilizador, é feita a comunicação com o Servidor
            // São enviados os dados pelo canal para serem gravadod na base de dados

            var channel = GrpcChannel.ForAddress("https://localhost:5001");

            var utiClient = new AdicionarUtilizador.AdicionarUtilizadorClient(channel);

            // vai ser atribuida as variaveis do proto os valores das textboxes preenchidas, guardadas numa variavel do tipo AddUtiLookupModel
            var clienteRequest = new UtilizadorVerModelo
            {
                Username = txbusername.Text,
                Pass = txbpassword.Text,
                Nome = txbnome.Text,
                Nif = txbnif.Text,
                Idade = Convert.ToInt32(txbidade.Text),
                Localidade = txblocalidade.Text

            };
            // É chamado o metodo do proto para comunicar com o servidor, enviar os dados e receber a resposta na variavel feedback
            var feedback = utiClient.GetUtilizadorInfo(clienteRequest);
            var message = MessageBox.Show(feedback.Feedback, "Adicionar Utilizador", MessageBoxButtons.OK);
            if (message == DialogResult.OK)
            {
                this.Close();
            }

        }

        void Clear()
        {
            txbnome.Text = txbidade.Text = txbusername.Text = txbpassword.Text = txbnif.Text = txblocalidade.Text = "";

        }
    }
}
