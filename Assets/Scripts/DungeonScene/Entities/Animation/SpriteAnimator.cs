
using Assets.Scripts.Datas.Enum;
using Assets.Scripts.Datas.Members;
using Assets.Scripts.Datas.Texture;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    public enum AnimationEnum
    {
        idle,
        walk,
        run,
    }

    public SpriteRenderer spriteRenderer;
    public SpriteRenderer shadowSpriteRenderer;
    public SpriteRenderer directionSpriteRenderer;

    public Sprite[] directions;

    private TextureTama textureTama;
    private int currentAnimationRow;
    private float timeTillNextFrame;
    private int currentFrame;
    public int Frames = 4;

    public AnimationEnum animationType = AnimationEnum.walk;


    public GameCharacterDirection currentDirection { get; private set; }

    public void Init(string tama)
    {
        textureTama = TextureGlobal.GetTextureTama(tama);
        spriteRenderer.gameObject.transform.localPosition = new Vector3(0, 0.5f, 0);

        if (shadowSpriteRenderer != null)
        {
            shadowSpriteRenderer.gameObject.transform.localPosition = new Vector3(0, 0.5f, 0);
        }

        ResetToIdle();
    }
   
    private int GetRow(GameCharacterDirection direction)
    {
        if (textureTama.IsSimpleSheet)
        {
            return 0;
        }

        switch (direction)
        {
            case GameCharacterDirection.Down:
                return 0;
            case GameCharacterDirection.Right:
                return 1;
            case GameCharacterDirection.Left:
                return 2;
            case GameCharacterDirection.Up:
                return 3;
            default:
                return 0;
        }
    }
    public void FixedUpdate()
    {
        if (timeTillNextFrame > 0f)
        {
            timeTillNextFrame -= Time.fixedDeltaTime;
        }
        else
        {
            currentFrame = (currentFrame + 1) % Frames;

            if (animationType != AnimationEnum.idle)
            {
                timeTillNextFrame = textureTama.Duration / 2f;
            }
            else
            {
                timeTillNextFrame = textureTama.Duration;
            }

            //   timeTillNextFrame = Duration / Frames;
            UpdateSprite();
        }
    }

    public void SetDirection(GameCharacterDirection direction)
    {
        currentAnimationRow = GetRow(direction);
        currentDirection = direction;
        directionSpriteRenderer.sprite = directions[(int)currentDirection];
        UpdateSprite();
        if (textureTama.IsSimpleSheet && (direction == GameCharacterDirection.Left || direction == GameCharacterDirection.Right))
        {
            spriteRenderer.flipX = direction == GameCharacterDirection.Left;
        }

    }
    public void SetAnim(AnimationEnum animationEnum)
    {
        if (animationEnum == AnimationEnum.idle)
        {
            ResetToIdle();
        }
        else if (animationEnum == AnimationEnum.walk)
        {
            ResetToWalk();
        }
        else
        {
            ResetToRun();
        }
    }
    public void ResetToRun()
    {
        if (animationType != AnimationEnum.run)
        {
            animationType = AnimationEnum.run;
            Frames = textureTama.FramesRun;
            currentFrame = currentFrame % Frames;
            UpdateSprite();
        }
    }
    public void ResetToWalk()
    {
        if (animationType !=AnimationEnum.walk)
        {
            animationType = AnimationEnum.walk;
            Frames = textureTama.FramesWalk;
            currentFrame = currentFrame % Frames;
            UpdateSprite();
        }
    }
    public void ResetToIdle()
    {
        if (animationType != AnimationEnum.idle)
        {
            animationType = AnimationEnum.idle;
            Frames = textureTama.FramesIdle;
            currentFrame = currentFrame % Frames;
            UpdateSprite();
        }
    }
    private void UpdateSprite()
    {
        //         Debug.Log(currentAnimationRow + " " + currentFrame);
        if (animationType == AnimationEnum.idle)
        {
            spriteRenderer.sprite = textureTama.spritesIdle[currentAnimationRow][currentFrame];
            if (shadowSpriteRenderer != null)
            {
                shadowSpriteRenderer.sprite = textureTama.spritesShadowIdle[currentAnimationRow][currentFrame % textureTama.spritesShadowIdle[0].Length];
            }

        }
        else if (animationType == AnimationEnum.walk)
        {
            spriteRenderer.sprite = textureTama.spritesWalk[currentAnimationRow][currentFrame];
            if (shadowSpriteRenderer != null)
            {
                shadowSpriteRenderer.sprite = textureTama.spritesShadowWalk[currentAnimationRow][currentFrame % textureTama.spritesShadowWalk[0].Length];
            }
        }
        else if (animationType == AnimationEnum.run)
        {
            spriteRenderer.sprite = textureTama.spritesRun[currentAnimationRow][currentFrame];
            if (shadowSpriteRenderer != null)
            {
                shadowSpriteRenderer.sprite = textureTama.spritesShadowRun[currentAnimationRow][currentFrame % textureTama.spritesShadowRun[0].Length];
            }
        }
    }
}
