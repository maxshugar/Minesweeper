using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Minesweeper.Properties;
using System.Diagnostics;
using System.Xml.Serialization;
using System.IO;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
/* 11/12/18 */
/* Max Rochefort-Shugar - USW HND COMPUTING - Computer Programming 2 - Assignment 1 - Minesweeper
* 
* I have followed a tutorial on youtube which has provided me with the basic functionality of a minesweeper game. 
* Please follow this link: https://www.youtube.com/watch?v=01XtKAVS6-s to watch the tutorial. I have studied the tutorials 
* code and written my own comments to explain what is happening.
* 
* I have added the following advanced functionality myself as suggested on Moodle:
* - Custom icons : https://www.flaticon.com/packs/numbers
* - Insane mode - mines move every five goes - although it does work, this actually makes the game easier!
* - Reset game
* - Timer – counts time to complete
* - Highscore for each difficulty saved to XML
* - Transparrent help Overlay
* - Game Over graphic
* - Win Graphic
* - Highscore graphic
* - Simplified some of the code.
*/

namespace Minesweeper
{
    public partial class MinesweeperForm : Form
    {

        static Stopwatch sw = new Stopwatch();
        static Timer timer = new Timer();

        /* Declare a static form variable for holding a form instance. */
        public static MinesweeperForm Form__;
        /* Create a difficulty enum for difficulty selection. */
        public enum Difficulty { Insane, Expert, Intermediate, Beginner };
        /* Set the start difficulty to beginner. */
        static private Difficulty difficulty = Difficulty.Beginner;
        
        /* Base class for holding high scores */
        public class high_score
        {
            public string Time { get; set; }
            public Difficulty Score_difficulty { get; set; }
        }

        /* List of highscores */
        static public List<high_score> high_scores = new List<high_score>();

        /* This is the first function to be executed. */
        public MinesweeperForm()
        {
            /* Set static form to this instance of the form. */
            Form__ = this;
           /* Run windows generated code. */
            InitializeComponent();
            /* Combo box beginner difficulty selected by default */
            cbDifficulty.SelectedIndex = 0;
            /* Create an instance of the XmlSerializer class, specifying the type of object to be deserialized. */
            var serializer = new XmlSerializer(high_scores.GetType(), "HighScores.Scores");
            object obj;
            /* Read from highscores.xml. */
            using (var reader = new StreamReader("highscores.xml"))
            {
                /* Deserialize the xml file. */
                obj = serializer.Deserialize(reader.BaseStream);
            }
            /* Store deserialized highscore object in high_scores object list. */
            high_scores = (List<high_score>)obj;
            /* Load the game for the first time */
            this.LoadGame(null, null);
            /* Add an event listener, Update the flag counter value. */
            this.tileGrid.TileFlagStatusChange += (sender, e) => { this.flagCounter.Text = e.Flags.ToString(); };
            /* Display the time eclapsed everytime the timer ticks. */
            timer.Tick += (sender, e) => { lblTime.Text = sw.Elapsed.ToString("mm\\:ss\\:ff"); };
            /* Start the timer. */
            timer.Start();
        }
        
        /* Sets number of mines and grid size based on the selected difficulty and invokes LoadGrid. */
        private void LoadGame(object sender, EventArgs e)
        {
            /* Hide Picture Boxes. These will be shown later. */
            pbGO.Hide();
            pbWin.Hide();
            pbHS.Hide();
            int x, y, mines;
            /* Set grid size and number of mines based on difficulty switch statement. */
            switch (difficulty)
            {
                case Difficulty.Beginner:
                    x = y = 9;
                    mines = 15;
                    break;
                case Difficulty.Intermediate:
                    x = y = 16;
                    mines = 40;
                    break;
                case Difficulty.Expert:
                    x = 30;
                    y = 16;
                    mines = 60;
                    break;
                case Difficulty.Insane:
                    x = 30;
                    y = 16;
                    mines = 60;
                    break;
                default:
                    throw new InvalidOperationException("Unimplemented difficulty selected");
            }
            /* Generate the tile grid and load with mines. */
            tileGrid.LoadGrid(new Size(x, y), mines);
            /* Set the form maximum and minimum size to prevent the user from resizing the form. */
            MaximumSize = this.MinimumSize = new Size(this.tileGrid.Width + 140, this.tileGrid.Height + 65);
            /* Make the initial flag counter equal to the number of mines for the selected difficulty. */
            flagCounter.Text = mines.ToString();
            /* Iterate over each highscore. */
            for(int i = 0; i < high_scores.Count; i++)
            {
                /* If the highscore difficulty is equal to the selected difficulty. */
                if(high_scores[i].Score_difficulty == difficulty)
                {
                    /* Parse the time from the object. */
                    TimeSpan Highscore = TimeSpan.Parse(high_scores[i].Time);
                    /* Display the highscore. */
                    this.lblHighscore.Text = Highscore.ToString("mm\\:ss\\.ff");
                    break;
                }
                /* If a highscore has not been found. */
                else if(i == high_scores.Count -1)
                {
                    /* Empty the highscore label. */
                    this.lblHighscore.Text = "";
                }
            }
        }
        
