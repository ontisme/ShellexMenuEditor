using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShellexMenuEditor
{
    public partial class Main : Form
    {
        public string key_base = @"*\shellex\ContextMenuHandlers";
        public Main()
        {
            InitializeComponent();
            InitKeyList();
        }

        public void InitKeyList()
        {
            dgv_key_list.Rows.Clear();
            foreach (var i in GetSubKeys())
            {
                dgv_key_list.Rows.Add(i);
            }
        }

        public string[] GetSubKeys()
        {
            RegistryKey registryKey = Registry.ClassesRoot.CreateSubKey(key_base);
            var sub_key_list = registryKey.GetSubKeyNames();
            return sub_key_list;
        }

        public void DelSubKey(string key_name)
        {
            RegistryKey key = Registry.ClassesRoot;

            key.DeleteSubKey($"{key_base}\\{key_name}", true);
            key.Close();
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitKeyList();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var index = dgv_key_list.CurrentCell.RowIndex;
            var key_name = dgv_key_list.Rows[index].Cells[0].Value.ToString();
            DelSubKey(key_name);
            MessageBox.Show("Deleted, may require a reboot to take effect");
            InitKeyList();
        }
    }
}
