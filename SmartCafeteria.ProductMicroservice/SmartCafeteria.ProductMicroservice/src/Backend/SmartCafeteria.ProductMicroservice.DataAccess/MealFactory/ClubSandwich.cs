using SmartCafeteria.ProductMicroservice.DataAccess.Entities;

namespace SmartCafeteria.ProductMicroservice.DataAccess.MealFactory;

public class ClubSandwich : Product
{
	public ClubSandwich()
	{
		Name = "Club Sandwich";
		Price = 15;
	}
}