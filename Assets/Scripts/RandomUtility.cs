using UnityEngine;

static public class RandomUtility 
{
    /// <summary>
    /// 获取随机正负值（1或-1）
    /// </summary>
    /// <returns>随机正负值（1或-1）</returns>
    static public float GetRandomSign()
    {
        return Mathf.Sign(Random.value - 0.5f);
    }


}
