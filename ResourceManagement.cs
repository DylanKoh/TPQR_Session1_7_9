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
    public partial class ResourceManagement : Form
    {
        public ResourceManagement()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Hide();
            (new Login()).ShowDialog();
            Close();
        }

        private void ResourceManagement_Load(object sender, EventArgs e)
        {
            LoadComboBox();
            LoadData();
        }

        private void LoadData()
        {
            dataGridView1.Rows.Clear();
            using (var context = new Session1Entities())
            {
                var getResource = (from x in context.Resources
                                   orderby x.remainingQuantity descending
                                   select x);
                if (cbSkill.SelectedItem.ToString() == "No Filter" && (cbType.SelectedItem == null || cbType.SelectedItem.ToString() == "No Filter"))
                {
                    foreach (var item in getResource)
                    {
                        var newRow = new List<string>()
                        {
                            item.resId.ToString(), item.resName, item.Resource_Type.resTypeName, item.Resource_Allocation.Count().ToString()

                        };
                        var getAllocations = (from x in context.Resource_Allocation
                                              where x.resIdFK == item.resId
                                              select x);
                        var sb = new StringBuilder("");
                        foreach (var allocations in getAllocations)
                        {
                            if (sb.ToString() == "")
                            {
                                sb.Append(allocations.Skill.skillName);
                            }
                            else
                            {
                                sb.Append($", {allocations.Skill.skillName}");
                            }
                        }
                        if (sb.ToString() == "")
                        {
                            newRow.Add("Nil");
                        }
                        else
                        {
                            newRow.Add(sb.ToString());
                        }
                        if (item.remainingQuantity > 5)
                        {
                            newRow.Add("Sufficient");
                        }
                        else if (item.remainingQuantity >= 1 && item.remainingQuantity <= 5)
                        {
                            newRow.Add("Low Stock");
                        }
                        else
                        {
                            newRow.Add("Not Available");
                        }
                        dataGridView1.Rows.Add(newRow.ToArray());
                    }
                }
                else if (cbSkill.SelectedItem.ToString() != "No Filter" && cbType.SelectedItem.ToString() == "No Filter")
                {
                    var getSkillID = (from x in context.Skills
                                      where x.skillName == cbSkill.SelectedItem.ToString()
                                      select x.skillId).FirstOrDefault();
                    foreach (var item in getResource)
                    {
                        var checkSkills = context.Resource_Allocation.Where(x => x.resIdFK == item.resId && x.skillIdFK == getSkillID).Select(x => x).FirstOrDefault();

                        if (checkSkills == null)
                        {
                            continue;
                        }
                        else
                        {
                            var newRow = new List<string>()
                        {
                            item.resId.ToString(), item.resName, item.Resource_Type.resTypeName, item.Resource_Allocation.Count().ToString()

                        };
                            var getAllocations = (from x in context.Resource_Allocation
                                                  where x.resIdFK == item.resId
                                                  select x);
                            var sb = new StringBuilder("");
                            foreach (var allocations in getAllocations)
                            {
                                if (sb.ToString() == "")
                                {
                                    sb.Append(allocations.Skill.skillName);
                                }
                                else
                                {
                                    sb.Append($", {allocations.Skill.skillName}");
                                }
                            }
                            if (sb.ToString() == "")
                            {
                                newRow.Add("Nil");
                            }
                            else
                            {
                                newRow.Add(sb.ToString());
                            }
                            if (item.remainingQuantity > 5)
                            {
                                newRow.Add("Sufficient");
                            }
                            else if (item.remainingQuantity >= 1 && item.remainingQuantity <= 5)
                            {
                                newRow.Add("Low Stock");
                            }
                            else
                            {
                                newRow.Add("Not Available");
                            }
                            dataGridView1.Rows.Add(newRow.ToArray());
                        }

                    }
                }
                else if (cbSkill.SelectedItem.ToString() == "No Filter" && cbType.SelectedItem.ToString() != "No Filter")
                {
                    var getTypeID = (from x in context.Resource_Type
                                     where x.resTypeName == cbType.SelectedItem.ToString()
                                     select x.resTypeId).FirstOrDefault();
                    var filtered = getResource.Where(x => x.resTypeIdFK == getTypeID);
                    foreach (var item in filtered)
                    {


                        var newRow = new List<string>()
                        {
                            item.resId.ToString(), item.resName, item.Resource_Type.resTypeName, item.Resource_Allocation.Count().ToString()

                        };
                        var getAllocations = (from x in context.Resource_Allocation
                                              where x.resIdFK == item.resId
                                              select x);
                        var sb = new StringBuilder("");
                        foreach (var allocations in getAllocations)
                        {
                            if (sb.ToString() == "")
                            {
                                sb.Append(allocations.Skill.skillName);
                            }
                            else
                            {
                                sb.Append($", {allocations.Skill.skillName}");
                            }
                        }
                        if (sb.ToString() == "")
                        {
                            newRow.Add("Nil");
                        }
                        else
                        {
                            newRow.Add(sb.ToString());
                        }
                        if (item.remainingQuantity > 5)
                        {
                            newRow.Add("Sufficient");
                        }
                        else if (item.remainingQuantity >= 1 && item.remainingQuantity <= 5)
                        {
                            newRow.Add("Low Stock");
                        }
                        else
                        {
                            newRow.Add("Not Available");
                        }
                        dataGridView1.Rows.Add(newRow.ToArray());
                    }

                }
                else
                {
                    var getSkillID = (from x in context.Skills
                                      where x.skillName == cbSkill.SelectedItem.ToString()
                                      select x.skillId).FirstOrDefault();
                    var getTypeID = (from x in context.Resource_Type
                                     where x.resTypeName == cbType.SelectedItem.ToString()
                                     select x.resTypeId).FirstOrDefault();
                    var filtered = getResource.Where(x => x.resTypeIdFK == getTypeID);
                    foreach (var item in filtered)
                    {
                        var checkSkills = context.Resource_Allocation.Where(x => x.resIdFK == item.resId && x.skillIdFK == getSkillID).Select(x => x).FirstOrDefault();

                        if (checkSkills == null)
                        {
                            continue;
                        }
                        var newRow = new List<string>()
                        {
                            item.resId.ToString(), item.resName, item.Resource_Type.resTypeName, item.Resource_Allocation.Count().ToString()

                        };
                        var getAllocations = (from x in context.Resource_Allocation
                                              where x.resIdFK == item.resId
                                              select x);
                        var sb = new StringBuilder("");
                        foreach (var allocations in getAllocations)
                        {
                            if (sb.ToString() == "")
                            {
                                sb.Append(allocations.Skill.skillName);
                            }
                            else
                            {
                                sb.Append($", {allocations.Skill.skillName}");
                            }
                        }
                        if (sb.ToString() == "")
                        {
                            newRow.Add("Nil");
                        }
                        else
                        {
                            newRow.Add(sb.ToString());
                        }
                        if (item.remainingQuantity > 5)
                        {
                            newRow.Add("Sufficient");
                        }
                        else if (item.remainingQuantity >= 1 && item.remainingQuantity <= 5)
                        {
                            newRow.Add("Low Stock");
                        }
                        else
                        {
                            newRow.Add("Not Available");
                        }
                        dataGridView1.Rows.Add(newRow.ToArray());
                    }
                }
                foreach (DataGridViewRow item in dataGridView1.Rows)
                {
                    if (dataGridView1[5, item.Index].Value.ToString() == "Not Available")
                    {
                        dataGridView1.Rows[item.Index].DefaultCellStyle.BackColor = Color.Red;
                    }
                }
            }
        }

        private void LoadComboBox()
        {
            cbSkill.Items.Clear();
            cbType.Items.Clear();
            cbSkill.Items.Add("No Filter");
            cbType.Items.Add("No Filter");
            cbSkill.SelectedIndex = 0;
            cbType.SelectedIndex = 0;
            using (var context = new Session1Entities())
            {
                var getSkills = (from x in context.Skills
                                 select x.skillName).ToArray();
                cbSkill.Items.AddRange(getSkills);

                var getType = (from x in context.Resource_Type
                               select x.resTypeName).ToArray();
                cbType.Items.AddRange(getType);

            }
        }

        private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void cbSkill_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Hide();
            (new AddResource()).ShowDialog();
            Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Please select a resource!");
            }
            else
            {
                var getResourceID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                (new UpdateResource(getResourceID)).ShowDialog();
                Close();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Please select a resource!");
            }
            else
            {
                var getResourceID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                using (var context =new Session1Entities())
                {
                    var getResource = (from x in context.Resources
                                       where x.resId == getResourceID
                                       select x).FirstOrDefault();
                    var result = MessageBox.Show("Are you sure you want to delete this resource?", "Delete Resource",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        context.Resources.Remove(getResource);
                        context.SaveChanges();
                        MessageBox.Show("Resource deleted successfully!");
                        dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
                    }
                }
            }
        }
    }
}
