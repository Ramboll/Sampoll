namespace Sampøll;

public class Constants
{
    public static readonly int Margin = 10;
    public static readonly int HeaderHeight = 50;
    public static readonly int HeaderWidth = 300;
    public static readonly int RowHeight = 30;
    public static readonly int LabelWidth = 100;
    public static readonly int LabelHeight = 20;
    public static readonly int InputWidth = 200;
    public static readonly int InputHeight = 20;
}

public class PaperDefinition
{
    public string Id { get; set; }
    public int MarginTop { get; set; }
    public int MarginBottom { get; set; }
    public int MarginMiddle { get; set; }
    public int MarginLeft { get; set; }
    public int MarginRight { get; set; }
    public int Columns { get; set; }
    public int Rows { get; set; }

    public PaperDefinition(string id, int marginTop, int marginBottom, int marginMiddle, int marginLeft, int marginRight, int columns, int rows)
    {
        Id = id;
        MarginTop = marginTop;
        MarginBottom = marginBottom;
        MarginMiddle = marginMiddle;
        MarginLeft = marginLeft;
        MarginRight = marginRight;
        Columns = columns;
        Rows = rows;
    }
}

public static class PaperDefinitions
{
    private static readonly Dictionary<string, PaperDefinition> Definitions = new Dictionary<string, PaperDefinition>
    {
        {
            "3x7", new PaperDefinition("3x7", 0, 0, 0, 0, 0, 3, 7)
        },
        {
            "3x8", new PaperDefinition("3x8", 0, 0, 0, 0, 0, 3, 8)
        },
        {
            "2x7", new PaperDefinition("2x7", 10, 10, 2, 5, 5, 2, 7)
        }
    };

    public static PaperDefinition GetDefinitionById(string id)
    {
        if (Definitions.TryGetValue(id, out PaperDefinition definition))
        {
            return definition;
        }
        throw new ArgumentException($"No paper definition found for id '{id}'");
    }
}