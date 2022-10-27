using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using GodotSharpSome.Drawing2D;
using static Godot.Mathf;
using V = Godot.Vector2;

public class Node2D : Godot.ColorRect
{
    private static Color BackColor = Color.ColorN("white");
    private static Color LineColor = Color.ColorN("black");
    private static Color AreaColor = Color.ColorN("lightgray");
    private static Color TextColor = Color.ColorN("black");
    private Grid _grid = new Grid(10, 100, 100);

    public override void _Ready()
    {
        Color = BackColor;
    }

    public override void _Draw()
    {
        DrawCrosses(1);

        DrawArrows(2);

        DrawSegmenedLines(3);

        DrawRectangles(4);
    }

    private void DrawCrosses(int row)
    {
        DrawMultiline(
            Multiline.Cross(_grid.Middle(row, column: 1), radius: 15),
            LineColor);

        DrawMultiline(
            Multiline.Cross2(_grid.Middle(row, column: 2), outerRadius: 15, innerRadius: 5),
            LineColor, width: 2);
    }

    private void DrawArrows(int row)
    {
        DrawMultiline(
            Multiline.Arrow(_grid.LeftBottom(row, column: 1), _grid.RightTop(row, column: 1), headRadius: 15),
            LineColor);

        DrawMultiline(
            Multiline.Arrow(_grid.LeftBottom(row, column: 2), _grid.RightTop(row, column: 2), headRadius: 10, arrowAngle: Pi * 5 / 6),
            LineColor);

        DrawMultiline(
            Multiline.DoubleArrow(_grid.LeftBottom(row, column: 3), _grid.RightTop(row, column: 3), headRadius: 15),
            LineColor);

        // dimmension length example
        var column = 4;
        var a = _grid.LeftMiddle(row, column);
        var b = _grid.RightMiddle(row, column);
        var points = new List<Vector2>(14);
        Multiline.AppendLine(points, a + new V(0, 8), a + new V(0, -4));
        Multiline.AppendLine(points, b + new V(0, 8), b + new V(0, -4));
        Multiline.AppendDoubleArrow(points, a, b, 16);
        DrawMultiline(points.ToArray(), LineColor);
        DrawString(GetFont(null), _grid.Middle(row, column) + new V(-8, -3), "42", TextColor);
    }

    private void DrawSegmenedLines(int row)
    {
        DrawMultiline(
            Multiline.SegmentedLine(_grid.LeftMiddle(row, column: 1), _grid.RightMiddle(row, column: 1), segmentCount: 4),
            LineColor);

        DrawMultiline(
            Multiline.SegmentedArrow(_grid.LeftTop(row, column: 2), _grid.RightBottom(row, column: 3), segmentSize: 20, headRadius: 15),
            LineColor);
    }

    private void DrawRectangles(int row)
    {
        DrawMultiline(
           Multiline.Rectangle(_grid.Middle(row, column: 1), halfLength: 40, halfWidth: 20, angle: Pi/5),
           LineColor);
    }
}
