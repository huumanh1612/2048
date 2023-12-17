namespace UI
{
    public partial class Form1 : Form
    {
        public int[,] map = new int[4, 4];
        public Label[,] labels = new Label[4, 4];
        public PictureBox[,] pics = new PictureBox[4, 4];
        private int score = 0;
        private int highestScore = 0;
        private Label scoreLabel;
        private Label highestScoreLabel;
        private GameControls gameControls;
        private ColorManager colorManager;


        public Form1()
        {
            InitializeComponent();
            this.KeyDown += new KeyEventHandler(KeyBoardEvent);
            map[0, 0] = 1;
            map[0, 1] = 1;
            CreateMap();
            GenerateNewpic();
            GenerateNewpic();
            ResetGame();

            // Initialize the game controls
            gameControls = new GameControls(this);

            // Initialize the color manager
            colorManager = new ColorManager(pics);

            // Initialize the score label
            scoreLabel = new Label();
            scoreLabel.Location = new Point(12, 50);
            this.Controls.Add(scoreLabel);
            UpdateScoreLabel();

            // Initialize the highest score label
            highestScoreLabel = new Label();
            highestScoreLabel.Location = new Point(12, 20);
            this.Controls.Add(highestScoreLabel);
            UpdateHighestScoreLabel();
        }


        private void CreateMap()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    PictureBox pic = new PictureBox();
                    pic.Location = new Point(12 + 56 * j, 73 + 56 * i);
                    pic.Size = new Size(50, 50);
                    pic.BackColor = Color.White;
                    this.Controls.Add(pic);
                }
            }
        }


        private void GenerateNewpic()
        {
            Random rnd = new Random();
            int a = rnd.Next(0, 4);
            int b = rnd.Next(0, 4);

            while (pics[a, b] != null)
            {
                a = rnd.Next(0, 4);
                b = rnd.Next(0, 4);
            }
            map[a, b] = 1;
            pics[a, b] = new PictureBox();
            labels[a, b] = new Label();
            labels[a, b].Text = "2";
            labels[a, b].Size = new Size(50, 50);
            labels[a, b].TextAlign = ContentAlignment.MiddleCenter;
            labels[a, b].Font = new Font(new FontFamily("Microsoft Sans Serif"), 13);
            pics[a, b].Controls.Add(labels[a, b]);
            pics[a, b].Location = new Point(12 + b * 56, 73 + 56 * a);
            pics[a, b].Size = new Size(50, 50);
            pics[a, b].BackColor = Color.Yellow;
            this.Controls.Add(pics[a, b]);
            pics[a, b].BringToFront();
        }



        public void ResetGame()
        {
            // Reset game state
            score = 0;

            // Clear existing labels and pictures
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (labels[i, j] != null)
                    {
                        this.Controls.Remove(labels[i, j]);
                        labels[i, j] = null;
                    }

                    if (pics[i, j] != null)
                    {
                        this.Controls.Remove(pics[i, j]);
                        pics[i, j] = null;
                    }
                }
            }

            // Reset map and generate new starting pieces
            Array.Clear(map, 0, map.Length);
            GenerateNewpic();
            GenerateNewpic();
        }

        private void changeColor(int sum, int k, int j)
        {
            colorManager.ChangeColor(sum, k, j);
        }


        private void KeyBoardEvent(object sender, KeyEventArgs e)
        {
            bool ifPicsWasMoved = false;

            switch (e.KeyCode.ToString())
            {
                case "Right":
                    for (int k = 0; k < 4; k++)
                    {
                        for (int l = 2; l >= 0; l--)
                        {
                            if (map[k, l] == 1)
                            {
                                for (int j = l + 1; j < 4; j++)
                                {
                                    if (map[k, j] == 0)
                                    {
                                        ifPicsWasMoved = true;
                                        map[k, j - 1] = 0;
                                        map[k, j] = 1;
                                        pics[k, j] = pics[k, j - 1];
                                        pics[k, j - 1] = null;
                                        labels[k, j] = labels[k, j - 1];
                                        labels[k, j - 1] = null;
                                        pics[k, j].Location = new Point(pics[k, j].Location.X + 56, pics[k, j].Location.Y);
                                    }
                                    else
                                    {
                                        int a = int.Parse(labels[k, j].Text);
                                        int b = int.Parse(labels[k, j - 1].Text);
                                        if (a == b)
                                        {
                                            ifPicsWasMoved = true;
                                            labels[k, j].Text = (a + b).ToString();
                                            score += (a + b);
                                            changeColor(a + b, k, j);
                                            UpdateScoreLabel();
                                            map[k, j - 1] = 0;
                                            this.Controls.Remove(pics[k, j - 1]);
                                            this.Controls.Remove(labels[k, j - 1]);
                                            pics[k, j - 1] = null;
                                            labels[k, j - 1] = null;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    break;
                case "Left":
                    for (int k = 0; k < 4; k++)
                    {
                        for (int l = 1; l < 4; l++)
                        {
                            if (map[k, l] == 1)
                            {
                                for (int j = l - 1; j >= 0; j--)
                                {
                                    if (map[k, j] == 0)
                                    {
                                        ifPicsWasMoved = true;
                                        map[k, j + 1] = 0;
                                        map[k, j] = 1;
                                        pics[k, j] = pics[k, j + 1];
                                        pics[k, j + 1] = null;
                                        labels[k, j] = labels[k, j + 1];
                                        labels[k, j + 1] = null;
                                        pics[k, j].Location = new Point(pics[k, j].Location.X - 56, pics[k, j].Location.Y);
                                    }
                                    else
                                    {
                                        int a = int.Parse(labels[k, j].Text);
                                        int b = int.Parse(labels[k, j + 1].Text);
                                        if (a == b)
                                        {
                                            ifPicsWasMoved = true;
                                            labels[k, j].Text = (a + b).ToString();
                                            score += (a + b);
                                            changeColor(a + b, k, j);
                                            UpdateScoreLabel();
                                            map[k, j + 1] = 0;
                                            this.Controls.Remove(pics[k, j + 1]);
                                            this.Controls.Remove(labels[k, j + 1]);
                                            pics[k, j + 1] = null;
                                            labels[k, j + 1] = null;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    break;
                case "Down":
                    for (int k = 2; k >= 0; k--)
                    {
                        for (int l = 0; l < 4; l++)
                        {
                            if (map[k, l] == 1)
                            {
                                for (int j = k + 1; j < 4; j++)
                                {
                                    if (map[j, l] == 0)
                                    {
                                        ifPicsWasMoved = true;
                                        map[j - 1, l] = 0;
                                        map[j, l] = 1;
                                        pics[j, l] = pics[j - 1, l];
                                        pics[j - 1, l] = null;
                                        labels[j, l] = labels[j - 1, l];
                                        labels[j - 1, l] = null;
                                        pics[j, l].Location = new Point(pics[j, l].Location.X, pics[j, l].Location.Y + 56);
                                    }
                                    else
                                    {
                                        int a = int.Parse(labels[j, l].Text);
                                        int b = int.Parse(labels[j - 1, l].Text);
                                        if (a == b)
                                        {
                                            ifPicsWasMoved = true;
                                            labels[j, l].Text = (a + b).ToString();
                                            score += (a + b);
                                            changeColor(a + b, j, l);
                                            UpdateScoreLabel();
                                            map[j - 1, l] = 0;
                                            this.Controls.Remove(pics[j - 1, l]);
                                            this.Controls.Remove(labels[j - 1, l]);
                                            pics[j - 1, l] = null;
                                            labels[j - 1, l] = null;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    break;
                case "Up":
                    for (int k = 1; k < 4; k++)
                    {
                        for (int l = 0; l < 4; l++)
                        {
                            if (map[k, l] == 1)
                            {
                                for (int j = k - 1; j >= 0; j--)
                                {
                                    if (map[j, l] == 0)
                                    {
                                        ifPicsWasMoved = true;
                                        map[j + 1, l] = 0;
                                        map[j, l] = 1;
                                        pics[j, l] = pics[j + 1, l];
                                        pics[j + 1, l] = null;
                                        labels[j, l] = labels[j + 1, l];
                                        labels[j + 1, l] = null;
                                        pics[j, l].Location = new Point(pics[j, l].Location.X, pics[j, l].Location.Y - 56);
                                    }
                                    else
                                    {
                                        int a = int.Parse(labels[j, l].Text);
                                        int b = int.Parse(labels[j + 1, l].Text);
                                        if (a == b)
                                        {
                                            ifPicsWasMoved = true;
                                            labels[j, l].Text = (a + b).ToString();
                                            score += (a + b);
                                            changeColor(a + b, j, l);
                                            UpdateScoreLabel();
                                            map[j + 1, l] = 0;
                                            this.Controls.Remove(pics[j + 1, l]);
                                            this.Controls.Remove(labels[j + 1, l]);
                                            pics[j + 1, l] = null;
                                            labels[j + 1, l] = null;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    break;
            }
            if (ifPicsWasMoved)
            {
                GenerateNewpic();
                CheckGameEnd();
            }

        }


        private void CheckGameEnd()
        {
            // Check for a game over condition (no more available moves)
            bool gameOver = true;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (map[i, j] == 0)
                    {
                        // There is an empty space, so the game is not over
                        gameOver = false;
                        break;
                    }

                    // Check adjacent tiles for possible moves
                    if (j < 3 && labels[i, j] != null && labels[i, j + 1] != null && labels[i, j].Text == labels[i, j + 1].Text)
                    {
                        // There is a matching tile to the right
                        gameOver = false;
                        break;
                    }

                    if (i < 3 && labels[i, j] != null && labels[i + 1, j] != null && labels[i, j].Text == labels[i + 1, j].Text)
                    {
                        // There is a matching tile below
                        gameOver = false;
                        break;
                    }
                }

                if (!gameOver)
                {
                    break;
                }
            }

            if (gameOver)
            {
                // Create a custom MessageBox
                CustomMessageBox gameOverMessageBox = new CustomMessageBox();
                gameOverMessageBox.StartPosition = FormStartPosition.CenterParent;
                gameOverMessageBox.SetMessage("Game Over! Your score: " + score);
                gameOverMessageBox.ShowDialog();
                ResetGame();
                // Reset the score label
                UpdateScoreLabel();
            }
        }


        public void UpdateScoreLabel()
        {
            scoreLabel.Text = "Score: " + score;
            if (score > highestScore)
            {
                highestScore = score;
                UpdateHighestScoreLabel();
            }
        }

        private void UpdateHighestScoreLabel()
        {
            highestScoreLabel.Text = "Highest: " + highestScore;
        }


        // Override the ProcessCmdKey method to handle key events
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Handle key events for the game
            KeyBoardEvent(this, new KeyEventArgs(keyData));

            // Call the base class implementation
            return base.ProcessCmdKey(ref msg, keyData);
        }

    }

    public class GameControls
    {
        private Form1 parentForm;

        public GameControls(Form1 form)
        {
            parentForm = form;

            // Add an exit button to the form
            Button exitButton = new Button();
            exitButton.Text = "Exit";
            exitButton.Size = new Size(75, 30);
            exitButton.Location = new Point(150, 5);
            exitButton.BackColor = Color.White;
            exitButton.FlatStyle = FlatStyle.Flat;
            exitButton.FlatAppearance.BorderSize = 0;
            exitButton.MouseClick += new MouseEventHandler(ExitButtonMouseClick);
            parentForm.Controls.Add(exitButton);

            // Add a replay button to the form
            Button replayButton = new Button();
            replayButton.Text = "Replay";
            replayButton.Size = new Size(75, 30);
            replayButton.Location = new Point(150, 38);
            replayButton.BackColor = Color.White;
            replayButton.FlatStyle = FlatStyle.Flat;
            replayButton.FlatAppearance.BorderSize = 0;
            replayButton.MouseClick += new MouseEventHandler(ReplayButtonMouseClick);
            parentForm.Controls.Add(replayButton);
        }

        private void ExitButtonMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Application.Exit();
            }
        }

        private void ReplayButtonMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                parentForm.ResetGame();
                parentForm.UpdateScoreLabel();
            }
        }
    }
}

public class ColorManager
{
    private readonly PictureBox[,] pics;

    public ColorManager(PictureBox[,] pics)
    {
        this.pics = pics;
    }

    public void ChangeColor(int sum, int k, int j)
    {
        if (sum % 1024 == 0) pics[k, j].BackColor = Color.Pink;
        else if (sum % 512 == 0) pics[k, j].BackColor = Color.Red;
        else if (sum % 256 == 0) pics[k, j].BackColor = Color.DarkViolet;
        else if (sum % 128 == 0) pics[k, j].BackColor = Color.Blue;
        else if (sum % 64 == 0) pics[k, j].BackColor = Color.Brown;
        else if (sum % 32 == 0) pics[k, j].BackColor = Color.Coral;
        else if (sum % 16 == 0) pics[k, j].BackColor = Color.Cyan;
        else if (sum % 8 == 0) pics[k, j].BackColor = Color.Maroon;
        else pics[k, j].BackColor = Color.Green;
    }
}

public class CustomMessageBox : Form
{
    private Label messageLabel;

    public CustomMessageBox()
    {
        this.Size = new Size(300, 150);
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.ControlBox = false;
        this.Text = "";

        // Initialize the label
        messageLabel = new Label();
        messageLabel.Location = new Point(10, 20);
        messageLabel.Size = new Size(280, 60);
        messageLabel.TextAlign = ContentAlignment.MiddleCenter;
        this.Controls.Add(messageLabel);

        // Add an OK button to close the MessageBox
        Button okButton = new Button();
        okButton.Text = "OK";
        okButton.Size = new Size(75, 30);
        okButton.Location = new Point(112, 90);
        okButton.Click += new EventHandler(OkButtonClick);
        this.Controls.Add(okButton);
    }

    public void SetMessage(string message)
    {
        messageLabel.Text = message;
    }

    private void OkButtonClick(object sender, EventArgs e)
    {
        this.Close();
    }
}
