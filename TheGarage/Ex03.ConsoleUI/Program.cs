public class Program
{
    public static void Main()
    {
        // Create garage
         Garage garage = new Garage();
         
        // Create garage UI
         GarageUI garageUI = new GarageUI(garage);

        // Start the work in the garage
         garageUI.StartWork();
    }
}