using SmartCafeteria.ProductMicroservice.DataAccess.Entities;

namespace SmartCafeteria.ProductMicroservice.DataAccess.MealFactory;

public class BeefBurger : Product
{
	public BeefBurger()
	{
		Name = "Beef Burger";
		Price = 13;
	}
}