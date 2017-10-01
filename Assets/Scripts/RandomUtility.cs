using UnityEngine;

static public class RandomUtility
{
    static public float sign { get { return Mathf.Sign(Random.value - 0.5f); } }

    /// <summary>
    /// 获取随机索引，weight为每个索引的权重（自动取绝对值）。失败返回-1
    /// </summary>
    /// <param name="weight">权重值</param>
    /// <returns>返回随机索引</returns>
    static public int Index(params float[] weight)
    {
        float sum = 0f;
        for (int i = 0; i < weight.Length; i++)
        {
            weight[i] = Mathf.Abs(weight[i]);
            sum += weight[i];
        }
        float value = Random.Range(0f, sum);
        for (int i = 0; i < weight.Length; i++)
        {
            value -= weight[i];
            if (value <= 0)
                return i;
        }
        return -1;
    }

    /// <summary>
    /// 获取随机范围内（-extents到extents）的值
    /// </summary>
    /// <param name="extents">范围</param>
    /// <returns>获取的值</returns>
    static public float Extents(float extents)
    {
        return Random.Range(-extents, extents);
    }

}
