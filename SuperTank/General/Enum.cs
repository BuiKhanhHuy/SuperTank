﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank.General
{
    public enum Direction
    {
        eLeft, eRight, eUp, eDown
    }
    public enum ExplosionSize
    {
        eSmallExplosion, eBigExplosion
    }
    public enum EnemyTankType
    {
        // 0, 1, 2
        eNormal, eMedium, eHard
    }
    public enum Skin
    {
        // 0, 1, 2, 3, 4, 5, 6, 7
       eGreen, eRed, eYellow, eBlue, ePurple, eLightBlue, eOrange, ePink
    }
    public enum ItemType
    {
        eItemHeart, eItemShield, eItemGrenade, eItemTimer
    }
}
