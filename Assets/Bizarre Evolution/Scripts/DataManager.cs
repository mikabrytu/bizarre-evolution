using System.Collections;
using System.Collections.Generic;

public static class DataManager
{
    
    public static bool isTimeRunnig = true;
    
    /*  Enemy Ids:
    *   1 - Sun
    *   2 - Sword
    *   3 - Garlic
    */
    public static int goalEnemyId = 0;
    public static int goalEnemy = 0;
    public static int goalCount = 0;
    public static int goal = 0;

    public static int level = 0;
    public static bool[] unlockedPowers = {false, false, false};
    
}
