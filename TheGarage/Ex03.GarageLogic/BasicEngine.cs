/// <summary>
/// Class that represent the basic engine that exists in all vehicles.
/// </summary>
public class BasicEngine
{
    private float m_EnergyLeftPercent;

    // Constructor
    public BasicEngine(float i_EnergyLeftPercent)
    {
        m_EnergyLeftPercent = i_EnergyLeftPercent;
    }

    // Properties
    public float EnergyLeftPercent
    {
        get
        {
            return m_EnergyLeftPercent;
        }

        set
        {
            if (value > 0)
            {
                m_EnergyLeftPercent = value;
            }
        }
    }

    // Method
    public override string ToString()
    {
        return string.Format("Energy left: {0}%.", m_EnergyLeftPercent);
    }
}