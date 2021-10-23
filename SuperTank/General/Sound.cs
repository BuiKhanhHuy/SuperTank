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
        private static SoundPlayer nextLevelSound;
        private static SoundPlayer gameOverSound;
        private static SoundPlayer gameWinSound;
        private static SoundPlayer clickRoomSound;
        private static SoundPlayer hitByBulletsSound;
        private static SoundPlayer eatItemsSound;
        private static SoundPlayer lowAmmoEnergySound;
        public static void InitSound(String path)
        {
            Sound.startSound = new SoundPlayer(path + @"\Sounds\amThanhGameStart.wav");
            Sound.nextLevelSound = new SoundPlayer(path + @"\Sounds\amThanhNextLevel.wav");
            Sound.gameOverSound = new SoundPlayer(path + @"\Sounds\amThanhGameOver.wav");
            Sound.gameWinSound = new SoundPlayer(path + @"\Sounds\amThanhChienThang.wav");
            Sound.clickRoomSound = new SoundPlayer(path + @"\Sounds\amThanhClick.wav");
            Sound.hitByBulletsSound = new SoundPlayer(path + @"\Sounds\amThanhNo.wav");
            Sound.eatItemsSound = new SoundPlayer(path + @"\Sounds\amThanhAnVatPham.wav");
            Sound.lowAmmoEnergySound = new SoundPlayer(path + @"\Sounds\amThanhNangLuongDanIt.wav");
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

        // phát âm thanh next level
        public static void PlayNextLevelSound()
        {
            Sound.nextLevelSound.Play();
        }

        // dừng âm thanh next level
        public static void StopNextLevelSound()
        {
            Sound.nextLevelSound.Play();
        }

        // phát âm thanh game over
        public static void PlayGameOverSound()
        {
            Sound.gameOverSound.Play();
        }

        // dừng âm thanh game over
        public static void StopGameOverSound()
        {
            Sound.gameOverSound.Play();
        }

        // phát âm thanh game win
        public static void PlayGameWinSound()
        {
            Sound.gameWinSound.Play();
        }

        // dừng âm thanh game win
        public static void StopGameWinSound()
        {
            Sound.gameWinSound.Play();
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

        // phát âm thanh ăn vật phẩm
        public static void PlayEatItemsSound()
        {
            Sound.eatItemsSound.Play();
        }

        // dừng âm thanh ăn vật phẩm
        public static void StopEatItemsSound()
        {
            Sound.eatItemsSound.Play();
        }

        // phát âm thanh năng lượng đạn ít
        public static void PlayLowAmmoEnergySound()
        {
            Sound.lowAmmoEnergySound.Play();
        }

        // dừng âm thanh năng lượng đạn ít
        public static void StopLowAmmoEnergySound()
        {
            Sound.lowAmmoEnergySound.Play();
        }

    }
}
