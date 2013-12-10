using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
namespace 学生成绩管理系统
{
    public partial class StuMain : Form
    {
        public bool Psdchange = true;
        private Form1  student;

        public Form1  Student
        {
            get { return student; }
            set { student = value; }
        }
        
        public StuMain()
        {
            InitializeComponent();
        }
        protected override void OnClosing(CancelEventArgs e)
        {
           
            Application.Exit();
            base.OnClosing(e);
        }

        private void StuMain_Load(object sender, EventArgs e)
        {
           
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.AppSettings["SQL"].ToString();
            if (con.State == ConnectionState.Closed) con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select Sname from Student where Sno=@Sno";
            cmd.Parameters.AddWithValue("@Sno", this.Student.tboxLoginName.Text.Trim());
            this.Text = "欢迎您 " + cmd.ExecuteScalar().ToString();
        }

        private void 密码修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Psdchange)
            {
                this.panel2.Visible = false;
                this.panel1.Visible = false;
                this.dataGridView2.Visible = false;
                this.dataGridView1.Visible = false;
                this.tableLayoutPanelInfo.Visible = false;
                this.plPassWord.Visible = true;
                tBoxOldPsd.Focus();
            }
            else
            {
                MessageBox.Show("每次登录只能修改一次密码,请重新登录!");
            }
        }

        private void btonSure_Click(object sender, EventArgs e)
        {
            string oldPassWord = this.Student.tboxPassWord.Text.Trim();
            if (tBoxOldPsd.Text.Trim()!= oldPassWord)
            {
                MessageBox.Show("您输入的原密码不正确！");
                tBoxOldPsd.Clear();
                tBoxNewPsd1.Clear();
                tBoxNewPsd2.Clear();
                tBoxOldPsd.Focus();  
                return;
            }
            if (tBoxOldPsd.Text.Trim() == oldPassWord && tBoxNewPsd1.Text.Trim() != tBoxNewPsd2.Text.Trim())
            {
                MessageBox.Show("您两次输入的新密码不一致，请重新确认！");
                tBoxNewPsd1.Clear();
                tBoxNewPsd2.Clear();
                tBoxNewPsd1.Focus();
                return;
            }
            if (tBoxOldPsd.Text.Trim()==oldPassWord&&tBoxNewPsd1.Text==string.Empty||tBoxNewPsd2.Text==string.Empty)
            { 
                 MessageBox.Show("新密码不能为空！");
                tBoxNewPsd1.Clear();
                tBoxNewPsd2.Clear();
                tBoxNewPsd1.Focus();
                return;
            }
           
            if (oldPassWord==tBoxOldPsd.Text.Trim()&&tBoxNewPsd1.Text.Trim()==tBoxNewPsd2.Text.Trim())
            {
                try
                {
                    using (SqlConnection con = new SqlConnection())
                    {
                        con.ConnectionString = ConfigurationManager.ConnectionStrings["SQL"].ConnectionString.ToString();
                        if (con.State == ConnectionState.Closed) con.Open();
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = con;
                        cmd.CommandText = "update Student set Password=@Password where Sno=@Sno ";
                        cmd.Parameters.AddWithValue("@Password", tBoxNewPsd2.Text.Trim());
                        cmd.Parameters.AddWithValue("@Sno", this.Student.tboxLoginName.Text.Trim());
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("密码修改成功!");
                        Psdchange = false;
                        plPassWord.Visible = false;
                    }
             
                   
                }
                catch (Exception)
                {

                    MessageBox.Show("密码修改失败！");

                }
            
            }
        }

        private void btonReset_Click(object sender, EventArgs e)
        {
            this.plPassWord.Visible = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void 个人信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.panel2.Visible = false;
            this.panel1.Visible = false;
            this.dataGridView2.Visible = false;
            this.plPassWord.Visible = false;
            this.dataGridView1.Visible = false;
            this.tableLayoutPanelInfo.Visible = true;
            this.tableLayoutPanelInfo.Dock = DockStyle.Fill;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.tableLayoutPanelInfo.Visible = false;
        }

        private void tableLayoutPanelInfo_Paint(object sender, PaintEventArgs e)
        {
         
            this.tBoxNo.Text = this.Student.tboxLoginName.Text;
            using (SqlConnection con = new SqlConnection())
            {
               con.ConnectionString = ConfigurationManager.ConnectionStrings["SQL"].ConnectionString.ToString();
               if (con.State == ConnectionState.Closed) con.Open();
               SqlCommand cmd = new SqlCommand();
               cmd.Connection = con;
               cmd.CommandText = "select * from Student where Sno=@Sno";
               cmd.Parameters.AddWithValue("@Sno", this.Student.tboxLoginName.Text.Trim());
               SqlDataReader read = cmd.ExecuteReader();
            if (read.Read())
            {
                this.tBoxAge.Text = read["Sage"].ToString();
                this.tBoxClass.Text = read["Class"].ToString();
                this.tBoxDepart.Text = read["Depart"].ToString();
                this.tBoxGender.Text = read["Sgender"].ToString();
                this.tBoxName.Text = read["Sname"].ToString();
            }
            }
        }
             public  bool show1 = true;
        private void 学生选课查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.panel2.Visible = false;
            this.panel1.Visible = false;
            this.dataGridView2.Visible = false;
            this.tableLayoutPanelInfo.Visible = false;
            this.plPassWord.Visible = false;
            this.dataGridView1.Visible=true;
            this.dataGridView1.Dock = DockStyle.Fill;
            if (show1)
            {
                this.dataGridView1.Rows.Clear();
                using (SqlConnection con = new SqlConnection())
                {
                    con.ConnectionString = ConfigurationManager.AppSettings["SQL"].ToString();
                    if (con.State == ConnectionState.Closed) con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = @" select Course.*,Teacher.Tname
                                     from Course,Teacher
                                     where Course.Teano=Teacher.Teano and
                                     Course.Teano in(
                                              select Teano 
                                              from SC,Course 
                                              where Sc.Cno=Course.Cno and Sno=@Sno)";
                    cmd.Parameters.AddWithValue("@Sno", this.Student.tboxLoginName.Text.Trim());
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = cmd;
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        this.dataGridView1.Rows.Add(row["Cno"], row["Cname"], row["Teano"], row["Credit"], row["Tname"]);
                    }
                    show1 = false;
                    dt.Dispose();
                }
            }
            
        }
        public bool show2 = true;
        private void 成绩查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.panel2.Visible = false;
            this.panel1.Visible = false;
            this.dataGridView1.Visible = false;
            this.plPassWord.Visible = false;
            this.tableLayoutPanelInfo.Visible = false;
            this.dataGridView2.Visible = true;
            this.dataGridView2.Dock = DockStyle.Fill;
            if (show2)
            {
                this.dataGridView2.Rows.Clear();
                using (SqlConnection con = new SqlConnection())
                {
                    this.dataGridView2.DataSource = null;
                    con.ConnectionString = ConfigurationManager.AppSettings["SQL"].ToString();
                    if (con.State == ConnectionState.Closed) con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = @" select Course.Cname,course.Credit,SC.Grade
                                         from SC,Course
                                         where SC.Cno=Course.Cno and Sno=@Sno;";
                    cmd.Parameters.AddWithValue("@Sno", this.Student.tboxLoginName.Text.Trim());
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = cmd;
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        this.dataGridView2.Rows.Add(row["Cname"], row["Credit"], row["Grade"]);
                    }
                    show2 = false;
                    dt.Dispose();
                }
            }
        }

        public bool show3 = true;
        private void 选退课ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.panel2.Visible = false;
            this.plPassWord.Visible = false;
            this.tableLayoutPanelInfo.Visible = false;
            this.dataGridView1.Visible = false;
            this.dataGridView2.Visible = false;
            this.panel1.Visible = true;
            this.panel1.Dock = DockStyle.Fill;
            if (show3)
            {
                this.dataGridView3.Rows.Clear();
                using (SqlConnection con = new SqlConnection())
                {

                    con.ConnectionString = ConfigurationManager.AppSettings["SQL"].ToString();
                    if (con.State == ConnectionState.Closed) con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = @" select Course.*,Teacher.Tname
                                     from Course,Teacher
                                     where Course.Teano=Teacher.Teano and
                                     Course.Teano in(
                                              select Teano 
                                              from SC,Course 
                                              where Sc.Cno=Course.Cno and Sno=@Sno)";
                    cmd.Parameters.AddWithValue("@Sno", this.Student.tboxLoginName.Text.Trim());
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = cmd;
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {

                        this.dataGridView3.Rows.Add(row["Cno"], row["Cname"], row["Tname"]);
                    }
                    show3 = false;
                    dt.Dispose();
            }
           
            }
        }

        private void dataGridView3_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (dgv.SelectedRows.Count != 0)
            {
               
                this.tBoxDrop.Text = dgv.SelectedRows[0].Cells["CourseName"].Value.ToString();
                strCno = dgv.SelectedRows[0].Cells["Courseno"].Value.ToString();
            }
            else
            {
                this.tBoxDrop.Text = string.Empty;
            }
              
                 
        }
        public string strCno;
        private void btonDrop_Click(object sender, EventArgs e)
        {
            if (this.dataGridView3.SelectedRows.Count != 0)
            {
                DialogResult resault = MessageBox.Show("确定退选？", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (resault == DialogResult.OK)
                {
                    using (SqlConnection con = new SqlConnection())
                    {

                        try
                        {
                            con.ConnectionString = ConfigurationManager.AppSettings["SQL"].ToString();
                            if (con.State == ConnectionState.Closed) con.Open();
                            SqlCommand cmd = new SqlCommand();
                            cmd.Connection = con;
                            cmd.CommandText = @"delete from SC where Sno=@Sno and Cno=@Cno; ";
                            cmd.Parameters.AddWithValue("@Sno", this.Student.tboxLoginName.Text.Trim());
                            cmd.Parameters.AddWithValue("@Cno", strCno);
                            cmd.ExecuteNonQuery();
                            this.dataGridView3.Rows.RemoveAt(dataGridView3.SelectedRows[0].Index);
                            show1 = true;
                            show2 = true;
                            show4 = true;
                        }
                        catch (Exception)
                        {

                            throw;
                        }
                    }
                }

            }
            else {
                MessageBox.Show("你没有课程可退！");
            }
           
        }

        private void btonCancel_Click(object sender, EventArgs e)
        {
            this.panel1.Visible = false;
        }

        private void btonClose_Click(object sender, EventArgs e)
        {
            this.panel2.Visible = false;
        }

        public bool show4 = true;
        private void 选课ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.plPassWord.Visible = false;
            this.tableLayoutPanelInfo.Visible = false;
            this.dataGridView1.Visible = false;
            this.dataGridView2.Visible = false;
            this.panel1.Visible = false;
            this.panel2.Visible = true;
            this.panel2.Dock = DockStyle.Fill;
            if (show4)
            {
                this.dataGridView4.Rows.Clear();
                using (SqlConnection con = new SqlConnection())
                {

                    con.ConnectionString = ConfigurationManager.AppSettings["SQL"].ToString();
                    if (con.State == ConnectionState.Closed) con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = @" select Course.Cno,Course.Cname,Teacher.Tname
                                         from Course,Teacher
                                         where Course.Teano=Teacher.Teano and Cno not in
                                        (
                                            select Cno
                                            from SC
                                            where Sno=@Sno
                                        );";
                    cmd.Parameters.AddWithValue("@Sno", this.Student.tboxLoginName.Text.Trim());
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = cmd;
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                       
                        this.dataGridView4.Rows.Add(row["Cno"], row["Cname"], row["Tname"]);
                    }
                    show4 = false;
                    dt.Dispose();
                }

            }
        }

        private void dataGridView4_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (dgv.SelectedRows.Count != 0)
            {
                this.tBoxSure1.Text = dgv.SelectedRows[0].Cells["CourseName1"].Value.ToString();
                strCno1 = dgv.SelectedRows[0].Cells["Courseno1"].Value.ToString();
            }
            else
            {
                this.tBoxSure1.Text = string.Empty;
            }
        }
        public string strCno1;
        private void btonSure1_Click(object sender, EventArgs e)
        {
             if (this.dataGridView4.SelectedRows.Count!=0)
             {

                 DialogResult resault = MessageBox.Show("确定选择这门课吗？", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                 if (resault == DialogResult.OK)
                 {
                     using (SqlConnection con = new SqlConnection())
                     {

                         try
                         {
                             con.ConnectionString = ConfigurationManager.AppSettings["SQL"].ToString();
                             if (con.State == ConnectionState.Closed) con.Open();
                             SqlCommand cmd = new SqlCommand();
                             cmd.Connection = con;
                             cmd.CommandText = @"insert into SC
                                              values(
                                                 @Sno,
                                                 @Cno,
                                                 null
                                              ) ";
                             cmd.Parameters.AddWithValue("@Sno", this.Student.tboxLoginName.Text.Trim());
                             cmd.Parameters.AddWithValue("@Cno", strCno1);
                             cmd.ExecuteNonQuery();
                             this.dataGridView4.Rows.RemoveAt(dataGridView4.SelectedRows[0].Index);
                             show1 = true;
                             show2 = true;
                             show3 = true;
                         }
                         catch (Exception)
                         {

                             throw;
                         }
                     }
                 }
             }
             else
             {
                 MessageBox.Show("没有课程可选！");
             }
        }
  
    }
}
