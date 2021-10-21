using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using SuperTank.General;
namespace SuperTank
{
    class Sound
    {
        private static SoundPlayer startSound;
        private static SoundPlayer clickRoomSound;
        private static SoundPlayer hitByBulletsSound;
        public static void InitSound(String path)
        {
            Sound.startSound = new SoundPlayer(path + @"\Sounds\startSound.wav");
            Sound.clickRoomSound = new SoundPlayer(path + @"\Sounds\clickRoomSound.wav");
            Sound.hitByBulletsSound = new SoundPlayer(path + @"\Sounds\hitByBulletsSound.wav");
        }

        // phát âm thanh bắt đầu
        public static void PlayStartSound()
        {
            Sound.startSound.Play();
        }

        // dừng âm thanh bắt đầu
        public static void StopStartSound()
        {
            Sound.startSound.Play();
        }

        // phát âm thanh click
        public static void PlayClickRoomSound()
        {
            Sound.clickRoomSound.Play();
        }

        // dừng âm thanh click
        public static void StopClickRoomSound()
        {
            Sound.clickRoomSound.Play();
        }

        // phát âm thanh trúng đạn
        public static void PlayHitByBulletsSound()
        {
            Sound.hitByBulletsSound.Play();
        }

        // dừng âm thanh trúng đạn
        public static void StopHitByBulletsSound()
        {
            Sound.hitByBulletsSound.Play();
        }

    }
}