        /* Definition of the TileGrid class which is of type Panel. */
        private class TileGrid : Panel
        {
            /* Used as part of a recursive function to open adjacent tiles that are not mines. */
            private static readonly HashSet<Tile> gridSearchBlacklist = new HashSet<Tile>();
            /* Keeps track of the tiles that have been opened. */
            private static readonly HashSet<Point> openedTilePoints = new HashSet<Point>();
            /* Used to randomly generate mine positions. */
            private static readonly Random random = new Random();
            
            private Size gridSize;
            private int mines;
            private int flags;
            private int ClickCount;
            private bool minesGenerated;

            /* Point to the */
            internal event EventHandler<TileFlagStatusChangedEventArgs> TileFlagStatusChange = delegate { };
            internal event EventHandler<TileClickEventArgs> TileClickStatusChange = delegate { };

            private Tile this[Point point] => (Tile)Controls[$"Tile_{point.X}_{point.Y}"];

            /* Renders tiles to the grid and invokes SetAdjacentTiles. */
            internal void LoadGrid(Size gridSize, int mines)
            {
                this.minesGenerated = false;
                /* Remove all controls from the grid. */
                this.Controls.Clear();
                this.gridSize = gridSize;
                this.mines = this.flags = mines;
                this.Size = new Size(gridSize.Width * Tile.LENGTH, gridSize.Height * Tile.LENGTH);
                /* Add tiles to the grid. */
                for (int x = 0; x < gridSize.Width; x++)
                {
                    for (int y = 0; y < gridSize.Height; y++)
                    {
                        Tile tile = new Tile(x, y);
                        /* Custom event listener triggered when the user clicks on a tile. */
                        tile.MouseDown += Tile_MouseDown;
                        this.Controls.Add(tile);
                    }
                }
                /* Iterate over every tile in the grid. */
                foreach (Tile tile in this.Controls)
                {
                    /* Add all adjacent tiles to the internal adjacent tile list. */ 
                    tile.SetAdjacentTiles();
                }
            }

            /* Event listener which generates mines, handles flags and invokes CheckForWin. */
            private void Tile_MouseDown(object sender, MouseEventArgs e)
            {
                Tile tile = (Tile)sender;
                if (!tile.Opened)
                {
                    //ShowMinesDebug(); /* UNCOMMENT THIS TO SEE ALL MINES WHEN PLAYING THE GAME */
                    switch (e.Button)
                    {
                        /* When the user left clicks on an unflagged tile. */
                        case MouseButtons.Left when !tile.Flagged:
                            ClickCount++;
                            /* If this is the first tile the user clicks on. Generate mines and start the game. */
                            if (!this.minesGenerated)
                            {
                                this.ClickCount = 0;
                                timer.Start();
                                sw.Restart();
                                this.GenerateMines(tile);
                            }
                            /* If the user clicks on a mine, disable clicking on all tiles and display Game Over image. */
                            if (tile.Mined)
                            {
                                this.DisableTiles(true);
                                Form__.pbGO.Show();
                            }
                            else
                            {
                                /* Open all tiles that do not have any adjacent mines. */
                                tile.TestAdjacentTiles();
                                /* Clear the adjacent tile blacklist. */
                                gridSearchBlacklist.Clear();
                            }
                            /* Regenerate mines every 5 clicks on Insane mode. */
                            if (this.ClickCount % 5 == 0 && this.ClickCount != 0 && difficulty == Difficulty.Insane)
                            {
                                this.RegenerateMines(tile);
                                //ShowMinesDebug(); /* UNCOMMENT THIS TO SEE ALL MINES WHEN PLAYING THE GAME */
                            }
                            /* Trigger the tile click custom event handler. */
                            TileClickStatusChange(this, new TileClickEventArgs(this.ClickCount));
                            break;
                        /* When the user right clicks on a tile when there are flags left. */
                        case MouseButtons.Right when this.flags > 0:
                            /* Prevent the user placing flags before the mines have been generated. */
                            if (!this.minesGenerated) return;
                                /* Toggle flag. */
                                if (tile.Flagged)
                            {
                                tile.Flagged = false;
                                this.flags++;
                            }
                            else
                            {
                                tile.Flagged = true;
                                this.flags--;
                            }
                            /* Trigger the flag custom event handler. */
                            TileFlagStatusChange(this, new TileFlagStatusChangedEventArgs(this.flags, this.flags < this.mines * 0.25 ? Color.Red : Color.Black));
                            break;
                    }
                }
                /* Check whether all tiles have been opened and all mines have been flagged. */
                this.CheckForWin();
            }

