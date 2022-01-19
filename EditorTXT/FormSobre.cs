using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EditorTXT
{
    public partial class FormSobre : Form
    {
        public FormSobre()
        {
            InitializeComponent();
            WebBrowser Webrowser = new WebBrowser();
            Webrowser.Dock = DockStyle.Fill;
            Webrowser.Navigate(Application.StartupPath + @"sobre\Menu\Menu.html");
            this.Controls.Add(Webrowser);
        }
    }
}
