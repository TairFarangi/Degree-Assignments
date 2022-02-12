using System.Collections.Generic;

public class Truck : BasicVehicle
{
    // Declare of variables
    private const int k_NumOfWheels = 16;
    private const int k_MaxAirPressure = 26;
    private bool m_HasHazardousMaterials;
    private float m_CargoVolume;

    // Constructor
    public Truck(string i_LicensePlate) : base(i_LicensePlate)
    {
        base.Wheels = new List<Wheel>(k_NumOfWheels);

        for (int i = 0; i < k_NumOfWheels; i++)
        {
            base.Wheels.Add(new Wheel());
            base.Wheels[i].MaxAirPressure = k_MaxAirPressure;
        }
    }

    // Properties
    public bool HasHazardousMaterials
    {
        get
        {
            return m_HasHazardousMaterials;
        }

        set
        {
            m_HasHazardousMaterials = value;
        }
    }

    public float CargoVolume
    {
        get
        {
            return m_CargoVolume;
        }

        set
        {
            m_CargoVolume = value;
        }
    }

    // Method
    public override string ToString()
    {
        return string.Format("{0}\nHas hazardous materials? {1}.\nCargo volume: {2}.", base.ToString(), m_HasHazardousMaterials, m_CargoVolume);
    }
}
