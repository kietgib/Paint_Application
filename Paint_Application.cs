using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Midterm
{
    public partial class Paint_Application : Form
    {
        public Paint_Application()
        {
            InitializeComponent();
            this.Shown += Paint_Application_Load; // Gán sự kiện Load
            flowLayoutPanel2.AutoSize = true;
            flowLayoutPanel2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutPanel1.AutoSize = true;
            flowLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutPanel3.AutoSize = true;
            flowLayoutPanel3.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            groupBox3.Visible = false;
            groupBox4.Visible = false;
            groupBox5.Visible = false;
            groupBox6.Visible = false;
            groupBox7.Visible = false;

        }
        Pen myPen = new Pen(Color.Black, 3f);

        private void Paint_Application_Load(object sender, EventArgs e)
        {
            // Full màn hình
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;

            // SplitContainer setup
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Orientation = Orientation.Horizontal;

            // GIỮ panel trái cố định không resize khi form thay đổi
            splitContainer1.FixedPanel = FixedPanel.Panel1;

            // Cho user kéo splitter nếu muốn
            splitContainer1.IsSplitterFixed = false;

            // Thiết lập width ban đầu — sẽ điều chỉnh chính xác trong Shown
            splitContainer1.SplitterDistance = 180;

            // Panel trái tối thiểu
            splitContainer1.Panel1MinSize = 150;

            // Panel phải tối thiểu (giữ không bị quá nhỏ)
            splitContainer1.Panel2MinSize = 200;

            // Setup combobox, colors...
            comboBox2.Items.Clear();
            for (int w = 1; w <= 20; w++)
                comboBox2.Items.Add(w);
            comboBox2.SelectedIndex = 0;


            comboBox1.DrawMode = DrawMode.OwnerDrawFixed;
            comboBox1.Items.Clear();

            comboBox5.DrawMode = DrawMode.OwnerDrawFixed;
            comboBox5.Items.Clear();

            comboBox4.DrawMode = DrawMode.OwnerDrawFixed;
            comboBox4.Items.Clear();

            comboBox6.DrawMode = DrawMode.OwnerDrawFixed;
            comboBox6.Items.Clear();

            var colors = Enum.GetValues(typeof(KnownColor))
                .Cast<KnownColor>()
                .Select(k => Color.FromName(k.ToString()))
                .Where(c => c.IsKnownColor)
                .OrderBy(c => c.GetHue())
                .ToList();

            foreach (var c in colors)
            {
                comboBox1.Items.Add(c.Name);
                comboBox5.Items.Add(c.Name);
                comboBox4.Items.Add(c.Name);
                comboBox6.Items.Add(c.Name);
            }
        }

        private void Paint_Application_Shown(object sender, EventArgs e)
        {
            // Set lại SplitterDistance sau khi form đã rendered
            splitContainer1.SplitterDistance = 250;
        }

        private void comboBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            string colorName = comboBox1.Items[e.Index].ToString();
            Color col = Color.FromName(colorName);

            e.DrawBackground();

            Rectangle colorRect = new Rectangle(e.Bounds.X + 2, e.Bounds.Y + 2, 20, e.Bounds.Height - 4);
            using (SolidBrush b = new SolidBrush(col))
            {
                e.Graphics.FillRectangle(b, colorRect);
                e.Graphics.DrawRectangle(Pens.Black, colorRect);
            }

            e.Graphics.DrawString(colorName, comboBox1.Font, Brushes.Black,
                e.Bounds.X + 26, e.Bounds.Y + 2);

            e.DrawFocusRectangle();
        }

        private void comboBox5_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            string colorName = comboBox5.Items[e.Index].ToString();
            Color col = Color.FromName(colorName);

            e.DrawBackground();

            Rectangle colorRect = new Rectangle(e.Bounds.X + 2, e.Bounds.Y + 2, 20, e.Bounds.Height - 4);
            using (SolidBrush b = new SolidBrush(col))
            {
                e.Graphics.FillRectangle(b, colorRect);
                e.Graphics.DrawRectangle(Pens.Black, colorRect);
            }

            e.Graphics.DrawString(colorName, comboBox5.Font, Brushes.Black,
                e.Bounds.X + 26, e.Bounds.Y + 2);

            e.DrawFocusRectangle();
        }

        private void comboBox4_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            string colorName = comboBox4.Items[e.Index].ToString();
            Color col = Color.FromName(colorName);

            e.DrawBackground();

            Rectangle colorRect = new Rectangle(e.Bounds.X + 2, e.Bounds.Y + 2, 20, e.Bounds.Height - 4);
            using (SolidBrush b = new SolidBrush(col))
            {
                e.Graphics.FillRectangle(b, colorRect);
                e.Graphics.DrawRectangle(Pens.Black, colorRect);
            }

            e.Graphics.DrawString(colorName, comboBox4.Font, Brushes.Black,
                e.Bounds.X + 26, e.Bounds.Y + 2);

            e.DrawFocusRectangle();
        }

        private void comboBox6_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            string colorName = comboBox6.Items[e.Index].ToString();
            Color col = Color.FromName(colorName);

            e.DrawBackground();

            Rectangle colorRect = new Rectangle(e.Bounds.X + 2, e.Bounds.Y + 2, 20, e.Bounds.Height - 4);
            using (SolidBrush b = new SolidBrush(col))
            {
                e.Graphics.FillRectangle(b, colorRect);
                e.Graphics.DrawRectangle(Pens.Black, colorRect);
            }

            e.Graphics.DrawString(colorName, comboBox6.Font, Brushes.Black,
                e.Bounds.X + 26, e.Bounds.Y + 2);

            e.DrawFocusRectangle();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            splitContainer1.Invalidate();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            Pen myPen = new Pen(Color.Black, 3f);
            string v = comboBox3.SelectedItem?.ToString();
            if (v == null) return;

            switch (v)
            {
                case "Solid": myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid; break;
                case "Dash": myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash; break;
                case "Dot": myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot; break;
                case "DashDot": myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot; break;
                case "DashDotDot": myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDotDot; break;
            }

            splitContainer1.Invalidate();
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox5.SelectedIndex < 0) return;

            // Lấy tên màu được chọn
            string colorName = comboBox5.SelectedItem.ToString();

            // Chuyển tên tên thành Color
            Color selectedColor = Color.FromName(colorName);

            // Áp màu cho Panel1
            splitContainer1.Panel1.BackColor = selectedColor;

            // Nếu muốn luôn cập nhật giao diện ngay
            splitContainer1.Panel1.Invalidate();
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox4.SelectedIndex < 0) return;

            // Lấy tên màu được chọn
            string colorName = comboBox4.SelectedItem.ToString();

            // Chuyển tên tên thành Color
            Color selectedColor = Color.FromName(colorName);

            // Áp màu cho Panel1
            splitContainer1.Panel2.BackColor = selectedColor;

            // Nếu muốn luôn cập nhật giao diện ngay
            splitContainer1.Panel2.Invalidate();
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox6.SelectedIndex < 0) return;

            // Lấy tên màu từ combobox
            string colorName = comboBox6.SelectedItem.ToString();

            // Chuyển tên sang Color
            Color c = Color.FromName(colorName);

            // Gán cho text label, combo box, groupbox
            label1.ForeColor = c;
            label3.ForeColor = c;
            label6.ForeColor = c;
            label7.ForeColor = c;
            label10.ForeColor = c;
            groupBox1.ForeColor = c;
            groupBox2.ForeColor = c;
            groupBox3.ForeColor = c;
            groupBox4.ForeColor = c;
            groupBox5.ForeColor = c;
            comboBox1.ForeColor = c;
            comboBox2.ForeColor = c;
            comboBox3.ForeColor = c;
            comboBox4.ForeColor = c;
            comboBox5.ForeColor = c;
            comboBox6.ForeColor = c;
            comboBox7.ForeColor = c;
            comboBox8.ForeColor = c;


        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Nếu chưa chọn gì
            if (comboBox7.SelectedIndex < 0)
            {
                flowLayoutPanel2.Visible = false;
                return;
            }

            // Có chọn thì hiện FlowLayoutPanel
            flowLayoutPanel2.Visible = true;
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            groupBox3.Visible = false;

            string selected = comboBox7.SelectedItem?.ToString();

            switch (selected)
            {
                case "Bottom Panel Color":
                    groupBox1.Visible = true;
                    break;
                case "Top Panel Color":
                    groupBox2.Visible = true;
                    break;
                case "Program Text Color":
                    groupBox3.Visible = true;
                    break;
            }

            // FlowLayoutPanel will auto adjust layout
            flowLayoutPanel2.PerformLayout();
        }

        private void comboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {

            // Nếu chưa chọn gì
            if (comboBox8.SelectedIndex < 0)
            {
                flowLayoutPanel1.Visible = false;
                return;
            }

            // Có chọn thì hiện FlowLayoutPanel
            flowLayoutPanel1.Visible = true;
            groupBox4.Visible = false;
            groupBox5.Visible = false;

            string selected = comboBox8.SelectedItem?.ToString();

            switch (selected)
            {
                case "Weights":
                    groupBox4.Visible = true;
                    break;
                case "Dash style":
                    groupBox5.Visible = true;
                    break;
            }

            // FlowLayoutPanel will auto adjust layout
            flowLayoutPanel1.PerformLayout();

        }
            
        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Nếu chưa chọn gì
            if (comboBox9.SelectedIndex < 0)
            {
                flowLayoutPanel3.Visible = false;
                return;
            }

            // Có chọn thì hiện FlowLayoutPanel
            flowLayoutPanel3.Visible = true;
            groupBox6.Visible = false;
            groupBox7.Visible = false;

            string selected = comboBox9.SelectedItem?.ToString();

            switch (selected)
            {
                case "Shape":
                    groupBox6.Visible = true;
                    break;
                case "Stroke Color ":
                    groupBox7.Visible = true;
                    break;
            }

            // FlowLayoutPanel will auto adjust layout
            flowLayoutPanel3.PerformLayout();
        }
    }
}

