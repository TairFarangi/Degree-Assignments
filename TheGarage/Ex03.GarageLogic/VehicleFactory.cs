/// <summary>
/// Class that creates objects of the vehicle classes.
/// </summary>
public class VehicleFactory
{
    // Method
    public static BasicVehicle getVehicle(string i_LicensePlate, eVehicleType i_VehicleType)
    {
        BasicVehicle newVehicle = null;

        if(i_VehicleType == eVehicleType.BasicCar || i_VehicleType == eVehicleType.ElectricCar)
        {
            newVehicle = new Car(i_LicensePlate);
        }
        else if(i_VehicleType == eVehicleType.BasicMotorcycle || i_VehicleType == eVehicleType.ElectricMotorcycle)
        {
            newVehicle = new Motorcycle(i_LicensePlate);
        }
        else
        {
            newVehicle = new Truck(i_LicensePlate);
        }

        return newVehicle;
    }
}