            /* Generates mines to the grid while omitting the first tile selected */
            private void GenerateMines(Tile safeTile)
            {
                int safeTilesCount = safeTile.AdjacentTiles.Length + 1; /* Omit the selected tile and all ajacent tiles from being mines. */
                /* Don't set tile as mine if it has already been flagged. */
                this.mines = this.mines - this.Controls.OfType<Tile>().Count(tile => tile.Flagged); 
                Point[] usedPositions = new Point[this.mines + safeTilesCount]; /* Create an array of positions of size equal to the number of mines and the number of adjacent tiles to the selected tile. */
                usedPositions[0] = safeTile.GridPosition; /* Set the first member of the array to be equal to the first selected tiles grid position. */
                for (int i = 1; i < safeTilesCount; i++) /* Loop through each member of the ajacent tiles to the safe tile. */
                {
                    usedPositions[i] = safeTile.AdjacentTiles[i - 1].GridPosition; /* Add all of adjacent safe tile positions to the array .*/
                }
                for (int i = safeTilesCount; i < usedPositions.Length; i++) /* Loop once for each mine. */
                {
                    /* Generate a random Point on the grid. */
                    Point point = new Point(random.Next(this.gridSize.Width), random.Next(this.gridSize.Height));
                    if (!usedPositions.Contains(point)) /* If the array does not contain the generated point. */
                    {
                        this[point].Mine(); /* Flag the point as a mine. */
                        usedPositions[i] = point; /* Add point to array. */
                    }
                    else /* If the array already contains the point. */
                    {
                        i--; /* Decrement i - generate another random Point. */
                    }
                }
                this.minesGenerated = true;
            }
            /* Used on insane mode every 5 clicks to regenerate the mines in new positions. */
            private void RegenerateMines(Tile safeTile)
            {
                int unopened_count = 0;
                /* Count the number of unopened tiles */
                foreach (Tile tile in this.Controls)
                {
                    if (tile.Opened == false)
                    {
                        unopened_count++;
                    }
                }
                /* Prevent mine regenration before the number of mines is equal to the number of opened tiles. */
                if (unopened_count - 30 <= mines) return;

                /* Remove all mines that have not been flagged. */
                foreach (Tile tile in this.Controls)
                {
                    if (tile.Mined && tile.Flagged == false)
                    {
                        tile.Mined = false;
                        tile.Image = Resources.Tile;
                    }
                }

                int safeTilesCount = safeTile.AdjacentTiles.Length + 1; /* Omit the selected tile and all ajacent tiles from being mines. */
                Point[] usedPositions = new Point[this.mines + safeTilesCount]; /* Create an array of positions */
                usedPositions[0] = safeTile.GridPosition; /* Set the first member of the array to be equal to the first selected tiles grid position. */
                for (int i = 1; i < safeTilesCount; i++) /* Loop through each member of the ajacent tiles to the safe tile. */
                {
                    usedPositions[i] = safeTile.AdjacentTiles[i - 1].GridPosition; /* Add all of adjacent safe tile positions to the array .*/
                }
                for (int i = safeTilesCount; i < usedPositions.Length; i++) /* Loop once for each mine. */
                {
                    /* Generate a random Point on the grid. */
                    Point point = new Point(random.Next(this.gridSize.Width), random.Next(this.gridSize.Height));
                    if (!usedPositions.Contains(point) && !openedTilePoints.Contains(point)) /* If the array does not contain the generated point. */
                    {
                        /* Don't move mines that have been flagged. */
                        if((this[point].Mined == false && this[point].Flagged == false))
                        {
                            this[point].Mine(); /* Flag the point as a mine. */
                            usedPositions[i] = point; /* Add point to array. */
                        }
                        
                    }
                    else /* If the array already contains the point. */
                    {
                        i--; /* Decrement i - generate another random Point. */
                    }
                }

                /* set tiles. */
                foreach (Tile tile in this.Controls)
                {
                    tile.SetAdjacentTiles();
                }

                /* Test tiles */
                foreach (Tile tile in this.Controls)
                {
                    if (openedTilePoints.Contains(tile.GridPosition)){
                        tile.TestAdjacentTiles();
                    }
                }
            }
            /* Prevent the user clicking on tiles and show all mine positions if the game is lost. */
            private void DisableTiles(bool gameLost)
            {
                /* Loop over each tile in the grid */
                foreach (Tile tile in this.Controls)
                {
                    /* Remove the mouse down event listener for each tile */
                    tile.MouseDown -= this.Tile_MouseDown;
                    if (gameLost)
                    {
                        /* Stop the stopwatch */
                        sw.Stop();
                        /* Show all mines */
                        tile.Image = !tile.Opened && tile.Mined && !tile.Flagged ? Resources.Mine :
                            tile.Flagged && !tile.Mined ? Resources.FalseFlaggedTile : tile.Image;
                        }
                    }
            }
            /* Debug function which shows the position of all mines. Helpful when developing new functionality. */
            private void ShowMinesDebug()
            {
                foreach (Tile tile in this.Controls)
                {
                    if (tile.Mined)
                    {
                        tile.Image = Resources.Mine;
                    }
                }
            }
            /* Checks whether all flags have been used and all tiles have been opened. */
            private void CheckForWin()
            {
                if (this.flags != 0 || this.Controls.OfType<Tile>().Count(tile => tile.Opened || tile.Flagged) != this.gridSize.Width * this.gridSize.Height)
                {
                    return;
                }
                /* Stop the stopwatch. */
                sw.Stop();
                /* Check if the high_scores list contains the current difficulty. */
                int difficulty_found = 0;
                for (int i = 0; i < high_scores.Count; i++)
                {
                    if (high_scores[i].Score_difficulty == difficulty)
                    {
                        difficulty_found++;
                    }
                }
                /* Flags whether a new highscore has been achieved. */
                int highscore_flag = 0;

                /* First entry for difficulty. */
                if(difficulty_found == 0)
                {
                    /* New high score. */
                    var score = new high_score() { Time = sw.Elapsed.ToString(), Score_difficulty = difficulty };
                    high_scores.Add(score);
                    /* Show the highscore image. */
                    Form__.pbHS.Show();
                    highscore_flag++;
                }
                /* Difficulty already exists. */
                else
                {
                    /* Get the current high score for this difficulty. */
                    for (int i = 0; i < high_scores.Count; i++)
                    {
                        if (high_scores[i].Score_difficulty == difficulty)
                        {
                            /* If the time eclapsed is less than the current highscore for the difficulty. */
                            if (sw.Elapsed < TimeSpan.Parse(high_scores[i].Time))
                            {
                                /* New high score. */
                                high_scores[i].Time = sw.Elapsed.ToString();
                                Form__.pbHS.Show();
                                highscore_flag++;
                            }
                        }
                    }
                }
                /* Write highscores to XML */
                var serializer = new XmlSerializer(high_scores.GetType(), "HighScores.Scores");
                using (var writer = new StreamWriter("highscores.xml", false))
                {
                    serializer.Serialize(writer.BaseStream, high_scores);
                }
                /* If the user did not get a highscore, display the trophy game picture. */
                if (highscore_flag == 0)
                {
                    Form__.pbWin.Show();
                }
                
                /* Disable all tiles without setting the gameLost flag. */
                this.DisableTiles(false);
            }

