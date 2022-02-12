using System.Collections.Generic;

public class Motorcycle : BasicVehicle
{
    // Declare of variables
    private const int k_NumOfWheels = 2;
    private const int k_MaxAirPressure = 28;

    private eLicenseType m_LicenseType;
    private int m_EngineVolume;

    // Constructor
    public Motorcycle(string i_LicensePlate) : base(i_LicensePlate)
    {
        base.Wheels = new List<Wheel>(k_NumOfWheels);

        for (int i = 0; i < k_NumOfWheels; i++)
        {
            base.Wheels.Add(new Wheel());
            base.Wheels[i].MaxAirPressure = k_MaxAirPressure;
        }        
    }

    // Properties
    public eLicenseType LicenseType
    {
        get
        {
            return m_LicenseType;
        }

        set
        {
            m_LicenseType = value;
        }
    }

    public int EngineVolume
    {
        get
        {
            return m_EngineVolume;
        }

        set
        {
            m_EngineVolume = value;
        }
    }

    // Method
    public override string ToString()
    {
        return string.Format("{0}\nLicense type: {1}.\nEngine Volume: {2}", base.ToString(), m_LicenseType, m_EngineVolume);
    }
}