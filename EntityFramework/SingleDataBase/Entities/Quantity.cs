namespace SingleDataBase.Entities;

public readonly record struct Quantity
{
    public Quantity(decimal value, QuantityType type)
    {
        if (type == QuantityType.Pieces && value % 1 != 0)
        {
            throw new ArgumentException("Value must be an integer for pieces");
        }

        Type = type;
        Value = value;
    }

    public decimal Value { get; init; }
    public QuantityType Type { get; init; }
}
