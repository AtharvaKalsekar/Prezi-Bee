using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO.Ports;
using System.Windows.Forms;
using System.Runtime;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows;
using Windows.Devices.Bluetooth.Rfcomm;
using Windows.Networking.Sockets;
using Windows.Devices.Enumeration;
using Windows.Devices.Enumeration.Pnp;
using Windows.Storage.Streams;


namespace myproject
   

{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
           // InitializeComponent();
        }
        private DeviceInformationCollection rfcommServiceInfoCollection;

        private StreamSocket streamSocket;

        private RfcommDeviceService rfcommDeviceService;



        /*SerialPort MyPort = new SerialPort("COM5", 9600);//Adjust the comp port
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            String tx = TXWINDOW.Text;


            MyPort.Write(tx);
            TXWINDOW.Clear();
 
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MyPort.IsOpen) MyPort.Close();
        }
        String RxString;
        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        { //  void delegate DisplayText();
            RxString = MyPort.ReadExisting();
           this.Invoke(new EventHandler(DisplayText));
          // DisplayText();
          
        }

        private void Invoke(EventHandler eventHandler)
        {
            throw new NotImplementedException();
        }
        private  void DisplayText(object sender, EventArgs e)
        {
            RXWINDOW.AppendText(RxString);


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           // SerialPort MyPort = new SerialPort("COM1", 9600);//Adjust the comp port
            MyPort.Open();
            if (!MyPort.IsOpen)
                return;
            Connect.IsEnabled = false;

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (MyPort.IsOpen)
                MyPort.Close();
            Connect.IsEnabled = true;
        }*/

        private async void  Button_Click_3(object sender, RoutedEventArgs e)
        {rfcommServiceInfoCollection = await DeviceInformation.FindAllAsync(
                RfcommDeviceService.GetDeviceSelector(RfcommServiceId.ObexObjectPush));

            var count = rfcommServiceInfoCollection.Count;

            Debug.WriteLine("Count of RFCOMM Service: " + count);

            if(count > 0)
            {
                lock (this)
                {
                    streamSocket = new StreamSocket();
                }

                var defaultSvcInfo = rfcommServiceInfoCollection.FirstOrDefault();

                rfcommDeviceService = await RfcommDeviceService.FromIdAsync(defaultSvcInfo.Id);

                if(rfcommDeviceService == null)
                {
                    Debug.WriteLine("Rfcomm Device Service is NULL, ID = {0}", defaultSvcInfo.Id);

                    return;
                }

                Debug.WriteLine("ConnectionHostName: {0}, ConnectionServiceName: {1}", rfcommDeviceService.ConnectionHostName, rfcommDeviceService.ConnectionServiceName);

                await streamSocket.ConnectAsync(rfcommDeviceService.ConnectionHostName, rfcommDeviceService.ConnectionServiceName);

                dataWriter = new DataWriter(streamSocket.OutputStream);

                connectButton.Visibility = Visibility.Collapsed;
            }
        }
          
          
        

       /* private void Button_Click(object sender, RoutedEventArgs e)
        {

            SerialPort MyPort = new SerialPort("COM1",9600);//Adjust the comp port
            String[] portnames = SerialPort.GetPortNames();//get the port names
            try
            {
                if (MyPort.IsOpen == false)
                    
                    MyPort.Open();//opening the port
            }
            catch (System.ArgumentException)
            {
                Console.Write("Port not reachable");
            }
            



        }
//displaying of the ports in the textbox
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SerialPort import = new SerialPort();
            String[] portnames = SerialPort.GetPortNames();
            foreach(string port in SerialPort.GetPortNames())
            { portsdisplay.Text += port; }

        }*/
    }
}
