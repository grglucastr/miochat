using Microsoft.AspNet.SignalR.Client;
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

namespace MioClient
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {

        HubConnection hubConnection;
        IHubProxy myProxy;

        public MainWindow()
        {
            InitializeComponent();

            hubConnection = new HubConnection("http://localhost:8001/signalr");
            myProxy = hubConnection.CreateHubProxy("MyHub");

            myProxy.On("notify", () => 
                Dispatcher.InvokeAsync(() => 
                {
                    ServerMessage.Text += "Notified! \n";
                })
            );

            myProxy.On<string>("UpdateSimpleString", (str) => 
                Dispatcher.Invoke(() => 
                {
                    ServerMessage.Text += string.Format("Server says: {0}\n", str);
                })
            );

            myProxy.On<string>("getMessage", (str) => Dispatcher.Invoke(() =>
            {
                ServerMessage.Text = str;
            }));

            hubConnection.Start();
        }

        private void UserMessage_DragEnter(object sender, DragEventArgs e)
        {
            MessageBox.Show($"Message typed: {this.UserMessage.Text}");
        }

        private void UserMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == System.Windows.Input.Key.Enter)
            {
                myProxy.Invoke<string>("sendMessage", this.UserMessage.Text);
                this.UserMessage.Text = "";
            }
        }
    }
}
