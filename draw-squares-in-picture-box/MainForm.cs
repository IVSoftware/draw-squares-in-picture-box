using System.Diagnostics;

namespace draw_squares_in_picture_box
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            // Capture starting point.
            pictureBox1.MouseDown += (sender, e) =>
                _mouseDownPoint = e.Location;

            // If Left button is down, tell the picture box to repaint itself.
            pictureBox1.MouseMove += (sender, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    _mouseCurrentPoint = e.Location;
                    pictureBox1.Invalidate();
                }
            };

            // Use the Paint event to draw.
            pictureBox1.Paint += (sender, e) =>
            {
                if (MouseButtons == MouseButtons.Left)
                {
                    var delta = _mouseCurrentPoint.Y - _mouseDownPoint.Y;
                    using (var pen = new Pen(Color.DarkRed, 2F))
                    {
                        Debug.WriteLine($"{_mouseDownPoint}{_mouseCurrentPoint}");
                        var width = _mouseCurrentPoint.X - _mouseDownPoint.X;
                        var height = _mouseCurrentPoint.Y - _mouseDownPoint.Y;
                        var side = Math.Max(width, height);

                        // Draw rectangle ON THE GRAPHICS PROVIDED.
                        _currentBounds = new Rectangle(_mouseDownPoint, new Size(side, side));
                        e.Graphics.DrawRectangle(pen, _currentBounds);

                        foreach (var savedRectangle in RectanglesToRedraw)
                        {
                            e.Graphics.DrawRectangle(pen, savedRectangle);                           
                        }
                    }
                }
            };

            // If you don't want this square to erase the next
            // time you Invalidate, add it to your "Document".
            pictureBox1.MouseUp += (sender, e) => 
                RectanglesToRedraw.Add(_currentBounds);
        }
        Point _mouseDownPoint = default;
        Point _mouseCurrentPoint = default;
        Rectangle _currentBounds = default;
        List<Rectangle> RectanglesToRedraw { get; } = new List<Rectangle>();
    }
}
