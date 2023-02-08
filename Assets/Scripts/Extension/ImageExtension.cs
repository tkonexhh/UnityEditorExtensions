using UnityEngine;
using UnityEngine.UI;

public static class ImageExtension
{
    public static T SetSprite<T>(this T self, Sprite sprite) where T : Image
    {
        self.sprite = sprite;
        return self;
    }
}
