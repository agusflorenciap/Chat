using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading; //biblioteca para uso de hilos
using System.Net.Sockets; //biblioteca para uso de sockets
using System.IO;
using System.Net;

namespace Servidor
{
    class Servidor_Chat
    {

        private TcpListener server; //es el server, que esperará la conexión del Cliente
        private TcpClient cliente = new TcpClient(); //el TcpClient me da la conexión entre el Server y el Cliente
        private IPEndPoint datosServer = new IPEndPoint(IPAddress.Parse("192.168.0.16"), 6666); //ip y puerto del Server. 
        private List<Connection> lista = new List<Connection>(); //lista que me guardará las conexiones

        Connection con; //variable de tipo Connection (será la conexión propiamente dicha)


        private struct Connection 
        {
            public NetworkStream stream; //me permite manejar el flujo de información (el envio de mensajes a través de los sockets)
            public StreamWriter streamw; //escribir
            public StreamReader streamr; //leer
            public string nick; //lo que será el nombre/usuario del Cliente que se conecta
        }

        public Servidor_Chat()
        {
            Inicio();
        }

        public void Inicio()
        {

            Console.WriteLine("Servidor de Chat Agus encendido!");
            server = new TcpListener(datosServer); //creo al server y lo pongo en escucha (esperando que se conecte algún cliente)
            server.Start();

            while (true) //ciclo while infinito para quedarme esperando clientes
            {
                cliente = server.AcceptTcpClient(); //acepto conexión de cliente

                con = new Connection(); //creo la conexión
                con.stream = cliente.GetStream(); //recibo el flujo de datos del cliente
                con.streamr = new StreamReader(con.stream); //para leer el flujo de datos 
                con.streamw = new StreamWriter(con.stream); //para escribir en ese flujo de datos

                con.nick = con.streamr.ReadLine(); //nick (nombre) del cliente

                lista.Add(con); //
                Console.WriteLine(con.nick + " se ha conectado.");



                Thread t = new Thread(Escuchar_conexion); //creo un hilo para verificar en paralelo el flujo de información (los mensajes que se envien)

                t.Start();
            }


        }

        void Escuchar_conexion()
        {
            Connection hcon = con;  //copio la conexión que ya tenía, así puedo reutilizar la variable "con" para una nueva posterior conexión

            do //do while infinito
            {
                try
                {
                    string tmp = hcon.streamr.ReadLine(); //leo los caracteres que mandó el cliente
                    Console.WriteLine(hcon.nick + ": " + tmp); //muestro el mensaje (nombre del usuario y lo que escribió)
                    foreach (Connection c in lista) //recorro la lista de conexiones que tengo y si hay más mensajes los muestra. Luego limpia  
                    {
                        try
                        {
                            c.streamw.WriteLine(hcon.nick + ": " + tmp);
                            c.streamw.Flush();
                        }
                        catch
                        {
                        }
                    }
                }
                catch //y si hubo algún problema al verificar los mensajes del cliente
                {
                    lista.Remove(hcon); //remueve la conexión
                    Console.WriteLine(con.nick + " se ha desconectado.");
                    break;
                }
            } while (true);
        }

    }
}
