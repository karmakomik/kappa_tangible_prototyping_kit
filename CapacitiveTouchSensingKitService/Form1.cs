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
using System.Threading;

namespace CapacitiveTouchSensingKitService
{
    public partial class Form1 : Form
    {
        [DllImport("User32.dll")]
        static extern int SetForegroundWindow(IntPtr point);
        SerialPort serialPrt;
        string selectedPort = "";
        bool isSerialReadActive = false;
        Thread readThread;

        public Form1()
        {
            InitializeComponent();
            updateSerialPorts();
            /*string[] allSerialPorts_array = SerialPort.GetPortNames();
            List<string> allSerialPorts = allSerialPorts_array.ToList<string>();
            allSerialPorts.Insert(0, "----------");
            comboBox7.DataSource = allSerialPorts;*/
            /*trackBar1.SetRange(400, 10000);
            trackBar2.SetRange(400, 10000);
            trackBar3.SetRange(400, 10000);
            trackBar4.SetRange(400, 10000);
            trackBar5.SetRange(400, 10000);
            trackBar6.SetRange(400, 10000);*/

            //checkIfArduinoConnected();
            
            
        }

        void updateSerialPorts()
        {
            string[] allSerialPorts_array = SerialPort.GetPortNames();
            List<string> allSerialPorts = allSerialPorts_array.ToList<string>();
            allSerialPorts.Insert(0, "----------");
            comboBox7.DataSource = allSerialPorts;
        }

        /*void checkIfArduinoConnected()
        {
            serialPrt = new SerialPort();
            serialPrt.BaudRate = 9600;
            string[] allSerialPorts = SerialPort.GetPortNames();
            foreach (string s in allSerialPorts)
            {
                serialPrt.PortName = s;
                try
                {
                    serialPrt.Open();
                    selectedPort = s;
                    Debug.WriteLine("COM port " + s + " selected");
                    break;
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }                
            }
            if (selectedPort == "")
            {
                Debug.WriteLine("No COM port found");
            }
            else
            {
                tableLayoutPanel1.Visible = true;
                pictureBox1.BackColor = Color.SkyBlue;
                label11.Visible = true;
                label10.Visible = false;
            }
        }*/

        void checkIfPortValidAndConnect(string port)
        {
            serialPrt = new SerialPort();
            serialPrt.BaudRate = 115200;
            serialPrt.PortName = port;
            selectedPort = "";
            try
            {
                serialPrt.Open();
                selectedPort = port;
                //serialPrt.DataReceived += new SerialDataReceivedEventHandler(sprt_DataReceived);
                Debug.WriteLine("Port " + port + " selected");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            if (selectedPort == "")
            {
                Debug.WriteLine("No COM port found");
                tableLayoutPanel1.Visible = false;
                pictureBox1.BackColor = Color.IndianRed;
                label11.Visible = false;
                label10.Visible = true;
            }
            else
            {
                tableLayoutPanel1.Visible = true;
                pictureBox1.BackColor = Color.SkyBlue;
                label11.Visible = true;
                label10.Visible = false;
                readThread = new Thread(readSerial);
                readThread.Start();
                //passKeyEvents();
            }
        }

        void passKeyEvents()
        {
            /*Process p = Process.GetProcessesByName("Scratch 2").FirstOrDefault();
            if (p != null)
            {
                IntPtr h = p.MainWindowHandle;
                SetForegroundWindow(h);
            }*/
            Debug.WriteLine("Passing key values");
            /*while (isSerialReadActive)
            {
                //SetForegroundWindow(h);
                string arduinoData = serialPrt.ReadLine();
                Debug.WriteLine("arduinoData : " + arduinoData);
                //SendKeys.SendWait("{RIGHT}");
                //SendKeys.SendWait(arduinoData);
            }*/
        }

        void readSerial()
        {
            while (isSerialReadActive)
            {
                if (serialPrt.IsOpen)
                {
                    try
                    {
                        string indata = serialPrt.ReadLine();
                        //Debug.WriteLine("indata" + indata);
                        int incomingVal = 0;
                        if (int.TryParse(indata, out incomingVal))
                        {
                            SendKeys.SendWait("" + incomingVal);
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e);
                    }
                }
            }
        }

        /*private void sprt_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string indata = serialPrt.ReadLine();
            //Debug.WriteLine("indata" + indata);
            int incomingVal = 0;
            if (int.TryParse(indata, out incomingVal))
            {
                SendKeys.SendWait(""+incomingVal);
            }
        }*/

        void stopArduinoRead(object sender, EventArgs e)
        {
            //readThread.Abort();
            serialPrt.Close();
            //Debug.WriteLine("stopArduinoRead");
            isSerialReadActive = false;

        }

        void onSelectSerialPort(object sender, EventArgs e)
        {
            if (selectedPort != "")
            {
                serialPrt.Close();
            }
            isSerialReadActive = true;
            Debug.WriteLine(comboBox7.SelectedValue.ToString());
            checkIfPortValidAndConnect(comboBox7.SelectedValue.ToString());
        }

        void connector1SensitivityValueChanged(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(trackBar1, "" + trackBar1.Value);
            //Debug.WriteLine(trackBar1.Value);
            //serialPrt.WriteLine("sdsds");
            serialPrt.WriteLine("<1, " + trackBar1.Value + ">");
            //serialPrt.WriteLine("<HelloWorld, 12, 24.7>");
        }

        void connector2SensitivityValueChanged(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(trackBar2, "" + trackBar1.Value);
            //Debug.WriteLine(trackBar2.Value);
            //serialPrt.WriteLine(""+trackBar2.Value);
            serialPrt.WriteLine("<2, " + trackBar2.Value + ">");
        }

        void connector3SensitivityValueChanged(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(trackBar3, "" + trackBar1.Value);
            //Debug.WriteLine(trackBar3.Value);
            //serialPrt.WriteLine(""+trackBar3.Value);
            serialPrt.WriteLine("<3, " + trackBar3.Value + ">");
        }

        void connector4SensitivityValueChanged(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(trackBar4, "" + trackBar1.Value);
            //Debug.WriteLine(trackBar4.Value);
            //serialPrt.WriteLine(""+trackBar4.Value);
            serialPrt.WriteLine("<4, " + trackBar4.Value + ">");
        }

        void connector5SensitivityValueChanged(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(trackBar5, "" + trackBar1.Value);
            //Debug.WriteLine(trackBar5.Value);
            //serialPrt.WriteLine(""+trackBar5.Value);
            serialPrt.WriteLine("<5, " + trackBar5.Value + ">");
        }

        void connector6SensitivityValueChanged(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(trackBar6, "" + trackBar1.Value);
            //Debug.WriteLine(trackBar6.Value);
            //serialPrt.WriteLine(""+trackBar6.Value);
            serialPrt.WriteLine("<6, " + trackBar6.Value + ">");
        }

        private void refreshPorts(object sender, EventArgs e)
        {
            updateSerialPorts();
        }
    }
}
