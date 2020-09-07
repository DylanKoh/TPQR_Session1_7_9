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
    public partial class CreateAccount : Form
    {
        public CreateAccount()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Hide();
            (new MainMenu()).ShowDialog();
            Close();
        }

        private void CreateAccount_Load(object sender, EventArgs e)
        {
            using (var context = new Session1Entities())
            {
                var getTypes = (from x in context.User_Type
                                select x.userTypeName).ToArray();
                cbUserType.Items.AddRange(getTypes);
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUserName.Text) || string.IsNullOrWhiteSpace(txtUserID.Text) || string.IsNullOrWhiteSpace(txtPassword.Text) || string.IsNullOrWhiteSpace(txtRePassword.Text) || cbUserType.SelectedItem == null)
            {
                MessageBox.Show("Please ensure that all fields are filled up!");
            }
            else if (txtUserID.TextLength < 8)
            {
                MessageBox.Show("Ensure that User ID is at least 8 characters long!");
            }
            else if (txtPassword.Text != txtRePassword.Text)
            {
                MessageBox.Show("Passwords do not match!");
            }
            else
            {
                using (var context = new Session1Entities())
                {
                    var getUser = (from x in context.Users
                                   where x.userId == txtUserID.Text
                                   select x).FirstOrDefault();
                    if (getUser != null)
                    {
                        MessageBox.Show("User ID taken!");
                    }
                    else
                    {
                        var getUserTypeID = (from x in context.User_Type
                                             where x.userTypeName == cbUserType.SelectedItem.ToString()
                                             select x.userTypeId).FirstOrDefault();
                        var newUser = new User()
                        {
                            userId = txtUserID.Text,
                            userName = txtUserName.Text,
                            userPw = txtPassword.Text,
                            userTypeIdFK = getUserTypeID
                        };
                        context.Users.Add(newUser);
                        context.SaveChanges();
                        MessageBox.Show("User account created successfully!");
                        Hide();
                        (new MainMenu()).ShowDialog();
                        Close();
                    }
                }
            }
        }
    }
}
