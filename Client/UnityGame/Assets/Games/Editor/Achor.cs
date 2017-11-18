using UnityEngine;
using System.Collections;
using UnityEditor;

namespace com.ihaiu
{
	public class Achor 
	{

		[MenuItem("Game/Tool UI AchorSelf")]
		[CanEditMultipleObjects]
		public static void AchorSelf()
		{
			Transform[] list =  Selection.transforms;
			foreach(Transform transform in list)
			{
				if(transform is RectTransform)
				{
					UIUtil.AchorSelf((RectTransform) transform);
				}
			}
		}
	}



    public class UIUtil
    {
        public static void AchorSelf(RectTransform rt)
        {
            if (rt == null) return;

            RectTransform pt = (RectTransform)rt.parent;
            if (pt == null) return;

            float pfx = pt.rect.width * pt.pivot.x;
            float pfy = pt.rect.height * pt.pivot.y;

            //---
            Vector2 posmin = new Vector2(0f, 0f);
            posmin.x = rt.localPosition.x + pfx - rt.pivot.x * rt.rect.width;
            posmin.y = rt.localPosition.y + pfy - rt.pivot.y * rt.rect.height;

            //			Debug.Log("rt.position=" + rt.position);
            //			Debug.Log("rt.localPosition=" + rt.localPosition);
            //			Debug.Log("rt.sizeDelta=" + rt.sizeDelta);
            //			Debug.Log("rt.rect=" + rt.rect);
            //			Debug.Log("pt.rect=" + pt.rect);

            Vector2 posmax = Vector2.zero;
            posmax.x = posmin.x + rt.rect.width;
            posmax.y = posmin.y + rt.rect.height;
            //			Debug.Log("posmin=" + posmin);
            //			Debug.Log("posmax=" + posmax);

            //---
            Vector2 a = new Vector2(0f, 0f);
            a.x = posmin.x / pt.rect.width;
            a.y = posmin.y / pt.rect.height;


            Vector2 b = new Vector2(0f, 0f);
            b.x = posmax.x / pt.rect.width;
            b.y = posmax.y / pt.rect.height;

            //			Debug.Log("a=" + a);
            //			Debug.Log("b=" + b);

            //---
            //			a.x = Mathf.Max(0, a.x);
            //			a.y = Mathf.Max(0, a.y);
            //			
            //			b.x = Mathf.Max(0, b.x);
            //			b.y = Mathf.Max(0, b.y);
            //
            //			
            //			a.x = Mathf.Min(1, a.x);
            //			a.y = Mathf.Min(1, a.y);
            //			
            //			b.x = Mathf.Min(1, b.x);
            //			b.y = Mathf.Min(1, b.y);

            //			Debug.Log("rt.offsetMin=" + rt.offsetMin);
            //			Debug.Log("rt.offsetMax=" + rt.offsetMax);


            //----
            float w;
            float h;
            Vector2 c = new Vector2(a.x, a.y);
            Vector2 d = new Vector2(b.x, b.y);


            //----
            if (a.x < 0 && b.x > 1)
            {
                c.x = 0F;
                d.x = 1F;
            }
            else if (a.x < 0)
            {
                w = b.x - a.x;

                c.x = 0F;
                d.x = w;
            }
            else if (b.x > 1)
            {
                w = b.x - a.x;

                c.x = 1F - w;
                d.x = 1F;
            }

            //-----
            if (a.y < 0 && b.y > 1)
            {
                c.y = 0F;
                d.y = 1F;
            }
            else if (a.y < 0)
            {
                h = b.y - a.y;

                c.y = 0F;
                d.y = h;
            }
            else if (b.y > 1)
            {
                h = b.y - a.y;

                c.y = 1F - h;
                d.y = 1F;
            }

            //---
            if (a.x < 0 || a.x > 1 ||
               a.y < 0 || a.y > 1 ||
               b.x < 0 || b.x > 1 ||
               b.y < 0 || b.y > 1)
            {

                //				rt.anchorMin = c;
                //				rt.anchorMax = d;
            }
            else
            {
                rt.anchorMin = a;
                rt.anchorMax = b;
                rt.offsetMin = Vector2.zero;
                rt.offsetMax = Vector2.zero;
            }
        }

        //2dUI计算3d世界坐标,默认计算的头顶的位置
        static Vector2 pos;
        public static void SetPosition(Canvas canvas, Vector3 position, RectTransform rt, float offsetX = 0, float offsetY = 0)
        {
            if (canvas != null)
            {
                RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Camera.main.WorldToScreenPoint(position), canvas.worldCamera, out pos);
                rt.anchoredPosition = pos + Vector2.up * 190 + new Vector2(offsetX, offsetY);
            }
        }

        
    }
}