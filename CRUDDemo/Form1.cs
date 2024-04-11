using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace CRUDDemo
{
#nullable disable
    public partial class CRUD : Form
    {
        public CRUD()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=localhost;Initial Catalog=crud_db;Integrated Security=True");

        private void Form1_Load(object sender, EventArgs e)
        {
            BinData();
        }

        void BinData()
        {
            SqlCommand cnn = new SqlCommand("select * from emptab", con);
            SqlDataAdapter da = new SqlDataAdapter(cnn);
            DataTable table = new DataTable();
            da.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (nameTb.Text == "" || ageTB.Text == "" || salaryTB.Text == "")
            {
                MessageBox.Show("Missing Information!!!");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cnn = new SqlCommand("insert into emptab values(@name,@age,@salary)", con);
                    cnn.Parameters.AddWithValue("@Name", nameTb.Text);
                    cnn.Parameters.AddWithValue("@Age", int.Parse(ageTB.Text));
                    cnn.Parameters.AddWithValue("@Salary", int.Parse(salaryTB.Text));
                    cnn.ExecuteNonQuery();
                    con.Close();
                    BinData();
                    MessageBox.Show("Record Added Successfully", "Add", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    con.Close();
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (idTB.Text == "" || nameTb.Text == "" || ageTB.Text == "" || salaryTB.Text == "")
            {
                MessageBox.Show("Missing Information!!!");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cnn = new SqlCommand("update emptab set name=@name,age=@age,salary=@salary where id=@id", con);
                    cnn.Parameters.AddWithValue("@Id", int.Parse(idTB.Text));
                    cnn.Parameters.AddWithValue("@Name", nameTb.Text);
                    cnn.Parameters.AddWithValue("@Age", int.Parse(ageTB.Text));
                    cnn.Parameters.AddWithValue("@Salary", int.Parse(salaryTB.Text));
                    cnn.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Record Updated Successfully", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    con.Close();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cnn = new SqlCommand("delete from emptab where id=@id", con);
                cnn.Parameters.AddWithValue("@Id", int.Parse(idTB.Text));
                cnn.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Deleted Successfully", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            idTB.Text = "";
            nameTb.Text = "";
            ageTB.Text = "";
            salaryTB.Text = "";
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            if (con == null)
            {
                MessageBox.Show("Employee informations is null");
            }
            else
            {
                try
                {
                    SqlCommand cnn = new SqlCommand("select * from emptab", con);
                    SqlDataAdapter da = new SqlDataAdapter(cnn);
                    DataTable table = new DataTable();
                    da.Fill(table);
                    dataGridView1.DataSource = table;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    con.Close();
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.PrintPreviewControl.Zoom = 1;
            printPreviewDialog1.ShowDialog();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (con == null)
            {
                MessageBox.Show("Employee informations is null");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cnn = new SqlCommand("select * from emptab where id=@id or name=@name or age=@age or salary=@salary", con);
                    cnn.Parameters.AddWithValue("@Id", int.Parse(idTB.Text));
                    SqlDataAdapter da = new SqlDataAdapter(cnn);
                    DataTable table = new DataTable();
                    da.Fill(table);
                    con.Close();
                    dataGridView1.DataSource = table;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    con.Close();
                }
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap imagebmp = new Bitmap(dataGridView1.Width, dataGridView1.Height);
            dataGridView1.DrawToBitmap(imagebmp, new Rectangle(0, 0, dataGridView1.Width, dataGridView1.Height));
            e.Graphics.DrawImage(imagebmp, 5, 20);
        }
    }
}
