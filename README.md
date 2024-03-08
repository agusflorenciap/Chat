<h1>Chat Grupal</h1>

<h2>Descripción</h2>
El proyecto consiste en realizar un chat entre dos procesos utilizando sockets. El objetivo es que los procesos sean ejecutados en un entorno distribuido. La infraestructura utilizada fue una Notebook con Windows 10 (servidor), una PC de escritorio con Windows 10 (cliente) y una máquina virtual con Windows 7 (2do cliente). Todos los clientes se conectan al servidor y pueden interactuar entre sí en un entorno compartido (chat grupal). El servidor es una aplicación de consola, mientras que el cliente tiene un pequeño entorno gráfico para hacerlo más atractivo. Los mensajes enviados figuran en la consola del servidor, así mismo se muestran notificaciones para indicar que se ha conectado un usuario al chat. El servidor da como finalizada la conexión cuando el cliente se retira de la aplicación. Para implementarlo se utilizaron las bibliotecas Threading y Net.Sockets.
<h2>Lenguaje y entorno</h2>

- <b>C# en Visual Studio 2019.</b> 
