using System.Diagnostics;

namespace draw_squares_in_picture_box
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        class PictureBoxEx : PictureBox
        {
            protected override void OnMouseDown(MouseEventArgs e)
            {
                base.OnMouseDown(e);
                _mouseDownPoint = e.Location;
            }
            Point _mouseDownPoint = default;

            protected override void OnMouseMove(MouseEventArgs e)
            {
                base.OnMouseMove(e);
                _mouseCurrentPoint = e.Location;
                if (MouseButtons == MouseButtons.Left)
                {
                    BeginInvoke((MethodInvoker)delegate { Invalidate(); });
                }
            }
            Point _mouseCurrentPoint = default;
            protected override void OnMouseUp(MouseEventArgs e)
            {
                base.OnMouseUp(e);
                Squares.Add(_currentSquare);
            }
            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);
                if (MouseButtons == MouseButtons.Left)
                {
                    var delta = _mouseCurrentPoint.Y - _mouseDownPoint.Y;
                    using (var pen = new Pen(Color.DarkRed, 2F))
                    {
                        Debug.WriteLine($"{_mouseDownPoint}{_mouseCurrentPoint}");
                        var width = _mouseCurrentPoint.X - _mouseDownPoint.X;
                        var height = _mouseCurrentPoint.Y - _mouseDownPoint.Y;
                        var side = Math.Max(width, height);
                        _currentSquare = new Rectangle(_mouseDownPoint, new Size(side, side));
                        e.Graphics.DrawRectangle(pen, _currentSquare);

                        foreach (var square in Squares)
                        {
                            e.Graphics.DrawRectangle(pen, square);
                        }
                    }
                }
            }
            Rectangle _currentSquare = default;
            List<Rectangle> Squares { get; } = new List<Rectangle>();
        }
    }
}
