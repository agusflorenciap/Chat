using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Threading;
using System.Net.Sockets;
using System.IO;

namespace Cliente
{    

    public partial class Form1 : Form
    {

        static private NetworkStream stream; 
        static private StreamWriter streamw;
        static private StreamReader streamr;
        static private TcpClient cliente = new TcpClient(); //creo al cliente
        static private string nombre;

        private delegate void DAddItem(String s); //el delegate me sirve para pasar un método como parámetro
        
   
              
        
        


        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e) //es el botón enviar
        {
            streamw.WriteLine(textBox1.Text); //mando al server el mensaje escrito
            streamw.Flush();
            textBox1.Clear();


        }

        private void agregoTextoListB(String s)
        {
            listBox1.Items.Add(s);
        }


        void Escuchar() //función del hilo
        {
            while (cliente.Connected)
            {
                try
                {
                    this.Invoke(new DAddItem(agregoTextoListB), streamr.ReadLine()); //creo al delegado y el parámetro es la función que transfiere
                                                                                     //los datos leídos del socket al listBox (para mostrar el mensaje en sí)
                   
                }
                catch
                {
                    MessageBox.Show("No se ha podido conectar al servidor");
                    Application.Exit();
                }
            }
        }

         void Conectar()
        {
            try
            {
                cliente.Connect("192.168.0.18", 6666); //me conecto al server
                if (cliente.Connected)
                {
                    Thread t = new Thread(Escuchar); //creo al hilo para que se quede verificando el flujo de info (mensajes)

                    stream = cliente.GetStream(); //obtengo el flujo de datos (socket)
                    streamw = new StreamWriter(stream); //para leer
                    streamr = new StreamReader(stream); //para escribir

                    streamw.WriteLine(nombre); //mando el nombre de usuario x socket al server
                    streamw.Flush(); //limpio

                    t.Start(); //que se ejecute el hilo
                }
                else //si hubo problema al conectar
                {
                    MessageBox.Show("Servidor no Disponible");
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Servidor no Disponible");
                Application.Exit();
            }
        }

        private void button2_Click(object sender, EventArgs e) //todo esto es para manejar la visibilidad de la interfaz al presionar el botón "Conectar"
        {
            label1.Visible = false;  
            textBox2.Visible = false;
            button2.Visible = false;
            listBox1.Visible = true;
            textBox1.Visible = true;
            Enviar.Visible = true;

            nombre = textBox2.Text;

            Conectar(); //comienza la conexión

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }





    }
}
