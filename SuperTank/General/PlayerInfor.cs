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

        // đọc thông tin level người chơi đã được lưu
        public static void ReadPlayerLevel(string fileName)
        {
            using (StreamReader reader = new StreamReader(Common.path + fileName))
            {
                PlayerInfor.level = int.Parse(reader.ReadLine());
            }
        }

        // ghi thông tin level người chơi vào file
        public static void WritePlayerLevel(string path)
        {
            using(StreamWriter writer = new StreamWriter(Common.path + path))
            {
                writer.WriteLine(PlayerInfor.level);
            }
        }

    }
}
