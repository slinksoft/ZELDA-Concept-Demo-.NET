﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ZeldaDemo
{
    public partial class credits : Form
    {
        public credits()
        {
            InitializeComponent();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void scroller_Tick(object sender, EventArgs e)
        {
            creditsw.Top -= 2;
        }

        private void credits_Load(object sender, EventArgs e)
        {
            this.Location = new System.Drawing.Point(500, 80);
        }
    }
}
