﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Painting;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace Pendulum
{
    public partial class PendulumForm : Form
    {
        public PendulumForm()
        {
            InitializeComponent();
            param.xmin = -50;
            param.xmax = 50;
            param.ymin = -1;
            param.ymax = 10;
            param.N = 50;
            param.stepx = (double)param.xmax / 5;
            param.stepy = (double)param.ymax / 5;
            param.osicolor = Color.Black;
            param.setkacolor = Color.Black;
            param.backgroundcolor = Color.LightBlue;
            param.graphcolor = Color.Red;
            param.pointcolor = Color.Brown;

            painting();
        }

        Parametrs param = new Parametrs();
        PhisycalBody body = new PhisycalBody();
        Bitmap bmp;
        double K, m, x0, l;


        private void painting()
        {
            bmp = new Bitmap(WorldPic.Width, WorldPic.Height);
            Graphics g = Graphics.FromImage(bmp); ;

            SolidBrush brush_back = new SolidBrush(param.backgroundcolor);

            g.FillRectangle(brush_back, 0, 0, WorldPic.Width, WorldPic.Height);
            //ручка для осей
            Pen osi = new Pen(param.osicolor, 3);
            osi.DashStyle = DashStyle.Solid;

            //ручка для сетки
            Pen setka = new Pen(param.setkacolor, 1);
            setka.DashStyle = DashStyle.DashDotDot;

            //ручка для сетки
            Pen graph_pen = new Pen(param.graphcolor, 3);
            setka.DashStyle = DashStyle.Solid;

            //ручка для сетки
            Pen bifur_pen = new Pen(param.graphcolor, 1);
            setka.DashStyle = DashStyle.Solid;

            //ручка для сетки
            SolidBrush solidline = new SolidBrush(param.pointcolor);

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;

            double width = WorldPic.Width, height = WorldPic.Height;

            //рисую оси
            g.DrawLine(osi, (float)param.X(width, param.xmin), (float)param.Y(height, 0), (float)param.X(width, param.xmax), (float)param.Y(height, 0));








            WorldPic.Image = bmp;
            bmp.Save(@"picture.png", System.Drawing.Imaging.ImageFormat.Png);
        }

        void RungeKutt(double dt)  //метод Рунге-Кутты
        {

            double xx = body.x;
            double yy = body.y;
            double dxx = body.dx;
            double dyy = body.dy;
            double dtt = dt;
            double k1x = ddx(xx, dxx) * dtt;
            double k2x = ddx(xx + (double)dxx * dtt / 2, dxx + (double)k1x / 2) * dtt;
            double k3x = ddx(xx + (double)dxx * dtt / 2 + (double)k1x * dtt / 4, dxx + (double)k2x / 2) * dtt;
            double k4x = ddx(xx + (double)dxx * dtt + k2x * dtt / 2, dxx + k3x) * dtt;
            body.x = xx + dxx * dtt + (double)1 / 6 * (k1x + k2x + k3x) * dtt;
            body.dx = dxx + (double)1 / 6 * (2 * k2x + 2 * k3x + k1x + k4x);

        }

        double ddx(double x, double dx)
        {
            return K * (Math.Sqrt(l * l + x * x) - l) * x / (Math.Sqrt(l * l + x * x) * m);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void Run_Click(object sender, EventArgs e)
        {
            m = double.Parse(mBox.Text);
            K = double.Parse(Kbox.Text);
            x0 = double.Parse(X0Box.Text);
            l = double.Parse(LBox.Text);

        }
    }
}
