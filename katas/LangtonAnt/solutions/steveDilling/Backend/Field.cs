namespace Backend;

public class Field
{
    public Field(FieldColor color)
    {
        Color = color;
    }

    public FieldColor Color { private set; get; }

    public void ChangeColor()
    {
        Color = Color == FieldColor.Black
            ? FieldColor.White
            : FieldColor.Black;
    }
}