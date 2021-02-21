using System;
using System.Net.Http;
using System.Windows;

using static TuyaDevelopment.DeviceInfoProp;

namespace TuyaDevelopment
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            InitializeComponent();

            Hide();

            contextMenu1 = new System.Windows.Forms.ContextMenu();
            menuItem3 = new System.Windows.Forms.MenuItem();
            menuItem4 = new System.Windows.Forms.MenuItem();
            contextMenu1.MenuItems.AddRange(
                new System.Windows.Forms.MenuItem[] { menuItem3, menuItem4 });

           

            menuItem3.Index = 2;
            menuItem3.Text = "Exit";
            menuItem3.Click += new System.EventHandler(this.menuItem2_Click);

            menuItem4.Index = 3;
            menuItem4.Text = "Wifi Relay";
            menuItem4.Click += new System.EventHandler(this.menuItem4_Click);

            m_notifyIcon = new System.Windows.Forms.NotifyIcon();
            m_notifyIcon.BalloonTipText = "Your Text";
            m_notifyIcon.BalloonTipTitle = "Your Title";
            m_notifyIcon.Text = "The Wifi Relay";
            m_notifyIcon.Icon = new System.Drawing.Icon(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\bulb_off.ico");
            m_notifyIcon.ContextMenu = contextMenu1;


            if (m_notifyIcon != null)
                m_notifyIcon.Visible = true;

            m_notifyIcon.ShowBalloonTip(1000);

        }

        private void menuItem4_Click(object sender, EventArgs e)
        {
           KapiOtomatik();
        }

        private async void KapiOtomatik()
        {

            var baseAddress = new Uri("http://blynk-cloud.com/");

            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {

                using (var response = await httpClient.GetAsync(DeviceInfoProp.blynk_auth+"/update/D0?value=0"))
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                }

                System.Threading.Thread.Sleep(100);

                using (var response = await httpClient.GetAsync(DeviceInfoProp.blynk_auth+"/update/D0?value=1"))
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                }

            }

        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            m_notifyIcon.Dispose();
            m_notifyIcon = null;

            Window.GetWindow(this).Close();
        }


    }
}
