using ContactsBusinessLayer;
using CountryDataAccess;
using System.Data;
using System.Windows.Forms;

namespace DeskPersentaionLayer
{
    public partial class frmAddEditContact : Form
    {

        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;
        int _ContactID;
        clsContact _Contact;


        public frmAddEditContact(int ContactID)
        {
            InitializeComponent();

            _ContactID = ContactID;
            if (_ContactID == -1)
                _Mode = enMode.AddNew;
            else
                _Mode = enMode.Update;
        }

        private void AddEditContact_Load(object sender, System.EventArgs e)
        {
            _LoadData();
        }

        private void _FillCountriesInComboBoX()
        {
            DataTable dtCountries = clsCountry.GetAllCountries();

            foreach (DataRow row in dtCountries.Rows)
            {
                cbCountry.Items.Add(row["CountryName"]);
            }

        }

        private void _LoadData()
        {
            _FillCountriesInComboBoX();
            cbCountry.SelectedIndex = 0;
            if (_Mode == enMode.AddNew)
            {
                lblMode.Text = "Add New Contact";
                _Contact = new clsContact();
                return;
            }
            _Contact = clsContact.Find(_ContactID);

            if (_Contact == null)
            {
                MessageBox.Show("this form will be closed because no contact Found");
                this.Close();
                return;
            }

            lblMode.Text = "Edit contact ID = " + _ContactID;
            lblContactID.Text = _ContactID.ToString();
            txtFirstName.Text = _Contact.FirstName;
            txtLastName.Text = _Contact.LastName;
            txtEmail.Text = _Contact.Email;
            txtPhone.Text = _Contact.Phone;
            txtAddress.Text = _Contact.Address;
            DtpDateOfBirth.Value = _Contact.DateOfBirth;

            if (_Contact.ImagePath != "")
            {
                pictureBox1.Load(_Contact.ImagePath);
            }

            llRemoveImage.Visible = (_Contact.ImagePath != "");

            cbCountry.SelectedIndex = cbCountry.FindString(clsCountry.Find(_Contact.CountryID).CountryName);
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            int CountryID = clsCountry.Find(cbCountry.Text).ID;

            _Contact.FirstName = txtFirstName.Text;
            _Contact.LastName = txtLastName.Text;
            _Contact.Email = txtEmail.Text;
            _Contact.Phone = txtPhone.Text;
            _Contact.Address = txtAddress.Text;
            _Contact.DateOfBirth = DtpDateOfBirth.Value;
            _Contact.CountryID = CountryID;

            if (pictureBox1.ImageLocation != null)
            {
                _Contact.ImagePath = pictureBox1.ImageLocation.ToString();
            }
            else
            {
                _Contact.ImagePath = "";
            }

            if (_Contact.Save())
            {
                MessageBox.Show("Data saved successfuly");
            }
            else
            {
                MessageBox.Show("Error : data is not saved successfuly ");
            }

            _Mode = enMode.Update;
            lblMode.Text = $"Edit Contact id {_Contact.ID}";
            lblContactID.Text = _Contact.ID.ToString();
        }

        private string _ImagePath;
        private void llSet_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog OpenFile = new OpenFileDialog();

            OpenFile.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

            if (OpenFile.ShowDialog() == DialogResult.OK)
            {
                _ImagePath = OpenFile.FileName;
                pictureBox1.ImageLocation = _ImagePath;
            }

        }

        private void llRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _ImagePath = null;
            pictureBox1.Image = null;
            llRemoveImage.Visible = false;
        }

        //struct CountryItem()
        //{
        //    public string Text;
        //    public int value;

        //    public CountryItem(string Text, int value)
        //    {
        //        this.Text = Text;
        //        this.value = value;
        //    }
        //}

    }
}
