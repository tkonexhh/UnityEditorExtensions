using System;
using System.Collections.Generic;
using System.Text;

public static class ArrayExtension
{
    public static T Find<T>(this T[] array, Predicate<T> match)
    {
        if (array == null || array.Length == 0 || match == null) return default(T);
        int size = array.Length;
        for (int i = 0; i < size; i++)
        {
            if (match(array[i])) return array[i];
        }
        return default(T);
    }

    public static T[] FindAll<T>(this T[] array, Predicate<T> match)
    {
        if (array == null || array.Length == 0 || match == null) return null;
        List<T> list = new List<T>();
        int size = array.Length;
        for (int i = 0; i < size; i++)
        {
            if (match(array[i])) list.Add(array[i]);
        }
        return list.ToArray();
    }

    public static int FindIndex<T>(this T[] array, Predicate<T> match)
    {
        if (array == null || array.Length == 0 || match == null) return -1;
        int size = array.Length;
        for (int i = 0; i < size; i++)
        {
            if (match(array[i])) return i;
        }
        return -1;
    }

    public static bool Exist<T>(this T[] array, Predicate<T> match)
    {
        if (array == null || array.Length == 0 || match == null) return false;
        int size = array.Length;
        for (int i = 0; i < size; i++)
        {
            if (match(array[i])) return true;
        }
        return false;
    }

    public static T[] RemoveAll<T>(this T[] array, Predicate<T> match)
    {
        if (array == null || array.Length == 0 || match == null) return null;
        List<T> list = new List<T>();
        int size = array.Length;
        for (int i = 0; i < size; i++)
        {
            if (match(array[i])) continue;
            list.Add(array[i]);
        }
        return list.ToArray();
    }

    /// <summary>
    /// 遍历
    /// </summary>
    /// <param name="action">遍历事件</param>
    public static T[] ForEach<T>(this T[] self, Action<int, T> action)
    {
        for (int i = 0; i < self.Length; i++)
        {
            action(i, self[i]);
        }
        return self;
    }

    /// <summary>
    /// 倒序遍历
    /// </summary>
    /// <param name="action">遍历事件</param>
    public static T[] ForEachReverse<T>(this T[] self, Action<T> action)
    {
        for (int i = self.Length - 1; i >= 0; i--)
        {
            action(self[i]);
        }
        return self;
    }

    /// <summary>
    /// 倒序遍历
    /// </summary>
    /// <param name="action">遍历事件</param>
    public static T[] ForEachReverse<T>(this T[] self, Action<int, T> action)
    {
        for (int i = self.Length - 1; i >= 0; i--)
        {
            action(i, self[i]);
        }
        return self;
    }
}
