using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
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
        string comboBoxSelectedText1, comboBoxSelectedText2, comboBoxSelectedText3, comboBoxSelectedText4, comboBoxSelectedText5, comboBoxSelectedText6;

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
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 1;
            comboBox4.SelectedIndex = 2;
            comboBox5.SelectedIndex = 3;
            comboBox6.SelectedIndex = 4;
            comboBox3.SelectedIndex = 5;
            updateComboBoxSelectedVals();
            comboBox1.SelectedIndexChanged += new System.EventHandler(updateComboBoxSelectedVals_event);
            comboBox2.SelectedIndexChanged += new System.EventHandler(updateComboBoxSelectedVals_event);
            comboBox3.SelectedIndexChanged += new System.EventHandler(updateComboBoxSelectedVals_event);
            comboBox4.SelectedIndexChanged += new System.EventHandler(updateComboBoxSelectedVals_event);
            comboBox5.SelectedIndexChanged += new System.EventHandler(updateComboBoxSelectedVals_event);
            comboBox6.SelectedIndexChanged += new System.EventHandler(updateComboBoxSelectedVals_event);            
        }

        void updateComboBoxSelectedVals()
        {
            comboBoxSelectedText1 = comboBox1.SelectedItem.ToString();
            comboBoxSelectedText2 = comboBox2.SelectedItem.ToString();
            comboBoxSelectedText3 = comboBox3.SelectedItem.ToString();
            comboBoxSelectedText4 = comboBox4.SelectedItem.ToString();
            comboBoxSelectedText5 = comboBox5.SelectedItem.ToString();
            comboBoxSelectedText6 = comboBox6.SelectedItem.ToString();

            if (comboBoxSelectedText1 == "{SPACE}") comboBoxSelectedText1 = " ";
            if (comboBoxSelectedText2 == "{SPACE}") comboBoxSelectedText2 = " ";
            if (comboBoxSelectedText3 == "{SPACE}") comboBoxSelectedText3 = " ";
            if (comboBoxSelectedText4 == "{SPACE}") comboBoxSelectedText4 = " ";
            if (comboBoxSelectedText5 == "{SPACE}") comboBoxSelectedText5 = " ";
            if (comboBoxSelectedText6 == "{SPACE}") comboBoxSelectedText6 = " ";
        }

        void updateComboBoxSelectedVals_event(object sender, EventArgs e)
        {
            bringScratchToForeground();
            updateComboBoxSelectedVals();
        }

        void updateSerialPorts()
        {
            string[] allSerialPorts_array = SerialPort.GetPortNames();
            List<string> allSerialPorts = new List<string>(allSerialPorts_array); //allSerialPorts_array.  ToList<string>();
            allSerialPorts.Insert(0, "----------");
            comboBox7.DataSource = allSerialPorts;
        }

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
                bringScratchToForeground();
                //passKeyEvents();
            }
        }

        void bringScratchToForeground()
        {
            try
            {
                Process[] pArr = Process.GetProcessesByName("Scratch 2");
                Process p;
                //p = Process.GetProcessesByName("Scratch 2").FirstOrDefault();

                p = pArr[0];

                if (p != null)
                {
                IntPtr h = p.MainWindowHandle;
                SetForegroundWindow(h);
                }
            }
            catch (Exception e)
            {

            }
        }

        /*void passKeyEvents()
        {
            Process p = Process.GetProcessesByName("Scratch 2").FirstOrDefault();
            if (p != null)
            {
                IntPtr h = p.MainWindowHandle;
                SetForegroundWindow(h);
            }
            Debug.WriteLine("Passing key values");
            while (isSerialReadActive)
            {
                //SetForegroundWindow(h);
                string arduinoData = serialPrt.ReadLine();
                Debug.WriteLine("arduinoData : " + arduinoData);
                //SendKeys.SendWait("{RIGHT}");
                //SendKeys.SendWait(arduinoData);
            }
        }*/

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
                            switch (incomingVal)
                            {
                                case 1:
                                    //Debug.WriteLine("" + comboBoxSelectedText1);
                                    SendKeys.SendWait("" + comboBoxSelectedText1);
                                    break;
                                case 2:
                                    SendKeys.SendWait("" + comboBoxSelectedText2);
                                    //Debug.WriteLine("" + comboBoxSelectedText2);
                                    break;
                                case 3:
                                    SendKeys.SendWait("" + comboBoxSelectedText4);
                                    //Debug.WriteLine("" + comboBoxSelectedText3);
                                    break;
                                case 4:
                                    SendKeys.SendWait("" + comboBoxSelectedText5);
                                    //Debug.WriteLine("" + comboBoxSelectedText4);
                                    break;
                                case 5:
                                    SendKeys.SendWait("" + comboBoxSelectedText6);
                                    //Debug.WriteLine("" + comboBoxSelectedText5);
                                    break;
                                case 6:
                                    SendKeys.SendWait("" + comboBoxSelectedText3);
                                    //Debug.WriteLine("" + comboBoxSelectedText6);
                                    break;
                                default:
                                    break;
                            }
                            //SendKeys.SendWait("" + incomingVal);
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e);
                    }
                }
            }
        }

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
            toolTip1.SetToolTip(trackBar2, "" + trackBar2.Value);
            //Debug.WriteLine(trackBar2.Value);
            //serialPrt.WriteLine(""+trackBar2.Value);
            serialPrt.WriteLine("<2, " + trackBar2.Value + ">");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //readThread.Abort();
            serialPrt.Close();
            //Debug.WriteLine("stopArduinoRead");
            isSerialReadActive = false;
        }

        void connector3SensitivityValueChanged(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(trackBar3, "" + trackBar3.Value);
            //Debug.WriteLine(trackBar3.Value);
            //serialPrt.WriteLine(""+trackBar3.Value);
            serialPrt.WriteLine("<3, " + trackBar3.Value + ">");
        }

        void connector4SensitivityValueChanged(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(trackBar4, "" + trackBar4.Value);
            //Debug.WriteLine(trackBar4.Value);
            //serialPrt.WriteLine(""+trackBar4.Value);
            serialPrt.WriteLine("<4, " + trackBar4.Value + ">");
        }

        void connector5SensitivityValueChanged(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(trackBar5, "" + trackBar5.Value);
            //Debug.WriteLine(trackBar5.Value);
            //serialPrt.WriteLine(""+trackBar5.Value);
            serialPrt.WriteLine("<5, " + trackBar5.Value + ">");
        }

        void connector6SensitivityValueChanged(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(trackBar6, "" + trackBar6.Value);
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
