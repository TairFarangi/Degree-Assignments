/// <summary>
/// Class that represent the vehicle wheels in the garage.
/// </summary>
public class Wheel
{
    // Declare of variables
    private float m_MaxAirPressure;
    private string m_ManufacturernName;
    private float m_CurrentAirPressure;

    // Constructor
    public Wheel()
    {
        m_ManufacturernName = string.Empty;
        m_CurrentAirPressure = 0;
    }

    // Propeties
    public float CurrentAirPressure
    {
        get
        {
            return m_CurrentAirPressure;
        }

        set
        {
            if (m_CurrentAirPressure > m_MaxAirPressure)
            {
                throw new ValueOutOfRangeException(0, m_MaxAirPressure);
            }

            m_CurrentAirPressure = value;
        }
    }

    public float MaxAirPressure
    {
        get
        {
            return m_MaxAirPressure;
        }

        set
        {
            m_MaxAirPressure = value;
        }
    }

    public string ManufacturernName
    {
        get
        {
            return m_ManufacturernName;
        }

        set
        {
            m_ManufacturernName = value;
        }
    }

    // Methods
    public void inflatingWheels(float i_AirToAdd)
    {
        if (m_CurrentAirPressure + i_AirToAdd > m_MaxAirPressure)
        {
            throw new ValueOutOfRangeException(0, m_MaxAirPressure);
        }

        m_CurrentAirPressure += i_AirToAdd;
    }

    public override string ToString()
    {
        return string.Format("Manufacturern name: {0}.\nCurrent air pressure: {1}.\nMaximumAirPressure: {2}.", m_ManufacturernName, m_CurrentAirPressure, m_MaxAirPressure);
    }
}