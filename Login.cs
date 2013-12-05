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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btSure_Click(object sender, EventArgs e)
        {
           
            if (rbutStu.Checked)
            {
                SqlConnection con = new SqlConnection();
                // con.ConnectionString = ConfigurationManager.AppSettings["SQL"].ToString();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["SQL"].ConnectionString.ToString();
                if (con.State == ConnectionState.Closed) con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "select Password from Student where Sno=@Sno";
                cmd.Parameters.AddWithValue("@Sno", tboxLoginName.Text.Trim());

                try
                {

                    if (tboxPassWord.Text == cmd.ExecuteScalar().ToString().Trim())
                    {
                        cmd.Parameters.Clear();
                        StuMain Stu = new StuMain();
                        Stu.Student = this;
                        Stu.Show();
                        this.Visible = false;
                    }
                    else
                    {
                        MessageBox.Show("密码不正确！");
                        tboxPassWord.Clear();
                        tboxPassWord.Focus();
                    }

                }
                catch (Exception)
                {

                    MessageBox.Show("用户信息不正确");
                    tboxLoginName.Clear();
                    tboxPassWord.Clear();
                    tboxLoginName.Focus();
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
            else if(rbutTea.Checked)
            {
                SqlConnection con = new SqlConnection();
                // con.ConnectionString = ConfigurationManager.AppSettings["SQL"].ToString();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["SQL"].ConnectionString.ToString();
                if (con.State == ConnectionState.Closed) con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "select Password from Teacher where Teano=@Teano";
                cmd.Parameters.AddWithValue("@Teano", tboxLoginName.Text.Trim());
                string result = cmd.ExecuteScalar().ToString();
                try
                {

                    if (tboxPassWord.Text == result.Trim())
                    {
                        cmd.Parameters.Clear();
                        TeacherMain Teacher = new TeacherMain();
                        Teacher.Tea = this;
                        Teacher.Show();
                        this.Visible = false;
                    }
                    else
                    {
                        MessageBox.Show("密码不正确！");
                        tboxPassWord.Clear();
                        tboxPassWord.Focus();
                    }

                }
                catch (Exception)
                {

                    MessageBox.Show("用户信息不正确");
                    tboxLoginName.Clear();
                    tboxPassWord.Clear();
                    tboxLoginName.Focus();
                }
                finally {

                    if (con.State== ConnectionState.Open)
                    {
                        con.Close();
                    }
                
                }                 
            }
            else if (rbuAdmin.Checked)
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConfigurationManager.AppSettings["SQL"].ToString();
                if (con.State == ConnectionState.Closed) con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = @"select PassWord 
                                    from Admin
                                    where Users=@Users";
                cmd.Parameters.AddWithValue("@Users",this.tboxLoginName.Text.Trim());
                string result = cmd.ExecuteScalar().ToString();
                try
                {

                    if (tboxPassWord.Text == result.Trim())
                    {
                        cmd.Parameters.Clear();
                        //TeacherMain Teacher = new TeacherMain();
                        //Teacher.Tea = this;
                        //Teacher.Show();
                        Admin ad = new Admin();
                        ad.Show();
                        this.Visible = false;
                    }
                    else
                    {
                        MessageBox.Show("密码不正确！");
                        tboxPassWord.Clear();
                        tboxPassWord.Focus();
                    }

                }
                catch (Exception)
                {

                    MessageBox.Show("用户信息不正确");
                    tboxLoginName.Clear();
                    tboxPassWord.Clear();
                    tboxLoginName.Focus();
                }
                finally
                {

                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }

                }                 
            }
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            tboxLoginName.Clear();
            tboxPassWord.Clear();
            tboxLoginName.Focus();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }



        //private void tboxPassWord_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode== Keys.Enter)
        //    {
        //        btSure_Click(sender,e);
        //    }
        //}
    }
}
