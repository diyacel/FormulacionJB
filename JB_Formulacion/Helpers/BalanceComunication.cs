using JB_Formulacion.Controllers;
using JB_Formulacion.Helpers;
using JB_Formulacion.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace JB_Formulacion.Helper
{
    public class BalanceComunication
    {
        private TcpListener tcpListener;
        private Thread listenThread;
        private int connectedClients = 0;
        BalanceOptions options;
        public  BalanceComunication()
        {
            Server();
            options = new BalanceOptions();
        }
        public async Task DestinarOpcion(string opcion, string[] trama_datos, ASCIIEncoding encoder, NetworkStream clientStream)
        {
            string respuesta = string.Empty;
            //List<string> datos = new List<string>();
            switch (opcion)
            {
                //Reenvío de ordenes pesadas pero no recibidas
                case "QR":
                    await ListarOrdenesPesadasNoRecibidas(trama_datos,encoder,clientStream);
                break;
                //Acceder usuario
                //usuario, contraseña
                case "QU":
                    respuesta = await options.DevolverGruposDirectorioActivo(trama_datos[1], trama_datos[2]);
                    Echo(respuesta,encoder,clientStream);
                break;
                //Listar OF liberadas para el pesaje
                case "QP":
                    respuesta = await options.DevolverOFs();
                    Echo(respuesta, encoder, clientStream);
                    break;
                //Contar número de materias primas de la orden
                //usuario, contraseña, número de orden de fabricación
                case "QF":
                    respuesta = await options.DevolverCantidadMPsPorOF(trama_datos[3]);
                    await options.CargarMPsPorOF(trama_datos[3]);
                    Echo(respuesta, encoder, clientStream);
                    break;

                case "QI":
                    respuesta = options.DevolverMPActual(trama_datos[1], trama_datos[2]);
                    Echo(respuesta, encoder, clientStream);
                    break;

                case "QT":

                break;

                case "QZ":

                break;
                //Enviar ordenes pesadas
                case "QZT":

                break;



            }
        }

        public async Task ListarOrdenesPesadasNoRecibidas(string[] trama_datos, ASCIIEncoding encoder, NetworkStream clientStream)
        {
            string respuesta = "OK;";
            Echo(respuesta, encoder, clientStream);
        }
       


        public async Task pruebaAsync(int num)
        {
            List<string> addresses = new List<string>();
            string respuesta=string.Empty;
            //addresses = options.DevolverOFs().GetAwaiter().GetResult();
            switch(num)
            {
                case 1:
                    respuesta = await options.DevolverOFs();
                    break;
                case 2:
                    addresses = await options.DevolverOFsPorArticulo("45100052");
                    break;
                case 3:
                    //addresses = await options.DevolverCantidadMPsPorOF("10009132");
                    break;
                case 4:
                    await options.DevolverGruposDirectorioActivo("prueba", "prueba");
                    break;
                case 5:
                    await options.DevolverCantidadMPsPorOF("3001391");
                    break;
                
            }
            foreach (string address in addresses)
            {
                Console.WriteLine(address);
            }
            
        }
        private void ListenForClients()
        {
            try
            {
                this.tcpListener.Start();
                do
                {
                    // bloquea hasta que un cliente se haya conectado al servidor
                    TcpClient client = this.tcpListener.AcceptTcpClient();
                    // crear un hilo para manejar la comunicación
                    // con cliente conectado
                    connectedClients += 1;
                    //lblNumberOfConnections.Text = connectedClients.ToString();
                    Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
                    clientThread.Start(client);
                }
                while (true);// Incrementar el número de clientes que se han comunicado con nosotros
            }
            catch (Exception ex)
            {
                // ErrorLog(ex.Message, "ERROR");
            }
        }

        private void Server()
        {
            try
            {
                this.tcpListener = new TcpListener(IPAddress.Any, 1930);  // Cambiar a IPAddress.Any para la comunicación a través de Internet
                this.listenThread = new Thread(new ThreadStart(ListenForClients));
                this.listenThread.Start();
                //ErrorLog("CONEXION EXITOSA", "CORRECTO");
            }
            catch (Exception ex)
            {
                //ErrorLog(ex.Message, "ERROR");
            }
        }

        private void Echo(string msg, ASCIIEncoding encoder, NetworkStream clientStream)
        {
            try
            {
                // Now Echo the message back
                byte[] buffer = encoder.GetBytes(msg);

                clientStream.Write(buffer, 0, buffer.Length);
                clientStream.Flush();
            }
            catch (Exception ex)
            {
                //ErrorLog(ex.Message, "ERROR");
            }
        }

        private async void HandleClientComm(object client)
        {
            TcpClient tcpClient = (TcpClient)client;
            NetworkStream clientStream = tcpClient.GetStream();
            byte[] message = new byte[4096];
            int bytesRead;
            do
            {
                bytesRead = 0;
                try
                {
                    //bloquea hasta que un cliente envía un mensaje
                    bytesRead = clientStream.Read(message, 0, 4096);
                }
                catch
                {
                    //ha ocurrido un error de socket
                    break;
                }
                if (bytesRead == 0)
                {
                    // el cliente se ha desconectado del servidor
                    connectedClients -= 1;
                    //  lblNumberOfConnections.Text = connectedClients.ToString();
                    break;
                }
                // mensaje ha sido recibido con éxito
                ASCIIEncoding encoder = new ASCIIEncoding();
                // Convertir los bytes recibidos en una cadena y mostrarlos en la pantalla del servidor
                string msg = encoder.GetString(message, 0, bytesRead);
                //WriteMessage(msg);
                // ************************************************************************************
                string[] Consulta_Dato = (msg.Replace(',', ';')).Split(';');
                await DestinarOpcion(Consulta_Dato[0], Consulta_Dato, encoder,clientStream);

            } while (true);
        }
    }
}
