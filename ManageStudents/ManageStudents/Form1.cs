using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/* Program Manage Students */

namespace ManageStudents
{
    public partial class ManageStudents : Form
    {
        string checkButton;/* Check whether the user clicks Add or Edit */

        public ManageStudents()
        {
            InitializeComponent();
        }

        /* Declaring Method of locking buttons is not needed */
        public void LockControl()
        {
            btn_Add.Enabled = true;
            btn_Edit.Enabled = true;
            btn_Remove.Enabled = true;
            btn_Save.Enabled = false;
            btn_Cancel.Enabled = false;

            txt_StudentID.ReadOnly = true;
            txt_FullName.ReadOnly = true;
            txt_Major.ReadOnly = true;
            txt_Class.ReadOnly = true;
            txt_Phone.ReadOnly = true;
            txt_AverageGrade.ReadOnly = true;

            btn_Add.Focus();
        }

        /* Declaring Method of unlocking the buttons*/
        public void UnlockControl()
        {
            btn_Add.Enabled = false;
            btn_Edit.Enabled = false;
            btn_Remove.Enabled = false;
            btn_Save.Enabled = true;
            btn_Cancel.Enabled = true;

            txt_StudentID.ReadOnly = false;
            txt_FullName.ReadOnly = false;
            txt_Major.ReadOnly = false;
            txt_Class.ReadOnly = false;
            txt_AverageGrade.ReadOnly = false;
            txt_Phone.ReadOnly = false;

            txt_StudentID.Focus();
        }

