using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Численные_методы_3
{   
    public struct ChartPoint
    {
        public List<double> x;
        public List<double> y;
        public List<double> Vx;
    }
    public struct StructForMainSolution
    {
        public ChartPoint chart_pointV;
        public ChartPoint chart_pointV2;
    }
    public partial class Form1 : Form
    {
        double x0 = 0;
        double xn = 1;
        double ksi = 0.125;
        ChartPoint chart_point;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            chart2.Series[0].Points.Clear();
            dataGridView1.Rows.Clear();
            chart_point = new ChartPoint();
            chart_point.x = new List<double>();
            chart_point.y = new List<double>();
            chart_point.Vx = new List<double>();
            List<double> max_abs = new List<double>();
            int n = Convert.ToInt32(numericUpDown1.Value);
            dataGridView1.ColumnCount = 5;
            dataGridView1.RowCount = n + 1;
            StructForMainSolution structForMain = new StructForMainSolution();
            structForMain.chart_pointV = new ChartPoint();
            structForMain.chart_pointV2 = new ChartPoint();
            structForMain.chart_pointV.x = new List<double>();
            structForMain.chart_pointV.Vx = new List<double>();
            structForMain.chart_pointV2.x = new List<double>();
            structForMain.chart_pointV2.Vx = new List<double>();
            bool flag = false;
            double max_UV;
            double max_UV_x;
            if (radioButton1.Checked)
            {
                chart_point = SolutionsTest1(Convert.ToDouble(textBox1.Text), Convert.ToDouble(textBox2.Text), Convert.ToDouble(textBox3.Text), n, x0, xn);
                flag = false;
            }
            if (radioButton2.Checked)
            {
                chart_point = SolutionsTest2(n, x0, xn, ksi);
                flag = false;
            }
            if (radioButton3.Checked)
            {
                structForMain = MainSolution(n, x0, xn, ksi);
                flag = true;
                dataGridView1.Columns[2].HeaderText = "v(x)";
                dataGridView1.Columns[3].HeaderText = "v2(x)";
                dataGridView1.Columns[4].HeaderText = "|v(x)-v2(x)|";
            }
            if (flag == true)
            {
                max_UV = Math.Abs(structForMain.chart_pointV.Vx[0] - structForMain.chart_pointV2.Vx[0]);
                max_UV_x = structForMain.chart_pointV.x[0];
                for (int i = 0; i < structForMain.chart_pointV.x.Count; i++)
                {
                    chart1.Series[0].Points.AddXY(structForMain.chart_pointV.x[i], structForMain.chart_pointV.Vx[i]);
                    chart1.Series[1].Points.AddXY(structForMain.chart_pointV2.x[2 * i], structForMain.chart_pointV2.Vx[2 * i]);
                    dataGridView1.Rows[i].Cells[0].Value = i;
                    dataGridView1.Rows[i].Cells[1].Value = structForMain.chart_pointV.x[i];
                    dataGridView1.Rows[i].Cells[2].Value = structForMain.chart_pointV.Vx[i];
                    dataGridView1.Rows[i].Cells[3].Value = structForMain.chart_pointV2.Vx[2 * i];
                    dataGridView1.Rows[i].Cells[4].Value = Math.Abs(structForMain.chart_pointV.Vx[i] - structForMain.chart_pointV2.Vx[2 * i]);
                    if (Math.Abs(structForMain.chart_pointV.Vx[i] - structForMain.chart_pointV2.Vx[2 * i]) > max_UV)
                    {
                        max_UV = Math.Abs(structForMain.chart_pointV.Vx[i] - structForMain.chart_pointV2.Vx[2 * i]);
                        max_UV_x = structForMain.chart_pointV.x[i];
                    }
                    chart2.Series[0].Points.AddXY(structForMain.chart_pointV.x[i], Math.Abs(structForMain.chart_pointV.Vx[i] - structForMain.chart_pointV2.Vx[2 * i]));
                }
                label8.Text = "Max(|v(x)-v2(x)|): " + max_UV;
            }
            else
            {
                max_UV = Math.Abs(chart_point.y[0] - chart_point.Vx[0]);
                max_UV_x = chart_point.x[0];
                for (int i = 0; i < chart_point.x.Count; i++)
                {
                    chart1.Series[0].Points.AddXY(chart_point.x[i], chart_point.y[i]);
                    chart1.Series[1].Points.AddXY(chart_point.x[i], chart_point.Vx[i]);
                    dataGridView1.Rows[i].Cells[0].Value = i;
                    dataGridView1.Rows[i].Cells[1].Value = chart_point.x[i];
                    dataGridView1.Rows[i].Cells[2].Value = chart_point.y[i];
                    dataGridView1.Rows[i].Cells[3].Value = chart_point.Vx[i];
                    dataGridView1.Rows[i].Cells[4].Value = Math.Abs(chart_point.y[i] - chart_point.Vx[i]);
                    if (Math.Abs(chart_point.y[i] - chart_point.Vx[i]) > max_UV)
                    {
                        max_UV = Math.Abs(chart_point.y[i] - chart_point.Vx[i]);
                        max_UV_x = chart_point.x[i];
                    }
                    chart2.Series[0].Points.AddXY(chart_point.x[i], Math.Abs(chart_point.y[i] - chart_point.Vx[i]));
                }
                label8.Text = "Max(|u(x)-v(x)|): " + max_UV;
            }

            label9.Text = "В точке: x= " + max_UV_x;
        }  
               

        private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
        {
            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            textBox1.Visible = true;
            textBox2.Visible = true;
            textBox3.Visible = true;
        }

        private void radioButton2_CheckedChanged_1(object sender, EventArgs e)
        {
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
        }

        private void radioButton3_CheckedChanged_1(object sender, EventArgs e)
        {
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
            numericUpDown1.Value = 10;
            textBox1.Text = "1";
            textBox2.Text = "1";
            textBox3.Text = "1";
        }
        private double P2(double x, double a, double b, double c)
        {
            return a * x * x + b * x + c;
        }
        public ChartPoint SolutionsTest1(double a, double b, double c, int n, double x0, double xn)
        {
            ChartPoint chart_point = new ChartPoint();
            chart_point.x = new List<double>();
            chart_point.y = new List<double>();
            chart_point.Vx = new List<double>();
            double[] diagonal1 = new double[n - 1];
            double[] diagonal2 = new double[n];
            double[] diagonal3 = new double[n - 1];
            double[] right_column = new double[n];
            double q, f, k;
            double h = (xn - x0) / n;
            double mu1 = P2(x0, a, b, c);
            double mu2 = P2(xn, a, b, c);
            for (int i = 0; i < n + 1; i++)
            {
                chart_point.x.Add(i * h);
                chart_point.y.Add(P2(i * h, a, b, c));
            }
            if ((a != 0) || (b != 0))
            {
                q = 0;
                k = 1;
                f = -2 * a;
            }
            else
            {
                k = 1;
                q = 1;
                f = c - 2 * a;
            }
            chart_point.Vx.Add(mu1);
            for (int i = 1; i < n - 1; i++)
            {
                diagonal1[i] = k / Math.Pow(h, 2);
                diagonal2[i] = -(2 * k / Math.Pow(h, 2) + q);
                diagonal3[i] = k / Math.Pow(h, 2);
            }
            diagonal2[n - 1] = -(2 * k / Math.Pow(h, 2) + q);
            right_column[1] = -(k * mu1) / Math.Pow(h, 2) - f;
            for (int i = 2; i < n - 1; i++)
            {
                right_column[i] = -f;
            }
            right_column[n - 1] = -(k * mu2) / Math.Pow(h, 2) - f;
            double[] sweep_result = Sweep(diagonal1, diagonal2, diagonal3, right_column);
            for (int i = 0; i < sweep_result.Length; i++)
            {
                chart_point.Vx.Add(sweep_result[i]);
            }
            chart_point.Vx.Add(mu2);
            return chart_point;
        }
        private List<double> GetSolutionTest2(double n, double h, double ksi)
        {
            List<double> solution = new List<double>();
            double C1, C2, C3, C4;
            double A1, A2, B1, B2;
            double A11, A12, A21, A22;
            double val1, val2;
            MyFunctions myFunc = new MyFunctions();
            A1 = myFunc.q1(ksi) / myFunc.k1(ksi);
            A2 = myFunc.q2(ksi) / myFunc.k2(ksi);
            B1 = -myFunc.f1(ksi) / myFunc.q1(ksi);
            B2 = -myFunc.f2(ksi) / myFunc.q2(ksi);
            A11 = -(Math.Exp(Math.Pow(A1, 0.5) * ksi) - Math.Exp(-Math.Pow(A1, 0.5) * ksi));
            A12 = Math.Exp(-2 * Math.Pow(A2, 0.5) + Math.Pow(A2, 0.5) * ksi) - Math.Exp(-Math.Pow(A2, 0.5) * ksi);
            A21 = -myFunc.k1(ksi) * Math.Pow(A1, 0.5) * (Math.Exp(Math.Pow(A1, 0.5) * ksi) + Math.Exp(-Math.Pow(A1, 0.5) * ksi));
            A22 = myFunc.k2(ksi) * Math.Pow(A2, 0.5) * (Math.Exp(-2 * Math.Pow(A2, 0.5) + Math.Pow(A2, 0.5) * ksi) + Math.Exp(-Math.Pow(A2, 0.5) * ksi));
            val1 = (1 + B2) * Math.Exp(Math.Pow(A2, 0.5) * ksi - Math.Pow(A2, 0.5)) - B2 + B1 * (1 - Math.Exp(Math.Pow(A1, 0.5) * ksi));
            val2 = -myFunc.k1(ksi) * Math.Pow(A1, 0.5) * B1 * Math.Exp(Math.Pow(A1, 0.5) * ksi) + myFunc.k2(ksi) * Math.Pow(A2, 0.5) * (1 + B2) * Math.Exp(Math.Pow(A2, 0.5) * ksi - Math.Pow(A2, 0.5));
            A12 = A12 / A11;
            val1 = val1 / A11;
            A22 = A22 / A21 - A12;
            val2 = val2 / A21 - val1;
            val2 = val2 / A22;
            val1 = val1 - A12 * val2;
            C2 = val1;
            C4 = val2;
            C1 = B1 - C2;
            C3 = (1 + B2 - C4 * Math.Exp(-Math.Pow(A2, 0.5))) * Math.Exp(-Math.Pow(A2, 0.5));
            for (int i = 0; i < n + 1; i++)
            {
                if (ksi > h * i)
                    solution.Add(C1 * Math.Exp(Math.Pow(A1, 0.5) * h * i) + C2 * Math.Exp(-Math.Pow(A1, 0.5) * h * i) - B1);
                else
                    solution.Add(C3 * Math.Exp(Math.Pow(A2, 0.5) * h * i) + C4 * Math.Exp(-Math.Pow(A2, 0.5) * h * i) - B2);
            }
            return solution;
        }
        public ChartPoint SolutionsTest2(int n, double x0, double xn, double ksi)
        {
            ChartPoint chart_point = new ChartPoint();
            chart_point.x = new List<double>();
            chart_point.y = new List<double>();
            chart_point.Vx = new List<double>();
            double[] k = new double[n + 1];
            double[] f = new double[n];
            double[] q = new double[n];
            double[] diagonal1 = new double[n - 1];
            double[] diagonal2 = new double[n];
            double[] diagonal3 = new double[n - 1];
            double[] right_column = new double[n];
            MyFunctions myFunc = new MyFunctions();
            double h = (xn - x0) / n;
            for (int i = 0; i < n + 1; i++)
            {
                chart_point.x.Add(i * h);
            }
            chart_point.y = GetSolutionTest2(n, h, ksi);
            for (int i = 1; i < n; ++i)
            {
                if (ksi >= (chart_point.x[i]) + 0.5 * h)
                {
                    f[i] = myFunc.f1(ksi);
                    q[i] = myFunc.q1(ksi);
                }
                else if (ksi <= (chart_point.x[i] - 0.5 * h))
                {
                    f[i] = myFunc.f2(ksi);
                    q[i] = myFunc.q2(ksi);
                }
                else
                {
                    f[i] = n * ((ksi - (chart_point.x[i] - 0.5 * h)) * myFunc.f1(ksi) + ((chart_point.x[i] + 0.5 * h) - ksi) * myFunc.f2(ksi));
                    q[i] = n * ((ksi - (chart_point.x[i] - 0.5 * h)) * myFunc.q1(ksi) + ((chart_point.x[i] + 0.5 * h) - ksi) * myFunc.q2(ksi));
                }

                if (ksi >= chart_point.x[i])
                {
                    k[i] = myFunc.k1(ksi);
                }
                else if (ksi <= (chart_point.x[i - 1]))
                {
                    k[i] = myFunc.k2(ksi);
                }
                else
                {
                    k[i] = 2 * h * myFunc.k1(ksi) * myFunc.k1(ksi) * myFunc.k2(ksi) * myFunc.k2(ksi) / ((ksi - chart_point.x[i - 1]) * (myFunc.k1(ksi) + myFunc.k1(ksi)) * myFunc.k2(ksi) * myFunc.k2(ksi) + (chart_point.x[i] - ksi) * (myFunc.k2(ksi) + myFunc.k2(ksi)) * myFunc.k1(ksi) * myFunc.k1(ksi));
                }
            }
            k[(int)n] = myFunc.k2(ksi);
            chart_point.Vx.Add(x0);

            for (int i = 1; i < n - 1; ++i)
            {
                diagonal1[i] = k[i + 1] * n * n;
                diagonal2[i] = -(n * n * (k[i] + k[i + 1]) + q[i]);
                diagonal3[i] = k[i + 1] * n * n;
            }
            diagonal2[n - 1] = -(n * n * (k[n - 1] + k[n]) + q[n - 1]);

            for (int i = 1; i < n; i++)
            {
                right_column[i] = -f[i];
            }
            right_column[1] -= x0 * k[1] * n * n;
            right_column[n - 1] -= xn * k[n] * n * n;
            double[] sweep_result = Sweep(diagonal1, diagonal2, diagonal3, right_column);
            for (int i = 0; i < sweep_result.Length; i++)
            {
                chart_point.Vx.Add(sweep_result[i]);
            }
            chart_point.Vx.Add(xn);
            return chart_point;
        }
        public StructForMainSolution MainSolution(int n, double x0, double xn, double ksi)
        {
            StructForMainSolution structForMain = new StructForMainSolution();
            structForMain.chart_pointV = new ChartPoint();
            structForMain.chart_pointV.x = new List<double>();
            structForMain.chart_pointV.Vx = new List<double>();
            structForMain.chart_pointV2 = new ChartPoint();
            structForMain.chart_pointV2.x = new List<double>();
            structForMain.chart_pointV2.Vx = new List<double>();
            double[] k = new double[n + 1];
            double[] k2 = new double[2 * n + 1];
            double[] f = new double[n];
            double[] f2 = new double[2 * n + 1];
            double[] q = new double[n];
            double[] q2 = new double[2 * n + 1];
            double[] diagonal1 = new double[n - 1];
            double[] diagonal1_2 = new double[2 * n - 1];
            double[] diagonal2 = new double[n];
            double[] diagonal2_2 = new double[2 * n];
            double[] diagonal3 = new double[n - 1];
            double[] diagonal3_2 = new double[2 * n - 1];
            double[] right_column = new double[n];
            double[] right_column2 = new double[2 * n];
            double h = (xn - x0) / n;
            double h2 = h / 2;
            MyFunctions myFunc = new MyFunctions();
            for (int i = 0; i < n + 1; ++i)
            {
                structForMain.chart_pointV.x.Add(i * h);
            }

            for (int i = 0; i < 2 * n + 1; ++i)
            {
                structForMain.chart_pointV2.x.Add(i * h2);
            }

            for (int i = 1; i < n; ++i)
            {
                if (ksi >= (structForMain.chart_pointV.x[i] + 0.5 * h))
                {
                    f[i] = myFunc.f1(structForMain.chart_pointV.x[i]);
                    q[i] = myFunc.q1(structForMain.chart_pointV.x[i]);
                }
                else if (ksi <= (structForMain.chart_pointV.x[i] - 0.5 * h))
                {
                    f[i] = myFunc.f2(structForMain.chart_pointV.x[i]);
                    q[i] = myFunc.q2(structForMain.chart_pointV.x[i]);
                }
                else
                {
                    f[i] = n * ((ksi - (structForMain.chart_pointV.x[i] - 0.5 * h)) * myFunc.f1(0.5 * ((structForMain.chart_pointV.x[i] - 0.5 * h) + ksi)) + ((structForMain.chart_pointV.x[i] + 0.5 * h) - ksi) * myFunc.f2(0.5 * (ksi + (structForMain.chart_pointV.x[i] + 0.5 * h))));
                    q[i] = n * ((ksi - (structForMain.chart_pointV.x[i] - 0.5 * h)) * myFunc.q1(0.5 * (ksi + (structForMain.chart_pointV.x[i] - 0.5 * h))) + ((structForMain.chart_pointV.x[i] + 0.5 * h) - ksi) * myFunc.q2(0.5 * ((structForMain.chart_pointV.x[i] + 0.5 * h) + ksi)));
                }


                if (ksi >= structForMain.chart_pointV.x[i])
                {
                    k[i] = 2 * myFunc.k1(structForMain.chart_pointV.x[i]) * myFunc.k1(structForMain.chart_pointV.x[i - 1]) / (myFunc.k1(structForMain.chart_pointV.x[i - 1]) + myFunc.k1(structForMain.chart_pointV.x[i]));
                }
                else if (ksi <= structForMain.chart_pointV.x[i - 1])
                {
                    k[i] = 2 * myFunc.k2(structForMain.chart_pointV.x[i - 1]) * myFunc.k2(structForMain.chart_pointV.x[i]) / (myFunc.k2(structForMain.chart_pointV.x[i - 1]) + myFunc.k2(structForMain.chart_pointV.x[i]));
                }
                else
                {
                    k[i] = 2 * h * myFunc.k1(structForMain.chart_pointV.x[i - 1]) * myFunc.k1(ksi) * myFunc.k2(ksi) * myFunc.k2(structForMain.chart_pointV.x[i]) / ((ksi - structForMain.chart_pointV.x[i - 1]) * (myFunc.k1(ksi) + myFunc.k1(structForMain.chart_pointV.x[i - 1])) * myFunc.k2(ksi) * myFunc.k2(structForMain.chart_pointV.x[i]) + (structForMain.chart_pointV.x[i] - ksi) * (myFunc.k2(structForMain.chart_pointV.x[i]) + myFunc.k2(ksi)) * myFunc.k1(structForMain.chart_pointV.x[i - 1]) * myFunc.k1(ksi));
                }

            }
            k[n] = 2 * myFunc.k2(structForMain.chart_pointV.x[(int)n]) * myFunc.k2(structForMain.chart_pointV.x[n - 1]) / (myFunc.k2(structForMain.chart_pointV.x[n - 1]) + myFunc.k2(structForMain.chart_pointV.x[(int)n]));

            structForMain.chart_pointV.Vx.Add(x0);

            for (int i = 1; i < 2 * n; ++i)
            {
                if (ksi >= (structForMain.chart_pointV2.x[i] + 0.5 * h2))
                {
                    f2[i] = myFunc.f1(structForMain.chart_pointV2.x[i]);
                    q2[i] = myFunc.q1(structForMain.chart_pointV2.x[i]);
                }
                else if (ksi <= (structForMain.chart_pointV2.x[i] - 0.5 * h2))
                {
                    f2[i] = myFunc.f2(structForMain.chart_pointV2.x[i]);
                    q2[i] = myFunc.q2(structForMain.chart_pointV2.x[i]);
                }
                else
                {
                    f2[i] = 2 * n * ((ksi - (structForMain.chart_pointV2.x[i] - 0.5 * h2)) * myFunc.f1(0.5 * ((structForMain.chart_pointV2.x[i] - 0.5 * h2) + ksi)) + ((structForMain.chart_pointV2.x[i] + 0.5 * h2) - ksi) * myFunc.f2(0.5 * (ksi + (structForMain.chart_pointV2.x[i] + 0.5 * h2))));
                    q2[i] = 2 * n * ((ksi - (structForMain.chart_pointV2.x[i] - 0.5 * h2)) * myFunc.q1(0.5 * (ksi + (structForMain.chart_pointV2.x[i] - 0.5 * h2))) + ((structForMain.chart_pointV2.x[i] + 0.5 * h2) - ksi) * myFunc.q2(0.5 * ((structForMain.chart_pointV2.x[i] + 0.5 * h2) + ksi)));
                }


                if (ksi >= structForMain.chart_pointV2.x[i])
                {
                    k2[i] = 2 * myFunc.k1(structForMain.chart_pointV2.x[i]) * myFunc.k1(structForMain.chart_pointV2.x[i - 1]) / (myFunc.k1(structForMain.chart_pointV2.x[i - 1]) + myFunc.k1(structForMain.chart_pointV2.x[i]));
                }
                else if (ksi <= structForMain.chart_pointV2.x[i - 1])
                {
                    k2[i] = 2 * myFunc.k2(structForMain.chart_pointV2.x[i - 1]) * myFunc.k2(structForMain.chart_pointV2.x[i]) / (myFunc.k2(structForMain.chart_pointV2.x[i - 1]) + myFunc.k2(structForMain.chart_pointV2.x[i]));
                }
                else
                {
                    k2[i] = 2 * h2 * myFunc.k1(structForMain.chart_pointV2.x[i - 1]) * myFunc.k1(ksi) * myFunc.k2(ksi) * myFunc.k2(structForMain.chart_pointV2.x[i]) / ((ksi - structForMain.chart_pointV2.x[i - 1]) * (myFunc.k1(ksi) + myFunc.k1(structForMain.chart_pointV2.x[i - 1])) * myFunc.k2(ksi) * myFunc.k2(structForMain.chart_pointV2.x[i]) + (structForMain.chart_pointV2.x[i] - ksi) * (myFunc.k2(structForMain.chart_pointV2.x[i]) + myFunc.k2(ksi)) * myFunc.k1(structForMain.chart_pointV2.x[i - 1]) * myFunc.k1(ksi));
                }

            }
            k2[2 * n] = 2 * myFunc.k2(structForMain.chart_pointV2.x[2 * n]) * myFunc.k2(structForMain.chart_pointV2.x[2 * n - 1]) / (myFunc.k2(structForMain.chart_pointV2.x[2 * n - 1]) + myFunc.k2(structForMain.chart_pointV2.x[2 * n]));
            structForMain.chart_pointV2.Vx.Add(x0);
            for (int i = 1; i < n - 1; ++i)
            {
                diagonal1[i] = k[i + 1] * n * n;
                diagonal2[i] = -(n * n * (k[i] + k[i + 1]) + q[i]);
                diagonal3[i] = k[i + 1] * n * n;
            }
            diagonal2[(int)n - 1] = -(n * n * (k[n - 1] + k[n]) + q[n - 1]);

            for (int i = 1; i < n; i++)
            {
                right_column[i] = -f[i];
            }
            right_column[1] -= x0 * k[1] * n * n;
            right_column[n - 1] -= xn * k[n] * n * n;
            double[] solutionForV = Sweep(diagonal1, diagonal2, diagonal3, right_column);
            for (int i = 1; i < n; ++i)
            {
                structForMain.chart_pointV.Vx.Add(solutionForV[i - 1]);
            }
            for (int i = 1; i < 2 * n - 1; ++i)
            {
                diagonal1_2[i] = k2[i + 1] * n * n * 4;
                diagonal2_2[i] = -(4 * n * n * (k2[i] + k2[i + 1]) + q2[i]);
                diagonal3_2[i] = k2[i + 1] * n * n * 4;
            }
            diagonal2_2[2 * n - 1] = -(4 * n * n * (k2[2 * n - 1] + k2[2 * n]) + q2[2 * n - 1]);

            for (int i = 1; i < 2 * n; i++)
            {
                right_column2[i] = -f2[i];
            }
            right_column2[1] -= x0 * k2[1] * n * n * 4;
            right_column2[2 * n - 1] -= xn * k2[2 * n] * n * n * 4;
            double[] solutionForV2 = Sweep(diagonal1_2, diagonal2_2, diagonal3_2, right_column2);
            for (int i = 1; i < 2 * n; ++i)
            {
                structForMain.chart_pointV2.Vx.Add(solutionForV2[i - 1]);
            }
            structForMain.chart_pointV.Vx.Add(xn);
            structForMain.chart_pointV2.Vx.Add(xn);
            return structForMain;
        }
        public double[] Sweep(double[] diag1, double[] diag2, double[] diag3, double[] rightColoumn)
        {
            int size = diag2.Length - 2;
            double[] solution = new double[size + 1];
            double[] A = new double[size];
            double[] B = new double[size];
            double[] C = new double[size];
            double[] alpha = new double[size + 1];
            double[] betta = new double[size + 1];
            double[] fi = new double[size];
            double mu1, mu2, kappa1, kappa2;
            for (int i = 1; i < size; i++)
            {
                A[i] = diag3[i];
                B[i] = diag1[i + 1];
                C[i] = -diag2[i + 1];
                fi[i] = -rightColoumn[i + 1];
            }
            mu1 = rightColoumn[1] / diag2[1];
            mu2 = rightColoumn[size + 1] / diag2[size + 1];
            kappa1 = -(diag1[1] / diag2[1]);
            kappa2 = -(diag3[size] / diag2[size + 1]);
            alpha[1] = kappa1;
            betta[1] = mu1;
            for (int i = 1; i < size; i++)
            {
                alpha[i + 1] = B[i] / (C[i] - alpha[i] * A[i]);
                betta[i + 1] = (fi[i] + A[i] * betta[i]) / (C[i] - alpha[i] * A[i]);
            }
            solution[size] = (mu2 + kappa2 * betta[size]) / (1 - kappa2 * alpha[size]);
            for (int i = size - 1; i > -1; i--)
            {
                solution[i] = alpha[i + 1] * solution[i + 1] + betta[i + 1];
            }
            return solution;
        }
    }
}
