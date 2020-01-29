using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace xml_prac
{
    public partial class Form1 : Form
    {
        int[] temp = new int[2];
        public Form1()
        {
            InitializeComponent();
            display_textBox.ReadOnly = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            XmlTextWriter writer = new XmlTextWriter("product.xml", Encoding.UTF8);
            writer.WriteStartDocument(true);
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 2;
            writer.WriteStartElement("Table");
            createNode("1", "Product 1", "1000", writer);
            createNode("2", "Product 2", "2000", writer);
            createNode("3", "Product 3", "3000", writer);
            createNode("4", "Product 4", "4000", writer);
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
            MessageBox.Show("XML File created ! ");
        }

        private void createNode(string pID, string pName, string pPrice, XmlWriter writer)
        {
            writer.WriteStartElement("Product");
            writer.WriteStartElement("Product_id");
            writer.WriteString(pID);
            writer.WriteEndElement();
            writer.WriteStartElement("Product_name");
            writer.WriteString(pName);
            writer.WriteEndElement();
            writer.WriteStartElement("Product_price");
            writer.WriteString(pPrice);
            writer.WriteEndElement();
            writer.WriteEndElement();
        }

        [Obsolete]
        private void button2_Click(object sender, EventArgs e)
        {
            XmlDataDocument xmldoc = new XmlDataDocument();
            XmlNodeList xmlnode;
            int i = 0, j = 0;
            string str = null;
            FileStream fs = new FileStream("product.xml", FileMode.Open, FileAccess.Read);
            xmldoc.Load(fs);
            xmlnode = xmldoc.GetElementsByTagName("Product");
            for (i = 0; i <= xmlnode.Count - 1; i++)
            {
                xmlnode[i].ChildNodes.Item(0).InnerText.Trim();
                str = Environment.NewLine;
                for ( j = 0; j < xmlnode[i].ChildNodes.Count; j++)
                {
                    str += xmlnode[i].ChildNodes.Item(j).InnerText.Trim() + "  ";
                }
                display_textBox.Text += str;
            }
            /*fs.Close();*/
        }

        [Obsolete]
        private void search_button_Click(object sender, EventArgs e)
        {
            XmlDataDocument xmldoc = new XmlDataDocument();
            XmlNodeList xmlnode;
            int i = 0, j = 0;
            string str = null;
            FileStream fs = new FileStream("product.xml", FileMode.Open, FileAccess.Read);
            xmldoc.Load(fs);
            xmlnode = xmldoc.GetElementsByTagName("Product");
            for (j = 0; j < xmlnode.Count; j++)
            {
                if (search_textBox.Text== xmlnode[j].ChildNodes.Item(0).InnerText.Trim().ToString())
                {
                    for (i = 1; i < xmlnode[j].ChildNodes.Count; i++)
                    {
                        str += xmlnode[j].ChildNodes.Item(i).InnerText.Trim() + "  ";
                    }
                    temp[0] = j;
                    temp[1] = i;
                }
                display_textBox.Text = str;
            }
            fs.Close();
        }

        [Obsolete]
        private void update_button_Click(object sender, EventArgs e)
        {
            XmlDataDocument xmldoc = new XmlDataDocument();
            XmlNodeList xmlnode;
            int i = temp[0], j = temp[1];
            string str = null;
            FileStream fs = new FileStream("product.xml", FileMode.Open, FileAccess.ReadWrite);
            xmldoc.Load(fs);
            XmlElement root = xmldoc.DocumentElement;
            XmlNodeList nodes = root.SelectNodes("/Table/Product");
            foreach (XmlNode node in nodes)
            {
                if (node["Product_id"].InnerText.ToString().Trim() == search_textBox.Text)
                {
                    node["Product_price"].InnerText = update_textBox.Text;
                }
            }
            xmldoc.Save(fs);
            /*
            xmlnode = xmldoc.GetElementsByTagName("Product");
            xmlnode[i].ChildNodes.Item(j).InnerText.Trim();
            str = update_textBox.Text;
            xmlnode[i].ChildNodes.Item(j).InnerText = str;*/
        }
    }
}
