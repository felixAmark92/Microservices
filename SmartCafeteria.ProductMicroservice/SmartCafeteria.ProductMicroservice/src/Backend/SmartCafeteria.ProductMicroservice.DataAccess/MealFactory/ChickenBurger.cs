using SmartCafeteria.ProductMicroservice.DataAccess.Entities;

namespace SmartCafeteria.ProductMicroservice.DataAccess.MealFactory;

public class ChickenBurger : Product
{
	public ChickenBurger()
	{
		Name = "Chicken Burger";
		Price = 12;
	}
}