using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO.Ports;

namespace CapacitiveTouchSensingKitService
{
    public partial class Form1 : Form
    {
        [DllImport("User32.dll")]
        static extern int SetForegroundWindow(IntPtr point);
        SerialPort serialPrt;

        public Form1()
        {
            InitializeComponent();
            serialPrt = new SerialPort();
            string[] allSerialPorts = SerialPort.GetPortNames();
            comboBox7.DataSource = allSerialPorts;
            serialPrt.BaudRate = 9600;
            serialPrt.PortName = "COM4";
            serialPrt.Open();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process p = Process.GetProcessesByName("Scratch 2").FirstOrDefault();
            if (p != null)
            {
                IntPtr h = p.MainWindowHandle;
                SetForegroundWindow(h);
                while (true)
                {
                    string arduinoData = serialPrt.ReadLine();
                    //SendKeys.SendWait("{RIGHT}");
                    SendKeys.SendWait(arduinoData);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
