using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using SuperTank.General;

namespace SuperTank.Objects
{
    class WallManagement
    {
        #region Danh sách tường
        private List<Wall> walls;
        #endregion

        public WallManagement()
        {
            walls = new List<Wall>();
        }

        // tạo một danh sách tường
        public void CreatWall(int[,] map, int level)
        {
            Wall wall = null;
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    // nếu map tại vị trí i, j == 0 là không có tường
                    // ngược lại tạo tường tại i, j
                    if (map[i, j] != 0)
                    {
                        wall = new Wall();
                        wall.RectX = j * Common.STEP;
                        wall.RectY = i * Common.STEP;
                        wall.RectWidth = Common.STEP;
                        wall.RectHeight = Common.STEP;
                        wall.WallNumber = map[i, j];
                        switch(map[i, j])
                        {
                            case 1:
                            case 2:
                                // là gạch
                                wall.LoadImage(Common.path + @"\Images\wall" + map[i, j] +(int)(level / 2.2) + ".png");
                                break;
                            case 3:
                                // là thép
                                wall.LoadImage(Common.path + @"\Images\wall" + map[i, j] + ".png");
                                break;
                            case 4:
                                // là bụi cây
                                wall.LoadImage(Common.path + @"\Images\wall" + map[i, j] + (int)(level / 6) + ".png");
                                break;
                            case 5:
                                break;
                        }
                        walls.Add(wall);
                    }
                }
            }
            wall = null;
            Console.WriteLine("Số lượng tường: " + walls.Count);
        }

        // hủy một viên gạch trong danh sách
        public void RemoveOneWall(int index)
        {
            this.walls[index] = null;
            this.walls.RemoveAt(index);
        }

        // giải phóng danh sách tường
        public void WallsClear()
        {
            this.walls.Clear();
        }

        // hiển thị toàn bộ danh sách tường lên bit map nền
        public void ShowAllWall(Bitmap background)
        {
            foreach (var wall in walls)
            {
                // nếu số của tường khác 6 là khác castle mới vẽ
                if (wall.WallNumber != 6)
                    wall.Show(background);
            }
        }

        #region properties
        public List<Wall> Walls
        {
            get
            {
                return walls;
            }

            set
            {
                walls = value;
            }
        }
        #endregion properties
    }
}
