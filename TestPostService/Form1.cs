using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;

namespace TestPostService
{
    public partial class frmMain : Form
    {
        private string URLHeader = "http://www.ccpn.gov.cn/syncservice/reportmaster/";

        public frmMain()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
           String ret = PostWebRequest(URLHeader + this.cboService.Text+"?para="+ this.txtRequest.Text, Encoding.Default);
           this.txtResponse.Text = ret;
        }

        private string PostWebRequest(string postUrl, Encoding dataEncode)
        {
            string ret = string.Empty;
            try
            {
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(postUrl));
                webReq.Method = "GET";
                
                HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.Default);
                ret = sr.ReadToEnd();
                sr.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return ret;
        }
    }
}
