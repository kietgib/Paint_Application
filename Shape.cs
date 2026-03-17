using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midterm
{
    public abstract class Shape
    {
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

        public abstract void Draw(Graphics g);
    }

    public class LineShape : Shape
    {
        public LineShape(Point start, Point end, Color strokeColor, int strokeWidth = 2)
            : base (start, end, strokeColor, Color.Transparent, strokeWidth) { }

        public override void Draw(Graphics g)
        {
            using (Pen pen = new Pen(StrokeColor, StrokeWidth))
            {
                g.DrawLine(pen, StartPoint, EndPoint);
            }
        }

    }

    public class RectangleShape : Shape
    {
        public RectangleShape(Point start, Point end, Color stroke, Color fill)
            : base(start, end, stroke, fill)
        {
        }

        public override void Draw(Graphics g)
        {
            int x = Math.Min(StartPoint.X, EndPoint.X);
            int y = Math.Min(StartPoint.Y, EndPoint.Y);
            int width = Math.Abs(StartPoint.X - EndPoint.X);
            int height = Math.Abs(StartPoint.Y - EndPoint.Y);

            using (Pen pen = new Pen(StrokeColor, StrokeWidth))
            {
                if (FillColor != Color.Transparent)
                {
                    using (Brush brush = new SolidBrush(FillColor))
                    {
                        g.FillRectangle(brush, x, y, width, height);
                    }
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

            int size = Math.Max(
                Math.Abs(StartPoint.X - EndPoint.X),
                Math.Abs(StartPoint.Y - EndPoint.Y)
            );

            using (Pen pen = new Pen(StrokeColor, StrokeWidth))
            {
                if (FillColor != Color.Transparent)
                {
                    using (Brush brush = new SolidBrush(FillColor))
                    {
                        g.FillEllipse(brush, x, y, size, size);
                    }
                }

                g.DrawEllipse(pen, x, y, size, size);
            }
        }
    }

    public class ArcShape : Shape
    {
        public float StartAngle { get; set; } = 0;
        public float SweepAngle { get; set; } = 180;

        public ArcShape(Point start, Point end, Color stroke)
            : base(start, end, stroke, Color.Transparent)
        {
        }

        public override void Draw(Graphics g)
        {
            int x = Math.Min(StartPoint.X, EndPoint.X);
            int y = Math.Min(StartPoint.Y, EndPoint.Y);
            int width = Math.Abs(StartPoint.X - EndPoint.X);
            int height = Math.Abs(StartPoint.Y - EndPoint.Y);

            using (Pen pen = new Pen(StrokeColor, StrokeWidth))
            {
                g.DrawArc(pen, x, y, width, height, StartAngle, SweepAngle);
            }
        }
    }
}
