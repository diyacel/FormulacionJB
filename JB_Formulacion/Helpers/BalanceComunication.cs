using AutoMapper;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace RecepciónPesosJamesBrown.Helpers
{
    public class BalanceComunication
    {
        ApplicationDbContext context;
        public static TcpListener tcpListener;
        private Thread listenThread;
        public static Thread clientThread;
        BalanceOptions options;
        ListBox txtLog;
        ProgressBar prgCache;
        public BalanceComunication(ListBox txtLog, ProgressBar prgCache)
        {
            Server();
            context = new ApplicationDbContext();
            var mapperConfig = new MapperConfiguration(m =>
            {
                m.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            options = new BalanceOptions(context, mapper);
            this.txtLog = txtLog;
            this.prgCache=prgCache;
        }
        //ASCIIEncoding encoder
        public async Task DestinarOpcion(string opcion, string[] trama_datos, Encoding encoder, NetworkStream clientStream)
        {
            //ActualizarTextBoxDesdeHilo(trama_datos);
            string respuesta = string.Empty;
            try
            {
                //List<string> datos = new List<string>();
                switch (opcion)
                {
                    //Devuelo numeros de ordenes terminadas pero no recibidas
                    case "QR":
                        respuesta = await options.DevolverTransferenciasTerminadas();
                        Echo(respuesta, encoder, clientStream);
                        break;
                    /*
                     Este método verifica que la contraseña y usuario sean correctos y tengan los permisos de acceso
                    entrada: nombre de usuario y contraseña
                    salida: OK si las credenciales son correctas y tiene accesos, caso contrario el error
                     */
                    case "QU":
                        respuesta = await options.DevolverGruposDirectorioActivo(trama_datos[1], trama_datos[2]);
                        Echo(respuesta, encoder, clientStream);
                        break;
                    //Listar OF liberadas para el pesaje
                    case "QP":
                        respuesta = await options.DevolverOFs();
                        Echo(respuesta, encoder, clientStream);
                        break;
                    /*
                     Este método inicializa toda una orden con sus componentes
                    entrada: número de orden
                    salida: primer componente que no esté pesado de la orden, si el componente tiene un dos lotes
                    devuelve el primer lote
                     */
                    case "QF":
                        respuesta = await options.CargarProductoPorOrden(trama_datos[1]);
                        Echo(respuesta, encoder, clientStream);
                        break;
                    /*
                     Este método calcula la cantidad requerida en base al peso del primer lote 
                    entrada: número de orden, codigo del componente y peso del primer lote
                    salida: segundo lote del mismo componente
                     */
                    case "QFX":
                        respuesta = options.DevolverSegundoLote2(trama_datos[1], trama_datos[2], trama_datos[3],"Orden");
                        Echo(respuesta, encoder, clientStream);
                        break;
                    /*
                     Este método devuelve el siguiente componente de la orden
                    entrada: número de orden y codigo del componente
                    salida: si el siguiente componente por pesar tiene dos lotes, devuelv el primer lote, 
                    si solo tiene un lote devuelve el único lote
                     */
                    case "QFS":
                        respuesta = await options.SaltarComponente2(trama_datos[1], trama_datos[2],"Orden");
                        Echo(respuesta, encoder, clientStream);
                        break;
                    /*
                     Este método envía el componente pesado y guarda dentro de la transferencia de stock
                    entrada: número de orden, codigo del componente y peso del segundo lote si el componente tiene dos lotes
                    si solo tiene un lote, el peso del único lote
                    salida: OK si se realizó el envío del componente, caso contrario el error
                     */

                    case "QFT":
                        respuesta = await options.GuardarComponentePesado2(trama_datos[1], trama_datos[2], trama_datos[3], "Orden");
                        Echo(respuesta, encoder, clientStream);
                        break;
                    /*
                    Este método carga todos los componentes de todas la ordenes que los contienen
                    entrada: codigo del componente
                    salida: primer componente por pesar de la primera orden entregada
                    */
                    case "QC":
                        respuesta = await options.CargarProductoPorCampaña(trama_datos[1]);
                        Echo(respuesta, encoder, clientStream);
                        break;
                    /*
                     Este método calcula la cantidad requerida en base al peso del primer lote 
                    entrada: número de orden, codigo del componente y peso del primer lote
                    salida: segundo lote del mismo componente
                     */
                    case "QCX":
                        respuesta = options.DevolverSegundoLote2(trama_datos[1], trama_datos[2], trama_datos[3], "Orden");
                        Echo(respuesta, encoder, clientStream);
                        break;
                    /*
                     Este método devuelve el siguiente componente de la orden
                    entrada: número de orden y codigo del componente
                    salida: si el siguiente componente por pesar tiene dos lotes, devuelv el primer lote, 
                    si solo tiene un lote devuelve el único lote
                     */
                    case "QCS":
                        respuesta = await options.SaltarComponente2(trama_datos[1], trama_datos[2], "Campaña");
                        Echo(respuesta, encoder, clientStream);
                        break;
                    /*
                     Este método envía el componente pesado y guarda dentro de la transferencia de stock
                    entrada: número de orden, codigo del componente y peso del segundo lote si el componente tiene dos lotes
                    si solo tiene un lote, el peso del único lote
                    salida: OK si se realizó el envío del componente, caso contrario el error
                     */

                    case "QCT":
                        respuesta = await options.GuardarComponentePesado2(trama_datos[1], trama_datos[2], trama_datos[3], "Orden");
                        Echo(respuesta, encoder, clientStream);
                        break;
                    /*
                     Este método extrae todos los números de orden que pertenecen a un producto terminado
                    entrada: código del producto terminado
                    salida: listado de números de orden
                     */

                    case "QA":
                        respuesta = await options.DevolverNumeroOrdenPorPT(trama_datos[1]);
                        Echo(respuesta, encoder, clientStream);
                        break;
                    

                    /*
                     Este método realiza el envpio de la transferencia de stock de todas las transferencias que tienen
                    estado terminado pero no enviado.
                    entrada: 
                    salida: OK si se realizó el envío de la transferencia, caso contraio el error
                     */
                    case "QZT":
                        respuesta = await options.ReenviarTransferenciasTerminadas();
                        Echo(respuesta, encoder, clientStream);
                        break;

                }
            }
            catch (Exception ex) {
                ActualizarTextBoxDesdeHilo2(ex.Message);
                MessageBox.Show(ex.Message);
            }

        }

        private void ListenForClients()
        {
            try
            {
                tcpListener.Start();
                do
                {
                    // bloquea hasta que un cliente se haya conectado al servidor
                    TcpClient client = tcpListener.AcceptTcpClient();
                    // crear un hilo para manejar la comunicación
                    //lblNumberOfConnections.Text = connectedClients.ToString();
                    clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
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
                tcpListener = new TcpListener(IPAddress.Any, 1930);  // Cambiar a IPAddress.Any para la comunicación a través de Internet
                listenThread = new Thread(new ThreadStart(ListenForClients));
                listenThread.Start();
                //ErrorLog("CONEXION EXITOSA", "CORRECTO");
            }
            catch (Exception ex)
            {
                //ErrorLog(ex.Message, "ERROR");
            }
        }
        //ASCIIEncoding encoder
        private void Echo(string msg, Encoding encoder, NetworkStream clientStream)
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

                    break;
                }
                // mensaje ha sido recibido con éxito
                //ASCIIEncoding encoder = new ASCIIEncoding();
                Encoding encoder = Encoding.UTF8;
                // Convertir los bytes recibidos en una cadena y mostrarlos en la pantalla del servidor
                string msg = encoder.GetString(message, 0, bytesRead);
                //WriteMessage(msg);
                // ************************************************************************************
                string[] Consulta_Dato = (msg.Replace(',', ';')).Split(';');
                ActualizarTextBoxDesdeHilo2(msg);
                await DestinarOpcion(Consulta_Dato[0], Consulta_Dato, encoder, clientStream);

            } while (true);
        }
        public static void hiloSegundoPlano()
        {
            if (clientThread is not null && clientThread.IsAlive == true)
                clientThread.IsBackground = true;
            if (tcpListener is not null)
                tcpListener.Stop();
        }

        public void ActualizarTextBoxDesdeHilo(string[] trama_datos)
        {
            string resultado = string.Join(";", trama_datos);
            //resultado = resultado + "\n";

            if (txtLog.InvokeRequired)
            {
                // Si estamos en un hilo diferente al de la interfaz de usuario, invocamos la operación
                txtLog.Invoke(new Action(() => ActualizarTextBoxDesdeHilo(trama_datos)));
            }
            else
            {
                // Estamos en el hilo de la interfaz de usuario, actualizamos el TextBox
                txtLog.Items.Add(resultado);
                //txtLog.AppendText('\n' + "asds");

            }
        }
        public void ActualizarTextBoxDesdeHilo2(string trama_datos)
        {

            if (txtLog.InvokeRequired)
            {
                // Si estamos en un subproceso distinto al hilo de interfaz de usuario, usamos Invoke o BeginInvoke
                txtLog.Invoke(new Action(() => txtLog.Items.Add(trama_datos)));
                // También puedes usar BeginInvoke para ejecutar el código de forma asincrónica
                // txtLog.BeginInvoke(new Action(() => txtLog.Text = "Texto de ejemplo"));
            }
            else
            {
                // Si ya estamos en el hilo de interfaz de usuario, podemos acceder directamente al control
                txtLog.Items.Add(trama_datos);
            }

            
                //txtLog.AppendText('\n' + "asds");

            
        }


    }
}
