using ContactsBusinessLayer;
using System;
using System.Windows.Forms;

namespace DeskPersentaionLayer
{
    public partial class Form1 : Form
    {

        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;
        int _ContactID;
        clsContact _Contact;
        public Form1()
        {
            InitializeComponent();
        }

        private void _RefreshContactList()
        {
            dgvAllContacts.DataSource = clsContact.GetAllContact();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

            _RefreshContactList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form AddEdit = new frmAddEditContact(-1);
            AddEdit.ShowDialog();
        }

        private void dgvAllContacts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditContact frm = new frmAddEditContact((int)dgvAllContacts.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            _RefreshContactList();

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                                    "Are you sure you want to delete this contact?",
                                    "Confirm Delete",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (_Contact.DelteContact((int)dgvAllContacts.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Contact deleted successfully.");
                    _RefreshContactList();
                }
                else
                {
                    MessageBox.Show("Contact was not deleted.");
                }
            }
        }
    }
}
