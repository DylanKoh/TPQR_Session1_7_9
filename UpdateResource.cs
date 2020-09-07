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
    public partial class UpdateResource : Form
    {
        int _resID = 0;
        Resource currentResource;
        List<string> skills = new List<string>();
        public UpdateResource(int ResID)
        {
            InitializeComponent();
            _resID = ResID;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Hide();
            (new ResourceManagement()).ShowDialog();
            Close();
        }

        private void UpdateResource_Load(object sender, EventArgs e)
        {
            using (var context = new Session1Entities())
            {
                currentResource = (from x in context.Resources
                                   where x.resId == _resID
                                   select x).FirstOrDefault();
                lblResourceName.Text = currentResource.resName;
                lblResourceType.Text = currentResource.Resource_Type.resTypeName;
                var getCurrentSkills = (from x in context.Resource_Allocation
                                        where x.resIdFK == _resID
                                        select x);
                var getSkills = (from x in context.Skills
                                 select x.skillName).ToArray();
                clbAllocated.Items.AddRange(getSkills);
                foreach (var item in getCurrentSkills)
                {
                    skills.Add(item.Skill.skillName);
                    var getIndex = clbAllocated.Items.IndexOf(item.Skill.skillName);
                    clbAllocated.SetItemChecked(getIndex, true);
                }
                nudQuantity.Value = currentResource.remainingQuantity;
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            using (var context = new Session1Entities())
            {
                var getResource = (from x in context.Resources
                                   where x.resId == _resID
                                   select x).FirstOrDefault();

                var getCurrentAllocation = (from x in context.Resource_Allocation
                                            where x.resIdFK == _resID
                                            select x);
                var listToDelete = new List<Resource_Allocation>();
                foreach (var item in getCurrentAllocation)
                {
                    listToDelete.Add(item);
                }
                foreach (var item in listToDelete)
                {
                    context.Resource_Allocation.Remove(item);
                    context.SaveChanges();
                }
                if (nudQuantity.Value == 0)
                {
                    getResource.remainingQuantity = Convert.ToInt32(nudQuantity.Value);
                    context.SaveChanges();

                }
                else
                {
                    getResource.remainingQuantity = Convert.ToInt32(nudQuantity.Value);
                    context.SaveChanges();

                    foreach (var item in skills)
                    {
                        var getSkillID = (from x in context.Skills
                                          where x.skillName == item
                                          select x.skillId).FirstOrDefault();
                        var newAllocation = new Resource_Allocation()
                        {
                            resIdFK = _resID,
                            skillIdFK = getSkillID
                        };
                        context.Resource_Allocation.Add(newAllocation);
                        context.SaveChanges();
                    }

                }
                MessageBox.Show("Resource Updated successfully!");
                Hide();
                (new ResourceManagement()).ShowDialog();
                Close();
            }


        }
    }
}
