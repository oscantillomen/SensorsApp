namespace DemoTaller.Models
{
    public enum MenuItemType
    {
        About,
        FlashLight,
        Geocoding
    }

    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
