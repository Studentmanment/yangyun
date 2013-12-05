using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;

namespace 学生成绩管理系统
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
            
        }

        private void 添加学生ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.panel2.Visible = false;
            this.panel3.Visible = false;
            this.panel4.Visible = false;
            this.panel5.Visible = false;
            this.panel6.Visible = false;
            this.panel7.Visible = false;
            this.panel1.Visible = true;
            this.panel1.Dock = DockStyle.Fill;
        }

        private void btnSure_Click(object sender, EventArgs e)
        {
            if (tBoxAge.Text == "" || tBoxNo.Text == "" || tBoxClass.Text == "" || tBoxPassWord.Text == "" || tBoxName.Text == "")
            {
                MessageBox.Show("请将信息填写完整！");
                return;
            }
            DialogResult resault = MessageBox.Show("确定添加？", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (resault == DialogResult.OK)
            {
                using (SqlConnection con = new SqlConnection())
                {
                    try
                    {
                        con.ConnectionString = ConfigurationManager.ConnectionStrings["SQL"].ConnectionString.ToString();
                        if (con.State == ConnectionState.Closed) con.Open();
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = con;
                        cmd.CommandText = @" insert into Student values
                                          (
                                              @Sno,
                                              @Password,
                                              @Sname,
                                              @Sgender,
                                              @Sage,
                                              @Depart,
                                              @Class
                                          );";
                        cmd.Parameters.AddWithValue("@Sno", this.tBoxNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@Password", this.tBoxPassWord.Text.Trim());
                        cmd.Parameters.AddWithValue("@Sname", this.tBoxName.Text.Trim());
                        cmd.Parameters.AddWithValue("@Sgender", this.tBoxGender.Text.Trim());
                        cmd.Parameters.AddWithValue("@Sage", this.tBoxAge.Text.Trim());
                        cmd.Parameters.AddWithValue("@Depart", this.tBoxDepart.Text.Trim());
                        cmd.Parameters.AddWithValue("@Class", this.tBoxClass.Text.Trim());
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("添加成功！");
                    }
                    catch (Exception)
                    {

                        MessageBox.Show("添加失败！");
                    }

                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
                var open = new OpenFileDialog { Filter = "常见图片|*.jpg;*.gif;*.png;*.bmp|全部文件|*.*" };
                if (open.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image = Image.FromFile(open.FileName);
                }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        VideoWork vw;
        private void button2_Click(object sender, EventArgs e)
        {
            vw = new VideoWork(pictureBox1.Handle,0,0,pictureBox1.Width,pictureBox1.Height);
            vw.Start();
        }

        private void 删除学生ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.panel1.Visible = false;
            this.panel7.Visible = false;
            this.panel4.Visible = false;
            this.panel5.Visible = false;
            this.panel6.Visible = false;
            this.panel3.Visible = false;
            this.panel2.Visible = true;
            this.panel2.Dock = DockStyle.Fill;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (tBoxDeleteName.Text == string.Empty || tBoxDeleteNo.Text == string.Empty)
            {
                MessageBox.Show("学生姓名和学号都必须填写");
                this.tBoxDeleteName.Focus();
                return;
            }
            DialogResult result = MessageBox.Show("删除后数据不可恢复，点确定键继续", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                using (SqlConnection con=new SqlConnection())
                {
                    try
                    {
                        con.ConnectionString = ConfigurationManager.AppSettings["SQL"].ToString();
                        if (con.State == ConnectionState.Closed) con.Open();
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = con;
                        cmd.CommandText = @" delete from Student where Sno=@Sno and Sname=@Sname;";
                        cmd.Parameters.AddWithValue("@Sno", this.tBoxDeleteNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@Sname",this.tBoxDeleteName.Text.Trim());
                       int i=cmd.ExecuteNonQuery(); 
                       if (i>0)
                       {
                           MessageBox.Show("删除成功！");
                       }
                       else
                       {
                           MessageBox.Show("删除失败，请核对学生姓名和学号！");
                       }
                    }
                    catch (Exception)
                    {

                        MessageBox.Show("删除失败！");
                    }
                    
                }
            
            }
        }

        private void 修改学生信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.panel1.Visible = false;
            this.panel2.Visible = false;
            this.panel4.Visible = true;
            this.panel5.Visible = false;
            this.panel6.Visible = false;
            this.panel7.Visible = false;
            this.panel3.Visible = true;
            this.panel3.Dock = DockStyle.Fill;
            
            
        }

        public void GetDepart()
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
                cmd.CommandText = @"select  distinct Depart  from  Student";
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {

                    //this.cbBoxCourseName.Items[] = read["Cname"].ToString();
                    //this.cbBoxCourseName.Items.AddRange = read[];
                    this.cboBoxDepart.Items.Add(read["Depart"].ToString().Trim());
                }
                this.cboBoxDepart.SelectedIndex = -1;
            }
        
        }
        public void GetClass()
        {
            this.cboBoxClass.Items.Clear();
            this.cboBoxNo.Items.Clear();
            if (this.cboBoxDepart.SelectedIndex != -1)
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
                    cmd.CommandText = @"select  distinct Class  from  Student where Depart=@Depart;";
                    cmd.Parameters.AddWithValue("@Depart", this.cboBoxDepart.SelectedItem.ToString());
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {

                        //this.cbBoxCourseName.Items[] = read["Cname"].ToString();
                        //this.cbBoxCourseName.Items.AddRange = read[];
                        this.cboBoxClass.Items.Add(read["Class"].ToString().Trim());
                    }
                    this.cboBoxClass.SelectedIndex = -1;
                }
            }
        }
        public void GetStuNo()
        {
            this.cboBoxNo.Items.Clear();
            if (this.cboBoxDepart.SelectedIndex != -1&&this.cboBoxClass.SelectedIndex!=-1)
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
                    cmd.CommandText = @"select  Sno  from  Student where Depart=@Depart and Class=@Class;";
                    cmd.Parameters.AddWithValue("@Depart", this.cboBoxDepart.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Class",this.cboBoxClass.SelectedItem.ToString());
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {

                        //this.cbBoxCourseName.Items[] = read["Cname"].ToString();
                        //this.cbBoxCourseName.Items.AddRange = read[];
                        this.cboBoxNo.Items.Add(read["Sno"].ToString().Trim());
                    }
                    this.cboBoxNo.SelectedIndex = -1;
                }
            }
        
        }
        public string  ID;
        public void GetAllInfo()
        {
            if (this.cboBoxDepart.SelectedIndex != -1 && this.cboBoxClass.SelectedIndex != -1&&this.cboBoxNo.SelectedIndex!=-1)
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
                    cmd.CommandText = @"select  *  from  Student where Depart=@Depart and Class=@Class and Sno=@Sno";
                    cmd.Parameters.AddWithValue("@Depart", this.cboBoxDepart.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Class", this.cboBoxClass.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Sno",this.cboBoxNo.SelectedItem.ToString());
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {

                        //this.cbBoxCourseName.Items[] = read["Cname"].ToString();
                        //this.cbBoxCourseName.Items.AddRange = read[];
                        //this.cboBoxNo.Items.Add(read["Sno"].ToString().Trim());
                        this.tBoxAge1.Text = read["Sage"].ToString().Trim();
                        this.tBoxClass1.Text = read["Class"].ToString().Trim();
                        this.tBoxDepart1.Text = read["Depart"].ToString().Trim();
                        this.tBoxGender1.Text = read["Sgender"].ToString().Trim();
                        this.tBoxName1.Text = read["Sname"].ToString().Trim();
                        this.tBoxNo1.Text = read["Sno"].ToString().Trim();
                        ID =read["ID"].ToString();
                    }
                   // this.cboBoxNo.SelectedIndex = -1;
                }
            }
        
        }

        private void cboBoxDepart_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetClass();
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            this.GetDepart();
        }

        private void cboBoxClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetStuNo();
        }

        private void cboBoxNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetAllInfo();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("确定修改？","",MessageBoxButtons.OKCancel,MessageBoxIcon.Information);
            if(result== System.Windows.Forms.DialogResult.OK)
            {
               using(SqlConnection con=new SqlConnection())
               {
                   try 
	                    {	        
		                    con.ConnectionString=ConfigurationManager.AppSettings["SQL"].ToString();
                             if(con.State==ConnectionState.Closed) con.Open();
                             SqlCommand cmd=new SqlCommand();
                             cmd.Connection=con;
                             cmd.CommandText=@"update Student 
                                               set Sno=@sno,
                                               Sname=@Sname,
                                               Sgender=@Sgender,
                                               Sage=@Sage,
                                               Depart=@Depart,
                                               Class=@Class 
                                               where ID=@ID";
                             cmd.Parameters.AddWithValue("@sno",this.tBoxNo1.Text.ToString().Trim());
                             cmd.Parameters.AddWithValue("@Sname",this.tBoxName1.Text.Trim());
                             cmd.Parameters.AddWithValue("@Sgender",this.tBoxGender1.Text.Trim());
                             cmd.Parameters.AddWithValue("@Sage",this.tBoxAge1.Text.Trim());
                             cmd.Parameters.AddWithValue("@Depart",this.tBoxDepart1.Text.Trim());
                             cmd.Parameters.AddWithValue("@Class",this.tBoxClass1.Text.Trim());
                             cmd.Parameters.AddWithValue("@ID",ID);
                             cmd.ExecuteNonQuery();
                             MessageBox.Show("修改成功");
                             this.cboBoxClass.SelectedIndex = -1;
                             this.cboBoxDepart.SelectedIndex = -1;
                             this.cboBoxNo.SelectedIndex = -1;
                             this.tBoxNo1.Clear();
                             this.tBoxName1.Clear();
                             this.tBoxGender1.Clear();
                             this.tBoxAge1.Clear();
                             this.tBoxDepart1.Clear();
                             this.tBoxClass1.Clear();
	                    }
	                    catch (Exception)
	                    {
                            MessageBox.Show("修改失败！");
		                    
	                    }
                   
               }
            
            
            }
        }

        private void 添加教师ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.panel1.Visible = false;
            this.panel2.Visible = false;
            this.panel3.Visible = false;
            this.panel4.Visible = false;
            this.panel6.Visible = false;
            this.panel7.Visible = false;
            this.panel5.Visible = true;
            this.panel5.Dock = DockStyle.Fill;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (this.tBoxTdepart.Text == "" || this.tBoxTgender.Text == "" || this.tBoxTname.Text == "" || this.tBoxTno.Text == "" || this.tBoxTpassword.Text == "")
            {
                MessageBox.Show("请将信息填写完整！");
                return;
            }
            DialogResult result = MessageBox.Show("确定添加？","",MessageBoxButtons.OKCancel,MessageBoxIcon.Information);
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                using (SqlConnection con = new SqlConnection())
                {
                    try
                    {
                        con.ConnectionString = ConfigurationManager.ConnectionStrings["SQL"].ConnectionString.ToString();
                        if (con.State == ConnectionState.Closed) con.Open();
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = con;
                        cmd.CommandText = @" insert into Teacher values
                                          (
                                              @Teano,
                                              @Password,
                                              @Tname,
                                              @Tgender,
                                              @Tdepart,
                                              @zhicheng                                
                                          );";
                        cmd.Parameters.AddWithValue("@Teano", this.tBoxTno.Text.Trim());
                        cmd.Parameters.AddWithValue("@Password", this.tBoxTpassword.Text.Trim());
                        cmd.Parameters.AddWithValue("@Tname", this.tBoxTname.Text.Trim());
                        cmd.Parameters.AddWithValue("@Tgender", this.tBoxTgender.Text.Trim());
                        cmd.Parameters.AddWithValue("@Tdepart", this.tBoxTdepart.Text.Trim());
                        cmd.Parameters.AddWithValue("@zhicheng", this.tBoxTzhicheng.Text.Trim());
                       // cmd.Parameters.AddWithValue("@Class", this.tBoxClass.Text.Trim());
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("添加成功！");
                    }
                    catch (Exception)
                    {

                        MessageBox.Show("添加失败！");
                    }

                }
            }

        }

        private void 删除教师ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.panel1.Visible = false;
            this.panel2.Visible = false;
            this.panel3.Visible = false;
            this.panel4.Visible = false;
            this.panel5.Visible = false;
            this.panel7.Visible = false;
            this.panel6.Visible = true;
            this.panel6.Dock = DockStyle.Fill;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (this.tBoxTname1.Text == "" || this.tBoxTno1.Text == "")
            {
                MessageBox.Show("请填写教师号和姓名");
                return;
            }
            DialogResult result = MessageBox.Show("删除后数据不可恢复，点确定键继续","",MessageBoxButtons.OKCancel,MessageBoxIcon.Information);
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                using (SqlConnection con = new SqlConnection())
                {
                    try
                    {
                        con.ConnectionString = ConfigurationManager.AppSettings["SQL"].ToString();
                        if (con.State == ConnectionState.Closed) con.Open();
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = con;
                        cmd.CommandText = @" delete from Teacher where Teano=@Teano and Tname=@Tname;";
                        cmd.Parameters.AddWithValue("@Teano", this.tBoxTno1.Text.Trim());
                        cmd.Parameters.AddWithValue("@Tname", this.tBoxTname1.Text.Trim());
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            MessageBox.Show("删除成功！");
                        }
                        else
                        {
                            MessageBox.Show("删除失败，请核对教师号和姓名！");
                        }
                    }
                    catch (Exception)
                    {

                        MessageBox.Show("删除失败！");
                    }

                }
            
            
            
            }
        }

        private void 修改教师信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.panel1.Visible = false;
            this.panel2.Visible = false;
            this.panel3.Visible = false;
            this.panel4.Visible = false;
            this.panel5.Visible = false;
            this.panel6.Visible = false;
            this.panel7.Visible = true;
            this.panel7.Dock = DockStyle.Fill;
            GetTdepart();
        }
        public void GetTdepart()
        {
            this.cboBoxTdepart.Items.Clear();
            this.cboBoxTname.Items.Clear();
            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = ConfigurationManager.AppSettings["SQL"].ToString();
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = @"select  distinct Tdepart  from  Teacher";
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {

                    //this.cbBoxCourseName.Items[] = read["Cname"].ToString();
                    //this.cbBoxCourseName.Items.AddRange = read[];
                    this.cboBoxTdepart.Items.Add(read["Tdepart"].ToString().Trim());
                }
                this.cboBoxDepart.SelectedIndex = -1;
            }
        }
        public void GetTname()
        {
            this.cboBoxTname.Items.Clear();
            if (this.cboBoxTdepart.SelectedIndex != -1)
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
                    cmd.CommandText = @"select  distinct Tname  from  Teacher where Tdepart=@Tdepart;";
                    cmd.Parameters.AddWithValue("@Tdepart", this.cboBoxTdepart.SelectedItem.ToString());
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {

                        //this.cbBoxCourseName.Items[] = read["Cname"].ToString();
                        //this.cbBoxCourseName.Items.AddRange = read[];
                        this.cboBoxTname.Items.Add(read["Tname"].ToString().Trim());
                    }
                    this.cboBoxClass.SelectedIndex = -1;
                }
            }
        }
        public string TID;
        public void GetAllTInfo()
        {
            if (this.cboBoxTname.SelectedIndex != -1 && this.cboBoxTdepart.SelectedIndex != -1)
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
                    cmd.CommandText = @"select  *  from  Teacher where Tdepart=@Tdepart and Tname=@Tname";
                    cmd.Parameters.AddWithValue("@Tdepart", this.cboBoxTdepart.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Tname", this.cboBoxTname.SelectedItem.ToString());
                   // cmd.Parameters.AddWithValue("@Sno", this.cboBoxNo.SelectedItem.ToString());
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {

                        //this.cbBoxCourseName.Items[] = read["Cname"].ToString();
                        //this.cbBoxCourseName.Items.AddRange = read[];
                        //this.cboBoxNo.Items.Add(read["Sno"].ToString().Trim());
                        this.textBox1.Text = read["Tname"].ToString().Trim();
                        this.textBox2.Text = read["Teano"].ToString().Trim();
                        this.textBox3.Text = read["Tgender"].ToString().Trim();
                        this.textBox4.Text = read["Tdepart"].ToString().Trim();
                        this.textBox5.Text = read["zhicheng"].ToString().Trim();
                       // this.tBoxNo1.Text = read["Sno"].ToString().Trim();
                        TID = read["ID"].ToString();
                    }
                    // this.cboBoxNo.SelectedIndex = -1;
                }
            }
        }

        private void cboBoxTdepart_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTname();
        }

        private void cboBoxTname_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAllTInfo();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("确定修改？", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                using (SqlConnection con = new SqlConnection())
                {
                    try
                    {
                        con.ConnectionString = ConfigurationManager.AppSettings["SQL"].ToString();
                        if (con.State == ConnectionState.Closed) con.Open();
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = con;
                        cmd.CommandText = @"update Teacher 
                                               set Teano=@Teano,
                                               Tname=@Tname,
                                               Tgender=@Tgender,
                                               Tdepart=@Tdepart,
                                               zhicheng=@zhicheng                           
                                               where ID=@ID";
                        cmd.Parameters.AddWithValue("@Teano", this.textBox2.Text.ToString().Trim());
                        cmd.Parameters.AddWithValue("@Tname", this.textBox1.Text.Trim());
                        cmd.Parameters.AddWithValue("@Tgender", this.textBox3.Text.Trim());
                        cmd.Parameters.AddWithValue("@Tdepart", this.textBox4.Text.Trim());
                        cmd.Parameters.AddWithValue("@zhicheng", this.textBox5.Text.Trim());
                        //cmd.Parameters.AddWithValue("@Class", this.tBoxClass1.Text.Trim());
                        cmd.Parameters.AddWithValue("@ID", TID);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("修改成功");
                        this.cboBoxTname.SelectedIndex = -1;
                        this.cboBoxTdepart.SelectedIndex = -1;
                        this.textBox1.Clear();
                        this.textBox2.Clear();
                        this.textBox3.Clear();
                        this.textBox4.Clear();
                        this.textBox5.Clear();

                        
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("修改失败！");

                    }

                }


            }
        }

    }
    public class VideoWork
    {
        private const int WM_USER = 0x400;
        private const int WS_CHILD = 0x40000000;
        private const int WS_VISIBLE = 0x10000000;
        private const int WM_CAP_START = WM_USER;
        private const int WM_CAP_STOP = WM_CAP_START + 68;
        private const int WM_CAP_DRIVER_CONNECT = WM_CAP_START + 10;
        private const int WM_CAP_DRIVER_DISCONNECT = WM_CAP_START + 11;
        private const int WM_CAP_SAVEDIB = WM_CAP_START + 25;
        private const int WM_CAP_GRAB_FRAME = WM_CAP_START + 60;
        private const int WM_CAP_SEQUENCE = WM_CAP_START + 62;
        private const int WM_CAP_FILE_SET_CAPTURE_FILEA = WM_CAP_START + 20;
        private const int WM_CAP_SEQUENCE_NOFILE = WM_CAP_START + 63;
        private const int WM_CAP_SET_OVERLAY = WM_CAP_START + 51;
        private const int WM_CAP_SET_PREVIEW = WM_CAP_START + 50;
        private const int WM_CAP_SET_CALLBACK_VIDEOSTREAM = WM_CAP_START + 6;
        private const int WM_CAP_SET_CALLBACK_ERROR = WM_CAP_START + 2;
        private const int WM_CAP_SET_CALLBACK_STATUSA = WM_CAP_START + 3;
        private const int WM_CAP_SET_CALLBACK_FRAME = WM_CAP_START + 5;
        private const int WM_CAP_SET_SCALE = WM_CAP_START + 53;
        private const int WM_CAP_SET_PREVIEWRATE = WM_CAP_START + 52;
        private IntPtr hWndC;
        private bool bWorkStart = false;
        private IntPtr mControlPtr;
        private int mWidth;
        private int mHeight;
        private int mLeft;
        private int mTop;

        /// <summary>
        /// 初始化显示图像
        /// </summary>
        /// <param name= “handle “> 控件的句柄 </param>
        /// <param name= “left “> 开始显示的左边距 </param>
        /// <param name= “top “> 开始显示的上边距 </param>
        /// <param name= “width “> 要显示的宽度 </param>
        /// <param name= “height “> 要显示的长度 </param>
        public VideoWork(IntPtr handle, int left, int top, int width, int height)
        {
            mControlPtr = handle;
            mWidth = width;
            mHeight = height;
            mLeft = left;
            mTop = top;
        }

        [DllImport("avicap32.dll ")]
        private static extern IntPtr capCreateCaptureWindowA(byte[] lpszWindowName, int dwStyle, int x, int y, int nWidth, int nHeight, IntPtr hWndParent, int nID);

        [DllImport("avicap32.dll ")]
        private static extern int capGetVideoFormat(IntPtr hWnd, IntPtr psVideoFormat, int wSize);

        //
        //这里特别注意，因为WinAPI中的long为32位，而C#中的long为64wei，所以需要将lParam该为int
        //
        [DllImport("User32.dll ")]
        private static extern bool SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        /// <summary>
        /// 开始显示图像
        /// </summary>
        public void Start()
        {
            if (bWorkStart)
                return;

            bWorkStart = true;
            byte[] lpszName = new byte[100];

            hWndC = capCreateCaptureWindowA(lpszName, WS_CHILD | WS_VISIBLE, mLeft, mTop, mWidth, mHeight, mControlPtr, 0);

            if (hWndC.ToInt32() != 0)
            {
                SendMessage(hWndC, WM_CAP_SET_CALLBACK_VIDEOSTREAM, 0, 0);
                SendMessage(hWndC, WM_CAP_SET_CALLBACK_ERROR, 0, 0);
                SendMessage(hWndC, WM_CAP_SET_CALLBACK_STATUSA, 0, 0);
                SendMessage(hWndC, WM_CAP_DRIVER_CONNECT, 0, 0);
                SendMessage(hWndC, WM_CAP_SET_SCALE, 1, 0);
                SendMessage(hWndC, WM_CAP_SET_PREVIEWRATE, 66, 0);
                SendMessage(hWndC, WM_CAP_SET_OVERLAY, 1, 0);
                SendMessage(hWndC, WM_CAP_SET_PREVIEW, 1, 0);
                //Global.log.Write( “SendMessage “);
            }
            return;

        }

        /// <summary>
        /// 停止显示
        /// </summary>
        public void Stop()
        {
            SendMessage(hWndC, WM_CAP_DRIVER_DISCONNECT, 0, 0);
            bWorkStart = false;
        }

        /// <summary>
        /// 抓图
        /// </summary>
        /// <param name= “path “> 要保存bmp文件的路径 </param>
        public void GrabImage(string path)
        {
            IntPtr hBmp = Marshal.StringToHGlobalAnsi(path);
            SendMessage(hWndC, WM_CAP_SAVEDIB, 0, hBmp.ToInt32());
        }
    }

}

