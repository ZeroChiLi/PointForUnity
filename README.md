# PointForUnity
A Convenient Tool For Unity, Can Be Used As Spawn Point Or Way Point,etc.

Unity 自制工具，方便标记出生点或巡逻点等功能。

--- 
##Point（点）

定义：只有位置和旋转角度两个属性的类对象。

```csharp
[System.Serializable]
public class Point
{
    public Vector3 position;
    public Vector3 eulerAngles;
    public Quaternion rotation { get { return Quaternion.Euler(eulerAngles); } set { eulerAngles = value.eulerAngles; } }

    public Point() { }
    public Point(Vector3 position, Quaternion rotation) { this.position = position; this.rotation = rotation; }
    public Point(Vector3 position, Vector3 eulerAngles) { this.position = position; this.eulerAngles = eulerAngles; }
}

```

---
##Points（点列表）

![](http://ox99tvy17.bkt.clouddn.com/02.png)

![](http://ox99tvy17.bkt.clouddn.com/g01.gif)
直接点击Gizmos可以选中点列表，控制Appearance来更方便控制点的位置角度。

![](http://ox99tvy17.bkt.clouddn.com/g02.gif)
点击按钮可以直接聚焦到对应的点。选择移动或旋转工具，可以直接在场景中控制点的位置已经旋转角度。

![](http://ox99tvy17.bkt.clouddn.com/g04.gif)
可以控制transform的位置和旋转来改变所有点的位置。除了缩放对所有点没有任何影响。

| 变量名 | 说明 |
| ---- | ---- | 
| Appearance | 外观。对点没有实际影响。 |
| Keep Gizmos | 在点列表对象没有选择时，仍然保持显示Gizmos。 |
| Use Screen Size | 使用屏幕尺寸显示点。打勾后所有点大小都会保持一致，不管镜头在什么位置。 |
| Axis Length | 每个点三个虚拟轴的长度。 |
| Focus Size | 在下方每个点项按钮点击时，会对焦到对应的点，这个变量是控制对焦后场景大小。 |
| Current Index | 当前点索引，用于函数中的变量，对应点列表索引。 |
| Looped | 是否循环，在调用GetNextPoint()时，如果当前是最后一个点，会重新跳到第一个点。 |
| Points Details | 点列表。前面两条杠拖拽可以控制点的顺序。按钮用于对焦选择点。直接点击这一项即是选中该点（可以在场景中点击）。 |

### 实例1.在每个点同时作为出生点和巡逻点

![](http://ox99tvy17.bkt.clouddn.com/g03.gif)

```csharp

    public Points points;

	// 作为出生点。因为定义了索引器，所以直接使用points[i]就代表索引为i对应的点。
    private void Start()
    {
        Point worldSpacePoint;
        for (int i = 0; i < points.Count; i++)
        {
            chickenList.Add(Instantiate(prefab, transform).GetComponent<ChickenManager>());
			//其中GetWorld***()的方法是获取世界坐标下的点，因为该点列表是基于GameObject的Transform的。
            worldSpacePoint = points.GetWorldSpacePoint(points[i]);
            chickenList[i].transform.position = worldSpacePoint.position;
            chickenList[i].transform.rotation = worldSpacePoint.rotation;
        }
    }

	// 作为巡逻点。如果没有勾选Looped，那么在最后一个点调用GetNextPoint()将是为null。
    private void Update()
    {
        elapsed -= Time.deltaTime;
        if (elapsed <= 0)
        {
            nextPoint = points.GetNextPoint();
			if (nextPoint != null)
            	for (int i = 0; i < chickenList.Count; i++)
                    chickenList[i].SetNextWayPoint(points.GetWorldPosition(nextPoint));
            elapsed = changeNextWayPointTime;
        }
    }

```

---
##Annulus Area（环状区域）

![](http://ox99tvy17.bkt.clouddn.com/03.png)

![](http://ox99tvy17.bkt.clouddn.com/g05.gif)
可在场景直接拖拽缩放杆，来控制区域大小，控制angle来控制区域的扇形面积。

![](http://ox99tvy17.bkt.clouddn.com/g06.gif)
同样可以控制Transform来控制区域的位置，同样不受缩放影响。


| 变量名 | 说明 |
| ---- | ---- | 
| Orientation Type | 点的方向。如下面的图。 |
| Orientation Euler Angle | 点方向角度。仅对类型Same和Rule有效。 |
| Appearance | 外观。 |
| Angle | 扇形面积的角度。 |
| MinRadius | 区域最小半径。 |
| MaxRadius | 区域最大半径。 |

###Qrientation Type

1. Same，获取的点旋转角都相同。
![](http://ox99tvy17.bkt.clouddn.com/04.png)

2. Rule，有规律的。
　　实际就是一条代码，`orientationAngle * Quaternion.LookRotation(offset)` ，orientationAngle是点方向角度，offset就是点相对圆心的位置偏移量。

	2.1. 角度为0，所有点相对圆心向外。
![](http://ox99tvy17.bkt.clouddn.com/05.png) 
	2.2. y为180，所有点相对圆心向内。
![](http://ox99tvy17.bkt.clouddn.com/06.png)
	2.3. y为90，所有点从上往下看是顺时针方向。
![](http://ox99tvy17.bkt.clouddn.com/07.png)

3. Random，随机方向。不受角度输入影响。
![](http://ox99tvy17.bkt.clouddn.com/08.png)

### 实例2.环状区域中（或边缘）随机产生超载鸡们。
![](http://ox99tvy17.bkt.clouddn.com/g07.gif)

```csharp
    private void ResetChicken(ChickenManager c)
    {
		// 判断是否打勾inEgde来判断产生位置（已经转换到世界坐标）。
        temP = inEgde ? area.GetRandomPointInEdge() : area.GetRandomPointInArea();
        c.transform.position = temP.position;
        c.transform.rotation = temP.rotation;
        c.autoMoveForward = autoMove;
    }
```

---
##Sphere Area（球状区域）
![](http://ox99tvy17.bkt.clouddn.com/09.png)
继承于Annulus Area，原理相同，但是不支持角度控制，因为光想想怎么绘制出被裁切的球体就觉得很麻烦。

### 实例3.球状区域中随机产生超载鸡们。
![](http://ox99tvy17.bkt.clouddn.com/g08.gif)

---
##Cube Area （立方体区域）
![](http://ox99tvy17.bkt.clouddn.com/10.png)
这个更简单，只有一个区域范围控制。

![](http://ox99tvy17.bkt.clouddn.com/g09.gif)
同样可以通过transform改变位置旋转，而且支持修改缩放控制大小。

### 实例4.立方体区域中随机产生超载鸡们。
![](http://ox99tvy17.bkt.clouddn.com/g10.gif)

---
