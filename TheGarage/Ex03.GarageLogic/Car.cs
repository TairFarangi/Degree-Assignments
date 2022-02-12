using System.Collections.Generic;

public class Car : BasicVehicle
{
    // Declare of variables
    private const int k_NumOfWheels = 4;
    private const int k_MaxAirPressure = 30;
    private eColor m_Color;
    private eNumberofDoors m_NumberOfDoors;

    // Constructor 
    public Car(string i_LicensePlate) : base(i_LicensePlate)
    {
        base.Wheels = new List<Wheel>(k_NumOfWheels);

        for (int i = 0; i < k_NumOfWheels; i++)
        {
            base.Wheels.Add(new Wheel());
            base.Wheels[i].MaxAirPressure = k_MaxAirPressure;
        }
    }

    // Properties
    public eColor Color
    {
        get
        {
            return m_Color;
        }

        set
        {
            m_Color = value;
        }
    }

    public eNumberofDoors NumberOfDoors
    {
        get
        {
            return m_NumberOfDoors;
        }

        set
        {
            m_NumberOfDoors = value;
        }
    }

    // Method
    public override string ToString()
    {
        return string.Format("{0}\nColor: {1}.\nNumber of doors: {2}.", base.ToString(), m_Color, m_NumberOfDoors);
    }
}