        /* Declaring Method to verify that the user entered data is complete and valid */
        public bool CheckInputData()
        {
            /* Check and notify if the user did not enter Student ID */
            if (string.IsNullOrWhiteSpace(txt_StudentID.Text))
            {
                MessageBox.Show("You are not entering Student ID. Please Enter!",
                    "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                txt_StudentID.Focus();
                
                return false;   
            }

            /* Check and notify if the user did not enter Full Name */
            if (string.IsNullOrWhiteSpace(txt_FullName.Text))
            {
                MessageBox.Show("You are not entering Full Name. Please Enter!",
                    "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                txt_FullName.Focus();
                
                return false;
            }

            /* Check and notify if the user did not enter Major */
            if (string.IsNullOrWhiteSpace(txt_Major.Text))
            {
                MessageBox.Show("You are not entering Major. Please Enter!",
                    "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                txt_Major.Focus();
                
                return false;
            }

            /* Check and notify if the user did not enter Class */
            if (string.IsNullOrWhiteSpace(txt_Class.Text))
            {
                MessageBox.Show("You are not entering Class. Please Enter!",
                    "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                txt_Class.Focus();
                
                return false;
            }

            /* Check and notify if the user did not enter Phone or value is character */
            try
            {
                if (string.IsNullOrWhiteSpace(txt_Phone.Text))
                {
                    MessageBox.Show("You are not entering Phone. Please Enter!",
                        "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txt_Phone.Focus();

                    return false;
                }

                int phone = int.Parse(txt_Phone.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Phone must be number. Please Enter Again!",
                    "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                txt_Phone.Clear();
                txt_Phone.Focus();

                return false;
            }

            /* Check and notify if the user did not enter Phone or value is character */
            try
            {
                if (string.IsNullOrWhiteSpace(txt_AverageGrade.Text))
                {
                    MessageBox.Show("You are not entering Average Grade. Please Enter!",
                        "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    txt_AverageGrade.Focus();

                    return false;
                }

                float averageGrade = float.Parse(txt_AverageGrade.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Average grade must be number. Please Enter Again!",
                    "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                txt_AverageGrade.Clear();
                txt_AverageGrade.Focus();

                return false;
            }
            return true;
        }

        /* Event load when user open program */
        private void ManageStudents_Load(object sender, EventArgs e)
        {
            LockControl();

            btn_Search.Enabled = false;
            btn_Edit.Enabled = false;
            
            txt_Search.ReadOnly = true;
            
            txt_Search.Text = "Enter Student ID to search";
        }

        /* Event click button Add */
        private void btn_Add_Click(object sender, EventArgs e)
        {
            UnlockControl();

            checkButton = "add";

            txt_StudentID.Clear();
            txt_FullName.Clear();
            txt_Major.Clear();
            txt_Class.Clear();
            txt_Phone.Clear();
            txt_AverageGrade.Clear();
            txt_Classification.Clear();
        }

        /* Event click button Edit */
        private void btn_Edit_Click(object sender, EventArgs e)
        {
            UnlockControl();

            checkButton = "edit";
        }

        /* Event click button Remove */
        private void btn_Remove_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("Do you want to delete this students ?",
                    "Confirm", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                
                if (dr == DialogResult.Yes)
                {
                    int RowIndex = dgv_Students.CurrentRow.Index;

                    dgv_Students.Rows.RemoveAt(RowIndex);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /* Event click button Save */
        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (checkButton == "add")
            {
                if (CheckInputData() == true)
                {
                    float averageGrade = float.Parse(txt_AverageGrade.Text);

                    /* Check Average grade to classification */
                    if (averageGrade >= 0 && averageGrade <= 4.99)
                    {
                        txt_Classification.Text = "Fail";
                        
                        if (CheckStudentID() == true)
                        {
                            SaveDataStudent(); /*Method Invocation*/
                        }
                    }
                    else if (averageGrade >= 5.0 && averageGrade <= 6.49)
                    {
                        txt_Classification.Text = "Pass";
                        
                        if (CheckStudentID() == true)
                        {
                            SaveDataStudent(); /*Method Invocation*/
                        }
                    }
                    else if (averageGrade >= 6.5 && averageGrade <= 7.99)
                    {
                        txt_Classification.Text = "Merit";
                        
                        if (CheckStudentID() == true)
                        {
                            SaveDataStudent(); /*Method Invocation*/
                        }
                    }
                    else if (averageGrade >= 8.0 && averageGrade <= 10)
                    {
                        txt_Classification.Text = "Distinction";
                        
                        if (CheckStudentID() == true)
                        {
                            SaveDataStudent(); /*Method Invocation*/
                        }
                    }
                    else
                    {
                        MessageBox.Show("Average grade must be from 0 to 10. Please Enter Again!",
                            "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        txt_AverageGrade.Clear();
                        txt_AverageGrade.Focus();
                    }
                }
            }

            if (checkButton == "edit")
            {
                if (CheckInputData() == true)
                {
                    float averageGrade = float.Parse(txt_AverageGrade.Text);

                    /* Check Average grade to classification */
                    if (averageGrade >= 0 && averageGrade <= 4.99)
                    {
                        txt_Classification.Text = "Fail";

                        EditDataStudent(); /*Method Invocation*/
                    }
                    else if (averageGrade >= 5.0 && averageGrade <= 6.49)
                    {
                        txt_Classification.Text = "Pass";

                        EditDataStudent(); /*Method Invocation*/
                    }
                    else if (averageGrade >= 6.5 && averageGrade <= 7.99)
                    {
                        txt_Classification.Text = "Merit";

                        EditDataStudent(); /*Method Invocation*/
                    }
                    else if (averageGrade >= 8.0 && averageGrade <= 10)
                    {
                        txt_Classification.Text = "Distinction";

                        EditDataStudent(); /*Method Invocation*/
                    }
                    else
                    {
                        MessageBox.Show("Average grade must be from 0 to 10. Please Enter Again!",
                            "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        txt_AverageGrade.Clear();
                        txt_AverageGrade.Focus();
                    }
                }
            }
        }

        /* Declaring method check Student ID already existed */
        public bool CheckStudentID()
        {
            try
            {
                if (dgv_Students.Rows.Count > 1)
                {
                    for (int i = 0; i < dgv_Students.Rows.Count; i++)
                    {
                        if (dgv_Students.Rows[i].Cells[0].Value != null 
                            && txt_StudentID.Text == dgv_Students.Rows[i].Cells[0].Value.ToString())
                            /* Check the Student ID column in dgv_Students if the data in dgv_Students is not null 
                            and the data in dgv_Students is equal to the value entered*/
                        {
                            MessageBox.Show("Student ID already existed. Please Enter Again!",
                                "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            
                            txt_StudentID.Clear();
                            txt_StudentID.Focus();

                            return false;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return true;
        }

        /* Declaring method save data student in dgv_Students */
        private void SaveDataStudent()
        {
            dgv_Students.Rows.Add(txt_StudentID.Text, txt_FullName.Text, txt_Major.Text,
                txt_Class.Text, txt_Phone.Text, txt_AverageGrade.Text, txt_Classification.Text);
            
            btn_Search.Enabled = true;
            txt_Search.ReadOnly = false;

            LockControl();
        }

        /* Declaring method edit data student in dgv_Students */
        private void EditDataStudent()
        {
            try
            {
                btn_Edit.Enabled = true;

                int RowIndex = dgv_Students.CurrentRow.Index;

                /* Display data in textbox when user click row in dgv_Students */
                dgv_Students[0, RowIndex].Value = txt_StudentID.Text;
                dgv_Students[1, RowIndex].Value = txt_FullName.Text;
                dgv_Students[2, RowIndex].Value = txt_Major.Text;
                dgv_Students[3, RowIndex].Value = txt_Class.Text;
                dgv_Students[4, RowIndex].Value = txt_Phone.Text;
                dgv_Students[5, RowIndex].Value = txt_AverageGrade.Text;
                dgv_Students[6, RowIndex].Value = txt_Classification.Text;

                LockControl();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /* Event click button Cancel */
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            txt_StudentID.Clear();
            txt_FullName.Clear();
            txt_Major.Clear();
            txt_Class.Clear();
            txt_Phone.Clear();
            txt_AverageGrade.Clear();
            txt_Classification.Clear();
        }

        /* Event click button Exit */
        private void btn_Exit_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("Are you sure to exit ?",
                    "Confirm", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                
                if (dr == DialogResult.Yes)
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void dataGirdStudent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                /* Save data row clicked */
                DataGridViewRow row = this.dgv_Students.Rows[e.RowIndex];

                /* Push data into text box */
                txt_StudentID.Text = row.Cells[0].Value.ToString();
                txt_FullName.Text = row.Cells[1].Value.ToString();
                txt_Major.Text = row.Cells[2].Value.ToString();
                txt_Class.Text = row.Cells[3].Value.ToString();
                txt_Phone.Text = row.Cells[4].Value.ToString();
                txt_AverageGrade.Text = row.Cells[5].Value.ToString();
                txt_Classification.Text = row.Cells[6].Value.ToString();
            }
        }

        /* Event click button Search */
        private void btn_Search_Click(object sender, EventArgs e)
        {
            btn_Edit.Enabled = true;
            btn_Remove.Enabled = true;
            
            string searchValue = txt_Search.Text;

            /* The searched row will be highlighted in blue */
            dgv_Students.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            
            /*Delete row was highlighted before*/
            foreach (DataGridViewRow row in dgv_Students.Rows)
            {
                row.Selected = false;
            }

            try
            {
                foreach (DataGridViewRow row in dgv_Students.Rows)
                {
                    if (row.Cells[0].Value.ToString().Equals(searchValue))
                    {
                        row.Selected = true;

                        dgv_Students.CurrentCell = dgv_Students.Rows[row.Index].Cells[0];

                        break;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("No data found. Please Enter again!", 
                    "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txt_Search.Clear();
                txt_Search.Focus();
            }
        }

        /* Event click button text box Search */
        private void txt_Search_Click(object sender, EventArgs e)
        {
            txt_Search.Text = null;
        }
    }
}