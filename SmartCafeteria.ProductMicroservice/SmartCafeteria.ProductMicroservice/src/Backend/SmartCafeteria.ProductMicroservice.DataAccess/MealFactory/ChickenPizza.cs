using SmartCafeteria.ProductMicroservice.DataAccess.Entities;

namespace SmartCafeteria.ProductMicroservice.DataAccess.MealFactory;

public class ChickenPizza : Product
{
	public ChickenPizza()
	{
		Name = "Chicken Pizza";
		Price = 20;
	}
}