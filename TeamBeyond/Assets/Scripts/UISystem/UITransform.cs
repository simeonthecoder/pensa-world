public class UITransform
{
    private const float DefaultSizeX = 10;
    private const float DefaultSizeY = 10;

    public float x;
    public float y;

    public float sizeX;
    public float sizeY;

    public UITransform(float x, float y)
    {
        this.sizeX = DefaultSizeX;
        this.sizeY = DefaultSizeY;
    }

    public UITransform(float x, float y, float sizeX, float sizeY)
    {
        this.x = x;
        this.y = y;
        this.sizeX = sizeX;
        this.sizeY = sizeY;
    }
}