            private class Tile : PictureBox
            {
                /* Used to calculate the size of the tile. */
                internal const int LENGTH = 30;
                /* Adjacent coordinates relative to the tile's position in the grid. */
                private static readonly int[][] adjacentCoords =
                {
                    new[] {-1, -1}, new[] {0, -1}, new[] {1, -1}, new[] {1, 0}, new[] {1, 1}, new[] {0, 1},
                    new[] {-1, 1}, new[] {-1, 0}
                };
                /* Used to keep track of whether the tile has been flagged. */
                private bool flagged;
                /* This function is called when the tile is first created. */
                internal Tile(int x, int y)
                {
                    this.Name = $"Tile_{x}_{y}";
                    this.Location = new Point(x * LENGTH, y * LENGTH);
                    this.GridPosition = new Point(x, y);
                    this.Size = new Size(LENGTH-5, LENGTH-5);
                    this.Image = Properties.Resources.Tile;
                    this.SizeMode = PictureBoxSizeMode.Zoom;
                }
       
                internal Point GridPosition { get; }
                internal bool Opened { get; private set; }
                internal bool Mined { get; set; }
                /*  Sets the flag image if the flag boolean is set to true. */
                internal bool Flagged
                {
                    get => this.flagged;
                    set
                    {
                        this.flagged = value;
                        this.Image = value ? Resources.Flag : Resources.Tile;
                    }
                }
                /* An array of tiles that are adjacent to the tile. */
                internal Tile[] AdjacentTiles { get; private set; }
                /* Add tiles that are adjacent to this tile in the grid to the array. */
                internal void SetAdjacentTiles()
                {
                    TileGrid tileGrid = (TileGrid)this.Parent;
                    List<Tile> adjacentTiles = new List<Tile>(8);
                    foreach (int[] adjacentCoord in adjacentCoords)
                    {
                        Tile tile = tileGrid[new Point(this.GridPosition.X + adjacentCoord[0], this.GridPosition.Y + adjacentCoord[1])];
                        if (tile != null)
                        {
                            adjacentTiles.Add(tile);
                        }
                    }
                    this.AdjacentTiles = adjacentTiles.ToArray();
                }

