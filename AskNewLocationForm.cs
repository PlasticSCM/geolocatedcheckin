using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Device.Location;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace geolocation
{
    public partial class AskNewLocationForm : Form
    {
        public AskNewLocationForm(GeoCoordinate location, string address)
        {
            mLocation = location;
            mAddress = address;

            InitializeComponent();

            NewNameTextBox.Select();
            NewNameTextBox.Focus();
        }

        internal string GetNewName()
        {
            return mResult;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(NewNameTextBox.Text))
            {
                MessageBox.Show("You must type a valid name");

                return;
            }

            mResult = NewNameTextBox.Text;

            this.Close();
        }

        private void AskNewLocationForm_Activated(object sender, EventArgs e)
        {
            LongitudeTextbox.Text = mLocation.Longitude.ToString();
            LatitudeTextbox.Text = mLocation.Latitude.ToString();
            AddressTextbox.Text = mAddress;
        }

        GeoCoordinate mLocation;
        string mAddress;
        string mResult;
    }
}
