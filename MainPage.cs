﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Planner
{
    public partial class MainPage : Form
    {
        Employee em = null;
        public MainPage(Employee employee)
        {
            InitializeComponent();
            em = employee;
        }

        private void MainPage_Load(object sender, EventArgs e)
        {

        }
    }
}
