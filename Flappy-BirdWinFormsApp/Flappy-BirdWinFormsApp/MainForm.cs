using Timer = System.Windows.Forms.Timer;

namespace Flappy_BirdWinFormsApp
{
    public partial class MainForm : Form
    {
        Bird _bird;
        Tube _tube1;
        Tube _tube2;
        Timer _timer;
        float _gravity;
        List<Tube> _tubesOnMap;
        Random _random = new Random();

        public MainForm()
        {
            InitializeComponent();
            NewGame();
            Invalidate();
        }

        private void NewGame()
        {
            MessageBox.Show("Just click on the form to jump and fly through the tube", "Instruction", MessageBoxButtons.OK, MessageBoxIcon.Information);
            _bird = new Bird(200, 200);
            _tubesOnMap = new();
            _tube1 = new Tube(Width, 450);
            _tube2 = new Tube(Width, -150, true);
            _tubesOnMap.Add(_tube1);
            _tubesOnMap.Add(_tube2);

            _gravity = 0;
            scoreLabel.Text = "0";

            _timer = new Timer();
            _timer.Interval = 10;
            _timer.Tick += new EventHandler(Update);
            _timer.Start();
        }

        private void Update(object sender, EventArgs e)
        {
            if (IsBumped())
            {
                EndGame();
            }
            else
            {
                if (_bird.gravityValue <= 0)
                {
                    _bird.gravityValue += 0.3f;
                }
                else
                {
                    _bird.gravityValue = 0.125f;
                }
                _gravity += _bird.gravityValue;
                _bird.y += _gravity;

                MoveTube();

                Invalidate();
            }
        }

        private void EndGame()
        {
            _timer.Stop();
            var answer = MessageBox.Show($"You are bumped, game over! Your score is {scoreLabel.Text}", "End game", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
            if (answer == DialogResult.Cancel)
            {
                Application.Exit();
            }
            else
            {
                NewGame();
            }
        }

        private bool IsBumped()
        {
            PointF alpha = new PointF();
            foreach (var tube in _tubesOnMap)
            {
                alpha.X = (_bird.x + _bird.size / 2) - (tube.x + tube.sizeX / 2);
                alpha.Y = (_bird.y + _bird.size / 2) - (tube.y + tube.sizeY / 2);
                if (Math.Abs(alpha.X) <= _bird.size / 2 + tube.sizeX / 2)
                {
                    if (Math.Abs(alpha.Y) <= _bird.size / 2 + tube.sizeY / 2)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private void CheckTubeForLivingMap()
        {
            if (_tubesOnMap.First().x < _bird.x - _tubesOnMap.First().sizeX - 100)
            {
                scoreLabel.Text = (Convert.ToInt32(scoreLabel.Text) + 1).ToString();
                _tubesOnMap.RemoveRange(0, 2);
            }
        }
        private void CreateNewTube()
        {
            if (Width - _tubesOnMap.Last().x >= 300)
            {
                int updY;

                updY = _random.Next(-250, 1);

                _tube1 = new Tube(Width, updY + 600);
                _tube2 = new Tube(Width, updY, true);
                _tubesOnMap.Add(_tube1);
                _tubesOnMap.Add(_tube2);
            }
        }
        private void MoveTube()
        {
            foreach (var tube in _tubesOnMap)
            {
                tube.x -= 3;
            }
            CheckTubeForLivingMap();
            CreateNewTube();

        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImage(_bird.bird, _bird.x, _bird.y, _bird.size, _bird.size);

            foreach (var tube in _tubesOnMap)
            {
                g.DrawImage(tube.tube, tube.x, tube.y, tube.sizeX, tube.sizeY);
            }

        }

        private void MainForm_MouseClick(object sender, MouseEventArgs e)
        {

            if (e.X >= 0 && e.X <= Width && e.Y >= 0 && e.Y <= Height)
            {
                _gravity = 0;
                _bird.gravityValue = -1.5f;
            }
        }
    }
}