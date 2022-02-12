/// <summary>
/// Class that represent the garage. contains all the garage cards, vehicles, and the functionality of the vehicles in the garage.
/// </summary>
using System.Collections.Generic;

public class Garage
{
    // Declare of variable
    private List<VehicleCard> m_GarageCards;

    // Constructor  
    public Garage()
    {
        m_GarageCards = new List<VehicleCard>();
    }

    // Properties
    public List<VehicleCard> GarageCards
    {
        get
        {
            return m_GarageCards;
        }
    }

    // Methods
    public void AddNewVehicle(VehicleCard i_VehicleCard)
    {
        m_GarageCards.Add(i_VehicleCard);
    }
   
    public List<string> LicencePlates(eVehicleStatus i_VehicleStatus)
    {
        // List of all the licence plates by vehicle status
        List<string> licensePlateList = new List<string>(); 

        if (i_VehicleStatus == eVehicleStatus.All)
        {
            foreach (VehicleCard vehicleCard in m_GarageCards)
            {
                licensePlateList.Add(vehicleCard.Vehicle.LicensePlate);
            }
        }
        else
        {
            foreach (VehicleCard vehicleCard in m_GarageCards)
            {
                if (vehicleCard.VehicleStatus == i_VehicleStatus)
                {
                    licensePlateList.Add(vehicleCard.Vehicle.LicensePlate);
                }
            }
        }

        return licensePlateList;
    }
    
    public void ChangeVehicleStatus(string i_LicensePlate, eVehicleStatus i_vehicleStatus, ref bool io_VehicleIsFound)
    {
        io_VehicleIsFound = false;

        foreach (VehicleCard vehicleCard in m_GarageCards)
        {
            if (vehicleCard.Vehicle.LicensePlate == i_LicensePlate)
            {
                vehicleCard.VehicleStatus = i_vehicleStatus;
                io_VehicleIsFound = true;
                break;
            }
        }
    }

    public void InflateWheels(string i_LicensePlate, ref bool io_VehicleIsFound)
    {
        io_VehicleIsFound = false;

        foreach (VehicleCard garageCard in m_GarageCards)
        {
            if (garageCard.Vehicle.LicensePlate == i_LicensePlate)
            {                
                float currentAirPressure = garageCard.Vehicle.Wheels[0].CurrentAirPressure;
                float maximumAirPressure = garageCard.Vehicle.Wheels[0].MaxAirPressure;
                float airToAdd = maximumAirPressure - currentAirPressure;

                foreach (Wheel wheel in garageCard.Vehicle.Wheels)
                {
                    wheel.inflatingWheels(airToAdd);
                }

                io_VehicleIsFound = true;
                break;
            }
        }
    }
    
    public void VehicleFueling(string i_LicensePlate, eFuelType i_FuelType, float i_QuantityOfLiterToAdd, ref bool io_VehicleIsFound, ref bool io_IsSuitableEngine)
    {
        io_VehicleIsFound = false;
        io_IsSuitableEngine = false;

        foreach (VehicleCard vehicleCard in m_GarageCards)
        {
            if (vehicleCard.Vehicle.LicensePlate == i_LicensePlate)
            {
                io_VehicleIsFound = true;

                if (vehicleCard.Vehicle.BasicEngine is ElectricEngine)
                {
                    io_IsSuitableEngine = false;
                }
                else
                {
                    FuelEngine fuelEngine = vehicleCard.Vehicle.BasicEngine as FuelEngine;
                    fuelEngine.AddFuel(i_QuantityOfLiterToAdd, i_FuelType);
                    io_IsSuitableEngine = true;
                    break;
                }                
            }
        }
    }
    
    public void ChargeElectricVehicle(string i_LicensePlate, float i_MinutesToCharge, ref bool io_VehicleIsFound, ref bool io_IsSuitableEngine)
    {
        io_VehicleIsFound = false;
        io_IsSuitableEngine = false;
        
        foreach (VehicleCard vehicleCard in m_GarageCards)
        {
            if (vehicleCard.Vehicle.LicensePlate == i_LicensePlate)
            {
                io_VehicleIsFound = true;

                if (vehicleCard.Vehicle.BasicEngine is FuelEngine)
                {
                    io_IsSuitableEngine = false;
                }
                else
                {
                    float minutesInHour = 60;
                    float hoursToCharge = i_MinutesToCharge / minutesInHour;
                    ElectricEngine electricEngine = vehicleCard.Vehicle.BasicEngine as ElectricEngine;
                    electricEngine.BatteryCharging(hoursToCharge);
                    io_IsSuitableEngine = true;
                    break;
                }                
            }
        }
    }

    public VehicleCard VehicleDetails(string i_LicensePlate, ref bool io_VehicleIsFound)
    {
        io_VehicleIsFound = false;
        VehicleCard vehicleCardToUser = null;

        foreach (VehicleCard vehicleCard in m_GarageCards)
        {
            if (vehicleCard.Vehicle.LicensePlate == i_LicensePlate)
            {
                vehicleCardToUser = vehicleCard;
                io_VehicleIsFound = true;
                break;
            }
        }

        return vehicleCardToUser;
    }
}