using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TPQR_Session1_7_9
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUserID.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Please ensure that fields are not empty!");
            }
            else
            {
                using (var context = new Session1Entities())
                {
                    var getUser = (from x in context.Users
                                   where x.userId == txtUserID.Text
                                   select x).FirstOrDefault();
                    if (getUser == null)
                    {
                        MessageBox.Show("User not found!");
                    }
                    else if (getUser.userPw != txtPassword.Text)
                    {
                        MessageBox.Show("Incorrect password or User ID!");
                    }
                    else
                    {
                        if (getUser.userTypeIdFK == 2)
                        {
                            MessageBox.Show($"Welcome {getUser.userName}!");
                            Hide();
                            (new ResourceManagement()).ShowDialog();
                            Close();
                        }
                    }
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Hide();
            (new MainMenu()).ShowDialog();
            Close();
        }
    }
}
