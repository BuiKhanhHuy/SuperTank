using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank.General
{
    class PlayerInfor
    {
        public static int level = 1;
        public static int rank = 0;

        // đọc thông tin level người chơi đã được lưu
        public static void ReadPlayerLevel(string fileName)
        {
            using (StreamReader reader = new StreamReader(Common.path + fileName))
            {
                string[] token = reader.ReadLine().Split('#');
                PlayerInfor.level = int.Parse(token[0]);
                PlayerInfor.rank = int.Parse(token[1]);
            }
        }

        // ghi thông tin level người chơi vào file
        public static void WritePlayerLevel(string path)
        {
            using (StreamWriter writer = new StreamWriter(Common.path + path))
            {
                writer.WriteLine(PlayerInfor.level + "#" + PlayerInfor.rank);
            }
        }

    }
}