                /* Holds the number of mines that are adjacent to the tile. */
                private int AdjacentMines => this.AdjacentTiles.Count(tile => tile.Mined);

                /* Recursive function that opens tiles that are adjacent to the selected tile that don't have adjacent mines. */
                internal void TestAdjacentTiles()
                {
                    if (this.flagged || gridSearchBlacklist.Contains(this)) /* Stops tiles that have been flagged or have already been opened being opened. */
                    {
                        return;
                    }
                    gridSearchBlacklist.Add(this);/* Add tile to the black list. */
                    openedTilePoints.Add(this.GridPosition);/* Add tile to the opened list */
                    /* If the tile has no adjacent mines. */
                    if (this.AdjacentMines == 0)
                    {
                        /* Iterate through each adjacent tile to this.tile. */
                        foreach (Tile tile in this.AdjacentTiles)
                        {
                            /* Call the function again and use the blacklist to workout what has already been opened. */
                            tile.TestAdjacentTiles();
                        }
                    }
                    /* Open the tile. */
                    this.Open();
                }
                /* Call this function to turn a tile into a mine. */
                internal void Mine()
                {
                    this.Mined = true;
                }
                /* Call this function to open a tile and set the image based on the number of adjacent mines. */
                private void Open()
                {
                    this.Opened = true; /* Set the opened flag to true. */
                    /* Set the image of the tile equal to the number of ajacent mines. */
                    this.Image = (Image)Resources.ResourceManager.GetObject($"EmptyTile_{this.AdjacentMines}");
                }
            }

            /* Event listener that is triggered when the user right clicks on a tile. */
            internal class TileFlagStatusChangedEventArgs : EventArgs
            {
                internal int Flags { get; }
                internal Color LabelColour { get; }

