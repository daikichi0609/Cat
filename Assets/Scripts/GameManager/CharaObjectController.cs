using System.Collections.Generic;
using TsugamerLibrary;
using UnityEngine;

public class CharaObjectController : Singleton<CharaObjectController>
{
    public List<ComponentCollector> FindCharacter(Vector3 center, float radius)
    {
        var list = new List<ComponentCollector>();

        var com = CharaObjectManager.GetInstance();
        var player = com.Player;
        var playerPos = player.GetInterface<CharaObjectHolder>().MoveObject.transform.position;
        if (playerPos.IsClash(center, radius) == true)
            list.Add(player);

        foreach (var enemy in com.EnemyList)
        {
            var enemyPos = enemy.GetInterface<CharaObjectHolder>().MoveObject.transform.position;
            if (enemyPos.IsClash(center, radius) == true)
                list.Add(enemy);
        }

        return list;
    }
}

public static class CollisionExtension
{
    public static bool IsClash(this Vector3 self, Vector3 opp, float d) => (self - opp).magnitude <= d;
}