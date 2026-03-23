using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Drawing.Drawing2D;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Midterm
{
    
    public partial class Paint_Application : Form
    {
        System.Drawing.Drawing2D.DashStyle currentDashStyle = System.Drawing.Drawing2D.DashStyle.Solid;

        List<Shape> shapes = new List<Shape>();
        //để lưu shapes đang được chọn khi Ctrl + Click
        List<Shape> selectedShapes = new List<Shape>(); // shapes đang được chọn
        bool isSelectMode = false; 

        Color strokeColor = Color.Black;
        Color fillColor = Color.Red;

        Point startPoint;
        Point endPoint;

        bool isDrawing = false;

        string selectedShape = "Rectangle";
        Shape currentShape = null;
        public Paint_Application()
        {
            InitializeComponent();
            this.KeyPreview = false;

            panel1.TabStop = true;   // ← thêm dòng này
            panel1.Focus();

            panel1.Paint += panel1_Paint;        
            panel1.MouseDown += panel1_MouseDown;
            panel1.MouseMove += panel1_MouseMove;
            panel1.MouseUp += panel1_MouseUp;

            panel1.GetType()
                 .GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                 .SetValue(panel1, true, null);
            this.Shown += Paint_Application_Load; // Gán sự kiện Load
            flowLayoutPanel2.AutoSize = true;
            flowLayoutPanel2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutPanel1.AutoSize = true;
            flowLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutPanel3.AutoSize = true;
            flowLayoutPanel3.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutPanel4.AutoSize = true;
            flowLayoutPanel4.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            groupBox3.Visible = false;
            groupBox10.Visible = false;
            groupBox5.Visible = false;
            groupBox6.Visible = false;
            groupBox9.Visible = false;
            groupBox8.Visible = false;
        }
        Pen myPen = new Pen(Color.Black, 3f);
        int currentStrokeWidth = 2;

        private void Paint_Application_Load(object sender, EventArgs e)
        {
            // Full màn hình
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;

            // SplitContainer setup
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Orientation = Orientation.Horizontal;
            panel1.Dock = DockStyle.Fill;


            // GIỮ panel trái cố định không resize khi form thay đổi
            splitContainer1.FixedPanel = FixedPanel.Panel1;

            // Cho user kéo splitter nếu muốn
            splitContainer1.IsSplitterFixed = false;

            // Thiết lập width ban đầu — sẽ điều chỉnh chính xác trong Shown
            splitContainer1.SplitterDistance = 130;

            // Panel trái tối thiểu
            splitContainer1.Panel1MinSize = 150;

            // Panel phải tối thiểu (giữ không bị quá nhỏ)
            splitContainer1.Panel2MinSize = 200;

            // Setup combobox, colors...
            comboBox2.Items.Clear();
            for (int w = 1; w <= 20; w++)
                comboBox2.Items.Add(w);
            comboBox2.SelectedIndex = 0;

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
            if (comboBox2.SelectedItem != null)
            {
                string text = comboBox2.SelectedItem.ToString();
                currentStrokeWidth = int.Parse(text.Replace("px", "").Trim());
            }
        }
        private void comboBox13_SelectedIndexChanged(object sender, EventArgs e)
        {
            string v = comboBox13.SelectedItem?.ToString();
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
            label10.ForeColor = c;
            groupBox1.ForeColor = c;
            groupBox2.ForeColor = c;
            groupBox3.ForeColor = c;
            groupBox5.ForeColor = c;
            groupBox6.ForeColor = c;
            groupBox8.ForeColor = c;
            groupBox9.ForeColor = c;
            groupBox10.ForeColor = c;
            comboBox2.ForeColor = c;
            comboBox4.ForeColor = c;
            comboBox5.ForeColor = c;
            comboBox6.ForeColor = c;
            comboBox7.ForeColor = c;
            comboBox8.ForeColor = c;
            comboBox9.ForeColor = c;
            comboBox12.ForeColor = c;
            comboBox11.ForeColor = c;
            comboBox10.ForeColor = c;
            Buttonbox.ForeColor = c;
            button1.ForeColor = c;
            button2.ForeColor = c;



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
            groupBox10.Visible = false;
            groupBox5.Visible = false;
            groupBox9.Visible = false;

            string selected = comboBox8.SelectedItem?.ToString();

            switch (selected)
            {
                case "Weights":
                    groupBox5.Visible = true;
                    break;
                case "Dash style":
                    groupBox10.Visible = true;
                    break;
                case "Color":
                    groupBox9.Visible = true;
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

            flowLayoutPanel3.Visible = true;

            // tắt cả nhóm trước khi bật lại
            groupBox6.Visible = false;

            string selected = comboBox9.SelectedItem?.ToString();

            switch (selected)
            {
                case "Shape":
                    groupBox6.Visible = true;
                    break;
            }
            flowLayoutPanel3.PerformLayout();
        }

        private void comboBox10_Click(object sender, EventArgs e)
        {

            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                Color selectedColor = colorDialog1.Color;

                fillColor = selectedColor; // ← FIX: gán trực tiếp

                comboBox10.Items.Clear();
                comboBox10.Items.Add(selectedColor.Name);
                comboBox10.SelectedIndex = 0;
                comboBox10.BackColor = selectedColor;
                comboBox10.ForeColor = Color.FromArgb(
                    255 - selectedColor.R,
                    255 - selectedColor.G,
                    255 - selectedColor.B
                );
            }
        }
        private void comboBox12_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                Color selectedColor = colorDialog1.Color;

                strokeColor = selectedColor; // ← FIX

                comboBox12.Items.Clear();
                comboBox12.Items.Add(selectedColor.Name);
                comboBox12.SelectedIndex = 0;
                comboBox12.BackColor = selectedColor;
                comboBox12.ForeColor = Color.FromArgb(
                    255 - selectedColor.R,
                    255 - selectedColor.G,
                    255 - selectedColor.B
                );
            }
        }
        private void comboBox11_SelectedIndexChanged(object sender, EventArgs e)
        {

            // Nếu chưa chọn gì
            if (comboBox11.SelectedIndex < 0)
            {
                flowLayoutPanel4.Visible = false;
                return;
            }

            // Có chọn thì hiện FlowLayoutPanel
            flowLayoutPanel4.Visible = true;
            groupBox8.Visible = false;

            string selected = comboBox11.SelectedItem?.ToString();

            switch (selected)
            {
                case "Filled Color":
                    groupBox8.Visible = true;
                    break;
            }
        }

        //private void comboBox10_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    fillColor = Color.FromName(comboBox10.SelectedItem.ToString());
        //}

        //private void comboBox12_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    strokeColor = Color.FromName(comboBox12.SelectedItem.ToString());
        //}

        private void Buttonbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedShape = Buttonbox.SelectedItem.ToString().Trim(); //vì có thể gõ thừa 
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            // CHUỘT PHẢI = chọn shape (thay thế Ctrl+Click)
            if (e.Button == MouseButtons.Right)
            {
                for (int i = shapes.Count - 1; i >= 0; i--)
                {
                    if (shapes[i].HitTest(e.Location))
                    {
                        if (selectedShapes.Contains(shapes[i]))
                            selectedShapes.Remove(shapes[i]);
                        else
                            selectedShapes.Add(shapes[i]);

                        panel1.Invalidate();
                        return;
                    }
                }
                return;
            }

            // CHUỘT TRÁI = vẽ shape mới, clear selection
            selectedShapes.Clear();
            isDrawing = true;
            startPoint = e.Location;

            switch (selectedShape)
            {
                case "Line":
                    currentShape = new LineShape(startPoint, startPoint, strokeColor)
                    { StrokeWidth = currentStrokeWidth };
                    break;
                case "Rectangle":
                    currentShape = new RectangleShape(startPoint, startPoint, strokeColor, fillColor)
                    { StrokeWidth = currentStrokeWidth, FillColor = fillColor };
                    break;
                case "Ellipse":
                    currentShape = new CircleShape(startPoint, startPoint, strokeColor, fillColor)
                    { StrokeWidth = currentStrokeWidth, FillColor = fillColor };
                    break;
                case "Arc":
                    currentShape = new ArcShape(startPoint, startPoint, strokeColor)
                    { StrokeWidth = currentStrokeWidth };
                    break;
            }

            panel1.Invalidate();
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isDrawing) return;

            Point endPoint = e.Location;
            panel1.Invalidate();

            switch (selectedShape)
            {
                case "Line":
                    currentShape = new LineShape(startPoint, endPoint, strokeColor)
                    {
                        StrokeWidth = currentStrokeWidth,
                        DashStyle = currentDashStyle

                    };
                    break;

                case "Rectangle":
                    currentShape = new RectangleShape(startPoint, endPoint, strokeColor, fillColor)
                    {

                        StrokeWidth = currentStrokeWidth,
                        FillColor = fillColor,
                        DashStyle = currentDashStyle
                    };
                    break;

                case "Ellipse":
                    currentShape = new CircleShape(startPoint, endPoint, strokeColor, fillColor)
                    {
                        StrokeWidth = currentStrokeWidth,
                        FillColor = fillColor,
                        DashStyle = currentDashStyle
                    };
                    break;

                case "Arc":
                    currentShape = new ArcShape(startPoint, endPoint, strokeColor)
                    {
                        StrokeWidth = currentStrokeWidth,
                        DashStyle = currentDashStyle
                    };
                    break;
            }

            panel1.Invalidate();
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            if (!isDrawing) return;

            isDrawing = false;

            if (currentShape != null)
            {
                shapes.Add(currentShape);
                currentShape = null;
            }

            panel1.Invalidate();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Vẽ tất cả shapes
            foreach (Shape s in shapes)
                s.Draw(e.Graphics);

            // Vẽ viền riêng lẻ cho từng shape được chọn
            foreach (Shape s in selectedShapes)
            {
                int x = Math.Min(s.StartPoint.X, s.EndPoint.X) - 3;
                int y = Math.Min(s.StartPoint.Y, s.EndPoint.Y) - 3;
                int w = Math.Abs(s.StartPoint.X - s.EndPoint.X) + 6;
                int h = Math.Abs(s.StartPoint.Y - s.EndPoint.Y) + 6;

                using (Pen highlightPen = new Pen(Color.Blue, 1.5f))
                {
                    highlightPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    e.Graphics.DrawRectangle(highlightPen, x, y, w, h);
                }
            }

            // Vẽ viền bao quanh TẤT CẢ shape đang chọn (nếu có >= 2)
            if (selectedShapes.Count >= 2)
            {
                int minX = selectedShapes.Min(s => Math.Min(s.StartPoint.X, s.EndPoint.X));
                int minY = selectedShapes.Min(s => Math.Min(s.StartPoint.Y, s.EndPoint.Y));
                int maxX = selectedShapes.Max(s => Math.Max(s.StartPoint.X, s.EndPoint.X));
                int maxY = selectedShapes.Max(s => Math.Max(s.StartPoint.Y, s.EndPoint.Y));

                int padding = 8;
                using (Pen groupPen = new Pen(Color.OrangeRed, 2f))
                {
                    groupPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    e.Graphics.DrawRectangle(groupPen,
                        minX - padding,
                        minY - padding,
                        (maxX - minX) + padding * 2,
                        (maxY - minY) + padding * 2);
                }
            }

            if (currentShape != null)
                currentShape.Draw(e.Graphics);
        }

        private void comboBox13_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string v = comboBox13.SelectedItem?.ToString();
            if (v == null) return;

            switch (v)
            {
                case "Solid": currentDashStyle = System.Drawing.Drawing2D.DashStyle.Solid; break;
                case "Dash": currentDashStyle = System.Drawing.Drawing2D.DashStyle.Dash; break;
                case "Dot": currentDashStyle = System.Drawing.Drawing2D.DashStyle.Dot; break;
                case "DashDot": currentDashStyle = System.Drawing.Drawing2D.DashStyle.DashDot; break;
                case "DashDotDot": currentDashStyle = System.Drawing.Drawing2D.DashStyle.DashDotDot; break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (selectedShapes.Count < 2)
            {
                MessageBox.Show("Chọn ít nhất 2 shape để group!");
                return;
            }

            GroupShape group = new GroupShape(new List<Shape>(selectedShapes));

            foreach (var s in selectedShapes)
                shapes.Remove(s);

            shapes.Add(group);
            selectedShapes.Clear();
            panel1.Invalidate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var groups = selectedShapes.OfType<GroupShape>().ToList();

            if (groups.Count == 0)
            {
                MessageBox.Show("Không có group nào được chọn!");
                return;
            }

            foreach (var group in groups)
            {
                foreach (var child in group.Children)
                    shapes.Add(child);

                shapes.Remove(group);
                selectedShapes.Remove(group);
            }

            // Xóa luôn các shape thường còn lại trong selectedShapes
            selectedShapes.Clear();
            panel1.Invalidate();
        }
    }
}

