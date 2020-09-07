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
    public partial class AddResource : Form
    {
        List<string> skills = new List<string>();
        public AddResource()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Hide();
            (new ResourceManagement()).ShowDialog();
            Close();
        }

        private void AddResource_Load(object sender, EventArgs e)
        {
            using (var context = new Session1Entities())
            {
                var getTypes = (from x in context.Resource_Type
                                select x.resTypeName).ToArray();
                cbResourceType.Items.AddRange(getTypes);

                var getSkills = (from x in context.Skills
                                 select x.skillName).ToArray();
                clbAllocated.Items.AddRange(getSkills);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtResourceName.Text) || cbResourceType.SelectedItem == null)
            {
                MessageBox.Show("Please ensure fields are filled!");
            }
            else
            {
                using (var context = new Session1Entities())
                {
                    var checkName = (from x in context.Resources
                                     where x.resName == txtResourceName.Text
                                     select x).FirstOrDefault();
                    var getTypeID = (from x in context.Resource_Type
                                     where x.resTypeName == cbResourceType.SelectedItem.ToString()
                                     select x.resTypeId).FirstOrDefault();
                    if (checkName != null)
                    {
                        MessageBox.Show("Resource name exist!");
                    }
                    else
                    {
                        if (nudQuantity.Value == 0)
                        {
                            var newResource = new Resource()
                            {
                                resName = txtResourceName.Text,
                                remainingQuantity = Convert.ToInt32(nudQuantity.Value),
                                resTypeIdFK = getTypeID
                            };
                            context.Resources.Add(newResource);
                            context.SaveChanges();
                            
                        }
                        else
                        {
                            var newResource = new Resource()
                            {
                                resName = txtResourceName.Text,
                                remainingQuantity = Convert.ToInt32(nudQuantity.Value),
                                resTypeIdFK = getTypeID
                            };
                            context.Resources.Add(newResource);
                            context.SaveChanges();

                            var getNewResID = (from x in context.Resources
                                               where x.resName == txtResourceName.Text
                                               select x.resId).FirstOrDefault();

                            foreach (var item in skills)
                            {
                                var getSkillID = (from x in context.Skills
                                                  where x.skillName == item
                                                  select x.skillId).FirstOrDefault();
                                var newAllocation = new Resource_Allocation()
                                {
                                    resIdFK = getNewResID,
                                    skillIdFK = getSkillID
                                };
                                context.Resource_Allocation.Add(newAllocation);
                                context.SaveChanges();
                            }
                            
                        }
                        MessageBox.Show("Resource Added successfully!");
                    }

                }

            }
        }

        private void nudQuantity_ValueChanged(object sender, EventArgs e)
        {
            if (nudQuantity.Value != 0)
            {
                clbAllocated.Enabled = true;
            }
            else
            {
                clbAllocated.Enabled = false;
            }
        }

        private void clbAllocated_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                if (!skills.Contains(clbAllocated.Items[e.Index].ToString()))
                {
                    skills.Add(clbAllocated.Items[e.Index].ToString());
                }
            }
            else
            {
                if (skills.Contains(clbAllocated.Items[e.Index].ToString()))
                {
                    skills.Remove(clbAllocated.Items[e.Index].ToString());
                }
            }
        }
    }
}
