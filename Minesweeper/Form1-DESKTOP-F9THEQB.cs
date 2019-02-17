using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            /* Array holding the x, y coordinates of the bombs */
            int[,] bomb_locations = new int[20,2] { { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 },
                                                    { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 } };

            /* Function that builds an array holding the x and y coordinates of each bomb */
            int generate_bomb_locations(int num_bombs)
            {
                /* Generate a unique bomb location. */
                for(var i = 0; i < num_bombs; i++)
                {
                    /* Generate two random numbers between 1 and 20 */
                    Random rnd = new Random();
                    int x_coordinate = rnd.Next(1, 20);
                    int y_coordinate = rnd.Next(1, 20);

                    bool unique_coordinate = false;

                    /* Break out of while loop when a unique coordinate has been found. */
                    while(unique_coordinate == false)
                    {
                        /* Innocent until proven guilty. */
                        unique_coordinate = true;

                        /* Check coordinates of existing bombs. */
                        for (var j = 0; j < bomb_locations.GetLength(0); j++)
                        {
                            if (bomb_locations[j, 0] == x_coordinate && bomb_locations[j, 1] == y_coordinate)
                            {
                                unique_coordinate = false;
                            }
                        }

                        if(unique_coordinate == false)
                        {
                            /* Generate new coordinates because bomb already exists */
                            x_coordinate = rnd.Next(1, 20);
                            y_coordinate = rnd.Next(1, 20);
                        }
                    }
                    /* Push bomb location to the array. */
                    bomb_locations[i,0] = x_coordinate;
                    bomb_locations[i, 1] = y_coordinate;
                }

                return 0;
            }

            generate_bomb_locations(20);

            /* Generate x coordinates. */
            for (int i = 0; i < 20; i++)
            {
                /* Generate y coordinates. */
                for (int j = 0; j < 20; j++)
                {
                    /* Assign label properties. */
                    Label lbl = new Label
                    {
                        AutoSize = false,
                        Size = new System.Drawing.Size(20, 20),
                        BackColor = Color.FromName("skyblue"),
                        Name = Convert.ToString("Label" + (i + 1) * (j + 1))
                    };
                    for (var k = 0; k < bomb_locations.GetLength(0); k++)
                    {
                        /* Check if this is a bomb location */
                        if(bomb_locations[k,0] == i && bomb_locations[k, 1] == j)
                        {
                            lbl.Text = "*";
                            lbl.BackColor = Color.FromName("red");
                        }
                        else
                        {
                            /* How many bombs do I have as neighbors? */
                            int number_of_neighbors = 0;

                            int my_x = i;
                            int my_y = j;

                            int[,] neighbors = new int[,] { { my_x + 1, my_y }, { my_x - 1, my_y }, { my_x, my_y + 1 }, { my_x, my_y - 1 }, { my_x + 1, my_y + 1 }, { my_x + 1, my_y - 1 }, { my_x - 1, my_y - 1 }, { my_x - 1, my_y + 1 } };


                            for (var l1 = 0; l1 < bomb_locations.GetLength(0); l1++)
                            {
                                for (var l2 = 0; l2 < neighbors.GetLength(0); l2++)
                                {
                                    if (bomb_locations[l1,0] == neighbors[l2,0] && bomb_locations[l1, 1] == neighbors[l2, 1])
                                    {
                                        number_of_neighbors++;
                                    }
                                }
                            }
                            lbl.Tag = number_of_neighbors;
                            lbl.Text = Convert.ToString(number_of_neighbors);
                        }
                    }
                    lbl.Click += new EventHandler(lbl_click); //assign click handler

                    /* Add label to the table */
                    tableLayoutPanel1.Controls.Add(lbl, i, j);
                    
                }
            }
        }

        private void lbl_click(object sender, EventArgs e)
        {
            Label lbl = sender as Label;

            lbl.BackColor = Color.FromName("red");
            lbl.Text = Convert.ToString(lbl.Tag);
        }
    }
}
