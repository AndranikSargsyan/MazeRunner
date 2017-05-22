/*
 * Author: Andranik Sargsyan 
 * 
 * Notice: Maze can be changed from Resources/maze.txt file, but after 
 * modifying it please check runners' default positions before building 
 * the solution.
 * 
 * I wanted to add special functionality for simplified maze editing, but
 * as the time was limited, I didn't manage to do.
*/

using System;
using System.Windows;
using System.Windows.Input;

namespace MazeRunner
{
    public partial class MainWindow : Window
    {
        private Scene scene;
        private Maze maze;
        private Runner righthandRunner;
        private Runner randomRunner;
        private Runner recursiveRunner;

        private System.Windows.Threading.DispatcherTimer refresh;

        public MainWindow()
        {       
            InitializeComponent();

            // Initializing Scene
            maze = Maze.getInstance();
            scene = new Scene(worldCanvas, maze);
            scene.drawMaze();
            
            // Initializing Runners
            righthandRunner = new RighthandRunner(maze, new Position(9, 1));
            randomRunner = new RandomRunner(maze, new Position(18, 1));
            recursiveRunner = new RecursiveRunner(maze, new Position(15, 1));

            // Initializing Timer
            refresh = new System.Windows.Threading.DispatcherTimer();
            refresh.Tick += new EventHandler(refresh_Tick);
            refresh.Interval = new TimeSpan(1000000);        
        }

        void refresh_Tick(object sender, EventArgs e)
        {
            if(scene.update())
            {
                scene.render();
            } else
            {
                refresh.Stop();
            }
        }

        private void run_Click(object sender, RoutedEventArgs e)
        {
            refresh.Start();
            run.Content = "Run";
        }

        private void stop_Click(object sender, RoutedEventArgs e)
        {
            refresh.Stop();
            run.Content = "Continue";
        }

        private void righthandCheck_Checked(object sender, RoutedEventArgs e)
        {
            scene.addRunner(righthandRunner);
            righthandPosRadio.IsEnabled = true;
        }

        private void righthandCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            scene.removeRunner(righthandRunner);
            righthandPosRadio.IsEnabled = false;
            righthandPosRadio.IsChecked = false;
        }

        private void randomCheck_Checked(object sender, RoutedEventArgs e)
        {
            scene.addRunner(randomRunner);
            randomPosRadio.IsEnabled = true;
        }

        private void randomCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            scene.removeRunner(randomRunner);
            randomPosRadio.IsEnabled = false;
            randomPosRadio.IsChecked = false;
        }

        private void recursiveCheck_Checked(object sender, RoutedEventArgs e)
        {
            scene.addRunner(recursiveRunner);
            recursivePosRadio.IsEnabled = true;
        }

        private void recursiveCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            scene.removeRunner(recursiveRunner);
            recursivePosRadio.IsEnabled = false;
            recursivePosRadio.IsChecked = false;
        }

        private void changePosition_Click(object sender, RoutedEventArgs e)
        {
            refresh.Stop();
            run.Content = "Continue";
        }

        private void worldCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            int posX = (int)e.GetPosition(worldCanvas).X / ((int) worldCanvas.Width/maze.getWidth());
            int posY = (int)e.GetPosition(worldCanvas).Y / ((int) worldCanvas.Height/maze.getHeight());

            if ((bool) righthandPosRadio.IsChecked)
            {            
                if (!maze.isBlock(posX, posY))
                {
                    righthandRunner.setPosition(posX, posY);
                    scene.render();
                }
            } else if ((bool)randomPosRadio.IsChecked)
            {
                if (!maze.isBlock(posX, posY))
                {
                    randomRunner.setPosition(posX, posY);
                    scene.render();
                }
            } else if ((bool)recursivePosRadio.IsChecked)
            {
                if (!maze.isBlock(posX, posY))
                {
                    recursiveRunner.setPosition(posX, posY);
                    scene.render();
                }
            }
        }
    }
}