                internal TileFlagStatusChangedEventArgs(int flags, Color labelColour)
                {
                    this.Flags = flags;
                    this.LabelColour = labelColour;
                }
            }
            /* Event listener that is triggered when the user left clicks on a tile. */
            internal class TileClickEventArgs : EventArgs
            {
                internal int Count { get; }
                internal TileClickEventArgs(int count)
                {
                    this.Count = count;
                }
            }
        }
        /* Switches between difficulties using the dropdown menu. */
        private void cbDifficulty_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedDifficulty = (string)cbDifficulty.SelectedItem;
            difficulty = (Difficulty)Enum.Parse(typeof(Difficulty), selectedDifficulty);
            timer.Stop();
            lblTime.Text = "00:00.00";
            this.LoadGame(null, null);
        }
        /* Resets the game back to its original state. */
        private void btnReset_Click(object sender, EventArgs e)
        {
            this.LoadGame(null, null);
            timer.Stop();
            lblTime.Text = "00:00.00";
        }
        /* Triggers an instance of the help overlay. */
        private void btnHelp_Click(object sender, EventArgs e)
        {
            new Plexiglass(this);
        }
        /* This class acts as a transparrent overlay for the form displaying helpful information about the game. */
        class Plexiglass : Form
        {
            public Plexiglass(Form tocover)
            {
                /* Basic properties for the form. */
                this.BackColor = Color.DarkGray;
                this.Opacity = 0.80;      // Tweak as desired
                this.FormBorderStyle = FormBorderStyle.None;
                this.ControlBox = false;
                this.ShowInTaskbar = false;
                this.StartPosition = FormStartPosition.Manual;
                this.AutoScaleMode = AutoScaleMode.None;
                this.Location = tocover.PointToScreen(Point.Empty);
                this.ClientSize = tocover.ClientSize;
                /* Event listeners. Copy the master forms movement and size. */
                tocover.LocationChanged += Cover_LocationChanged;
                tocover.ClientSizeChanged += Cover_ClientSizeChanged;
                this.Show(tocover);
                tocover.Focus();

                // Disable Aero transitions, the plexiglass gets too visible
                if (Environment.OSVersion.Version.Major >= 6)
                {
                    int value = 1;
                    DwmSetWindowAttribute(tocover.Handle, 3, ref value, 4);
                }
                /* Button to close this form. */
                Button CloseHelp = new Button();
                CloseHelp.Text = "Close";
                CloseHelp.Click += (s, e) => { Dispose(); };
                Controls.Add(CloseHelp);
                /* Highlight the clock icon. */
                Label ClockSelector = new Label();
                ClockSelector.BackColor = Color.Blue;
                ClockSelector.Size = new Size(100, 30);
                ClockSelector.Location = new Point(ClientRectangle.Width - 100, 12);
                ClockSelector.ForeColor = Color.Black;
                Controls.Add(ClockSelector);
                /* Display helpful text. */
                Label ClockInfo = new Label();
                ClockInfo.BackColor = Color.Blue;
                ClockInfo.Size = new Size(130, 30);
                ClockInfo.Text = "Displays the time eclapsed.";
                ClockInfo.Location = new Point(ClientRectangle.Width - 230, 12);
                ClockInfo.Font = new Font(ClockInfo.Font, FontStyle.Bold);
                ClockInfo.ForeColor = Color.Black;
                Controls.Add(ClockInfo);
                /* Highlight the flag icon. */
                Label FlagSelector = new Label();
                FlagSelector.BackColor = Color.Blue;
                FlagSelector.Size = new Size(120, 33);
                FlagSelector.Location = new Point(ClientRectangle.Width - 100, 47);
                FlagSelector.Font = new Font(FlagSelector.Font, FontStyle.Bold);
                FlagSelector.ForeColor = Color.Black;
                Controls.Add(FlagSelector);
                /* Display helpful text. */
                Label FlagInfo = new Label();
                FlagInfo.BackColor = Color.Blue;
                FlagInfo.Size = new Size(130, 33);
                FlagInfo.Text = "Displays the number of flags.";
                FlagInfo.Location = new Point(ClientRectangle.Width - 230, 47);
                FlagInfo.Font = new Font(FlagInfo.Font, FontStyle.Bold);
                FlagInfo.ForeColor = Color.Black;
                Controls.Add(FlagInfo);
                /* Highlight the Highscore icon. */
                Label HighScoreSelector = new Label();
                HighScoreSelector.BackColor = Color.Blue;
                HighScoreSelector.Size = new Size(120, 33);
                HighScoreSelector.Location = new Point(ClientRectangle.Width - 100, 84);
                HighScoreSelector.ForeColor = Color.Black;
                Controls.Add(HighScoreSelector);
                /* Display helpful text. */
                Label HighScoreInfo = new Label();
                HighScoreInfo.BackColor = Color.Blue;
                HighScoreInfo.Size = new Size(130, 33);
                HighScoreInfo.Text = "Highscore for each difficulty.";
                HighScoreInfo.Location = new Point(ClientRectangle.Width - 230, 84);
                HighScoreInfo.Font = new Font(FlagInfo.Font, FontStyle.Bold);
                HighScoreInfo.ForeColor = Color.Black;
                Controls.Add(HighScoreInfo);
                /* Highlight the reset button. */
                Label ResetSelector = new Label();
                ResetSelector.BackColor = Color.Blue;
                ResetSelector.Size = new Size(120, 27);
                ResetSelector.Location = new Point(ClientRectangle.Width - 100, ClientRectangle.Height - 74);
                ResetSelector.ForeColor = Color.Black;
                Controls.Add(ResetSelector);
                /* Display helpful text. */
                Label ResetInfo = new Label();
                ResetInfo.BackColor = Color.Blue;
                ResetInfo.Size = new Size(130, 27);
                ResetInfo.Text = "Press this button to start a new game.";
                ResetInfo.Location = new Point(ClientRectangle.Width - 230, ClientRectangle.Height - 74);
                ResetInfo.Font = new Font(FlagInfo.Font, FontStyle.Bold);
                ResetInfo.ForeColor = Color.Black;
                Controls.Add(ResetInfo);
                /* Highlight the difficulty dropdown. */
                Label DifficultySelector = new Label();
                DifficultySelector.BackColor = Color.Blue;
                DifficultySelector.Size = new Size(120, 27);
                DifficultySelector.Location = new Point(ClientRectangle.Width - 100, ClientRectangle.Height - 44);
                DifficultySelector.ForeColor = Color.Black;
                Controls.Add(DifficultySelector);
                /* Display helpful text. */
                Label DifficultyInfo = new Label();
                DifficultyInfo.BackColor = Color.Blue;
                DifficultyInfo.Size = new Size(130, 27);
                DifficultyInfo.Text = "Use the dropdown to select difficulty.";
                DifficultyInfo.Location = new Point(ClientRectangle.Width - 230, ClientRectangle.Height - 44);
                DifficultyInfo.Font = new Font(FlagInfo.Font, FontStyle.Bold);
                DifficultyInfo.ForeColor = Color.Black;
                Controls.Add(DifficultyInfo);
                /* Display helpful text. */
                Label GridInfo = new Label();
                GridInfo.BackColor = Color.Red;
                GridInfo.Size = new Size(160, 110);
                GridInfo.Text = "The aim is to flag all mines and open all tiles in the fastest time possible.\nLeft click to open a tile.\nRight click to place a Flag.\nIf you click on a mine, you loose!";
                GridInfo.Location = new Point(10, 120);
                GridInfo.Font = new Font(FlagInfo.Font, FontStyle.Bold);
                GridInfo.ForeColor = Color.Black;
                Controls.Add(GridInfo); 
            }

            private void Cover_LocationChanged(object sender, EventArgs e)
            {
                // Ensure the plexiglass follows the owner
                this.Location = Form__.PointToScreen(Point.Empty);
            }
            private void Cover_ClientSizeChanged(object sender, EventArgs e)
            {
                // Ensure the plexiglass keeps the owner covered
                this.ClientSize = Form__.ClientSize;
            }
            protected override void OnFormClosing(FormClosingEventArgs e)
            {
                // Restore owner
                Form__.LocationChanged -= Cover_LocationChanged;
                Form__.ClientSizeChanged -= Cover_ClientSizeChanged;
                if (!Form__.IsDisposed && Environment.OSVersion.Version.Major >= 6)
                {
                    int value = 1;
                    DwmSetWindowAttribute(Form__.Handle, 3, ref value, 4);
                }
                base.OnFormClosing(e);
            }

            [DllImport("dwmapi.dll")]
            private static extern int DwmSetWindowAttribute(IntPtr hWnd, int attr, ref int value, int attrLen);
        }
    }
}

