using SmartCafeteria.ProductMicroservice.DataAccess.Entities;

namespace SmartCafeteria.ProductMicroservice.DataAccess.MealFactory;

public class ChickenNuggets : Product
{
	public ChickenNuggets()
	{
		Name = "Chicken Nuggets";
		Price = 8;
	}
}