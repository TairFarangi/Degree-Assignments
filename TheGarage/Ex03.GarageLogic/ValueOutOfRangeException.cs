/// <summary>
/// Exception class that will be thrown in case of non-suitable input in a desired value range.
/// </summary>
using System;

public class ValueOutOfRangeException : Exception
{
    // Declare of variables
    private float m_MinValue;
    private float m_MaxValue;

    // Constructors
    public ValueOutOfRangeException(float i_MinValue, float i_MaxValue) : base(string.Format("Out of range: {0} - {1}. Try again.", i_MinValue, i_MaxValue))
    {
        m_MaxValue = i_MaxValue;
        m_MinValue = i_MinValue;
    }
}