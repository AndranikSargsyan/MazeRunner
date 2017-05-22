/*
 * Author: Andranik Sargsyan 
*/

using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Collections;
using System.Collections.Generic;

namespace MazeRunner
{
    class Scene
    {
        private Canvas worldCanvas;
        private Maze maze;
        private Dictionary<string, Runner> runners;

        public Scene(Canvas wc, Maze maze)
        {
            this.worldCanvas = wc;
            this.maze = maze;
            this.runners = new Dictionary<string, Runner>();
        }

        public void drawMaze()
        {
            string[,] blueprint = maze.getBlueprint();
            BitmapImage blockBitmap = new BitmapImage(new Uri("pack://application:,,,/Resources/block.png"));

            for (int i = 0; i < maze.getHeight(); i++)
            {
                for (int j = 0; j < maze.getWidth(); j++)
                {
                    if (blueprint[i, j] == "#")
                    {
                        Image block = new Image();
                        block.Source = blockBitmap;
                        block.Width = blockBitmap.Width;
                        block.Height = blockBitmap.Height;
                        Canvas.SetLeft(block, 30 * j);
                        Canvas.SetTop(block, 30 * i);
                        worldCanvas.Children.Add(block);
                    }
                }
            }
        }

        public void addRunner(Runner runner)
        {
            runners.Add(runner.getName(), runner);                      
            Image runnerImage = runner.getImage();

            if (runnerImage.Source == null)
            {
                string bitmapURI;

                switch (runner.getName())
                {
                    case "Righthand Runner":
                        bitmapURI = "pack://application:,,,/Resources/righthandRunner.png";
                        break;

                    case "Random Runner":
                        bitmapURI = "pack://application:,,,/Resources/randomRunner.png";
                        break;

                    case "Recursive Runner":
                        bitmapURI = "pack://application:,,,/Resources/recursiveRunner.png";
                        break;

                    default:
                        bitmapURI = "pack://application:,,,/Resources/righthandRunner.png";
                        break;
                }

                BitmapImage runnerBitmap = new BitmapImage(new Uri(bitmapURI));
                runnerImage.Source = runnerBitmap;
                runnerImage.Width = runnerBitmap.Width;
                runnerImage.Height = runnerBitmap.Height;

                Canvas.SetLeft(runnerImage, worldCanvas.Width / maze.getWidth() * runner.getX() + 7);
                Canvas.SetTop(runnerImage, worldCanvas.Height / maze.getHeight() * runner.getY() + 7);              
            }

            worldCanvas.Children.Add(runner.getImage());
        }

        public void removeRunner(Runner runner)
        {
           if(runners.ContainsKey(runner.getName()))
            {
                runners.Remove(runner.getName());
                worldCanvas.Children.Remove(runner.getImage());
            }     
        }

        public bool update()
        {
            if(runners.Count > 0)
            {
                foreach (var runner in runners)
                {
                    if (!runner.Value.run())
                    {
                        removeRunner(runner.Value);
                        break;
                    }                      
                }
                return true;       
            }
            else
            {
                return false;
            }         
        }

        public void render()
        {
            foreach(var entity in runners)
            {
                Runner runner = entity.Value;
                Image runnerImage = runner.getImage(); 
                Canvas.SetLeft(runnerImage, worldCanvas.Width / maze.getWidth() * runner.getX() + 7);
                Canvas.SetTop(runnerImage, worldCanvas.Height / maze.getHeight() * runner.getY() + 7);
            }
        }
    }
}
