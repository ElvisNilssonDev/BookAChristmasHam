
namespace BookAChristmasHam.Interfaces
{
    public interface IHasId
    {
        // Alla objekt som implementerar detta interface måste ha en Id property av typen int.
        // alltså en identifierare för objektet.
        int Id { get; set; }
    }
}
