using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1
{
    public partial class frmQuanLySach : Form
    {
        Model1 db = new Model1();
        public frmQuanLySach()
        {
            InitializeComponent();
        }

        private void frmQuanLySach_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Saches.ToList();
            cbbLoai.DataSource = db.LoaiSaches.ToList();
            dataGridView1.Columns[3].Visible = false;
            cbbLoai.DisplayMember = "TenLoai";
            cbbLoai.ValueMember = "MaLoai";
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMa.Text = dataGridView1.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
            txtTen.Text = dataGridView1.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
            txtNam.Text = dataGridView1.Rows[e.RowIndex].Cells[2].FormattedValue.ToString();
            cbbLoai.Text = dataGridView1.Rows[e.RowIndex].Cells[4].FormattedValue.ToString();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Sach s = new Sach();
            if(cbbLoai.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn mã loại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error );
            }
            if (txtMa.Text.Trim() == "" || txtTen.Text.Trim() == "" || txtNam.Text.Trim() == "") 
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin ","Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error );
            s.MaSach = txtMa.Text.Trim();
            s.TenSach = txtTen.Text.Trim();
            s.NamXB =Convert.ToInt32( txtNam.Text.Trim());
            s.MaLoai = Convert.ToInt32(cbbLoai.SelectedValue);
            try
            {
                db.Saches.Add(s);
                MessageBox.Show("Da them thanh cong:  ",
                          "Thanh cong", MessageBoxButtons.OK, MessageBoxIcon.Information);

                db.SaveChanges();
                frmQuanLySach_Load(sender, e);
                reset();
            }
            catch (Exception)
            {

                throw;
            }

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            var rs = db.Saches.FirstOrDefault(p => p.MaSach == txtMa.Text.Trim());
            if (rs != null)
            {
                rs.TenSach = txtTen.Text.Trim();
                rs.NamXB = Convert.ToInt32(txtNam.Text.Trim());
                rs.MaLoai = Convert.ToInt32(cbbLoai.SelectedValue);
               

            }
            try
            {
                db.SaveChanges();
                MessageBox.Show("Da sua thanh cong:  " + txtTen.Text.Trim(),
                          "Thanh cong", MessageBoxButtons.OK, MessageBoxIcon.Information);

                frmQuanLySach_Load(sender, e);
                reset();
            }
            catch (Exception)
            {

                throw;
            }

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            var rs = db.Saches.FirstOrDefault(s => s.MaSach == txtMa.Text.Trim());
            if(rs != null)
            {
                MessageBox.Show("Bạn có muốn xóa không?","OK",MessageBoxButtons.OK, MessageBoxIcon.Error);
                db.Saches.Remove(rs);
                db.SaveChanges();
                frmQuanLySach_Load(sender, e);
                reset();

            }
        }
        public void reset()
        {
            txtMa.Text = "";
            txtTen.Text = "";
            txtNam.Text = "";
            cbbLoai.SelectedIndex = -1;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string key = txtSearch.Text.Trim();
            var rs = db.Saches.Where(s=>s.MaSach.Contains(key) || s.TenSach.Contains(key)||s.NamXB.ToString().Contains (key)).ToList();
            
            if (rs.Count > 0)
            {
                dataGridView1.DataSource = rs;
            }
        }
    }
}
