using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Midterm
{
    public abstract class Shape
    {
        public System.Drawing.Drawing2D.DashStyle DashStyle { get; set; }
            = System.Drawing.Drawing2D.DashStyle.Solid;
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }
        public Color StrokeColor { get; set; }
        public Color FillColor { get; set; }
        public int StrokeWidth { get; set; } = 2;

        public Shape(Point start, Point end, Color strokeColor, Color fillColor, int strokeWidth = 2)
        {
            StartPoint = start;
            EndPoint = end;
            StrokeColor = strokeColor;
            FillColor = fillColor;
            StrokeWidth = strokeWidth;
        }

        public virtual bool HitTest(Point p)
        {
            int x = Math.Min(StartPoint.X, EndPoint.X);
            int y = Math.Min(StartPoint.Y, EndPoint.Y);
            int width = Math.Abs(StartPoint.X - EndPoint.X);
            int height = Math.Abs(StartPoint.Y - EndPoint.Y);

            if (width < 10) width = 10;
            if (height < 10) height = 10;

            Rectangle bounds = new Rectangle(x - 5, y - 5, width + 10, height + 10);
            return bounds.Contains(p);
        }

        public abstract void Draw(Graphics g);
    }

    public class LineShape : Shape
    {
        public LineShape(Point start, Point end, Color strokeColor, int strokeWidth = 2)
            : base(start, end, strokeColor, Color.Transparent, strokeWidth) { }

        public override bool HitTest(Point p)
        {
            float dx = EndPoint.X - StartPoint.X;
            float dy = EndPoint.Y - StartPoint.Y;
            float lenSq = dx * dx + dy * dy;

            if (lenSq == 0)
            {
                float d = (float)Math.Sqrt(Math.Pow(p.X - StartPoint.X, 2) + Math.Pow(p.Y - StartPoint.Y, 2));
                return d <= 6;
            }

            float t = ((p.X - StartPoint.X) * dx + (p.Y - StartPoint.Y) * dy) / lenSq;
            t = Math.Max(0, Math.Min(1, t));

            float closestX = StartPoint.X + t * dx;
            float closestY = StartPoint.Y + t * dy;

            float dist = (float)Math.Sqrt(Math.Pow(p.X - closestX, 2) + Math.Pow(p.Y - closestY, 2));
            return dist <= 6;
        }

        public override void Draw(Graphics g)
        {
            using (Pen pen = new Pen(StrokeColor, StrokeWidth))
            {
                pen.DashStyle = DashStyle;
                g.DrawLine(pen, StartPoint, EndPoint);
            }
        }
    }

    public class RectangleShape : Shape
    {
        public RectangleShape(Point start, Point end, Color stroke, Color fill)
            : base(start, end, stroke, fill) { }

        public override void Draw(Graphics g)
        {
            int x = Math.Min(StartPoint.X, EndPoint.X);
            int y = Math.Min(StartPoint.Y, EndPoint.Y);
            int width = Math.Abs(StartPoint.X - EndPoint.X);
            int height = Math.Abs(StartPoint.Y - EndPoint.Y);

            using (Pen pen = new Pen(StrokeColor, StrokeWidth))
            {
                pen.DashStyle = DashStyle; // ← đặt trước mọi thứ

                if (FillColor != Color.Transparent)
                {
                    using (Brush brush = new SolidBrush(FillColor))
                        g.FillRectangle(brush, x, y, width, height);
                }

                g.DrawRectangle(pen, x, y, width, height);
            }
        }
    }

    public class CircleShape : Shape
    {
        public CircleShape(Point start, Point end, Color stroke, Color fill)
            : base(start, end, stroke, fill) { }

        public override void Draw(Graphics g)
        {
            int x = Math.Min(StartPoint.X, EndPoint.X);
            int y = Math.Min(StartPoint.Y, EndPoint.Y);
            int width = Math.Abs(StartPoint.X - EndPoint.X);
            int height = Math.Abs(StartPoint.Y - EndPoint.Y);

            if (width < 1) width = 1;
            if (height < 1) height = 1;

            using (Pen pen = new Pen(StrokeColor, StrokeWidth))
            {
                pen.DashStyle = DashStyle; // ← đặt trước mọi thứ

                if (FillColor != Color.Transparent)
                {
                    using (Brush brush = new SolidBrush(FillColor))
                        g.FillEllipse(brush, x, y, width, height);
                }

                g.DrawEllipse(pen, x, y, width, height);
            }
        }
    }

    public class ArcShape : Shape
    {
        public float StartAngle { get; set; } = 0;
        public float SweepAngle { get; set; } = 180;

        public ArcShape(Point start, Point end, Color stroke)
            : base(start, end, stroke, Color.Transparent) { }

        public override void Draw(Graphics g)
        {
            int x = Math.Min(StartPoint.X, EndPoint.X);
            int y = Math.Min(StartPoint.Y, EndPoint.Y);
            int width = Math.Abs(StartPoint.X - EndPoint.X);
            int height = Math.Abs(StartPoint.Y - EndPoint.Y);

            if (width < 1) width = 1;
            if (height < 1) height = 1;

            using (Pen pen = new Pen(StrokeColor, StrokeWidth))
            {
                pen.DashStyle = DashStyle;
                g.DrawArc(pen, x, y, width, height, StartAngle, SweepAngle);
            }
        }
    }

    public class GroupShape : Shape
    {
        public List<Shape> Children { get; set; } = new List<Shape>();

        public GroupShape(List<Shape> children)
            : base(new Point(0, 0), new Point(0, 0), Color.Transparent, Color.Transparent)
        {
            Children = children;
            UpdateBounds();
        }

        public void UpdateBounds()
        {
            if (Children.Count == 0) return;

            int minX = Children.Min(s => Math.Min(s.StartPoint.X, s.EndPoint.X));
            int minY = Children.Min(s => Math.Min(s.StartPoint.Y, s.EndPoint.Y));
            int maxX = Children.Max(s => Math.Max(s.StartPoint.X, s.EndPoint.X));
            int maxY = Children.Max(s => Math.Max(s.StartPoint.Y, s.EndPoint.Y));

            StartPoint = new Point(minX, minY);
            EndPoint = new Point(maxX, maxY);
        }

        public override void Draw(Graphics g)
        {
            foreach (var shape in Children)
                shape.Draw(g);
        }

        public override bool HitTest(Point p)
        {
            return Children.Any(s => s.HitTest(p));
        }
    }
}