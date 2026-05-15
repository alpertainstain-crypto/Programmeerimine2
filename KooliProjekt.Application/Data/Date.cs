using System;

public class Date
{
    public DateTime DateValue { get; set; }

    public Date() { }

    public Date(DateTime dateValue)
    {
        DateValue = dateValue;
    }

    protected bool Equals(Date other)
    {
        return DateValue.Equals(other.DateValue);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Date)obj);
    }

    public override int GetHashCode()
    {
        return DateValue.GetHashCode();
    }
}
