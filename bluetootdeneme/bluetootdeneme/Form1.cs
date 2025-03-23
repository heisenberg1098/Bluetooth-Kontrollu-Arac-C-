using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using AForge.Video;
using AForge.Video.DirectShow;

namespace bluetootdeneme
{

    public partial class Form1 : Form
    {
        string[] portlar = SerialPort.GetPortNames();
        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true;
        }
        int hız;
        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(comboBox1.Text))
            {

                serialPort1.PortName = comboBox1.Text;
                serialPort1.Open();
                serialPort1.BaudRate = 9600;
                label1.Text = "baglantı   açık";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            hız = trackBar1.Value;
            label3.Text = "hız : " + hız.ToString();
            foreach (string port in portlar)
            {
                comboBox1.Items.Add(port);
                comboBox1.SelectedIndex = 0;

            }
            label1.Text = "baglantı   kapalı";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == true)
            {
                serialPort1.Close();
                label1.Text = "baglantı   kapalı";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == true)
            {
                serialPort1.Write("F");
                label2.Text = "far   acık !";
            }
            else
            {
                MessageBox.Show("hata !!! baglantı YOK");
            }
        }
    

        private void button3_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == true)
            {
                serialPort1.Write("G"); label2.Text = "far   kapalı !";
            }
            else
            {
                MessageBox.Show("hata !!! baglantı YOK");
            }


        }

        private void button5_Click(object sender, EventArgs e)
        {
            FilterInfoCollection fic;
            VideoCaptureDevice vcd;

            fic = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo dev in fic)
            {
                comboBox2.Items.Add(dev.Name);
                comboBox2.SelectedIndex = 0;
                vcd = new VideoCaptureDevice();
                vcd = new VideoCaptureDevice(fic[comboBox2.SelectedIndex].MonikerString);
                vcd.NewFrame += final;
                vcd.Start();

            }
        }
        private void final(object sender, NewFrameEventArgs eventArgs)
        {
            pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            hız = trackBar1.Value;
            label3.Text = "hız : " + hız.ToString();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.F)
            {
                if (label2.Text == "far   kapalı !")
                {
                    button4.PerformClick();
                }
                else if (label2.Text == "far   acık !")
                {
                    button3.PerformClick();
                }
            }
            if (serialPort1.IsOpen == true)
            {


                if (e.KeyCode == Keys.W)
                {
                    MessageBox.Show("oldu");
                    serialPort1.Write("1");//ileri
                }
                else if (e.KeyCode == Keys.A)
                {
                    serialPort1.Write("2");//sol
                }
                else if (e.KeyCode == Keys.D)
                {
                    serialPort1.Write("3");//sag
                }
                else if (e.KeyCode == Keys.S)
                {
                    serialPort1.Write("4");//geri
                }
                else
                {
                    serialPort1.Write("0");//dur
                }
            }
            else MessageBox.Show("baglantı yok");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (string port in portlar)
            {
                comboBox1.Items.Add(port);
                comboBox1.SelectedIndex = 0;

            }
        }
    }
}


