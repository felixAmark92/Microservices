using SmartCafeteria.ProductMicroservice.DataAccess.Entities;

namespace SmartCafeteria.ProductMicroservice.DataAccess.MealFactory;

public class MealFactory
{
	public static IProduct CreateMeal(MealType mealType)
	{
		IProduct product = mealType switch
		{
			MealType.ClubSandwich => new ClubSandwich(),
			MealType.ChickenBurger => new ChickenBurger(),
			MealType.BeefBurger => new BeefBurger(),
			MealType.ChickenPizza => new ChickenPizza(),
			MealType.FishNChips => new FishNChips(),
			MealType.ChickenNuggets => new ChickenNuggets(),
			MealType.ChickenSalad => new ChickenSalad(),
			_ => throw new ArgumentOutOfRangeException(nameof(mealType), mealType, null)
		};

		return product;
	}
}
