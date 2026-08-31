using UnityEngine;

[System.Serializable]
public class ParallaxLayer
{
    [SerializeField] private Transform background;
    [SerializeField] private float parallaxMultiplier;
    [SerializeField] private float imageWidthOffset = 10;

    private float imageFullWidth;
    private float imageHalfWidth;

    public void CalculateImageWidth()
    {
        imageFullWidth = background.GetComponent<SpriteRenderer>().bounds.size.x;
        imageHalfWidth = imageFullWidth / 2f;
    }

    public void Move(float distanceToMove)
    {
        background.position = background.position + new Vector3(distanceToMove * parallaxMultiplier, 0f, 0f);
    }

    public void LoopBackground(float cameraLeftEdge, float cameraRightEdge)
    {
        float backgroundLeftEdge = (background.position.x - imageHalfWidth) + imageWidthOffset;
        float backgroundRightEdge = (background.position.x + imageHalfWidth) - imageWidthOffset;

        if(backgroundRightEdge < cameraLeftEdge)
        {
            background.position = background.position + new Vector3(imageFullWidth, 0f, 0f);
        }
        else if(backgroundLeftEdge > cameraRightEdge)
        {
            background.position = background.position - new Vector3(imageFullWidth, 0f, 0f);
        }
    }
}
