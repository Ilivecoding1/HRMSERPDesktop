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

namespace DeepakERPSystem
{
    public partial class Employee_Master : Form
    {
        SqlConnection conmmc = new SqlConnection(SqlconnMain.sbmmc);//for mmc database
        SqlConnection connpayroll = new SqlConnection(SqlconnPayroll.sbpayroll);//for current database
        SqlConnection conbackyrpayroll = new SqlConnection(SqlconnPayrollbackdb.sbbackpayroll);//for backyear database database
        public Employee_Master()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void Employee_Master_Load(object sender, EventArgs e)
        {
            try
            {
                loadbasiccontrol();
                getcountrylist();
                getstatelist();
                getcountrylist1();
                getstatelist1();
                getunitlist();
            }
            catch(Exception ee)
            {
                MessageBox.Show("Can not open employee master due to" + ee.ToString());
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog selectfile = new OpenFileDialog();
            selectfile.Filter = "Image Files(*.jpg;)|*.jpg";
            if (selectfile.ShowDialog() == DialogResult.OK)
            {
                lbladharpath.Text = selectfile.FileName;
                pictureBox2adhar.Image = new Bitmap(selectfile.FileName);
                pictureBox2adhar.SizeMode = PictureBoxSizeMode.StretchImage;

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog selectfile = new OpenFileDialog();
            selectfile.Filter = "Image Files(*.jpg;)|*.jpg";
            if (selectfile.ShowDialog() == DialogResult.OK)
            {
                lblpanpath.Text = selectfile.FileName;
                pictureBox3pan.Image = new Bitmap(selectfile.FileName);
                pictureBox3pan.SizeMode = PictureBoxSizeMode.StretchImage;

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog selectfile = new OpenFileDialog();
            selectfile.Filter = "Image Files(*.jpg;)|*.jpg";
            if (selectfile.ShowDialog() == DialogResult.OK)
            {
                lblprofilepath.Text = selectfile.FileName;
                pictureBox4profile.Image = new Bitmap(selectfile.FileName);
                pictureBox4profile.SizeMode = PictureBoxSizeMode.StretchImage;

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog selectfile = new OpenFileDialog();
            selectfile.Filter = "Image Files(*.jpg;)|*.jpg";
            if (selectfile.ShowDialog() == DialogResult.OK)
            {
                lblSignpath.Text = selectfile.FileName;
                pictureBox5sign.Image = new Bitmap(selectfile.FileName);
                pictureBox5sign.SizeMode = PictureBoxSizeMode.StretchImage;

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog selectfile = new OpenFileDialog();
            selectfile.Filter = "Image Files(*.jpg;)|*.jpg";
            if (selectfile.ShowDialog() == DialogResult.OK)
            {
                lblpasssbokpath.Text = selectfile.FileName;
                pictureBox6passbok.Image = new Bitmap(selectfile.FileName);
                pictureBox6passbok.SizeMode = PictureBoxSizeMode.StretchImage;

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            OpenFileDialog selectfile = new OpenFileDialog();
            selectfile.Filter = "Image Files(*.jpg;)|*.jpg";
            if (selectfile.ShowDialog() == DialogResult.OK)
            {
                lblmarksheetpath.Text = selectfile.FileName;
                pictureBox7markshet.Image = new Bitmap(selectfile.FileName);
                pictureBox7markshet.SizeMode = PictureBoxSizeMode.StretchImage;

            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            OpenFileDialog selectfile = new OpenFileDialog();
            selectfile.Filter = "Image Files(*.jpg;)|*.jpg";
            if (selectfile.ShowDialog() == DialogResult.OK)
            {
                lblpassportpath.Text = selectfile.FileName;
                pictureBox8pasport.Image = new Bitmap(selectfile.FileName);
                pictureBox8pasport.SizeMode = PictureBoxSizeMode.StretchImage;

            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            OpenFileDialog selectfile = new OpenFileDialog();
            selectfile.Filter = "Image Files(*.jpg;)|*.jpg";
            if (selectfile.ShowDialog() == DialogResult.OK)
            {
                lblvoteridpath.Text = selectfile.FileName;
                pictureBox9voterid.Image = new Bitmap(selectfile.FileName);
                pictureBox9voterid.SizeMode = PictureBoxSizeMode.StretchImage;

            }
        }
        //
        private void loadbasiccontrol()
        {
            
            //////////////////////////
            comboBox10_gender.DisplayMember = "Text";
            comboBox10_gender.ValueMember = "Value";
            var items = new[] {
                new {Text="-Select-", Value="0"},
                new {Text="Male", Value="1"},
                new {Text="Female", Value="2"},
                new {Text="Others", Value="3"},
            };
            comboBox10_gender.DataSource = items;
            comboBox10_gender.SelectedIndex = 0;
            ////////////////////////////////////
            comboBox11_religon.DisplayMember = "Text";
            comboBox11_religon.ValueMember = "Value";
            var items1 = new[] {
                new {Text="-Select-", Value="0"},
                new {Text="Hindu", Value="1"},
                new {Text="Muslim", Value="2"},
                new {Text="Sikh", Value="3"},
                new {Text="Christian", Value="4"}
            };
            comboBox11_religon.DataSource = items1;
            comboBox11_religon.SelectedIndex = 0;
            /////////////////////////
            comboBox1mrd_st.DisplayMember = "Text";
            comboBox1mrd_st.ValueMember = "Value";
            var items2 = new[] {
                new {Text="-Select-", Value="0"},
                new {Text="Single", Value="1"},
                new {Text="Married", Value="2"},
                new {Text="Widow", Value="3"},
                new {Text="Widower", Value="4"},
                new {Text="Divorced", Value="5"},
            };
            comboBox1mrd_st.DataSource = items2;
            comboBox1mrd_st.SelectedIndex = 0;
            /////////////////////////
            comboBox28_usrtype.DisplayMember = "Text";
            comboBox28_usrtype.ValueMember = "Value";
            var items3 = new[] {
                new {Text="-Select-", Value="0"},
                new {Text="Administrator", Value="1"},
                new {Text="User", Value="2"}
                
            };
            comboBox28_usrtype.DataSource = items3;
            comboBox28_usrtype.SelectedIndex = 0;
            /////////////////////////
            comboBox29_ishod.DisplayMember = "Text";
            comboBox29_ishod.ValueMember = "Value";
            var items4 = new[] {
                new {Text="-Select-", Value="0"},
                new {Text="Yes", Value="1"},
                new {Text="No", Value="2"}

            };
            comboBox29_ishod.DataSource = items4;
            comboBox29_ishod.SelectedIndex = 0;
            //////////////////////
            comboBox2_continent.DisplayMember = "Text";
            comboBox2_continent.ValueMember = "Value";
            var items5 = new[] {
                 new {Text="-Select-", Value="0"},
                 new {Text="Asia", Value="1"},
                new {Text="Asia", Value="2"} //enter continent value from database
               

            };
            comboBox2_continent.DataSource = items5;
            comboBox2_continent.SelectedIndex = 2;
            ///////////////////////////////
            comboBox9_continent1.DisplayMember = "Text";
            comboBox9_continent1.ValueMember = "Value";
            var items8 = new[] {
                 new {Text="-Select-", Value="0"},
                 new {Text="Asia", Value="1"},
                new {Text="Asia", Value="2"} //enter continent value from database
               

            };
            comboBox9_continent1.DataSource = items8;
            comboBox9_continent1.SelectedIndex = 2;
            /////////////////////////////
            comboBox1_migrant.DisplayMember = "Text";
            comboBox1_migrant.ValueMember = "Value";
            var items7 = new[] {
                new {Text="-Select-", Value="0"},
                new {Text="Indian", Value="1"},
                new {Text="International", Value="2"}

            };
            comboBox1_migrant.DataSource = items7;
            comboBox1_migrant.SelectedIndex = 1;
            /////////////////

        }
        private void getcountrylist()
        {
            try
            {
                DataRow dr;
                //SqlConnection conn2 = new SqlConnection(sbmmc.ConnectionString);
                if (connpayroll.State == ConnectionState.Closed)
                   connpayroll.Open();
                SqlCommand cmd = new SqlCommand("select *from country where continentid=@continentid", connpayroll);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@continentid",comboBox2_continent.SelectedValue);
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                comboBox3_country.DisplayMember = "country";
                comboBox3_country.ValueMember = "id";
                comboBox3_country.DataSource = dt;
                dr = dt.NewRow();
                dr.ItemArray = new object[] { 0, "-Select Country-" };
                dt.Rows.InsertAt(dr, 0);
                comboBox3_country.SelectedIndex = 83;
            }
            catch (Exception ee)
            {
                //MessageBox.Show(ee.ToString());
            }
        }

        private void getstatelist()
        {
            try
            {
                DataRow dr;
                //SqlConnection conn2 = new SqlConnection(sbmmc.ConnectionString);
                if (connpayroll.State == ConnectionState.Closed)
                    connpayroll.Open();
                SqlCommand cmd = new SqlCommand("select *from state where countryid=@countryid", connpayroll);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@countryid", comboBox3_country.SelectedValue);
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                comboBox4_state.DisplayMember = "state";
                comboBox4_state.ValueMember = "id";
                comboBox4_state.DataSource = dt;
                dr = dt.NewRow();
                dr.ItemArray = new object[] { 0, "-Select State-" };
                dt.Rows.InsertAt(dr, 0);
                comboBox4_state.SelectedIndex = 0;
            }
            catch (Exception ee)
            {
                //MessageBox.Show(ee.ToString());
            }
        }


        private void getcountrylist1()
        {
            try
            {
                DataRow dr;
                //SqlConnection conn2 = new SqlConnection(sbmmc.ConnectionString);
                if (connpayroll.State == ConnectionState.Closed)
                    connpayroll.Open();
                SqlCommand cmd = new SqlCommand("select *from country where continentid=@continentid", connpayroll);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@continentid", comboBox9_continent1.SelectedValue);
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                comboBox8_country1.DisplayMember = "country";
                comboBox8_country1.ValueMember = "id";
                comboBox8_country1.DataSource = dt;
                dr = dt.NewRow();
                dr.ItemArray = new object[] { 0, "-Select Country-" };
                dt.Rows.InsertAt(dr, 0);
                comboBox8_country1.SelectedIndex = 83;
            }
            catch (Exception ee)
            {
                //MessageBox.Show(ee.ToString());
            }
        }

        private void getstatelist1()
        {
            try
            {
                DataRow dr;
                //SqlConnection conn2 = new SqlConnection(sbmmc.ConnectionString);
                if (connpayroll.State == ConnectionState.Closed)
                    connpayroll.Open();
                SqlCommand cmd = new SqlCommand("select *from state where countryid=@countryid", connpayroll);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@countryid", comboBox8_country1.SelectedValue);
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                comboBox7_state1.DisplayMember = "state";
                comboBox7_state1.ValueMember = "id";
                comboBox7_state1.DataSource = dt;
                dr = dt.NewRow();
                dr.ItemArray = new object[] { 0, "-Select State-" };
                dt.Rows.InsertAt(dr, 0);
                comboBox7_state1.SelectedIndex = 0;
            }
            catch (Exception ee)
            {
                //MessageBox.Show(ee.ToString());
            }
        }

        private void getunitlist()
        {
            try
            {

                DataRow dr;
                //SqlConnection conn2 = new SqlConnection(sbmmc.ConnectionString);
                if (connpayroll.State == ConnectionState.Closed)
                    connpayroll.Open();
                SqlCommand cmd = new SqlCommand("select sub_code,sub_sname from subcomp order by sub_code", connpayroll);
                cmd.CommandType = CommandType.Text;
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                comboBox5_distric.DisplayMember = "sub_sname";
                comboBox5_distric.ValueMember = "sub_code";
                comboBox5_distric.DataSource = dt;
                dr = dt.NewRow();
                dr.ItemArray = new object[] { 0, "-Select Unit-" };
                dt.Rows.InsertAt(dr, 0);
                comboBox5_distric.SelectedIndex = 0;
            }
            catch (Exception ee)
            {
                //MessageBox.Show(ee.ToString());
            }
        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void comboBox4_state_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            try
            {
                
                DataRow dr;
                //SqlConnection conn2 = new SqlConnection(sbmmc.ConnectionString);
                if (connpayroll.State == ConnectionState.Closed)
                    connpayroll.Open();
                SqlCommand cmd = new SqlCommand("select *from city where stateid=@stateid", connpayroll);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@stateid", comboBox4_state.SelectedValue);
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                comboBox5_distric.DisplayMember = "city";
                comboBox5_distric.ValueMember = "id";
                comboBox5_distric.DataSource = dt;
                dr = dt.NewRow();
                dr.ItemArray = new object[] { 0, "-Select District-" };
                dt.Rows.InsertAt(dr, 0);
                comboBox5_distric.SelectedIndex = 0;
            }
            catch (Exception ee)
            {
                //MessageBox.Show(ee.ToString());
            }
        
    }

        private void comboBox7_state1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                DataRow dr;
                //SqlConnection conn2 = new SqlConnection(sbmmc.ConnectionString);
                if (connpayroll.State == ConnectionState.Closed)
                    connpayroll.Open();
                SqlCommand cmd = new SqlCommand("select *from city where stateid=@stateid", connpayroll);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@stateid", comboBox7_state1.SelectedValue);
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                comboBox6_distric1.DisplayMember = "city";
                comboBox6_distric1.ValueMember = "id";
                comboBox6_distric1.DataSource = dt;
                dr = dt.NewRow();
                dr.ItemArray = new object[] { 0, "-Select District-" };
                dt.Rows.InsertAt(dr, 0);
                comboBox6_distric1.SelectedIndex = 0;
            }
            catch (Exception ee)
            {
                //MessageBox.Show(ee.ToString());
            }
        }
        //
    }
}
