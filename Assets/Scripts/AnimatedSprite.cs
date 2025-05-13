using UnityEngine;

public class AnimatedSprite : MonoBehaviour
{
    public Sprite[] sprites;
    public float framerate = 1f / 6f;

    private SpriteRenderer spriteRenderer;
    private int currentFrame;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        InvokeRepeating(nameof(animate), framerate, framerate);
    }

    private void onDisable()
    {
        CancelInvoke();
    }

    private void animate()
    {
        currentFrame++;

        if(currentFrame >= sprites.Length)
        {
            currentFrame = 0;
        }
        if (currentFrame >= 0 &&  currentFrame < sprites.Length)
        {
            spriteRenderer.sprite = sprites[currentFrame];
        }
       
    }
}
