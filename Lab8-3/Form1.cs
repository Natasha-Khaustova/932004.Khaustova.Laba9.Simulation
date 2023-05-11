using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab8_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        double Value1 ;
        double Value2 ;
        double Value3 ;
        double Value4 ;
        double Value5 ;
        int n ;
        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var series in chart1.Series)
            {
                series.Points.Clear();
            }
            Value1 = (double)Val1Ed1.Value;
            Value2 = (double)Val1Ed2.Value;
            Value3 = (double)Val1Ed3.Value;
            Value4 = (double)Val1Ed4.Value;
            double s = Value1 + Value2 + Value3 + Value4;
            if (s > 1) 
            {
                MessageBox.Show("!FAIL! the sum of the probabilities must be equal to 1");
            }
            else
            {
                Value5 = 1 - s;
            }
            Val1Ed5.Value = (decimal)Value5;
            n = (int)numEd.Value;
            

            double[] Probabilities = { Value1, Value2, Value3, Value4, Value5 };
            double[] Generation = new double[5];

            Random rnd = new Random();
            double randNum;
            for (int i = 0; i < n; i++)
            {
                randNum = rnd.NextDouble();
                int event_id = 0;
                randNum -= Probabilities[0];
                while (randNum > 0)
                {
                    event_id++;
                    randNum -= Probabilities[event_id];
                };
                Generation[event_id]++;
            }
            double Average=0;
            for (int j = 0; j < 5; j++)
            {
                Generation[j] /= n;
                Average += Generation[j];
                chart1.Series[j].Points.AddXY(j,Generation[j]);
            }
            double MathExpect = Average / 5;
            double Variance = 0;
            for (int i = 0; i < 5; i++)
            {
                Variance += ((Generation[i] - MathExpect) * (Generation[i] - MathExpect))*Generation[i];
            }
            double[] RelativeError = new double[5]; 
            for (int i = 0; i < 5; i++)
            {
                RelativeError[i] = (Generation[i] - Probabilities[i]) / Probabilities[i] * 100;
            }
            double ChiSquared = 0;
            for (int i = 0; i < 5; i++)
            {
                double c;
                c = Math.Abs(Generation[i] - Probabilities[i]);
                if (c == 0)
                {
                    c = 1;
                }
                ChiSquared = ChiSquared + (double)(c * c) / Probabilities[i];
            }

            EmpVer1.Text = Math.Round(Generation[0], 4).ToString();
            EmpVer2.Text = Math.Round(Generation[1], 4).ToString();
            EmpVer3.Text = Math.Round(Generation[2], 4).ToString();
            EmpVer4.Text = Math.Round(Generation[3], 4).ToString();
            EmpVer5.Text = Math.Round(Generation[4], 4).ToString();
            vibSredLbl.Text = Math.Round(MathExpect, 4).ToString();
            DLbl.Text = Math.Round(Variance, 4).ToString();
            sigma1.Text = Math.Round(RelativeError[0], 4).ToString();
            sigma2.Text = Math.Round(RelativeError[1], 4).ToString();
            sigma3.Text = Math.Round(RelativeError[2], 4).ToString();
            sigma4.Text = Math.Round(RelativeError[3], 4).ToString();
            sigma5.Text = Math.Round(RelativeError[4], 4).ToString();


            if(ChiSquared>9.488)
            {
                chiLbl.Text = ChiSquared.ToString() + " > " + "9.488" + " true";
            }
            else
            {
                chiLbl.Text = ChiSquared.ToString() + " < " + "9.488" + " false";
            }
        }


    }
}
