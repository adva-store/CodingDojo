using LangtonAnt.backend;

namespace LangtonAnt.Utils;
/// <summary>
/// Frontend Realisierung
/// Hilfsklasse für die Graphische-Ausgabe
/// rojekt: Langton's Ant Assessment
/// Autor: Petar Peev i.A. von Oliver Arentzen, advastore SE
/// </summary>
internal class AntDrawable : IDrawable
{
    /// <summary>
    /// Zeichnet die Ausgabe auf einem Leinwand
    /// </summary>
    /// <param name="canvas">die Leinwand</param>
    /// <param name="dirtyRect">das Rechteck zum aktualisieren</param>
    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        if (Matchfield != null)
        {
            // Zell-Eigenschaften ermitteln
            var rowHeight = (dirtyRect.Height - 1) / Matchfield.EdgeLenght;
            var columnWidth = (dirtyRect.Width - 1) / Matchfield.EdgeLenght;

            // Feld-Raster zeichnen
            canvas.StrokeColor = Colors.Black;
            
            for (int row = 0; row <= Matchfield.EdgeLenght; row++)
            {
                canvas.DrawLine(dirtyRect.X, dirtyRect.Y + row * rowHeight + 0.5F,
                        dirtyRect.Width, dirtyRect.Y + row * rowHeight + 0.5F);
                canvas.DrawLine(dirtyRect.X + row * columnWidth + 0.5F, dirtyRect.Y,
                        dirtyRect.X + row * columnWidth + 0.5F, dirtyRect.Height);

            }
            // Zellen-Inhalt zeichnen
            var allCells = Matchfield.CurrentResult?.Split(",");
            if (allCells != null)
            {
                for (int row = 0; row < Matchfield.EdgeLenght; row++)
                {
                    for (int column = 0; column < Matchfield.EdgeLenght; column++)
                    {
                        var cellIndex = row * Matchfield.EdgeLenght + column;
                        if (cellIndex >= 0 && cellIndex < allCells.Length)
                        {
                            canvas.FillColor = allCells[cellIndex].ToLower().StartsWith("s") ?
                            Colors.Black : Colors.White;
                            canvas.FillRectangle(new RectF(column * columnWidth + 1F, row * rowHeight + 1F, columnWidth - 2, rowHeight - 2));
                        }
                    }
                }
                //ToDo: evtl. mit der nächste MAUI Relese https://github.com/dotnet/maui/issues/6742
                //var image = new Image { Source = ImageSource.FromResource("ant.jpg") };
                //canvas.DrawImage((Microsoft.Maui.Graphics.IImage)image,dirtyRect.X + Matchfield.CurrentAntPosition.Column * columnWidth, dirtyRect.Y + Matchfield.CurrentAntPosition.Row * rowHeight,rowHeight, columnWidth);
                drawAntAt(Matchfield.AntCurrentOutlook, dirtyRect.X + Matchfield.CurrentAntPosition.Column * columnWidth, dirtyRect.Y + Matchfield.CurrentAntPosition.Row * rowHeight,
                          columnWidth,rowHeight, canvas);
            }
        }
    }
    /// <summary>
    /// das Feld mit den aktuellen Daten zum zeichnen
    /// </summary>
    public static IMatchfield Matchfield { get; set; }

    /// <summary>
    /// Workaround: Ameise schematisch mahlen
    /// </summary>
    /// <param name="outlook">die aktuelle Blickrichtung </param>
    /// <param name="x">Start X</param>
    /// <param name="y">Start Y</param>
    /// <param name="width">Breite</param>
    /// <param name="height">Höhe</param>
    /// <param name="canvas">Leinwand</param>
    private void drawAntAt(Outlook outlook, float x, float y, float width, float height, ICanvas canvas)
    {
        var antHeight = 2 * height / 3;
        var antWidth = 2*  width / 3;
        var heightMargin = (height - antHeight ) / 2;
        var xStart = 0F; 
        var yStart = 0F;
        var xEnd = 0F;
        var yEnd = 0F;
        
        canvas.StrokeColor = Colors.OrangeRed;

        if (outlook == Outlook.East || outlook == Outlook.West)
        {
            xStart = x + heightMargin;
            yStart = y + height / 2;
            xEnd = x + height - heightMargin;
            yEnd = y + height / 2;
        }
        else
        {
            xStart = x + width / 2;
            yStart = y + heightMargin;
            xEnd = x + width / 2;
            yEnd = y + height - heightMargin;
        }
        canvas.DrawLine(xStart, yStart, xEnd, yEnd);
        if(outlook == Outlook.East || outlook == Outlook.South)
            canvas.DrawCircle(xEnd, yEnd, 1.5F);
        else
            canvas.DrawCircle(xStart, yStart, 1.5F);
    }
}
