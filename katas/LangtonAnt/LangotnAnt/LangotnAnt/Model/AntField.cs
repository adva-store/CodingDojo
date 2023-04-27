using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangotnAnt.Model;

public class AntField:ObservableObject
{
    public Point Position { get; private set; }
    public Color FieldColor { get; set; }
    public bool HasAnt { get; set; }

    public AntField(Point position, Color fieldColor)
    {
        Position = position;
        FieldColor = fieldColor;
    }
}

