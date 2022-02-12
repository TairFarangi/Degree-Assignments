/// <summary>
/// Class that represents the enginge for electric vehicles.
/// </summary>
public class ElectricEngine : BasicEngine
{
    // Declare of variables
    private readonly float r_MaximumBatteryTime;
    private float m_BatteryTimeLeft;

    // Constructor
    public ElectricEngine(float i_BatteryTimeLeft, float i_MaximumBatteryTime, float i_EnergyLeft) : base(i_EnergyLeft)
    {
        r_MaximumBatteryTime = i_MaximumBatteryTime;
        m_BatteryTimeLeft = i_BatteryTimeLeft;
    }

    // Properties
    public float BatteryTimeLeft
    {
        get
        {
            return m_BatteryTimeLeft;
        }
    }

    // Methods
    public void BatteryCharging(float i_HoursToCharge)
    {
        if (m_BatteryTimeLeft + i_HoursToCharge > r_MaximumBatteryTime)
        {
            throw new ValueOutOfRangeException(0, r_MaximumBatteryTime);
        }

        m_BatteryTimeLeft += i_HoursToCharge;
        EnergyLeftPercent = m_BatteryTimeLeft / r_MaximumBatteryTime;
    }

    public override string ToString()
    {
        return string.Format("Battery time left: {0}.\nMaximum battery time: {1}.\n{2}", m_BatteryTimeLeft, r_MaximumBatteryTime, base.ToString());
    }
}