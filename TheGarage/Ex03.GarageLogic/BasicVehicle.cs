/// <summary>
/// Abstract class that represents the basic vehicle. All vehicle classes inherit from it.
/// </summary>
using System.Collections.Generic;

public abstract class BasicVehicle
{
    // Declare of variables
    private string m_ModelName;
    private string m_LicensePlate;
    private List<Wheel> m_Wheels;
    private BasicEngine m_BasicEngine;

    // Constructor
    public BasicVehicle(string i_LicensePlate)
    {
        m_LicensePlate = i_LicensePlate;
    }

    // Properties
    public string ModelName
    {
        get
        {
            return m_ModelName;
        }

        set
        {
            m_ModelName = value;
        }
    }   

    public string LicensePlate
    {
        get
        {
            return m_LicensePlate;
        }

        set
        {
            m_LicensePlate = value;
        }
    }
        
    public List<Wheel> Wheels
    {
        get
        {
            return m_Wheels;
        }

        set
        {
            m_Wheels = value;
        }
    }
        
    public BasicEngine BasicEngine
    {
        get
        {
            return m_BasicEngine;
        }

        set
        {
            m_BasicEngine = value;
        }
    }

    // Method
    public override string ToString()
    {
        return string.Format("Model name: {0}.\nLicense plate: {1}.\nWheels:\n{2}.\nEngine:\n{3}.", m_ModelName, m_LicensePlate, m_Wheels[0].ToString(), m_BasicEngine.ToString());
    }
}