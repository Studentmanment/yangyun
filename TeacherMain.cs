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
    public partial class TeacherMain : Form
    {
        private Form1 tea;
        public Form1 Tea
        {
            get { return tea; }
            set { tea = value; }
        }
        
        public TeacherMain()
        {
            InitializeComponent();
        }

        private void TeacherMain_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.AppSettings["SQL"].ToString();
            if (con.State == ConnectionState.Closed) con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select Tname from Teacher where Teano=@Teano";
            cmd.Parameters.AddWithValue("@Teano", this.Tea.tboxLoginName.Text.Trim());
            this.Text = "欢迎您 " + cmd.ExecuteScalar().ToString();

            GetTeacherInfo();
            GetScInfo();
            GetCourseChose();
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            Application.Exit();
            base.OnClosing(e);
        }

       
        private void btonSure_Click(object sender, EventArgs e)
        {
            string oldPassWord;
            using (SqlConnection con=new SqlConnection())
            {
                con.ConnectionString = ConfigurationManager.AppSettings["SQL"].ToString();
                if (con.State == ConnectionState.Closed) con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "select Password from Teacher where Teano=@Teano;";
                cmd.Parameters.AddWithValue("@Teano",this.Tea.tboxLoginName.Text.Trim());
                oldPassWord = cmd.ExecuteScalar().ToString().Trim();
            }
             
            if (tBoxOldPsd.Text.Trim() != oldPassWord)
            {
                MessageBox.Show("您输入的原密码不正确！");
                tBoxOldPsd.Clear();
                tBoxNewPsd1.Clear();
                tBoxNewPsd2.Clear();
                tBoxOldPsd.Focus();
                return;
            }
            if (tBoxOldPsd.Text.Trim()==oldPassWord&&tBoxNewPsd1.Text.Trim()!= tBoxNewPsd2.Text.Trim())
            {
                MessageBox.Show("您两次输入的新密码不一致，请重新确认！");
                tBoxNewPsd1.Clear();
                tBoxNewPsd2.Clear();
                tBoxNewPsd1.Focus();
                return;
            }
            if (tBoxOldPsd.Text.Trim() == oldPassWord && tBoxNewPsd1.Text == string.Empty || tBoxNewPsd2.Text == string.Empty)
            {
                MessageBox.Show("新密码不能为空！");
                tBoxNewPsd1.Clear();
                tBoxNewPsd2.Clear();
                tBoxNewPsd1.Focus();
                return;
            }

            if (oldPassWord == tBoxOldPsd.Text.Trim() && tBoxNewPsd1.Text.Trim() == tBoxNewPsd2.Text.Trim())
            {
                try
                {
                    using (SqlConnection con = new SqlConnection())
                    {
                        con.ConnectionString = ConfigurationManager.ConnectionStrings["SQL"].ConnectionString.ToString();
                        if (con.State == ConnectionState.Closed) con.Open();
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = con;
                        cmd.CommandText = "update Teacher set Password=@Password where Teano=@Teano ";
                        cmd.Parameters.AddWithValue("@Password", tBoxNewPsd2.Text.Trim());
                        cmd.Parameters.AddWithValue("@Teano", this.Tea.tboxLoginName.Text.Trim());
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("密码修改成功!");
                        this.tBoxNewPsd1.Clear();
                        this.tBoxNewPsd2.Clear();
                        this.tBoxOldPsd.Clear();
                        this.Focus();
                    }


                }
                catch (Exception)
                {

                    MessageBox.Show("密码修改失败！");

                }

            }
        }

        private void btonCancel_Click(object sender, EventArgs e)
        {
            this.tBoxNewPsd1.Clear();
            this.tBoxNewPsd2.Clear();
            this.tBoxOldPsd.Clear();
            this.tBoxOldPsd.Focus();
        }

        private void GetTeacherInfo()
        {
            using (SqlConnection con=new SqlConnection())
            {
                con.ConnectionString = ConfigurationManager.AppSettings["SQL"].ToString();
                if (con.State == ConnectionState.Closed) con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "select * from Teacher where Teano=@Teano";
                cmd.Parameters.AddWithValue("@Teano",this.Tea.tboxLoginName.Text.Trim());
                SqlDataReader read = cmd.ExecuteReader();
                if (read.Read())
                {
                    this.tBoxName.Text = read["Tname"].ToString();
                    this.tBoxTeano.Text = read["Teano"].ToString();
                    this.tBoxDepart.Text = read["Tdepart"].ToString();
                    this.tBoxGender.Text = read["Tgender"].ToString();
                    this.tBoxZhicheng.Text = read["zhicheng"].ToString();
                
                }

            }
        
        
        
        }

        private void GetScInfo()
        {

            using (SqlConnection con = new SqlConnection())
            {

                con.ConnectionString = ConfigurationManager.AppSettings["SQL"].ToString();
                if (con.State == ConnectionState.Closed) con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = @" select Cno,Cname,Credit
                                    from Course
                                    where Teano=@Teano;";
                cmd.Parameters.AddWithValue("@Teano", this.Tea.tboxLoginName.Text.Trim());
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    this.dataGridView1.Rows.Add(row["Cno"], row["Cname"], row["Credit"]);
                }
            }
              
        }

        private void GetCourseChose()
        {
            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = ConfigurationManager.AppSettings["SQL"].ToString();
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "select Cname from Course where Teano=@Teano";
                cmd.Parameters.AddWithValue("@Teano", this.Tea.tboxLoginName.Text.Trim());
                SqlDataReader read = cmd.ExecuteReader();
                while(read.Read())
                {

                    //this.cbBoxCourseName.Items[] = read["Cname"].ToString();
                    //this.cbBoxCourseName.Items.AddRange = read[];
                    this.cbBoxCourseName.Items.Add(read["Cname"].ToString().Trim());
                }
                this.cbBoxCourseName.SelectedIndex = 0;
            }
            
        
        }

        private void cbBoxCourseName_SelectedValueChanged(object sender, EventArgs e)
        {
            using (SqlConnection con=new SqlConnection())
            {
                this.dataGridView2.Rows.Clear();
                con.ConnectionString = ConfigurationManager.AppSettings["SQL"].ToString();
                if (con.State == ConnectionState.Closed) con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection= con;
                cmd.CommandText=@"select Student.Sno,Sname,Class,Grade
                                  from Student,SC
                                  where Student.Sno=SC.Sno and Cno=
                                  (
                                      select Cno 
                                      from Course
                                      where Cname=@Cname);";
                cmd.Parameters.AddWithValue("@Cname",cbBoxCourseName.SelectedItem.ToString().Trim());
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand=cmd;
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                foreach (DataRow  row in dt.Rows)
                {
                    this.dataGridView2.Rows.Add(row["Sname"],row["Sno"],row["Class"],row["Grade"]);
                   
                }
            }
            
        }

        private void btonAdd_Click(object sender, EventArgs e)
        {
            if (this.tBoxAdd.Text.ToString().Trim() == "")
            {
                MessageBox.Show("输入的成绩不能为空");
                this.tBoxAdd.Focus();
                return;
            }
            int AddScore=Convert.ToInt32(this.tBoxAdd.Text.ToString());
            if (AddScore>100||AddScore<0)
            {
                this.tBoxAdd.Clear();
                this.tBoxAdd.Focus();
                MessageBox.Show("成绩应该在1至100之间");
            }
            else{
                try
                {
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = ConfigurationManager.AppSettings["SQL"].ToString();
                    if (con.State == ConnectionState.Closed) con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = @"update SC set Grade=@Grade 
                                  where Sno=@Sno and Cno=(
                                  select Cno
                                  from Course
                                   where Cname=@Cname  
                                  );";
                    cmd.Parameters.AddWithValue("@Grade", this.tBoxAdd.Text.ToString().Trim());
                    cmd.Parameters.AddWithValue("@Sno", this.dataGridView2.SelectedRows[0].Cells["Sno"].Value.ToString().Trim());
                    cmd.Parameters.AddWithValue("@Cname", this.cbBoxCourseName.SelectedItem.ToString().Trim());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("成绩添加更改成功！");
                    this.tBoxAdd.Clear();
                    this.tBoxAdd.Focus();
                    cbBoxCourseName_SelectedValueChanged(sender, e);
                }
                catch (Exception)
                {
                    
                    throw;
                }
               
            }
        }
        
    }
}
