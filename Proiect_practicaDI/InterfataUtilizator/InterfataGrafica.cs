using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InterfataUtilizator
{
    public partial class InterfataGrafica : Form
    {
        public Size originalSize;
        public InterfataGrafica()
        {
            this.Text = "Interfață Grafică";
            InitializeComponent();
            string macAddress = GetMacAddress();
            // Setează textul pentru Label
            lblMAC.Text = macAddress;
        }
        public static string GetMacAddress()
        {
            /*Obtine lista de placi de retea*/
            var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            /*Cautam prima placa de retea activa si obtinem adresa MAC*/
            foreach (var networkInterface in networkInterfaces)
            {
                /*Verifica daca placa de retea nu este de tip Loopback si este activs*/
                if (networkInterface.NetworkInterfaceType != NetworkInterfaceType.Loopback &&
                    networkInterface.OperationalStatus == OperationalStatus.Up)
                {
                    var macAddress = networkInterface.GetPhysicalAddress();
                    if (macAddress != null)
                    {
                        /*Formateaza adresa MAC intr-un sir de caractere hexazecimale*/
                        return string.Join("::", macAddress.GetAddressBytes().Select(b => b.ToString("X2")));
                    }
                }
            }
            return "Adresa MAC nu a putut fi gasita";
        }
        private void btnCitire_MouseEnter(object sender, EventArgs e)
        {
            originalSize = btnCitire.Size;
            btnCitire.Size = new Size(300, 75);
        }

        private void btnCitire_MouseLeave(object sender, EventArgs e)
        {
            btnCitire.Size = originalSize;
        }
    }

}
